namespace ShoppingCartService.ShoppingCart
{

    public class ShoppingCartItem
    {
        public int ProductCatalogId { get; }
        public string ProductName { get; }
        public string Description { get; }
        public Money Price { get; }

        public ShoppingCartItem(int productCatalogId, string productName, string description, Money price)
        {
            this.ProductCatalogId = productCatalogId;
            this.ProductName = productName;
            this.Description = description;
            this.Price = price;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var that = obj as ShoppingCartItem;
            return this.ProductCatalogId.Equals(that.ProductCatalogId);
        }

        public override int GetHashCode()
        {
            return this.ProductCatalogId.GetHashCode();
        }
    }

}