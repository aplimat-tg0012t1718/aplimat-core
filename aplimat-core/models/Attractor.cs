using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aplimat_core.utilities;

namespace aplimat_core.models
{
    public class Attractor : CubeMesh
    {
        public float G = 1f;

        public Vector3 CalculateAttraction(Movable movable)
        {
            var force = this.Position - movable.Position;
            var distance = force.GetLength();

            distance = Utils.ConstrainFloat(distance, 5, 25);
            force.Normalize();

            var strength = (this.G * this.Mass * movable.Mass) / (distance * distance);
            force *= strength;

            return force;
        }
    }
}
