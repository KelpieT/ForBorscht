using System.Collections;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class PickupbleObject : MonoBehaviour
{
    [SerializeField] private InventoryItem inventoryItem;
    [SerializeField] private float pickupRadius;
    private Player player;

    [Inject]
    public void Construct(Player player)
    {
        this.player = player;
    }

    private void Start()
    {
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    
    private void Update()
    {
        if ((transform.position - player.transform.position).magnitude < pickupRadius)
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        player.PickUp(inventoryItem);
        Destroy(gameObject);
    }
}
