using PracticeOpenGL.Source.Framework;

namespace PracticeOpenGL.Source.Workspace.GLGameTest
{
    public class GLGameTest : GLGame
    {
        public override Screen GetStartScreen()
        {
            //return new Draw2DTestScreen(this);
            //return new TouchTestScreen(this);
            return new StaticLinkTestScreen(this);
        }
    }
}
