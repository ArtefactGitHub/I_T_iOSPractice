using System;
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

        public nint GetWidth()
        {
            return m_GlView.DrawableWidth;
        }

        public nint GetHeight()
        {
            return m_GlView.DrawableHeight;
        }
    }
}
