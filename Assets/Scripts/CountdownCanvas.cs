using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownCanvas : MonoBehaviour {

    public Text textCountdown;
    int i = 3;

	// Use this for initialization
	void Start () {
        Invoke("Countdown",0);
	}

    void Countdown()
    {
        if (i == 0)
        {
            textCountdown.text = "";
        }else
        {
            textCountdown.text = "" + i;
            i--;
            Invoke("Countdown", 1);
        }
        
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
