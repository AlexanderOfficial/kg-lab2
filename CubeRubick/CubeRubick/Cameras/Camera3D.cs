using GlmNet;
using OpenTK.Windowing.Desktop;

public abstract class Camera3D : CameraBase
    {
        public vec3 Position { get; } = new vec3(-4, 3, 5);

        protected mat4 projection = mat4.identity();
        protected mat4 view = mat4.identity();
        protected Camera3D(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings,
                nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            projection = glm.perspective(45f, 4f / 3f, 0.1f, 100f);
            view = glm.lookAt(Position, new vec3(0, 0, 0), new vec3(0, 1, 0));
        }
    }
