using System;
using System.Collections.Generic;
using System.Text;

namespace Brainer
{
    public interface IActivation
    {
        float activate(float input);
        float derivative(float input);
    }
}
