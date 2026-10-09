#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform float size;
uniform float cursor;
uniform vec4 colDiffuse;

out vec4 finalColor;

float roundedBox(vec2 p, vec2 halfExtent, float radius) {
  vec2 q = abs(p) - halfExtent + vec2(radius);
  return min(max(q.x, q.y), 0.0) + length(max(q, vec2(0.0))) - radius;
}

void main() {
  vec2 pixel = fragTexCoord * size;
  vec2 rel = pixel - vec2(size * 0.5);

  float dist = roundedBox(rel, vec2(size * 0.42, size * 0.46), 4.0);
  float outside = smoothstep(0.0, 1.0, dist);
  float band = 1.0 - smoothstep(1.0, 11.0, dist);

  float ang = atan(rel.y, rel.x);
  float spin = cursor * 0.22;
  float wave = 0.5 + 0.5 * cos(ang * 8.0 - spin);
  float wave2 = 0.5 + 0.5 * cos(ang * 12.0 + spin * 1.25);
  float spoke = mix(0.62, 1.0, pow(wave, 1.4));
  float spoke2 = mix(0.7, 1.0, pow(wave2, 1.8));

  float alpha = band * outside * (0.42 + 0.38 * spoke * spoke2);
  float edge = min(min(pixel.x, pixel.y), min(size - pixel.x, size - pixel.y));
  alpha *= smoothstep(0.0, 4.0, edge);
  alpha = clamp(alpha, 0.0, 0.8);

  vec3 color = vec3(0.9, 0.95, 1.0);
  finalColor = vec4(color, alpha) * colDiffuse * fragColor;
}
