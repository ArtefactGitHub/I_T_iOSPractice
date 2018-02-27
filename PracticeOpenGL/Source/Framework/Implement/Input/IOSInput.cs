using System;
using System.Collections.Generic;
using PracticeOpenGL.Source.Framework.Implement.Input;
using PracticeOpenGL.Source.Framework.Interface;

namespace PracticeOpenGL.Source.Framework.Implement
{
    #region MotionEvent

    public enum MotionEvent
    {
        ACTION_NONE = -1,
        ACTION_MOVE = 0,
        ACTION_UP,
        ACTION_DOWN,
        ACTION_CANCEL,
        //ACTION_OUTSIDE,
    }

    #endregion

    public class IOSInput : IInput
    {
        #region singleton

        //public static IOSInput Instance
        //{
        //    get
        //    {
        //        if (m_Instance == null)
        //        {
        //            m_Instance = new IOSInput();
        //        }
        //        return m_Instance;
        //    }
        //}

        //static IOSInput m_Instance;

        //IOSInput() { }

        #endregion

        ITouchHandler m_TouchHandler;

        public void Initialize(ITouchHandler touchHandler)
        {
            m_TouchHandler = touchHandler;
        }

        public bool IsTouchDown(IntPtr pointer)
        {
            return m_TouchHandler.IsTouchDown(pointer);
        }

        public List<TouchEvent> GetTouchEvents()
        {
            return m_TouchHandler.GetTouchEvents();
        }

        public int GetTouchX(IntPtr pointer)
        {
            return m_TouchHandler.GetTouchX(pointer);
        }

        public int GetTouchY(IntPtr pointer)
        {
            return m_TouchHandler.GetTouchY(pointer);
        }

        public float GetAccelX()
        {
            throw new NotImplementedException();
        }

        public float GetAccelY()
        {
            throw new NotImplementedException();
        }

        public float GetAccelZ()
        {
            throw new NotImplementedException();
        }

        public List<KeyEvent> GetKeyEvents()
        {
            throw new NotImplementedException();
        }

        public bool IsKeyPressed(int keyCode)
        {
            throw new NotImplementedException();
        }
    }
}
