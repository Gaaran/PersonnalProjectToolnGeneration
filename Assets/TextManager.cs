using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    
    static Text[] texts;
	// Use this for initialization
	void Start () {
        texts = new Text[GetComponentsInChildren<Text>().Length];
        texts = GetComponentsInChildren<Text>();
    }

    static public void SetTexts(string objName, string strength, string dex, string intel)
    {
        texts[0].text = "Name : " + objName;
        texts[1].text = "Strength : " + strength;
        texts[2].text = "Dex : " + dex;
        texts[3].text = "Intel : " + intel;
    }
}
