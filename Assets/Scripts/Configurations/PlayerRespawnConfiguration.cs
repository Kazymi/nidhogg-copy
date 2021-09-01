using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRespawnConfiguration
{
    [SerializeField] private List<Transform> spawnPoints;

    public List<Transform> SpawnPoints => spawnPoints;
}