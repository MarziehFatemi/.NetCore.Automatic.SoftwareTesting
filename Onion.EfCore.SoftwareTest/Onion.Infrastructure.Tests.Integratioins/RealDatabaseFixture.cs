using Microsoft.EntityFrameworkCore;
using Onion.Domain.Product_agg;
using Onion.Domain.Product_Category_agg;
using Onion.Infrastructure.EfCore;
using Onion.Infrastructure.EfCore.Repository;
using System.Transactions;

namespace Onion.Infrastructure.Tests.Integratioins
{
    public class RealDatabaseFixture : IDisposable
    {
        public Context TestContext;
        private readonly TransactionScope _Scope; 

        public RealDatabaseFixture()
        {
            var Options = new DbContextOptionsBuilder<Context>()
               .UseSqlServer("Server=.;Database=Onion.EfCore_db;Trusted_Connection=True;")
               .Options;
            TestContext = new Context(Options);
            _Scope = new TransactionScope();

            


            // 1 seed data for product category 
            TestContext.Add(new ProductCategory(Guid.NewGuid().ToString()));
            TestContext.SaveChanges();


        }

        public void Dispose()
        {
            TestContext.Dispose();
            _Scope.Dispose();
        }
    }
}
