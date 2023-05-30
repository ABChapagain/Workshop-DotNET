public interface IStudentRepo
{

    IQueryable<Student> GetQueryable();
    Task<Student> FindOrThrowAsync(long id);
    Task AddAsync(Student student);
}