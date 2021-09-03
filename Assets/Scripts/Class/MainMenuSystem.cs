using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem 
{
   public void LoadGame()
   {
      SceneManager.LoadScene(1);
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
