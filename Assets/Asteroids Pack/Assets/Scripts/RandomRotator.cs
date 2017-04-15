using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;
    Vector3 speed;
    void Start()
    {
        speed = Random.insideUnitSphere * tumble;
        
    }
    void Update(){
        this.gameObject.transform.Rotate(speed);
    }
}