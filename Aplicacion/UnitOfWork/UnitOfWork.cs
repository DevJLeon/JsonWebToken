using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWebToken.Repository;
using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiWebTokenContext context;
    private UserRepository users;
    private RolRepository rols;

    public UnitOfWork(ApiWebTokenContext _context)
    {
        context = _context;

    }
    public IUser Users
    {
        get
        {
            if (users == null)
            {
                users = new UserRepository(context);
            }
            return users;
        }
    }
    public IRol Rols
    {
        get
        {
            if (rols == null)
            {
                rols = new RolRepository(context);
            }
            return rols;
        }
    }


    public void Dispose()
    {
        context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}