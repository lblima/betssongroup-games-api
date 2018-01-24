using System.Collections.Generic;

namespace OnlineCassino.WebApi.DTOs
{
    public class GameCollectionDto
    {
        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public IEnumerable<GameDto> Games { get; set; }
        public IEnumerable<GameCollectionDto> SubCollections { get; set; }
    }
}