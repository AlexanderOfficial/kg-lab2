// unset

using Common._3D_Objects;
using GlmNet;

public class World : SceneObject3D
{
    public World() : base(null)
    {
    }

    public override mat4 Transform => mat4.identity();
    public override mat4 ParentTransform => mat4.identity();

    public override void Draw(ref mat4 view, ref mat4 projection)
    {
    }
}