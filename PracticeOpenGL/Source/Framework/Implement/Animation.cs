using System;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class Animation
    {
        public const int ANIMATION_LOOPING = 0;
        public const int ANIMATION_NONLOOPING = 1;

        readonly TextureRegion[] m_KeyFrames;

        readonly float m_FrameDuration;

        public Animation(float frameDuration, TextureRegion[] keyFrames)
        {
            this.m_FrameDuration = frameDuration;
            this.m_KeyFrames = keyFrames;
        }

        public TextureRegion GetKeyFrame(float stateTime, int mode)
        {
            int frameNumber = (int)(stateTime / m_FrameDuration);

            if (mode == ANIMATION_NONLOOPING)
            {
                frameNumber = Math.Min(m_KeyFrames.Length - 1, frameNumber);
            }
            else
            {
                frameNumber = frameNumber % m_KeyFrames.Length;
            }

            return m_KeyFrames[frameNumber];
        }
    }
}
