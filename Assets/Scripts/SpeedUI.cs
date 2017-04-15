using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedUI : MonoBehaviour {
    public int kph;
    
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        var kph1 = GameObject.Find("Player").GetComponent<CharacterController>().velocity.magnitude * 3.6;
        kph = (int)kph1;
        TextMeshProUGUI text = this.GetComponent<TextMeshProUGUI>();
        text.text = "SPEED: " + kph;
        GameObject.Find("SpeedSlider").GetComponent<Slider>().value = kph;

    }

    
}
