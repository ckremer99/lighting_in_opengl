#version 330 core
layout (location = 0) in vec3 aPos;    // Position attribute
layout (location = 1) in vec3 aNormal; // Normal attribute

out vec3 FragPos;  // Pass the fragment position to the fragment shader
out vec3 Normal;   // Pass the normal to the fragment shader

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;


void main()
{
    // World space position of the fragment
    FragPos = vec3(model * vec4(aPos, 1.0));
    
    // Correct normal transformation (accounting for non-uniform scaling)
    Normal = mat3(transpose(inverse(model))) * aNormal;

    // Final position in clip space
    gl_Position = projection * view * vec4(FragPos, 1.0);
}
