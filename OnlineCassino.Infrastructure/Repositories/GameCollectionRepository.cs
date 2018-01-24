using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;

namespace OnlineCassino.Infrastructure.Repositories
{
    public class GameCollectionRepository : BaseRepository<GameCollection>, IGameCollectionRepository
    {
        new OnlineCassinoContext context;

        public GameCollectionRepository(OnlineCassinoContext context) : base(context)
        {
            this.context = context;
        }
    }
}