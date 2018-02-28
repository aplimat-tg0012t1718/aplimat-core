using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_core.models
{
    public class Movable
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Acceleration;
        public float Mass = 1;

        public void ApplyForce(Vector3 force)
        {
            // F = MA
            // A = F/M
            this.Acceleration += (force / Mass); //force accumulation
        }
    }
}
