#version 330 core
layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec3 a_Normal;
layout (location = 2) in vec3 a_Color;

uniform mat4 projection;
uniform mat4 translation;
uniform mat4 rotation;
uniform mat4 scaling;
uniform mat4 view;
uniform mat4 test;

uniform mat4 parentModel;


out vec3 vertout_Normal;
out vec3 vertout_FragPos;
out vec3 vertout_Color;
void main()
{   
    vertout_Color = a_Color;
    mat4 resultModel = test* parentModel*translation*rotation*scaling;
    vec3 position = a_Position;
    gl_Position =
        projection*view*
        resultModel*
        vec4(position, 1.0);
    vertout_FragPos = vec3(resultModel*vec4(position, 1.0));
    vertout_Normal = mat3(transpose(inverse(resultModel)))*a_Normal;
}