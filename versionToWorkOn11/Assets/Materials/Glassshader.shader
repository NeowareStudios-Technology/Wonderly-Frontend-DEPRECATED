// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Frost" 
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)

        _MainTex("Diffuse", 2D) = "white" {}
        _Noise("Noise", 2D) = "black" {}

        _Range("Range", Float) = 0.025
        _Blur("Blur", Float) = 0.005
    }

    SubShader
    {
        Cull Back

        GrabPass{ "_Frost" }

        CGINCLUDE
        #include "UnityCG.cginc"

        half4 _Color;

        sampler2D _MainTex;
        float4 _MainTex_ST;

        sampler2D _Frost;
        sampler2D _Noise;
        float4 _Noise_ST;

        half _Range;
        half _Blur;

        ENDCG

        Pass
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            struct v2f
            {
                float4 pos : SV_POSITION;

                float3 uv : TEXCOORD;
                float4 screenPos : TEXCOORD1;
                float3 ray : TEXCOORD2;
            };

            v2f vert(appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.screenPos = ComputeScreenPos(o.pos);
                o.ray = mul(UNITY_MATRIX_MV, v.vertex).xyz * float3(-1, -1, 1);
                return o;
            }



            half4 frag(v2f i) : SV_Target
            {
                i.ray = i.ray * (_ProjectionParams.z / i.ray.z);
                float2 uv = (1-i.screenPos.xy) / i.screenPos.w;
                uv.x = 1-uv.x;
                float2 frostUV = tex2D(_Noise, i.uv * _Noise_ST.xy + _Noise_ST.zw).xy;

                frostUV -= 0.5;
                frostUV *= _Range;
                frostUV += uv;

                half4 frost = tex2D(_Frost, frostUV);
                frost += tex2D(_Frost, frostUV + float2(_Blur, _Blur));
                frost += tex2D(_Frost, frostUV + float2(_Blur, -_Blur));
                frost += tex2D(_Frost, frostUV + float2(-_Blur, _Blur));
                frost += tex2D(_Frost, frostUV + float2(-_Blur, -_Blur));
                frost *= 0.2;

                half4 diffuse = tex2D(_MainTex, i.uv * _MainTex_ST.xy + _MainTex_ST.zw);

                half alpha = _Color.a * diffuse.a;

                return half4(frost.xyz + (diffuse.rgb * _Color.rgb * alpha), 1);
            }

            ENDCG
        }
    }

    Fallback Off
}