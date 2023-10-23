     Shader "Laali Unit/Transparent/Diffuse" {
    Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
    _Cutoff ("Base Alpha cutoff", Range (0,0.9)) = 0.5
    }
    SubShader {
    Tags {"Queue"="Transparent 100" "IgnoreProjector"="True" }
    Alphatest Greater 0
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
    ColorMask RGB
    Cull Off
    CGPROGRAM
    #pragma surface surf Lambert alphatest:_Cutoff
    sampler2D _MainTex;
    fixed4 _Color;
    struct Input {
    float2 uv_MainTex;
    };
    void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    o.Albedo = c.rgb;
    o.Alpha = c.a;
    }
    ENDCG
    }
    Fallback "Transparent/Cutout/VertexLit"
    }