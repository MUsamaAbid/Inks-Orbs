/* Use for grass, trees or foliage.*/

Shader "Laali Unit/Vegetation/Vegetation Diffuse Two Side" {
Properties {
    _Color ("Main Color", Color) = (1, 1, 1, 1)
    _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
    _Cutoff ("Base Alpha cutoff", Range (0,.9)) = .5
}
SubShader {
    Tags {"Queue"="Transparent-110" "IgnoreProjector"="True" }
    Alphatest Greater 0
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
    ColorMask RGB

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
    //   render the semitransparent detail
    Pass {
        //Tags { "RequireOption" = "SoftVegetation" }
        // Dont write to the depth buffer
        ZWrite Off

        // Only render pixels less or equal to the value
        AlphaTest LEqual [_Cutoff]

        Blend SrcAlpha OneMinusSrcAlpha

        SetTexture [_MainTex] {
            constantColor [_Color]
            Combine texture * constant, texture * constant
        }
    }
}
}