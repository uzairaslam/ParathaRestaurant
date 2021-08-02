using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Paratha.Services.ProductAPI.DbContexts;
using Paratha.Services.ProductAPI.Models;
using Paratha.Services.ProductAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Paratha.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _db;
        private DbSet<Product> _dbSet;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _dbSet = _db.Set<Product>();
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            if (product.ProductId > 0)
            {
                _db.Update(product);
            }
            else
            {
                _db.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> Delete(int productId)
        {
            try
            {
                Product product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product == null)
                    return false;

                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(Expression<Func<ProductDto, bool>> filter = null, Func<IQueryable<ProductDto>, IOrderedQueryable<ProductDto>> orderBy = null, string includeProperties = "", bool noTracking = false)
        {
            IQueryable<Product> query = noTracking ? _dbSet.AsNoTracking() : _dbSet;
            IQueryable<ProductDto> productDtos = query.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
            if (filter != null)
            {
                productDtos = productDtos.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
              (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                productDtos = productDtos.Include(includeProperty);
            }

            if (orderBy != null)
                productDtos = orderBy(productDtos);

            return await productDtos.ToListAsync();
        }
    }
}
