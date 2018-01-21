namespace PracticeOpenGL.Source.Framework
{
    public abstract class Screen
    {
        protected readonly GLGame m_Game;

        public Screen(GLGame game)
        {
            this.m_Game = game;
        }

        public abstract void Update(float deltaTime);

        public abstract void Present(float deltaTime);

        public abstract void Pause();

        public abstract void Resume();

        public abstract void Dispose();
    }
}
