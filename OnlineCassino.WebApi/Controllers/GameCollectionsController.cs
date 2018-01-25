﻿using AutoMapper;
using OnlineCassino.Domain.Interfaces;
using OnlineCassino.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace OnlineCassino.WebApi.Controllers
{
    public class GameCollectionsController : ApiController
    {
        IUnitOfWork unitOfWork;

        public GameCollectionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/GameCollections
        public IHttpActionResult Get(int page = 0, int pageSize = 10)
        {
            var gameCollections = unitOfWork.GameCollections.GetAll();

            var totalCount = gameCollections.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { controller = "GameCollections", page = page - 1, pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("DefaultApi", new { controller = "GameCollections", page = page + 1, pageSize }) : "";

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            return Ok(Mapper.Map<List<GameCollectionDto>>(gameCollections.OrderBy(x => x.Id).Skip(pageSize * page).Take(pageSize).ToList()));
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