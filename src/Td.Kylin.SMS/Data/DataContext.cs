using System;
using Microsoft.EntityFrameworkCore;
using Td.Kylin.Entity;
using Td.Kylin.SMS.Core;

namespace Td.Kylin.SMS.Data
{
    internal partial class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (ConfigRoot.SqlType)
            {
                case EnumLibrary.SqlProviderType.SqlServer:
                    optionsBuilder.UseSqlServer(ConfigRoot.SqlConnectionString);
                    break;
                case EnumLibrary.SqlProviderType.NpgSQL:
                    throw new InvalidOperationException("暂未实现对NpgSQL数据库的支持");
                    //break;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //用户
            modelBuilder.Entity<User_Account>(entity =>
            {
                entity.Property(p => p.UserID).ValueGeneratedNever();
                entity.HasKey(p => p.UserID);
            });
            //用户
            modelBuilder.Entity<Worker_Account>(entity =>
            {
                entity.Property(p => p.WorkerID).ValueGeneratedNever();
                entity.HasKey(p => p.WorkerID);
            });


            //短信发送记录
            modelBuilder.Entity<SmsSendRecords>(entity =>
            {
                entity.Property(p => p.SendID).ValueGeneratedNever();
                entity.HasKey(p => p.SendID);
            });

            //运营商
            modelBuilder.Entity<Area_Operator>(entity =>
            {
                entity.Property(p => p.OperatorID).ValueGeneratedNever();
                entity.HasKey(p => p.OperatorID);
            });
            //运营商及运营的区域关联
            modelBuilder.Entity<Area_OperatorRelation>(entity =>
            {
                entity.HasKey(p => new { p.OperatorID, p.AreaID });
            });
            //运营商子账号
            modelBuilder.Entity<Area_OperatorSubAccount>(entity =>
            {
                entity.Property(p => p.SubID).ValueGeneratedNever();
                entity.HasKey(p => p.SubID);
            });
            //运营商通知管理配置
            modelBuilder.Entity<AreaOperator_BusinessNoticeConfig>(entity =>
            {
                entity.Property(p => p.ConfigID).ValueGeneratedNever();
                entity.HasKey(p => p.ConfigID);
            });
            //运营商资产
            modelBuilder.Entity<AreaOperator_Assets>(entity =>
            {
                entity.HasKey(p => new { p.OperatorID, p.AssetsType });
            });
        }
    }
}
