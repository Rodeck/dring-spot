using System.Linq;
using System.Threading.Tasks;

namespace DringSpot.DataAccess.EF
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Collection { get; set; }

        Task Add(TEntity entity);
    }
}