using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScore : MonoBehaviour {
    double lastPainted;
	// Use this for initialization
	void Start () {
        lastPainted = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision()
    {

        if (Time.time - lastPainted > .05)
        {
            lastPainted = Time.time;
            GameObject.Find("HeroPoints").GetComponent<HeroScore>().score += 10;
        }
        
    }


}
