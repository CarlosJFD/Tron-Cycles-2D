using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneEndGame : MonoBehaviour {

    public string sceneToLoad;
    public float delay;

    [ContextMenu("Activate Load")]

    public void ActivateLoad()
    {
        Invoke("SceneLoad", delay);
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
