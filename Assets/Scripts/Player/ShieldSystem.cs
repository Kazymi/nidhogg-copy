using System.Collections;
using UnityEngine;
using Zenject;

public class ShieldSystem : MonoBehaviour, IShieldSystem
{
    [SerializeField] private Shield shield;
    [SerializeField] private int amountShields;

    private bool _isShieldUnlock = true;
    private float _currentAmountShields;
    public bool IsShieldActivated { get; private set; }
    public InputAction ShieldCrash { get; set; } = new InputAction();

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
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
        if (_isShieldUnlock == false || _currentAmountShields >= amountShields)
        {
            return;
        }

        IsShieldActivated = true;
        shield.gameObject.SetActive(true);
    }

    public void CloseShield()
    {
        IsShieldActivated = false;
        shield.gameObject.SetActive(false);
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