using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{
    [SerializeField] private Button firearmsButton;
    [SerializeField] private Button withoutWeaponButton;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;

    private void Awake()
    {
        firearmsButton.onClick.AddListener(playerAnimatorController.PlayerTakeWeapon);
        withoutWeaponButton.onClick.AddListener(playerAnimatorController.PlayerWithoutWeapon);
    }
}
