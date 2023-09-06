using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class ApiWebTokenContext:DbContext
    {
        public ApiWebTokenContext(DbContextOptions<ApiWebTokenContext> options) : base(options)
        {
        }
    }
}