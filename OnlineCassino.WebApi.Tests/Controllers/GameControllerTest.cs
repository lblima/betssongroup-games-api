using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;
using OnlineCassino.WebApi.Controllers;
using OnlineCassino.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;

namespace OnlineCassino.WebApi.Tests.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        private IUnitOfWork mockUnitOfWork;

        private Mock<HttpContextBase> moqContext;
        private Mock<HttpRequestBase> moqRequest;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AutoMapperConfig.Initialize();
        }

        [TestInitialize]
        public void SetupTests()
        {
            moqContext = new Mock<HttpContextBase>();
            moqRequest = new Mock<HttpRequestBase>();
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);

            //Setup repositories
            var games = new List<Game>();

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

            var unitMoc = new Mock<IUnitOfWork>();

            unitMoc.Setup(x => x.Games.GetById(It.IsAny<int>())).Returns((int i) => games.FirstOrDefault(x => x.Id == i));
            unitMoc.Setup(x => x.Games.GetAll()).Returns(() => games.AsQueryable());

            this.mockUnitOfWork = unitMoc.Object;
        }

        [TestMethod]
        public void GameControllerGet()
        {
            // Arrange
            var controller = new GamesController(mockUnitOfWork);
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://localhost:53389/api/Games/"),
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, config } }
            };

            controller.RequestContext = new System.Web.Http.Controllers.HttpRequestContext() { Configuration = config, Url = new System.Web.Http.Routing.UrlHelper(new HttpRequestMessage(HttpMethod.Get, "http://localhost:53389/api/Games/")) };

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);

            var content = ((OkNegotiatedContentResult<ReturnGameDto>)result).Content;

            Assert.IsTrue(content.Results.Count() == 5);
        }

        [TestMethod]
        public void GameControllerGetById()
        {
            // Arrange
            var controller = new GamesController(mockUnitOfWork);

            // Act
            var result = controller.Get(1) as OkNegotiatedContentResult<GameDto>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Id);
        }

        [TestMethod]
        public void GameControllerGetByInvalidIdShouldReturnNotFoundResult()
        {
            // Arrange
            var controller = new GamesController(mockUnitOfWork);

            // Act
            var result = controller.Get(10);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}