using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public abstract class CameraBase : GameWindow
{
    protected CameraBase(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(
        gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);
        base.OnResize(e);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        GLFW.WindowHint(WindowHintInt.Samples, 16);
        GL.Enable(EnableCap.ProgramPointSize);
        GL.Enable(EnableCap.Multisample);
        GL.Enable(EnableCap.ProgramPointSize);
        GL.Enable(EnableCap.Blend);

        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Lequal);
    }
}
