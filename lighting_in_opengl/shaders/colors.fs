#version 330 core
out vec4 FragColor;

in vec3 Normal;    // Incoming normal from the vertex shader
in vec3 FragPos;   // Incoming fragment position from the vertex shader

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

void main()
{
    // Ambient lighting
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
    
    // Normalize the incoming normal vector
    vec3 norm = normalize(Normal);
    
    // Compute the direction of the light
    vec3 lightDir = normalize(lightPos - FragPos);
    
    // Diffuse lighting
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    // Specular lighting
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - FragPos);  // Direction from fragment to camera
    vec3 reflectDir = reflect(-lightDir, norm);   // Reflect the light direction around the normal
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);  // 32 is the shininess factor
    vec3 specular = specularStrength * spec * lightColor;

    // Combine results
    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
}
