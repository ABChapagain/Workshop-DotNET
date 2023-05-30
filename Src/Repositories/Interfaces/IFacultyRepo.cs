public interface IfacultyRepo
{

    IQueryable<Faculty> GetQueryable();
    Task<Faculty> FindOrThrowAsync(long id);

}