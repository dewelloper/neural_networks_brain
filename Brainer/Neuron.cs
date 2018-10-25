﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Brainer
{
    public class Neuron
    {

        private float[] weights;
        private float input;
        private float output;
        private IActivation activation;
        private float delta;
        public float desired;
        public float error;
        private bool dummy;
        private static Random random = new Random();

        public Neuron(IActivation activation, int weights)
        {
            this.weights = new float[weights];
            this.activation = activation;
            delta = 0;
            dummy = false;
            error = desired = 0;

            for (int i = 0; i < weights; i++)
            {
                this.weights[i] = getRandomWeight();
            }
        }

        protected float getRandomWeight()
        {
            float r = random.Next();
            float range = 2.4f / weights.Length;

            return (r * 2 * range) - range;
        }

        public float getWeight(int index)
        {
            return weights[index];
        }

        public void setWeight(int index, float weight)
        {
            if (!dummy)
            {
                weights[index] = weight;
            }
        }

        public float getInput()
        {
            return input;
        }

        public void setInput(float input)
        {
            this.input = input;
        }

        public float getOutput()
        {
            return output;
        }

        public void setOutput(float output)
        {
            this.output = output;
        }

        public float getDelta()
        {
            return delta;
        }

        public bool isDummy()
        {
            return dummy;
        }

        public void setDummy(bool dummy)
        {
            this.dummy = dummy;
        }

        public void activate(float lambda)
        {
            float value = input;
            if (!dummy)
            {
                value *= -lambda;
            }
            output = activation.activate(value);
        }

        public void calculateDelta(float lambda, float error)
        {
            if (!dummy)
            {
                delta = error * lambda * activation.derivative(output);
            }
        }
    }
}
