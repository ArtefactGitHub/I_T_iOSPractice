attribute vec3 position;
attribute vec3 normal;
attribute vec2 textureCoordinates;
uniform mat4 world;
uniform mat4 viewProjection;
uniform mat4 lightDirection;
varying vec4 diffuseColor;
varying vec2 fragmentTextureCoordinates; 

void main()
{
    gl_Position = viewProjection * world * vec4(position, 1.0);
    fragmentTextureCoordinates = textureCoordinates;

//    diffuseColor = vec4(max(0, dot(mat3(world) * normal, -lightDirection)));
//    diffuseColor.a = 1.0;
}