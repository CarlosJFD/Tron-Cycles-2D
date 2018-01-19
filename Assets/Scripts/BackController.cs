using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackController : MonoBehaviour {

    public bool exit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (exit == true)
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }

        }
    }
}
