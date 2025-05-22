using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuickStart
{
    public class Menu : MonoBehaviour
    {
        public void GoToMatchMaking()
        {
            SceneManager.LoadScene("MatchMaking");
        }

        public void GoToControls()
        {
            SceneManager.LoadScene("Controls");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
