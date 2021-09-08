using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform firstPlayerTransform;
    [SerializeField] private Transform secondPlayerTransform;

    private bool _isFirstPlayerAlive = true;
    private bool _isSecondPlayerAlive = true;

    private Transform _target;
    
    [Inject(Optional = true, Id = PlayerType.FirstPlayer)]
    private PlayerHealth _firstPlayerHealth;

    [Inject(Optional = true, Id = PlayerType.SecondPlayer)]
    private PlayerHealth _secondPlayerHealth;

    [Inject(Optional = true, Id = PlayerType.FirstPlayer)]
    private PlayerRespawnSystem _firstPlayerRespawnSystem;

    [Inject(Optional = true, Id = PlayerType.SecondPlayer)]
    private PlayerRespawnSystem _secondPlayerRespawnSystem;

    private void Start()
    {
        _firstPlayerHealth.PlayerDeath += target => _isFirstPlayerAlive = false;
        _secondPlayerHealth.PlayerDeath += target => _isSecondPlayerAlive = false;

        _firstPlayerRespawnSystem.RespawnAction += () => _isFirstPlayerAlive = true;
        _secondPlayerRespawnSystem.RespawnAction += () => _isSecondPlayerAlive = true;
    }
    
    private void Update()
    {
        if ((_isFirstPlayerAlive && _isSecondPlayerAlive) ||
            (_isFirstPlayerAlive == false && _isSecondPlayerAlive == false))
        {
            return;
        }

        _target = _isFirstPlayerAlive ? firstPlayerTransform : secondPlayerTransform;
        Vector3 targetPos = new Vector3(_target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp (transform.position, targetPos, speed * Time.deltaTime);
    }
}