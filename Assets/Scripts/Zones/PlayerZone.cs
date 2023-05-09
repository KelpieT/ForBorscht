using UnityEngine;
using VContainer;

public class PlayerZone : MonoBehaviour
{
    private Inventory inventory = new Inventory();
    private Player player;

    public Inventory Inventory { get => inventory; }

    [Inject]
    public void Construct(Player player)
    {
        this.player = player;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(ConstParams.TAG_PLAYER))
        {
            if (player.Inventory.IsEmpty() == false)
            {
                inventory.PasteInventory(player.Inventory);
            }
        }
    }
}
