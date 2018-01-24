using AutoMapper;
using OnlineCassino.Domain;
using OnlineCassino.Domain.Interfaces;
using OnlineCassino.WebApi.DTOs;
using OnlineCassino.WebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OnlineCassino.WebApi.Controllers
{
    public class GameSessionsController : ApiController
    {
        IUnitOfWork unitOfWork;
        IIdentityProvider identityProvider;

        public GameSessionsController(IUnitOfWork unitOfWork, IIdentityProvider identityProvider)
        {
            this.unitOfWork = unitOfWork;
            this.identityProvider = identityProvider;
        }

        // GET: api/GameSessions
        public IHttpActionResult Get()
        {
            var gameSessions = unitOfWork.GameSessions.GetAll();

            return Ok(Mapper.Map<List<NewGameSessionDto>>(gameSessions.ToList()));
        }

        // GET: api/GameSessions/5
        public IHttpActionResult Get(int id)
        {
            var gameSession = unitOfWork.GameSessions.GetById(id);

            if (gameSession == null)
                return NotFound();

            return Ok(Mapper.Map<NewGameSessionDto>(gameSession));
        }

        // POST: api/GameSessions
        [Authorize]
        public IHttpActionResult Post([FromBody]NewGameSessionDto value)
        {
            if (value != null)
            {
                var user = unitOfWork.Users.GetByAspNetId(identityProvider.GetUserId());

                if (unitOfWork.GameSessions.Find(x => x.IsInProgress == true && x.Game.Id == value.GameId && x.User.AccountId == user.AccountId).Count() > 0)
                {
                    return BadRequest("User is already playing this game");
                }

                var game = unitOfWork.Games.GetById(value.GameId);
                var gameSession = new GameSession(game, user);

                unitOfWork.GameSessions.Add(gameSession);

                unitOfWork.Complete();

                return Ok(Mapper.Map<NewGameSessionDto>(gameSession));
            }
            else
            {
                return BadRequest("Invalid params");
            }
        }

        // PUT: api/GameSessions/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/GameSessions/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}