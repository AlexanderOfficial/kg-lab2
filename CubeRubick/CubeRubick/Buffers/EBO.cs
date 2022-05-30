using OpenTK.Graphics.OpenGL4;

public class EBO
{
    public int ProgramID { get; }

    private readonly uint[] _indices;

    public EBO(uint[] indices)
    {
        ProgramID = GL.GenBuffer();
        _indices = indices;
    }

    public void Free()
    {
        GL.DeleteBuffer(ProgramID);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ProgramID);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices,
            BufferUsageHint.StaticDraw);
    }
}
