using FirstApp.DataContext;
using Microsoft.EntityFrameworkCore;

public class FacultyRepo : IfacultyRepo
{
    private readonly AppDbContext _context;

    public FacultyRepo(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Faculty> GetQueryable()
    {
        return _context.Set<Faculty>();
    }

    public async Task<Faculty> FindOrThrowAsync(long id)
    {
        var faculty = await GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
        if (faculty == null) throw new Exception($"Faculty with id {id} not found");
        return faculty;
    }
}