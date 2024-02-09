﻿using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interfaces;
using CleanArchMVC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMVC.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _productContext;
        public ProductRepository(ApplicationDbContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _productContext.products.FindAsync(id);

        }

        public async Task<IEnumerable<Product>> GetProductAsync()
        {
            return await _productContext.products.ToListAsync();
        }

        public async Task<Product> GetProductCategoryAsync(int? id)
        {
            return await _productContext.products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }
    }
}