namespace OnlineCassino.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByAspNetId(string id);
    }
}