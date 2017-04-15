using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroScore : MonoBehaviour
{
    public int score;

    // Use this for initialization
    void Start()
    {
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        TextMeshProUGUI text = this.GetComponent<TextMeshProUGUI>();
        text.text = "HERO SCORE: " + score;
        GameObject.Find("HeroPointSlider").GetComponent<Slider>().value = score;

    }


}
