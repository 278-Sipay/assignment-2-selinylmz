using static Dapper.SqlMapper;
using System.Linq.Expressions;

namespace SipayApi.Data.Repository;

public interface IGenericRepository<Entity> where Entity : class
{
    IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression);
}
