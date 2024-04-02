// FILEPATH: /c:/sources/eshoponweb/tests/UnitTests/ApplicationCore/Entities/BasketAggregate/BasketTests.cs

using Xunit;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketAggregate
{
    public class BasketTests
    {
        [Fact]
        public void AddItem_NewItem_Success()
        {
            var basket = new Basket("buyer1");
            basket.AddItem(1, 10m, 2);

            Assert.Single(basket.Items);
            Assert.Equal(2, basket.Items.First().Quantity);
            Assert.Equal(10m, basket.Items.First().UnitPrice);
        }

        [Fact]
        public void AddItem_ExistingItemIncreaseQuantity_Success()
        {
            var basket = new Basket("buyer1");
            basket.AddItem(1, 10m, 2);
            basket.AddItem(1, 10m, 3);

            Assert.Single(basket.Items);
            Assert.Equal(5, basket.Items.First().Quantity);
            Assert.Equal(10m, basket.Items.First().UnitPrice);
        }

        [Fact]
        public void AddItem_ExistingItemChangePrice_Success()
        {
            var basket = new Basket("buyer1");
            basket.AddItem(1, 10m, 2);
            basket.AddItem(1, 15m, 3);

            Assert.Single(basket.Items);
            Assert.Equal(5, basket.Items.First().Quantity);
            Assert.Equal(15m, basket.Items.First().UnitPrice);
        }

        [Fact]
        public void AddItem_QuantityExceedsLimit_Success()
        {
            var basket = new Basket("buyer1");
            basket.AddItem(1, 10m, 15);

            Assert.Single(basket.Items);
            Assert.Equal(10, basket.Items.First().Quantity);
            Assert.Equal(10m, basket.Items.First().UnitPrice);
        }
    }
}
