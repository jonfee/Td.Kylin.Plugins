using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary.Operator;
using Td.Kylin.SMS.Data;

namespace Td.Kylin.SMS.Services
{
    /// <summary>
    /// 区域运营商资产数据服务
    /// </summary>
    public class OperatorAssetsService
    {
        /// <summary>
        ///  获取剩余资产数量 
        /// </summary>
        /// <param name="operatorId">运营商ID</param>
        /// <param name="assetsType">资产类型</param>
        /// <returns></returns>
        public int GetAssetsBalance(long operatorId, OperatorAssetsType assetsType)
        {
            using (var db = new DataContext())
            {
               return  db.AreaOperator_Assets.Where(
                        p => p.OperatorID == operatorId && p.AssetsType == (int) assetsType).Select(p=>p.Balance).FirstOrDefault();
            }
        }

        /// <summary>
        /// 资产使用更新
        /// </summary>
        /// <param name="operatorId">运营商ID</param>
        /// <param name="assetsType">资产类型</param>
        /// <param name="useNumber">本次使用量</param>
        /// <returns></returns>
        public bool UseAssets(long operatorId, OperatorAssetsType assetsType, int useNumber)
        {
            using (var db = new DataContext())
            {
                //指定类型的资产
                var assets = db.AreaOperator_Assets.FirstOrDefault(p => p.OperatorID == operatorId && p.AssetsType == (int)assetsType);

                if (assets == null) throw new InvalidOperationException("资产信息异常");
                if (assets.Balance < useNumber) throw new ArgumentOutOfRangeException(nameof(assets.Balance), "可用资产不足");

                db.AreaOperator_Assets.Attach(assets);
                db.Entry(assets).Property(p => p.Balance).IsModified = true;
                db.Entry(assets).Property(p => p.UsedNumber).IsModified = true;
                db.Entry(assets).Property(p => p.UpdateTime).IsModified = true;

                assets.Balance -= useNumber;
                assets.UsedNumber += useNumber;

                return db.SaveChanges() > 0;
            }
        }
    }
}
