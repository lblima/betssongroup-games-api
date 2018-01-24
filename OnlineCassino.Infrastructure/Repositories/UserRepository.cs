using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;
using System.Linq;

namespace OnlineCassino.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        new OnlineCassinoContext context;

        public UserRepository(OnlineCassinoContext context) : base(context)
        {
            this.context = context;
        }

        public User GetByAspNetId(string id)
        {
            return context.Users.FirstOrDefault(u => u.AccountId == id);
        }
    }
}