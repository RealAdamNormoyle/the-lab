using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronBody : MonoBehaviour
{

    public DynamicNeuralNet.Neuron neuronCell = new DynamicNeuralNet.Neuron();
    Renderer renderer;

    // Start is called before the first frame update
    public void Awake()
    {
        neuronCell.positionRequest = GetPosition;
        neuronCell.onFireEvent = OnFire;
        neuronCell.onRemovedConnectionEvent = LostConnection;
        neuronCell.onNewConnectionEvent = NewConnection;
        neuronCell.onRecievedEvent = OnRecievedTransmition;

        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.gray;
        DynamicNeuralNet.DynamicNeuralNet.AddNeuron(neuronCell);
    }


    public void OnRecievedTransmition()
    {
        Debug.Log("Transmition");

    }

    public void LostConnection()
    {
        Debug.Log("LostConnection");

    }

    public void NewConnection()
    {
        Debug.Log("NewConnection");
    }

    public void OnFire()
    {
        Debug.Log("OnFIre");
        renderer.material.color = Color.white;
        System.Threading.Thread.Sleep(1000);
        renderer.material.color = Color.gray;

    }


    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        foreach (var item in neuronCell.axionConnections)
        {
            if (neuronCell.IsFired)
            {
                Debug.DrawLine(transform.position, item.neuron.positionRequest(), Color.white, 1f);
            }
            else
            {
                Debug.DrawLine(transform.position, item.neuron.positionRequest(),Color.red,1f);
            }
        }

        neuronCell.Decay(Time.deltaTime);
    }
}
