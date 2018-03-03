using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_labs.Models
{
    public class Movable
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Acceleration;
        public float Mass = 1;

        public Movable()
        {
            this.Position = new Vector3();
            this.Velocity = new Vector3();
            this.Acceleration = new Vector3();
        }

        public void ApplyForce(Vector3 force)
        {
            //f = MA
            //A = F/M
            this.Acceleration += (force / Mass);
        }

    }
}
