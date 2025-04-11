using System;
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

        private void Start()
        {
            GameManager.Instance.localPlayer.GetComponent<MouseLook>().dontLook = true;
        }

        public void DisableCanvas()
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
