#version 330 core

out vec4 result_color;
in vec3 out_normal;
in vec3 out_position;
in vec3 out_color;
uniform vec3 camera;
void main()
{
    vec3 light_position = camera;
    vec3 light_color = vec3(1, 1, 1);


    //ambient
    float ambient_mix  = 0.2;
    vec3 ambient = ambient_mix * light_color;

    // diffuse
    vec3 light_direction= normalize(light_position - out_position);
    float diff = max(dot(out_normal, light_direction), 0.0);
    vec3 diffuse = diff * light_color;

    // specular
    vec3 v = normalize( camera-out_position);
    vec3 r = reflect(-v, out_normal);
    float ks = 0.5;
    float spec = ks*pow(max(0, dot(v, r)),128);
    vec3 specular = light_color * spec;


    // point light
    float d = length(light_position-out_position);
    float k0 = 1;
    float k1 = 0.02;
    float k2 = 0.011;
    float attenuation = 1.0 / (k0 + k1 * d + k2 * d*d); //decreasing light intensity with distance

    vec3 result = out_color*(ambient  + (diffuse ));
    result_color = vec4(result, 1);
}
