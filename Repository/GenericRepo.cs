using Ecommerce.Exceptions;
using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class GenericRepo<TEnitiy,T>(ECommerceContext contxt) : IGeneric<TEnitiy,T> where TEnitiy : class
    {
        public Task Add(TEnitiy e)
        {
            contxt.Set<TEnitiy>().Add(e);
            return Task.CompletedTask;
        }

        public Task Delete(TEnitiy e)
        {
            contxt.Set<TEnitiy>().Remove(e);
            return Task.CompletedTask;
        }

        public async Task<ICollection<TEnitiy>> GetAllAsync()
        {
            return await contxt.Set<TEnitiy>().AsNoTracking().ToListAsync();
        }

        public async Task<TEnitiy> GetByIdAsync(T id)
        {
            return await contxt.Set<TEnitiy>().FindAsync(id)??throw new ElementIsNotFound($"Id {id} is not found..");
        }

        public async Task<int> SaveChangesAsync()
        {
            return await contxt.SaveChangesAsync();
        }

        public Task Update(TEnitiy e)
        {
            contxt.Set<TEnitiy>().Update(e);
            return Task.CompletedTask;
        }
    }
}
