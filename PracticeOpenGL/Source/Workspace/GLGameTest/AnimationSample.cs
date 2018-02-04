using System;
namespace PracticeOpenGL.Source.Workspace.GLGameTest
{
    public class AnimationSample
    {
        public float m_Time = 0f;

        Random m_Random;

        public AnimationSample()
        {
            m_Random = new Random();

            // 初期フレームをランダムにする
            this.m_Time = (float)m_Random.Next(0, 100) / 100.0f * 10.0f;
        }

        public void Update(float deltaTime)
        {
            m_Time += deltaTime;
        }
    }
}
