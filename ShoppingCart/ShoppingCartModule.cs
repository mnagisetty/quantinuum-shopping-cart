namespace ShoppingCartService.ShoppingCart
{
    using Nancy;
    using Nancy.ModelBinding;
    using ShoppingCartService.EventStore;
    using ShoppingCartService.ProductCatalogClient;

    public class ShoppingCartModule : NancyModule
    {
        public ShoppingCartModule(IShoppingCartStore shoppingCartStore, IProductCatalogClient productCatalog, IEventStore eventStore)
           : base("/shopping-cart")
        {
            Get("/{userid:int}", parameters =>
            {
                var userId = (int)parameters.userid;
                return shoppingCartStore.Get(userId);
            });

            Post("/{userid:int}/items", async (parameters, _) =>
            {
                var productCatalogIds = this.Bind<int[]>();
                var userId = (int)parameters.userid;

                var shoppingCart = shoppingCartStore.Get(userId);
                var shoppingCartItems = await productCatalog.GetShoppingCartItems(productCatalogIds).ConfigureAwait(false);
                shoppingCart.AddItems(shoppingCartItems, eventStore);
                shoppingCartStore.Save(shoppingCart);

                return shoppingCart;
            });

            Delete("/{userid:int}/items", parameters =>
            {
                var productCatalogueIds = this.Bind<int[]>();
                var userId = (int)parameters.userid;

                var shoppingCart = shoppingCartStore.Get(userId);
                shoppingCart.RemoveItems(productCatalogueIds, eventStore);
                shoppingCartStore.Save(shoppingCart);

                return shoppingCart;
            });
        }
    }
}
