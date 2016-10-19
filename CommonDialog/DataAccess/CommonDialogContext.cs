using System.Data.Entity;
using DataLayer.Model;
//using DataLayer.Migrations;

namespace DataLayer.DataAccess
{
    public class CommonDialogContext : DbContext
    {
        public CommonDialogContext() : base("CommonDialogDB")
        {
            Database.SetInitializer<CommonDialogContext>(new CreateDatabaseIfNotExists<CommonDialogContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CommonDialogContext, Configuration>(@"Data Source=.\SQLEXPRESS;Initial Catalog=CommonDialogDB;Integrated Security=True"));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<GeneralContact> GeneralContacts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
