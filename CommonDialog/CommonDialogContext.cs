using System.Data.Entity;
using BaseEntyties;
//using DataLayer.Migrations;

namespace DataLayer
{
    public class CommonDialogContext : DbContext
    {
        public CommonDialogContext() : base("CommonDialogDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new CreateDatabaseIfNotExists<CommonDialogContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CommonDialogContext, Configuration>(@"Data Source=.\SQLEXPRESS;Initial Catalog=CommonDialogDB;Integrated Security=True"));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<MetaContact> MetaContacts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
