using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;

namespace OnlineCassino.Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        new OnlineCassinoContext context;

        public GameRepository(OnlineCassinoContext context) : base(context)
        {
            this.context = context;
        }
    }
}