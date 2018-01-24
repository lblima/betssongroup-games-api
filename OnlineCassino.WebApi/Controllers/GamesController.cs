using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineCassino.WebApi.Controllers
{
    public class GamesController : ApiController
    {
        // GET: api/Games
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Games/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Games
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Games/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Games/5
        public void Delete(int id)
        {
        }
    }
}
