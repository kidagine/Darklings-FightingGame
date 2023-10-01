// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Trail/Curve"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_Speed("Speed", Vector) = (0,0,0,0)
		_MaskSize("Mask Size", Vector) = (0.08,0.2,0,0)
		_MainTexOffsetY("Main Tex Offset Y", Float) = 0
		_MainTexOffsetX("Main Tex Offset X", Float) = 0
		_TexAddX("Tex Add X", Float) = 0.5
		_MaskMap("Mask Map", 2D) = "white" {}
		[Toggle(_USEMASKMAP_ON)] _UseMaskMap("Use Mask Map", Float) = 1
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
				#pragma shader_feature_local _USEMASKMAP_ON


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
				uniform float4 _Speed;
				uniform float _MainTexOffsetX;
				uniform float _TexAddX;
				uniform float _MainTexOffsetY;
				uniform float2 _MaskSize;
				uniform sampler2D _MaskMap;
				uniform float4 _MaskMap_ST;


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

					float2 appendResult228 = (float2(_Speed.x , _Speed.y));
					float2 uv0238 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float3 uv0200 = i.texcoord.xyz;
					uv0200.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_198_0 = ( _MainTexOffsetX + uv0200.z );
					float2 appendResult192 = (float2(( temp_output_198_0 - _TexAddX ) , _MainTexOffsetY));
					float2 panner232 = ( _Time.y * appendResult228 + ( uv0238 - appendResult192 ));
					float4 tex2DNode266 = tex2D( _MainTex, panner232 );
					float2 uv0128 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float2 break168 = uv0128;
					float temp_output_152_0 = saturate( ( step( abs( ( break168.x - temp_output_198_0 ) ) , _MaskSize.x ) * step( abs( ( break168.y - 0.5 ) ) , _MaskSize.y ) ) );
					float4 emission243 = ( tex2DNode266.r * _TintColor * temp_output_152_0 * i.color );
					float2 uv0211 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float uv_mask215 = ( ( uv0211.y * ( 1.0 - uv0211.y ) * ( 1.0 - uv0211.x ) * 8.0 ) * ( ( uv0211.x * uv0211.y * ( 1.0 - uv0211.y ) ) * 20.0 ) );
					float opacity244 = ( temp_output_152_0 * tex2DNode266.r * i.color.a );
					float2 uv_MaskMap = i.texcoord.xy * _MaskMap_ST.xy + _MaskMap_ST.zw;
					#ifdef _USEMASKMAP_ON
					float staticSwitch267 = ( opacity244 * tex2D( _MaskMap, uv_MaskMap ).r );
					#else
					float staticSwitch267 = ( uv_mask215 * opacity244 );
					#endif
					float4 appendResult265 = (float4(emission243.rgb , saturate( staticSwitch267 )));
					

					fixed4 col = appendResult265;
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
20;394;1331;566;3722.802;1154.782;3.620994;True;False
Node;AmplifyShaderEditor.CommentaryNode;201;-2179.96,-694.678;Inherit;False;576.5295;359.0144;is animated by Particle System;4;198;191;200;197;;0.09805238,1,0,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;200;-2121.291,-508.1628;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;191;-2133.857,-633.1845;Float;False;Property;_MainTexOffsetX;Main Tex Offset X;3;0;Create;True;0;0;False;0;0;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;128;-1661.613,-830.1138;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;198;-1735.974,-525.2242;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;197;-1756.194,-418.8128;Float;False;Property;_TexAddX;Tex Add X;4;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;162;-1200.308,-395.6854;Float;False;Constant;_TexAddY;Tex Add Y;10;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;168;-1393.807,-829.3569;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.WireNode;199;-1475.158,-596.1755;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;155;-1003.731,-409.0334;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;177;-1533.781,-458.894;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;236;-1516.459,314.4313;Inherit;False;2156.593;792.9319;;14;243;244;266;239;261;253;240;232;262;230;176;228;223;238;FLOW & MAIN;0,0,0,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;196;-1588.659,-16.79239;Float;False;Property;_MainTexOffsetY;Main Tex Offset Y;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;214;-1515.32,1252.48;Inherit;False;2276.908;815.8495;;12;211;213;210;212;209;208;207;206;205;204;203;215;UV MASK;0,1,0.4206896,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;156;-1001.467,-534.9742;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;192;-1323.719,-35.94199;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;223;-1455.924,495.8799;Float;False;Property;_Speed;Speed;0;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;149;-962.3005,-275.5811;Float;False;Property;_MaskSize;Mask Size;1;0;Create;True;0;0;False;0;0.08,0.2;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;211;-1426.126,1536.055;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;238;-1492.147,363.7191;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;137;-852.6734,-531.631;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;146;-858.8792,-409.6407;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;203;-668.5741,1864.075;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;204;-1036.16,1789.224;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;143;-705.4492,-530.8107;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;144;-706.3538,-411.9863;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;228;-1102.679,524.6601;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;176;-1181.987,369.112;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TimeNode;230;-1163.753,648.342;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;205;-655.7947,1487.422;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;207;-430.2327,1751.73;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;-529.6799,-489.929;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;262;-919.9966,411.9787;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;232;-900.9697,502.6113;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;213;-904.1432,1353.125;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;206;-655.7953,1567.243;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;212;-658.5981,1652.866;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;240;-550.6964,860.2989;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;209;-196.5023,1693.071;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;20;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;266;-672.6464,474.9359;Inherit;True;Property;_TextureSample0;Texture Sample 0;8;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;208;-394.1655,1464.654;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;152;-392.7132,-491.1033;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;210;61.3055,1571.549;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;253;180.57,938.2366;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;215;345.1277,1567.302;Float;False;uv_mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;244;417.6177,933.5095;Float;False;opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;246;974.568,1270.27;Inherit;False;244;opacity;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;254;960.3379,1190.188;Inherit;False;215;uv_mask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;261;-529.9725,682.9681;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;202;916.3649,1419.271;Inherit;True;Property;_MaskMap;Mask Map;5;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;259;1183.62,1218.271;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;1253.5,1358.338;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;239;-40.21186,507.1892;Inherit;True;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;267;1416.634,1229.407;Inherit;False;Property;_UseMaskMap;Use Mask Map;6;0;Create;True;0;0;False;0;0;1;0;True;;Toggle;2;Key0;Key1;Create;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;243;367.8086,506.3065;Float;False;emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;245;1698.868,1101.3;Inherit;False;243;emission;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;216;-1505.611,2161.486;Inherit;False;1415.85;589.0923;;5;221;220;219;218;217;ADJUST UV MASK;0,1,0.3793101,1;0;0
Node;AmplifyShaderEditor.SaturateNode;75;1703.104,1238.929;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;265;1963.991,1183.853;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SmoothstepOpNode;220;-895.304,2277.999;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;217;-1368.549,2580.109;Float;False;Constant;_Float2;Float 2;5;0;Create;True;0;0;False;0;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;219;-1106.754,2280.013;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;218;-1306.379,2282.311;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;221;-649.1569,2279.452;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;260;2117.377,1184.158;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Trail/Curve;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;198;0;191;0
WireConnection;198;1;200;3
WireConnection;168;0;128;0
WireConnection;199;0;198;0
WireConnection;155;0;168;1
WireConnection;155;1;162;0
WireConnection;177;0;198;0
WireConnection;177;1;197;0
WireConnection;156;0;168;0
WireConnection;156;1;199;0
WireConnection;192;0;177;0
WireConnection;192;1;196;0
WireConnection;137;0;156;0
WireConnection;146;0;155;0
WireConnection;203;0;211;2
WireConnection;204;0;211;2
WireConnection;143;0;137;0
WireConnection;143;1;149;1
WireConnection;144;0;146;0
WireConnection;144;1;149;2
WireConnection;228;0;223;1
WireConnection;228;1;223;2
WireConnection;176;0;238;0
WireConnection;176;1;192;0
WireConnection;205;0;204;0
WireConnection;207;0;211;1
WireConnection;207;1;211;2
WireConnection;207;2;203;0
WireConnection;154;0;143;0
WireConnection;154;1;144;0
WireConnection;232;0;176;0
WireConnection;232;2;228;0
WireConnection;232;1;230;2
WireConnection;213;0;211;2
WireConnection;206;0;211;1
WireConnection;209;0;207;0
WireConnection;266;0;262;0
WireConnection;266;1;232;0
WireConnection;208;0;213;0
WireConnection;208;1;205;0
WireConnection;208;2;206;0
WireConnection;208;3;212;0
WireConnection;152;0;154;0
WireConnection;210;0;208;0
WireConnection;210;1;209;0
WireConnection;253;0;152;0
WireConnection;253;1;266;1
WireConnection;253;2;240;4
WireConnection;215;0;210;0
WireConnection;244;0;253;0
WireConnection;259;0;254;0
WireConnection;259;1;246;0
WireConnection;84;0;246;0
WireConnection;84;1;202;1
WireConnection;239;0;266;1
WireConnection;239;1;261;0
WireConnection;239;2;152;0
WireConnection;239;3;240;0
WireConnection;267;1;259;0
WireConnection;267;0;84;0
WireConnection;243;0;239;0
WireConnection;75;0;267;0
WireConnection;265;0;245;0
WireConnection;265;3;75;0
WireConnection;220;0;219;0
WireConnection;219;0;218;0
WireConnection;219;1;217;0
WireConnection;218;0;211;1
WireConnection;221;0;220;0
WireConnection;260;0;265;0
ASEEND*/
//CHKSM=3CA5E8BB9AC612A71598F0F614B09C6523770094