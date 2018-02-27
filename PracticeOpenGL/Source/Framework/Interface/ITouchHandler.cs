using System;
using System.Collections.Generic;
using PracticeOpenGL.Source.Framework.Interface;

namespace PracticeOpenGL.Source.Framework.Implement.Input
{
    public interface ITouchHandler
    {
        bool IsTouchDown(IntPtr pointer);

        int GetTouchX(IntPtr pointer);

        int GetTouchY(IntPtr pointer);

        List<TouchEvent> GetTouchEvents();

        //bool OnTouch();
    }
}
