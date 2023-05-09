using Cinemachine;
using UnityEngine;
using VContainer;

public class LevelCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private Player player;

    [Inject]
    public void Construct(Player player)
    {
        this.player = player;
    }

    public void Init()
    {
        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
    }
}
