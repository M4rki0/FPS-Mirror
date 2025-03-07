using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickStart
{
    public class SceneReference : MonoBehaviour
    {
        public SceneScript sceneScript;

        private void Start()
        {
            sceneScript = GetComponent<SceneScript>();
        }
    }
}
