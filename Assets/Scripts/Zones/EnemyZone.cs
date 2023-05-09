using System;
using UnityEngine;
using VContainer;

public class EnemyZone : MonoBehaviour
{
    public event Action<bool> OnPlayerInZone;
    private Player player;

    [Inject]
    public void Construct(Player player)
    {
        this.player = player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstParams.TAG_PLAYER))
        {
            player.ChangeAttackState(Player.PlayerCommand.Attack);
            OnPlayerInZone?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ConstParams.TAG_PLAYER))
        {
            player.ChangeAttackState(Player.PlayerCommand.Idle);
            OnPlayerInZone?.Invoke(false);

        }
    }
}
