using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour, IInventory
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Shield shield;
    
    public bool IsShieldActivated { get; private set; }
    public InputAction ShieldCrash { get; set; } = new InputAction();
    
    public void OpenShield()
    {
        weapon.gameObject.SetActive(false);
        IsShieldActivated = true;
        shield.gameObject.SetActive(true);
    }

    public void CloseShield()
    {
        IsShieldActivated = false;
        weapon.gameObject.SetActive(true);
        shield.gameObject.SetActive(false);
    }

    private void ActivateWeapon(bool isActivated)
    {
        weapon.gameObject.SetActive(isActivated);
    }
    
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

}
