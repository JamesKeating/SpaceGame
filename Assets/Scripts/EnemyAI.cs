using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform target;
    Vector3 storeTarget;
    Vector3 newTargetPos;
    bool savePos;
    bool overrideTarget;

    Vector3 acceleration;
    Vector3 velocity;
    public float maxSpeed = 5f;
    

    Rigidbody ridgidBody;

    private ParticleSystem.EmissionModule myEmissionModule;
    private float startTime = 0;
    private float lastShot;

    public List<Vector3> EscapeDirections = new List<Vector3>();
	// Use this for initialization
	void Start () {
        lastShot = Time.time;
        myEmissionModule = GameObject.Find("EnemyPaint").GetComponent<ParticleSystem>().emission;
        ridgidBody = GetComponent<Rigidbody>();
		
	}    

	// Update is called once per frame
	void Update () {
        maxSpeed = target.transform.root.GetComponent<CharacterController>().velocity.magnitude;
        if (maxSpeed > 20)
            maxSpeed = 20;
        else if (maxSpeed < 5)
            maxSpeed = 5;

        Debug.DrawLine(transform.position, target.position);

        Vector3 forces = MoveTowardsTarget(target.position - new Vector3(0,1,0));

        acceleration = forces;

        velocity += 2 * acceleration * Time.deltaTime;

        
        if (velocity.magnitude > maxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }

        ridgidBody.velocity = velocity;

        Quaternion desiredRotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 3);

        ObsticleAvoidance(transform.forward, 0);
        shoot(transform.forward, 0);

	}

    Vector3 MoveTowardsTarget(Vector3 target) {
        Vector3 distance = target - transform.position;

            if (distance.magnitude < 5)
                return distance.normalized * -maxSpeed;
        
            else
                return distance.normalized * maxSpeed;
        

    }

    void ObsticleAvoidance(Vector3 direction, float offsetX) {
        RaycastHit[] hit = Rays(direction, offsetX, 4);

        for (int i = 0; i < hit.Length -1; i++) {
            if (hit[i].transform.root.gameObject != this.gameObject && hit[i].transform.root.tag != "Player")
            {
                print(hit[i].transform.root.gameObject);
                FindEscapeDirections(hit[i].collider);
            }
           
        }

        if (EscapeDirections.Count > 0) {
                target.position = getClosest();
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 10) {
            target.position = target.root.position;
            EscapeDirections.Clear();
        }
        
    }

    Vector3 getClosest() {
        Vector3 close = EscapeDirections[0];
        float distance = Vector3.Distance(transform.position, EscapeDirections[0]);

        foreach (Vector3 dir in EscapeDirections) {
            float tempdistance = Vector3.Distance(transform.position, dir);

            if (tempdistance < distance) {
                distance = tempdistance;
                close = dir;
            }
        }
        return close;
    }

    void FindEscapeDirections(Collider col) {
        RaycastHit hitUp;

        if (Physics.Raycast(col.transform.position, col.transform.up, out hitUp, col.bounds.extents.y * 2.5f)) { }

        else {
            Vector3 dir = col.transform.position + new Vector3(0, col.bounds.extents.y * 2.5f);

            if (!EscapeDirections.Contains(dir)) {
                EscapeDirections.Add(dir);
            }
        }

        RaycastHit hitDown;

        if (Physics.Raycast(col.transform.position, -col.transform.up, out hitDown, col.bounds.extents.y * 2.5f)) { }

        else
        {
            Vector3 dir = col.transform.position + new Vector3(0, -col.bounds.extents.y * 2.5f, 0);

            if (!EscapeDirections.Contains(dir))
            {
                EscapeDirections.Add(dir);
            }
        }

        RaycastHit hitRight;

        if (Physics.Raycast(col.transform.position, col.transform.right, out hitRight, col.bounds.extents.x * 2.5f)) { }

        else
        {
            Vector3 dir = col.transform.position + new Vector3(col.bounds.extents.x * 2.5f, 0,0);

            if (!EscapeDirections.Contains(dir))
            {
                EscapeDirections.Add(dir);
            }
        }

        RaycastHit hitLeft;

        if (Physics.Raycast(col.transform.position, -col.transform.right, out hitLeft, col.bounds.extents.x * 2.5f)) { }

        else
        {
            Vector3 dir = col.transform.position + new Vector3( -col.bounds.extents.x * 2.5f, 0,0);

            if (!EscapeDirections.Contains(dir))
            {
                EscapeDirections.Add(dir);
            }
        }
    }

    RaycastHit[] Rays(Vector3 direction, float offsetX, float dist) {
        Ray ray = new Ray(transform.position + new Vector3(offsetX, 0, 0), direction);
        Debug.DrawRay(transform.position + new Vector3(offsetX, 0, 0), direction *5 *maxSpeed, Color.red );

        float distanceToLookAhead = maxSpeed * dist;
        RaycastHit[] hits = Physics.SphereCastAll(ray, 2, distanceToLookAhead);
        return hits;

    }

    void shoot(Vector3 direction, float offsetX){

        RaycastHit[] hit = Rays(direction, offsetX, 100);

        for (int i = 0; i < hit.Length - 1; i++){
            if (hit[i].transform.root.gameObject != this.gameObject)
                
                if (hit[i].transform.root.gameObject.tag == "Player")
                {

                    if (startTime == 0 && Time.time - lastShot > 1)
                    {
                        GameObject.Find("EnemyPaint").GetComponent<ParticleSystem>().startSpeed = GameObject.Find("Enemy").GetComponent<Rigidbody>().velocity.magnitude + 20;
                        startTime = Time.time;
                        myEmissionModule.enabled = true;

                    }
                }
                else
                {
                    break;
                }
        }

        if (startTime != 0 && Time.time - startTime > .25)
        {
            myEmissionModule.enabled = false;
            startTime = 0;
            lastShot = Time.time;

        }
    }
}
