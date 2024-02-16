// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Cutout/CutoutAB_CullBack"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_TexPower("Tex Power", Float) = 1
		_AlphaCutout("Alpha Cutout", Float) = 0
		[HDR]_DissolveColor("Dissolve Color", Color) = (1,1,1,1)
		_MainTilingOffset("Main Tiling Offset", Vector) = (1,1,0,0)
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_DissolveEdgeWidth("Dissolve Edge Width", Range( 0 , 1)) = 0
		_NoiseSpeed("Noise Speed", Vector) = (0,0,0,0)
		_MaskClipValue("Mask Clip Value", Float) = 0.5

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
				uniform float4 _MainTilingOffset;
				uniform float _AlphaCutout;
				uniform sampler2D _NoiseTex;
				uniform float2 _NoiseSpeed;
				uniform float4 _NoiseTex_ST;
				uniform float4 _DissolveColor;
				uniform float _TexPower;
				uniform float _MaskClipValue;


				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.ase_texcoord3.xy = v.ase_texcoord1.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord3.zw = 0;

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

					float2 uv095 = i.texcoord.xy * (_MainTilingOffset).xy + (_MainTilingOffset).zw;
					float4 tex2DNode82 = tex2D( _MainTex, uv095 );
					float3 uv046 = i.texcoord.xyz;
					uv046.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					float2 uv1_NoiseTex = i.ase_texcoord3.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
					float2 panner71 = ( _Time.y * _NoiseSpeed + uv1_NoiseTex);
					float OpacityMask27 = ( ( 1.0 - ( _AlphaCutout + uv046.z ) ) - tex2D( _NoiseTex, panner71 ).r );
					float temp_output_65_0 = saturate( ( tex2DNode82.a * OpacityMask27 ) );
					float4 temp_cast_0 = (_TexPower).xxxx;
					float4 Emission22 = ( pow( tex2DNode82 , temp_cast_0 ) * _TintColor * i.color );
					clip( temp_output_65_0 - _MaskClipValue);
					

					fixed4 col = (( _DissolveEdgeWidth > temp_output_65_0 ) ? _DissolveColor :  Emission22 );
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
20;394;1331;566;688.6813;752.4755;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;97;-456.6043,1297.642;Inherit;False;733.1929;322.7531;Cutout;4;46;15;43;55;;0.04830408,1,0,1;0;0
Node;AmplifyShaderEditor.Vector2Node;70;-591.2852,1009.245;Float;False;Property;_NoiseSpeed;Noise Speed;6;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;72;-595.2852,1145.245;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;92;-1007.758,-569.8314;Float;False;Property;_MainTilingOffset;Main Tiling Offset;3;0;Create;True;0;0;False;0;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;61;-597.7073,852.5314;Inherit;False;1;12;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-353.2013,1347.642;Float;False;Property;_AlphaCutout;Alpha Cutout;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-406.6043,1441.395;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;93;-746.4467,-617.3598;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;94;-744.7146,-522.5807;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-131.3144,1352.586;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;71;-344.2852,991.2446;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;95;-513.9727,-595.924;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-132.9651,963.6368;Inherit;True;Property;_NoiseTex;Noise Tex;4;0;Create;True;0;0;False;0;-1;None;140c5b15ccac91a4fb58b5ea4666c02f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;55;89.58849,1353.16;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;81;-498.7647,-704.9963;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;306.6187,969.66;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;82;-249.7828,-704.7959;Inherit;True;Property;_TextureSample0;Texture Sample 0;7;0;Create;True;0;0;False;0;-1;None;89cc1c5e64e94a24a822e10b9501f2dc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;57;-128.5686,-456.9718;Float;False;Property;_TexPower;Tex Power;0;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;59;83.34039,-278.1558;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;56;89.47234,-675.9882;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;80;45.33933,-504.9088;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;570.7059,967.0314;Float;True;OpacityMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;362.9343,-525.8784;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;26;-678.6791,594.6582;Inherit;True;27;OpacityMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;532.8392,-531.2465;Float;True;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-393.4294,558.6085;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-528.0078,21.10897;Float;False;Property;_DissolveEdgeWidth;Dissolve Edge Width;5;0;Create;True;0;0;False;0;0;0.55;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;64;-461.6236,109.2715;Float;False;Property;_DissolveColor;Dissolve Color;2;1;[HDR];Create;True;0;0;False;0;1,1,1,1;4.237095,2.7286,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;30;-463.3519,297.9594;Inherit;True;22;Emission;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;65;-208.3618,524.7786;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareGreater;63;-69.56338,240.4235;Inherit;True;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;86;78.39445,485.8256;Float;False;Property;_MaskClipValue;Mask Clip Value;7;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;78;323.99,240.3333;Inherit;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;87;548.7306,241.1558;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Cutout/CutoutAB_CullBack;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;93;0;92;0
WireConnection;94;0;92;0
WireConnection;43;0;15;0
WireConnection;43;1;46;3
WireConnection;71;0;61;0
WireConnection;71;2;70;0
WireConnection;71;1;72;2
WireConnection;95;0;93;0
WireConnection;95;1;94;0
WireConnection;12;1;71;0
WireConnection;55;0;43;0
WireConnection;19;0;55;0
WireConnection;19;1;12;1
WireConnection;82;0;81;0
WireConnection;82;1;95;0
WireConnection;56;0;82;0
WireConnection;56;1;57;0
WireConnection;27;0;19;0
WireConnection;18;0;56;0
WireConnection;18;1;80;0
WireConnection;18;2;59;0
WireConnection;22;0;18;0
WireConnection;96;0;82;4
WireConnection;96;1;26;0
WireConnection;65;0;96;0
WireConnection;63;0;62;0
WireConnection;63;1;65;0
WireConnection;63;2;64;0
WireConnection;63;3;30;0
WireConnection;78;0;63;0
WireConnection;78;1;65;0
WireConnection;78;2;86;0
WireConnection;87;0;78;0
ASEEND*/
//CHKSM=A06ABF543D5DB5D87E79D49D7155CE8EA27BFE5C