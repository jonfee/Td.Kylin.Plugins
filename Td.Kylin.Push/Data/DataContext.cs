using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Td.Kylin.Entity;
using Td.Kylin.Push.Core;

namespace Td.Kylin.Push.Data
{
    public class DataContext : DbContext
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
            modelBuilder.Entity<Legwork_AreaConfig>().HasKey(p => p.AreaID);
            modelBuilder.Entity<Legwork_Evaluate>().HasKey(p => p.EvaluateID);
            modelBuilder.Entity<Legwork_GlobalConfig>().HasKey(p => p.GlobalConfigID);
            modelBuilder.Entity<Legwork_GoodsCategory>().HasKey(p => p.CategoryID);
            modelBuilder.Entity<Legwork_OfferRecord>().HasKey(p => p.OfferID);
            modelBuilder.Entity<Worker_Account>().HasKey(p => p.WorkerID);
            modelBuilder.Entity<Worker_Profile>().HasKey(p => p.UserID);
            modelBuilder.Entity<Legwork_Order>().HasKey(p => p.OrderID);
            modelBuilder.Entity<System_Area>().HasKey(p => p.AreaID);
            modelBuilder.Entity<Worker_BusinessRelation>().HasKey(p => p.WorkerID);
            modelBuilder.Entity<User_Address>().HasKey(p => p.UserID);
            modelBuilder.Entity<User_Account>().HasKey(p => p.UserID);
        }

        public DbSet<User_Account> User_Account { get; set; }
        public DbSet<User_Address> User_Address { get; set; }
        public DbSet<Worker_BusinessRelation> Worker_BusinessRelation { get; set; }
        public DbSet<System_Area> System_Area { get; set; }
        public DbSet<Legwork_AreaConfig> Legwork_AreaConfig { get; set; }
        public DbSet<Legwork_Evaluate> Legwork_Evaluate { get; set; }
        public DbSet<Legwork_GlobalConfig> Legwork_GlobalConfig { get; set; }
        public DbSet<Legwork_GoodsCategory> Legwork_GoodsCategory { get; set; }
        public DbSet<Legwork_OfferRecord> Legwork_OfferRecord { get; set; }
        public DbSet<Legwork_Order> Legwork_Order { get; set; }
        public DbSet<Worker_Account> Worker_Account { get; set; }
        public DbSet<Worker_Profile> Worker_Profile { get; set; }
    }
}
