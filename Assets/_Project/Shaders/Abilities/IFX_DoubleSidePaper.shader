// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Abilities/DoubleSidePaper"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		[KeywordEnum(None,X,Y,Z)] _MaskAxis("Mask Axis", Float) = 2
		[HDR]_DissolveColor("Dissolve Color", Color) = (1,1,1,1)
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_DissolveEdgeWidth("Dissolve Edge Width", Range( 0 , 1)) = 0
		_MaskClipValue("Mask Clip Value", Float) = 0.5
		_MainTexBack("Main Tex Back", 2D) = "white" {}
		_MaskOffset("Mask Offset", Float) = 0
		[Toggle(_MASKINVERT_ON)] _MaskInvert("Mask Invert", Float) = 0
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
				#pragma shader_feature_local _MASKAXIS_NONE _MASKAXIS_X _MASKAXIS_Y _MASKAXIS_Z
				#pragma shader_feature_local _MASKINVERT_ON


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
					float4 ase_texcoord3 : TEXCOORD3;
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
				uniform float _DissolveEdgeWidth;
				uniform sampler2D _NoiseTex;
				uniform float4 _NoiseTex_ST;
				uniform float _MaskOffset;
				uniform float4 _DissolveColor;
				uniform sampler2D _MainTexBack;
				uniform float4 _MainTexBack_ST;
				uniform float _MaskClipValue;


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					o.ase_texcoord3.xyz = ase_worldPos;
					
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord3.w = 0;

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

				fixed4 frag ( v2f i , half ase_vface : VFACE ) : SV_Target
				{
					#ifdef _USESOFTPARTICLES_ON
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif
					#endif

					float2 uv_NoiseTex = i.texcoord.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
					float4 transform31 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
					float3 ase_worldPos = i.ase_texcoord3.xyz;
					#ifdef _MASKINVERT_ON
					float4 staticSwitch76 = ( float4( ase_worldPos , 0.0 ) - transform31 );
					#else
					float4 staticSwitch76 = ( transform31 - float4( ase_worldPos , 0.0 ) );
					#endif
					float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
					float3 uv074 = i.texcoord.xyz;
					uv074.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					float4 temp_output_35_0 = ( ( staticSwitch76 / float4( ase_objectScale , 0.0 ) ) + ( _MaskOffset + uv074.z ) );
					float4 break24 = temp_output_35_0;
					float4 temp_cast_3 = (break24.y).xxxx;
					float4 temp_cast_5 = (break24.x).xxxx;
					float4 temp_cast_6 = (break24.y).xxxx;
					float4 temp_cast_7 = (break24.z).xxxx;
					#if defined(_MASKAXIS_NONE)
					float4 staticSwitch37 = temp_output_35_0;
					#elif defined(_MASKAXIS_X)
					float4 staticSwitch37 = temp_cast_5;
					#elif defined(_MASKAXIS_Y)
					float4 staticSwitch37 = temp_cast_3;
					#elif defined(_MASKAXIS_Z)
					float4 staticSwitch37 = temp_cast_7;
					#else
					float4 staticSwitch37 = temp_cast_3;
					#endif
					float temp_output_68_0 = ( tex2D( _NoiseTex, uv_NoiseTex ).r - ( staticSwitch37 - float4( 0,0,0,0 ) ).x );
					float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float2 uv_MainTexBack = i.texcoord.xy * _MainTexBack_ST.xy + _MainTexBack_ST.zw;
					float4 switchResult5 = (((ase_vface>0)?(( _TintColor * tex2D( _MainTex, uv_MainTex ) )):(( _TintColor * tex2D( _MainTexBack, uv_MainTexBack ) ))));
					clip( temp_output_68_0 - _MaskClipValue);
					

					fixed4 col = (( _DissolveEdgeWidth > temp_output_68_0 ) ? _DissolveColor :  switchResult5 );
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
20;394;1331;566;3801.113;-871.4601;4.88558;True;False
Node;AmplifyShaderEditor.CommentaryNode;58;-2013.188,2014.425;Inherit;False;2715.113;805.891;Mask;15;33;37;24;28;35;30;27;32;29;31;26;70;68;66;76;;0,0,0,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;26;-1947.188,2064.425;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;31;-1963.188,2256.425;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;75;-1782.639,2838.891;Inherit;False;511.9041;303.314;Mask Offset;3;73;40;74;;0.02666688,1,0,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;29;-1691.188,2272.425;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;32;-1691.188,2144.425;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;76;-1532.436,2169.957;Inherit;False;Property;_MaskInvert;Mask Invert;7;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;9;1;FLOAT4;0,0,0,0;False;0;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;5;FLOAT4;0,0,0,0;False;6;FLOAT4;0,0,0,0;False;7;FLOAT4;0,0,0,0;False;8;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1671.156,2912.992;Float;False;Property;_MaskOffset;Mask Offset;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;74;-1728.04,2987.306;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ObjectScaleNode;27;-1522.188,2356.425;Inherit;False;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;-1291.188,2304.426;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-1420.135,2916.346;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-1099.188,2304.426;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.BreakToComponentsNode;24;-875.187,2304.426;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.WireNode;28;-866.7652,2182.358;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;37;-588.1871,2271.425;Float;True;Property;_MaskAxis;Mask Axis;0;0;Create;True;0;0;False;0;0;2;2;True;;KeywordEnum;4;None;X;Y;Z;Create;True;9;1;FLOAT4;0,0,0,0;False;0;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;5;FLOAT4;0,0,0,0;False;6;FLOAT4;0,0,0,0;False;7;FLOAT4;0,0,0,0;False;8;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;71;-587.7303,1534.218;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-323.9591,1735.709;Inherit;True;Property;_MainTexBack;Main Tex Back;5;0;Create;True;0;0;False;0;-1;None;42af91b0e8f7dcf45bd50f72836a2ab4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-327.3858,1536.108;Inherit;True;Property;_MainTexFront;Main Tex Front;8;0;Create;True;0;0;False;0;-1;None;83e349c7bcb87a84291c70e9b5d13fb3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;33;-299.1873,2368.426;Inherit;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;72;-316.6748,1357.48;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;50.59363,1670.91;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;70;-43.82788,2372.031;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SamplerNode;66;-110.46,2113.729;Inherit;True;Property;_NoiseTex;Noise Tex;2;0;Create;True;0;0;False;0;-1;None;d784595d7b8bfef41ac0a5bd8fa0a662;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;57.56772,1478.102;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;68;330.5199,2350.316;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;54;230.6865,1289.383;Float;False;Property;_DissolveColor;Dissolve Color;1;1;[HDR];Create;True;0;0;False;0;1,1,1,1;4,2.776471,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwitchByFaceNode;5;337.6733,1516.02;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;52;164.3023,1201.22;Float;False;Property;_DissolveEdgeWidth;Dissolve Edge Width;3;0;Create;True;0;0;False;0;0;0.6;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;713.332,1685.074;Float;False;Property;_MaskClipValue;Mask Clip Value;4;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareGreater;56;664.4832,1449.957;Inherit;True;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClipNode;48;1121.882,1451.14;Inherit;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;57;1323.411,1453.628;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Abilities/DoubleSidePaper;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;29;0;26;0
WireConnection;29;1;31;0
WireConnection;32;0;31;0
WireConnection;32;1;26;0
WireConnection;76;1;32;0
WireConnection;76;0;29;0
WireConnection;30;0;76;0
WireConnection;30;1;27;0
WireConnection;73;0;40;0
WireConnection;73;1;74;3
WireConnection;35;0;30;0
WireConnection;35;1;73;0
WireConnection;24;0;35;0
WireConnection;28;0;35;0
WireConnection;37;1;28;0
WireConnection;37;0;24;0
WireConnection;37;2;24;1
WireConnection;37;3;24;2
WireConnection;3;0;71;0
WireConnection;33;0;37;0
WireConnection;21;0;72;0
WireConnection;21;1;6;0
WireConnection;70;0;33;0
WireConnection;4;0;72;0
WireConnection;4;1;3;0
WireConnection;68;0;66;1
WireConnection;68;1;70;0
WireConnection;5;0;4;0
WireConnection;5;1;21;0
WireConnection;56;0;52;0
WireConnection;56;1;68;0
WireConnection;56;2;54;0
WireConnection;56;3;5;0
WireConnection;48;0;56;0
WireConnection;48;1;68;0
WireConnection;48;2;46;0
WireConnection;57;0;48;0
ASEEND*/
//CHKSM=82791869F71EF70F119B05F30B89B8785DA9D02E