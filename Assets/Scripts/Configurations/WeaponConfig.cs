using System;
using UnityEngine;

[Serializable]
public class PlayerWeaponManagerConfig
{
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform bodyTransform;

    public Transform BodyTransform => bodyTransform;
    public Transform RightHandTransform => rightHandTransform;
}