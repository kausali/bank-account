using Microsoft.EntityFrameworkCore;
 
namespace bank.Models
{
    public class UserContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Transaction> transactions { get; set; }

        //bdset allowss too communicate with our db and allows to get and set stuffs
        //users and transacations should match the name of the table in the database
        // you will need as many dbsets as many tables you have in db
        //RegisterViewModel should match with our class in the RegisterViewModesl.cs
    }
}