using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button toGameButton;
    [SerializeField] private Button exitGameButton;

    private MainMenuSystem _mainMenuSystem;

    [Inject]
    private void Construct(MainMenuSystem menuSystem)
    {
        _mainMenuSystem = menuSystem;
    }

    private void OnEnable()
    {
        toGameButton.onClick.AddListener(_mainMenuSystem.LoadGame);
        exitGameButton.onClick.AddListener(_mainMenuSystem.QuitGame);
    }

    private void OnDisable()
    {
        toGameButton.onClick.RemoveListener(_mainMenuSystem.LoadGame);
        exitGameButton.onClick.RemoveListener(_mainMenuSystem.QuitGame);
    }
}