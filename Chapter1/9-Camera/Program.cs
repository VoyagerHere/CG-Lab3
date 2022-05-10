using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace LearnOpenTK
{
    public static class Program
    {
        private static void Main()
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "LearnOpenTK - Camera",
                Flags = ContextFlags.ForwardCompatible,
            };
            GameWindowSettings settings = new GameWindowSettings
            {
                RenderFrequency = 1,
                UpdateFrequency = 1
            };
            using var window = new Window(settings, nativeWindowSettings);
            window.Run();
        }
    }
}
