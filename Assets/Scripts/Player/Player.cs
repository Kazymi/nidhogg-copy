using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;

    private void Start()
    {
        playerMovement.Initialize(inputHandler);
        playerAnimatorController.Initialize(inputHandler);
    }
}
