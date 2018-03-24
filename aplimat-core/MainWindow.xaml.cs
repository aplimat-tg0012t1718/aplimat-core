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
        private Attractor Earth = new Attractor()
        {
            Mass = 3
        };
        private List<CubeMesh> stars = new List<CubeMesh>();
        private CubeMesh Star = new CubeMesh(-20, -30, 0);
        private Randomizer xRand = new Randomizer((double)-50,(double)50);
        private Randomizer yRand = new Randomizer((double)-50,(double)50);
        private Randomizer scaleRand = new Randomizer((double)1, (double)2.5);
        private int count = 0;

        public MainWindow()
        {
            Title = "Gravitational Attraction";
            InitializeComponent();
        }
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            /// Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            /// Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -100.0f);

            if(count < 10)
            {
                CubeMesh star = new CubeMesh((float)xRand.GenerateDouble(), (float)yRand.GenerateDouble(), 0);
                //star.Scale *= (float)Gaussian.Generate(1,1.5);
                star.Scale *= (float)scaleRand.GenerateDouble();
                stars.Add(star);
                count++;
            }

            Earth.Draw(gl);
            Earth.Scale = new Vector3(Earth.Mass, Earth.Mass, Earth.Mass);

            foreach (var star in stars) {
                star.Draw(gl);
                star.ApplyForce(Earth.CalculateAttraction(star));
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
