using Microsoft.EntityFrameworkCore;

namespace interviewer.Data;

public class InterviewerContext : DbContext
{
    public DbSet<Student>? Students { get; set; }
    public DbSet<Interviewer>? Interviewers { get; set; }
    public DbSet<StudentAccount>? StudentAccounts { get; set; }
    public DbSet<InterviewerAccount>? InterviewerAccounts { get; set; }

    private readonly string? _connectionString;

    public InterviewerContext(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        if (_connectionString is null)
            throw new Exception("Connection string is null");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Student> students= new List<Student>();
        for (int i = 0; i < 50; i++)
        {
            students.Add(new Student
            {
                Id = Guid.NewGuid(),
                College = "�����ѧԺ",
                FirstDepartment = ElcDepartment.Software,
                Grade = "22���繤��",
                Introduction = "ѧ��",
                Name = "HeChu",
                Phone = "13323588435",
                Qq = "235247902",
                StudentId = "3122004832"
            });
            students.Add(new Student
            {
                Id = Guid.NewGuid(),
                College = "�����ѧԺ",
                FirstDepartment = ElcDepartment.Project,
                Grade = "22���繤��",
                Introduction = "ѧ��",
                Name = "ChuHe",
                Phone = "13323588435",
                Qq = "235247902",
                StudentId = "3122004832"
            });
        }
        modelBuilder.Entity<Student>().HasData(students);
    }
}