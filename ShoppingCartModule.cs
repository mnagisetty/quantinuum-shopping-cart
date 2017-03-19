namespace ShoppingCartService
{
    using Nancy;

    public class ShoppingCartModule : NancyModule
    {
        public ShoppingCartModule(IShoppingCartStore shoppingCartStore)
        {
            Get("/{userid:int}", parameters => 
            {
                var userId = (int) parameters.userid;
                return shoppingCartStore.Get(userId);
            });
        }
    }
}
