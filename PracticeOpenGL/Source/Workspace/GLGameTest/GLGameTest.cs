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

            TextureRegion m_RegionUL;
            TextureRegion m_RegionUR;

            SpriteBatcher m_SpriteBatcher;

            const float FRUSTRUM_WIDTH = 720.0f;
            const float FRUSTRUM_HEIGHT = 1280.0f;

            #region data

            // 左上が原点
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

            // 左上が原点（シェーダで上下反転させている）
            // https://ja.wikibooks.org/wiki/OpenGL%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0/%E3%83%A2%E3%83%80%E3%83%B3OpenGL_%E3%83%81%E3%83%A5%E3%83%BC%E3%83%88%E3%83%AA%E3%82%A2%E3%83%AB_06
            TextureCoord[] textureCoordinates =
            {
                new TextureCoord { S = 0.0f, T = 0.0f},
                new TextureCoord { S = 0.0f, T = 1.0f},
                new TextureCoord { S = 1.0f, T = 1.0f},
                new TextureCoord { S = 1.0f, T = 0.0f},
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

                m_SpriteBatcher = new SpriteBatcher(m_GLGraphics, m_ProgramParam, 100);

                GL.FrontFace(FrontFaceDirection.Ccw);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);

                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.Enable(EnableCap.Texture2D);
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

                m_Texture = new Texture(m_Game, "Images/TestIconAtlas.png");

                m_RegionUL = new TextureRegion(m_Texture, 0, 0, 128, 128);
                m_RegionUR = new TextureRegion(m_Texture, 128, 0, 128, 128);
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

                m_SpriteBatcher.BeginBatch(m_Texture);

                {
                    m_SpriteBatcher.DrawSprite(0, 600, 128.0f, 128.0f, m_RegionUL);
                    m_SpriteBatcher.DrawSprite(128.0f, 0, 128.0f, 128.0f, m_RegionUR);
                }

                m_SpriteBatcher.EndBatch();
            }
        }

        #endregion
    }
}
