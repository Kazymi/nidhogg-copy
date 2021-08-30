using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private MainMenuConfiguration menuConfiguration;

   [Inject]
   private void Construct(MainMenuSystem menuSystem)
   {
      menuConfiguration.ToGameButton.onClick.AddListener(menuSystem.LoadGame);
      menuConfiguration.ExitButton.onClick.AddListener(menuSystem.QuitGame);
   }
}
