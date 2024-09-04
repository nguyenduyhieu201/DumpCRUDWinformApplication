using System.Data.Entity;

namespace DumpCRUDWinformApplication
{
    public class SchoolInforDbContext : DbContext
    {
        public DbSet<Student> Students {  get; set; }
        public DbSet<Class> Classses { get; set; }

        public SchoolInforDbContext() : base("name = SchoolDB")
        {

        }
    }
}
