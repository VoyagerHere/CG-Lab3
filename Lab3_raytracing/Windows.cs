using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.IO;
using OpenTK.Mathematics;

namespace RayTracing
{
    public class Window : GameWindow
    {
        private bool _firstMove = true;

        private Vector2 _lastPos;


        private ShaderProgram _shaderProgram;

        private float[] _vertices = {
            -1f, -1f, 0.0f, 
            -1f, 1f, 0.0f, 
            1f, -1f, 0.0f, 
            1f, 1f, 0f
        };
        private int _vertexArrayObject;
        private int _vertexBufferObject;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            //Сборка шейдерной программы (компиляция и линковка двух шейдеров)
            string vertexShaderText = File.ReadAllText("../../../Shaders/raytracing.vert");
            string fragmentShaderText = File.ReadAllText("../../../Shaders/raytracing.frag");

            _shaderProgram = new ShaderProgram(
                vertexShaderText, fragmentShaderText);

            //Создание Vertex Array Object и его привязка
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            //Создание объекта буфера вершин/нормалей, его привязка и заполнение
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (sizeof(float) * _vertices.Length),
                _vertices, BufferUsageHint.StaticDraw);

            //Указание OpenGL, где искать вершины в буфере вершин/нормалей
            var posLoc = _shaderProgram.GetAttribLocation("vPosition");
            GL.EnableVertexAttribArray(posLoc);
            GL.VertexAttribPointer(posLoc, 3, VertexAttribPointerType.Float, false, 0, 0);

            //Установка фона
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //Включение теста глубины во избежание наложений
            GL.Enable(EnableCap.DepthTest);

            _shaderProgram.SetVector3("uCamera.Position", new Vector3(0.0f, 0.0f, -7.5f));
            _shaderProgram.SetVector3("uCamera.View", new Vector3(0.0f, 0.0f, 1f));
            _shaderProgram.SetVector3("uCamera.Up", new Vector3(0.0f, 1f, 0.0f));
            _shaderProgram.SetVector3("uCamera.Side", new Vector3(1.0f, 0.0f, 0.0f));
            _shaderProgram.SetVector2("uCamera.Scale", new Vector2(1.4f));
            _shaderProgram.SetVector3("uLight.Position", new Vector3(2, -1.0f, -4.0f));
        }

        protected override void OnUnload()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.UseProgram(0);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteBuffer(_vertexBufferObject);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            //Очистка буферов цвета и глубины
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //Привязка буфера вершин
            GL.BindVertexArray(_vertexArrayObject);

            _shaderProgram.Use();
            

            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (!IsFocused)
                return;

            var input = KeyboardState;

            //Закрытие окна на Esc
            if (input.IsKeyDown(Keys.Escape))
                Close();

            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
            }
        }

        //Обновление размеров области видимости при изменении размеров окна
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}