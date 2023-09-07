using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IRol : IGenericRepository<Rol>
{
    new Task<Rol> GetByIdAsync(string id);
}