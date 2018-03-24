using aplimat_core.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_core.models
{
    class Attractor : CubeMesh
    {
        public float G = 1.0f;

        public Vector3 CalculateAttraction(Movable moveable)
        {
            var force = this.Position - moveable.Position;
            var distance = force.GetLength();

            distance = Utils.ConstrainFloat(distance, 5, 25);
            force.Normalize();

            var strength = (this.G * this.Mass * moveable.Mass) / (distance * distance);
            force *= strength;

            return force;
        }
    }
}
