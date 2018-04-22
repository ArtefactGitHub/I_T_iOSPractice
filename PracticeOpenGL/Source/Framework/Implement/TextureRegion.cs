using System;
using OpenTK;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class TextureRegion
    {
        public Vector2 UV1 { get; private set; }

        public Vector2 UV2 { get; private set; }

        public Texture Texture { get; private set; }

        public TextureRegion(Texture texture, float x, float y, float width, float height)
        {
            this.UV1 = new Vector2(x / texture.Width, y / texture.Height);
            this.UV2 = new Vector2(UV1.X + width / texture.Width, UV1.Y + height / texture.Height);
            this.Texture = texture;
        }
    }
}
