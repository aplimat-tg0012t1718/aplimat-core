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
    /// 
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();
        }

        private Vector3 mousePos = new Vector3();
        private List<CubeMesh> myCubes = new List<CubeMesh>();
        private Randomizer rng = new Randomizer(30, 50);
        private Randomizer rngMass = new Randomizer(1,3);
        private Vector3 gravity = new Vector3(0,-0.5f,0);
        private Vector3 bounce = new Vector3(0, 2.0f, 0);
        private bool isBouncing;
       
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            mousePos.Normalize();
            mousePos *= 0.5f;
            CubeMesh myCube = new CubeMesh();
            myCube.Position = new Vector3((float)Gaussian.Generate(0, 30),rng.Generate(), 0);
            myCube.Mass *= rngMass.Generate();
            
            myCubes.Add(myCube);

            foreach(var cube in myCubes)
            {
                cube.Draw(gl);
                cube.Velocity = new Vector3(mousePos.x, 0, 0);
                cube.ApplyForce(gravity);

                if(cube.Position.y <= -35)
                {
                    cube.ApplyForce(bounce);
                    isBouncing = true;
                }

                if(isBouncing)
                {
                    cube.Velocity.y *=  2;
                    

                    isBouncing = false;
                }

                else if(!isBouncing)
                {
                    if(cube.Velocity.y <= 3)
                    {
                        cube.Velocity.y = 0;
                    }
                }
               

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
            mousePos = new Vector3 ((float)position.X,(float)position.Y,0);

            mousePos.x = (float)position.X - (float)Width / 2.0f;
            mousePos.y = -((float)position.Y - (float)Height / 2.0f);
            //to get X = (float)position.X - (float)Width / 2.0f;
            //to get Y = -((float)position.Y - (float)Height / 2.0f);
            mousePos.y = -mousePos.y;
        }
        #endregion

    }
}
