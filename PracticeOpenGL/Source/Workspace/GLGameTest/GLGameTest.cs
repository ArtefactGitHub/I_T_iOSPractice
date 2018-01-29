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

            Camera2D m_Camera;

            Vertices m_Vertices;

            const float FRUSTRUM_WIDTH = 720.0f;
            const float FRUSTRUM_HEIGHT = 1280.0f;

            #region data

            Vector3[] vertices =
            {
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 400.0f, 0.0f),
                new Vector3(400.0f, 400.0f, 0.0f),
                new Vector3(400.0f, 0.0f, 0.0f),
            };

            ushort[] indecies = {
                0, 1, 2,
                2, 3, 0
            };

            // 左下（0, 0)、右上（1, 1）
            TextureCoord[] textureCoordinates =
            {
                new TextureCoord { S = 0.0f, T = 1.0f},
                new TextureCoord { S = 0.0f, T = 0.0f},
                new TextureCoord { S = 1.0f, T = 0.0f},
                new TextureCoord { S = 1.0f, T = 1.0f},
            };

            #endregion

            #endregion

            public TestScreen(GLGame game) : base(game)
            {
                m_GLGraphics = game.GetGLGraphics();
                m_Camera = new Camera2D(game.GetGLGraphics(), FRUSTRUM_WIDTH, FRUSTRUM_HEIGHT);

                Setup();
            }

            void Setup()
            {
                m_ProgramParam = new GLProgramParameter("Shader", "Shader");

                m_Texture = new Texture(m_Game, "Images/TestIcon.png");
                m_TextureNotAlpha = new Texture(m_Game, "Images/TestIconNotAlpha.png");

                GL.FrontFace(FrontFaceDirection.Ccw);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);

                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.Enable(EnableCap.Texture2D);

                m_Vertices = new Vertices(vertices, indecies, textureCoordinates, m_ProgramParam);
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

                GL.ClearColor(0.7f, 0.83f, 0.86f, 1f);

                // ライトを設定
                float lightAngle = 45.0f;
                Vector3 lightDir = new Vector3((float)Math.Cos((float)lightAngle), -1.0f, (float)Math.Sin((float)lightAngle));
                lightDir.Normalize();
                GL.Uniform3(GL.GetUniformLocation(m_ProgramParam.GLProgram.Program, "lightDirection"), lightDir);

                // カメラを設定
                m_Camera.CreateViewportMatrix();
                Matrix4 viewProjectionMatrix = m_Camera.ViewProjectionMatrix;
                GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramParam.GLProgram.Program, "viewProjection"),
                                  false, ref viewProjectionMatrix);
            }

            public override void Update(float deltaTime)
            {
            }

            public override void Present(float deltaTime)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                m_Camera.SetViewport();

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
                    var baseModelViewMatrix = Matrix4.CreateTranslation(0.0f, 100.0f, 0.0f);
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
