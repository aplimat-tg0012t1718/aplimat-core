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

        public void ApplyGravity(float gravity = 0.1f)
        {
            this.Acceleration += (new models.Vector3(0, -gravity * Mass, 0) / Mass);
        }

        public void ApplyFriction(float frictionCoefficient = 0.05f, float normalForce = 1)
        {
            //var frictionCoefficient = 0.05f;
            //var normalForce = 1;
            var frictionMagnitude = frictionCoefficient * normalForce;

            var friction = this.Velocity;
            friction *= -1;
            friction.Normalize();
            friction *= frictionMagnitude;


            this.Acceleration += (friction / Mass);
        }
    }
}
