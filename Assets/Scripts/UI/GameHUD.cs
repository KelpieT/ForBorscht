using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameHUD : MonoBehaviour
{
    private const string HP = "HP ";
    private const string FORMAT = "0.#";
    [SerializeField] private Button restartButton;
    [SerializeField] private TMP_Text coinsCount;
    [SerializeField] private TMP_Text crystalsCount;
    [SerializeField] private TMP_Text health;
    private MainUI mainUI;
    private Level level;
    private Player player;
    private PlayerZone playerZone;

    [Inject]
    public void Construct(MainUI mainUI, Level level, Player player, PlayerZone playerZone)
    {
        this.mainUI = mainUI;
        this.level = level;
        this.player = player;
        this.playerZone = playerZone;
    }

    public void Init()
    {
        restartButton.onClick.AddListener(mainUI.ShowRestartWindow);
        player.OnHealthChanged += ShowPlayerHP;
        ShowPlayerHP();
        playerZone.Inventory.OnItemChanged += ShowChangedItem;
    }

    private void ShowChangedItem(InventoryItem inventoryItem)
    {
        switch (inventoryItem.itemID)
        {
            case ItemID.Coin:
                ShowCoinsCount(inventoryItem.count);
                break;
            case ItemID.Crystal:
                ShowCrystalsCount(inventoryItem.count);
                break;
        }
    }
    public void ShowCoinsCount(int count)
    {
        coinsCount.text = count.ToString();
    }

    public void ShowCrystalsCount(int count)
    {
        crystalsCount.text = count.ToString();
    }

    private void ShowPlayerHP()
    {
        health.text = HP + player.Health.ToString(FORMAT);
    }
}
