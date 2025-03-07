using System.Collections;
using System.Collections.Generic;
using QuickStart;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGamesList : MonoBehaviour
{

    public void GoScene()
    {
        SceneManager.LoadScene("GamesList");
    }
}
