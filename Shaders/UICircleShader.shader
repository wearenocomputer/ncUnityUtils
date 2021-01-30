Shader "nocomputer/UI/Circle"
{
    Properties
    {
        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0

        _Color("Color", Color) = (1, 1, 1, 1)
        _InactiveColor("InactiveColor", Color) = (1, 1, 1, 0)
        _InnerRadius("InnerRadius", Range(0, 1)) = 0
        _EdgeSoftness("EdgeSoftness", Range(0, 1)) = 0.01
        _SectorAngle("SectorAngle", Range(0, 360)) = 0
        _ZOffset("ZOffset", Float) = 0.0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest[unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask[_ColorMask]

            Pass
            {
                Name "Default"
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"
                #include "UnityUI.cginc"

                #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
                #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 xy  : TEXCOORD0;
                    float4 worldPosition : TEXCOORD1;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                fixed4 _Color;
                fixed4 _InactiveColor;
                float4 _ClipRect;
                float _InnerRadius;
                float _EdgeSoftness;
                float _SectorAngle;
                
                v2f vert(appdata_t v)
                {
                    v2f OUT;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                    OUT.worldPosition = v.vertex;
                    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                    OUT.xy = v.texcoord * 2.0 - 1.0;
                    OUT.xy.y = -OUT.xy.y;
                    OUT.color = v.color * _Color;
                    return OUT;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    half4 color = IN.color;

                    #ifdef UNITY_UI_CLIP_RECT
                    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                    #endif

                    #ifdef UNITY_UI_ALPHACLIP
                    clip(color.a - 0.001);
                    #endif

                    float radius = length(IN.xy);

                    float edge = 1.0 - _EdgeSoftness;

                    color.w *= 1.0 - smoothstep(edge, 1.0, radius);
                    color.w *= smoothstep(_InnerRadius - _EdgeSoftness, _InnerRadius, radius);

                    if (color.w < 0.007)
                        discard;

                    float sectorAngle = (_SectorAngle - 180) * UNITY_PI / 180.0;
                    float angleX = atan2(IN.xy.x, IN.xy.y);

                    if (angleX >= sectorAngle)
                        color = _InactiveColor;

                    return color;
                }
            ENDCG
            }
        }
}