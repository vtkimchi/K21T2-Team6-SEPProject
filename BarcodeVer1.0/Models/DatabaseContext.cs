using System.Data.Entity;

namespace BarcodeVer1._0.Models
{
    public class DatabaseContext
        : DbContext
    {
        public DatabaseContext()
            : base(@"Data Source=CHIVO\SQLEXPRESS;Initial Catalog=SEP;Integrated Security=True;MultipleActiveResultSets=True")
        {
        }

        public DbSet<Attendance> Attendance { get; set; }

        public DbSet<Lesson> Lesson { get; set; }

        public DbSet<Member> Member { get; set; }
    }
}