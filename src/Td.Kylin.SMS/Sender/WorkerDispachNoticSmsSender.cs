using System;
using System.Threading.Tasks;
using Td.Kylin.Entity;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Core;
using Td.Kylin.SMS.Services;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 配送订单指派消息发送器
    /// </summary>
    public class WorkerDispachNoticSmsSender : BaseSender
    {
        /// <summary>
        /// 运营商ID
        /// </summary>
        private readonly long _operatorId;

        /// <summary>
        /// 配送工作人员手机号
        /// </summary>
        private readonly string _workerMobile;

        /// <summary>
        /// 配送订单编号
        /// </summary>
        private readonly string _orderCode;

        private readonly OperatorAssetsService _operatorAssetsService;

        /// <summary>
        /// 初始化配送订单指派消息发送器实例
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="workerId">工作人员ID</param>
        /// <param name="orderCode">配送订单编号</param>
        public WorkerDispachNoticSmsSender(int areaId, long workerId, string orderCode) : base(SmsTemplateOption.MallOrderDispachNotice)
        {
            _orderCode = orderCode;

            _operatorAssetsService = new OperatorAssetsService();

            _operatorId = new AreaOperatorService().GetOperatorId(areaId);

            _workerMobile = new UserService().GetUserMobile(workerId);

            base.ContentFactory(new { OrderCode = orderCode });
        }

        public override async Task<bool> SendAsync()
        {
            //运营商剩余短信数量
            var balance = _operatorAssetsService.GetAssetsBalance(_operatorId, EnumLibrary.Operator.OperatorAssetsType.Sms);

            if (balance < 1) return false;

            var result = await ConfigRoot.SendProvider.SendSmsAsync(_workerMobile, Content, _orderCode);

            bool success = result.IsSuccess;

            //发送成功，则更新资产
            if (success)
            {
                try
                {
                    _operatorAssetsService.UseAssets(_operatorId, EnumLibrary.Operator.OperatorAssetsType.Sms, 1);
                }
                catch
                {
                    //TODO
                }
            }

            SmsSendRecords record = new SmsSendRecords
            {
                IsSuccess = success,
                Message = Content,
                Mobile = _workerMobile,
                Remark = result.Remark,
                SenderId = 0,
                SenderType = (int)IdentityType.Platform,
                SmsType = (int)SmsTemplateOption.MallOrderDispachNotice,
                SendID = IDProvider.NewId(),
                SendTime = DateTime.Now
            };

            new SmsSendRecordsService().AddRecord(record);

            return success;
        }
    }
}
