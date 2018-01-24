using OnlineCassino.Domain;
using System;
using System.Collections.Generic;

namespace OnlineCassino.WebApi.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GameCategory GameCategory { get; set; }
        public IEnumerable<DeviceTypeDto> DevicesAvailability { get; set; }
        public IEnumerable<GameCollectionDto> GameCollections { get; set; }
    }
}