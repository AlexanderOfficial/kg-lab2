using Common;
using Common._3D_Objects;
using GlmNet;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

public class CubeRubickWorld : World
{
    private Cube3D[,,] _cubeRubick = new Cube3D[3, 3, 3];
    private Cube3D _leftOrigin = null;
    private Cube3D _rightOrigin = null;
    private List<SceneObject3D> _toDraw = new();
    public CubeRubickWorld(CubeRubickCamera camera)
    {
        _cubeRubick[0, 0, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Blue, Color.Black, Color.Orange, Color.Black);  // left side
        _cubeRubick[0, 0, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Blue, Color.Black, Color.Orange, Color.Black);
        _cubeRubick[0, 0, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Blue, Color.Black, Color.Orange, Color.Black);

        _cubeRubick[0, 1, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Blue, Color.Black, Color.Black, Color.Black);
        _cubeRubick[0, 1, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Blue, Color.Black, Color.Black, Color.Black);
        _cubeRubick[0, 1, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Blue, Color.Black, Color.Black, Color.Black);

        _cubeRubick[0, 2, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Blue, Color.Black, Color.Black, Color.Yellow);
        _cubeRubick[0, 2, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Blue, Color.Black, Color.Black, Color.Yellow);
        _cubeRubick[0, 2, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Blue, Color.Black, Color.Black, Color.Yellow);

        _cubeRubick[1, 0, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Black, Color.Black, Color.Orange, Color.Black);  // middle side
        _cubeRubick[1, 0, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Black, Color.Black, Color.Orange, Color.Black);
        _cubeRubick[1, 0, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Black, Color.Black, Color.Orange, Color.Black);

        _cubeRubick[1, 1, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
        _cubeRubick[1, 1, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
        _cubeRubick[1, 1, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Black);

        _cubeRubick[1, 2, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Black, Color.Black, Color.Black, Color.Yellow);
        _cubeRubick[1, 2, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Yellow);
        _cubeRubick[1, 2, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Yellow);

        _cubeRubick[2, 0, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Black, Color.Green, Color.Orange, Color.Black);  // right side
        _cubeRubick[2, 0, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Black, Color.Green, Color.Orange, Color.Black);
        _cubeRubick[2, 0, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Black, Color.Green, Color.Orange, Color.Black);

        _cubeRubick[2, 1, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Black, Color.Green, Color.Black, Color.Black);
        _cubeRubick[2, 1, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Black, Color.Green, Color.Black, Color.Black);
        _cubeRubick[2, 1, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Black, Color.Green, Color.Black, Color.Black);

        _cubeRubick[2, 2, 0] = new Cube3D(camera, Color.Red, Color.Black, Color.Black, Color.Green, Color.Black, Color.Yellow);
        _cubeRubick[2, 2, 1] = new Cube3D(camera, Color.Black, Color.Black, Color.Black, Color.Green, Color.Black, Color.Yellow);
        _cubeRubick[2, 2, 2] = new Cube3D(camera, Color.Black, Color.White, Color.Black, Color.Green, Color.Black, Color.Yellow);

        float offset = 0.07f; // зазор между кубиками

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    Cube3D cube = _cubeRubick[i, j, k];
                    cube.TranslateWorld(new vec3(i*(1f+offset)-(1f+offset), j*(1f+offset)-(1f+offset), k*(1f+offset)-(1f+offset)));
                    _toDraw.Add(cube);
                    _cubeRubick[i, j, k] = cube;
                }
            }
        }
        _leftOrigin = GetLeftOriginCube(); // кубик, вокруг которого будет вроащаться левая грань
        _rightOrigin = GetRightOriginCube(); // кубик, вокруг которого будет вроащаться правая грань
    }
    public override void Draw(ref mat4 view, ref mat4 projection)
    {
        foreach (var item in _toDraw)
        {
            item.Draw(ref view, ref projection);
        }
        _leftOrigin.RotateLocal((float)GLFW.GetTime()*1.4f, new vec3(1, 0, 0));
        _rightOrigin.RotateLocal((float)GLFW.GetTime()*0.9f, new vec3(1, 0, 0));
    }
    private Cube3D GetLeftOriginCube()
    {
        var cubeOrigin = _cubeRubick[0, 1, 1];
        _cubeRubick[0, 0, 0].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[0, 0, 1].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[0, 0, 2].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[0, 1, 0].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[0, 1, 2].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[0, 2, 0].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[0, 2, 1].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[0, 2, 2].AttachWithoutTransform(cubeOrigin);
        return cubeOrigin;
    }
    private Cube3D GetRightOriginCube()
    {
        var cubeOrigin = _cubeRubick[2, 1, 1]; // куб, вокруг которого все будет вращаться
        _cubeRubick[2, 0, 0].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[2, 0, 1].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[2, 0, 2].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[2, 1, 0].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[2, 1, 2].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[2, 2, 0].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[2, 2, 1].AttachWithoutTransform(cubeOrigin);
        _cubeRubick[2, 2, 2].AttachWithoutTransform(cubeOrigin);
        return cubeOrigin;
    }
}

