
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Zenject;

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float restartLevelTime;

        [Inject]
        private void Construct(IPlayerHealth playerHealth)
        {
            playerHealth.PlayerDeath+= (DamageTarget target) => { StartCoroutine(RestartScene());};
        }

        IEnumerator RestartScene()
        {
            yield return new WaitForSeconds(restartLevelTime);
            SceneManager.LoadScene(0);
        }
    }
