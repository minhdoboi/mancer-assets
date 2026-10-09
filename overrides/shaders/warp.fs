#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;
uniform float u_amount;
uniform float u_time;

out vec4 finalColor;

float hash(float n) {
  return fract(sin(n * 127.1) * 43758.5453);
}

vec4 pick(vec2 uv) {
  if (uv.x < 0.0 || uv.y < 0.0 || uv.x > 1.0 || uv.y > 1.0) return vec4(0.0);
  return texture(texture0, uv);
}

void main() {
  vec2 c = fragTexCoord - 0.5;
  float r = length(c);
  float a = u_amount;

  // the picture swirls about its centre, hardest in the middle
  float twist = a * 2.6 * (1.0 - smoothstep(0.0, 0.75, r));
  twist *= sin(u_time * 5.0) * 0.5 + 0.5 + 0.35;
  float s = sin(twist), co = cos(twist);
  vec2 q = vec2(c.x * co - c.y * s, c.x * s + c.y * co);

  // rings of space ripple outward
  q *= 1.0 + a * 0.06 * sin(r * 26.0 - u_time * 11.0);

  // torn horizontal strips slide sideways
  float row = floor(fragTexCoord.y * 16.0);
  float tick = floor(u_time * 14.0);
  float torn = step(0.78, hash(row + tick * 7.0)) * (hash(row * 3.0 + tick) - 0.5);
  q.x += torn * 0.22 * a;

  // colour channels drift apart
  vec2 split = normalize(c + 0.0001) * 0.035 * a;
  vec2 uv = q + 0.5;
  vec4 g = pick(uv);
  float red = pick(uv + split).r;
  float blue = pick(uv - split).b;
  vec3 col = vec3(red, g.g, blue);
  float alpha = max(g.a, max(pick(uv + split).a, pick(uv - split).a) * 0.9);

  // violet-cyan sheen travelling over the surface
  float sheen = 0.5 + 0.5 * sin((fragTexCoord.x + fragTexCoord.y) * 9.0 - u_time * 8.0);
  vec3 glow = mix(vec3(0.55, 0.3, 1.0), vec3(0.3, 0.95, 1.0), sheen);
  col += glow * a * 0.22 * (0.4 + r);
  col = mix(col, col * vec3(0.85, 0.8, 1.15), a * 0.5);

  finalColor = vec4(col, alpha) * fragColor * colDiffuse;
}
