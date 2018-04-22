using CoreGraphics;
using GLKit;
using PracticeOpenGL.Source.Framework.Implement.Settings;
using UIKit;

namespace PracticeOpenGL.Source.Framework
{
    public class GLGraphics
    {
        #region property

        GLKView m_GlView;

        CGRect m_DrawableAreaRect;

        float m_ScaleFactor;

        #endregion

        public GLGraphics(GLKView glView)
        {
            this.m_GlView = glView;

            if (Setting.CanUseSafeArea)
            {
                //var margins = glView.LayoutMargins;
                var margins = glView.SafeAreaInsets;
                m_DrawableAreaRect = new CGRect(
                    margins.Left,
                    margins.Top,
                    (GetWidth() - (margins.Left + margins.Right)),
                    (GetHeight() - (margins.Top + margins.Bottom)));

                m_ScaleFactor = (float)glView.ContentScaleFactor;
            }
            else
            {
                m_DrawableAreaRect = new CGRect(0, 0, GetDrawableWidth(), GetDrawableHeight());

                m_ScaleFactor = 1.0f;
            }
        }

        public float GetScaleFactor()
        {
            return m_ScaleFactor;
        }

        public CGRect GetDrawableAreaRect()
        {
            return m_DrawableAreaRect;
        }

        public int GetWidth()
        {
            return (int)m_GlView.Bounds.Width;
        }

        public int GetHeight()
        {
            return (int)m_GlView.Bounds.Height;
        }

        public int GetDrawableWidth()
        {
            return (int)m_GlView.DrawableWidth;
        }

        public int GetDrawableHeight()
        {
            return (int)m_GlView.DrawableHeight;
        }
    }
}
