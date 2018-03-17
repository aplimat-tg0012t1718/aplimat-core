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
        private List<CubeMesh> Cubes = new List<CubeMesh>();
        private Vector3 Wind =new Vector3(0.05f,0,0);
        private Liquid ocean = new Liquid(0, 0, 100, 50, 0.8f);

        private CubeMesh heavyCube = new CubeMesh(-5, 20, 0);
        private CubeMesh lightCube = new CubeMesh(5, 20, 0);

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

            /// Draw Cubes
            /// 

            Cubes.Add(heavyCube);
            Cubes.Add(new CubeMesh(-3, 20, 0));
            Cubes.Add(new CubeMesh(-2, 20, 0));
            Cubes.Add(new CubeMesh(-1, 20, 0));
            Cubes.Add(new CubeMesh(0, 20, 0));
            Cubes.Add(new CubeMesh(1, 20, 0));
            Cubes.Add(new CubeMesh(2, 20, 0));
            Cubes.Add(new CubeMesh(3, 20, 0));
            Cubes.Add(new CubeMesh(4, 20, 0));
            Cubes.Add(lightCube);

            for (int x = 0; x < 10; x++)
            {
                Cubes[x].Draw(gl);
                Cubes[x].Mass = 10 - x;
                Cubes[x].applyGravity();
            }

            for (int x = 0; x < 10; x++)
            {
                if (Cubes[x].Position.y <= -40)
                {
                    Cubes[x].Position.y = -40;
                    Cubes[x].Velocity.y *= -1;
                }
            }

            for (int x = 0; x < 10; x++)
            {
                if (ocean.Contains(Cubes[x]))
                {
                    var dragForce = ocean.CalculateDragForce(Cubes[x]);
                    Cubes[x].ApplyForce(dragForce);
                    Cubes[x].ApplyForce(Wind);
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
        }
        #endregion

    }
}
