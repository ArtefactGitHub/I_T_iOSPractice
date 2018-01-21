using System;
using System.Diagnostics;
using Foundation;
using GLKit;
using OpenTK.Graphics.ES20;

namespace PracticeOpenGL.Source.Framework
{
    public abstract class GLScreen : Screen
    {
        #region Enum

        enum Uniform
        {
            ModelViewProjection_Matrix,
            Normal_Matrix,
            Count
        }

        //enum Attribute
        //{
        //    Vertex,
        //    Normal,
        //    Count
        //}

        #endregion

        public GLScreen(GLGame game) : base(game)
        {
            SetupGL();
        }

        public override void Dispose()
        {
            Debug.WriteLine("Dispose");

            if (program > 0)
            {
                GL.DeleteProgram(program);
                program = 0;
            }
        }

        protected virtual void SetupGL()
        {
            LoadShaders();
        }

        #region Shader

        int[] uniforms = new int[(int)Uniform.Count];

        int program;

        protected virtual bool LoadShaders()
        {
            int vertShader, fragShader;

            // Create shader program.
            program = GL.CreateProgram();

            // Create and compile vertex shader.
            if (!CompileShader(ShaderType.VertexShader, LoadResource("Shader", "vsh"), out vertShader))
            {
                Console.WriteLine("Failed to compile vertex shader");
                return false;
            }
            // Create and compile fragment shader.
            if (!CompileShader(ShaderType.FragmentShader, LoadResource("Shader", "fsh"), out fragShader))
            {
                Console.WriteLine("Failed to compile fragment shader");
                return false;
            }

            // Attach vertex shader to program.
            GL.AttachShader(program, vertShader);

            // Attach fragment shader to program.
            GL.AttachShader(program, fragShader);

            // Bind attribute locations.
            // This needs to be done prior to linking.
            GL.BindAttribLocation(program, (int)GLKVertexAttrib.Position, "position");
            GL.BindAttribLocation(program, (int)GLKVertexAttrib.Normal, "normal");

            // Link program.
            if (!LinkProgram(program))
            {
                Console.WriteLine("Failed to link program: {0:x}", program);

                if (vertShader != 0)
                    GL.DeleteShader(vertShader);

                if (fragShader != 0)
                    GL.DeleteShader(fragShader);

                if (program != 0)
                {
                    GL.DeleteProgram(program);
                    program = 0;
                }
                return false;
            }

            // Get uniform locations.
            uniforms[(int)Uniform.ModelViewProjection_Matrix] = GL.GetUniformLocation(program, "modelViewProjectionMatrix");
            uniforms[(int)Uniform.Normal_Matrix] = GL.GetUniformLocation(program, "normalMatrix");

            // Release vertex and fragment shaders.
            if (vertShader != 0)
            {
                GL.DetachShader(program, vertShader);
                GL.DeleteShader(vertShader);
            }

            if (fragShader != 0)
            {
                GL.DetachShader(program, fragShader);
                GL.DeleteShader(fragShader);
            }

            return true;
        }

        protected virtual string LoadResource(string name, string type)
        {
            var path = NSBundle.MainBundle.PathForResource(name, type);
            return System.IO.File.ReadAllText(path);
        }

        protected virtual bool CompileShader(ShaderType type, string src, out int shader)
        {
            shader = GL.CreateShader(type);
            GL.ShaderSource(shader, src);
            GL.CompileShader(shader);

#if DEBUG
            int logLength = 0;
            GL.GetShader(shader, ShaderParameter.InfoLogLength, out logLength);
            if (logLength > 0)
            {
                Console.WriteLine("Shader compile log:\n{0}", GL.GetShaderInfoLog(shader));
            }
#endif

            int status = 0;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out status);
            if (status == 0)
            {
                GL.DeleteShader(shader);
                return false;
            }

            return true;
        }

        protected virtual bool LinkProgram(int prog)
        {
            GL.LinkProgram(prog);

#if DEBUG
            int logLength = 0;
            GL.GetProgram(prog, ProgramParameter.InfoLogLength, out logLength);
            if (logLength > 0)
                Console.WriteLine("Program link log:\n{0}", GL.GetProgramInfoLog(prog));
#endif
            int status = 0;
            GL.GetProgram(prog, ProgramParameter.LinkStatus, out status);
            return status != 0;
        }

        protected virtual bool ValidateProgram(int prog)
        {
            int logLength, status = 0;

            GL.ValidateProgram(prog);
            GL.GetProgram(prog, ProgramParameter.InfoLogLength, out logLength);
            if (logLength > 0)
            {
                var log = new System.Text.StringBuilder(logLength);
                GL.GetProgramInfoLog(prog, logLength, out logLength, log);
                Console.WriteLine("Program validate log:\n{0}", log);
            }

            GL.GetProgram(prog, ProgramParameter.LinkStatus, out status);
            return status != 0;
        }

        #endregion
    }
}
