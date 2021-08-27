using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour, IInventory
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Shield shield;

    public bool IsShieldActivated { get; private set; }
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
}
