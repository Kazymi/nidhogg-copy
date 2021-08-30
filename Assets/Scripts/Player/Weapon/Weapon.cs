using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private BulletConfiguration bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform playerPivot;

    private BulletManager _bulletManager;
    private float _currentTimer;
    private IInputHandler _inputHandler;

    private void OnEnable()
    {
        _inputHandler.Fire.Action += StartFire;
    }

    private void OnDisable()
    {
        _inputHandler.Fire.Action -= StartFire;
    }
    
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0,90*playerPivot.forward.x,0);
        if (_currentTimer >= 0)
        {
            _currentTimer -= Time.deltaTime;
        }
    }
    
    private void StartFire()
    {
        if (_currentTimer < 0)
        {
            _currentTimer = fireRate;
            Fire();
        }
    }
    
    private void Fire()
    {
        var newBullet = _bulletManager.GetBulletByBulletConfiguration(bullet);
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
    }

    [Inject]
    private void Construct(IInputHandler inputHandler,BulletManager bulletManager)
    {
        _bulletManager = bulletManager;
        _inputHandler = inputHandler;
    }
    
}
