﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository

    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        => await _context.ProductBrands.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int? id)
        => await _context.products
            .Include(product => product.ProductType)
            .Include(product => product.ProductBrand)
            .FirstOrDefaultAsync(product => product.Id == id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
         => await _context.products
            .Include(product => product.ProductType)
            .Include(product => product.ProductBrand)
            .ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetProducttypesAsync()
        => await _context.ProductTypes.ToListAsync();
    }
}
