using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebris : MonoBehaviour {

    public GameObject debris;
    // Use this for initialization
    void Start () {

        for (int i = 0; i < 100; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-200, 200),
                Random.Range(-200, 200),
                Random.Range(-200, 200));
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(45, 0, 0));
            Instantiate(debris, spawnPosition, spawnRotation);
        }
    }
	
	// Update is called once per frame
	void Update () {
		

	}
}
