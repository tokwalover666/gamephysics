Shader "Custom/Dither" {
    Properties {
        _MainTex("Texture", 2D) = "white" {}
        _Scale("Pixelation Scale", Float) = 1
        _Threshold("Dither Threshold", Float) = 0.5
        _ColorBoost("Color Boost", Float) = 1.5
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Scale;
            float _Threshold;
            float _ColorBoost;

            v2f vert(appdata_t v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target {

                float4x4 bayer = float4x4(
                    1,  9,  3, 11,
                    13, 5, 15, 7,
                    4, 12, 2, 10,
                    16, 8, 14, 6
                ) / 17.0; 

                int px = int(i.pos.x * _Scale) % 4;
                int py = int(i.pos.y * _Scale) % 4;

                float threshold = bayer[px][py];

                float4 col = tex2D(_MainTex, i.uv);
                col.rgb *= _ColorBoost; 

                float3 dithered;
                dithered.r = col.r + (_Threshold - threshold);
                dithered.g = col.g + (_Threshold - threshold);
                dithered.b = col.b + (_Threshold - threshold);

                dithered = step(0.5, dithered) * col.rgb;

                return float4(dithered, col.a);
            }
            ENDCG
        }
    }
    FallBack "Unlit/Color"
}