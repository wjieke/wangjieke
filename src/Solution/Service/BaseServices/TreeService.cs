using IServices.IBaseServices;
using Model.ModelBase;
using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.BaseServices
{
    /// <summary>
    /// 基类Tree结构服务
    /// </summary>
    /// <typeparam name="TTreeModel">Tree结构模型类，必须继承TreeBase模型基类</typeparam>
    public class TreeService<TTreeModel> : BaseService<TTreeModel>, ITreeService<TTreeModel>
        where TTreeModel : TreeBase<TTreeModel>
    {
        #region 同步

        /// <summary>
        /// 获取对象父id集合
        /// </summary>
        /// <param name="id">对象id</param>
        /// <returns></returns>
        private List<int> GetIds(int? id = null)
        {
            List<int> idList = new List<int>();
            var obj = base.Find(id);
            if (obj.ParentId == null)
            {
                return idList;
            }
            else
            {
                var ids = GetIds(obj.ParentId);
                foreach (var item in ids)
                {
                    idList.Add(item);
                }
                idList.Add(obj.Id);
            }
            return idList;
        }

        /// <summary>
        /// 查询(根据ID查询单条数据信息)
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        public override QueryResultInfo<TTreeModel> GetInfo(int id)
        {
            try
            {
                var ids = GetIds(id);
                var res = base.GetInfo(id);
                if (res.Data != null)
                {
                    res.Data.Ids = ids;
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取级联器数据
        /// </summary>
        /// <param name="id">节点父id</param>
        /// <returns></returns>
        public virtual List<TTreeModel> GetNode(int? id = null)
        {
            var list = base.GetList().Datas;
            var newList = list.Where(m => m.ParentId == id).ToList();
            return newList;
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <returns></returns>
        //public virtual List<NodeModelDto<TTreeModel>> GetTree()
        //{
        //    List<NodeModelDto<TTreeModel>> list = new List<NodeModelDto<TTreeModel>>()
        //    {
        //        new NodeModelDto<TTreeModel>
        //        {
        //            Id=0,
        //            NodeName="节点信息",
        //            Children= GetNode()
        //        }
        //    };
        //    return list;
        //}
        #endregion

        #region 异步
        /// <summary>
        /// 获取对象父id集合
        /// </summary>
        /// <param name="id">对象id</param>
        /// <returns></returns>
        private async Task<List<int>> GetIdsAsync(int? id = null)
        {
            List<int> idList = new List<int>();
            var obj = await base.FindAsync(id);
            if (obj.ParentId == null)
            {
                return idList;
            }
            else
            {
                var ids = await GetIdsAsync(obj.ParentId);
                foreach (var item in ids)
                {
                    idList.Add(item);
                }
                idList.Add(obj.Id);
            }
            return idList;
        }

        /// <summary>
        /// 查询(根据ID查询单条数据信息)
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        public override async Task<QueryResultInfo<TTreeModel>> GetInfoAsync(int id)
        {
            try
            {
                var ids = await GetIdsAsync(id);
                var res = await base.GetInfoAsync(id);
                if (res.Data != null)
                {
                    res.Data.Ids = ids;
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取级联器数据
        /// </summary>
        /// <param name="id">节点父id</param>
        /// <returns></returns>
        public virtual async Task<List<TTreeModel>> GetNodeAsync(int? id = null)
        {
            var list = await base.GetListAsync();
            var newList = list.Datas.Where(m => m.ParentId == id).ToList();
            return newList;
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <returns></returns>
        //public virtual async Task<List<NodeModelDto<TTreeModel>>> GetTreeAsync()
        //{
        //    List<NodeModelDto<TTreeModel>> list = new List<NodeModelDto<TTreeModel>>()
        //    {
        //        new NodeModelDto<TTreeModel>
        //        {
        //            Id=0,
        //            NodeName="节点信息",
        //            Children=await GetNodeAsync()
        //        }
        //    };
        //    return list;
        //}
        #endregion
    }
}