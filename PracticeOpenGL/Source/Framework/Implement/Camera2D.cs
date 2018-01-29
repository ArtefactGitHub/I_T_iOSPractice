using OpenTK;
using OpenTK.Graphics.ES20;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class Camera2D
    {
        public Vector2 Position { get; private set; }

        public float Zoom { get; private set; }

        public float FrustrumWidth { get; private set; }

        public float FrustrumHeight { get; private set; }

        public Matrix4 ViewProjectionMatrix { get { return m_ViewProjectionMatrix; } }

        GLGraphics m_GLGraphics;

        Matrix4 m_ViewProjectionMatrix;

        public Camera2D(GLGraphics glGraphics, float frustrumWidth, float frustrumHeight)
        {
            this.m_GLGraphics = glGraphics;
            this.FrustrumWidth = frustrumWidth;
            this.FrustrumHeight = frustrumHeight;
            this.Position = new Vector2(frustrumWidth / 2.0f, frustrumHeight / 2.0f);
            this.Zoom = 1.0f;

            m_ViewProjectionMatrix = Matrix4.Identity;
        }

        public void CreateViewportMatrix()
        {
            // ビュー、プロジェクション行列
#if false
            // 透視投影
            float aspect = (float)Math.Abs((float)m_GLGraphics.GetWidth() / m_GLGraphics.GetHeight());
            Vector3 eyePos = new Vector3(0.0f, 0.0f, 5.0f);
            Vector3 lookAt = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 eyeUp = new Vector3(0.0f, 1.0f, 0.0f);
            Matrix4 viewMatrix = Matrix4.LookAt(eyePos, lookAt, eyeUp);

            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)System.Math.PI / 4.0f, (float)m_GLGraphics.GetWidth() / (float)m_GLGraphics.GetHeight(), 0.1f, 100.0f);
            Matrix4 viewProjectionMatrix = viewMatrix * projectionMatrix;
#else
            // 平行投影
            m_ViewProjectionMatrix = Matrix4.Identity;
            Matrix4.CreateOrthographicOffCenter(
                0f, m_GLGraphics.GetWidth(),
                m_GLGraphics.GetHeight(), 0f,
                0f, 1f,
                out m_ViewProjectionMatrix);
#endif
        }

        public void SetViewport()
        {
            GL.Viewport(0, 0, m_GLGraphics.GetWidth(), m_GLGraphics.GetHeight());
        }

        public void TouchToWorld(Vector2 touch)
        {
            touch.X = (touch.X / (float)m_GLGraphics.GetWidth()) * FrustrumWidth * Zoom;
            touch.Y = (1 - touch.Y / (float)m_GLGraphics.GetHeight()) * FrustrumHeight * Zoom;
            touch = Vector2.Add(Position, touch);
            touch = Vector2.Subtract(touch, new Vector2(FrustrumWidth * Zoom / 2.0f, FrustrumHeight * Zoom / 2.0f));
        }
    }
}
