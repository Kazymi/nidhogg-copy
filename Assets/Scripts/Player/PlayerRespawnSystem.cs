using System;
using Random = UnityEngine.Random;

public class PlayerRespawnSystem
{
    private PlayerRespawnConfiguration _respawnConfiguration;
    private IPlayerMovement _playerMovement;

    public event Action RespawnAction;

    public PlayerRespawnSystem(PlayerRespawnConfiguration playerRespawnConfiguration, IPlayerMovement playerMovement)
    {
        _respawnConfiguration = playerRespawnConfiguration;
        _playerMovement = playerMovement;
    }
    
    public void Respawn()
    {
        _playerMovement.SetPosition(
            _respawnConfiguration.SpawnPoints[Random.Range(0, _respawnConfiguration.SpawnPoints.Count)]);
        RespawnAction?.Invoke();
    }
}