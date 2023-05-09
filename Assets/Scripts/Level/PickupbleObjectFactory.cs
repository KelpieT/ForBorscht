using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PickupbleObjectFactory : MonoBehaviour
{
    [SerializeField] private PickupbleObject coinPickupblePrefab;
    [SerializeField] private PickupbleObject crystalPickupblePrefab;
    private Player player;

    [Inject]
    private void Construct(Player player)
    {
        this.player = player;
    }

    public PickupbleObject Create(ItemID itemID)
    {
        PickupbleObject prefab = null;
        switch (itemID)
        {
            case ItemID.Coin:
                prefab = coinPickupblePrefab;
                break;
            case ItemID.Crystal:
                prefab = crystalPickupblePrefab;
                break;
            default:
                return null;
        }
        PickupbleObject pickupbleObject = Instantiate<PickupbleObject>(prefab);
        pickupbleObject.Construct(player);
        return pickupbleObject;
    }
}
