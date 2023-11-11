// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Cutout/CutoutAlignment"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_AlphaCutout("Alpha Cutout", Float) = 0
		_TexPower("Tex Power", Float) = 1
		_NoiseSpeed("Noise Speed", Vector) = (0,0,0,0)
		_MaskClipValue("Mask Clip Value", Float) = 0.5
		_NoiseMin("Noise Min", Float) = 0
		_NoiseMax("Noise Max", Float) = 1
		[KeywordEnum(X,Y)] _Alignment("Alignment", Float) = 0
		_Size("Size", Float) = 1
		_NoisePower("Noise Power", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#pragma shader_feature_local _USESOFTPARTICLES_ON
				#include "UnityShaderVariables.cginc"
				#define ASE_NEEDS_FRAG_COLOR
				#pragma shader_feature_local _ALIGNMENT_X _ALIGNMENT_Y


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef _USESOFTPARTICLES_ON
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float _TexPower;
				uniform sampler2D _NoiseTex;
				uniform float2 _NoiseSpeed;
				uniform float4 _NoiseTex_ST;
				uniform float _NoisePower;
				uniform float _NoiseMin;
				uniform float _NoiseMax;
				uniform float _AlphaCutout;
				uniform float _Size;
				uniform float _MaskClipValue;


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					
					#ifdef _USESOFTPARTICLES_ON
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					#endif
					
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					#ifdef _USESOFTPARTICLES_ON
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif
					#endif

					float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float4 temp_cast_0 = (_TexPower).xxxx;
					float2 uv0_NoiseTex = i.texcoord.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
					float2 panner62 = ( _Time.y * _NoiseSpeed + uv0_NoiseTex);
					float2 uv078 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					#if defined(_ALIGNMENT_X)
					float staticSwitch96 = uv078.x;
					#elif defined(_ALIGNMENT_Y)
					float staticSwitch96 = uv078.y;
					#else
					float staticSwitch96 = uv078.x;
					#endif
					float3 uv046 = i.texcoord.xyz;
					uv046.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_128_0 = ( abs( ( staticSwitch96 + ( _AlphaCutout + uv046.z ) ) ) - _Size );
					float smoothstepResult81 = smoothstep( _NoiseMin , _NoiseMax , temp_output_128_0);
					float OpacityMask27 = ( ( ( tex2D( _NoiseTex, panner62 ).r * _NoisePower ) - smoothstepResult81 ) * saturate( ( 1.0 - temp_output_128_0 ) ) );
					clip( saturate( OpacityMask27 ) - _MaskClipValue);
					

					fixed4 col = ( pow( tex2D( _MainTex, uv_MainTex ) , temp_cast_0 ) * _TintColor * i.color );
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18000
20;394;1331;566;4593.564;757.2635;3.906585;True;False
Node;AmplifyShaderEditor.CommentaryNode;142;-3184.716,-507.1367;Inherit;False;560.2888;322.753;Cutout;3;46;15;43;;0,1,0.1572132,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-3081.314,-457.1367;Float;False;Property;_AlphaCutout;Alpha Cutout;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-3134.716,-363.3837;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;78;-3101.07,-172.5572;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;96;-2808.987,-155.4186;Float;False;Property;_Alignment;Alignment;7;0;Create;True;0;0;False;0;0;0;0;True;;KeywordEnum;2;X;Y;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-2859.427,-452.1926;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;65;-2735.176,290.3732;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;61;-2744.495,-28.2289;Inherit;False;0;12;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;64;-2700.794,131.5439;Float;False;Property;_NoiseSpeed;Noise Speed;3;0;Create;True;0;0;False;0;0,0;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;79;-2554.662,-303.8715;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.28;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;80;-2421.579,-303.1217;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;62;-2450.783,121.2088;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;129;-2449.242,-220.6861;Float;False;Property;_Size;Size;8;0;Create;True;0;0;False;0;1;0.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-2038.781,-265.9724;Float;False;Property;_NoiseMax;Noise Max;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-2051.771,-351.4138;Float;False;Property;_NoiseMin;Noise Min;5;0;Create;True;0;0;False;0;0;0.35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;130;-2155.022,288.4718;Float;False;Property;_NoisePower;Noise Power;9;0;Create;True;0;0;False;0;1;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;128;-2254.667,-302.4662;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;12;-2266.483,94.42086;Inherit;True;Property;_NoiseTex;Noise Tex;0;0;Create;True;0;0;False;0;-1;None;163632276e446414db3976d5befc6048;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;81;-1810.972,-302.91;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-1946.613,138.3917;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;103;-2098.679,-122.8822;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1620.272,-189.1535;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;131;-1938.782,-124.6114;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-1409.885,-138.1117;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;139;-2137.42,558.2763;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;57;-1826.99,794.8143;Float;False;Property;_TexPower;Tex Power;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-1256.851,-140.9371;Float;False;OpacityMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;141;-1934.65,552.46;Inherit;True;Property;_TextureSample0;Texture Sample 0;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;140;-1549.984,805.3271;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;59;-1569.832,987.3086;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;26;-1310.238,850.8354;Inherit;False;27;OpacityMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;56;-1617.052,684.8545;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-1355.404,608.2515;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;88;-1099.628,856.5438;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;138;-1131.775,974.9332;Float;False;Property;_MaskClipValue;Mask Clip Value;4;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;137;-825.2498,605.6534;Inherit;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;135;-603.7475,607.5684;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Cutout/CutoutAlignment;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;96;1;78;1
WireConnection;96;0;78;2
WireConnection;43;0;15;0
WireConnection;43;1;46;3
WireConnection;79;0;96;0
WireConnection;79;1;43;0
WireConnection;80;0;79;0
WireConnection;62;0;61;0
WireConnection;62;2;64;0
WireConnection;62;1;65;2
WireConnection;128;0;80;0
WireConnection;128;1;129;0
WireConnection;12;1;62;0
WireConnection;81;0;128;0
WireConnection;81;1;82;0
WireConnection;81;2;83;0
WireConnection;126;0;12;1
WireConnection;126;1;130;0
WireConnection;103;0;128;0
WireConnection;19;0;126;0
WireConnection;19;1;81;0
WireConnection;131;0;103;0
WireConnection;123;0;19;0
WireConnection;123;1;131;0
WireConnection;27;0;123;0
WireConnection;141;0;139;0
WireConnection;56;0;141;0
WireConnection;56;1;57;0
WireConnection;75;0;56;0
WireConnection;75;1;140;0
WireConnection;75;2;59;0
WireConnection;88;0;26;0
WireConnection;137;0;75;0
WireConnection;137;1;88;0
WireConnection;137;2;138;0
WireConnection;135;0;137;0
ASEEND*/
//CHKSM=95F81E081FC41B1525809D07216B83F9A772DB5C