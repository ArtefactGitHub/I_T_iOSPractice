using System;
using System.Diagnostics;
using System.Threading;
using GLKit;

namespace PracticeOpenGL.Source.Framework
{
    public abstract class GLGame : IGame
    {
        #region Enum

        public enum GLGameState
        {
            None,
            Initialized,
            Running,
            Paused,
            Finished,
            Idel
        }

        #endregion

        #region property

        public bool IsFinishing { protected get; set; }

        protected GLKView m_GLView;

        protected GLGraphics m_GLGraphics;

        protected Screen m_Screen;

        protected GLGameState m_State = GLGameState.Initialized;

        protected Object m_StateChanged = new object();

        protected Stopwatch m_Stopwatch = new Stopwatch();

        #endregion

        public virtual void OnResume()
        {
        }

        public void OnSurfaceCreated(GLKView glkView)
        {
            m_GLGraphics = new GLGraphics(glkView);
            Debug.WriteLine(string.Format("Width:{0} / Height:{1}", m_GLGraphics.GetWidth(), m_GLGraphics.GetHeight()));

            lock (m_StateChanged)
            {
                if (m_State == GLGameState.Initialized)
                {
                    m_Screen = GetStartScreen();
                }

                m_State = GLGameState.Running;
                m_Screen.Resume();
            }
        }

        public void OnDrawFrame()
        {
            GLGameState state = GLGameState.None;
            lock (m_StateChanged)
            {
                state = this.m_State;
            }

            if (state == GLGameState.Running)
            {
                m_Stopwatch.Stop();

                // 前回計測時間からの経過時間の取得
                float deltaTime = m_Stopwatch.ElapsedTicks / 10000000.0f;

                m_Stopwatch.Reset();
                m_Stopwatch.Start();

                m_Screen.Update(deltaTime);
                m_Screen.Present(deltaTime);
            }

            if (m_State == GLGameState.Paused)
            {
                m_Screen.Pause();
                lock (m_StateChanged)
                {
                    this.m_State = GLGameState.Idel;
                    NotifyAll();
                }
            }

            if (m_State == GLGameState.Finished)
            {
                m_Screen.Pause();
                m_Screen.Dispose();

                lock (m_StateChanged)
                {
                    this.m_State = GLGameState.Idel;
                    NotifyAll();
                }
            }
        }

        public void OnPause()
        {
            lock (m_StateChanged)
            {
                if (IsFinishing)
                {
                    m_State = GLGameState.Finished;
                }
                else
                {
                    m_State = GLGameState.Paused;
                }

                while (true)
                {
                    try
                    {
                        Monitor.Wait(m_StateChanged);
                        break;
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Debug.WriteLine("ERROR : " + e);
                    }
                }
            }
        }

        public GLGraphics GetGLGraphics() { return m_GLGraphics; }
        //public Input GetInput() { return null; }

        public abstract Screen GetStartScreen();

        public void SetScreen(Screen screen)
        {
            if (screen == null)
            {
                throw new ArgumentException();
            }

            this.m_Screen.Pause();
            this.m_Screen.Dispose();
            screen.Resume();
            screen.Update(0);
            this.m_Screen = screen;
        }

        public Screen GetCurrentScreen()
        {
            return m_Screen;
        }

        #region private function

        protected long GetNanoTime()
        {
            return DateTime.Now.Ticks;
        }

        protected void NotifyAll()
        {
            Monitor.PulseAll(m_StateChanged);
        }

        #endregion
    }
}
