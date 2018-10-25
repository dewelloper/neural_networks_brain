using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Brainer
{
    public class NeuralNetwork
    {

        private IList<Neuron> inputLayer;
        private IList<Neuron> hiddenLayer;
        private IList<Neuron> outputLayer;
        private float E; //5E-3f
        private float e;
        private float lambda; //1.0f
        private float nu; //0.1f

        public NeuralNetwork(float E, float lambda, float nu)
        {
            inputLayer = new List<Neuron>();
            hiddenLayer = new List<Neuron>();
            outputLayer = new List<Neuron>();
            this.E = E;
            e = 0;
            this.lambda = lambda;
            this.nu = nu;
        }

        public void addInputNeuron(Neuron neuron)
        {
            inputLayer.Add(neuron);
        }

        public void addHiddeNeuralNetworkeuron(Neuron neuron)
        {
            hiddenLayer.Add(neuron);
        }

        public void addOutNeuron(Neuron neuron)
        {
            outputLayer.Add(neuron);
        }

        public Neuron getOutput(int index)
        {
            return outputLayer[index];
        }

        public void train()
        {
            feedForward();
            calculateError();
            backPropagation();
            //Console.WriteLine("Error value: " + e);
        }

        public void test()
        {
            feedForward();
        }

        protected void feedForward()
        {
            foreach (Neuron nH in hiddenLayer)
            {
                float inputs = 0;
                foreach (Neuron nI in inputLayer)
                {
                    int i = inputLayer.IndexOf(nI);
                    inputs += nI.getOutput() * nH.getWeight(i);
                }
                nH.setInput(inputs);
                nH.activate(lambda);
            }

            foreach (Neuron nO in outputLayer)
            {
                float inputs = 0;
                foreach (Neuron nH in hiddenLayer)
                {
                    int i = hiddenLayer.IndexOf(nH);
                    inputs += nH.getOutput() * nO.getWeight(i);
                }
                nO.setInput(inputs);
                nO.activate(lambda);
            }
        }

        protected void calculateError()
        {
            e = 0;
            foreach (Neuron nO in outputLayer)
            {
                nO.error = nO.desired - nO.getOutput();
                e += (float)Math.Pow(nO.error, 2);
            }

            e = (float)Math.Sqrt(e);
        }

        protected void backPropagation()
        {
            foreach (Neuron nO in outputLayer)
            {
                nO.calculateDelta(lambda, nO.error);
                foreach (Neuron nH in hiddenLayer)
                {
                    int i = hiddenLayer.IndexOf(nH);
                    nH.calculateDelta(lambda, nO.getDelta() * nO.getWeight(i));
                    float diff = nu * nO.getDelta() * nH.getOutput();
                    nO.setWeight(i, nO.getWeight(i) + diff);
                }
            }

            foreach (Neuron nH in hiddenLayer)
            {
                foreach (Neuron nI in inputLayer)
                {
                    int i = inputLayer.IndexOf(nI);
                    float diff = nu * nH.getDelta() * nI.getOutput();
                    nH.setWeight(i, nH.getWeight(i) + diff);
                }
            }
        }

        public float getE()
        {
            return e;
        }

        public bool learnt()
        {
            return (e < E);
        }
    }
}
