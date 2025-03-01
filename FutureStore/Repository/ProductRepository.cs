using FutureStore.Data;
using FutureStore.Interfaces;
using FutureStore.Models.Buisness;

namespace FutureStore.GenericRepository
{
    public class ProductRepository :GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(FutureStoreContext context)
       : base(context)
        {


        }
    }
}
