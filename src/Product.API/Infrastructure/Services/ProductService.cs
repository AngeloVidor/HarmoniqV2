using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Domain.Interfaces;
using Product.API.Models;
using Stripe;

namespace Product.API.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly Stripe.ProductService _productService;
        private readonly Stripe.PriceService _priceService;

        public ProductService(Stripe.ProductService productService, PriceService priceService, StripeSettings stripeSettings)
        {
            _productService = productService;
            _priceService = priceService;
            _stripeSettings = stripeSettings;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<(string productId, string priceId)> CreateProductAsync(Domain.Aggregates.Product product)
        {
            try
            {
                var productOptions = new ProductCreateOptions
                {
                    Name = product.Name,
                    Description = product.Description,
                };

                var createdProduct = await _productService.CreateAsync(productOptions);
                if (createdProduct == null)
                {
                    throw new Exception("Failed to create product in Stripe.");
                }

                var priceOptions = new PriceCreateOptions
                {
                    UnitAmount = (long)(product.Price * 100),
                    Currency = "usd",
                    Product = createdProduct.Id,
                };

                var price = await _priceService.CreateAsync(priceOptions);
                if (price == null)
                {
                    throw new Exception("Failed to create price for product in Stripe.");
                }
                return (createdProduct.Id, price.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating product in Stripe: " + ex.Message);
            }
        }
    }
}