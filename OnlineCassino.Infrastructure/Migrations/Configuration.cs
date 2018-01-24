namespace OnlineCassino.Infrastructure.Migrations
{
    using OnlineCassino.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineCassino.Infrastructure.OnlineCassinoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OnlineCassino.Infrastructure.OnlineCassinoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var deviceTypes = new List<DeviceType>();
            var games = new List<Game>();
            var gameCollection = new List<GameCollection>();

            var device1 = new DeviceType("Desktop");
            var device2 = new DeviceType("Mobile");
            var device3 = new DeviceType("Console");

            deviceTypes.AddRange(new DeviceType[] { device1, device2, device3 });

            games.Add(new Game("Texas Hold�em", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker) { DevicesAvailability = new DeviceType[] { device1, device2 } });
            games.Add(new Game("Stud", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker) { DevicesAvailability = new DeviceType[] { device1, device2 } });
            games.Add(new Game("Card 21", 1, DateTime.Now.AddMonths(-1), GameCategory.Blackjack) { DevicesAvailability = new DeviceType[] { device1, device2 } });
            games.Add(new Game("60 roulet", 1, DateTime.Now.AddMonths(-1), GameCategory.Roulette) { DevicesAvailability = new DeviceType[] { device1, device2, device3 } });
            games.Add(new Game("Game 5", 1, DateTime.Now.AddMonths(-1), GameCategory.VideoSlots) { DevicesAvailability = new DeviceType[] { device3 } });

            gameCollection.Add(new GameCollection("Game Collection 01", 1));
            gameCollection.Add(new GameCollection("Game Collection 02", 2));
            gameCollection.Add(new GameCollection("Game Collection 03", 3));
            gameCollection.Add(new GameCollection("Game Collection 04", 4));

            foreach (var g in games)
                context.Games.AddOrUpdate(x => x.DisplayName, g);

            foreach (var gc in gameCollection)
                context.GameCollections.AddOrUpdate(x => x.DisplayName, gc);

            base.Seed(context);
        }
    }
}