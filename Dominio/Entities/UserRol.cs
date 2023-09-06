using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class UserRol:BaseEntity
    {
        public int UsuarioIdFk { get; set; }
        public User Usuario { get; set; }
        public int RolIdFk { get; set; }
        public Rol Rol { get; set; }
    }
}