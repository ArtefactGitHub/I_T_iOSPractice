namespace PracticeOpenGL.Source.Framework
{
    public interface IGame
    {
        //Input GetInput();

        GLGraphics GetGLGraphics();

        void SetScreen(Screen screen);

        Screen GetCurrentScreen();

        Screen GetStartScreen();
    }
}
