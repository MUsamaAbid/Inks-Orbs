/* Use for grass, trees or foliage.*/

Shader "Laali Unit/Vegetation/Vegetation Diffuse Two Side Tint" {
Properties {
    _Color ("Main Color", Color) = (1, 1, 1, 1)
    _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
    _ColorTint ("Tint", Color) = (1.0, 1.0, 1.0, 1.0)
    _Cutoff ("Base Alpha cutoff", Range (0,.9)) = .5
}
SubShader {
    Tags {"Queue"="Transparent-110" "IgnoreProjector"="True" }
    Alphatest Greater 0
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
    ColorMask RGB

    // Render both front and back facing polygons.
    Cull Off

    // first pass:
    //   render any pixels that are more than [_Cutoff] opaque
    Pass {
        ZWrite On
        AlphaTest Greater [_Cutoff]
        SetTexture [_MainTex] {
            constantColor [_Color]
            combine texture * constant, texture * constant
        }
    }

    // Second pass:
    //   render the semitransparent details.
    Pass {
        //Tags { "RequireOption" = "SoftVegetation" }
        // Dont write to the depth buffer
        ZWrite Off

        // Only render pixels less or equal to the value
        AlphaTest LEqual [_Cutoff]

        // Set up alpha blending
        Blend SrcAlpha OneMinusSrcAlpha

        SetTexture [_MainTex] {
            constantColor [_Color]
            Combine texture * constant, texture * constant
        }
    }
Tags { "RenderType" = "Transparent" }

//used for backface culling
Cull Off

// Surface shaders are placed between CGPROGRAM and ENDCG
// - They use #pragma to let unity know its a surface shader
// - Must be in a SubShader block
CGPROGRAM
#pragma surface surf Unlit alphatest:_Cutoff
struct Input
{
float2 uv_MainTex;
};
sampler2D _MainTex;

// applies a color tint to the shader
fixed4 _ColorTint;

half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
{
return half4(s.Albedo, s.Alpha);
}

// applies the texture to the UV's
void surf (Input IN, inout SurfaceOutput o)
{
fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _ColorTint;
o.Albedo = c.rgb;
o.Alpha = c.a;
}
ENDCG
}
}
