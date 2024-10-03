#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#define MAX_BONES 90

float4x4 World;
float4x4 View;
float4x4 Projection;

float3 lightPosition;
float4 lightColor;
float lightIntensity;

Texture2D SpriteTexture;

matrix Bones[MAX_BONES];
matrix RestPose[MAX_BONES];

// Debug
bool debugMode;
uint currentBone;

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
    float4 Joints : BLENDINDICES0;
    float4 Weights : BLENDWEIGHT0;
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

    matrix skinMatrix = mul(input.Weights.x, mul(RestPose[int(input.Joints.x)], Bones[int(input.Joints.x)]));
    skinMatrix += mul(input.Weights.y, mul(RestPose[int(input.Joints.y)], Bones[int(input.Joints.y)]));
    skinMatrix += mul(input.Weights.z, mul(RestPose[int(input.Joints.z)], Bones[int(input.Joints.z)]));
    skinMatrix += mul(input.Weights.w, mul(RestPose[int(input.Joints.w)], Bones[int(input.Joints.w)]));

    float4 worldPosition = mul(mul(input.Position, skinMatrix), World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
    float4 fragPos = mul(modelViewProjection, mul(input.Position, skinMatrix));

    float4 norm = normalize(mul(inverse(World), input.Normal));
    float4 lightDir = normalize(float4(lightPosition, 0) - float4(fragPos[0], fragPos[1], fragPos[2], 1));

    output.Color = LambertShading(lightColor, lightIntensity, norm, lightDir);
    output.TextureCoordinates = input.TextureCoordinates;

    if (debugMode)
    {
        if (input.Joints.x == currentBone)
            output.Color = float4(1, 0, 0, 1);
        else
            output.Color = float4(0, 0, 1, 1);
    }
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