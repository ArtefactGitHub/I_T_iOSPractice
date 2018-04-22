using CoreGraphics;
using OpenTK;

namespace PracticeOpenGL.Source.Framework.Implement.Utility
{
    public static class DetectChecker
    {
        public static bool PointInRectangle(CGRect rect, Vector2 point)
        {
            return ((rect.Left <= point.X && rect.Right >= point.X) &&
                    // 左上が0の Top < Bottom なので判定を逆にする
                    //(rect.Bottom <= point.Y && rect.Top >= point.Y));
                    (rect.Top <= point.Y && rect.Bottom >= point.Y));
        }
    }
}
