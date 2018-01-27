using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.ES20;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class Vertices
    {
        //GLGraphics m_GLGraphics;
        //bool m_HasColor;
        //bool m_HasTexCoords;
        //int m_VertexSize;
        Vector3[] m_Vertices;
        ushort[] m_Indecies;
        TextureCoord[] m_TextureCoordinates;
        GLProgramParameter m_ProgramParam;

        //public Vertices(GLGraphics glGraphics, int maxVertices, int maxIndices,
        //               bool hasColor, bool hasTexCoords)
        //{
        //    this.m_GLGraphics = glGraphics;
        //    this.m_HasColor = hasColor;
        //    this.m_HasTexCoords = hasTexCoords;
        //    this.m_VertexSize = (2 + (hasColor ? 4 : 0) + (hasTexCoords ? 2 : 0)) * 4;
        //}

        public Vertices(Vector3[] vertices, ushort[] indecies, TextureCoord[] textureCoordinates, GLProgramParameter programParam)
        {
            this.m_Vertices = vertices;
            this.m_Indecies = indecies;
            this.m_TextureCoordinates = textureCoordinates;
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

        public void Draw()
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

            GL.DrawElements(BeginMode.Triangles, m_Indecies.Length, DrawElementsType.UnsignedShort, m_Indecies);

            GL.DisableVertexAttribArray(m_ProgramParam.PositionAttribute);
            GL.DisableVertexAttribArray(m_ProgramParam.TextureCoordinateAttribute);
        }
    }
}
