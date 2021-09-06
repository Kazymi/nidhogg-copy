using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "VFX Configuration", fileName = "Bullet configuration")]
public class VFXConfiguration : ScriptableObject
{
    [SerializeField] private GameObject vfxEffectGameObject;

    public GameObject VFXEffectGameObject => vfxEffectGameObject;
}
