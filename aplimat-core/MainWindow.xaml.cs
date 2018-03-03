using aplimat_labs.Models;
using aplimat_labs.Utilities;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
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
    public partial class MainWindow : Window
    {

  
        private Randomizer yAxis = new Randomizer(30, 50);
        

        public MainWindow()
        {
            InitializeComponent();

        }

        private Vector3 gravity = new Vector3(0, -0.05f, 0);
        //private CubeMesh mover = new CubeMesh(-25, 0, 0);
        private Vector3 mouseMovement = new Vector3(0, 0, 0);
        private Vector3 velocity = new Vector3(1, 0, 0);
        private List<CubeMesh> myCubes = new List<CubeMesh>();
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.Translate(0.0f, 0.0f, -40.0f);

            

            CubeMesh myCube = new CubeMesh();
            myCube.Position = new Vector3(Gaussian.Generate(0, 15), yAxis.GenerateDouble(), 0);
            myCubes.Add(myCube);

            myCube.Draw(gl);
           
            myCube.ApplyForce(gravity);
            //myCube.ApplyForce(mouseMovement);
            //mouseMovement.Normalize();


            if (myCube.Position.x >= 25.0f)
            {
                velocity.x = -1;
            }
            else if (myCube.Position.x <= -25.0f)
            {
                velocity.x = 1;
            }
            if (myCube.Position.y >= 10.0f)
            {
                velocity.y = -1;
            }
            else if (myCube.Position.y <= -10.0f)
            {
                velocity.y = 1;
            }
            if (myCube.Position.y >= 30.0f)
            {
                velocity.x = -1;
            }
            if (myCube.Position.y <= 30.0f)
            {
                velocity.y = 1;
            }


            foreach (var cube in myCubes)
            {
                cube.Draw(gl);
                cube.ApplyForce(mouseMovement);
                mouseMovement.Normalize();
            }

        }

       #region opengl init
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
            //gl.Color(RGB.GenerateDouble(), RGB.GenerateDouble(), RGB.GenerateDouble());
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_LIGHT0);



            gl.ShadeModel(OpenGL.GL_SMOOTH);

        }
        //private Vector3 mousePos = new Vector3(2.0f, -2.0f, 0);

        private void OpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            //mousePos = new Vector3(e.GetPosition(this).x, e.GetPosition(this).y, 0);

            //myCube.ApplyForce(mousePos);

            var pos = e.GetPosition(this);
            mouseMovement.x = (float)pos.X - (float)Width / 2;
            mouseMovement.y = (float)pos.Y - (float)Height / 2;
         

            Console.WriteLine("mouse x;" + mouseMovement.x + " y:" + mouseMovement.y);
        }
        #endregion

        #region draw text
        private void DrawText(OpenGL gl, string text, int x, int y)
        {
            gl.DrawText(x, y, 1, 1, 1, "Arial", 12, text);
        }
        #endregion

        private void OpenGLControl_MouseMove_1(object sender, MouseEventArgs e)
        {

        }
    }
}