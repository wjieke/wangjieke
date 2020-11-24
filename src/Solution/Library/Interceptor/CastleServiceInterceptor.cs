using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Interceptor
{
    /// <summary>
    /// 暂时未用
    /// </summary>
    public class CastleServiceInterceptor: StandardInterceptor
    {
        /// <summary>
        /// 调用前的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("调用前的拦截器，方法名是：{0}。", invocation.Method.Name);// 方法名   获取当前成员的名称。 
        }

        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine("拦截的方法返回时调用的拦截器，方法名是：{0}。", invocation.Method.Name);
            base.PerformProceed(invocation);
        }

        /// <summary>
        /// 调用后的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("调用后的拦截器，方法名是：{0}。", invocation.Method.Name);
        }
    }
}
