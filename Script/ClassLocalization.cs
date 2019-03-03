using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassLocalization : MonoBehaviour {

    public Sprite Rus,Eng;
    public string rus, eng;
   // public Font rusF, engF;
    public bool Text = false;
	// Use this for initialization
	void Start () {
        updateText();
	}

     public void updateText()
    {
        if (Text)
        {
            // GetComponent<Text>().font = Application.systemLanguage == SystemLanguage.Russian ? rusF : engF;
            GetComponent<Text>().text = Application.systemLanguage == SystemLanguage.Russian ? rus : eng;
        }
        else
            GetComponent<Image>().sprite = Application.systemLanguage == SystemLanguage.Russian ? Rus : Eng;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
