using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Concreate.EfCore;
using MyBlog.DataAccess.Context;
using MyBlog.Entities.Abstract;
using System.Linq.Expressions;

namespace MyBlog.Business.Concreate
{
    public class ManagerBase<T> : IManagerService<T> where T : BaseEntity, new()
    {

        public EfRepositoryBase<T> _efRepositoryBase;
        public MyBlogContext _myBlogContext { get; set; }


        public ManagerBase()
        {
            _efRepositoryBase = new EfRepositoryBase<T>();
            _myBlogContext = new MyBlogContext();

        }

        public virtual async Task<int> CreateAsync(T entity)
        {
            return await _efRepositoryBase.CreateAsync(entity);
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            return await _efRepositoryBase.UpdateAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            return await _efRepositoryBase.DeleteAsync(entity);
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _efRepositoryBase.GetByIdAsync(id);
        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>>? filter = null)
        {
            return await _efRepositoryBase.FindAsync(filter);
        }


        public virtual async Task<IList<T>> FindAllAsnyc(Expression<Func<T, bool>>? filter = null)
        {
            return await _efRepositoryBase.FindAllAsnyc(filter);
        }

        public virtual async Task<IQueryable<T>> FindAllIncludeAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? include)
        {
            return await _efRepositoryBase.FindAllIncludeAsync(filter, include);
        }
    }
}
