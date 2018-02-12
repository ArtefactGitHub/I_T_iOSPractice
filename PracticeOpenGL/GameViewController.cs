using System.Diagnostics;
using Foundation;
using GLKit;
using OpenGLES;
using PracticeOpenGL.Source.Framework;
using PracticeOpenGL.Source.Framework.Implement.Debugs;
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

        public override void ViewDidLoad()
        {
            Debug.WriteLine("ViewDidLoad()");

            base.ViewDidLoad();

            m_Context = new EAGLContext(EAGLRenderingAPI.OpenGLES2);

            if (m_Context == null)
            {
                Debug.WriteLine("Failed to create ES context");
            }

            var view = (GLKView)View;
            view.Context = m_Context;
            view.DrawableColorFormat = GLKViewDrawableColorFormat.RGBA8888;
            view.DrawableDepthFormat = GLKViewDrawableDepthFormat.Format24;
            // フレームバッファがバインドされていないので、
            // フレームバッファ関連のOpenGL関数を使うにはこの方法で事前にバインドしておく
            view.BindDrawable();

#if DEBUG
            // デバッグログビューアーの作成
            var debugLogViewer = DebugLogViewer.CreateView(
                0, 0,
                (float)(view.Bounds.Width * DEBUG_LOG_VIEWER_WIDTH_RATIO), (float)(view.Bounds.Height * DEBUG_LOG_VIEWER_HEIGHT_RATIO),
                DEBUG_LOG_VIEWER_MAX_TEXT_COUNT,
                DEBUG_LOG_VIEWER_BACKGROUND_COLOR,
                UITextAlignment.Left);
            view.AddSubview(debugLogViewer);
#endif

            SetupGL();

            m_Activity = new GLGameTest();
            m_Activity.OnSurfaceCreated(view);
        }

        protected override void Dispose(bool disposing)
        {
            Debug.WriteLine("Dispose() : " + disposing);

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
