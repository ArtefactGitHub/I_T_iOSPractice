using System.Diagnostics;
using OpenTK.Graphics.ES20;
using PracticeOpenGL.Source.Framework;

namespace PracticeOpenGL.Source.Workspace
{
    public class GLGameTest : GLGame
    {
        public override Screen GetStartScreen()
        {
            return new TestScreen(this);
        }

        #region Screen

        class TestScreen : GLScreen
        {
            #region Enum

            enum Uniform
            {
                ModelViewProjection_Matrix,
                Normal_Matrix,
                Count
            }

            enum Attribute
            {
                Vertex,
                Normal,
                Count
            }

            #endregion

            #region property

            #region VertexData

            float[] cubeVertexData = {
                // Data layout for each line below is:
                // positionX, positionY, positionZ,     normalX, normalY, normalZ,
                0.5f, -0.5f, -0.5f,        1.0f, 0.0f, 0.0f,
                0.5f, 0.5f, -0.5f,         1.0f, 0.0f, 0.0f,
                0.5f, -0.5f, 0.5f,         1.0f, 0.0f, 0.0f,
                0.5f, -0.5f, 0.5f,         1.0f, 0.0f, 0.0f,
                0.5f, 0.5f, -0.5f,          1.0f, 0.0f, 0.0f,
                0.5f, 0.5f, 0.5f,         1.0f, 0.0f, 0.0f,

                0.5f, 0.5f, -0.5f,         0.0f, 1.0f, 0.0f,
                -0.5f, 0.5f, -0.5f,        0.0f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.5f,          0.0f, 1.0f, 0.0f,
                0.5f, 0.5f, 0.5f,          0.0f, 1.0f, 0.0f,
                -0.5f, 0.5f, -0.5f,        0.0f, 1.0f, 0.0f,
                -0.5f, 0.5f, 0.5f,         0.0f, 1.0f, 0.0f,

                -0.5f, 0.5f, -0.5f,        -1.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, -0.5f,       -1.0f, 0.0f, 0.0f,
                -0.5f, 0.5f, 0.5f,         -1.0f, 0.0f, 0.0f,
                -0.5f, 0.5f, 0.5f,         -1.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, -0.5f,       -1.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, 0.5f,        -1.0f, 0.0f, 0.0f,

                -0.5f, -0.5f, -0.5f,       0.0f, -1.0f, 0.0f,
                0.5f, -0.5f, -0.5f,        0.0f, -1.0f, 0.0f,
                -0.5f, -0.5f, 0.5f,        0.0f, -1.0f, 0.0f,
                -0.5f, -0.5f, 0.5f,        0.0f, -1.0f, 0.0f,
                0.5f, -0.5f, -0.5f,        0.0f, -1.0f, 0.0f,
                0.5f, -0.5f, 0.5f,         0.0f, -1.0f, 0.0f,

                0.5f, 0.5f, 0.5f,          0.0f, 0.0f, 1.0f,
                -0.5f, 0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
                0.5f, -0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
                0.5f, -0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
                -0.5f, 0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f,        0.0f, 0.0f, 1.0f,

                0.5f, -0.5f, -0.5f,        0.0f, 0.0f, -1.0f,
                -0.5f, -0.5f, -0.5f,       0.0f, 0.0f, -1.0f,
                0.5f, 0.5f, -0.5f,         0.0f, 0.0f, -1.0f,
                0.5f, 0.5f, -0.5f,         0.0f, 0.0f, -1.0f,
                -0.5f, -0.5f, -0.5f,       0.0f, 0.0f, -1.0f,
                -0.5f, 0.5f, -0.5f,        0.0f, 0.0f, -1.0f
            };

            #endregion

            //Matrix4 modelViewProjectionMatrix;
            //Matrix3 normalMatrix;

            //uint vertexArray;
            //uint vertexBuffer;

            #endregion

            private GLGraphics m_GLGraphics;

            public TestScreen(GLGame game) : base(game)
            {
                this.m_GLGraphics = game.GetGLGraphics();
            }

            public override void Dispose()
            {
                Debug.WriteLine("Dispose");

                //GL.DeleteBuffers(1, ref vertexBuffer);
                //GL.Oes.DeleteVertexArrays(1, ref vertexArray);
            }

            public override void Pause()
            {
                Debug.WriteLine("Pause");
            }

            public override void Resume()
            {
                Debug.WriteLine("Resume");
            }

            public override void Update(float deltaTime)
            {
                ////var aspect = (float)Math.Abs(View.Bounds.Size.Width / View.Bounds.Size.Height);
                //var aspect = (float)Math.Abs(m_GLGraphics.GetWidth() / m_GLGraphics.GetHeight());
                //var projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(65.0f), aspect, 0.1f, 100.0f);

                //var baseModelViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
                //var modelViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

                //modelViewMatrix = modelViewMatrix * baseModelViewMatrix;

                //normalMatrix = new Matrix3(Matrix4.Transpose(Matrix4.Invert(modelViewMatrix)));

                //modelViewProjectionMatrix = modelViewMatrix * projectionMatrix;
            }

            public override void Present(float deltaTime)
            {
                float g = ((int)deltaTime % 5) == 0 ? 0f : 0.5f;
                GL.ClearColor(0.5f, g, 0f, 1f);
                GL.Clear(ClearBufferMask.ColorBufferBit);
            }

            protected override void SetupGL()
            {
                //LoadShaders();

                //GL.Enable(EnableCap.DepthTest);

                //GL.Oes.GenVertexArrays(1, out vertexArray);
                //GL.Oes.BindVertexArray(vertexArray);

                //GL.GenBuffers(1, out vertexBuffer);
                //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
                //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(cubeVertexData.Length * sizeof(float)), cubeVertexData, BufferUsage.StaticDraw);

                //GL.EnableVertexAttribArray((int)GLKVertexAttrib.Position);
                //GL.VertexAttribPointer((int)GLKVertexAttrib.Position, 3, VertexAttribPointerType.Float, false, 24, new IntPtr(0));
                //GL.EnableVertexAttribArray((int)GLKVertexAttrib.Normal);
                //GL.VertexAttribPointer((int)GLKVertexAttrib.Normal, 3, VertexAttribPointerType.Float, false, 24, new IntPtr(12));

                //GL.Oes.BindVertexArray(0);
            }
        }

        #endregion
    }
}
