using Microsoft.EntityFrameworkCore;
using PhoneShopServer.Data;
using PhoneShopShareLibrary.Contracts;
using PhoneShopShareLibrary.Models;
using PhoneShopShareLibrary.Responses;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhoneShopServer.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly AppDBContext appDbContext;
        public ProductRepository(AppDBContext appDBContext)
        {
            this.appDbContext = appDBContext;
        }
        public async Task<ServiceResponse> AddProduct(Product model)
        {
            if (model is null) return new ServiceResponse(false, "Null product");
            var (flag, message) = await CheckName(model.Name!);
            if (flag)
            {
                appDbContext.Products.Add(model);
                await Commit();
                return new ServiceResponse(true, "Product Added");
            }
            return new ServiceResponse(false, message);

        }

        public async Task<List<Product>> GetAllProducts(bool featured)
        {
            if (featured)
            {
                var data= await appDbContext.Products.Where(p => p.Featured).ToListAsync();
                return data;
            }

            else
            {
                var data = await appDbContext.Products.ToListAsync();
                return data;
            }
            return null;
        }

        public async Task<ServiceResponse> CheckName(string name)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return product is null? new ServiceResponse(true, null) : new ServiceResponse(false, "Product Exist");
        }

        private async Task Commit() => appDbContext.SaveChanges();
    }
}
