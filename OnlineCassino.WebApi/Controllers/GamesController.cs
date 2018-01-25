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
    public class GamesController : ApiController
    {
        IUnitOfWork unitOfWork;

        public GamesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Games
        public IHttpActionResult Get(int page = 0, int pageSize = 10)
        {
            if (page < 0)
                return BadRequest("page should be 0 or more");

            if (pageSize < 0)
                return BadRequest("pageSize should be 0 or more");

            var games = unitOfWork.Games.GetAll();

            var totalCount = games.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { controller = "Games", page = page - 1, pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("DefaultApi", new { controller = "Games", page = page + 1, pageSize }) : "";

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            return Ok(Mapper.Map<List<GameDto>>(games.OrderBy(x => x.Id).Skip(pageSize * page).Take(pageSize).ToList()));
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