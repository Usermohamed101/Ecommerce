using Ecommerce.Dtos.Address;
using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public interface IAddressRepo:IGeneric<Address,int>
    {


      

        Task<ICollection<Address>> GetByUserIdAsync(string id);

        






    }
}
