// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Fire"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_WaveWith("Wave Width", float) = 1
		_WaveLength("Wave Length", float) = 1
		_WaveWidthSpeed("Wave Speed of Width", float) = 1
		_SecondWaveWidth("Second Wave Width", float) = 1
		_NoiseSpeed("Noise Speed", float) = 1
		_NoiseStrength("Strength Of Noise Texture", float) = 1
		_SecondWaveWidthSpeed("Second Wave Speed of Width", float) = 1
		_SecondWaveLength("Second Wave Length", float) = 1
		_ColLerp("Wave Strength", float) = 1
		_BotColor("Bottom Color", Color) = (1,1,1,1)
		_TopColor("Upper Color", Color) = (1,1,1,1)
		_FlameColor("Special Flames Color", Color) = (1,1,1,1)
		_ToppingColor("Spacial Top Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float2 colorOne : TEXCOORD1;
				float2 colorTwo : TEXCOORD2;
				float color : TEXCOORD3;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			sampler2D _NoiseTex;
            float4 _MainTex_ST;
			float4 _NoiseTex_ST;
			float _WaveWith;
			float _WaveLength;
			float _WaveWidthSpeed;
			float _SecondWaveWidth;
			float _NoiseStrengt;
			float _SecondWaveWidthSpeed;
			float _SecondWaveLength;
			float _NoiseSpeed;
			float _NoiseStrength;
			float _ColLerp;
			float4 _BotColor;
			float4 _TopColor;
			float4 _ToppingColor;
			float4 _FlameColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _NoiseTex);
				o.color = (mul(unity_ObjectToWorld, v.vertex).y - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).y + 0.5);
				o.colorOne = mul(unity_ObjectToWorld, v.vertex).y - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).y + 0.5 + sin(_Time[0] * _SecondWaveWidthSpeed + o.uv.x * _SecondWaveLength) * _SecondWaveWidth;
				o.colorTwo = mul(unity_ObjectToWorld, v.vertex).y - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).y + 0.5 + sin(_Time[0] * _WaveWidthSpeed + o.uv.x * _WaveLength) * _WaveWith;
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				//i.uvtu.x += randal.r * _NoiseStrengt;
				//i.uvtu.y += randal.g * _NoiseStrengt;
				//i.uvTree.x += randal.g * _NoiseStrengt;
				//i.uvTree.y += randal.b * _NoiseStrengt;
				i.uv.y -= _Time[1] * 0.3;
				float2 firstUv = i.uv;
				float2 secondUv = i.uv;
				firstUv += sin(_Time[1] * _WaveWidthSpeed + i.uv.y * _WaveLength) * _WaveWith;
				secondUv -= sin(_Time[1] * _WaveWidthSpeed * 0.7 + i.uv.y * _WaveLength * 0.7) * _WaveWith * 0.7;
				fixed4 firstRandal = tex2D(_NoiseTex, secondUv);
				fixed4 secondRandal = tex2D(_NoiseTex, firstUv);
				fixed4 col = lerp(_BotColor, _TopColor, i.color + lerp(firstRandal.r, secondRandal.g, _ColLerp) - 0.5);
				float lastLerp = (tex2D(_NoiseTex, i.uv).b / i.color).r;
				lastLerp = clamp(lastLerp - 1,0,1);
				col = lerp(col, _FlameColor, lastLerp);
				lastLerp = (tex2D(_NoiseTex, i.uv)).g * i.color * i.color * 2;
				col = lerp(col, _ToppingColor, lastLerp);
				return col;
            }
            ENDCG
        }
    }
}
