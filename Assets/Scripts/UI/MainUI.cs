using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private RestartMenu restartMenu;
    [SerializeField] private GameHUD gameHUD;

    public void Init()
    {
        restartMenu.Init();
        gameHUD.Init();
    }

    public void ShowRestartWindow()
    {
        restartMenu.Show(true);
    }
}
