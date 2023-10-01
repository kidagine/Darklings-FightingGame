// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Aura/WindAura"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_Speed("Speed", Vector) = (0,0,0,0)
		_UVGradientOffset("UV Gradient Offset", Float) = 0
		_MainTilingOffset("Main Tiling Offset", Vector) = (1,1,0,0)
		_Cutout("Cutout", Float) = 0

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
				uniform float _UVGradientOffset;
				uniform float4 _Speed;
				uniform float4 _MainTilingOffset;
				uniform float _Cutout;


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

					float2 uv017 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float lerpResult18 = lerp( _UVGradientOffset , 1.0 , ( 1.0 - uv017.y ));
					float lerpResult65 = lerp( _UVGradientOffset , 1.0 , uv017.y);
					float gradient62 = saturate( ( lerpResult18 * lerpResult65 * 4.0 ) );
					float2 uv016 = i.texcoord.xy * (_MainTilingOffset).xy + (_MainTilingOffset).zw;
					float2 panner12 = ( 1.0 * _Time.y * (_Speed).xy + uv016);
					float3 uv046 = i.texcoord.xyz;
					uv046.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					float4 temp_output_4_0 = ( _TintColor * i.color * gradient62 * saturate( ( tex2D( _MainTex, panner12 ).r - ( uv046.z + _Cutout ) ) ) );
					float4 appendResult32 = (float4((temp_output_4_0).rgb , saturate( (temp_output_4_0).a )));
					

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
20;394;1331;566;1231.797;33.57639;1.780222;True;False
Node;AmplifyShaderEditor.CommentaryNode;48;-1243.862,834.7735;Inherit;False;1464.539;493.1131;Gradient;9;62;23;66;65;18;72;20;22;17;;0,0,0,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1203.845,935.4179;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;59;-1942.094,306.5463;Float;False;Property;_MainTilingOffset;Main Tiling Offset;2;0;Create;True;0;0;False;0;1,1,0,0;3,0.3,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;22;-929.5161,981.6954;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-937.2859,878.0404;Float;False;Property;_UVGradientOffset;UV Gradient Offset;1;0;Create;True;0;0;False;0;0;-0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;60;-1680.784,259.0179;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;61;-1679.052,353.797;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;13;-1530.348,466.3523;Float;False;Property;_Speed;Speed;0;0;Create;True;0;0;False;0;0,0,0,0;0,-0.5,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;49;-822.4783,157.9966;Inherit;False;944.5719;465.3857;Cutout;6;64;56;46;10;44;57;;0.1468607,1,0,1;0;0
Node;AmplifyShaderEditor.LerpOp;18;-686.4224,937.0211;Inherit;False;3;0;FLOAT;-0.1;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;65;-689.8056,1076.42;Inherit;False;3;0;FLOAT;-0.1;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;14;-1346.516,465.8571;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-1419.863,273.675;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;72;-680.8926,1230.224;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-436.3168,949.6754;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;2;-1059.146,336.6135;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;12;-1113.561,448.9037;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-578.824,349.7815;Float;False;Property;_Cutout;Cutout;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-792.261,202.2077;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-394.3698,271.9264;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;10;-792.7047,422.1701;Inherit;True;Property;_NoiseTexture;Noise Texture;1;0;Create;True;0;0;False;0;-1;e399c37bf0955764da18ac5a6f418997;e399c37bf0955764da18ac5a6f418997;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;23;-218.4791,949.8911;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;44;-251.8613,446.3027;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;62;-32.13865,943.3359;Float;True;gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;64;-78.62965,444.2081;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;63;-245.8122,16.987;Inherit;False;62;gradient;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;5;-251.1524,-185.6867;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-237.2082,-359.3757;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;64.22108,-88.05323;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;31;416.073,-3.344194;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;30;413.7916,-149.1723;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;34;674.3071,3.550571;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;32;878.5688,-143.3803;Inherit;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;47;1197.062,-145.223;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Aura/WindAura;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;22;0;17;2
WireConnection;60;0;59;0
WireConnection;61;0;59;0
WireConnection;18;0;20;0
WireConnection;18;2;22;0
WireConnection;65;0;20;0
WireConnection;65;2;17;2
WireConnection;14;0;13;0
WireConnection;16;0;60;0
WireConnection;16;1;61;0
WireConnection;66;0;18;0
WireConnection;66;1;65;0
WireConnection;66;2;72;0
WireConnection;12;0;16;0
WireConnection;12;2;14;0
WireConnection;56;0;46;3
WireConnection;56;1;57;0
WireConnection;10;0;2;0
WireConnection;10;1;12;0
WireConnection;23;0;66;0
WireConnection;44;0;10;1
WireConnection;44;1;56;0
WireConnection;62;0;23;0
WireConnection;64;0;44;0
WireConnection;4;0;1;0
WireConnection;4;1;5;0
WireConnection;4;2;63;0
WireConnection;4;3;64;0
WireConnection;31;0;4;0
WireConnection;30;0;4;0
WireConnection;34;0;31;0
WireConnection;32;0;30;0
WireConnection;32;3;34;0
WireConnection;47;0;32;0
ASEEND*/
//CHKSM=35CD04D1D9B98DC0BC71A544B2A72671F85DE7A8