// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Trail/SmoothTrail"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		[HDR]_DissolveColor("Dissolve Color", Color) = (1,1,1,1)
		_AlphaCutout("Alpha Cutout", Float) = 0.37
		_DissolveTex("Dissolve Tex", 2D) = "white" {}
		_MaskClipValue("Mask Clip Value", Float) = 0.5
		_MainTilingOffset("Main Tiling Offset", Vector) = (0,0,0,0)
		_DissolveEdgeWidth("Dissolve Edge Width", Range( 0 , 1)) = 0.37
		_MainSpeed("Main Speed", Vector) = (0,0,0,0)
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
				uniform float _DissolveEdgeWidth;
				uniform sampler2D _DissolveTex;
				uniform float4 _DissolveTex_ST;
				uniform float _AlphaCutout;
				uniform float4 _DissolveColor;
				uniform float2 _MainSpeed;
				uniform float4 _MainTilingOffset;
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

					float2 uv_DissolveTex = i.texcoord.xy * _DissolveTex_ST.xy + _DissolveTex_ST.zw;
					float4 uv053 = i.texcoord;
					uv053.xy = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_56_0 = ( 1.0 - ( _AlphaCutout + uv053.z ) );
					float temp_output_59_0 = ( tex2D( _DissolveTex, uv_DissolveTex ).r + temp_output_56_0 );
					float2 uv078 = i.texcoord.xy * (_MainTilingOffset).xy + (_MainTilingOffset).zw;
					float2 panner99 = ( 1.0 * _Time.y * _MainSpeed + uv078);
					float4 tex2DNode76 = tex2D( _MainTex, panner99 );
					float4 temp_output_19_0 = ( _TintColor * tex2DNode76 * i.color );
					float2 uv079 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float smoothstepResult52 = smoothstep( 0.2 , 0.8 , ( ( 1.0 - uv079.x ) - -1.0 ));
					float uv_gradient_184 = saturate( smoothstepResult52 );
					float2 uv011 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_42_0 = ( uv011.y * ( 1.0 - uv011.y ) * ( 1.0 - uv011.x ) * 8.0 );
					float uv_gradient_287 = temp_output_42_0;
					float uv_gradient_389 = ( temp_output_42_0 * ( ( uv011.x * uv011.y * ( 1.0 - uv011.y ) ) * 20.0 ) );
					float4 appendResult72 = (float4((( _DissolveEdgeWidth > temp_output_59_0 ) ? _DissolveColor :  temp_output_19_0 ).rgb , saturate( ( (temp_output_19_0).a * ( uv_gradient_184 * uv_gradient_287 * uv_gradient_389 ) ) )));
					clip( temp_output_59_0 - _MaskClipValue);
					

					fixed4 col = appendResult72;
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
20;394;1331;566;2890.802;-176.4251;2.221489;True;False
Node;AmplifyShaderEditor.CommentaryNode;93;-2493.954,1112.141;Inherit;False;1530.559;664.5071;UV Gradient;14;11;83;82;62;41;40;39;61;42;60;63;81;89;87;;0,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;80;-2401.399,1812.028;Inherit;False;1441.03;269.861;UV Gradient;7;47;52;49;46;20;79;84;;0,0,0,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-2443.954,1185.215;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;79;-2351.399,1862.028;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;83;-2065.508,1506.447;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;82;-2070.207,1548.742;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;62;-2072.799,1635.993;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-2099.981,1976.377;Float;False;Constant;_Float2;Float 2;5;0;Create;True;0;0;False;0;-1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;20;-2093.234,1890.506;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;94;-2083.261,593.4075;Float;False;Property;_MainTilingOffset;Main Tiling Offset;4;0;Create;True;0;0;False;0;0,0,0,0;0.5,2,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;81;-1947.318,1162.141;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-2062.823,1424.784;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;40;-2060.02,1339.161;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-1834.457,1523.648;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;96;-1813.674,666.2747;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;39;-2060.019,1259.34;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;49;-1943.298,1892.031;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;95;-1808.674,587.2748;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-1600.726,1464.989;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;20;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;100;-1677.869,801.7743;Float;False;Property;_MainSpeed;Main Speed;6;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1798.389,1236.572;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;78;-1554.432,596.3792;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;52;-1731.848,1890.017;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;77;-557.3212,1653.834;Inherit;False;513.423;360.2621;Alpha Cutout;3;53;54;55;;0.06627893,1,0,1;0;0
Node;AmplifyShaderEditor.SaturateNode;47;-1547.132,1890.662;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;71;-1299.692,370.3491;Inherit;True;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;99;-1284.664,595.1755;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-1368.595,1337.44;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;84;-1299.415,1885.798;Float;False;uv_gradient_1;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;70;-150.9189,249.1516;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;87;-1507.024,1228.729;Float;False;uv_gradient_2;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;53;-507.3212,1812.098;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;89;-1212.395,1330.051;Float;False;uv_gradient_3;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;76;-1036.76,562.848;Inherit;True;Property;_TextureSample0;Texture Sample 0;7;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;43;-127.7414,681.9107;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;54;-497.4412,1703.833;Float;False;Property;_AlphaCutout;Alpha Cutout;1;0;Create;True;0;0;False;0;0.37;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;91;-186.6354,1252.422;Inherit;False;89;uv_gradient_3;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;86;-199.7063,1075.401;Inherit;False;84;uv_gradient_1;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;105.3561,577.2098;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;90;-190.8174,1168.781;Inherit;False;87;uv_gradient_2;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;-197.8982,1721.269;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;56;11.53945,1723.144;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;75;326.2747,993.5042;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;141.3811,1150.42;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;57;-511.0998,1447.107;Inherit;True;Property;_DissolveTex;Dissolve Tex;2;0;Create;True;0;0;False;0;-1;None;163632276e446414db3976d5befc6048;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;556.8016,1092.466;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;67;297.3425,411.2455;Float;False;Property;_DissolveColor;Dissolve Color;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;66;306.4853,310.2243;Float;False;Property;_DissolveEdgeWidth;Dissolve Edge Width;5;0;Create;True;0;0;False;0;0.37;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;59;386.1201,1489.86;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;48;706.604,1011.568;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareGreater;68;631.0709,754.5011;Inherit;True;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;73;867.6768,1129.426;Float;False;Property;_MaskClipValue;Mask Clip Value;3;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;72;933.9603,866.4839;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-905.2854,904.4237;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;58;185.2643,1722.399;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.6;False;4;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;74;1098.103,868.7637;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;85;-866.0574,780.0607;Inherit;False;84;uv_gradient_1;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-639.8002,784.341;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;25;-462.1407,896.1999;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;69;1288.952,867.9084;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Trail/SmoothTrail;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;83;0;11;1
WireConnection;82;0;11;2
WireConnection;62;0;11;2
WireConnection;20;0;79;1
WireConnection;81;0;11;2
WireConnection;40;0;11;1
WireConnection;61;0;83;0
WireConnection;61;1;82;0
WireConnection;61;2;62;0
WireConnection;96;0;94;0
WireConnection;39;0;11;2
WireConnection;49;0;20;0
WireConnection;49;1;46;0
WireConnection;95;0;94;0
WireConnection;60;0;61;0
WireConnection;42;0;81;0
WireConnection;42;1;39;0
WireConnection;42;2;40;0
WireConnection;42;3;41;0
WireConnection;78;0;95;0
WireConnection;78;1;96;0
WireConnection;52;0;49;0
WireConnection;47;0;52;0
WireConnection;99;0;78;0
WireConnection;99;2;100;0
WireConnection;63;0;42;0
WireConnection;63;1;60;0
WireConnection;84;0;47;0
WireConnection;87;0;42;0
WireConnection;89;0;63;0
WireConnection;76;0;71;0
WireConnection;76;1;99;0
WireConnection;19;0;70;0
WireConnection;19;1;76;0
WireConnection;19;2;43;0
WireConnection;55;0;54;0
WireConnection;55;1;53;3
WireConnection;56;0;55;0
WireConnection;75;0;19;0
WireConnection;98;0;86;0
WireConnection;98;1;90;0
WireConnection;98;2;91;0
WireConnection;97;0;75;0
WireConnection;97;1;98;0
WireConnection;59;0;57;1
WireConnection;59;1;56;0
WireConnection;48;0;97;0
WireConnection;68;0;66;0
WireConnection;68;1;59;0
WireConnection;68;2;67;0
WireConnection;68;3;19;0
WireConnection;72;0;68;0
WireConnection;72;3;48;0
WireConnection;58;0;56;0
WireConnection;74;0;72;0
WireConnection;74;1;59;0
WireConnection;74;2;73;0
WireConnection;24;0;85;0
WireConnection;24;1;76;1
WireConnection;25;0;24;0
WireConnection;25;1;26;1
WireConnection;69;0;74;0
ASEEND*/
//CHKSM=CD0AE8184B73116E448CFFB99333D51013455D24