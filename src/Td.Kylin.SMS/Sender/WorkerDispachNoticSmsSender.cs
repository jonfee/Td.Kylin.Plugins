using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Services;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 配送订单指派消息发送器
    /// </summary>
    public class WorkerDispachNoticSmsSender : AreaSender
    {
        /// <summary>
        /// 工作人员ID
        /// </summary>
        private readonly long _workerId;

        /// <summary>
        /// 配送订单编号
        /// </summary>
        private readonly string _orderCode;

        /// <summary>
        /// 初始化配送订单指派消息发送器实例
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="workerId">工作人员ID</param>
        /// <param name="orderCode">配送订单编号</param>
        public WorkerDispachNoticSmsSender(int areaId, long workerId, string orderCode) : base(areaId, SmsTemplateOption.MallOrderDispachNotice)
        {
            _workerId = workerId;

            _orderCode = orderCode;

            base.ContentFactory(new { OrderCode = orderCode });
        }

        public override async Task<bool> SendAsync()
        {
            //计划发送的目标手机号
            planSendMobiles = new[] { new UserService().GetUserMobile(_workerId) };

            //发送短信
            await Send(_orderCode);

            return true;
        }
    }
}
