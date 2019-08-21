using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhysicsModel : MonoBehaviour
{

    public List<Particle> particles = new List<Particle>();
    bool isDoneSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GenerateParticles(10000));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDoneSpawning)
            return;
    }

    public IEnumerator GenerateParticles(int n)
    {
        for (int i = 0; i < n; i++)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var particle = obj.AddComponent<Particle>();
            particle.Init();
            particles.Add(particle);
            yield return new WaitForEndOfFrame();


        }

        isDoneSpawning = true;
    }

    public void WeakForce()
    {

    }

    public void StrongForce()
    {

    }

}

[System.Serializable]
public class DataClasses
{



}

[System.Serializable]
public class ParticleBinding
{

}
