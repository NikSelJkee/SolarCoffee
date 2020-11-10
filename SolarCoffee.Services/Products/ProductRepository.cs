using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly SolarDbContext _db;
        public ProductRepository(SolarDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Archives a Product by setting boolean IsArchived to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Product>> ArchieveProductAsync(int id)
        {
            try
            {
                var product = await _db.Products.FindAsync(id);
                product.IsArchived = true;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archived Product",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    IsSuccess = false
                };
            }
        }

        /// <summary>
        /// Adds a new product to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<Product>> CreateProductAsync(Product product)
        {
            try
            {
                await _db.Products.AddAsync(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                await _db.ProductInventories.AddAsync(newInventory);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    IsSuccess = false
                };
            }
        }

        /// <summary>
        /// Retrieves a Product from the database by primary key 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all Product from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _db.Products.ToListAsync(); 
        }
    }
}
