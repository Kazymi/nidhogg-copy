using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerRespawnSystem : MonoBehaviour,IPlayerRespawnSystem
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float timeToRespawn;
    
    private IPlayerMovement _playerMovement;

    public event Action RespawnAction;
    
    [Inject]
    private void Construct(IPlayerMovement playerMovement, IPlayerHealth playerHealth)
    {
        _playerMovement = playerMovement;
        playerHealth.PlayerDeath += target => StartCoroutine(Respawn());
    }
    
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        _playerMovement.SetPosition(
            spawnPoints[Random.Range(0, spawnPoints.Count)]);
        RespawnAction?.Invoke();
    }
}