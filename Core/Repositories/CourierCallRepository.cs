using System.Data.Entity;

namespace Core.Repositories
{
    public interface ICourierCallRepository : IRepositoy<CourierCall>
    {

    }

    public class CourierCallRepository : Repository<CourierCall>, ICourierCallRepository
    {
        public CourierCallRepository(DbContext context)
            : base(context)
        {
        }
    }
}
