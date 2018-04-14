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

        private Vector3 gravity = new Vector3(0, -1.0f, 0);
        private Vector3 velocity = new Vector3(1, 0, 0);
        private float speed = 1.0f;
        private Randomizer yAxis = new Randomizer(30, 50);
        private Vector3 mouseMove = new Vector3(2, 2, 0);

        private List<CubeMesh> myCubes = new List<CubeMesh>();

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            CubeMesh myCube = new CubeMesh();

            // QUIZ # 1
            myCube.Position = new Vector3((float)Gaussian.Generate(), yAxis.Generate(), 0);
            myCubes.Add(myCube);

            myCube.Draw(gl);
            myCube.Position += velocity * speed;
            //myCube.ApplyForce(gravity);

            //myCube.ApplyForce(mouseMove);
            //mouseMove.Normalize();

            CubeMesh myCube2 = new CubeMesh();
            myCubes.Add(myCube2);

            foreach (var cube in myCubes)
            {
                gl.Color(Gaussian.Generate(0.0f, 1.0f), Gaussian.Generate(0.0f, 1.0f), Gaussian.Generate(0.0f, 1.0f));
                cube.Draw(gl);
            }
            // QUIZ # 1

            // QUIZ # 2
            if (Keyboard.IsKeyDown (Key.D))
            {
                myCube2.Velocity.x += 0.5f;
            }

            if (myCube2.Position.x <= -40)
            {
                myCube2.Position.x = -40;
                myCube2.Velocity.x *= +1;
            }
            // QUIZ # 2

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
            mouseMove.x = (float)position.X - (float)Width / 2.0f;
            mouseMove.y = -((float)position.Y - (float)Height / 2.0f);
        }
        #endregion

    }
}


