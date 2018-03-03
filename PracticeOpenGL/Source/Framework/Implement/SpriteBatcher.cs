using OpenTK;
using OpenTK.Graphics.ES20;

namespace PracticeOpenGL.Source.Framework.Implement
{
    /// <summary>
    /// スプライト描画のバッチ処理を行うクラス
    /// 
    /// ・シェーダ単位でバッチ処理を行います
    /// </summary>
    public class SpriteBatcher
    {
        readonly GLProgramParameter m_GlProgramParam;
        readonly Vector3[] m_VerticesVec;
        readonly TextureCoord[] m_TextureCoordinates;
        int m_BufferIndex;
        readonly Vertices m_Vertices;
        int m_NumSprites;
        int m_NumVertices;

        public SpriteBatcher(GLGraphics glGraphics, GLProgramParameter glProgramParam, int maxSprites)
        {
            m_NumVertices = maxSprites * 4;

            this.m_GlProgramParam = glProgramParam;
            this.m_VerticesVec = new Vector3[m_NumVertices];
            this.m_TextureCoordinates = new TextureCoord[m_NumVertices];
            this.m_Vertices = new Vertices();
            this.m_BufferIndex = 0;
            this.m_NumSprites = 0;

            ushort[] indices = new ushort[maxSprites * 6];
            int len = indices.Length;
            ushort j = 0;

            for (int i = 0; i < len; i += 6, j += 4)
            {
                indices[i + 0] = (ushort)(j + 0);
                indices[i + 1] = (ushort)(j + 1);
                indices[i + 2] = (ushort)(j + 2);
                indices[i + 3] = (ushort)(j + 2);
                indices[i + 4] = (ushort)(j + 3);
                indices[i + 5] = (ushort)(j + 0);
            }

            m_Vertices.SetProgram(glProgramParam);
            m_Vertices.SetIndecies(indices);

            for (int i = 0; i < m_NumVertices; i++)
            {
                m_VerticesVec[i] = new Vector3();
                m_TextureCoordinates[i] = new TextureCoord();
            }
        }

        public void BeginBatch(Texture texture)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            texture.Bind();
            GL.Uniform1(m_GlProgramParam.TextureUniform, 0);

            m_NumSprites = 0;
            m_BufferIndex = 0;
        }

        public void EndBatch()
        {
            m_Vertices.SetTextureCoordinates(m_TextureCoordinates, m_BufferIndex);
            m_Vertices.SetVertices(m_VerticesVec, m_BufferIndex);
            m_Vertices.Bind();
            m_Vertices.Draw(m_NumSprites * 6);
            m_Vertices.UnBind();
        }

        public void DrawSprite(float x, float y, float width, float height, TextureRegion region)
        {
#if true
            // 左上を原点とする
            float x1 = x;
            float y1 = y;
            float x2 = x + width;
            float y2 = y + height;
#else
            // 中央を原点とする
            float halfWidth = width / 2.0f;
            float halfHeight = height / 2.0f;

            float x1 = x - halfWidth;
            float y1 = y - halfHeight;
            float x2 = x + halfWidth;
            float y2 = y + halfHeight;
#endif

            m_VerticesVec[m_BufferIndex].X = x1;
            m_VerticesVec[m_BufferIndex].Y = y1;
            m_VerticesVec[m_BufferIndex].Z = 0f;
            m_TextureCoordinates[m_BufferIndex].S = region.UV1.X;
            m_TextureCoordinates[m_BufferIndex].T = region.UV1.Y;
            m_BufferIndex++;

            m_VerticesVec[m_BufferIndex].X = x1;
            m_VerticesVec[m_BufferIndex].Y = y2;
            m_VerticesVec[m_BufferIndex].Z = 0f;
            m_TextureCoordinates[m_BufferIndex].S = region.UV1.X;
            m_TextureCoordinates[m_BufferIndex].T = region.UV2.Y;
            m_BufferIndex++;

            m_VerticesVec[m_BufferIndex].X = x2;
            m_VerticesVec[m_BufferIndex].Y = y2;
            m_VerticesVec[m_BufferIndex].Z = 0f;
            m_TextureCoordinates[m_BufferIndex].S = region.UV2.X;
            m_TextureCoordinates[m_BufferIndex].T = region.UV2.Y;
            m_BufferIndex++;

            m_VerticesVec[m_BufferIndex].X = x2;
            m_VerticesVec[m_BufferIndex].Y = y1;
            m_VerticesVec[m_BufferIndex].Z = 0f;
            m_TextureCoordinates[m_BufferIndex].S = region.UV2.X;
            m_TextureCoordinates[m_BufferIndex].T = region.UV1.Y;
            m_BufferIndex++;

            m_NumSprites++;
        }
    }
}
