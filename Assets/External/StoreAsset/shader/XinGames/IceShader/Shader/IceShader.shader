// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/IceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Transparency("Transparency", Float) = 1.5
		_BaseTransparency("Base Transparency", Float) = 0.5
		_IceRoughness("Ice Roughness", Float) = 0.005
		_Distortion("Distortion", Float) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		Cull Off
		
		GrabPass {}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _GrabTexture;

		struct Input {
			float2 uv_MainTex;
			float3 worldNormal;
			float3 viewDir;
			float4 screenPos;
		};

		fixed4 _Color;
		float _Transparency;
		float _BaseTransparency;
		float _IceRoughness;
		float _Distortion;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		float random(fixed2 p) {
			return frac(sin(dot(p, fixed2(12.9898, 78.233))) * 43758.5453);
		}

		struct appdata {
			float4 texcoord : TEXCOORD0;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

			float2 uv = float2(v.texcoord.x, v.texcoord.y);
			
			float r = random(uv);
			v.vertex.xyz = v.vertex.xyz +_IceRoughness * r * v.normal - _IceRoughness / 2.0f * v.normal;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _Color;

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			float gray = 0.299f * c.r + 0.587f * c.g + 0.114f * c.b;

			float alpha = 1.0f - (abs(dot(IN.viewDir, IN.worldNormal)));
			o.Alpha = alpha * gray * _Transparency + _BaseTransparency;

			o.Smoothness = gray;
			o.Metallic = (1.0f - gray);

			float2 grabUV = (IN.screenPos.xy / IN.screenPos.w);
			grabUV += _Distortion * (gray - 0.5f) * 2.0f;
			fixed3 grab = tex2D(_GrabTexture, grabUV).rgb;
			o.Emission = grab;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
