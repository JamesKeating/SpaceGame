using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WipeVisor : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown("tab") || this.GetComponent<Animation>().IsPlaying("wipe")) {
            if (!this.GetComponent<Animation>().IsPlaying("wipe"))
                this.GetComponent<Animation>().Play();

            foreach (Image splat in GameObject.Find("Visor").transform.GetComponentsInChildren<Image>()) {
                if (splat.name != "Visor")
                {
                    Color curColor = splat.color;
                    float alphaDiff = Mathf.Abs(curColor.a);

                    if (alphaDiff > 0.3f)
                    {
                        curColor.a = Mathf.Lerp(curColor.a, 0, .5f * Time.deltaTime);
                        splat.color = curColor;
                    }

                    else if (alphaDiff > 0.0001f)
                    {
                        curColor.a = Mathf.Lerp(curColor.a, 0, 1.75f*Time.deltaTime);
                        splat.color = curColor;
                    }
                }
            }
        }
    }
}
