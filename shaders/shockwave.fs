#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform vec2 u_center;
uniform vec2 u_size;
uniform vec2 u_stretch;
uniform float u_radius;
uniform float u_radius2;
uniform float u_width;
uniform float u_fade;
uniform float u_flash;

out vec4 finalColor;

float ring(float dist, float radius, float width) {
  float d = abs(dist - radius);
  float core = 1.0 - smoothstep(0.0, width * 0.32, d);
  float halo = 1.0 - smoothstep(0.0, width, d);
  return halo * 0.5 + core;
}

void main() {
  vec2 local = fragTexCoord;
  vec2 d = (local - u_center) * u_stretch;
  float dist = length(d);
  float ang = atan(d.y, d.x);
  float wobble = 1.0 + 0.04 * sin(ang * 7.0 + u_radius * 0.045);
  float r0 = u_radius * wobble;
  float r1 = u_radius2 * (1.0 + 0.03 * sin(ang * 5.0 - u_radius * 0.035));

  float wave = 0.0;
  if (u_radius > 2.0) wave += ring(dist, r0, u_width);
  if (u_radius2 > 2.0) wave += ring(dist, r1, u_width * 0.8) * 0.62;

  float dust = 0.0;
  if (u_radius > 2.0) {
    float inner = smoothstep(r0, r0 - u_width * 2.6, dist);
    dust = inner * 0.14 * (1.0 - smoothstep(r0 * 0.12, r0, dist));
  }

  float flash = u_flash * (1.0 - smoothstep(0.0, 52.0, dist));
  float a = (wave + dust + flash) * u_fade;
  float edge = min(min(local.x, local.y), min(u_size.x - local.x, u_size.y - local.y));
  a *= smoothstep(0.0, 16.0, edge);
  if (a < 0.012) discard;

  vec3 gold = vec3(0.96, 0.68, 0.26);
  vec3 violet = vec3(0.7, 0.52, 1.0);
  vec3 white = vec3(1.0, 0.97, 0.9);
  vec3 color = mix(gold, violet, 0.38);
  color = mix(color, white, clamp(wave * 0.7 + flash, 0.0, 1.0));

  finalColor = vec4(color, clamp(a, 0.0, 1.0));
}
