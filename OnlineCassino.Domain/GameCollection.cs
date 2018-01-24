using OnlineCassino.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace OnlineCassino.Domain
{
    public class GameCollection : IEntity
    {
        protected GameCollection()
        {

        }

        public GameCollection(string displayName, int displayIndex)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException(nameof(displayName));

            DisplayName = displayName;
            DisplayIndex = displayIndex;

            Games = new List<Game>();
            SubCollections = new List<GameCollection>();
        }

        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<GameCollection> SubCollections { get; set; }
    }
}