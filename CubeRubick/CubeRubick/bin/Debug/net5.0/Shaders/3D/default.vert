#version 330 core
layout (location = 0) in vec3 in_position;
layout (location = 1) in vec3 in_normal;
layout (location = 2) in vec3 in_color;

uniform mat4 projection;
uniform mat4 translation;
uniform mat4 rotation;
uniform mat4 view;
uniform mat4 children_transform_prevent;

uniform mat4 parent_transform;

out vec3 out_normal;
out vec3 out_position;
out vec3 out_color;
void main()
{   
    out_color = in_color;
    mat4 result_transform= children_transform_prevent* parent_transform*translation*rotation;
    vec3 position = in_position;
    gl_Position = projection*view*result_transform*vec4(position, 1.0);
    out_position = vec3(result_transform*vec4(position, 1.0));
    out_normal = mat3(transpose(inverse(result_transform)))*in_normal;
}