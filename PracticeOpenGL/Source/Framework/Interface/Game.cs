using PracticeOpenGL.Source.Framework.Interface;

namespace PracticeOpenGL.Source.Framework
{
    public interface IGame
    {
        GLGraphics GetGLGraphics();

        IInput GetInput();

        void SetScreen(Screen screen);

        Screen GetCurrentScreen();

        Screen GetStartScreen();
    }
}
