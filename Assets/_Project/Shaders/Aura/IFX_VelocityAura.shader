// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Aura/VelocityAura"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_MainTilingOffset("Main Tiling Offset", Vector) = (1,1,0,0)
		_Noise1Texture("Noise 1 Texture", 2D) = "white" {}
		_Noise2Texture("Noise 2 Texture", 2D) = "white" {}
		_Speed("Speed", Vector) = (0,0,0,0)
		_MaskTextureOffset("Mask Texture Offset", Float) = 0
		[KeywordEnum(Multiply,Lerp,Add)] _NoiseBlend("NoiseBlend", Float) = 0
		_Adjust("Adjust", Range( 0 , 1)) = 0
		_SmoothstepMin("Smoothstep Min", Range( 0 , 1)) = 0
		_SmoothstepMax("Smoothstep Max", Range( 0 , 1)) = 1
		[KeywordEnum(U,V)] _GradientUV("Gradient UV", Float) = 0
		_UVGradientOffset("UV Gradient Offset", Range( -1 , 1)) = 0
		_UVGradientAdjust("UV Gradient Adjust", Range( 0 , 1)) = 0
		[KeywordEnum(Noise1,Noise2,BlendNoise)] _NoiseOpacity("NoiseOpacity", Float) = 0
		_Opacity("Opacity", Range( 0 , 1)) = 0
		[Toggle(_USENOISEUVGRADIENT_ON)] _UseNoiseUVGradient("Use Noise UV Gradient", Float) = 0
		_RampMap("Ramp Map", 2D) = "white" {}
		[KeywordEnum(U,V)] _MaskTextureOffsetAxis("Mask Texture Offset Axis", Float) = 0
		[Toggle(_USENOISEOPACITY_ON)] _UseNoiseOpacity("Use Noise Opacity", Float) = 1
		[Toggle(_BLENDTEXTURES_ON)] _BlendTextures("Blend Textures", Float) = 0
		[Toggle(_INVERTGRADIENT_ON)] _InvertGradient("Invert Gradient", Float) = 0

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
				#pragma shader_feature_local _BLENDTEXTURES_ON
				#pragma shader_feature_local _NOISEBLEND_MULTIPLY _NOISEBLEND_LERP _NOISEBLEND_ADD
				#pragma shader_feature_local _MASKTEXTUREOFFSETAXIS_U _MASKTEXTUREOFFSETAXIS_V
				#pragma shader_feature_local _USENOISEUVGRADIENT_ON
				#pragma shader_feature_local _INVERTGRADIENT_ON
				#pragma shader_feature_local _GRADIENTUV_U _GRADIENTUV_V
				#pragma shader_feature_local _USENOISEOPACITY_ON
				#pragma shader_feature_local _NOISEOPACITY_NOISE1 _NOISEOPACITY_NOISE2 _NOISEOPACITY_BLENDNOISE


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
				uniform sampler2D _RampMap;
				uniform float4 _MainTilingOffset;
				uniform sampler2D _Noise1Texture;
				uniform float4 _Speed;
				uniform float4 _Noise1Texture_ST;
				uniform sampler2D _Noise2Texture;
				uniform float4 _Noise2Texture_ST;
				uniform float _MaskTextureOffset;
				uniform float _SmoothstepMin;
				uniform float _SmoothstepMax;
				uniform float _Adjust;
				uniform float _UVGradientAdjust;
				uniform float _UVGradientOffset;
				uniform float _Opacity;


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

					float2 break100 = (_MainTilingOffset).zw;
					float2 appendResult102 = (float2(break100.x , break100.y));
					float2 uv0104 = i.texcoord.xy * (_MainTilingOffset).xy + appendResult102;
					float2 uv0_Noise1Texture = i.texcoord.xy * _Noise1Texture_ST.xy + _Noise1Texture_ST.zw;
					float2 panner13 = ( 1.0 * _Time.y * (_Speed).xy + uv0_Noise1Texture);
					float4 tex2DNode5 = tex2D( _Noise1Texture, panner13 );
					float2 uv0_Noise2Texture = i.texcoord.xy * _Noise2Texture_ST.xy + _Noise2Texture_ST.zw;
					float2 panner14 = ( 1.0 * _Time.y * (_Speed).zw + uv0_Noise2Texture);
					float4 tex2DNode6 = tex2D( _Noise2Texture, panner14 );
					float lerpResult74 = lerp( tex2DNode5.r , tex2DNode6.r , 0.5);
					#if defined(_NOISEBLEND_MULTIPLY)
					float staticSwitch76 = ( tex2DNode5.r * tex2DNode6.r );
					#elif defined(_NOISEBLEND_LERP)
					float staticSwitch76 = lerpResult74;
					#elif defined(_NOISEBLEND_ADD)
					float staticSwitch76 = ( tex2DNode5.r + tex2DNode6.r );
					#else
					float staticSwitch76 = ( tex2DNode5.r * tex2DNode6.r );
					#endif
					float4 uv079 = i.texcoord;
					uv079.xy = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_81_0 = ( uv079.z + _MaskTextureOffset );
					float2 appendResult172 = (float2(0.0 , temp_output_81_0));
					float2 appendResult82 = (float2(temp_output_81_0 , 0.0));
					#if defined(_MASKTEXTUREOFFSETAXIS_U)
					float2 staticSwitch171 = appendResult172;
					#elif defined(_MASKTEXTUREOFFSETAXIS_V)
					float2 staticSwitch171 = appendResult82;
					#else
					float2 staticSwitch171 = appendResult172;
					#endif
					float2 uv0128 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					#if defined(_MASKTEXTUREOFFSETAXIS_U)
					float staticSwitch175 = uv0128.y;
					#elif defined(_MASKTEXTUREOFFSETAXIS_V)
					float staticSwitch175 = uv0128.x;
					#else
					float staticSwitch175 = uv0128.y;
					#endif
					#ifdef _USENOISEUVGRADIENT_ON
					float staticSwitch133 = staticSwitch175;
					#else
					float staticSwitch133 = 1.0;
					#endif
					float4 tex2DNode26 = tex2D( _MainTex, ( uv0104 + ( ( staticSwitch76 * staticSwitch171 ) * staticSwitch133 ) ) );
					float smoothstepResult94 = smoothstep( _SmoothstepMin , _SmoothstepMax , saturate( ( tex2DNode26.r - _Adjust ) ));
					float4 temp_cast_0 = (( tex2DNode26.r + smoothstepResult94 )).xxxx;
					#ifdef _BLENDTEXTURES_ON
					float4 staticSwitch184 = temp_cast_0;
					#else
					float4 staticSwitch184 = tex2DNode26;
					#endif
					float2 appendResult140 = (float2(staticSwitch184.r , 0.0));
					float4 temp_output_20_0 = ( i.color * _TintColor * tex2D( _RampMap, appendResult140 ) * staticSwitch184 );
					float2 uv028 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					#if defined(_GRADIENTUV_U)
					float staticSwitch83 = uv028.x;
					#elif defined(_GRADIENTUV_V)
					float staticSwitch83 = uv028.y;
					#else
					float staticSwitch83 = uv028.x;
					#endif
					#ifdef _INVERTGRADIENT_ON
					float staticSwitch185 = ( 1.0 - staticSwitch83 );
					#else
					float staticSwitch185 = staticSwitch83;
					#endif
					float lerpResult56 = lerp( _UVGradientOffset , 1.0 , staticSwitch185);
					float smoothstepResult87 = smoothstep( 0.0 , _UVGradientAdjust , lerpResult56);
					float gradient63 = smoothstepResult87;
					#if defined(_NOISEOPACITY_NOISE1)
					float staticSwitch156 = tex2DNode5.r;
					#elif defined(_NOISEOPACITY_NOISE2)
					float staticSwitch156 = tex2DNode6.r;
					#elif defined(_NOISEOPACITY_BLENDNOISE)
					float staticSwitch156 = staticSwitch76;
					#else
					float staticSwitch156 = tex2DNode5.r;
					#endif
					float noise_opacity67 = staticSwitch156;
					float4 uv053 = i.texcoord;
					uv053.xy = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					#ifdef _USENOISEOPACITY_ON
					float staticSwitch183 = step( saturate( noise_opacity67 ) , ( 1.0 - ( _Opacity + uv053.w ) ) );
					#else
					float staticSwitch183 = 1.0;
					#endif
					float4 appendResult32 = (float4((temp_output_20_0).rgb , saturate( ( (temp_output_20_0).a * gradient63 * staticSwitch183 ) )));
					

					fixed4 col = appendResult32;
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
20;394;1331;566;2712.634;175.3141;4.272663;True;False
Node;AmplifyShaderEditor.Vector4Node;9;-2700.473,-168.9056;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-2374.711,-124.2246;Inherit;False;0;6;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-2390.769,-250.4056;Inherit;False;0;5;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;77;-1541.776,389.7886;Inherit;False;497.0916;324.395;Mask Texture Offset;3;81;79;78;;0.4552704,1,0,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;10;-2390.659,-362.6944;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;12;-2388.941,14.47116;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;13;-2053.911,-246.0867;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;96;-1058.308,-1181.846;Inherit;False;1317.168;614.0859;Offset;8;104;103;102;101;100;99;98;70;;0.1206665,1,0,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;79;-1491.776,439.7886;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;14;-2060.911,-119.0869;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-1488.004,609.5051;Float;False;Property;_MaskTextureOffset;Mask Texture Offset;4;0;Create;True;0;0;False;0;0;-0.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;81;-1198.686,505.9135;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-1795.513,50.17901;Inherit;True;Property;_Noise2Texture;Noise 2 Texture;2;0;Create;True;0;0;False;0;-1;None;163632276e446414db3976d5befc6048;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-1800.482,-320.0666;Inherit;True;Property;_Noise1Texture;Noise 1 Texture;1;0;Create;True;0;0;False;0;-1;None;6da64f90d187eda4a85ce02732d2883c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;70;-1024.803,-851.4699;Float;False;Property;_MainTilingOffset;Main Tiling Offset;0;0;Create;True;0;0;False;0;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;80;-1222.944,239.7939;Float;False;Constant;_Float1;Float 1;19;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;74;-1152.315,-8.958941;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1151.117,-123.178;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;128;-879.3794,514.0682;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;82;-1002.152,385.3258;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;98;-760.9619,-954.8849;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;172;-1004.763,273.2205;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;75;-1147.991,118.2775;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;76;-942.1488,-37.27603;Float;False;Property;_NoiseBlend;NoiseBlend;5;0;Create;True;0;0;False;0;0;0;0;True;;KeywordEnum;3;Multiply;Lerp;Add;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;171;-823.7712,291.7636;Float;False;Property;_MaskTextureOffsetAxis;Mask Texture Offset Axis;16;0;Create;True;0;0;False;0;0;0;0;True;;KeywordEnum;2;U;V;Create;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;134;-380.0703,276.4225;Float;False;Constant;_Float2;Float 2;17;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;175;-656.8396,530.7057;Float;False;Property;_Keyword0;Keyword 0;16;0;Create;True;0;0;False;0;0;0;0;True;;KeywordEnum;2;U;V;Reference;171;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;100;-541.009,-950.675;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.StaticSwitch;133;-146.9348,216.032;Float;False;Property;_UseNoiseUVGradient;Use Noise UV Gradient;14;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;102;-116.6632,-951.3055;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;103;-776.4373,-707.7496;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-222.7904,-35.90864;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;104;0.2337687,-723.7604;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;207.6346,-39.18714;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;25;464.9892,-540.976;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;23;458.7184,-442.0241;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;89;723.0566,-228.4292;Float;False;Property;_Adjust;Adjust;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;724.4151,-468.8641;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;-1;76d073b7506b31444a45e9077ac1d0e0;76d073b7506b31444a45e9077ac1d0e0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;65;195.3472,808.5087;Inherit;False;1962.962;589.7911;Gradient;9;85;56;63;87;86;58;83;28;185;;0,0,0,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;90;1032.627,-353.4002;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;92;1022.428,-197.6338;Float;False;Property;_SmoothstepMin;Smoothstep Min;7;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;245.3471,864.8614;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;91;1188.392,-349.6252;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;1017.212,-94.97769;Float;False;Property;_SmoothstepMax;Smoothstep Max;8;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;94;1349.05,-351.5741;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;83;483.941,883.08;Float;False;Property;_GradientUV;Gradient UV;9;0;Create;True;0;0;False;0;0;0;0;True;;KeywordEnum;2;U;V;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;156;-890.8839,-391.8233;Float;False;Property;_NoiseOpacity;NoiseOpacity;12;0;Create;True;0;0;False;0;0;0;0;True;;KeywordEnum;3;Noise1;Noise2;BlendNoise;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;95;1547.99,-373.7068;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;66;1786.136,-65.8083;Inherit;False;655.0621;336.315;Opacity Power;4;48;53;52;51;;0.07418633,1,0,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;58;664.4598,995.8558;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;85;919.5829,990.5677;Float;False;Property;_UVGradientOffset;UV Gradient Offset;10;0;Create;True;0;0;False;0;0;0.12;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;184;1662.183,-475.7359;Inherit;False;Property;_BlendTextures;Blend Textures;19;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;67;-630.037,-392.6435;Float;True;noise_opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;53;1836.134,91.50676;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;1847.655,-15.80825;Float;False;Property;_Opacity;Opacity;13;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;141;1746.459,-724.8311;Float;False;Constant;_Float3;Float 3;18;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;185;816.0759,881.8187;Inherit;False;Property;_InvertGradient;Invert Gradient;20;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;56;1235.733,845.7067;Inherit;True;3;0;FLOAT;-0.1;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;140;1921.425,-668.5898;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;2122.004,17.4686;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;1290.708,1121.195;Float;False;Property;_UVGradientAdjust;UV Gradient Adjust;11;0;Create;True;0;0;False;0;0;0.74;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;157;1991.932,-370.8441;Inherit;True;67;noise_opacity;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;42;2330.02,-1137.207;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;87;1635.574,859.9794;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;3;2308.659,-914.0186;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;136;2082.96,-691.527;Inherit;True;Property;_RampMap;Ramp Map;15;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;182;2210.93,-318.3732;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;51;2254.197,17.80078;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;63;1934.939,856.6939;Float;False;gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;2688.538,-533.7701;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;169;2494.939,-20.63686;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;119;2623.569,-150.1439;Float;False;Constant;_Float0;Float 0;18;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;183;2794.226,-52.44406;Inherit;False;Property;_UseNoiseOpacity;Use Noise Opacity;18;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;64;2780.138,-257.1816;Inherit;False;63;gradient;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;31;2927.862,-483.4639;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;3091.647,-174.5295;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;37;3258.238,-375.7113;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;30;2940.072,-618.6427;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;99;-555.7181,-1103.225;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;101;-309.0998,-1025.292;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;177;-521.2391,700.692;Float;False;Property;_Float4;Float 4;17;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;32;3435.301,-608.7283;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;176;-313.542,562.2272;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;3620.309,-610.2775;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Aura/VelocityAura;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;10;0;9;0
WireConnection;12;0;9;0
WireConnection;13;0;8;0
WireConnection;13;2;10;0
WireConnection;14;0;21;0
WireConnection;14;2;12;0
WireConnection;81;0;79;3
WireConnection;81;1;78;0
WireConnection;6;1;14;0
WireConnection;5;1;13;0
WireConnection;74;0;5;1
WireConnection;74;1;6;1
WireConnection;73;0;5;1
WireConnection;73;1;6;1
WireConnection;82;0;81;0
WireConnection;82;1;80;0
WireConnection;98;0;70;0
WireConnection;172;0;80;0
WireConnection;172;1;81;0
WireConnection;75;0;5;1
WireConnection;75;1;6;1
WireConnection;76;1;73;0
WireConnection;76;0;74;0
WireConnection;76;2;75;0
WireConnection;171;1;172;0
WireConnection;171;0;82;0
WireConnection;175;1;128;2
WireConnection;175;0;128;1
WireConnection;100;0;98;0
WireConnection;133;1;134;0
WireConnection;133;0;175;0
WireConnection;102;0;100;0
WireConnection;102;1;100;1
WireConnection;103;0;70;0
WireConnection;27;0;76;0
WireConnection;27;1;171;0
WireConnection;104;0;103;0
WireConnection;104;1;102;0
WireConnection;125;0;27;0
WireConnection;125;1;133;0
WireConnection;23;0;104;0
WireConnection;23;1;125;0
WireConnection;26;0;25;0
WireConnection;26;1;23;0
WireConnection;90;0;26;1
WireConnection;90;1;89;0
WireConnection;91;0;90;0
WireConnection;94;0;91;0
WireConnection;94;1;92;0
WireConnection;94;2;93;0
WireConnection;83;1;28;1
WireConnection;83;0;28;2
WireConnection;156;1;5;1
WireConnection;156;0;6;1
WireConnection;156;2;76;0
WireConnection;95;0;26;1
WireConnection;95;1;94;0
WireConnection;58;0;83;0
WireConnection;184;1;26;0
WireConnection;184;0;95;0
WireConnection;67;0;156;0
WireConnection;185;1;83;0
WireConnection;185;0;58;0
WireConnection;56;0;85;0
WireConnection;56;2;185;0
WireConnection;140;0;184;0
WireConnection;140;1;141;0
WireConnection;52;0;48;0
WireConnection;52;1;53;4
WireConnection;87;0;56;0
WireConnection;87;2;86;0
WireConnection;136;1;140;0
WireConnection;182;0;157;0
WireConnection;51;0;52;0
WireConnection;63;0;87;0
WireConnection;20;0;42;0
WireConnection;20;1;3;0
WireConnection;20;2;136;0
WireConnection;20;3;184;0
WireConnection;169;0;182;0
WireConnection;169;1;51;0
WireConnection;183;1;119;0
WireConnection;183;0;169;0
WireConnection;31;0;20;0
WireConnection;36;0;31;0
WireConnection;36;1;64;0
WireConnection;36;2;183;0
WireConnection;37;0;36;0
WireConnection;30;0;20;0
WireConnection;101;0;99;1
WireConnection;101;1;100;1
WireConnection;32;0;30;0
WireConnection;32;3;37;0
WireConnection;176;0;175;0
WireConnection;176;1;177;0
WireConnection;1;0;32;0
ASEEND*/
//CHKSM=17C52919C8D943FE1A011FA007CF066A3BC9EF23