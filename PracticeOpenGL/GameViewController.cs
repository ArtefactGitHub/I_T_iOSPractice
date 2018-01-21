﻿using System.Diagnostics;

using Foundation;
using GLKit;
using OpenGLES;
using PracticeOpenGL.Source.Framework;
using PracticeOpenGL.Source.Workspace;

namespace PracticeOpenGL
{
    [Register("GameViewController")]
    public class GameViewController : GLKViewController//, IGLKViewDelegate
    {
        #region Enum

        enum Uniform
        {
            ModelViewProjection_Matrix,
            Normal_Matrix,
            Count
        }

        enum Attribute
        {
            Vertex,
            Normal,
            Count
        }

        #endregion

        //int[] uniforms = new int[(int)Uniform.Count];

        #region VertexData

        //     float[] cubeVertexData = {
        //// Data layout for each line below is:
        //// positionX, positionY, positionZ,     normalX, normalY, normalZ,
        //0.5f, -0.5f, -0.5f,        1.0f, 0.0f, 0.0f,
        //    0.5f, 0.5f, -0.5f,         1.0f, 0.0f, 0.0f,
        //    0.5f, -0.5f, 0.5f,         1.0f, 0.0f, 0.0f,
        //    0.5f, -0.5f, 0.5f,         1.0f, 0.0f, 0.0f,
        //    0.5f, 0.5f, -0.5f,          1.0f, 0.0f, 0.0f,
        //    0.5f, 0.5f, 0.5f,         1.0f, 0.0f, 0.0f,

        //    0.5f, 0.5f, -0.5f,         0.0f, 1.0f, 0.0f,
        //    -0.5f, 0.5f, -0.5f,        0.0f, 1.0f, 0.0f,
        //    0.5f, 0.5f, 0.5f,          0.0f, 1.0f, 0.0f,
        //    0.5f, 0.5f, 0.5f,          0.0f, 1.0f, 0.0f,
        //    -0.5f, 0.5f, -0.5f,        0.0f, 1.0f, 0.0f,
        //    -0.5f, 0.5f, 0.5f,         0.0f, 1.0f, 0.0f,

        //    -0.5f, 0.5f, -0.5f,        -1.0f, 0.0f, 0.0f,
        //    -0.5f, -0.5f, -0.5f,       -1.0f, 0.0f, 0.0f,
        //    -0.5f, 0.5f, 0.5f,         -1.0f, 0.0f, 0.0f,
        //    -0.5f, 0.5f, 0.5f,         -1.0f, 0.0f, 0.0f,
        //    -0.5f, -0.5f, -0.5f,       -1.0f, 0.0f, 0.0f,
        //    -0.5f, -0.5f, 0.5f,        -1.0f, 0.0f, 0.0f,

        //    -0.5f, -0.5f, -0.5f,       0.0f, -1.0f, 0.0f,
        //    0.5f, -0.5f, -0.5f,        0.0f, -1.0f, 0.0f,
        //    -0.5f, -0.5f, 0.5f,        0.0f, -1.0f, 0.0f,
        //    -0.5f, -0.5f, 0.5f,        0.0f, -1.0f, 0.0f,
        //    0.5f, -0.5f, -0.5f,        0.0f, -1.0f, 0.0f,
        //    0.5f, -0.5f, 0.5f,         0.0f, -1.0f, 0.0f,

        //    0.5f, 0.5f, 0.5f,          0.0f, 0.0f, 1.0f,
        //    -0.5f, 0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
        //    0.5f, -0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
        //    0.5f, -0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
        //    -0.5f, 0.5f, 0.5f,         0.0f, 0.0f, 1.0f,
        //    -0.5f, -0.5f, 0.5f,        0.0f, 0.0f, 1.0f,

        //    0.5f, -0.5f, -0.5f,        0.0f, 0.0f, -1.0f,
        //    -0.5f, -0.5f, -0.5f,       0.0f, 0.0f, -1.0f,
        //    0.5f, 0.5f, -0.5f,         0.0f, 0.0f, -1.0f,
        //    0.5f, 0.5f, -0.5f,         0.0f, 0.0f, -1.0f,
        //    -0.5f, -0.5f, -0.5f,       0.0f, 0.0f, -1.0f,
        //    -0.5f, 0.5f, -0.5f,        0.0f, 0.0f, -1.0f
        //};

        #endregion

        //int program;

        //Matrix4 modelViewProjectionMatrix;
        //Matrix3 normalMatrix;

        //uint vertexArray;
        //uint vertexBuffer;

        EAGLContext context { get; set; }

        [Export("initWithCoder:")]
        public GameViewController(NSCoder coder) : base(coder)
        {
        }

        private GLGame m_Activity;

        public override void ViewDidLoad()
        {
            Debug.WriteLine("ViewDidLoad()");

            base.ViewDidLoad();

            context = new EAGLContext(EAGLRenderingAPI.OpenGLES2);

            if (context == null)
            {
                Debug.WriteLine("Failed to create ES context");
            }

            var view = (GLKView)View;
            view.Context = context;
            view.DrawableDepthFormat = GLKViewDrawableDepthFormat.Format24;

            SetupGL();

            m_Activity = new GLGameTest();
            m_Activity.OnSurfaceCreated();
        }

        protected override void Dispose(bool disposing)
        {
            Debug.WriteLine("Dispose() : " + disposing);

            base.Dispose(disposing);

            TearDownGL();

            if (EAGLContext.CurrentContext == context)
                EAGLContext.SetCurrentContext(null);
        }

        public override void DidReceiveMemoryWarning()
        {
            Debug.WriteLine("DidReceiveMemoryWarning");

            base.DidReceiveMemoryWarning();

            if (IsViewLoaded && View.Window == null)
            {
                View = null;

                TearDownGL();

                if (EAGLContext.CurrentContext == context)
                {
                    EAGLContext.SetCurrentContext(null);
                }
            }

            // Dispose of any resources that can be recreated.
        }

        #region PrefersStatusBarHidden
        public override bool PrefersStatusBarHidden()
        {
            Debug.WriteLine("PrefersStatusBarHidden()");

            return true;
        }
        #endregion

        void SetupGL()
        {
            EAGLContext.SetCurrentContext(context);

            //LoadShaders();

            //GL.Enable(EnableCap.DepthTest);

            //GL.Oes.GenVertexArrays(1, out vertexArray);
            //GL.Oes.BindVertexArray(vertexArray);

            //GL.GenBuffers(1, out vertexBuffer);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(cubeVertexData.Length * sizeof(float)), cubeVertexData, BufferUsage.StaticDraw);

            //GL.EnableVertexAttribArray((int)GLKVertexAttrib.Position);
            //GL.VertexAttribPointer((int)GLKVertexAttrib.Position, 3, VertexAttribPointerType.Float, false, 24, new IntPtr(0));
            //GL.EnableVertexAttribArray((int)GLKVertexAttrib.Normal);
            //GL.VertexAttribPointer((int)GLKVertexAttrib.Normal, 3, VertexAttribPointerType.Float, false, 24, new IntPtr(12));

            //GL.Oes.BindVertexArray(0);
        }

        void TearDownGL()
        {
            EAGLContext.SetCurrentContext(context);
            //GL.DeleteBuffers(1, ref vertexBuffer);
            //GL.Oes.DeleteVertexArrays(1, ref vertexArray);

            //if (program > 0)
            //{
            //    GL.DeleteProgram(program);
            //    program = 0;
            //}

            // 終了させる
            m_Activity.IsFinishing = true;
            m_Activity.OnPause();
        }

        #region GLKViewController methods

        public override void Update()
        {
            //var aspect = (float)Math.Abs(View.Bounds.Size.Width / View.Bounds.Size.Height);
            //var projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(65.0f), aspect, 0.1f, 100.0f);

            //var baseModelViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
            //var modelViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

            //modelViewMatrix = modelViewMatrix * baseModelViewMatrix;

            //normalMatrix = new Matrix3(Matrix4.Transpose(Matrix4.Invert(modelViewMatrix)));

            //modelViewProjectionMatrix = modelViewMatrix * projectionMatrix;
        }

        public override void DrawInRect(GLKView view, CoreGraphics.CGRect rect)
        {
            //GL.ClearColor(0.65f, 0.65f, 0.65f, 1.0f);
            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL.Oes.BindVertexArray(vertexArray);

            //// Render the object again with ES2
            //GL.UseProgram(program);

            //GL.UniformMatrix4(uniforms[(int)Uniform.ModelViewProjection_Matrix], false, ref modelViewProjectionMatrix);
            //GL.UniformMatrix3(uniforms[(int)Uniform.Normal_Matrix], false, ref normalMatrix);

            //GL.DrawArrays(BeginMode.Triangles, 0, 36);

            m_Activity.OnDrawFrame();
        }

        #endregion

#if false

        #region Shader

        bool LoadShaders()
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

        string LoadResource(string name, string type)
        {
            var path = NSBundle.MainBundle.PathForResource(name, type);
            return System.IO.File.ReadAllText(path);
        }

        bool CompileShader(ShaderType type, string src, out int shader)
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

        bool LinkProgram(int prog)
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

        bool ValidateProgram(int prog)
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

#endif
    }
}
