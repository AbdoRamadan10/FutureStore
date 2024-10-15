using FutureStore.Data;
using FutureStore.Interfaces;
using FutureStore.Models;

namespace FutureStore.GenericRepository
{
    public class CategoryRepository :GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(FutureStoreContext context)
       : base(context)
        {
        }
    }
}
