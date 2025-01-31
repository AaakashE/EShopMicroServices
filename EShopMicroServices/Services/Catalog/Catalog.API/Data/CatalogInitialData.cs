using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if(await session.Query<Product>().AnyAsync()) 
                return;

            session.Store<Product>(GetPreConfiguredProduct());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreConfiguredProduct() => new List<Product>()
        {
            new Product()
            {

            }
        }
    }
}
