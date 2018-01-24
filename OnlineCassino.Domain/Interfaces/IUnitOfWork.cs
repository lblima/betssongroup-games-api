using System;
using System.Data.Entity;

namespace OnlineCassino.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; set; }
        IGameCollectionRepository GameCollections { get; set; }
        IUserRepository Users { get; set; }
        IGameSessionRepository GameSessions { get; set; }

        DbContext Context { get; }

        int Complete();
    }
}