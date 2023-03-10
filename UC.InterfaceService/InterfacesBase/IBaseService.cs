
using System.Linq.Expressions;

namespace UC.InterfaceService.InterfacesBase
{
    public interface IBaseService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll_SP();
        Task<List<T>> GetAll(Expression<Func<T, bool>> IDExpression);
        Task<T> GetByWhere(Expression<Func<T, bool>> IDExpression);
        Task<T> GetByID_SP(int ID);
        Task<bool> Insert(T entity);
        Task<bool> InsertRange(List<T> entitis);
        bool Update(T entity);
        bool UpdateRange(List<T> entitis);
        bool Delete(T entity);
        bool DeleteRange(List<T> entitis);
        Task<bool> Exists(Expression<Func<T, bool>> IDExpression);
    }
}
