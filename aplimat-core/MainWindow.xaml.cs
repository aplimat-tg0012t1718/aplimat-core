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
        private Vector3 friction = new Vector3();
        #region Old Code
        //private CubeMesh heavyCube = new CubeMesh(-5, 20, 0);
        //private CubeMesh lightCube = new CubeMesh(5, 20, 0);

        //private Vector3 gravity = new Vector3(0, -0.1f, 0);
        //private Vector3 wind = new Vector3(0.1f, 0, 0);
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            int xPos = 50;
            for (int i = 0; i <= 10; i++)
            {
                cubes.Add(new CubeMesh
                {
                    Position = new Vector3(xPos, 20, 0),
                    Mass = i
                });
                xPos -= 10;
            }

            #region Old Code
            //heavyCube.Mass = 3;
            //lightCube.Mass = 1;

            //heavyCube.Scale = new Vector3(1 * heavyCube.Mass, 1 * heavyCube.Mass, 1 * heavyCube.Mass);
            //lightCube.Scale = new Vector3(1, 1, 1);



            #endregion
        }

        //int counter = 0;
        /*private Randomizer rng = new Randomizer(30,50);
        private List<CubeMesh> snow = new List<CubeMesh>();
        private Vector3 Gravity = new Vector3(0, -0.01f, 0);
        private Vector3 MouseTracker = new Vector3(0, 0, 0);
        private Vector3 Bounce = new Vector3(0, 0.1f, 0);*/


        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            var frictionCoefficient = 0.05f;
            var normalForce = 1;
            var frictionMagnitude = frictionCoefficient * normalForce;

            foreach (var cube in cubes)
            {
                var friction = cube.Velocity;
                friction *= -1;
                friction.Normalize();
                friction *= frictionMagnitude;

                cube.Draw(gl);
                cube.ApplyGravity();
                cube.ApplyForce(friction);
                cube.ApplyForce(new Vector3(0.1f, 0.0f, 0.0f));

                cube.Scale = new Vector3(1 * cube.Mass / 2, 1 * cube.Mass / 2, 1 * cube.Mass / 2);

                if (cube.Position.y <= -40)
                {
                    cube.Position.y = -40;
                    cube.Velocity.y *= -1;
                }

                if (cube.Position.x >= 40)
                {
                    cube.Position.x = 40;
                    cube.Velocity.x *= -1;
                }

            }

            #region Old Code
            //heavyCube.Draw(gl);
            //lightCube.Draw(gl);

            //heavyCube.ApplyGravity();
            //lightCube.ApplyGravity();

            //heavyCube.ApplyForce(wind);
            //lightCube.ApplyForce(wind);

            //if (heavyCube.Position.y <= -40)
            //{
            //    heavyCube.Position.y = -40;
            //    heavyCube.Velocity.y *= -1;
            //}

            //if (lightCube.Position.y <= -40)
            //{
            //    lightCube.Position.y = -40;
            //    lightCube.Velocity.y *= -1;
            //}

            //if (heavyCube.Position.x >= 40)
            //{
            //    heavyCube.Position.x = 40;
            //    heavyCube.Velocity.x *= -1;
            //}

            //if (lightCube.Position.x >= 40)
            //{
            //    lightCube.Position.x = 40;
            //    lightCube.Velocity.x *= -1;
            //}


            ////Create new snowflake
            //CubeMesh snowflake = new CubeMesh();
            //snowflake.Position = new Vector3((float)Gaussian.Generate(0, 30), (float)rng.Generate(), 0);

            ////Adds a snowflake to the snow list
            //snow.Add(snowflake);

            //// Clear The Screen And The Depth Buffer
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //// Move Left And Into The Screen
            //gl.LoadIdentity();
            //gl.Translate(0.0f, 0.0f, -100.0f);

            ////Applies the physics to the snowflakes in the snow list
            //MouseTracker.Normalize();
            //foreach (var flake in snow)
            //{
            //    flake.ApplyForce(Gravity);
            //    flake.ApplyForce(MouseTracker);
            //    if (flake.Position.y < -20.0f)
            //        flake.Velocity.y *= -0.5f;

            //}

            ////Draws the snowflakes in the snow list
            //foreach (var flake in snow)
            //{
            //    flake.Draw(gl);
            //}
            //gl.DrawText(0, 0, 1, 1, 1, "Arial", 15, "Mouse X  is:" + MouseTracker.x + " Mouse Y is:" + MouseTracker.y);
            #endregion
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
            #region Old Code
            /*float X = (float)position.X - (float)Width / 2.0f;
            float Y = (float)position.Y - (float)Width / 2.0f;

            Y = -Y;

            MouseTracker.x = X;
            MouseTracker.y = Y;*/
            //to get X = (float)position.X - (float)Width / 2.0f;
            //to get Y = -((float)position.Y - (float)Height / 2.0f);
#endregion
        }
        #endregion

    }
}
