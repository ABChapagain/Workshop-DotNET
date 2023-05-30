public interface IClassInfoRepo
{
    IQueryable<ClassInfo> GetQueryable();
    Task<ClassInfo> FindOrThrowAsync(long id);
}