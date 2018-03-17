using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_core.models
{
    public class CubeMesh : Movable
    {
        private utilities.Randomizer cubeS = new utilities.Randomizer(1, 3);

        public Vector3 Scale = new Vector3(1, 1, 1);

        public CubeMesh()
        {
            this.Position = new Vector3();
            this.Velocity = new Vector3();
            this.Acceleration = new Vector3();

            //this.Scale.y /= 2;
            //this.Scale = new Vector3(cubeS.GenerateInt(), cubeS.GenerateInt(), cubeS.GenerateInt());
        }
        public CubeMesh(Vector3 initPos)
        {
            this.Position = initPos;
            this.Velocity = new Vector3();
            this.Acceleration = new Vector3();

            //this.Scale.y /= 2;
            //this.Scale = new Vector3(cubeS.GenerateInt(), cubeS.GenerateInt(), cubeS.GenerateInt());
        }

        public CubeMesh(float x, float y, float z)
        {
            this.Position = new Vector3();
            this.Velocity = new Vector3();
            this.Acceleration = new Vector3();
            this.Position.x = x;
            this.Position.y = y;
            this.Position.z = z;

            //this.Scale.y /= 2;
            //this.Scale = new Vector3(cubeS.GenerateInt(), cubeS.GenerateInt(), cubeS.GenerateInt());
        }

        public void Draw(OpenGL gl)
        {
            
            gl.Begin(OpenGL.GL_TRIANGLE_STRIP);
            //Front face
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y + this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y - this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y + this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y - this.Scale.y, this.Position.z + this.Scale.z);
            //Right face
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y + this.Scale.y, this.Position.z - this.Scale.z);
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y - this.Scale.y, this.Position.z - this.Scale.z);
            //Back face
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y + this.Scale.y, this.Position.z - this.Scale.z);
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y - this.Scale.y, this.Position.z - this.Scale.z);
            //Left face
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y + this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y - this.Scale.y, this.Position.z + this.Scale.z);
            gl.End();
            gl.Begin(OpenGL.GL_TRIANGLE_STRIP);
            //Top face
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y + this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y + this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y + this.Scale.y, this.Position.z - this.Scale.z);
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y + this.Scale.y, this.Position.z - this.Scale.z);
            gl.End();
            gl.Begin(OpenGL.GL_TRIANGLE_STRIP);
            //Bottom face
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y - this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y - this.Scale.y, this.Position.z + this.Scale.z);
            gl.Vertex(this.Position.x - this.Scale.x, this.Position.y - this.Scale.y, this.Position.z - this.Scale.z);
            gl.Vertex(this.Position.x + this.Scale.x, this.Position.y - this.Scale.y, this.Position.z - this.Scale.z);
            gl.End();

            UpdateMotion();
        }

        private void UpdateMotion()
        {
            this.Velocity += this.Acceleration;
            this.Position += this.Velocity;
            this.Acceleration *= 0;
        }
    }
}
