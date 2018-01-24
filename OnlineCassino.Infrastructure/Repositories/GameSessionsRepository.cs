using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;

namespace OnlineCassino.Infrastructure.Repositories
{
    public class GameSessionsRepository : BaseRepository<GameSession>, IGameSessionRepository
    {
        new OnlineCassinoContext context;

        public GameSessionsRepository(OnlineCassinoContext context) : base(context)
        {
            this.context = context;
        }
    }
}