using System.Diagnostics;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class GLProgramParameter
    {
        public GLProgram GLProgram { get; private set; }

        public int PositionAttribute { get; private set; }
        public int NormalAttribute { get; private set; }
        public int TextureCoordinateAttribute { get; private set; }
        public int WorldMatrixUniform { get; private set; }
        public int ViewProjectionMatrixUniform { get; private set; }
        public int LightDirectionMatrixUniform { get; private set; }
        public int TextureUniform { get; private set; }

        public void Use()
        {
            GLProgram.Use();
        }

        public GLProgramParameter(string vShaderFileName, string fShaderFileName)
        {
            GLProgram = new GLProgram(vShaderFileName, fShaderFileName);

            GLProgram.AddAttribute("position");
            GLProgram.AddAttribute("normal");
            GLProgram.AddAttribute("textureCoordinates");

            if (!GLProgram.Link())
            {
                Debug.WriteLine("Link failed.");
                Debug.WriteLine(string.Format("GLProgram Log: {0}", GLProgram.ProgramLog()));
                Debug.WriteLine(string.Format("Fragment Log: {0}", GLProgram.FragmentShaderLog()));
                Debug.WriteLine(string.Format("Vertex Log: {0}", GLProgram.VertexShaderLog()));

                GLProgram = null;
                return;
            }

            PositionAttribute = GLProgram.GetAttributeIndex("position");
            NormalAttribute = GLProgram.GetAttributeIndex("normal");
            TextureCoordinateAttribute = GLProgram.GetAttributeIndex("textureCoordinates");
            WorldMatrixUniform = GLProgram.GetUniformIndex("world");
            ViewProjectionMatrixUniform = GLProgram.GetUniformIndex("viewProjection");
            LightDirectionMatrixUniform = GLProgram.GetUniformIndex("lightDirection");
            TextureUniform = GLProgram.GetUniformIndex("texture");
        }
    }
}
