#version 330 core

out vec4 outColor;
in vec3 vertout_Normal;
in vec3 vertout_FragPos;
in vec3 vertout_Color;
uniform vec3 camera;
void main()
{
    vec3 lightPosition = camera;
    vec3 lightColor = vec3(1, 1, 1);


    float ambientStrength  = 0.2;
    vec3 ambient = ambientStrength * lightColor;


    vec3 lightDir= normalize(lightPosition - vertout_FragPos);
    float diff = max(dot(vertout_Normal, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;


    vec3 v = normalize( camera-vertout_FragPos);
    vec3 r = reflect(-v, vertout_Normal);
    float ks = 0.5;
    float spec = ks*pow(max(0, dot(v, r)),128);
    vec3 specular = lightColor * spec;

    float d = length(lightPosition-vertout_FragPos);
    float k0 = 1;
    float k1 = 0.02;
    float k2 = 0.011;
    float attenuation = 1.0 / (k0 + k1 * d + k2 * d*d);

    vec3 result = vertout_Color*(ambient + (diffuse  + specular)* attenuation);
    outColor = vec4(result, 1);
}
