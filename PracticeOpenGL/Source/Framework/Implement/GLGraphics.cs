using GLKit;

namespace PracticeOpenGL.Source.Framework
{
    public class GLGraphics
    {
        GLKView m_GlView;

        public GLGraphics(GLKView glView)
        {
            this.m_GlView = glView;
        }

        public int GetWidth()
        {
            return (int)m_GlView.DrawableWidth;
        }

        public int GetHeight()
        {
            return (int)m_GlView.DrawableHeight;
        }
    }
}
