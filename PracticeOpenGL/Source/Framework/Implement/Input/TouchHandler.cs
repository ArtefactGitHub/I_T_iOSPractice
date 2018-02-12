using System;
using System.Collections.Generic;
using PracticeOpenGL.Source.Framework.Interface;

namespace PracticeOpenGL.Source.Framework.Implement.Input
{
    public class TouchHandler : ITouchHandler
    {
        const int TOUCH_COUNT_MAX = 10;

        List<bool> m_IsTouched = new List<bool>();

        List<int> m_TouchX = new List<int>();

        List<int> m_TouchY = new List<int>();

        Pool<TouchEvent> m_TouchEventPool;

        List<TouchEvent> m_TouchEvents = new List<TouchEvent>();

        List<TouchEvent> m_TouchEventsBuffer = new List<TouchEvent>();

        float m_ScaleX;

        float m_ScaleY;

        public TouchHandler()
        {
        }

        public List<TouchEvent> GetTouchEvents()
        {
            throw new NotImplementedException();
        }

        public int GetTouchX(int pointerId)
        {
            throw new NotImplementedException();
        }

        public int GetTouchY(int pointerId)
        {
            throw new NotImplementedException();
        }

        public bool IsTouchDown(int pointerId)
        {
            throw new NotImplementedException();
        }

        public bool OnTouch()
        {
            throw new NotImplementedException();
        }
    }
}
