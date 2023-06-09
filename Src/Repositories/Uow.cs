using FirstApp.DataContext;

public class Uow : IUow
{
    private readonly AppDbContext _context;

    public Uow(AppDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

}