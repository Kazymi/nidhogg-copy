using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerRespawnSystem
{
    private List<Transform> _spawnPoints;
    private IPlayerMovement _playerMovement;

    public event Action RespawnAction;

    public PlayerRespawnSystem(List<Transform> spawnpoints, IPlayerMovement playerMovement)
    {
        _spawnPoints = spawnpoints;
        _playerMovement = playerMovement;
    }
    
    public void Respawn()
    {
        _playerMovement.SetPosition(
           _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
        RespawnAction?.Invoke();
    }
}