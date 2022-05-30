using GlmNet;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Common._3D_Objects
{
    public abstract class SceneObject3D
    {
        public mat4 TranslationMatrix => _translationMatrix; // матрица перемещения
        public mat4 RotationMatrix => _rotationMatrix;// матрица вращения
        public virtual mat4 Transform => ParentTransform * _translationMatrix * _rotationMatrix;
        public mat4 LocalTransform => _translationMatrix * _rotationMatrix;
        public virtual mat4 ParentTransform => Parent.Transform;
        public IReadOnlyCollection<SceneObject3D> Children => _children;
        public SceneObject3D Parent { get; protected set; }
        public vec3 WorldPosition => new(ParentTransform * new vec4(LocalPosition, 1));
        public vec3 LocalPosition { get; private set; }

        private const string DEFAULT_SCENE_OBJECT_SHADER_VERTEX_PATH = @"Shaders\3D\default.vert";
        private const string DEFAULT_SCENE_OBJECT_SHADER_FRAGMENT_PATH = @"Shaders\3D\default.frag";
        private readonly List<SceneObject3D> _children = new();

        private mat4 _rotationMatrix = mat4.identity();
        private mat4 _translationMatrix = mat4.identity();
        private mat4 _childrenTransformPreventMatrix = mat4.identity(); // матрица, которая позволяет не менять текущую мировую позицию, при добавлении объекта к родителю
        private readonly Camera3D _camera;


        protected int verticesCount;
        protected Shader shader;
        protected VAO vao;
        protected VBO vbo;

        public SceneObject3D(Camera3D camera)
        {
            if (Parent == null && this is not World)
                Parent = new World();

            _camera = camera;

            shader ??= new Shader(DEFAULT_SCENE_OBJECT_SHADER_VERTEX_PATH,
                DEFAULT_SCENE_OBJECT_SHADER_FRAGMENT_PATH);
        }

 

        public void AttachWithoutTransform(SceneObject3D parent)
        {
            if (parent == Parent)
                return;
            Parent = parent;
            _childrenTransformPreventMatrix = glm.inverse(parent.LocalTransform);
            Parent.AddChild(this);
        }

        public void TranslateWorld(vec3 newPosition)
        {
            LocalPosition = new vec3(glm.inverse(Transform)*glm.inverse(ParentTransform)* new vec4(newPosition, 1));
            _translationMatrix = glm.translate(mat4.identity(), LocalPosition);
        }

      
        public void RotateLocal(float angle, vec3 pivot)
        {
            _rotationMatrix = glm.rotate(angle, pivot);
        }

        protected void AddChild(SceneObject3D newChild)
        {
            _children.Add(newChild);
        }

        public virtual void Draw(ref mat4 view, ref mat4 projection)
        {
            vao.Bind();
            UpdateShader(ref view, ref projection);
            GL.DrawArrays(PrimitiveType.Triangles, 0, verticesCount*2);
        }


        protected void InitializeVAO_VBO(float[] vertices, float[] normals)
        {
            vbo = new VBO(
                null,
                (sizeof(float) * vertices.Length) + (sizeof(float) * normals.Length),
                new[]
                {
                    new SubData
                    {
                        Data = vertices, Index = 0, SizeInBytes = sizeof(float) * vertices.Length
                    },
                    new SubData
                    {
                        Data = normals, Index = sizeof(float) * vertices.Length, SizeInBytes = sizeof(float) * normals.Length
                    }
                });
            verticesCount = vertices.Length;
            vao = new VAO(vbo,
                new[]
                {
                    new VertexAttribPointer
                    {
                        Index = 0,
                        Normalize = false,
                        OffsetInBytes = 0,
                        Size = 3,
                        StrideInBytes = 3 * sizeof(float),
                        Type = VertexAttribPointerType.Float
                    },
                    new VertexAttribPointer
                    {
                        Index = 1,
                        Normalize = false,
                        OffsetInBytes = vertices.Length * sizeof(float),
                        Size = 3,
                        StrideInBytes = 3 * sizeof(float),
                        Type = VertexAttribPointerType.Float
                    }
                });
        }

        protected virtual void UpdateShader(ref mat4 view, ref mat4 projection)
        {
            mat4 parentModel = ParentTransform;

            shader.SetMat4("parent_transform", ref parentModel);
            shader.SetMat4("view", ref view);
            shader.SetMat4("translation", ref _translationMatrix);
            shader.SetMat4("rotation", ref _rotationMatrix);
            shader.SetMat4("children_transform_prevent", ref _childrenTransformPreventMatrix);
            shader.SetMat4("projection", ref projection);

            var pos = _camera.Position; // camera.Position сама по себе не может передаваться как реф

            shader.SetVec3("camera", ref pos);
        }
    }
}