using Paratha.Services.ProductAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Paratha.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts(Expression<Func<ProductDto, bool>> filter = null, Func<IQueryable<ProductDto>, IOrderedQueryable<ProductDto>> orderBy = null, string includeProperties = "", bool noTracking = false);
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> Delete(int productId);
    }
}
