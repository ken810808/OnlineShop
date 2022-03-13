using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace OnlineShop.Repository
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// 判斷數據是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查詢列表數據
        /// </summary>
        /// <param name="expression">SQL條件</param>
        /// <returns></returns>
        List<T> Get(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查詢列表數據
        /// </summary>
        /// <param name="expression">SQL條件</param>
        /// <returns></returns>
        List<T> GetAsNotracking(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Save(T entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update(T entity);

        /// <summary>
        /// 僅修改指定欄位
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateOnlyColumn(T entity, Expression<Func<T, object>> expression);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Delete(T entity);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private DbSet<T> _entity;

        protected DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        /// <summary>
        /// 判斷數據是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _entity.AnyAsync(expression);
        }

        /// <summary>
        /// 查詢列表數據
        /// </summary>
        /// <param name="expression">SQL條件</param>
        /// <returns></returns>
        public List<T> Get(Expression<Func<T, bool>> expression)
        {
            return _entity.Where(expression).ToList();
        }

        /// <summary>
        /// 查詢列表數據
        /// </summary>
        /// <param name="expression">SQL條件</param>
        /// <returns></returns>
        public List<T> GetAsNotracking(Expression<Func<T, bool>> expression)
        {
            return _entity.Where(expression).AsNoTracking().ToList();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Save(T entity)
        {
            await _entity.AddAsync(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(T entity)
        {
            _entity.Update(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// 僅修改指定欄位
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOnlyColumn(T entity, Expression<Func<T, object>> expression)
        {
            _entity.Attach(entity);
            foreach (var proInfo in expression.GetPropertyAccessList())
            {
                if(string.IsNullOrWhiteSpace(proInfo.Name))
                    _context.Entry(entity).Property(proInfo.Name).IsModified = true;    
            }
            return (await _context.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Delete(T entity)
        {
            _entity.Remove(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
