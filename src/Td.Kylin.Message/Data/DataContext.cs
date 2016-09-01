using Microsoft.EntityFrameworkCore;
using System;
using Td.Kylin.Entity;
using Td.Kylin.Message.Core;

namespace Td.Kylin.Message.Data
{
    internal partial class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (Configs.SqlType)
            {
                case EnumLibrary.SqlProviderType.SqlServer:
                    optionsBuilder.UseSqlServer(Configs.SqlConnectionString);
                    break;
                case EnumLibrary.SqlProviderType.NpgSQL:
                    throw new InvalidOperationException("暂未实现对NpgSQL数据库的支持");
                    //break;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //用户消息
            modelBuilder.Entity<User_Message>(entity =>
            {
                entity.Property(p => p.MessageID).ValueGeneratedNever();
                entity.HasKey(p => p.MessageID);
            });

            //商户消息
            modelBuilder.Entity<Merchant_Message>(entity =>
            {
                entity.Property(p => p.MessageID).ValueGeneratedNever();
                entity.HasKey(p => p.MessageID);
            });

            //工作人员消息
            modelBuilder.Entity<Worker_Message>(entity =>
            {
                entity.Property(p => p.MessageID).ValueGeneratedNever();
                entity.HasKey(p => p.MessageID);
            });

            //社区帖子
            modelBuilder.Entity<Circle_Topic>(entity =>
            {
                entity.Property(p => p.TopicID).ValueGeneratedNever();
                entity.HasKey(p => p.TopicID);
            });

            //商家订单
            modelBuilder.Entity<Merchant_Order>(entity =>
            {
                entity.Property(p => p.OrderID).ValueGeneratedNever();
                entity.HasKey(p => p.OrderID);
            });

            //商家
            modelBuilder.Entity<Merchant_Account>(entity =>
            {
                entity.Property(p => p.MerchantID).ValueGeneratedNever();
                entity.HasKey(p => p.MerchantID);
            });

            //用户
            modelBuilder.Entity<User_Account>(entity =>
            {
                entity.Property(p => p.UserID).ValueGeneratedNever();
                entity.HasKey(p => p.UserID);
            });

            //福利
            modelBuilder.Entity<Merchant_Welfare>(entity =>
            {
                entity.Property(p => p.WelfareID).ValueGeneratedNever();
                entity.HasKey(p => p.WelfareID);
            });

            //用户福利
            modelBuilder.Entity<User_Welfare>(entity =>
            {
                entity.Property(p => p.ConsumerCode).ValueGeneratedNever();
                entity.HasKey(p => p.ConsumerCode);
            });

            //福利参与
            modelBuilder.Entity<Welfare_PartUser>(entity =>
            {
                entity.HasKey(p => new { p.UserID, p.WelfareID });
            });
        }
    }
}
