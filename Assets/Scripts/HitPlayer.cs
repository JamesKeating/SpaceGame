using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPlayer : MonoBehaviour {

    public Sprite hud1, hud2, hud3;
    private Sprite[] hudSprites;
    private int damageCount, paintCount;
    private float lastDamaged, lastPainted;
    
    Canvas HUD;

    void OnParticleCollision()
    {
        if (Time.time - lastPainted > .15 && paintCount >= 0) {

            Color temp = GameObject.Find("Visor").transform.GetChild(paintCount).GetComponent<Image>().color;
            temp.a = 1;
            GameObject.Find("Visor").transform.GetChild(paintCount).GetComponent<Image>().color = temp;
            paintCount--;
            lastPainted = Time.time;

        }

     

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        print(hit.gameObject.tag);
        if (Time.time - lastDamaged > 2)
        {
            if (damageCount == 3)
            {
                //kill player
                damageCount = 0;
            }

            HUD.transform.FindChild("Helmet").FindChild("Visor").GetComponent<Image>().sprite = hudSprites[damageCount];
            damageCount++;
            lastDamaged = Time.time;

        }

    }

    // Use this for initialization
    void Start () {
        hudSprites = new Sprite[] { hud1, hud2, hud3};
        lastDamaged = Time.time;
        lastPainted = Time.time;
        damageCount = 0;
        paintCount = 5;
        HUD = GameObject.Find("HUD").GetComponent<Canvas>();

        for (int i = 0; i < GameObject.Find("Visor").transform.childCount; i++)
        {
            Color temp = GameObject.Find("Visor").transform.GetChild(i).GetComponent<Image>().color;
            temp.a = 0;
            GameObject.Find("Visor").transform.GetChild(i).GetComponent<Image>().color = temp;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("tab")) {
            paintCount = 5; 
        }


    }
}
