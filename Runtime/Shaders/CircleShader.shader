Shader "nocomputer/Sprites/CircleShader"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_SectorColor("SectorColor", Color) = (0, 0, 0, 0)
		_Size("Size", Float) = 0.01	// in vertical NDC coords (-1 -> 1), assuming it's rendered with a Quad (width and height 1)
		_InnerRadius("InnerRadius", Range(0, 1)) = 0
		_EdgeSoftness("EdgeSoftness", Range(0, 1)) = 0.01	
		_SectorAngle("SectorAngle", Range(0, 360)) = 0		
    }
    SubShader
    {
        Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" "PreviewType"="Plane"}
        LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			#pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 xy : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
			fixed4 _SectorColor;
			float _Size;
			float _InnerRadius;
			float _EdgeSoftness;
			float _SectorAngle;

            v2f vert (appdata v)
            {
                v2f o;
                
				// flip vertical to match NDC
				// for some reason, screen params doesn't work on android
				float aspect = -UNITY_MATRIX_P[0][0] / UNITY_MATRIX_P[1][1];
				float2 aspectSize = float2(_Size * aspect, -_Size);
				
				v.vertex.xy *= 2.0;
				o.vertex = UnityObjectToClipPos(float4(0.0, 0.0, 0.0, 1.0));

				v.vertex.w = 0.0;
				float4 localPos = v.vertex; // mul(unity_ObjectToWorld, v.vertex);
				localPos.xy *= aspectSize;
				o.vertex.xy += localPos.xy * o.vertex.w;
				o.xy = v.vertex.xy;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;

				float radius = length(i.xy);

				float edge = 1.0 - _EdgeSoftness;
				
				col.w = 1.0 - smoothstep(edge, 1.0, radius);
				col.w *= smoothstep(_InnerRadius - _EdgeSoftness, _InnerRadius, radius);
				
				if (col.w < 0.007)
					discard;


				float sectorAngle = (_SectorAngle - 180) * UNITY_PI / 180.0;
				float angleX = atan2(i.xy.x, i.xy.y);

				if (angleX >= sectorAngle)
					col = _SectorColor;


				return col;
            }
            ENDCG
        }
    }
}
