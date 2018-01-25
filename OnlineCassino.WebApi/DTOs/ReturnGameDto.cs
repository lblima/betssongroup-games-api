using System.Collections.Generic;

namespace OnlineCassino.WebApi.DTOs
{
    public class ReturnGameDto
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string PrevPageLink { get; set; }
        public string NextPageLink { get; set; }      
        public IEnumerable<GameDto> Results { get; set; }
    }
}