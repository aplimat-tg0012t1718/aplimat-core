using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_core.models
{
    public class Liquid
    {
        public float drag; // Cdrag
        public float x, y;
        public float width, depth;

        public Liquid(float x, float y, float width, float depth, float drag)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.depth = depth;
            this.drag = drag;
        }

        public void Draw(OpenGL gl, byte r = 28, byte g = 120, byte b = 186)
        {
            gl.Color(r, g, b);
            gl.Begin(OpenGL.GL_POLYGON);
            gl.Vertex(x - width, y, 0);
            gl.Vertex(x + width, y, 0);
            gl.Vertex(x + width, y - depth, 0);
            gl.Vertex(x - width, y - depth, 0);

            gl.End();
        }
        /*
         * 
         * 
         */
        public bool Contains(Movable movable)
        {
            var pos = movable.Position;

            return pos.x > this.x - this.width &&
                pos.x < this.x + this.width &&
                pos.y < this.y;
        }

        /*
         * Calculates the drag force with formula:
         * Fd = v^2 * drag * vdirection * -1
         */

        public Vector3 CalculateDragForce(Movable movable)
        {
            // Magnitude = coefficient of drag * speed suared
            var speed = movable.Velocity.GetLength(); // length of a vector is also a magnitude
            var dragMagnitude = this.drag * speed * speed;

            //Direction is inverse of velocity
            var dragForce = movable.Velocity;
            dragForce *= -1;

            // Scale according to it's magnitude
            dragForce.Normalize();
            dragForce *= dragMagnitude;

            return dragForce;

        }

    }
}
