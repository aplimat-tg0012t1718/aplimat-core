using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplimat_labs.Models
{
    public class CubeMesh : Movable
    {
        public CubeMesh()
        {
            this.Position = new Vector3();
        }

        public CubeMesh(Vector3 initPos)
        {
            this.Position = initPos;
        }
        public CubeMesh(float x, float y, float z)
        {
            this.Position = new Vector3(x, y, z);
        }

        public void Draw(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_TRIANGLE_STRIP);




            //frontface
            gl.Vertex(this.Position.x - 0.5f, this.Position.y + 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x - 0.5f, this.Position.y - 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x + 0.5f, this.Position.y + 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x + 0.5f, this.Position.y - 0.5f, this.Position.z + 0.5f);



            //rightface
            gl.Vertex(this.Position.x + 0.5f, this.Position.y + 0.5f, this.Position.z - 0.5f);
            gl.Vertex(this.Position.x + 0.5f, this.Position.y - 0.5f, this.Position.z - 0.5f);

            //backface
            gl.Vertex(this.Position.x - 0.5f, this.Position.y + 0.5f, this.Position.z - 0.5f);
            gl.Vertex(this.Position.x - 0.5f, this.Position.y - 0.5f, this.Position.z - 0.5f);

            //leftface
            gl.Vertex(this.Position.x - 0.5f, this.Position.y + 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x - 0.5f, this.Position.y - 0.5f, this.Position.z + 0.5f);

            //topface
            gl.Begin(OpenGL.GL_TRIANGLE_STRIP);
            gl.Vertex(this.Position.x - 0.5f, this.Position.y + 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x + 0.5f, this.Position.y + 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x - 0.5f, this.Position.y + 0.5f, this.Position.z - 0.5f);
            gl.Vertex(this.Position.x + 0.5f, this.Position.y + 0.5f, this.Position.z - 0.5f);

            gl.End();

            //bottomface
            gl.Begin(OpenGL.GL_TRIANGLE_STRIP);
            gl.Vertex(this.Position.x - 0.5f, this.Position.y - 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x + 0.5f, this.Position.y - 0.5f, this.Position.z + 0.5f);
            gl.Vertex(this.Position.x - 0.5f, this.Position.y - 0.5f, this.Position.z - 0.5f);
            gl.Vertex(this.Position.x + 0.5f, this.Position.y - 0.5f, this.Position.z - 0.5f);
            //gl.Color(0, 1, 0);
            gl.End();

            UpdateMotion();
        }

        private void UpdateMotion()
        {
            this.Velocity += this.Acceleration;
            this.Position += this.Velocity;
            this.Acceleration *= 1;
            //this.Position -= Velocity;
        }
    }
}