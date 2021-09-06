using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private float respawnTime;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int amountSpawnWeapons;
    
    private WeaponManager _weaponManager;
    private int _currentSpawnWeapons;
    private bool _isUnlockSpawn = true;
    
    [Inject]
    private void Construct(WeaponManager weaponManager)
    {
        _weaponManager = weaponManager;
    }

    private void Update()
    {
        if (_currentSpawnWeapons < amountSpawnWeapons)
        {
            if (_isUnlockSpawn)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    private void Respawn(DroppedWeapon droppedWeapon)
    {
        droppedWeapon.OnDestroy -= Respawn;
        _currentSpawnWeapons--;
    }

    private IEnumerator Spawn()
    {
        _isUnlockSpawn = false;
        yield return new WaitForSeconds(respawnTime);
        var allKeys = Enum.GetValues(typeof(WeaponClassName));
        var randomKey = (WeaponClassName)allKeys.GetValue(Random.Range(0, allKeys.Length));
        
        var spawnPos = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var newSpawnObject = _weaponManager.GetDroppedWeaponByWeaponName(randomKey);
        var weapon = newSpawnObject.GetComponent<DroppedWeapon>();
        
        weapon.Initialize(_weaponManager.GetAmountUseByType(randomKey),randomKey);
        newSpawnObject.transform.position = spawnPos.position;
        newSpawnObject.transform.rotation = spawnPos.rotation;
        _currentSpawnWeapons++;
        weapon.OnDestroy += Respawn;
        _isUnlockSpawn = true;
    }
}