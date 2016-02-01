using System.Data.Entity;
using Database.Migrations;
using Domain.University;

namespace Database
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext()
            : base("DefaultConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<UniversityDbContext, Configuration>());
        }

        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Professor>()
                .Property(e => e.Surname)
                .IsFixedLength();


            modelBuilder.Entity<Student>()
                .Property(e => e.Name)
                .IsFixedLength();
            modelBuilder.Entity<Student>()
                .Property(e => e.Surname)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Marks)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Subjects)
                .WithMany(e => e.Students)
                .Map(m => m.ToTable("StudentsSubjects").MapLeftKey("StudentId").MapRightKey("SubjectId"));



            modelBuilder.Entity<Subject>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Marks)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);
        }


        public static UniversityDbContext Create()
        {
            return new UniversityDbContext();
        }
    }
}