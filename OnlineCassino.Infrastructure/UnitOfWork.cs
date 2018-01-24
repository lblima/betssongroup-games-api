using OnlineCassino.Domain.Interfaces;
using OnlineCassino.Infrastructure.Repositories;

namespace OnlineCassino.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private OnlineCassinoContext context;

        public UnitOfWork(OnlineCassinoContext context)
        {
            this.context = context;

            GameCollections = new GameCollectionRepository(this.context);
            Games = new GameRepository(this.context);
        }

        public IGameCollectionRepository GameCollections { get; set; }
        public IGameRepository Games { get; set; }

        public System.Data.Entity.DbContext Context
        {
            get { return context; }
            protected set
            {
            }
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
