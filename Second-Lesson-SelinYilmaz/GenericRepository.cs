using SipayApi.Base;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace SipayApi.Data.Repository;

public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
{
    private readonly List<Entity> _entities;

    public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression) 
    {
        return _entities.Where(expression.Compile());
    }
}
