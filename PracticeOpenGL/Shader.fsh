precision mediump float;

uniform sampler2D texture;
varying mediump vec2 fragmentTextureCoordinates;

void main()
{
   vec2 flipped_texcoord = vec2(fragmentTextureCoordinates.x, 1.0 - fragmentTextureCoordinates.y);
   gl_FragColor = texture2D(texture, flipped_texcoord);
}