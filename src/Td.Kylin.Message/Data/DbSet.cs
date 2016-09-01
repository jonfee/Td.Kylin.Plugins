using Microsoft.EntityFrameworkCore;
using Td.Kylin.Entity;

namespace Td.Kylin.Message.Data
{
    internal partial class DataContext
    {
        public DbSet<User_Message> User_Message { get { return Set<User_Message>(); } }

        public DbSet<Merchant_Message> Merchant_Message { get { return Set<Merchant_Message>(); } }

        public DbSet<Worker_Message> Worker_Message { get { return Set<Worker_Message>(); } }

        public DbSet<Circle_Topic> Circle_Topic { get { return Set<Circle_Topic>(); } }

        public  DbSet<Merchant_Order> Merchant_Order { get { return Set<Merchant_Order>(); } }

        public DbSet<Merchant_Account> Merchant_Account {get { return Set<Merchant_Account>(); } }

        public  DbSet<User_Account> User_Account { get { return Set<User_Account>(); } }

        public  DbSet<Merchant_Welfare> Merchant_Welfare { get { return Set<Merchant_Welfare>(); } }

        public  DbSet<User_Welfare> User_Welfare { get { return Set<User_Welfare>(); } }

        public  DbSet<Welfare_PartUser> Welfare_PartUser { get { return Set<Welfare_PartUser>(); } }
    }
}
