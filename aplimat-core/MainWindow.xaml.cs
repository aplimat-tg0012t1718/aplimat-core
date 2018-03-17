using aplimat_core.models;
using aplimat_core.utilities;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aplimat_lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CubeMesh> cubes = new List<CubeMesh>();
        private CubeMesh cube1 = new CubeMesh(-60,20,0);
        private CubeMesh cube2 = new CubeMesh(-50, 20, 0);
        private CubeMesh cube3 = new CubeMesh(-40, 20, 0);
        private CubeMesh cube4 = new CubeMesh(-30, 20, 0);
        private CubeMesh cube5 = new CubeMesh(-20, 20, 0);
        private CubeMesh cube6 = new CubeMesh(-10, 20, 0);
        private CubeMesh cube7 = new CubeMesh(0, 20, 0);
        private CubeMesh cube8 = new CubeMesh(10, 20, 0);
        private CubeMesh cube9 = new CubeMesh(20, 20, 0);
        private CubeMesh cube10 = new CubeMesh(30, 20, 0);
        private Liquid ocean = new Liquid(0, 0, 100, 50, .8f);

        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            this.Title = "Water Resistance";
            OpenGL gl = args.OpenGL;

            /// Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            /// Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            ocean.Draw(gl);
            gl.Color(1.0f, 1.0f, 1.0f);

            Vector3 temp = new Vector3(1, 1, 1);

            cube1.Mass = 1f;
            cube1.Scale = temp * 1;
            cube1.Draw(gl);
            cube1.ApplyGravity();
            if (cube1.Position.y <= -40)
            {
                cube1.Position.y = -40;
                cube1.Velocity.y *= -1;
            }
            if (ocean.Contains(cube1))
            {
                var dragForce = ocean.CalculateDragForce(cube1);
                cube1.ApplyForce(dragForce);
            }

            cube2.Mass = 2f;
            cube2.Scale = temp * 1.2f;
            cube2.Draw(gl);
            cube2.ApplyGravity();
            if (cube2.Position.y <= -40)
            {
                cube2.Position.y = -40;
                cube2.Velocity.y *= -1;
            }
            if (ocean.Contains(cube2))
            {
                var dragForce = ocean.CalculateDragForce(cube2);
                cube2.ApplyForce(dragForce);
            }

            cube3.Mass = 3f;
            cube3.Scale = temp * 1.3f;
            cube3.Draw(gl);
            cube3.ApplyGravity();
            if (cube3.Position.y <= -40)
            {
                cube3.Position.y = -40;
                cube3.Velocity.y *= -1;
            }
            if (ocean.Contains(cube3))
            {
                var dragForce = ocean.CalculateDragForce(cube3);
                cube3.ApplyForce(dragForce);
            }

            cube4.Mass = 4f;
            cube4.Scale = temp * 1.4f;
            cube4.Draw(gl);
            cube4.ApplyGravity();
            if (cube4.Position.y <= -40)
            {
                cube4.Position.y = -40;
                cube4.Velocity.y *= -1;
            }
            if (ocean.Contains(cube4))
            {
                var dragForce = ocean.CalculateDragForce(cube4);
                cube4.ApplyForce(dragForce);
            }

            cube5.Mass = 5;
            cube5.Scale = temp * 1.5f;
            cube5.Draw(gl);
            cube5.ApplyGravity();
            if (cube5.Position.y <= -40)
            {
                cube5.Position.y = -40;
                cube5.Velocity.y *= -1;
            }
            if (ocean.Contains(cube5))
            {
                var dragForce = ocean.CalculateDragForce(cube5);
                cube5.ApplyForce(dragForce);
            }

            cube6.Mass = 6;
            cube6.Scale = temp * 1.6f;
            cube6.Draw(gl);
            cube6.ApplyGravity();
            if (cube6.Position.y <= -40)
            {
                cube6.Position.y = -40;
                cube6.Velocity.y *= -1;
            }
            if (ocean.Contains(cube6))
            {
                var dragForce = ocean.CalculateDragForce(cube6);
                cube6.ApplyForce(dragForce);
            }

            cube7.Mass = 7;
            cube7.Scale = temp * 1.7f;
            cube7.Draw(gl);
            cube7.ApplyGravity();
            if (cube7.Position.y <= -40)
            {
                cube7.Position.y = -40;
                cube7.Velocity.y *= -1;
            }
            if (ocean.Contains(cube7))
            {
                var dragForce = ocean.CalculateDragForce(cube7);
                cube7.ApplyForce(dragForce);
            }

            cube8.Mass = 8;
            cube8.Scale = temp * 1.8f;
            cube8.Draw(gl);
            cube8.ApplyGravity();
            if (cube8.Position.y <= -40)
            {
                cube8.Position.y = -40;
                cube8.Velocity.y *= -1;
            }
            if (ocean.Contains(cube8))
            {
                var dragForce = ocean.CalculateDragForce(cube8);
                cube8.ApplyForce(dragForce);
            }

            cube9.Mass = 9;
            cube9.Scale = temp * 1.9f;
            cube9.Draw(gl);
            cube9.ApplyGravity();
            if (cube6.Position.y <= -40)
            {
                cube9.Position.y = -40;
                cube9.Velocity.y *= -1;
            }
            if (ocean.Contains(cube9))
            {
                var dragForce = ocean.CalculateDragForce(cube9);
                cube9.ApplyForce(dragForce);
            }

            cube10.Mass = 10;
            cube10.Scale = temp * 2f;
            cube10.Draw(gl);
            cube10.ApplyGravity();
            if (cube10.Position.y <= -40)
            {
                cube10.Position.y = -40;
                cube10.Velocity.y *= -1;
            }
            if (ocean.Contains(cube10))
            {
                var dragForce = ocean.CalculateDragForce(cube10);
                cube10.ApplyForce(dragForce);
            }

        }

        #region Initialization


        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }

        #endregion

        #region Mouse Func
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            //mousePos.x = (float)position.X - (float)Width / 2.0f;
           // mousePos.y = -((float)position.Y - (float)Height / 2.0f);
            //mousePos.y = -mousePos.y;

            //Console.WriteLine("mouse x:" + mousePos.x + "    y:" + mousePos.y);
        }
        #endregion

    }
}
