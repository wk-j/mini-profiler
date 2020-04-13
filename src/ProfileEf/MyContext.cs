using Microsoft.EntityFrameworkCore;

namespace MyWeb {
    public class Student {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public class MyContext : DbContext {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Student> Students { set; get; }
    }
}
