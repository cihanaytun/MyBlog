using Microsoft.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.DataAccess.Context;
using MyBlog.Entities.Abstract;
using System.Linq.Expressions;

namespace MyBlog.DataAccess.Concreate.EfCore
{
    public class EfRepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity, new()
    {
        public MyBlogContext myBlogContext { get; set; }

        public EfRepositoryBase()
        {
            myBlogContext = new MyBlogContext();
        }


        public virtual async Task<int> CreateAsync(T entity)
        {
            await myBlogContext.Set<T>().AddAsync(entity);
            return await myBlogContext.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            myBlogContext.Set<T>().Update(entity);
            return await myBlogContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            myBlogContext.Set<T>().Remove(entity);
            return await myBlogContext.SaveChangesAsync();
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await myBlogContext.Set<T>().FindAsync(Guid.Parse(id));

        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return await myBlogContext.Set<T>().Where(filter).FirstOrDefaultAsync();
            else
                return await myBlogContext.Set<T>().FirstOrDefaultAsync();


        }

        public virtual async Task<ICollection<T>> RawSqlQuery(T entity, string sql)
        {
            var result = myBlogContext.Set<T>().FromSqlRaw(sql);

            return await result.ToListAsync();
        }

        public virtual async Task<IList<T>> FindAllAsnyc(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return await myBlogContext.Set<T>().Where(filter).ToListAsync();
            else
                return await myBlogContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IQueryable<T>> FindAllIncludeAsync(Expression<Func<T, bool>> filter = null
            , params Expression<Func<T, object>>[] include)
        {
            var query = myBlogContext.Set<T>();
            if (filter != null)
            {
                query.Where(filter);
            }
            var result = include.Aggregate(query.AsQueryable(),
                                    (current, includeprop) => current.Include(includeprop));
            return result;
        }
    }
}
