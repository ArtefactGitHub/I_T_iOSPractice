using System;
using System.Diagnostics;
using System.IO;
using CoreGraphics;
using Foundation;
using OpenTK.Graphics.ES20;
using UIKit;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public class Texture
    {
        #region property

        public int TextureId { get { return m_TextureId; } }

        public int Width { get; private set; }

        public int Height { get; private set; }

        int m_TextureId = 0;

        GLGraphics m_GLGraphics;

        string m_FileName = string.Empty;

        #endregion

        public Texture(GLGame glGame, string fileName)
        {
            this.m_GLGraphics = glGame.GetGLGraphics();
            this.m_FileName = fileName;

            Load();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureId);
        }

        public void UnBind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            if (TextureId != 0)
            {
                Bind();
                GL.DeleteTexture(TextureId);
            }
        }

        public void Reload()
        {
            Width = 0;
            Height = 0;

            Load();
        }

        #region private function

        void Load()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);

            GL.Hint(HintTarget.GenerateMipmapHint, HintMode.Nicest);

            //GL.GenTextures(1, out m_TextureId);
            //GL.BindTexture(TextureTarget.Texture2D, m_TextureId);
            GenAndBindTexture();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);

            //TODO Remove the Substring method if you don't support iOS versions prior to iOS 6.
            //string extension = Path.GetExtension(m_FileName).Substring(1);
            string extension = Path.GetExtension(m_FileName);
            string baseFilename = Path.GetFileNameWithoutExtension(m_FileName);

            // ディレクトリ構成の場合、パスを変更する
            string directoryName = Path.GetDirectoryName(m_FileName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                baseFilename = string.Format("{0}/{1}", directoryName, baseFilename);
            }

            string path = NSBundle.MainBundle.PathForResource(baseFilename, extension);
            NSData texData = NSData.FromFile(path);

            UIImage image = UIImage.LoadFromData(texData);
            if (image == null)
                return;

            nint width = image.CGImage.Width;
            nint height = image.CGImage.Height;

            CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
            byte[] imageData = new byte[height * width * 4];
            CGContext context = new CGBitmapContext(imageData, width, height, 8, 4 * width, colorSpace,
                                                      CGBitmapFlags.PremultipliedLast | CGBitmapFlags.ByteOrder32Big);

            context.TranslateCTM(0, height);
            context.ScaleCTM(1, -1);
            colorSpace.Dispose();
            context.ClearRect(new CGRect(0, 0, width, height));
            context.DrawImage(new CGRect(0, 0, width, height), image.CGImage);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)width, (int)height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageData);
            context.Dispose();

            Width = (int)width;
            Height = (int)height;
        }

        void GenAndBindTexture()
        {
            if (TextureId == 0)
            {
                GL.GenTextures(1, out m_TextureId);
                Bind();
            }
            else
            {
                Debug.WriteLine("TextureId is already used");
            }
        }

        #endregion
    }
}
