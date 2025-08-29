using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Models;
using OnlineShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _db;
        public ProductRepository(ShopDbContext db) => _db = db;


        public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);


        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default)
        => await _db.Products.AsNoTracking().OrderBy(p => p.Name).ToListAsync(ct);


        public async Task AddAsync(Product product, CancellationToken ct = default)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync(ct);
        }


        public async Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync(ct);
        }


        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.Products.FindAsync(new object?[] { id }, ct);
            if (entity is null) return;
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
