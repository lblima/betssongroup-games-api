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

            //Devices ==============================================
            var device1 = new DeviceType("Desktop");
            var device2 = new DeviceType("Mobile");
            var device3 = new DeviceType("Console");

            deviceTypes.AddRange(new DeviceType[] { device1, device2, device3 });

            //Games =================================================
            var game1 = new Game("Texas Hold´em", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker);
            game1.DevicesAvailability.Add(device1);
            game1.DevicesAvailability.Add(device2);

            var game2 = new Game("Stud", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker);
            game2.DevicesAvailability.Add(device1);
            game2.DevicesAvailability.Add(device2);

            var game3 = new Game("Card 21", 1, DateTime.Now.AddMonths(-1), GameCategory.Blackjack);
            game3.DevicesAvailability.Add(device2);

            var game4 = new Game("60 roulet", 1, DateTime.Now.AddMonths(-1), GameCategory.Roulette);
            game4.DevicesAvailability.Add(device1);
            game4.DevicesAvailability.Add(device2);

            var game5 = new Game("Game 5", 1, DateTime.Now.AddMonths(-1), GameCategory.VideoSlots);
            game1.DevicesAvailability.Add(device1);

            games.AddRange(new Game[] { game1, game2, game3, game4, game5 });

            //Game collections  ======================================
            var gameCollection1 = new GameCollection("Game Collection 01", 1);
            gameCollection1.Games.Add(game1);
            gameCollection1.Games.Add(game2);

            var gameCollection2 = new GameCollection("Game Collection 02", 2);
            gameCollection2.Games.Add(game1);
            gameCollection2.Games.Add(game2);
            gameCollection2.Games.Add(game3);

            var gameCollection3 = new GameCollection("Game Collection 03", 3);
            gameCollection1.Games.Add(game3);

            var gameCollection4 = new GameCollection("Game Collection 04", 4);
            gameCollection1.Games.Add(game1);
            gameCollection1.Games.Add(game4);

            gameCollection.AddRange(new GameCollection[] { gameCollection1, gameCollection2, gameCollection3, gameCollection4 });

            foreach (var g in games)
                context.Games.AddOrUpdate(x => x.DisplayName, g);

            foreach (var gc in gameCollection)
                context.GameCollections.AddOrUpdate(x => x.DisplayName, gc);

            base.Seed(context);
        }
    }
}