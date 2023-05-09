using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public event Action<InventoryItem> OnItemChanged;
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public IEnumerable<InventoryItem> InventoryItems { get => inventoryItems; }

    public void Add(InventoryItem inventoryItem)
    {
        InventoryItem item = inventoryItems.FirstOrDefault(x => x.itemID == inventoryItem.itemID);
        if (item == null)
        {
            item = new InventoryItem() { itemID = inventoryItem.itemID, count = inventoryItem.count };
            inventoryItems.Add(item);
        }
        else
        {
            item.count += inventoryItem.count;
        }
        OnItemChanged?.Invoke(item);
    }

    public void PasteInventory(Inventory inventory)
    {
        foreach (var item in inventory.InventoryItems)
        {
            Add(item);
        }
        inventory.RemoveAll();

    }

    public bool Remove(InventoryItem inventoryItem)
    {
        InventoryItem item = inventoryItems.FirstOrDefault(x => x.itemID == inventoryItem.itemID);
        if (item == null)
        {
            if (inventoryItem.count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (item.count >= inventoryItem.count)
            {
                item.count -= inventoryItem.count;
                OnItemChanged?.Invoke(item);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void RemoveAll()
    {
        foreach (var item in inventoryItems)
        {
            item.count = 0;
            OnItemChanged?.Invoke(item);
        }
        inventoryItems.Clear();
    }

    public bool IsEmpty()
    {
        return (inventoryItems.Any(x => x.count > 0) == false);
    }
}
