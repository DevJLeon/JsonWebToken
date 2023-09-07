
using Microsoft.EntityFrameworkCore;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace ApiWebToken.Repository;

public class UserRepository :GenericRepository<User>, IUser
{
    protected readonly ApiWebTokenContext _context;
    public UserRepository(ApiWebTokenContext context) : base (context)
    {
        _context = context;
    }
    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
                    .Include(u=>u.Rols)
                    .FirstOrDefaultAsync(u=>u.UserName.ToLower()==username.ToLower());
    }
}