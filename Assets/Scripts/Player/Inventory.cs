using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour, IInventory
{
    [SerializeField] private InventoryConfig inventoryConfig;

    private bool _isShieldUnlock = true;
    private float _currentAmountShields;
    public bool IsShieldActivated { get; private set; }
    public InputAction ShieldCrash { get; set; } = new InputAction();

    [Inject]
    private void Construct(IInputHandler inputHandler, IPlayerMovement playerMovement)
    {
        playerMovement.DefaultMovement += ActivateWeapon;
        inputHandler.ShieldButtonDownAction.Action += () =>
        {
            if (IsShieldActivated)
            {
                CloseShield();
            }
            else
            {
                OpenShield();
            }

        };
    }
    
    private void Awake()
    {
        ShieldCrash.Action += ShieldCrashed;
    }

    public void OpenShield()
    {
        if (_isShieldUnlock == false || _currentAmountShields >= inventoryConfig.AmountShields)
        {
            return;
        }
        IsShieldActivated = true;
        inventoryConfig.Shield.gameObject.SetActive(true);
    }

    public void CloseShield()
    {
        IsShieldActivated = false;
        inventoryConfig.Shield.gameObject.SetActive(false);
    }

    private void ActivateWeapon(bool isActivated)
    {
        inventoryConfig.Weapon.gameObject.SetActive(isActivated);
    }

    private IEnumerator ShieldCooldown()
    {
        _isShieldUnlock = false;
        yield return new WaitForSeconds(3f);
        _isShieldUnlock = true;
    }

    private void ShieldCrashed()
    {
        _currentAmountShields++;
        CloseShield();
        StartCoroutine(ShieldCooldown());
    }

}
