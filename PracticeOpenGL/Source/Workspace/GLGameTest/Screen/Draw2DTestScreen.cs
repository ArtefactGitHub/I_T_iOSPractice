﻿using System;
using OpenTK;
using OpenTK.Graphics.ES20;
using PracticeOpenGL.Source.Framework;
using PracticeOpenGL.Source.Framework.Implement;
using PracticeOpenGL.Source.Framework.Implement.Debugs;

namespace PracticeOpenGL.Source.Workspace.GLGameTest
{
    public class Draw2DTestScreen : GLScreen
    {
        #region property

        GLGraphics m_GLGraphics;

        SpriteBatcher m_SpriteBatcher;

        GLProgramParameter m_ProgramParam;

        Texture m_Texture;

        Camera2D m_Camera;

        TextureRegion m_RegionUL;

        Animation m_Animation;
        AnimationSample m_AnimationSample;

        Texture m_TextureAnimation;

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

        public Draw2DTestScreen(GLGame game) : base(game)
        {
            m_GLGraphics = game.GetGLGraphics();
            m_Camera = new Camera2D(game.GetGLGraphics(), FRUSTRUM_WIDTH, FRUSTRUM_HEIGHT);

            m_AnimationSample = new AnimationSample();

            Setup();
        }

        void Setup()
        {
            m_ProgramParam = new GLProgramParameter("Shader", "Shader");

            m_SpriteBatcher = new SpriteBatcher(m_GLGraphics, m_ProgramParam, 100);

            GL.FrontFace(FrontFaceDirection.Ccw);
            //GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }

        public override void Dispose()
        {
            DebugLogViewer.WriteLine("Dispose");

            //GL.DeleteBuffers(1, ref vertexBuffer);
            //GL.Oes.DeleteVertexArrays(1, ref vertexArray);
        }

        public override void Pause()
        {
            DebugLogViewer.WriteLine("Pause");
        }

        public override void Resume()
        {
            DebugLogViewer.WriteLine("Resume");

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

            m_Texture = new Texture(m_Game, "Images/AnimationSample.png");
            m_RegionUL = new TextureRegion(m_Texture, 0, 0, 64, 64);


            m_TextureAnimation = new Texture(m_Game, "Images/AnimationSample.png");
            m_Animation = new Animation(0.2f, new TextureRegion[]
            {
                    new TextureRegion(m_TextureAnimation, 0, 0, 64, 64),
                    new TextureRegion(m_TextureAnimation, 64, 0, 64, 64),
                    new TextureRegion(m_TextureAnimation, 128, 0, 64, 64),
                    new TextureRegion(m_TextureAnimation, 192, 0, 64, 64),
            });
        }

        public override void Update(float deltaTime)
        {
            m_AnimationSample.Update(deltaTime);
        }

        public override void Present(float deltaTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Texture2D);

            m_Camera.SetViewport();

            // ワールド行列
            Matrix4 worldMatrix = Matrix4.Identity;
            GL.UniformMatrix4(GL.GetUniformLocation(m_ProgramParam.GLProgram.Program, "world"),
                              false, ref worldMatrix);

            // スプライトの描画
            m_SpriteBatcher.BeginBatch(m_Texture);
            {
                m_SpriteBatcher.DrawSprite(0, 700, 128.0f, 128.0f, m_RegionUL);
            }
            m_SpriteBatcher.EndBatch();

            // アニメーションの描画
            m_SpriteBatcher.BeginBatch(m_Texture);
            {
                TextureRegion keyFrame = m_Animation.GetKeyFrame(m_AnimationSample.m_Time, Animation.ANIMATION_LOOPING);
                m_SpriteBatcher.DrawSprite(300, 700, 128, 128, keyFrame);
                //m_SpriteBatcher.DrawSprite(0, 0, 128.0f, 128.0f, m_RegionUL2);
            }
            m_SpriteBatcher.EndBatch();


            GL.Disable(EnableCap.Blend);
        }
    }
}
