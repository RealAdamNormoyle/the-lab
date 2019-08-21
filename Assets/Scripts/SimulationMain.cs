using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationMain : MonoBehaviour
{
    // Start is called before the first frame update

    public DynamicNeuralNet.DynamicNeuralNet neuralNet = new DynamicNeuralNet.DynamicNeuralNet();
    public GameObject neuronPrefab;
    GameObject neuronContainer;

    int ticks = 0;

    void Start()
    {
        neuronContainer = new GameObject();

        for (int i = 0; i < 2000; i++)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.AddComponent<NeuronBody>();
            obj.transform.position = Random.insideUnitSphere * 100;
            obj.transform.SetParent(neuronContainer.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (ticks < 10000) {
        //Debug.Log(neuralNet.totalNeurons);
            if(!DynamicNeuralNet.DynamicNeuralNet.StimulateRandomNeuron())
                Debug.Log("No active Neurons");

            
            DynamicNeuralNet.DynamicNeuralNet.StimulateRandomNeuron();

            DynamicNeuralNet.DynamicNeuralNet.StimulateRandomNeuron();



            ticks++;
        }
    }
}
