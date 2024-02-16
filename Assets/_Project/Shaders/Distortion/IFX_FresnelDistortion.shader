// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Distortion/FresnelDistortion"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_FresnelScale("Fresnel Scale", Range( 0 , 1)) = 0.510905
		_MainTilingOffset("Main Tiling Offset", Vector) = (1,1,0,0)
		_FresnelPower("Fresnel Power", Range( 0 , 5)) = 2
		_MainSpeed("Main Speed", Vector) = (0.1,0.1,0,0)
		_MainMultiply("Main Multiply", Range( 0 , 1)) = 0

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
			GrabPass{ }

			Pass {
			
				CGPROGRAM
				#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
				#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
				#else
				#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
				#endif

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
				ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
				uniform float _MainMultiply;
				uniform float2 _MainSpeed;
				uniform float4 _MainTilingOffset;
				uniform float _FresnelScale;
				uniform float _FresnelPower;
				inline float4 ASE_ComputeGrabScreenPos( float4 pos )
				{
					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					float4 o = pos;
					o.y = pos.w * 0.5f;
					o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
					return o;
				}
				


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
					float4 screenPos = ComputeScreenPos(ase_clipPos);
					o.ase_texcoord3 = screenPos;
					float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					o.ase_texcoord4.xyz = ase_worldPos;
					float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
					o.ase_texcoord5.xyz = ase_worldNormal;
					
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord4.w = 0;
					o.ase_texcoord5.w = 0;

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

					float2 uv067 = i.texcoord.xy * (_MainTilingOffset).xy + (_MainTilingOffset).zw;
					float2 panner96 = ( 1.0 * _Time.y * ( _Time.x * _MainSpeed ) + uv067);
					float4 screenPos = i.ase_texcoord3;
					float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
					float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
					float4 screenColor80 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( float4( ( _MainMultiply * UnpackNormal( tex2D( _MainTex, panner96 ) ) ) , 0.0 ) + ase_grabScreenPosNorm ).xy);
					float4 temp_output_98_0 = ( ( _TintColor + screenColor80 ) * i.color );
					float3 ase_worldPos = i.ase_texcoord4.xyz;
					float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
					ase_worldViewDir = normalize(ase_worldViewDir);
					float3 ase_worldNormal = i.ase_texcoord5.xyz;
					float fresnelNdotV62 = dot( ase_worldNormal, ase_worldViewDir );
					float fresnelNode62 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV62, _FresnelPower ) );
					float4 appendResult44 = (float4((temp_output_98_0).rgb , saturate( ( (temp_output_98_0).a * fresnelNode62 ) )));
					

					fixed4 col = appendResult44;
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
20;394;1331;566;1084.663;281.0221;1.425119;True;False
Node;AmplifyShaderEditor.Vector4Node;89;-2882.69,-30.24262;Float;False;Property;_MainTilingOffset;Main Tiling Offset;1;0;Create;True;0;0;False;0;1,1,0,0;2,2,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;91;-2621.274,10.49515;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;90;-2621.377,-77.771;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;73;-2702.875,285.2549;Float;False;Property;_MainSpeed;Main Speed;3;0;Create;True;0;0;False;0;0.1,0.1;-0.1,-0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;71;-2711.93,145.3965;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-2472.46,226.8966;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;67;-2328.761,-23.49689;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;88;-2104.96,-131.8232;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;96;-2074.246,64.36356;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;70;-1829.417,-126.1645;Float;False;Property;_MainMultiply;Main Multiply;4;0;Create;True;0;0;False;0;0;0.03;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;75;-1848.023,-22.60529;Inherit;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;-1;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-1543.138,-75.50249;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GrabScreenPosition;76;-1334.079,20.25392;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;79;-1007.47,-66.45084;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;86;-840.3879,-318.1241;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;80;-852.9886,26.14617;Float;False;Global;_GrabScreen1;Grab Screen 1;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;103;-1072.144,472.8016;Inherit;False;861.978;314.1815;Fresnel;4;62;65;66;95;;0,0,0,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;87;-626.6963,-86.83135;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;97;-636.4104,76.74129;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;65;-1015.958,565.6268;Float;False;Property;_FresnelScale;Fresnel Scale;0;0;Create;True;0;0;False;0;0.510905;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-1022.144,671.983;Float;False;Property;_FresnelPower;Fresnel Power;2;0;Create;True;0;0;False;0;2;1.2;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;-438.5561,-70.63257;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;62;-705.1653,522.8016;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;94;-226.7055,-35.26563;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-383.1698,501.2682;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;93;-219.7932,-141.1499;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;63;177.757,166.5219;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;44;345.5204,27.79636;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;85;559.0933,29.08446;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Distortion/FresnelDistortion;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;91;0;89;0
WireConnection;90;0;89;0
WireConnection;68;0;71;1
WireConnection;68;1;73;0
WireConnection;67;0;90;0
WireConnection;67;1;91;0
WireConnection;96;0;67;0
WireConnection;96;2;68;0
WireConnection;75;0;88;0
WireConnection;75;1;96;0
WireConnection;72;0;70;0
WireConnection;72;1;75;0
WireConnection;79;0;72;0
WireConnection;79;1;76;0
WireConnection;80;0;79;0
WireConnection;87;0;86;0
WireConnection;87;1;80;0
WireConnection;98;0;87;0
WireConnection;98;1;97;0
WireConnection;62;2;65;0
WireConnection;62;3;66;0
WireConnection;94;0;98;0
WireConnection;95;0;94;0
WireConnection;95;1;62;0
WireConnection;93;0;98;0
WireConnection;63;0;95;0
WireConnection;44;0;93;0
WireConnection;44;3;63;0
WireConnection;85;0;44;0
ASEEND*/
//CHKSM=47A1DD4E13D3EB1C7B7D20BADF9F4BA389A5CBE2