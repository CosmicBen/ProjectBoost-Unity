using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.isEditor)
            {
                Debug.Log("Cannot quit in editor.");
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
