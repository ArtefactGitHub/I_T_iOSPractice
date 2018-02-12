using System.Collections.Generic;
using PracticeOpenGL.Source.Framework.Interface;

namespace PracticeOpenGL.Source.Framework.Implement.Input
{
    public interface ITouchHandler
    {
        bool IsTouchDown(int pointerId);

        int GetTouchX(int pointerId);

        int GetTouchY(int pointerId);

        List<TouchEvent> GetTouchEvents();

        bool OnTouch();
    }
}
