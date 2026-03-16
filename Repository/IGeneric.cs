namespace Ecommerce.Repository
{
    public interface IGeneric<Entity,T> where Entity:class
    {

        Task<ICollection<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(T id);

        Task Add(Entity e);

        Task Delete(Entity e);

        Task Update(Entity e);

        Task<int> SaveChangesAsync();


    }
}
