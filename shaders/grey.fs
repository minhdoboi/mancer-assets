#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;

out vec4 finalColor;

float low(float x) {
  return max(x - 0.5, 0.0);
}

void main() {
  vec4 color = texture(texture0, fragTexCoord) * fragColor * colDiffuse;
  vec4 base = vec4(low(fragColor.r), low(fragColor.g), low(fragColor.b), color.a);
  finalColor = mix(color, base, 0.5);
}
