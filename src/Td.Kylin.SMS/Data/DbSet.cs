using Microsoft.EntityFrameworkCore;
using Td.Kylin.Entity;

namespace Td.Kylin.SMS.Data
{
    partial class DataContext
    {
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User_Account> User_Account { get { return Set<User_Account>(); } }
        public DbSet<Worker_Account> Worker_Account { get { return Set<Worker_Account>(); } }

        /// <summary>
        /// 短信记录
        /// </summary>
        public DbSet<SmsSendRecords> SmsSendRecords { get { return Set<SmsSendRecords>(); } }

        /// <summary>
        /// 运营商
        /// </summary>
        public DbSet<Area_Operator> Area_Operator { get { return Set<Area_Operator>(); } }
        
        /// <summary>
        /// 运营商及运营区域关联
        /// </summary>
        public DbSet<Area_OperatorRelation> Area_OperatorRelation { get { return Set<Area_OperatorRelation>(); } }
        
        /// <summary>
        /// 运营商子账号
        /// </summary>
        public DbSet<Area_OperatorSubAccount> Area_OperatorSubAccount { get { return Set<Area_OperatorSubAccount>(); } }
        
        /// <summary>
        /// 通知管理配置
        /// </summary>
        public  DbSet<AreaOperator_BusinessNoticeConfig> AreaOperator_BusinessNoticeConfig { get{return Set<AreaOperator_BusinessNoticeConfig>();} }

        /// <summary>
        /// 运营商资产
        /// </summary>
        public DbSet<AreaOperator_Assets> AreaOperator_Assets { get { return Set<AreaOperator_Assets>(); } }

        /// <summary>
        /// 全局资源配置
        /// </summary>
        public DbSet<System_GlobalResources> System_GlobalResources { get { return Set<System_GlobalResources>(); } }
    }
}
