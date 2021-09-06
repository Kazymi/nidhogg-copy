using UnityEngine;

[CreateAssetMenu(menuName = "Bullet configuration", fileName = "Bullet configuration")]
public class BulletConfiguration : ScriptableObject
{
    [SerializeField] private GameObject ammoGameObject;
    [SerializeField] private float damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private float flySpeed;
    [SerializeField] private VFXConfiguration vfxEffect;

    public GameObject AmmoGameObject => ammoGameObject;

    public float Damage => damage;

    public float LifeTime => lifeTime;

    public float FlySpeed => flySpeed;

    public VFXConfiguration VFXEffect => vfxEffect;
}
