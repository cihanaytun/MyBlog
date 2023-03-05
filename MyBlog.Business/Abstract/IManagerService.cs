using MyBlog.DataAccess.Context;
using MyBlog.Entities.Abstract;
using System.Linq.Expressions;

namespace MyBlog.Business.Abstract
{
    public interface IManagerService<T> where T : BaseEntity, new()
    {

        MyBlogContext _myBlogContext { get; set; }

        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);

        Task<T> GetByIdAsync(string id);
        Task<T> FindAsync(Expression<Func<T, bool>>? filter = null);
        Task<IList<T>> FindAllAsnyc(Expression<Func<T, bool>>? filter = null);

        Task<IQueryable<T>> FindAllIncludeAsync(Expression<Func<T, bool>>? filter = null
            , params Expression<Func<T, object>>[]? include);



    }
}
