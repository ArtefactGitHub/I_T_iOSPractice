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

            GLProgramParameter m_ProgramParam;

            Texture m_Texture;
            Texture m_TextureNotAlpha;

            #endregion

            #region data

            Vector3[] vertices =
            {
                new Vector3 { X = -0.5f, Y = -0.5f, Z = 0f },
                new Vector3 { X = 0.5f, Y = -0.5f, Z = 0f },
                new Vector3 { X = 0.5f, Y = 0.5f, Z = 0f },
                new Vector3 { X = -0.5f, Y = 0.5f, Z = 0f },
            };

            ushort[] indecies = {
                0, 1, 2,
                2, 3, 0
            };

            TextureCoord[] textureCoordinates =
            {
                new TextureCoord { S = 0.0f, T = 0.0f},
                new TextureCoord { S = 1.0f, T = 0.0f},
                new TextureCoord { S = 1.0f, T = 1.0f},
                new TextureCoord { S = 0.0f, T = 1.0f},
            };

            #endregion

            public TestScreen(GLGame game) : base(game)
            {
                m_GLGraphics = game.GetGLGraphics();

                Setup();
            }

            void Setup()
            {
                m_ProgramParam = new GLProgramParameter("Shader", "Shader");

                m_Texture = new Texture(m_Game, "Images/TestIcon.png");
                m_TextureNotAlpha = new Texture(m_Game, "Images/TestIconNotAlpha.png");

                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);

                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.Enable(EnableCap.Texture2D);

                m_Vertices = new Vertices(vertices, indecies, textureCoordinates, m_ProgramParam);
            }

            Vertices m_Vertices;

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
                GL.ClearColor(0.7f, 0.83f, 0.86f, 1f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                // ライトを設定
                float lightAngle = deltaTime;
                Vector3 lightDir = new Vector3((float)Math.Cos((float)lightAngle), -1.0f, (float)Math.Sin((float)lightAngle));
                lightDir.Normalize();
                GL.Uniform3(GL.GetUniformLocation(m_ProgramParam.GLProgram.Program, "lightDirection"), lightDir);

                // ビュー、プロジェクション行列
                float aspect = (float)Math.Abs((float)m_GLGraphics.GetWidth() / m_GLGraphics.GetHeight());
                Vector3 eyePos = new Vector3(0.0f, 0.0f, 5.0f);
                Vector3 lookAt = new Vector3(0.0f, 0.0f, 0.0f);
                Vector3 eyeUp = new Vector3(0.0f, 1.0f, 0.0f);
                Matrix4 viewMatrix = Matrix4.LookAt(eyePos, lookAt, eyeUp);
                Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)System.Math.PI / 4.0f, (float)m_GLGraphics.GetWidth() / (float)m_GLGraphics.GetHeight(), 0.1f, 100.0f);
                Matrix4 viewProjectionMatrix = viewMatrix * projectionMatrix;
                GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramParam.GLProgram.Program, "viewProjection"),
                                  false, ref viewProjectionMatrix);

                // ワールド行列
                Matrix4 worldMatrix = Matrix4.Identity;
                GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramParam.GLProgram.Program, "world"),
                                  false, ref worldMatrix);

                {
                    GL.ActiveTexture(TextureUnit.Texture0);
                    m_Texture.Bind();
                    GL.Uniform1(m_ProgramParam.TextureUniform, 0);

                    m_Vertices.Draw();
                }

                {
                    var baseModelViewMatrix = Matrix4.CreateTranslation(0.0f, 1.0f, 0.0f);
                    worldMatrix = worldMatrix * baseModelViewMatrix;
                    GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramParam.GLProgram.Program, "world"),
                                      false, ref worldMatrix);

                    GL.ActiveTexture(TextureUnit.Texture0);
                    m_TextureNotAlpha.Bind();
                    GL.Uniform1(m_ProgramParam.TextureUniform, 0);

                    m_Vertices.Draw();
                }
            }
        }

        #endregion
    }
}
