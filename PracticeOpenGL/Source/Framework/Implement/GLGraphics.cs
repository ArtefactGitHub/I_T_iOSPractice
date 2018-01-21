using System;
using GLKit;

namespace PracticeOpenGL.Source.Framework
{
    public class GLGraphics
    {
        //public GL m_GL;

        private GLKView m_GlView;

        public GLGraphics(GLKView glView)
        {
            this.m_GlView = glView;
        }

        //public GL GetGL()
        //{
        //    return m_GL;
        //}

        //public void SetGL(GL gl)
        //{
        //    this.m_GL = gl;
        //}

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
