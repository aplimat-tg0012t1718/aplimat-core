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
        #region Variable Declarations

        private List<CubeMesh> cubes = new List<CubeMesh>();
        private Vector3 wind = new Vector3(0.5f, 0.0f, 0.0f);
        private Vector3 mouseForce = new Vector3(0.0f, 0.0f, 0.0f);
        private CubeMesh heavyCube = new CubeMesh(-5, 20 ,0);
        private CubeMesh lightCube = new CubeMesh(5, 20, 0);

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            int xPos = 50;
            //for(int i = 0; i<= 10; i++)
            //{
            //    cubes.Add(new CubeMesh()
            //    {
            //        Position = new Vector3(xPos, 20, 0),
            //        Mass = i
            //    });
            //}
            heavyCube.Mass = 3;
            lightCube.Mass = 1;

            heavyCube.Scale = new Vector3(1 * heavyCube.Mass, 1 * heavyCube.Mass, 1 * heavyCube.Mass);
            lightCube.Scale = new Vector3(1 * lightCube.Mass, 1 * lightCube.Mass, 1 * lightCube.Mass);
        }



        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            heavyCube.Draw(gl);
            lightCube.Draw(gl);

            heavyCube.ApplyGravity();
            lightCube.ApplyGravity();
            heavyCube.ApplyForce(wind);
            lightCube.ApplyForce(wind);

            if(heavyCube.Position.y <= -40)
            {
                heavyCube.Position.y = -40;
                heavyCube.Velocity *= -1;
            }
            if (lightCube.Position.y <= -40)
            {
                lightCube.Position.y = -40;
                lightCube.Velocity *= -1;
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
            mouseForce.x = (float)position.X - (float)Width / 2.0f;
            mouseForce.y = -((float)position.Y - (float)Height / 2.0f);
        }
        #endregion

    }
}
