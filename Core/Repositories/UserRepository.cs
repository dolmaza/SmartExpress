using System.Data.Entity;

namespace Core.Repositories
{
    public interface IUserRepository : IRepositoy<User>
    {

    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
    }
}
