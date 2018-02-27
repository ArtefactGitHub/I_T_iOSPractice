using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using PracticeOpenGL.Source.Framework.Interface;

namespace PracticeOpenGL.Source.Framework.Implement.Input
{
    public class TouchHandler : ITouchHandler
    {
        //const int TOUCH_COUNT_MAX = 10;

        //List<bool> m_IsTouched = new List<bool>();

        //List<int> m_TouchX = new List<int>();

        //List<int> m_TouchY = new List<int>();

        Pool<TouchEvent> m_TouchEventPool;

        //List<TouchEvent> m_TouchEvents = new List<TouchEvent>();

        //List<TouchEvent> m_TouchEventsBuffer = new List<TouchEvent>();

        Dictionary<IntPtr, TouchEvent> m_TouchEvents = new Dictionary<IntPtr, TouchEvent>();

        Dictionary<IntPtr, TouchEvent> m_TouchEventsBuffer = new Dictionary<IntPtr, TouchEvent>();

        float m_ScaleX;

        float m_ScaleY;

        #region TouchEventFactory
        public class TouchEventFactory : PoolObjectFactory<TouchEvent>
        {
            public TouchEvent CreateObject()
            {
                return new TouchEvent();
            }
        }
        #endregion

        public TouchHandler(float scaleX, float scaleY)
        {
            m_TouchEventPool = new Pool<TouchEvent>(new TouchEventFactory(), 100);

            this.m_ScaleX = scaleX;
            this.m_ScaleY = scaleY;
        }

        public List<TouchEvent> GetTouchEvents()
        {
            lock (this)
            {
                var keys = m_TouchEvents.Keys;
                foreach (var key in keys)
                {
                    m_TouchEventPool.Free(m_TouchEvents[key]);
                }
                m_TouchEvents.Clear();

                foreach (var data in m_TouchEventsBuffer)
                {
                    m_TouchEvents.Add(data.Key, data.Value);
                }
                m_TouchEventsBuffer.Clear();

                return m_TouchEvents.Values.ToList();
            }
        }

        public void OnTouchEvent(IntPtr handle, float x, float y, MotionEvent motion)
        {
            lock (this)
            {
                switch (motion)
                {
                    case MotionEvent.ACTION_DOWN:
                        //case MotionEvent.ACTION_POINTER_DOWN:
                        SetupTouchEvent(handle, x, y, TouchEvent.TOUCH_DOWN, true);
                        break;

                    case MotionEvent.ACTION_UP:
                    //case MotionEvent.ACTION_POINTER_UP:
                    case MotionEvent.ACTION_CANCEL:
                        SetupTouchEvent(handle, x, y, TouchEvent.TOUCH_UP, false);
                        break;

                    case MotionEvent.ACTION_MOVE:
                        SetupTouchEvent(handle, x, y, TouchEvent.TOUCH_DRAGGED, true);
                        break;
                }
            }
        }

        //public int GetTouchX(int pointerId)
        public int GetTouchX(IntPtr pointer)
        {
            lock (this)
            {
                if (m_TouchEventsBuffer.ContainsKey(pointer))
                {
                    return m_TouchEventsBuffer[pointer].X;
                }

                return 0;
            }
        }

        //public int GetTouchY(int pointer)
        public int GetTouchY(IntPtr pointer)
        {
            lock (this)
            {
                if (m_TouchEventsBuffer.ContainsKey(pointer))
                {
                    return m_TouchEventsBuffer[pointer].Y;
                }

                return 0;
            }
        }

        //public bool IsTouchDown(int pointerId)
        public bool IsTouchDown(IntPtr pointer)
        {
            lock (this)
            {
                if (m_TouchEventsBuffer.ContainsKey(pointer))
                {
                    return m_TouchEventsBuffer[pointer].IsTouched;
                }

                return false;
            }
        }

        void SetupTouchEvent(IntPtr handle, float x, float y, int touchEventType, bool isTouched)
        {
            TouchEvent touchEvent;
            // バッファに取得済み
            if (m_TouchEventsBuffer.ContainsKey(handle))
            {
                touchEvent = m_TouchEventsBuffer[handle];
            }
            else
            {
                touchEvent = m_TouchEventPool.NewObject();
                m_TouchEventsBuffer.Add(handle, touchEvent);
            }

            touchEvent.Pointer = handle;
            touchEvent.Type = touchEventType;
            touchEvent.X = (int)(x * m_ScaleX);
            touchEvent.Y = (int)(y * m_ScaleY);
            touchEvent.IsTouched = isTouched;
        }
    }
}
