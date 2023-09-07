using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiWebToken.Dtos;
    public class AddRoleDto
    {
        [Required]
        public string Username{ get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        public string Rol{ get; set; }
    }