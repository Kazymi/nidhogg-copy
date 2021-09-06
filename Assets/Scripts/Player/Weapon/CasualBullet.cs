using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class CasualBullet : MonoBehaviour, IPolledObject, IBullet
{
    private float _lifeTime;
    private float _flySpeed;
    private float _damage;
    private VFXConfiguration _vfxConfiguration;
    
    private float _currentTime;

    public Factory ParentFactory { get; set; }


    private void Update()
    {
        transform.position += transform.forward * _flySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage,_vfxConfiguration);
        }

        Destroy();
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy();
    }

    private void Destroy()
    {
        StopAllCoroutines();
        ParentFactory.Destroy(gameObject);
    }

    public void Initialize(BulletConfiguration bulletConfiguration)
    {
        _flySpeed = bulletConfiguration.FlySpeed;
        _damage = bulletConfiguration.Damage;
        _lifeTime = bulletConfiguration.LifeTime;
        _vfxConfiguration = bulletConfiguration.VFXEffect;
        StartCoroutine(DestroyTimer());
    }
}