using UnityEngine;

[CreateAssetMenu(menuName = "Bullet configuration", fileName = "Bullet configuration")]
public class BulletConfiguration : ScriptableObject
{
    [SerializeField] private GameObject ammoGameObject;
    [SerializeField] private float damage;

    public GameObject AmmoGameObject => ammoGameObject;
    public float Damage => damage;
}
