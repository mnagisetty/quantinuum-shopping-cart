using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartService.ShoppingCart;

namespace ShoppingCartService.ProductCatalogClient
{
    public interface IProductCatalogClient
    {
         Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds);
    }
}
