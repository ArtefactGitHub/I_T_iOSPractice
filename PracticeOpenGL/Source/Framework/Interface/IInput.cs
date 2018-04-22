
using System;
using System.Collections.Generic;

namespace PracticeOpenGL.Source.Framework.Interface
{
    public interface IInput
    {
        bool IsKeyPressed(int keyCode);

        bool IsTouchDown(IntPtr pointer);

        int GetTouchX(IntPtr pointer);
        int GetTouchY(IntPtr pointer);

        float GetAccelX();
        float GetAccelY();
        float GetAccelZ();

        List<KeyEvent> GetKeyEvents();
        List<TouchEvent> GetTouchEvents();
    }

    public class KeyEvent
    {
        public const int KEY_DOWN = 0;
        public const int KEY_UP = 1;

        public int m_Type;
        public int m_KeyCode;
        public char m_KeyChar;
    }

    public class TouchEvent
    {
        public const int TOUCH_DOWN = 0;
        public const int TOUCH_UP = 1;
        public const int TOUCH_DRAGGED = 2;

        public int Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public IntPtr Pointer { get; set; }

        public bool IsTouched { get; set; }
    }
}
