using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace RayTracing
{
    public static class Program
    {
        private static void Main()
        {
            //Установка настроек окна программы
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(900, 800),
                Title = "RayTracing",
                Flags = ContextFlags.ForwardCompatible
            };

            //Запуск окна программы
            using var window = new Window(GameWindowSettings.Default, nativeWindowSettings);
            window.Run();
        }
    }
}