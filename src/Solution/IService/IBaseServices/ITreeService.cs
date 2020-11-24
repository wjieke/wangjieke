using Model.ModelBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IServices.IBaseServices
{
    /// <summary>
    /// 基类Tree结构服务接口
    /// </summary>
    /// <typeparam name="TTreeModel">Tree结构模型类，必须继承TreeBase模型基类</typeparam>
    public interface ITreeService<TTreeModel> : IBaseService<TTreeModel>
        where TTreeModel : TreeBase<TTreeModel>
    {
        #region 同步

        /// <summary>
        /// 获取级联器数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<TTreeModel> GetNode(int? id = null);

        #endregion

        #region 异步

        /// <summary>
        /// 获取级联器数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<TTreeModel>> GetNodeAsync(int? id = null);

        #endregion
    }
}