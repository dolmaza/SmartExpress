using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IUserRepository : IRepositoy<User>
    {
        User GetUserByContractNumber(string contractNumber);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public User GetUserByContractNumber(string contractNumber)
        {
            return Find(u => u.ContractNumber == contractNumber).Include(u => u.Role).FirstOrDefault();
        }
    }
}
