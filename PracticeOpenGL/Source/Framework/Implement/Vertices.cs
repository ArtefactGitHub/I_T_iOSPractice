using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.ES20;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class Vertices
    {
        Vector3[] m_Vertices;

        ushort[] m_Indecies;

        TextureCoord[] m_TextureCoordinates;

        GLProgramParameter m_ProgramParam;

        public Vertices()
        {
        }

        public void SetProgram(GLProgramParameter programParam)
        {
            this.m_ProgramParam = programParam;
        }

        public void SetVertices(Vector3[] vertices)
        {
            this.m_Vertices = vertices;
        }

        public void SetIndecies(ushort[] indecies)
        {
            this.m_Indecies = indecies;
        }

        public void SetTextureCoordinates(TextureCoord[] textureCoordinates)
        {
            this.m_TextureCoordinates = textureCoordinates;
        }

        public void Bind()
        {
            if (m_ProgramParam == null)
            {
                Debug.WriteLine("program is invalid");
                return;
            }

            // 頂点座標
            GL.VertexAttribPointer(m_ProgramParam.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, m_Vertices);
            GL.EnableVertexAttribArray(m_ProgramParam.PositionAttribute);

            // テクスチャ座標
            GL.VertexAttribPointer(m_ProgramParam.TextureCoordinateAttribute, 2, VertexAttribPointerType.Float, false, 0, m_TextureCoordinates);
            GL.EnableVertexAttribArray(m_ProgramParam.TextureCoordinateAttribute);
        }

        public void UnBind()
        {
            if (m_ProgramParam == null)
            {
                Debug.WriteLine("program is invalid");
                return;
            }

            GL.DisableVertexAttribArray(m_ProgramParam.PositionAttribute);
            GL.DisableVertexAttribArray(m_ProgramParam.TextureCoordinateAttribute);
        }

        public void Draw()
        {
            GL.DrawElements(BeginMode.Triangles, m_Indecies.Length, DrawElementsType.UnsignedShort, m_Indecies);
        }
    }
}
