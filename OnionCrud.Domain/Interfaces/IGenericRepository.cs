using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Domain.Interfaces
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> GetByIdAsync(int id);
        Task<TModel> GetEverythingAsync(Expression<Func<TModel, bool>> filter);
        Task<TModel> CreateAsync(TModel model);
        Task<bool> UpdateAsync(TModel model);
        Task<bool> SoftDeleteAsync(TModel model);
        Task<IQueryable<TModel>> VerifyDataExistenceAsync(Expression<Func<TModel, bool>> filter = null);

    }

}
