using OpenTK.Graphics.ES20;
using PracticeOpenGL.Source.Framework;
using PracticeOpenGL.Source.Framework.Implement.Debugs;

namespace PracticeOpenGL.Source.Workspace.GLGameTest
{
    public class StaticLinkTestScreen : GLScreen
    {
        #region property

        GLGraphics m_GLGraphics;

        #endregion

        public StaticLinkTestScreen(GLGame game) : base(game)
        {
            m_GLGraphics = game.GetGLGraphics();
        }

        public override void Dispose()
        {
            DebugLogViewer.WriteLine("Dispose");
        }

        public override void Pause()
        {
            DebugLogViewer.WriteLine("Pause");
        }

        public override void Resume()
        {
            DebugLogViewer.WriteLine("Resume");

            GL.ClearColor(0.7f, 0.83f, 0.86f, 1f);
        }

        public override void Update(float deltaTime)
        {
            var d = CFunctions.clib_add_internal(1, 2);
            DebugLogViewer.WriteLine("=== d = " + d);
        }

        public override void Present(float deltaTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
