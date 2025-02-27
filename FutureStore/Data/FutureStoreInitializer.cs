using FutureStore.Models.Buisness;

namespace FutureStore.Data
{
    public static class FutureStoreInitializer
    {
        public static void Initializer(FutureStoreContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return; // Categories already seeded, no need to proceed
            }

            var categories = new Category[]
            {
                new Category{   NameEN="Mobiles" ,NameAR="موبايل"},
                new Category{   NameEN="TVs" , NameAR="تليفيزيون"},
                new Category{   NameEN="Cameras" , NameAR = "كاميرا"},
                new Category{   NameEN="LapTops"  , NameAR = "لابتوب"},
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();

            // Now that categories are added, populate products
            var products = new Product[]
            {
                new Product { NameEN="LG Smart 40 Inch", Price=16000, CategoryId = categories.SingleOrDefault(c => c.NameEN == "TVs").Id },
               new Product { NameEN = "Sory Digital", Price = 76000, CategoryId = categories.SingleOrDefault(c => c.NameEN == "Cameras").Id },
                new Product { NameEN="HP probook 4045", Price=11000, CategoryId = categories.SingleOrDefault(c => c.NameEN == "LapTops").Id },
                new Product { NameEN="Iphone 14", Price=85000, CategoryId = categories.SingleOrDefault(c => c.NameEN == "Mobiles").Id },
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}
