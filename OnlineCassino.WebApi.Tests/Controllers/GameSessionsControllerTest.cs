using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;
using OnlineCassino.WebApi.Controllers;
using OnlineCassino.WebApi.DTOs;
using OnlineCassino.WebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;

namespace OnlineCassino.WebApi.Tests.Controllers
{
    [TestClass]
    public class GameSessionsControllerTest
    {
        private IUnitOfWork mockUnitOfWork;
        private IIdentityProvider moqIdentityProvider;

        private Mock<HttpContextBase> moqContext;
        private Mock<HttpRequestBase> moqRequest;

        [TestInitialize]
        public void SetupTests()
        {
            moqContext = new Mock<HttpContextBase>();
            moqRequest = new Mock<HttpRequestBase>();
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);

            //Setup Identity provider
            var identityProviderMoq = new Mock<IIdentityProvider>();
            identityProviderMoq.Setup(x => x.GetUserId()).Returns("00000001");
            this.moqIdentityProvider = identityProviderMoq.Object;

            //Setup repositories
            var deviceTypes = new List<DeviceType>();
            var games = new List<Game>();
            var gameCollection = new List<GameCollection>();
            var gameSessions = new List<GameSession>();

            var user = new User("Leonardo", "xxxxxxxxxxxxxx");

            //Devices ==============================================
            var device1 = new DeviceType("Desktop");
            var device2 = new DeviceType("Mobile");
            var device3 = new DeviceType("Console");

            deviceTypes.AddRange(new DeviceType[] { device1, device2, device3 });

            //Games =================================================
            var game1 = new Game("Texas Hold´em", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker);
            game1.Id = 1;
            game1.DevicesAvailability.Add(device1);
            game1.DevicesAvailability.Add(device2);

            var game2 = new Game("Stud", 1, DateTime.Now.AddMonths(-1), GameCategory.Poker);
            game2.Id = 2;
            game2.DevicesAvailability.Add(device1);
            game2.DevicesAvailability.Add(device2);

            var game3 = new Game("Card 21", 1, DateTime.Now.AddMonths(-1), GameCategory.Blackjack);
            game3.Id = 3;
            game3.DevicesAvailability.Add(device2);

            var game4 = new Game("60 roulet", 1, DateTime.Now.AddMonths(-1), GameCategory.Roulette);
            game4.Id = 4;
            game4.DevicesAvailability.Add(device1);
            game4.DevicesAvailability.Add(device2);

            var game5 = new Game("Game 5", 1, DateTime.Now.AddMonths(-1), GameCategory.VideoSlots);
            game5.Id = 5;
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
            gameCollection3.Games.Add(game3);

            var gameCollection4 = new GameCollection("Game Collection 04", 4);
            gameCollection4.Games.Add(game1);
            gameCollection4.Games.Add(game4);

            gameCollection.AddRange(new GameCollection[] { gameCollection1, gameCollection2, gameCollection3, gameCollection4 });

            //Game sessions
            var gameSession1 = new GameSession(game1, user) { Id = 1 };
            var gameSession2 = new GameSession(game2, user) { Id = 2 };

            gameSessions.AddRange(new GameSession[] { gameSession1, gameSession2 });

            var unitMoc = new Mock<IUnitOfWork>();

            unitMoc.Setup(x => x.GameSessions.GetById(It.IsAny<int>())).Returns((int i) => gameSessions.FirstOrDefault(x => x.Id == i));
            unitMoc.Setup(x => x.GameSessions.Find(It.IsAny<Expression<System.Func<GameSession, bool>>>())).Returns((Expression<System.Func<GameSession, bool>> criteria) => gameSessions.Where(criteria.Compile()));
            unitMoc.Setup(x => x.GameSessions.GetAll()).Returns(() => gameSessions.AsQueryable());

            unitMoc.Setup(x => x.Games.GetById(It.IsAny<int>())).Returns((int i) => games.FirstOrDefault(x => x.Id == i));

            unitMoc.Setup(x => x.Users.GetByAspNetId(It.IsAny<string>())).Returns(user);

            this.mockUnitOfWork = unitMoc.Object;
        }

        [TestMethod]
        public void GameSessionsControllerGet()
        {
            // Arrange
            var controller = new GameSessionsController(mockUnitOfWork, moqIdentityProvider);
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://localhost:53389/api/GameSessions/"),
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, config } }
            };

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);

            var content = ((OkNegotiatedContentResult<ReturnListGameSessionDto>)result).Content;

            Assert.IsTrue(content.Results.Count() == 2);
        }

        [TestMethod]
        public void GameSessionsControllerGetById()
        {
            // Arrange
            var controller = new GameSessionsController(mockUnitOfWork, moqIdentityProvider);

            // Act
            var result = controller.Get(1) as OkNegotiatedContentResult<GameSessionDto>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Id);
        }

        [TestMethod]
        public void GameSessionsControllerPost()
        {
            // Arrange
            var controller = new GameSessionsController(mockUnitOfWork, moqIdentityProvider);
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:53389/api/GameSessions/"),
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, config } }
            };

            // Act
            var gameSessionDto = new NewGameSessionDto()
            {
                GameId = 3
            };

            var result = controller.Post(gameSessionDto) as OkNegotiatedContentResult<ReturnGameSessionDto>;

            // Assert
            Assert.IsNotNull(result.Content);
            Assert.AreEqual("http://localhost:53389/api/Games/3", result.Content.GameUrl);
        }
    }
}