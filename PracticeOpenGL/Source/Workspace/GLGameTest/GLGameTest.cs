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
                m_ProgramPram = new GLProgramParameter("Shader", "Shader");

                m_Texture = new Texture(m_Game, "Images/TestIcon.png");

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
                GL.ClearColor(0.7f, 0.83f, 0.86f, 1f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                if (m_ProgramPram == null)
                {
                    Debug.WriteLine("program is invalid");
                    return;
                }

                m_ProgramPram.Use();

                // ライトを設定
                float lightAngle = deltaTime;
                Vector3 lightDir = new Vector3((float)Math.Cos((float)lightAngle), -1.0f, (float)Math.Sin((float)lightAngle));
                lightDir.Normalize();
                GL.Uniform3(GL.GetUniformLocation(m_ProgramPram.GLProgram.Program, "lightDirection"), lightDir);

                // 頂点座標
                GL.VertexAttribPointer(m_ProgramPram.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, vertices);
                GL.EnableVertexAttribArray(m_ProgramPram.PositionAttribute);

                // テクスチャ座標
                GL.VertexAttribPointer(m_ProgramPram.TextureCoordinateAttribute, 2, VertexAttribPointerType.Float, false, 0, textureCoordinates);
                GL.EnableVertexAttribArray(m_ProgramPram.TextureCoordinateAttribute);

                // ビュー、プロジェクション行列
                float aspect = (float)Math.Abs((float)m_GLGraphics.GetWidth() / m_GLGraphics.GetHeight());
                Vector3 eyePos = new Vector3(0.0f, 0.0f, 5.0f);
                Vector3 lookAt = new Vector3(0.0f, 0.0f, 0.0f);
                Vector3 eyeUp = new Vector3(0.0f, 1.0f, 0.0f);
                Matrix4 viewMatrix = Matrix4.LookAt(eyePos, lookAt, eyeUp);
                Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)System.Math.PI / 4.0f, (float)m_GLGraphics.GetWidth() / (float)m_GLGraphics.GetHeight(), 0.1f, 100.0f);
                Matrix4 viewProjectionMatrix = viewMatrix * projectionMatrix;
                GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramPram.GLProgram.Program, "viewProjection"),
                                  false, ref viewProjectionMatrix);

                // ワールド行列
                Matrix4 worldMatrix = Matrix4.Identity;
                GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramPram.GLProgram.Program, "world"),
                                  false, ref worldMatrix);

                GL.ActiveTexture(TextureUnit.Texture0);
                if (m_Texture != null)
                {
                    m_Texture.Bind();
                }
                GL.Uniform1(m_ProgramPram.TextureUniform, 0);

                GL.DrawElements(BeginMode.Triangles, indecies.Length, DrawElementsType.UnsignedShort, indecies);
            }
        }

        #endregion
    }
}
