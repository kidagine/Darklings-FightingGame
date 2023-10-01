// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Cutout/GlowCutout"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_FresnelScale("Fresnel Scale", Range( 0 , 1)) = 0.510905
		_FresnelPower("Fresnel Power", Range( 0 , 5)) = 2
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_AlphaCutout("Alpha Cutout", Float) = 0
		_MaskClipValue("Mask Clip Value", Float) = 0.5
		_NoiseAdjust("Noise Adjust", Range( 0 , 1)) = 1
		[HDR]_GlowColor("Glow Color", Color) = (0,0,0,0)
		_MainSpeed("Main Speed", Vector) = (0,0,0,0)

	}


	Category 
	{
		SubShader
		{
		LOD 0

			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Back
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


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					float3 ase_normal : NORMAL;
					float4 ase_texcoord1 : TEXCOORD1;
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
					float4 ase_texcoord3 : TEXCOORD3;
					float4 ase_texcoord4 : TEXCOORD4;
					float4 ase_texcoord5 : TEXCOORD5;
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
				uniform float2 _MainSpeed;
				uniform float4 _GlowColor;
				uniform float _FresnelScale;
				uniform float _FresnelPower;
				uniform float _NoiseAdjust;
				uniform sampler2D _NoiseTex;
				uniform float4 _NoiseTex_ST;
				uniform float _AlphaCutout;
				uniform float _MaskClipValue;


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					o.ase_texcoord3.xyz = ase_worldPos;
					float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
					o.ase_texcoord4.xyz = ase_worldNormal;
					
					o.ase_texcoord5.xy = v.ase_texcoord1.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord3.w = 0;
					o.ase_texcoord4.w = 0;
					o.ase_texcoord5.zw = 0;

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

					float2 uv076 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float2 panner77 = ( _Time.y * _MainSpeed + uv076);
					float3 ase_worldPos = i.ase_texcoord3.xyz;
					float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
					ase_worldViewDir = normalize(ase_worldViewDir);
					float3 ase_worldNormal = i.ase_texcoord4.xyz;
					float fresnelNdotV90 = dot( ase_worldNormal, ase_worldViewDir );
					float fresnelNode90 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV90, _FresnelPower ) );
					float temp_output_91_0 = saturate( fresnelNode90 );
					float4 lerpResult50 = lerp( _GlowColor , ( _TintColor * i.color * temp_output_91_0 ) , temp_output_91_0);
					float lerpResult72 = lerp( _GlowColor.a , _TintColor.a , temp_output_91_0);
					float2 uv1_NoiseTex = i.ase_texcoord5.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
					float3 uv065 = i.texcoord.xyz;
					uv065.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					float smoothstepResult69 = smoothstep( 0.0 , _NoiseAdjust , ( tex2D( _NoiseTex, uv1_NoiseTex ).r - ( _AlphaCutout + uv065.z ) ));
					float4 appendResult83 = (float4(( tex2D( _MainTex, panner77 ) * lerpResult50 ).rgb , ( ( i.color.a * lerpResult72 ) * smoothstepResult69 )));
					clip( smoothstepResult69 - _MaskClipValue);
					

					fixed4 col = appendResult83;
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
20;394;1331;566;2024.449;567.8724;3.212274;True;False
Node;AmplifyShaderEditor.RangedFloatNode;88;-1847.368,450.7298;Float;False;Property;_FresnelScale;Fresnel Scale;0;0;Create;True;0;0;False;0;0.510905;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;93;-1457.382,921.9526;Inherit;False;639.7019;358.579;Alpha Cutout;3;65;58;56;;0.05291831,1,0,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-1839.389,531.5894;Float;False;Property;_FresnelPower;Fresnel Power;1;0;Create;True;0;0;False;0;2;0.3;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;92;-1750.85,289.7599;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;57;-1758.938,701.829;Inherit;False;1;59;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;56;-1365.879,971.9526;Float;False;Property;_AlphaCutout;Alpha Cutout;3;0;Create;True;0;0;False;0;0;-0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;90;-1510.85,289.7599;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;65;-1407.382,1101.532;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;79;-337.3557,-303.7734;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;58;-1052.68,1023.126;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;52;-1252.481,-243.2147;Float;False;Property;_GlowColor;Glow Color;7;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;91;-1190.85,289.7599;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;19;-1223.824,-659.1197;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;76;-373.0832,-575.9539;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;86;-1207.299,-465.2904;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;78;-293.4479,-442.0886;Float;False;Property;_MainSpeed;Main Speed;8;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;59;-1503.932,679.8417;Inherit;True;Property;_NoiseTex;Noise Tex;2;0;Create;True;0;0;False;0;-1;140c5b15ccac91a4fb58b5ea4666c02f;140c5b15ccac91a4fb58b5ea4666c02f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;94;-930.5882,838.1804;Float;False;Property;_NoiseAdjust;Noise Adjust;6;0;Create;True;0;0;False;0;1;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-862.1118,-549.5767;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;61;-855.0975,714.526;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;85;-111.1167,-632.9122;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;77;-51.04938,-459.3573;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;72;-665.7707,129.3181;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-131.2187,115.3599;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;69;-618.5609,709.5107;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;50;-122.0952,-93.459;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;87;143.3926,-535.726;Inherit;True;Property;_TextureSample0;Texture Sample 0;7;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;367.0875,81.00702;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;523.1132,-108.2283;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;83;717.7289,-33.22547;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;82;744.1886,366.639;Float;False;Property;_MaskClipValue;Mask Clip Value;5;0;Create;True;0;0;False;0;0.5;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;84;964.3135,138.0016;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;70;-18.24366,561.8123;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;49;-1150.519,414.581;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-652.4562,413.1635;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;51;-881.1391,412.9941;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;71;-291.0021,390.6394;Float;False;Property;_CutoutFresnel;Cutout Fresnel;4;0;Create;True;0;0;False;0;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;81;1151.002,5.858412;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Cutout/GlowCutout;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;90;0;92;0
WireConnection;90;2;88;0
WireConnection;90;3;89;0
WireConnection;58;0;56;0
WireConnection;58;1;65;3
WireConnection;91;0;90;0
WireConnection;59;1;57;0
WireConnection;18;0;86;0
WireConnection;18;1;19;0
WireConnection;18;2;91;0
WireConnection;61;0;59;1
WireConnection;61;1;58;0
WireConnection;77;0;76;0
WireConnection;77;2;78;0
WireConnection;77;1;79;2
WireConnection;72;0;52;4
WireConnection;72;1;86;4
WireConnection;72;2;91;0
WireConnection;55;0;19;4
WireConnection;55;1;72;0
WireConnection;69;0;61;0
WireConnection;69;2;94;0
WireConnection;50;0;52;0
WireConnection;50;1;18;0
WireConnection;50;2;91;0
WireConnection;87;0;85;0
WireConnection;87;1;77;0
WireConnection;97;0;55;0
WireConnection;97;1;69;0
WireConnection;74;0;87;0
WireConnection;74;1;50;0
WireConnection;83;0;74;0
WireConnection;83;3;97;0
WireConnection;84;0;83;0
WireConnection;84;1;69;0
WireConnection;84;2;82;0
WireConnection;70;0;51;0
WireConnection;70;1;69;0
WireConnection;49;0;91;0
WireConnection;67;0;51;0
WireConnection;67;1;69;0
WireConnection;51;0;49;0
WireConnection;71;0;69;0
WireConnection;71;1;67;0
WireConnection;81;0;84;0
ASEEND*/
//CHKSM=A603A32F54A0B9130F20DD564EA4B709DBD5CF7C