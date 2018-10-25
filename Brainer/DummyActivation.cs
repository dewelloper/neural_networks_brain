using System;
using System.Collections.Generic;
using System.Text;

namespace Brainer
{
    public class DummyActivation : IActivation
    {
        public float activate(float input)
        {
            return input;
        }

        public float derivative(float input)
        {
            return input;
        }
    }
}
