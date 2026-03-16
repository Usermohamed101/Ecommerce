using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class AddressRepo(ECommerceContext contxt) : GenericRepo<Address, int>(contxt), IAddressRepo
    {
        public async Task<ICollection<Address>> GetByUserIdAsync(string id)
        {
            return await contxt.Set<Address>().Where(a => a.UserId == id).ToListAsync();
        }
    }
}
