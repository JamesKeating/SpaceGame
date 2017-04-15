using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    
    private ParticleSystem.EmissionModule myEmissionModule;
    private float startTime = 0;
    // Use this for initialization
    void Start()
    {
        myEmissionModule = this.GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Spray") && startTime == 0)
        {
            this.GetComponent<ParticleSystem>().startSpeed = this.transform.root.GetComponent<CharacterController>().velocity.magnitude + 20;
            startTime = Time.time;
            myEmissionModule.enabled = true;

        }
        else if (startTime != 0 && Time.time - startTime > .25) {

            myEmissionModule.enabled = false;
            startTime = 0;

        }
        
    }
}
