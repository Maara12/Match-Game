
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaarasMatchGame
{

    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] string level1_SceneName;
        public void OnPlayButtonClicked()
        {
            Invoke(nameof(LoadScene1),1f);
        }

        private void LoadScene1()
        {
            SceneManager.LoadScene(level1_SceneName);
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }


}

