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
        private List<CubeMesh> stars = new List<CubeMesh>();      
        private Randomizer yRand = new Randomizer(-30.0, 30.0);
        private Randomizer scaleRand = new Randomizer(.1, 1.0);
        private Randomizer colorRand = new Randomizer((double)0, (double)1);

        private int count = 0;
        private int frames = 0; // "frames"

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

            frames++;
            if(count < 90)
            {
                float x = (float)Gaussian.Generate(-10,10);
                float y = (float)yRand.GenerateDouble();
                CubeMesh star = new CubeMesh(x, y, 0);
                star.Mass = (float)Gaussian.Generate(.2, .5);
                star.Scale = new Vector3(star.Mass, star.Mass, star.Mass);
                stars.Add(star);
                count++;
            }

            foreach (var star in stars)
            {
                Attractor pull = new Attractor();
                pull.Mass = star.Mass;
                foreach (var other in stars)
                {
                    if (star != other)
                    {
                        Attractor otherPull = new Attractor();
                        otherPull.Mass = other.Mass;
                        other.ApplyForce(pull.CalculateAttraction(other));
                        star.ApplyForce(otherPull.CalculateAttraction(star));
                    }
                }
                star.Draw(gl);
                gl.Color(colorRand.GenerateDouble(), colorRand.GenerateDouble(),colorRand.GenerateDouble());
            }

            if(frames == 300)
            {
                frames = 0;
                stars.Clear();
                count = 0;
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
