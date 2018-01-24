using System;
using System.Collections.Generic;

namespace OnlineCassino.Domain
{
    public enum GameCategory
    {
        ClassicSlots = 0,
        VideoSlots = 1,
        Roulette = 2,
        LiveRoulette = 3,
        Poker = 4,
        Blackjack = 5
    }

    public class Game : BaseEntity
    {
        protected Game()
        {

        }

        public Game(string displayName, int displayIndex, DateTime releaseDate, GameCategory gameCategory)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException(nameof(displayName));

            DisplayName = displayName;
            DisplayIndex = displayIndex;
            ReleaseDate = releaseDate;
            GameCategory = gameCategory;

            DevicesAvailability = new List<DeviceType>();
            GameCollections = new List<GameCollection>();
        }

        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GameCategory GameCategory { get; set; }
        public virtual ICollection<DeviceType> DevicesAvailability { get; set; }
        public virtual ICollection<GameCollection> GameCollections { get; set; }
    }
}