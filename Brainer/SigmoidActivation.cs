using System;
using System.Collections.Generic;
using System.Text;

namespace Brainer
{
    class SigmoidActivation : IActivation
    {
        float IActivation.activate(float input)
        {
            return (float)(1 / (1 + Math.Exp(input)));
        }
        

        float IActivation.derivative(float input)
        {
            return input * (1 - input);
        }
    }
}
