using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class RestartMenu : Window
{
    [SerializeField] private Button restartButton;
    private Level level;
    private Player player;

    [Inject]
    private void Construct(Level level, Player player)
    {
        this.level = level;
        this.player = player;
    }

    public void Init()
    {
        player.OnDead += () => this.Show(true);
        restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        level.RevivePlayer();
        this.Show(false);
    }
}
