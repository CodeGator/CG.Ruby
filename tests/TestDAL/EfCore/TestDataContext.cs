using Microsoft.EntityFrameworkCore;

namespace TestDAL.EfCore
{
    public class TestDataContext : DbContext
    {
        public TestDataContext(
            DbContextOptions<TestDataContext> options
            ) : base(options)
        {

        }
    }
}
