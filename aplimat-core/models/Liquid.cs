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
         * Checks if the position of a movable
         * is inside the actual liquid
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
            // Magnitude = coefficient of drag * speed squared
            var speed = movable.Velocity.GetLength(); // Length of a vector is also the magnitude
            var dragMagnitude = this.drag * speed * speed;

            // Direction is inverse of velocity
            var dragForce = movable.Velocity;
            dragForce *= -1;

            // Scale according to its magnitude
            dragForce.Normalize();
            dragForce *= dragMagnitude;
            return dragForce;
        }
    }
}
