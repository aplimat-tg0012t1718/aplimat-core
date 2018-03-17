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

        private CubeMesh heavyCube = new CubeMesh(-5, 20, 0);
        private CubeMesh lightCube = new CubeMesh(5, 20, 0);

        private Vector3 wind = new Vector3(0.1f, 0, 0);

        private CubeMesh myCube = new CubeMesh(0, 20, 0);

        private Liquid ocean = new Liquid(0, 0, 100, 50, 0.8f);

        public MainWindow()
        {
            InitializeComponent();

            heavyCube.Mass = 3;
            lightCube.Mass = 1;

            heavyCube.Scale = new Vector3(1 * heavyCube.Mass, 1 * heavyCube.Mass, 1 * heavyCube.Mass);
            lightCube.Scale = new Vector3(1, 1, 1);
            
        }

        float boxT = 40.0f;
        float boxB = -40.0f;
        float boxL = -55.0f;
        float boxR = 55.0f;
        int cnt = 0;
        int xPos = 50;
        //private List<CubeMesh> myCubes = new List<CubeMesh>();
        ////private CubeMesh myCube = new CubeMesh(0, 0, 0);
        //private Randomizer yPos = new Randomizer(30, 50);
        //private Randomizer cubeM = new Randomizer(1, 3);
        private Vector3 mousePos = new Vector3(0, 0, 0);

        //private CubeMesh test = new CubeMesh()
        //{
        //    //Acceleration = new Vector3(0.1f, 0, 0),
        //    Position = new Vector3(0, 20, 0),
        //    Mass = 2
        //};

        

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            cnt++;

            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            ocean.Draw(gl);


            gl.Color(1.0f, 1.0f, 1.0f);
            
            if (cubes.Count <= 10)
            {
                for (int i = 0; i <= 10; i++)
                {
                    cubes.Add(new CubeMesh()
                    {
                        Position = new Vector3(xPos, 20, 0),
                        Mass = i
                    });
                    xPos -= 10;
                }
            }

            foreach (var cube in cubes)
            {
                cube.Draw(gl);
                cube.Scale = new Vector3(1 * cube.Mass/2, 1 * cube.Mass/2, 1 * cube.Mass/2);
                
                physicsTest(cube);
            }
            Console.WriteLine(cnt);

            if (cnt == 100)
            {
                cnt = 0;
                xPos = 50;
                cubes.Clear();
                
            }

            //myCube.Draw(gl);
            //myCube.ApplyGravity();
            //if(myCube.Position.y <= -40)
            //{
            //    myCube.Position.y = -40;
            //    myCube.Velocity *= -1;
            //}


            



            #region quiz
            //CubeMesh myCube = new CubeMesh();
            //myCube.Mass = cubeM.GenerateInt();
            //myCube.Position = new Vector3(Gaussian.Generate(0, 30), yPos.GenerateInt(), 0);


            //myCubes.Add(myCube);

            //foreach (var cube in myCubes)
            //{
            //    cube.Draw(gl);

            //    gravity(cube);
            //}


            //test.Draw(gl);
            //Console.WriteLine(test.Position.y);
            //gravity(test);
            //gravity(myCube);
            #endregion



        }

        private void physicsTest(CubeMesh cube)
        {
            //mousePos.Normalize();
            //mousePos *= 1;
            //cube.ApplyForce(mousePos);

            //var frictionCoefficient = 0.05f;
            //var normalForce = 1;
            //var frictionMagnitude = frictionCoefficient * normalForce;

            //var friction = cube.Velocity;
            //friction *= -1;
            //friction.Normalize();
            //friction *= frictionMagnitude;

            cube.ApplyGravity();
            cube.ApplyFriction();
            
            if (ocean.Contains(cube))
            {
                var dragForce = ocean.CalculateDragForce(cube);
                cube.ApplyForce(dragForce);
                cube.ApplyForce(wind);
            }
            
            if (cube.Position.y <= boxB)
            {
                cube.Position.y = boxB;
                cube.Velocity.y *= -1;
            }
            
            if (cube.Position.x >= boxR)
            {
                cube.Position.x = boxR;
                cube.Velocity.x *= -1;
            }
            if (cube.Position.x <= boxL)
            {
                cube.Position.x = boxL;
                cube.Velocity.x *= -1;
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
            //to get X = (float)position.X - (float)Width / 2.0f;
            //to get Y = -((float)position.Y - (float)Height / 2.0f);
            mousePos.x = (float)position.X - (float)Width / 2.0f;
            mousePos.y = (float)position.Y - (float)Height / 2.0f;

            mousePos.y = -mousePos.y;

            //Console.WriteLine("mouse x:" + mousePos.x + " y:" + mousePos.y);
        }
        #endregion

    }
}
