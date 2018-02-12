using UIKit;

namespace PracticeOpenGL.Source.Framework.Interface
{
    public interface IDebugLogViewer
    {
        UIView CreateView(float x, float y,
                          float width, float height,
                          int maxTextCount,
                          UIColor backGroundColor,
                          UITextAlignment align);

        void SetVisible(bool isVisible);

        void Clear();

        void WriteLine(string text);
    }
}
