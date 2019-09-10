using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {
    int number;

    Image image;


    static List<Action<int>> callbacks = new List<Action<int>>();
    public static int chosenLevel = -1;
    
    // Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        callbacks.Add(Callback);
        number = Int32.Parse(Regex.Match(gameObject.name, @"\d+").Value);
        transform.GetChild(0).GetComponent<Text>().text = number.ToString();
        GetComponent<Button>().onClick.AddListener(Light);
    }
	
	
	void Light () {
		for(int i = 0; i < callbacks.Count; i++) {
            callbacks[i](number);
        }
	}

    void Callback(int value) {
        if (value == number) {
            image.color = Color.green;
            chosenLevel = value;
        }
        else image.color = Color.white;
    }
}
