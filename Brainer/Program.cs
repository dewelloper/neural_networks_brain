using System;

namespace Brainer
{
    public class Program
    {

        public static String quantizeResult(float result)
        {
            return Math.Round(result) + " (" + (float)result + ")";
        }

        public static void Main(String[] args)
        {
            NeuralNetwork NeuralNetwork = new NeuralNetwork(5E-3f, 1.0f, 0.1f);
            SigmoidActivation sa = new SigmoidActivation();
            DummyActivation da = new DummyActivation();
            float BIAS = -1f;

            Neuron input1 = new Neuron(da, 0);
            Neuron input2 = new Neuron(da, 0);
            Neuron input3 = new Neuron(da, 0); // BIAS.
            Neuron hidden1 = new Neuron(sa, 3);
            Neuron hidden2 = new Neuron(sa, 3);
            Neuron hidden3 = new Neuron(da, 3); // Dummy neuron.
            Neuron output1 = new Neuron(sa, 3);

            NeuralNetwork.addInputNeuron(input1);
            NeuralNetwork.addInputNeuron(input2);
            NeuralNetwork.addInputNeuron(input3);

            NeuralNetwork.addHiddeNeuralNetworkeuron(hidden1);
            NeuralNetwork.addHiddeNeuralNetworkeuron(hidden2);
            NeuralNetwork.addHiddeNeuralNetworkeuron(hidden3);

            NeuralNetwork.addOutNeuron(output1);

            hidden3.setDummy(true);
            input3.setOutput(BIAS);

            Console.WriteLine("All inputs & weights has been set.");
            Console.WriteLine("Training has been started.");

            int iteration = 0;
            do
            {
                iteration++;

                input1.setOutput(0);
                input2.setOutput(0);

                output1.desired = 0;

                NeuralNetwork.train();

                input1.setOutput(0);
                input2.setOutput(1);

                output1.desired = 1;

                NeuralNetwork.train();

                input1.setOutput(1);
                input2.setOutput(0);

                output1.desired = 1;

                NeuralNetwork.train();

                input1.setOutput(1);
                input2.setOutput(1);

                output1.desired = 0;

                NeuralNetwork.train();

                if (iteration % 50000 == 0)
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Current iteration:" + iteration);
                    Console.WriteLine("Current error:" + NeuralNetwork.getE());
                    Console.WriteLine("-------------------------------");
                }
            } while (!NeuralNetwork.learnt() && iteration < 3000000);

            Console.WriteLine("Training has been completed.");
            Console.WriteLine("Total iteration: " + iteration + ", accepted error: " + NeuralNetwork.getE());
            Console.WriteLine("Test cases are in progress...");

            input1.setOutput(1);
            input2.setOutput(0);

            NeuralNetwork.test();

            Console.WriteLine("1 XOR 0 = " + quantizeResult(NeuralNetwork.getOutput(0).getOutput()));

            input1.setOutput(1);
            input2.setOutput(1);

            NeuralNetwork.test();

            Console.WriteLine("1 XOR 1 = " + quantizeResult(NeuralNetwork.getOutput(0).getOutput()));

            input1.setOutput(0);
            input2.setOutput(1);

            NeuralNetwork.test();

            Console.WriteLine("0 XOR 1 = " + quantizeResult(NeuralNetwork.getOutput(0).getOutput()));

            input1.setOutput(0);
            input2.setOutput(0);

            NeuralNetwork.test();

            Console.WriteLine("0 XOR 0 = " + quantizeResult(NeuralNetwork.getOutput(0).getOutput()));
        }
    }
}
