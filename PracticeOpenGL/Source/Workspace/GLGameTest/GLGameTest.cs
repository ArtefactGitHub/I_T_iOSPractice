using System.Diagnostics;
using OpenTK.Graphics.ES20;
using PracticeOpenGL.Source.Framework;
using PracticeOpenGL.Source.Framework.Implement;
using OpenTK;
using System;

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
            #region property

            GLGraphics m_GLGraphics;
            GLProgramParameter m_ProgramPram;
            Texture m_Texture;

            #endregion

            public TestScreen(GLGame game) : base(game)
            {
                this.m_GLGraphics = game.GetGLGraphics();

                this.m_ProgramPram = new GLProgramParameter("Shader", "Shader");

                this.m_Texture = new Texture(m_Game, "icon.png");

                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
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
            }

            public override void Present(float deltaTime)
            {
#if false
                float g = ((int)deltaTime % 5) == 0 ? 0f : 0.5f;
                GL.ClearColor(0.5f, g, 0f, 1f);
                GL.Clear(ClearBufferMask.ColorBufferBit);
#else
                Vector3[] vertices =
                {
                    new Vector3 { X = -0.5f, Y = -0.5f, Z = 0f },
                    new Vector3 { X = 0.5f, Y = -0.5f, Z = 0f },
                    new Vector3 { X = 0.5f, Y = 0.5f, Z = 0f },
                    new Vector3 { X = 0.5f, Y = 0.5f, Z = 0f },
                    new Vector3 { X = -0.5f, Y = 0.5f, Z = 0f },
                    new Vector3 { X = -0.5f, Y = -0.5f, Z = 0f },
                };
                TextureCoord[] textureCoordinates =
                {
                    new TextureCoord { S = 0.0f, T = 0.0f},
                    new TextureCoord { S = 1.0f, T = 0.0f},
                    new TextureCoord { S = 1.0f, T = 1.0f},
                    new TextureCoord { S = 1.0f, T = 1.0f},
                    new TextureCoord { S = 0.0f, T = 1.0f},
                    new TextureCoord { S = 0.0f, T = 0.0f},
                };

                GL.ClearColor(0f, 0f, 0f, 1f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                if (m_ProgramPram != null)
                    m_ProgramPram.Use();

                GL.VertexAttribPointer(m_ProgramPram.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, vertices);
                GL.EnableVertexAttribArray(m_ProgramPram.PositionAttribute);

                GL.VertexAttribPointer(m_ProgramPram.TextureCoordinateAttribute, 2, VertexAttribPointerType.Float, false, 0, textureCoordinates);
                GL.EnableVertexAttribArray(m_ProgramPram.TextureCoordinateAttribute);


                float aspect = (float)Math.Abs((float)m_GLGraphics.GetWidth() / m_GLGraphics.GetHeight());
                Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(65.0f), aspect, 0.1f, 100.0f);

                Matrix4 baseModelViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
                Matrix4 modelViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -1.0f);

                modelViewMatrix = modelViewMatrix * baseModelViewMatrix;

                Matrix3 normalMatrix = new Matrix3(Matrix4.Transpose(Matrix4.Invert(modelViewMatrix)));

                Matrix4 modelViewProjectionMatrix = modelViewMatrix * projectionMatrix;

                GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramPram.GLProgram.Program, "matrix"),
                                  false,
                                  ref modelViewProjectionMatrix);


                GL.ActiveTexture(TextureUnit.Texture0);
                if (m_Texture != null)
                    m_Texture.Bind();
                GL.Uniform1(m_ProgramPram.TextureUniform, 0);

                GL.DrawArrays(BeginMode.Triangles, 0, vertices.Length);
#endif
            }
        }

        #endregion
    }
}
