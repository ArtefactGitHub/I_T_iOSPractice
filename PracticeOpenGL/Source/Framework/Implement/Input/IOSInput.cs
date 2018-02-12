using System;
using System.Collections.Generic;
using PracticeOpenGL.Source.Framework.Implement.Input;
using PracticeOpenGL.Source.Framework.Interface;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class IOSInput : IInput
    {
        ITouchHandler m_TouchHandler;

        public IOSInput()
        {
            m_TouchHandler = new TouchHandler();
        }

        public bool IsTouchDown(int pointerId)
        {
            return m_TouchHandler.IsTouchDown(pointerId);
        }

        public List<TouchEvent> GetTouchEvents()
        {
            return m_TouchHandler.GetTouchEvents();
        }

        public int GetTouchX(int pointerId)
        {
            return m_TouchHandler.GetTouchX(pointerId);
        }

        public int GetTouchY(int pointerId)
        {
            return m_TouchHandler.GetTouchY(pointerId);
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
