using AutoMapper;
using OnlineCassino.Domain.Interfaces;
using OnlineCassino.WebApi.DTOs;
using OnlineCassino.WebApi.Providers;
using System;
using System.Linq;
using System.Collections.Generic;
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

        // GET: api/Games
        public IHttpActionResult Get()
        {
            var games = unitOfWork.GameSessions.GetAll();

            return Ok(Mapper.Map<List<GameDto>>(games.ToList()));
        }

        // GET: api/Games/5
        public IHttpActionResult Get(int id)
        {
            var game = unitOfWork.Games.GetById(id);

            if (game == null)
                return NotFound();

            return Ok(Mapper.Map<GameDto>(game));
        }

        // POST: api/Games
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Games/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Games/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}