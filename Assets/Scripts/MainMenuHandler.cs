using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaarasMatchGame
{

    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] string level1_SceneName;
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(level1_SceneName);
        }
        
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }


}

