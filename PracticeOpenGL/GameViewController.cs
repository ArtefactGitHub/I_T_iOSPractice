using System;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using GLKit;
using OpenGLES;
using PracticeOpenGL.Source.Framework;
using PracticeOpenGL.Source.Framework.Implement;
using PracticeOpenGL.Source.Framework.Implement.Debugs;
using PracticeOpenGL.Source.Framework.Implement.Input;
using PracticeOpenGL.Source.Framework.Interface;
using PracticeOpenGL.Source.Workspace.GLGameTest;
using UIKit;

namespace PracticeOpenGL
{
    [Register("GameViewController")]
    public class GameViewController : GLKViewController//, IGLKViewDelegate
    {
        EAGLContext m_Context { get; set; }

        GLGame m_Activity { get; set; }

#if DEBUG
        /** 表示領域に対するデバッグログビューアーの幅の割合 */
        float DEBUG_LOG_VIEWER_WIDTH_RATIO = (2.0f / 3.0f);
        /** 表示領域に対するデバッグログビューアーの高さの割合 */
        float DEBUG_LOG_VIEWER_HEIGHT_RATIO = (1.0f / 2.0f);
        /** デバッグログビューアーの行数 */
        int DEBUG_LOG_VIEWER_MAX_TEXT_COUNT = 40;
        /** デバッグログビューアーの背景色 */
        UIColor DEBUG_LOG_VIEWER_BACKGROUND_COLOR = new UIColor(0.5f, 0.5f, 0.5f, 0.5f);
#endif

        public GameViewController() { }

        [Export("initWithCoder:")]
        public GameViewController(NSCoder coder) : base(coder)
        {
        }

        public override void LoadView()
        {
            base.LoadView();

#if DEBUG
            var view = View;
            // デバッグログビューアーの作成
            var debugLogViewer = DebugLogViewer.CreateView(
                0, 0,
                (float)(view.Bounds.Width * DEBUG_LOG_VIEWER_WIDTH_RATIO), (float)(view.Bounds.Height * DEBUG_LOG_VIEWER_HEIGHT_RATIO),
                DEBUG_LOG_VIEWER_MAX_TEXT_COUNT,
                DEBUG_LOG_VIEWER_BACKGROUND_COLOR,
                UITextAlignment.Left);
            view.AddSubview(debugLogViewer);
#endif
        }

        public override void ViewDidLoad()
        {
            DebugLogViewer.WriteLine("ViewDidLoad()");

            base.ViewDidLoad();

            m_Context = new EAGLContext(EAGLRenderingAPI.OpenGLES2);

            if (m_Context == null)
            {
                DebugLogViewer.WriteLine("Failed to create ES context");
            }

            var view = (GLKView)View;
            view.Context = m_Context;
            view.DrawableColorFormat = GLKViewDrawableColorFormat.RGBA8888;
            view.DrawableDepthFormat = GLKViewDrawableDepthFormat.Format24;
            // フレームバッファがバインドされていないので、
            // フレームバッファ関連のOpenGL関数を使うにはこの方法で事前にバインドしておく
            view.BindDrawable();

            DebugLogViewer.WriteLine(string.Format("View.Drawable ({0}, {1})", view.DrawableWidth, view.DrawableHeight));
            DebugLogViewer.WriteLine(string.Format("view.Bounds {0}", view.Bounds));

            SetupGL();

            m_Activity = new GLGameTest();
            m_Activity.OnSurfaceCreated(view);

            RegisterTouchEvent(m_Activity);
        }

        #region RegisterEvent

        TouchHandler m_TouchHandler;
#if false

        protected void RegisterTouchEvent()
        {
            m_TouchHandler = new TouchHandler();
            IOSInput.Instance.Initialize(m_TouchHandler);

            View.AddGestureRecognizer(new UITapGestureRecognizer((UITapGestureRecognizer obj) =>
            {
                CGPoint p = obj.LocationInView(View);
                m_TouchHandler.OnTouchEvent((float)p.X, (float)p.Y);
            })
            {
                //NumberOfTapsRequired = 2
            });
        }
#else

        protected void RegisterTouchEvent(GLGame game)
        {
            m_TouchHandler = new TouchHandler(1.0f, 1.0f);
            //IOSInput.Instance.Initialize(m_TouchHandler);
            IOSInput input = (IOSInput)game.GetInput();
            input.Initialize(m_TouchHandler);

            // マルチタッチを有効にする
            View.MultipleTouchEnabled = true;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            DebugLogViewer.WriteLine(string.Format("TouchesBegan"));

            foreach (UITouch touch in touches.ToArray<UITouch>())
            //foreach (UITouch touch in evt.AllTouches.ToArray<UITouch>())
            {
                SendHandlerOnTouchEvent(touch, View, MotionEvent.ACTION_DOWN);
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            DebugLogViewer.WriteLine(string.Format("TouchesMoved"));

            foreach (UITouch touch in touches.ToArray<UITouch>())
            //foreach (UITouch touch in evt.AllTouches.ToArray<UITouch>())
            {
                SendHandlerOnTouchEvent(touch, View, MotionEvent.ACTION_MOVE);
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            DebugLogViewer.WriteLine(string.Format("TouchesCancelled"));

            foreach (UITouch touch in touches.ToArray<UITouch>())
            //foreach (UITouch touch in evt.AllTouches.ToArray<UITouch>())
            {
                SendHandlerOnTouchEvent(touch, View, MotionEvent.ACTION_CANCEL);
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            DebugLogViewer.WriteLine(string.Format("TouchesEnded"));

            foreach (UITouch touch in touches.ToArray<UITouch>())
            //foreach (UITouch touch in evt.AllTouches.ToArray<UITouch>())
            {
                SendHandlerOnTouchEvent(touch, View, MotionEvent.ACTION_NONE);
            }
        }

        void SendHandlerOnTouchEvent(UITouch touch, UIView view, MotionEvent motion)
        {
            CGPoint point = touch.LocationInView(view);
            DebugLogViewer.WriteLine(string.Format("  Id[{0}] / Pos[{1}] / Motion[{2}]", touch.Handle, point, motion));
            m_TouchHandler.OnTouchEvent(touch.Handle, (float)point.X, (float)point.Y, motion);
        }

#endif

        #endregion

        protected override void Dispose(bool disposing)
        {
            DebugLogViewer.WriteLine("Dispose() : " + disposing);

            base.Dispose(disposing);

            TearDownGL();

            if (EAGLContext.CurrentContext == m_Context)
                EAGLContext.SetCurrentContext(null);
        }

        public override void DidReceiveMemoryWarning()
        {
            Debug.WriteLine("DidReceiveMemoryWarning");

            base.DidReceiveMemoryWarning();

            if (IsViewLoaded && View.Window == null)
            {
                View = null;

                TearDownGL();

                if (EAGLContext.CurrentContext == m_Context)
                {
                    EAGLContext.SetCurrentContext(null);
                }
            }

            // Dispose of any resources that can be recreated.
        }

        #region PrefersStatusBarHidden
        public override bool PrefersStatusBarHidden()
        {
            Debug.WriteLine("PrefersStatusBarHidden()");

            return true;
        }
        #endregion

        void SetupGL()
        {
            EAGLContext.SetCurrentContext(m_Context);
        }

        void TearDownGL()
        {
            EAGLContext.SetCurrentContext(m_Context);
            //GL.DeleteBuffers(1, ref vertexBuffer);
            //GL.Oes.DeleteVertexArrays(1, ref vertexArray);

            //if (program > 0)
            //{
            //    GL.DeleteProgram(program);
            //    program = 0;
            //}

            // 終了させる
            if (m_Activity != null)
            {
                m_Activity.IsFinishing = true;
                m_Activity.OnPause();
            }
        }

        #region GLKViewController methods

        public override void Update()
        {
        }

        public override void DrawInRect(GLKView view, CoreGraphics.CGRect rect)
        {
            m_Activity.OnDrawFrame();
        }

        #endregion
    }
}
