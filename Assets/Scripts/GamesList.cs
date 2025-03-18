using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuickStart
{
    public class GamesList : MonoBehaviour
    {
        public Canvas canvas;
        
        public void DisableCanvas()
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
