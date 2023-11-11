// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Decal/DepthDecalDistortion"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_Size("Size", Range( 0 , 1)) = 1
		_RampFalloff("Ramp Falloff", Range( 0.01 , 14)) = 5
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_NoiseSpeed("Noise Speed", Vector) = (0.5,0.5,0,0)
		_Offset("Offset", Float) = 0
		_NoiseMultiply("Noise Multiply", Float) = 0.45

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
				uniform sampler2D _NoiseTex;
				uniform float2 _NoiseSpeed;
				uniform float _NoiseMultiply;
				uniform float _Offset;
				uniform float _Size;
				uniform float _RampFalloff;
				float2 UnStereo( float2 UV )
				{
					#if UNITY_SINGLE_PASS_STEREO
					float4 scaleOffset = unity_StereoScaleOffset[ unity_StereoEyeIndex ];
					UV.xy = (UV.xy - scaleOffset.zw) / scaleOffset.xy;
					#endif
					return UV;
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

					float4 screenPos = i.ase_texcoord3;
					float4 ase_screenPosNorm = screenPos / screenPos.w;
					ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
					float2 UV22_g3 = ase_screenPosNorm.xy;
					float2 localUnStereo22_g3 = UnStereo( UV22_g3 );
					float2 break64_g1 = localUnStereo22_g3;
					float4 tex2DNode36_g1 = tex2D( _CameraDepthTexture, ase_screenPosNorm.xy );
					#ifdef UNITY_REVERSED_Z
					float4 staticSwitch38_g1 = ( 1.0 - tex2DNode36_g1 );
					#else
					float4 staticSwitch38_g1 = tex2DNode36_g1;
					#endif
					float3 appendResult39_g1 = (float3(break64_g1.x , break64_g1.y , staticSwitch38_g1.r));
					float4 appendResult42_g1 = (float4((appendResult39_g1*2.0 + -1.0) , 1.0));
					float4 temp_output_43_0_g1 = mul( unity_CameraInvProjection, appendResult42_g1 );
					float4 appendResult49_g1 = (float4(( ( (temp_output_43_0_g1).xyz / (temp_output_43_0_g1).w ) * float3( 1,1,-1 ) ) , 1.0));
					float4 temp_output_248_0 = mul( unity_WorldToObject, mul( unity_CameraToWorld, appendResult49_g1 ) );
					float2 panner280 = ( _Time.y * _NoiseSpeed + temp_output_248_0.xy);
					float4 temp_cast_5 = (_Offset).xxxx;
					float4 transform114 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
					float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
					float4 appendResult302 = (float4(( _TintColor * i.color ).rgb , ( tex2D( _MainTex, ( ( ( ( tex2D( _NoiseTex, panner280 ).r * _NoiseMultiply ) + temp_output_248_0 ) + 0.5 ) - temp_cast_5 ).xy ).r * ( 1.0 - saturate( pow( ( length( abs( (( transform114 - float4( 0,0,0,0 ) )).xyz ) ) / ( max( max( ase_objectScale.x , ase_objectScale.y ) , ase_objectScale.z ) * 0.5 * _Size ) ) , _RampFalloff ) ) ) )));
					

					fixed4 col = appendResult302;
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
20;394;1331;566;-2210.454;73.7678;1.833983;True;False
Node;AmplifyShaderEditor.CommentaryNode;219;1430.562,431.2389;Inherit;False;339.352;234.4789;Object pivot position in world space;1;114;;0,0,0,1;0;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;114;1480.562,481.2389;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;244;1423.137,308.2795;Inherit;False;Reconstruct World Position From Depth;0;;1;e7094bcbcc80eb140b2a3dbe6a861de8;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.WorldToObjectMatrix;274;1633.331,206.4805;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.CommentaryNode;220;1720.822,722.6879;Inherit;False;527.0399;229;Make the effect scale with the sphere;3;152;154;155;;0,0,0,1;0;0
Node;AmplifyShaderEditor.ObjectScaleNode;152;1770.822,772.6879;Inherit;False;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;113;1850.315,480.0514;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TimeNode;284;1490.788,-67.89606;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;248;1875.491,206.3374;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;283;1514.087,-192.4115;Float;False;Property;_NoiseSpeed;Noise Speed;5;0;Create;True;0;0;False;0;0.5,0.5;-0.4,-0.4;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ComponentMaskNode;160;2019.684,492.7632;Inherit;False;True;True;True;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PannerNode;280;1716.634,-159.1553;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;154;1969.448,772.6873;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;279;1914.06,-184.5853;Inherit;True;Property;_NoiseTex;Noise Tex;4;0;Create;True;0;0;False;0;-1;140c5b15ccac91a4fb58b5ea4666c02f;357928dd8c8088440b4662373bd09d7a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;158;2249.322,496.2934;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;287;2050.791,34.2052;Float;False;Property;_NoiseMultiply;Noise Multiply;7;0;Create;True;0;0;False;0;0.45;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;164;2219.559,637.4156;Float;False;Property;_RampFalloff;Ramp Falloff;3;0;Create;True;0;0;False;0;5;6.25;0.01;14;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;216;1994.883,967.0018;Float;False;Property;_Size;Size;2;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;155;2098.858,820.165;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;286;2264.755,-83.61971;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;159;2400,499.7799;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;293;2538.424,604.926;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;2308.661,817.793;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;296;2556.854,582.5112;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;148;2571.09,499.1686;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;250;2332.517,327.9362;Float;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;285;2391.782,189.0243;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;249;2569.467,248.6715;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PowerNode;163;2730.691,497.8898;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;294;2628.559,337.9805;Float;False;Property;_Offset;Offset;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;301;2780.391,132.8703;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;187;2947.78,499.78;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;289;2819.683,240.737;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;300;3364.01,-60.17512;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;184;3120,496;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;277;3083.49,237.4845;Inherit;True;Property;_MaskMap;Mask Map;6;0;Create;True;0;0;False;0;-1;None;81ab317842bdaef44953adbfb5b0f499;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;304;3397.007,150.8011;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;303;3572.026,87.35683;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;278;3413.409,472.5801;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;302;3748.871,341.1714;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;306;3933.371,339.8851;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Decal/DepthDecalDistortion;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;113;0;114;0
WireConnection;248;0;274;0
WireConnection;248;1;244;0
WireConnection;160;0;113;0
WireConnection;280;0;248;0
WireConnection;280;2;283;0
WireConnection;280;1;284;2
WireConnection;154;0;152;1
WireConnection;154;1;152;2
WireConnection;279;1;280;0
WireConnection;158;0;160;0
WireConnection;155;0;154;0
WireConnection;155;1;152;3
WireConnection;286;0;279;1
WireConnection;286;1;287;0
WireConnection;159;0;158;0
WireConnection;293;0;164;0
WireConnection;156;0;155;0
WireConnection;156;2;216;0
WireConnection;296;0;293;0
WireConnection;148;0;159;0
WireConnection;148;1;156;0
WireConnection;285;0;286;0
WireConnection;285;1;248;0
WireConnection;249;0;285;0
WireConnection;249;1;250;0
WireConnection;163;0;148;0
WireConnection;163;1;296;0
WireConnection;187;0;163;0
WireConnection;289;0;249;0
WireConnection;289;1;294;0
WireConnection;184;0;187;0
WireConnection;277;0;301;0
WireConnection;277;1;289;0
WireConnection;303;0;300;0
WireConnection;303;1;304;0
WireConnection;278;0;277;1
WireConnection;278;1;184;0
WireConnection;302;0;303;0
WireConnection;302;3;278;0
WireConnection;306;0;302;0
ASEEND*/
//CHKSM=80C1CA713B899BA7D94FBAF46D2A3E1B99B70D53