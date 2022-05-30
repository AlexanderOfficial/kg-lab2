using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

public class Program
{
    private static void Main(string[] args)
    {
        var settings = GameWindowSettings.Default;
        var nativeWindowSettings = new NativeWindowSettings
        {
            Size = new Vector2i(800, 600),
            Title = "Cube Rubick"
        };

        using (var window = new CubeRubickCamera(settings, nativeWindowSettings))
        {
            window.Run();
        }
    }
}

