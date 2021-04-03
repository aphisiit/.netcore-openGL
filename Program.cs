using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace openTK_dotNetStandard
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var window = new Window())
            {
                window.Run(60);
            }
        }
    }

    class Window : GameWindow
    {   
        float r = 0.0f, g = 0.0f, b = 0.0f;
        float x = 0.0f, y = 1.0f, z = 1.0f;


        protected override void OnLoad(System.EventArgs e)
        {
            base.VSync = VSyncMode.On;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, base.Width, base.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            else if (input.IsKeyDown(Key.C))
            {
                if(r < 1.0f)
                    r += 0.1f;
                else if(g < 1.0f)
                    g += 0.1f;
                else if(b < 1.0f)
                    b += 0.1f;
                else
                    r = g = b = 0.0f;
            }
            else if (input.IsKeyDown(Key.Left) || input.IsKeyDown(Key.Right) || input.IsKeyDown(Key.Down) || input.IsKeyDown(Key.Up))
            {
                if(input.IsKeyDown(Key.Left))
                {
                    x -= 0.01f;
                }
                if (input.IsKeyDown(Key.Right))
                {
                    x += 0.01f;
                }
                if (input.IsKeyDown(Key.Down))
                {
                    y -= 0.01f;
                }
                if (input.IsKeyDown(Key.Up))
                {
                    y += 0.01f;
                }
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 0.0);

            // Update position 
            Matrix4 proj = Matrix4.CreateTranslation(x, y, z);
            GL.LoadMatrix(ref proj);

            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(r, g, b, 0.0f);
            GL.Vertex2(-1.0f, 1.0f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(0.0f, -0.5f);
            GL.Color3(Color.Ivory);
            GL.Vertex2(1.0f, 1.0f);
            
            GL.End();

            SwapBuffers();
        }
    }
}
