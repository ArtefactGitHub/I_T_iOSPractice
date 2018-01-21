using System;
using System.Diagnostics;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class GLProgramParameter
    {
        public GLProgram GLProgram { get { return program; } }

        public int PositionAttribute { get { return positionAttribute; } }
        public int TextureCoordinateAttribute { get { return textureCoordinateAttribute; } }
        public int MatrixUniform { get { return matrixUniform; } }
        public int TextureUniform { get { return textureUniform; } }

        GLProgram program;
        int positionAttribute,
            textureCoordinateAttribute,
            matrixUniform,
            textureUniform;

        public void Use()
        {
            program.Use();
        }

        public GLProgramParameter(string vShaderFileName, string fShaderFileName)
        {
            program = new GLProgram(vShaderFileName, fShaderFileName);

            program.AddAttribute("position");
            program.AddAttribute("textureCoordinates");

            if (!program.Link())
            {
                Debug.WriteLine("Link failed.");
                Debug.WriteLine(string.Format("Program Log: {0}", program.ProgramLog()));
                Debug.WriteLine(string.Format("Fragment Log: {0}", program.FragmentShaderLog()));
                Debug.WriteLine(string.Format("Vertex Log: {0}", program.VertexShaderLog()));

                program = null;
                return;
            }

            positionAttribute = program.GetAttributeIndex("position");
            textureCoordinateAttribute = program.GetAttributeIndex("textureCoordinates");
            matrixUniform = program.GetUniformIndex("matrix");
            textureUniform = program.GetUniformIndex("texture");
        }
    }
}
