#version 330 core
out vec4 FragColor;

in vec3 FragPos;    // Fragment position from vertex shader
in vec3 Normal;     // Normal vector from vertex shader
in vec2 TexCoord;   // Texture coordinates from vertex shader

// Material properties
struct Material {
    sampler2D diffuse;  // Texture sampler for the diffuse color
    vec3 specular;      // Specular color (non-textured)
    float shininess;    // Shininess factor
};

// Light properties
struct Light {
    vec3 position;      // Light position in world space
    vec3 ambient;       // Ambient color
    vec3 diffuse;       // Diffuse color
    vec3 specular;      // Specular color
};

uniform Material material;
uniform Light light;
uniform vec3 viewPos;   // Camera position (for specular reflection)

void main()
{
    // Ambient lighting
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoord).rgb;

    // Diffuse lighting
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoord).rgb;

    // Specular lighting
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * material.specular;

    // Combine the lighting components
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
}
