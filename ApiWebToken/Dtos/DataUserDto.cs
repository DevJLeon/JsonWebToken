using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWebToken.Dtos;

    public class DataUserDto
    {
        public string Mensaje { get; set; }
        public bool EstaAutenticado { get; set; }
        public string Username { get; set; }
        public string  Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

    }
