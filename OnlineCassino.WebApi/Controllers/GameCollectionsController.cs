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
    public class GameCollectionsController : ApiController
    {
        IUnitOfWork unitOfWork;
        IIdentityProvider identityProvider;

        public GameCollectionsController(IUnitOfWork unitOfWork, IIdentityProvider identityProvider)
        {
            this.unitOfWork = unitOfWork;
            this.identityProvider = identityProvider;
        }

        // GET: api/GameCollections
        public IHttpActionResult Get()
        {
            var gameCollections = unitOfWork.GameCollections.GetAll();

            return Ok(Mapper.Map<List<GameCollectionDto>>(gameCollections.ToList()));
        }

        // GET: api/GameCollections/5
        public IHttpActionResult Get(int id)
        {
            var gameCollection = unitOfWork.GameCollections.GetById(id);

            if (gameCollection == null)
                return NotFound();

            return Ok(Mapper.Map<GameCollectionDto>(gameCollection));
        }

        // POST: api/GameCollections
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/GameCollections/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/GameCollections/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}