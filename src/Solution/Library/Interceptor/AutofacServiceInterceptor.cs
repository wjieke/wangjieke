using Castle.DynamicProxy;
using Library.Attributes;
using Library.Tool;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace Library.Interceptor
{
    /// <summary>
    /// Autofac 服务接口拦截器
    /// </summary>
    public class AutofacServiceInterceptor : IInterceptor
    {
        private static IMemoryCache _cache;
        public AutofacServiceInterceptor(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        private readonly MemoryCacheTool cache = new MemoryCacheTool(_cache);

        /// <summary>
        /// 服务方法调用拦截处理
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            //方法执行前
            //Console.WriteLine("你正在调用方法 \"{0}\"  参数是 {1}... ", invocation.Method.Name, string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

            MethodInfo methodInfo = invocation.MethodInvocationTarget;
            if (methodInfo == null)
            {
                methodInfo = invocation.Method;
            }
            TransactionHandleAttribute transactionHandleAttribute = methodInfo.GetCustomAttributes<TransactionHandleAttribute>(true).FirstOrDefault();
            /*
             * 如果当前调用的方法标记了事务处理特性属性(TransactionHandleAttribute)，就调用事务处理方法进行事务处理完成之后退出，不执行之后代码
             * 事务处理操作的一般是增删改，所以有事务时，不进行缓存操作。
             */
            if (transactionHandleAttribute != null)
            {
                TransactionHandle(invocation, transactionHandleAttribute);
                return;
            }

            /*---------------------------------------------------------------------------------------------------------------------------------*/

            /*
             * todo
             * 问题1：缓存方法一般为查询方法，还需要进一步过滤掉增删改操作方法
             * 问题2：添加完信息之后，应该重新查询数据库一次刷新页面，而不是又根据缓存建拿缓存数据，不然以后没添加成功，实际上是添加成功了。
             */
            var cacheKey = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
            var isExistsCacheKey = cache.Exists(cacheKey);
            if (isExistsCacheKey)
            {
                //如果有缓存，读取缓存设置方法返回值，返回不继续执行方法
                invocation.ReturnValue = cache.Get(cacheKey);


                return;
            }

            /*---------------------------------------------------------------------------------------------------------------------------------*/

            invocation.Proceed();//继续执行当前调用拦截的方法

            //方法执行后
            //Console.WriteLine("方法执行完毕，返回结果：{0}", invocation.ReturnValue);

            var cacheValue = invocation.ReturnValue;
            if (isExistsCacheKey == false)
            {
                //如果没有缓存当前调用的方法返回值，设置缓存
                cache.Add(cacheKey, cacheValue);
            }
        }

        /// <summary>
        /// 事务处理方法
        /// </summary>
        /// <param name="invocation">当前调用对象</param>
        /// <param name="transactionHandleAttribute">事务处理特性</param>
        public void TransactionHandle(IInvocation invocation, TransactionHandleAttribute transactionHandleAttribute)
        {
            TransactionOptions transactionOptions = new TransactionOptions()
            {
                //设置事务隔离级别
                IsolationLevel = transactionHandleAttribute.IsolationLevel,
                //设置事务超时时间为60秒
                Timeout = new TimeSpan(0, 0, transactionHandleAttribute.Timeout)
            };
            using (TransactionScope scope = new TransactionScope(transactionHandleAttribute.ScopeOption, transactionOptions))
            {
                try
                {
                    //继续调用事务性方法
                    invocation.Proceed();
                    //事务范围中所有操作都已成功完成
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // 这个快捕获异常可在服务层记录异常信息（通常在API接口层做异常日志记录也可以）
                    throw ex;
                }
            }
        }
    }
}