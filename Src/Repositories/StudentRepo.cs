using FirstApp.DataContext;
using Microsoft.EntityFrameworkCore;

public class StudentRepo : IStudentRepo
{
    private readonly AppDbContext _context;

    public StudentRepo(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Student> GetQueryable()
    {
        return _context.Set<Student>();
    }

    public async Task<Student> FindOrThrowAsync(long id)
    {
        var student = await GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
        if (student == null) throw new Exception($"Student with id {id} not found");
        return student;
    }

    public async Task AddAsync(Student student)
    {
        await _context.Set<Student>().AddAsync(student);
    }
}