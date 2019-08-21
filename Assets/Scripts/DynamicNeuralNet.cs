using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DynamicNeuralNet
{
    [Serializable]
    public class DynamicNeuralNet
    {
        public static List<Neuron> neurons = new List<Neuron>();
        public int totalNeurons { get { return neurons.Count; }}
        public void SetupBrain(int n)
        {
            for (int i = 0; i < n; i++)
            {
                neurons.Add(new Neuron());
            }
        }

        public static void AddNeuron(Neuron n)
        {
            neurons.Add(n);
        }

        public static Neuron GetRandomNeuron()
        {
            return neurons[new Random().Next(neurons.Count)];
        }

        public static bool StimulateRandomNeuron()
        {
            if(neurons.Count > 0)
            {
                Task.Factory.StartNew(() => { DynamicNeuralNet.neurons[new Random().Next(neurons.Count)].Fire(); });
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [Serializable]
    public class AxionConnection
    {

        public AxionConnection(Neuron n)
        {
            neuron = n;
        }

        public Neuron neuron;
        public float lastActivePotential;
    }

    [Serializable]
    public class Neuron
    {
        const float maxPotential = 1;
        int maxAxions = 5;
        const float actionPotentialCooldown = 0.1f;
        const float maxTimeInactiveConnection = 100;

        const float actionPotentialDecay = 1;
        const float transmitionRate = 0.4f;

        float currentPotential = 0;
        bool inhibitory;
        public bool IsFired { get { return currentPotential > 1; } }

        public List<AxionConnection> axionConnections = new List<AxionConnection>();


        /// Callbacks
        /// 
        public Action onFireEvent;
        public Action onNewConnectionEvent;
        public Action onRemovedConnectionEvent;
        public Action onRecievedEvent;

        public Func<UnityEngine.Vector3> positionRequest;



        public Neuron()
        {
            maxAxions = new Random().Next(10);
            //Task.Factory.StartNew(Decay);

        }

        public bool RecieveTransmition(float value, bool _inhibitory)
        {

            onRecievedEvent();

            if (_inhibitory)
            {
                currentPotential -= value;
            }
            else
            {
                currentPotential += value;
                if (currentPotential >= maxPotential)
                {
                    Fire();
                    return true;
                }
            }

            return false;
        }

        public void Decay(float t)
        {
            currentPotential -= actionPotentialDecay * 0.1f;
            currentPotential = Math.Max(currentPotential, 0);
            if (!inhibitory)
            {
                foreach (var item in axionConnections)
                {
                    item.lastActivePotential += t;

                    if (item.lastActivePotential >= maxTimeInactiveConnection)
                    {
                        axionConnections.Remove(item);
                        onRemovedConnectionEvent();
                        break;
                    }
                }
            }

            if (axionConnections.Count < maxAxions && (maxPotential - currentPotential) < 0.2f)
            {

                Neuron n = DynamicNeuralNet.GetRandomNeuron();

                if (n != this)
                {
                    //Look for other connections
                    if (positionRequest != null)
                    {
                        if (UnityEngine.Vector3.Distance(positionRequest(), n.positionRequest()) < 50)
                        {

                            if (!n.CheckConnections(this))
                            {
                                axionConnections.Add(new AxionConnection(n));
                                onNewConnectionEvent();
                            }

                        }
                    }
                    else
                    {
                        onNewConnectionEvent();
                        axionConnections.Add(new AxionConnection(n));
                    }

                }
            }

        }

        private bool CheckConnections(Neuron neuron)
        {
            foreach (var item in axionConnections)
            {
                if (item.neuron == neuron)
                    return true;
            }

            return false;
        }

        public void Fire()
        {
            currentPotential = 1.1f;

            foreach (var item in axionConnections)
            {
                if (item.neuron.RecieveTransmition(transmitionRate, inhibitory))
                {
                    item.lastActivePotential = 0;
                }

                
            }

            onFireEvent();
        }

    }
}
