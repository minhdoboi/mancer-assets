#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;
uniform float u_night;

out vec4 finalColor;

void main() {
  vec4 color = texture(texture0, fragTexCoord) * fragColor * colDiffuse;
  vec3 day = color.rgb;
  float grey = dot(day, vec3(0.30, 0.59, 0.11));
  vec3 moon = mix(day, vec3(grey), 0.38) * vec3(0.26, 0.36, 0.70);
  moon += vec3(0.03, 0.05, 0.12);
  vec2 d = fragTexCoord - vec2(0.5);
  float vig = smoothstep(0.92, 0.18, dot(d, d) * 2.15);
  moon *= mix(0.52, 1.0, vig);
  finalColor = vec4(mix(day, moon, clamp(u_night, 0.0, 1.0)), color.a);
}
