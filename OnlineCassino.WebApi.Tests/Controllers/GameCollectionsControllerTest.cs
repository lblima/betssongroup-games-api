using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;
using OnlineCassino.WebApi.Controllers;
using OnlineCassino.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;

namespace OnlineCassino.WebApi.Tests.Controllers
{
    [TestClass]
    public class GameCollectionsControllerTest
    {
        private IUnitOfWork mockUnitOfWork;

        private const string rootUrl = "http://localhost:53389";
        
        [TestInitialize]
        public void SetupTests()
        {
            //Setup repositories
            var games = new List<Game>();
            var gameCollection = new List<GameCollection>();

            //Games =================================================
            var game1 = new Game("Texas Hold´em", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker);
            game1.Id = 1;

            var game2 = new Game("Stud", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker);
            game2.Id = 2;

            var game3 = new Game("Card 21", 1, DateTime.Now.AddMonths(-1), GameCategory.Blackjack);
            game3.Id = 3;

            var game4 = new Game("60 roulet", 1, DateTime.Now.AddMonths(-1), GameCategory.Roulette);
            game4.Id = 4;

            var game5 = new Game("Game 5", 1, DateTime.Now.AddMonths(-1), GameCategory.VideoSlots);
            game5.Id = 5;

            games.AddRange(new Game[] { game1, game2, game3, game4, game5 });

            //Game collections  ======================================
            var gameCollection1 = new GameCollection("Game Collection 01", 1);
            gameCollection1.Id = 1;
            gameCollection1.Games.Add(game1);
            gameCollection1.Games.Add(game2);

            var gameCollection2 = new GameCollection("Game Collection 02", 2);
            gameCollection2.Id = 2;
            gameCollection2.Games.Add(game1);
            gameCollection2.Games.Add(game2);
            gameCollection2.Games.Add(game3);

            var gameCollection3 = new GameCollection("Game Collection 03", 3);
            gameCollection3.Id = 3;
            gameCollection3.Games.Add(game3);

            var gameCollection4 = new GameCollection("Game Collection 04", 4);
            gameCollection4.Id = 4;
            gameCollection4.Games.Add(game1);
            gameCollection4.Games.Add(game4);

            gameCollection.AddRange(new GameCollection[] { gameCollection1, gameCollection2, gameCollection3, gameCollection4 });

            var unitMoc = new Mock<IUnitOfWork>();

            unitMoc.Setup(x => x.GameCollections.GetById(It.IsAny<int>())).Returns((int i) => gameCollection.FirstOrDefault(x => x.Id == i));
            unitMoc.Setup(x => x.GameCollections.GetAll()).Returns(() => gameCollection.AsQueryable());

            this.mockUnitOfWork = unitMoc.Object;
        }

        [TestMethod]
        public void GameCollectionsControllerGet()
        {
            // Arrange
            var controller = new GameCollectionsController(mockUnitOfWork);
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{rootUrl}/api/GameCollections/"),
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, config } }
            };
            
            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);

            var content = ((OkNegotiatedContentResult<ReturnGameCollectionDto>)result).Content;

            Assert.IsTrue(content.Results.Count() == 4);
        }

        [TestMethod]
        public void GameCollectionsControllerGetById()
        {
            // Arrange
            var controller = new GameCollectionsController(mockUnitOfWork);

            // Act
            var result = controller.Get(1) as OkNegotiatedContentResult<GameCollectionDto>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Id);
        }
    }
}