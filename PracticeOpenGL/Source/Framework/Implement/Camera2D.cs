using OpenTK;
using OpenTK.Graphics.ES20;
using PracticeOpenGL.Source.Framework.Implement.Debugs;

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
            this.Position = Vector2.Zero;
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
            var x = Position.X + (FrustrumWidth * (1.0f - Zoom) / 2.0f);
            var width = (Position.X + FrustrumWidth) - (FrustrumWidth * (1.0f - Zoom) / 2.0f);
            var y = Position.Y + (FrustrumHeight * (1.0f - Zoom) / 2.0f);
            var height = (Position.Y + FrustrumHeight) - (FrustrumHeight * (1.0f - Zoom) / 2.0f);

            // 平行投影
            m_ViewProjectionMatrix = Matrix4.Identity;
            Matrix4.CreateOrthographicOffCenter(
                //0f, FrustrumWidth,
                //FrustrumHeight, 0f,
                x, width,
                height, y,
                0f, 1f,
                out m_ViewProjectionMatrix);
#endif
        }

        public void SetZoom(float value)
        {
            if (value > 0f && value <= 1.0f)
            {
                this.Zoom = value;
            }
        }

        public void SetViewport()
        {
            //GL.Viewport(0, 0, m_GLGraphics.GetWidth(), m_GLGraphics.GetHeight());
            GL.Viewport(0, 0, m_GLGraphics.GetDrawableWidth(), m_GLGraphics.GetDrawableHeight());
        }

        public void TouchToWorld(ref Vector2 touch)
        {
            var scWidth = (float)m_GLGraphics.GetWidth();
            var scHeight = (float)m_GLGraphics.GetHeight();

            // 起点を算出
            var x = Position.X + (FrustrumWidth * (1.0f - Zoom) / 2.0f);
            var y = Position.Y + (FrustrumHeight * (1.0f - Zoom) / 2.0f);
            // スクリーンの座標の割合に、ズーム時の幅・高さを乗算して、投影する座標を算出
            x += (touch.X / scWidth * (FrustrumWidth * Zoom));
            y += (touch.Y / scHeight * (FrustrumHeight * Zoom));

            touch.X = x;
            touch.Y = y;

            //DebugLogViewer.WriteLine(string.Format(">>> touch ({0:0.0}, {1:0.0})", touch.X, touch.Y));
        }
    }
}
