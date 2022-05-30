using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
public class CubeRubickCamera : Camera3D
{
    private World _world = null;
    public CubeRubickCamera(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings,
        nativeWindowSettings)
    {
        _world = new CubeRubickWorld(this);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        _world.Draw(ref view, ref projection);
        Context.SwapBuffers();
        base.OnRenderFrame(args);
    }

}

