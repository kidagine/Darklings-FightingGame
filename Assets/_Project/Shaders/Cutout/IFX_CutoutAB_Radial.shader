// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Cutout/CutoutRadialAB"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_NoiseTex("Noise Tex", 2D) = "white" {}
		[HDR]_DissolveColor("Dissolve Color", Color) = (1,1,1,1)
		_AlphaCutout("Alpha Cutout", Float) = 0
		_MaskClipValue("Mask Clip Value", Float) = 0.5
		_TexPower("Tex Power", Float) = 1
		_DissolveEdgeWidth("Dissolve Edge Width", Range( 0 , 1)) = 0
		_SphereMaskHardness("Sphere Mask Hardness", Range( 0 , 1)) = 0.8
		_SphereMaskRadius("Sphere Mask Radius", Range( 0 , 1)) = 0.3
		_NoiseTilingOffset("Noise Tiling Offset", Vector) = (1,1,0,0)

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
				uniform float _AlphaCutout;
				uniform float _SphereMaskRadius;
				uniform float _SphereMaskHardness;
				uniform sampler2D _NoiseTex;
				uniform float4 _NoiseTilingOffset;
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

					float3 uv046 = i.texcoord.xyz;
					uv046.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					float2 uv1112 = i.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
					float2 temp_cast_0 = (0.5).xx;
					float clampResult119 = clamp( ( ( distance( uv1112 , temp_cast_0 ) - _SphereMaskRadius ) / ( 1.0 - _SphereMaskHardness ) ) , 0.0 , 1.0 );
					float2 temp_output_123_0 = (_NoiseTilingOffset).xy;
					float2 uv187 = i.ase_texcoord3.xy * temp_output_123_0 + (_NoiseTilingOffset).zw;
					float2 temp_output_89_0 = ( uv187 - ( temp_output_123_0 / 2.0 ) );
					float2 appendResult97 = (float2(frac( ( atan2( (temp_output_89_0).x , (temp_output_89_0).y ) / 6.28318548202515 ) ) , length( temp_output_89_0 )));
					float OpacityMask27 = ( ( 1.0 - ( _AlphaCutout + uv046.z ) ) - ( ( 1.0 - clampResult119 ) * tex2D( _NoiseTex, appendResult97 ).r ) );
					float temp_output_65_0 = saturate( OpacityMask27 );
					float2 uv060 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float4 temp_cast_1 = (_TexPower).xxxx;
					float4 Emission22 = ( pow( tex2D( _MainTex, uv060 ) , temp_cast_1 ) * _TintColor * i.color );
					clip( ( (Emission22).a * temp_output_65_0 ) - _MaskClipValue);
					

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
20;394;1331;566;4865.628;590.3129;4.491326;True;False
Node;AmplifyShaderEditor.Vector4Node;122;-4138.445,15.58352;Float;False;Property;_NoiseTilingOffset;Noise Tiling Offset;8;0;Create;True;0;0;False;0;1,1,0,0;4,4,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;103;-3698.958,211.5145;Float;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;124;-3915.201,66.11588;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;123;-3917.26,-30.65199;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;101;-3522.638,142.8709;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;87;-3612.346,9.559681;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;89;-3360.943,84.89946;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;113;-3075.444,-291.9664;Float;False;Constant;_Float1;Float 1;10;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;112;-3131.649,-420.0493;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;90;-3183.289,79.02538;Inherit;False;False;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;91;-3172.39,-36.39608;Inherit;False;True;False;True;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;116;-2935.785,-202.4353;Float;False;Property;_SphereMaskRadius;Sphere Mask Radius;7;0;Create;True;0;0;False;0;0.3;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ATan2OpNode;92;-2938.75,-1.996078;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;118;-2938.357,-108.3267;Float;False;Property;_SphereMaskHardness;Sphere Mask Hardness;6;0;Create;True;0;0;False;0;0.8;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;114;-2847.763,-372.5854;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;93;-2933.938,112.5204;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;94;-2775.118,1.181633;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;121;-2636.763,-106.6817;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;115;-2646.352,-373.1987;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;96;-2591.289,79.02538;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;95;-2934.549,263.3404;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;134;-2482.918,406.4542;Inherit;False;647.6295;322.7531;Cutout;4;15;46;43;55;;0,1,0.03551388,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;117;-2415.56,-372.4712;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2379.515,456.4542;Float;False;Property;_AlphaCutout;Alpha Cutout;2;0;Create;True;0;0;False;0;0;0.11;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;97;-2460.284,240.3175;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-2432.918,550.2072;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;119;-2265.656,-373.8211;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;126;-2047.016,804.544;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-2161.29,503.4528;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;12;-2249.444,208.8681;Inherit;True;Property;_NoiseTex;Noise Tex;0;0;Create;True;0;0;False;0;-1;140c5b15ccac91a4fb58b5ea4666c02f;e399c37bf0955764da18ac5a6f418997;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;120;-2103.982,-373.6888;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;60;-2081.875,998.8557;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;131;-1805.55,874.6748;Inherit;True;Property;_TextureSample0;Texture Sample 0;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;57;-1704.618,1094.632;Float;False;Property;_TexPower;Tex Power;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;55;-2022.288,501.6232;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;105;-1892.737,212.5414;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;127;-1554.212,1161.307;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;59;-1560.89,1330.718;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1687.219,186.6077;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;56;-1486.577,875.6161;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1260.122,983.9414;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-1513.403,178.7784;Float;False;OpacityMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;-1101.874,979.3506;Float;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;26;-478.5533,829.9319;Inherit;True;27;OpacityMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;48;-870.9568,979.114;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;64;-328.6083,464.4092;Float;False;Property;_DissolveColor;Dissolve Color;1;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0.4852942,1.022921,3,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;62;-397.1174,358.2407;Float;False;Property;_DissolveEdgeWidth;Dissolve Edge Width;5;0;Create;True;0;0;False;0;0;0.548;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;65;-188.8323,836.3575;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-310.6072,655.2841;Inherit;False;22;Emission;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;113.4171,988.0708;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareGreater;63;96.70038,607.2673;Inherit;True;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;128;408.1858,892.0936;Float;False;Property;_MaskClipValue;Mask Clip Value;3;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;130;665.1431,611.8455;Inherit;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;125;917.6412,609.243;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Cutout/CutoutRadialAB;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;124;0;122;0
WireConnection;123;0;122;0
WireConnection;101;0;123;0
WireConnection;101;1;103;0
WireConnection;87;0;123;0
WireConnection;87;1;124;0
WireConnection;89;0;87;0
WireConnection;89;1;101;0
WireConnection;90;0;89;0
WireConnection;91;0;89;0
WireConnection;92;0;91;0
WireConnection;92;1;90;0
WireConnection;114;0;112;0
WireConnection;114;1;113;0
WireConnection;94;0;92;0
WireConnection;94;1;93;0
WireConnection;121;0;118;0
WireConnection;115;0;114;0
WireConnection;115;1;116;0
WireConnection;96;0;94;0
WireConnection;95;0;89;0
WireConnection;117;0;115;0
WireConnection;117;1;121;0
WireConnection;97;0;96;0
WireConnection;97;1;95;0
WireConnection;119;0;117;0
WireConnection;43;0;15;0
WireConnection;43;1;46;3
WireConnection;12;1;97;0
WireConnection;120;0;119;0
WireConnection;131;0;126;0
WireConnection;131;1;60;0
WireConnection;55;0;43;0
WireConnection;105;0;120;0
WireConnection;105;1;12;1
WireConnection;19;0;55;0
WireConnection;19;1;105;0
WireConnection;56;0;131;0
WireConnection;56;1;57;0
WireConnection;18;0;56;0
WireConnection;18;1;127;0
WireConnection;18;2;59;0
WireConnection;27;0;19;0
WireConnection;22;0;18;0
WireConnection;48;0;22;0
WireConnection;65;0;26;0
WireConnection;69;0;48;0
WireConnection;69;1;65;0
WireConnection;63;0;62;0
WireConnection;63;1;65;0
WireConnection;63;2;64;0
WireConnection;63;3;30;0
WireConnection;130;0;63;0
WireConnection;130;1;69;0
WireConnection;130;2;128;0
WireConnection;125;0;130;0
ASEEND*/
//CHKSM=96CFF106FA4DB302FEA2BFE0B2FBAC9EDEC258CC