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
        public float drag;
        public float x, y;
        public float width, depth;


        public Liquid(float x, float y, float width, float depth, float drag)
        {
            this.x = x;
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

        public bool Contains(Movable moveable)
        {
            var pos = moveable.Position;
            return pos.x < this.x + this.width && pos.y < this.y;
        }

        public Vector3 CalculateDragForce(Movable moveable)
        {
            var speed = moveable.Velocity.GetLength();
            var dragMagnitude = this.drag * speed * speed;

            var dragForce = moveable.Velocity;
            dragForce *= -1;

            dragForce.Normalize();
            dragForce *= dragMagnitude;

            return dragForce;
        }
    }
}
