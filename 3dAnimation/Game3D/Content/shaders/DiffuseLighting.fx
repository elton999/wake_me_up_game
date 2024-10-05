#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 World;
float4x4 View;
float4x4 Projection;

float3 lightPosition;
float4 lightColor;
float lightIntensity;
float4 modelColor;

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float4 Normal : NORMAL0;
    float2 TextureCoordinates : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD1;
};

#include "ultis.hfx"

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    matrix modelViewProjection = mul(mul(Projection, View), World);

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    float4 pos = mul(viewPosition, Projection);

    output.Position = pos;

    float4 fragPos = mul(modelViewProjection, input.Position);

    float4 norm = normalize(mul(inverse(World), input.Normal));
    float4 lightDir = normalize(float4(lightPosition, 0) - float4(fragPos[0], fragPos[1], fragPos[2], 1));

    output.Color = modelColor * LambertShading(lightColor, lightIntensity, norm, lightDir);
    output.TextureCoordinates = input.TextureCoordinates;

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 result = tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;

    return result;
}

technique BasicColorDrawing{
    pass P0{
        VertexShader = compile VS_SHADERMODEL MainVS();
PixelShader = compile PS_SHADERMODEL MainPS();
}
}
;