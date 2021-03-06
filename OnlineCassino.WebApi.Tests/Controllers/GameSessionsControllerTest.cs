﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        private const string rootUrl = "http://localhost:53389";

        [TestInitialize]
        public void SetupTests()
        {
            //Setup Identity provider
            var identityProviderMoq = new Mock<IIdentityProvider>();
            identityProviderMoq.Setup(x => x.GetUserId()).Returns("00000001");
            this.moqIdentityProvider = identityProviderMoq.Object;

            //Setup repositories
            var games = new List<Game>();
            var gameSessions = new List<GameSession>();

            var user = new User("Leonardo", "xxxxxxxxxxxxxx");

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
            
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{rootUrl}/api/GameSessions/"),
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
                RequestUri = new Uri($"{rootUrl}/api/GameSessions/"),
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
            Assert.AreEqual($"{rootUrl}/api/Games/3", result.Content.GameUrl);
        }
    }
}