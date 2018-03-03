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

        public MainWindow()
        {
            InitializeComponent();

        }

        private List<CubeMesh> cubes = new List<CubeMesh>();

        private Randomizer scRandomizer = new Randomizer(0, 3);
        private Randomizer yRandomizer = new Randomizer(30, 50);
        private Randomizer mRandomizer = new Randomizer(1, 6);

        private Vector3 mousePos = new Vector3();
        private Vector3 gravity = new Vector3(0, -.4f, 0);
        private Vector3 mGravity = new Vector3();

        private float yBottom = -45;

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            /// Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            /// Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            /// Draw Cubes

            mousePos.Normalize();
            mousePos *= 10;
            mGravity = mousePos;

            CubeMesh cube = new CubeMesh();
            cube.Position = new aplimat_core.models.Vector3((float)Gaussian.Generate(0, 30), (float)yRandomizer.Generate(), 0);
            float cubeScale = (float)scRandomizer.Generate();
            cube.Scale = new Vector3(cubeScale, cubeScale, 1);
            cube.Mass = mRandomizer.Generate();

            cubes.Add(cube);

            foreach (var c in cubes)
            {
                c.Draw(gl);
                c.ApplyForce(gravity);
                if(c.Position.y <= yBottom)
                {
                    c.Velocity.y *= -1;
                    c.Velocity /= 2;
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
            mousePos.x = (float)position.X - (float)Width / 2.0f;
            mousePos.y = -((float)position.Y - (float)Height / 2.0f);
            //mousePos.y = -mousePos.y;

            foreach(var c in cubes)
            {
                mousePos.Normalize();
                mousePos /= 10 ;
                c.ApplyForce(mousePos);
            }

            Console.WriteLine("mouse x:" + mousePos.x + "    y:" + mousePos.y);
        }
        #endregion

    }
}
