using Common._3D_Objects;
using GlmNet;
using OpenTK.Graphics.OpenGL4;


public class Cube3D : SceneObject3D
{
    public Cube3D(Camera3D window, vec3 back, vec3 front, vec3 left, vec3 right, vec3 bottom, vec3 top)
         : base(window)
    {
        float[] vertices = new float[] {
                    -0.5f, -0.5f, -0.5f, // back
                     0.5f, -0.5f, -0.5f,
                     0.5f,  0.5f, -0.5f,
                     0.5f,  0.5f, -0.5f,
                    -0.5f,  0.5f, -0.5f,
                    -0.5f, -0.5f, -0.5f,

                    -0.5f, -0.5f,  0.5f, // front
                     0.5f, -0.5f,  0.5f,
                     0.5f,  0.5f,  0.5f,
                     0.5f,  0.5f,  0.5f,
                    -0.5f,  0.5f,  0.5f,
                    -0.5f, -0.5f,  0.5f,

                    -0.5f,  0.5f,  0.5f, // left
                    -0.5f,  0.5f, -0.5f,
                    -0.5f, -0.5f, -0.5f,
                    -0.5f, -0.5f, -0.5f,
                    -0.5f, -0.5f,  0.5f,
                    -0.5f,  0.5f,  0.5f,

                     0.5f,  0.5f,  0.5f, //right
                     0.5f,  0.5f, -0.5f,
                     0.5f, -0.5f, -0.5f,
                     0.5f, -0.5f, -0.5f,
                     0.5f, -0.5f,  0.5f,
                     0.5f,  0.5f,  0.5f,

                    -0.5f, -0.5f, -0.5f, // bottom
                     0.5f, -0.5f, -0.5f,
                     0.5f, -0.5f,  0.5f,
                     0.5f, -0.5f,  0.5f,
                    -0.5f, -0.5f,  0.5f,
                    -0.5f, -0.5f, -0.5f,

                    -0.5f,  0.5f, -0.5f,
                     0.5f,  0.5f, -0.5f, // top
                     0.5f,  0.5f,  0.5f,
                     0.5f,  0.5f,  0.5f,
                    -0.5f,  0.5f,  0.5f,
                    -0.5f,  0.5f, -0.5f,
            };

        float[] normals = new float[]
        {
                0, 0, -1,// back
                0, 0, -1,
                0, 0, -1,
                0, 0, -1,
                0, 0, -1,
                0, 0, -1,

                0, 0, 1,// front
                0, 0, 1,
                0, 0, 1,
                0, 0, 1,
                0, 0, 1,
                0, 0, 1,

                -1, 0, 0,// left
                -1, 0, 0,
                -1, 0, 0,
                -1, 0, 0,
                -1, 0, 0,
                -1, 0, 0,

                 1, 0, 0,//right
                 1, 0, 0,
                 1, 0, 0,
                 1, 0, 0,
                 1, 0, 0,
                 1, 0, 0,

                 0, -1, 0,// bottom
                 0, -1, 0,
                 0, -1, 0,
                 0, -1, 0,
                 0, -1, 0,
                 0, -1, 0,

                 0,  1, 0,// top
                 0,  1, 0,
                 0,  1, 0,
                 0,  1, 0,
                 0,  1, 0,
                 0,  1, 0,
        };

        float[] colors = new float[]
            {
                    back.x, back.y, back.z,
                    back.x, back.y, back.z,
                    back.x, back.y, back.z,
                    back.x, back.y, back.z,
                    back.x, back.y, back.z,
                    back.x, back.y, back.z,

                    front.x, front.y, front.z,
                    front.x, front.y, front.z,
                    front.x, front.y, front.z,
                    front.x, front.y, front.z,
                    front.x, front.y, front.z,
                    front.x, front.y, front.z,

                    left.x, left.y, left.z,
                    left.x, left.y, left.z,
                    left.x, left.y, left.z,
                    left.x, left.y, left.z,
                    left.x, left.y, left.z,
                    left.x, left.y, left.z,

                    right.x, right.y, right.z,
                    right.x, right.y, right.z,
                    right.x, right.y, right.z,
                    right.x, right.y, right.z,
                    right.x, right.y, right.z,
                    right.x, right.y, right.z,

                    bottom.x,   bottom.y, bottom.z,
                    bottom.x,   bottom.y, bottom.z,
                    bottom.x,   bottom.y, bottom.z,
                    bottom.x,   bottom.y, bottom.z,
                    bottom.x,   bottom.y, bottom.z,
                    bottom.x,   bottom.y, bottom.z,

                    top.x, top.y, top.z,
                    top.x, top.y, top.z,
                    top.x, top.y, top.z,
                    top.x, top.y, top.z,
                    top.x, top.y, top.z,
                    top.x, top.y, top.z,
            };

        // инициализируем VBO, который хранит в себе информацию о вершинах, нормалях и цветах
        vbo = new VBO(
            null,
            (sizeof(float) * vertices.Length) + (sizeof(float) * normals.Length)+ (sizeof(float) * colors.Length),
            new[]
            {
                    new SubData
                    {
                        Data = vertices, Index = 0, SizeInBytes = sizeof(float) * vertices.Length
                    },
                    new SubData
                    {
                        Data = normals, Index = sizeof(float) * (vertices.Length), SizeInBytes = sizeof(float) * normals.Length
                    },
                    new SubData
                    {
                        Data = colors, Index = sizeof(float) * (vertices.Length + normals.Length), SizeInBytes = sizeof(float) * colors.Length
                    }
            });

        verticesCount = vertices.Length;

        vao = new VAO(vbo,
            new[]
            {
                    new VertexAttribPointer // position
                    {
                        Index = 0,
                        Normalize = false,
                        OffsetInBytes = 0,
                        Size = 3,
                        StrideInBytes = 3 * sizeof(float),
                        Type = VertexAttribPointerType.Float
                    },
                    new VertexAttribPointer // normal
                    {
                        Index = 1,
                        Normalize = false,
                        OffsetInBytes = vertices.Length * sizeof(float),
                        Size = 3,
                        StrideInBytes = 3 * sizeof(float),
                        Type = VertexAttribPointerType.Float
                    },
                    new VertexAttribPointer // color
                    {
                        Index = 2,
                        Normalize = false,
                        OffsetInBytes = (vertices.Length+normals.Length) * sizeof(float),
                        Size = 3,
                        StrideInBytes = 3 * sizeof(float),
                        Type = VertexAttribPointerType.Float
                    }
            });
    }
}
