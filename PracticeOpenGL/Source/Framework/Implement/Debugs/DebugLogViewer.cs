using System.Collections.Generic;
using System.Diagnostics;
using CoreGraphics;
using PracticeOpenGL.Source.Framework.Interface;
using UIKit;

namespace PracticeOpenGL.Source.Framework.Implement.Debugs
{
    public static class DebugLogViewer
    {
        static DebugLogViewerImpl m_Impl = new DebugLogViewerImpl();

#if DEBUG

        public static UIView CreateView(
            float x, float y,
            float width, float height,
            int maxTextCount,
            UIColor backGroundColor,
            UITextAlignment align)
        {
            return m_Impl.CreateView(
                x, y,
                width, height,
                maxTextCount,
                backGroundColor,
                align);
        }

        public static void Clear()
        {
            m_Impl.Clear();
        }

        public static void WriteLine(object text)
        {
            m_Impl.WriteLine(text);
        }

        public static void SetVisible(bool isVisible)
        {
            m_Impl.SetVisible(isVisible);
        }

        #region Implements

        class DebugLogViewerImpl : IDebugLogViewer
        {
            UITextView m_View;

            List<object> m_Texts = new List<object>();

            int m_MaxTextCount = 10;

            public void Clear()
            {
                m_View.Text = "";

                m_Texts.Clear();
            }

            public UIView CreateView(
                float x, float y,
                float width, float height,
                int maxTextCount,
                UIColor backGroundColor,
                UITextAlignment align)
            {
                if (m_View != null)
                {
                    return m_View;
                }

                m_View = new UITextView(new CGRect(x, y, width, height))
                {
                    BackgroundColor = backGroundColor,
                    TextAlignment = align,

                    UserInteractionEnabled = false,

                    // 編集不可にする
                    Editable = false
                };
                m_MaxTextCount = maxTextCount;

                return m_View;
            }

            public void WriteLine(object text)
            {
                if (text == null)
                {
                    return;
                }

                // コンソールへ出力する
                Debug.WriteLine(text);

                // 最大要素数以上の場合、最後の要素を削除する
                if (m_Texts.Count >= m_MaxTextCount)
                {
                    m_Texts.RemoveAt(m_Texts.Count - 1);
                }

                // 先頭に追加する
                m_Texts.Insert(0, text);
                m_View.Text = string.Join("\n", m_Texts);
            }

            public void SetVisible(bool isVisible)
            {
                m_View.Hidden = !isVisible;
            }
        }

        #endregion

#else

        public static UIView CreateView(
            float x, float y,
            float width, float height,
            int maxTextCount,
            UIColor backGroundColor,
            UITextAlignment align)
        {
            return null;
        }

        public static void Clear()
        {
        }

        public static void WriteLine(object text)
        {
        }

        public static void SetVisible(bool isVisible)
        {
        }

#endif
    }
}
