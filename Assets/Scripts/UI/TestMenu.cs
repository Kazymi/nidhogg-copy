using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{
    [SerializeField] private Button firearmsButton;
    [SerializeField] private Button withoutWeaponButton;
    [SerializeField] private Button stunStartButton;
    [SerializeField] private Button stunOffButton;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;

    private void Awake()
    {
        firearmsButton.onClick.AddListener(playerAnimatorController.PlayerTakeWeapon);
        withoutWeaponButton.onClick.AddListener(playerAnimatorController.PlayerWithoutWeapon);
        stunStartButton.onClick.AddListener(playerAnimatorController.Stun);
        stunOffButton.onClick.AddListener(playerAnimatorController.StunOff);
    }
}
