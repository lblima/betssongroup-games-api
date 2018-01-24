using System;

namespace OnlineCassino.Domain
{
    public class GameSession : BaseEntity
    {
        protected GameSession()
        {

        }

        public GameSession(Game game, User user)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            Game = game;
            User = user;
            IsInProgress = true;
        }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public bool IsInProgress { get; set; }

        public void EndSession()
        {
            IsInProgress = false;
        }
    }
}