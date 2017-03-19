namespace ShoppingCartService
{
    public interface IShoppingCartStore
    {
        ShoppingCart Get(int userId);
    }
}