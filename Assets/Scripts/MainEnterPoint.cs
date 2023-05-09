using UnityEngine;
using VContainer;

public class MainEnterPoint : MonoBehaviour
{
    private Player player;
    private Level level;
    private LevelCamera levelCamera;
    private MainUI mainUI;

    [Inject]
    public void Construct(Player player, Level level, LevelCamera levelCamera, MainUI mainUI)
    {
        this.player = player;
        this.level = level;
        this.levelCamera = levelCamera;
        this.mainUI = mainUI;
    }

    public void Init()
    {
        player.Init();
        level.Init();
        levelCamera.Init();
        mainUI.Init();
    }
}
