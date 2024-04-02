using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

public class Basket : BaseEntity, IAggregateRoot
{
    public string BuyerId { get; private set; }
    private readonly List<BasketItem> _items = new List<BasketItem>();
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

    public int TotalItems => _items.Sum(i => i.Quantity);


    public Basket(string buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int catalogItemId, decimal unitPrice, int quantity = 1)
    {
        const int maxQuantityPerItem = 10; 

        var existingItems = Items.Where(i => i.CatalogItemId == catalogItemId).ToList();

        if (!existingItems.Any())
        {
            quantity = Math.Min(quantity, maxQuantityPerItem); // Don't add more than the limit
            _items.Add(new BasketItem(catalogItemId, quantity, unitPrice));
            return;
        }
        
        foreach (var existingItem in existingItems)
        {
            if (existingItem.UnitPrice != unitPrice)
            {
                existingItem.SetUnitPrice(unitPrice); // Update the unit price
            }

            int newQuantity = existingItem.Quantity + quantity;
            if (newQuantity > maxQuantityPerItem)
            {
                existingItem.SetQuantity(maxQuantityPerItem); // Adjust to the limit
            }
            else
            {
                existingItem.AddQuantity(quantity);
            }
        }
    }

    public void RemoveEmptyItems()
    {
        _items.RemoveAll(i => i.Quantity == 0);
    }

    public void SetNewBuyerId(string buyerId)
    {
        BuyerId = buyerId;
    }
}
