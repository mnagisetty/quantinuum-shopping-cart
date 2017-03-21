using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingCartService.ShoppingCart;
using Polly;

namespace ShoppingCartService.ProductCatalogClient
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        private static string productCatalogueBaseUrl =
        @"http://private-05cc8-chapter2productcataloguemicroservice.apiary-mock.com";

        private static string getProductPathTemplate =
          "/products?productIds=[{0}]";

        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] ids) =>
            exponetialRetryPolicy.ExecuteAsync(async () =>
                await GetItemsFromCatalogService(ids).ConfigureAwait(false));

        private static async Task<HttpResponseMessage> RequestProductFromProductCatalog(int[] ids)
        {
            var productResource = string.Format(getProductPathTemplate, string.Join(",", ids));

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(productCatalogueBaseUrl);
                return await httpClient.GetAsync(productResource).ConfigureAwait(false);
            }
        }

        private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var products = JsonConvert.DeserializeObject<List<ProductCatalogProduct>>(
                await response.Content.ReadAsStringAsync().ConfigureAwait(false));

            return products.Select(p => new ShoppingCartItem(
                int.Parse(p.ProductId),
                p.ProductName,
                p.ProductDescription,
                p.Price
            ));
        }

        private static async Task<IEnumerable<ShoppingCartItem>> GetItemsFromCatalogService(int[] ids)
        {
            var response = await RequestProductFromProductCatalog(ids).ConfigureAwait(false);

            return await ConvertToShoppingCartItems(response).ConfigureAwait(false);
        }

        private static Policy exponetialRetryPolicy = Policy.Handle<Exception>()
                                                            .WaitAndRetryAsync(3, attempt =>
                                                                TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)));

        private class ProductCatalogProduct
        {
            public string ProductId { get; set; }

            public string ProductName { get; set; }

            public string ProductDescription { get; set; }

            public Money Price { get; set; }
        }

    }
}
