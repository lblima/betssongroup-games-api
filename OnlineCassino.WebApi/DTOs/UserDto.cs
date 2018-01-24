using OnlineCassino.Domain;
using System;
using System.Collections.Generic;

namespace OnlineCassino.WebApi.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountId { get; set; }
    }
}