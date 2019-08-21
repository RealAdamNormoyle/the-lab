using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float Mass;
    public Vector3 Velocity;

    public void Init()
    {
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.position = Random.insideUnitSphere * 100;

        Mass = Random.Range(0.1f, 1f);
        gameObject.transform.localScale = new Vector3(Mass, Mass, Mass);
        //gameObject.AddComponent<SphereCollider>();
    }

    public void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
