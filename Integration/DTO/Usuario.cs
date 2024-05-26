using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GlovoSoft.Integration.DTO
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Email { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }
        public string? Avatar { get; set; }

    }
}