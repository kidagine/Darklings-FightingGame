// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/IFX/Abilities/CurveAppearMask"
{
	Properties
	{
		[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,10.0)) = 1.0
		[Toggle(_USESOFTPARTICLES_ON)] _UseSoftParticles("Use Soft Particles", Float) = 1
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_FlowMap("Flow Map", 2D) = "white" {}
		_NoiseSpeed("Noise Speed", Vector) = (0,0,0,0)
		_FlowMapSpeed("Flow Map Speed", Vector) = (0,0,0,0)
		_TimeScale("Time Scale", Float) = 0
		_FlowStrength("Flow Strength", Float) = 0
		_MaskOffset("Mask Offset", Float) = 0
		_MaskSize("Mask Size", Float) = -0.24
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
				uniform sampler2D _FlowMap;
				uniform float _TimeScale;
				uniform float2 _FlowMapSpeed;
				uniform float4 _FlowMap_ST;
				uniform float _FlowStrength;
				uniform sampler2D _NoiseTex;
				uniform float2 _NoiseSpeed;
				uniform float4 _NoiseTex_ST;
				uniform float _MaskOffset;
				uniform float _MaskSize;
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

					float2 uv0128 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float mulTime6 = _Time.y * _TimeScale;
					float time7 = mulTime6;
					float2 uv0_FlowMap = i.texcoord.xy * _FlowMap_ST.xy + _FlowMap_ST.zw;
					float2 panner9 = ( time7 * _FlowMapSpeed + uv0_FlowMap);
					float2 myVarName171 = ( ( ( (tex2D( _FlowMap, panner9 )).rg + -0.5 ) * 2.0 ) * _FlowStrength * uv0_FlowMap.x * ( 1.0 - uv0_FlowMap.x ) );
					float2 uv0_NoiseTex = i.texcoord.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
					float2 panner217 = ( _Time.y * _NoiseSpeed + uv0_NoiseTex);
					float2 uv0222 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float3 uv0221 = i.texcoord.xyz;
					uv0221.xy = i.texcoord.xyz.xy * float2( 1,1 ) + float2( 0,0 );
					clip( ( tex2D( _NoiseTex, panner217 ).r - ( abs( ( (-0.5 + (uv0222.x - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) + ( 1.0 - ( _MaskOffset + uv0221.z ) ) ) ) - _MaskSize ) ) - _MaskClipValue);
					

					fixed4 col = ( _TintColor * tex2D( _MainTex, ( uv0128 + myVarName171 ) ).r * i.color );
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
20;394;1331;566;1236.76;916.7816;2.118506;True;False
Node;AmplifyShaderEditor.RangedFloatNode;5;-2035.831,-573.676;Float;False;Property;_TimeScale;Time Scale;4;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1881.703,-567.4301;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;7;-1698.466,-572.553;Float;False;time;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1899.242,-870.125;Inherit;False;0;12;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;61;-1897.268,-733.3942;Float;False;Property;_FlowMapSpeed;Flow Map Speed;3;0;Create;True;0;0;False;0;0,0;1,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexturePropertyNode;12;-1453.236,-960.4964;Float;True;Property;_FlowMap;Flow Map;1;0;Create;True;0;0;False;0;None;140c5b15ccac91a4fb58b5ea4666c02f;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;9;-1398.424,-742.6001;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-0.2;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;11;-1192.995,-770.7102;Inherit;True;Property;_1;1;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;238;-689.6815,555.4783;Inherit;False;686.0135;367.4793;Mask Offset;4;224;223;221;220;;0.03127098,1,0,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;184;-634.2609,-360.2052;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;13;-884.6208,-770.5872;Inherit;True;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;221;-639.6815,740.6031;Inherit;False;0;-1;3;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;220;-637.4958,605.4783;Float;False;Property;_MaskOffset;Mask Offset;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;14;-638.0809,-764.1802;Inherit;True;ConstantBiasScale;-1;;11;63208df05c83e8e49a48ffbdce2e43a0;0;3;3;FLOAT2;0,0;False;1;FLOAT;-0.5;False;2;FLOAT;2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;223;-370.6815,700.6031;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;185;-483.2509,-432.8181;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;186;-402.9932,-481.2222;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;222;-520.1371,336.4347;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-598.8939,-547.1811;Float;False;Property;_FlowStrength;Flow Strength;5;0;Create;True;0;0;False;0;0;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;224;-197.3095,700.7424;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;225;-222.9909,359.9567;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.5;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-360.4431,-764.1802;Inherit;False;4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TimeNode;216;-504.4991,158.6932;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;171;-165.469,-768.2992;Float;True;myVarName;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;214;-535.9222,-87.99554;Inherit;False;0;218;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;226;61.03603,359.9526;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;215;-477.2179,38.89692;Float;False;Property;_NoiseSpeed;Noise Speed;2;0;Create;True;0;0;False;0;0,0;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;172;199.196,-579.4955;Inherit;False;171;myVarName;1;0;OBJECT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;128;191.4415,-701.6564;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;228;332.499,357.3667;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;227;326.8198,579.4441;Float;False;Property;_MaskSize;Mask Size;7;0;Create;True;0;0;False;0;-0.24;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;217;-234.9765,24.47224;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;173;495.1026,-652.7208;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;218;-41.41423,-3.109019;Inherit;True;Property;_NoiseTex;Noise Tex;0;0;Create;True;0;0;False;0;-1;None;163632276e446414db3976d5befc6048;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;229;581.7384,357.2263;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;235;462.85,-759.5214;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;237;694.9636,-682.2471;Inherit;True;Property;_TextureSample0;Texture Sample 0;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;219;756.0204,29.21112;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;236;835.5337,-855.1008;Inherit;False;0;0;_TintColor;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;49;822.5773,-479.7781;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;1165.847,-655.8478;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;244;1292.334,-158.2552;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;243;1243.14,-13.93073;Float;False;Property;_MaskClipValue;Mask Clip Value;8;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;241;1519.42,-235.3096;Inherit;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;231;1807.512,-235.9476;Float;False;True;-1;2;ASEMaterialInspector;0;9;QFX/IFX/Abilities/CurveAppearMask;91fd38b9dd01e8241bb4853244f909b3;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;6;0;5;0
WireConnection;7;0;6;0
WireConnection;9;0;4;0
WireConnection;9;2;61;0
WireConnection;9;1;7;0
WireConnection;11;0;12;0
WireConnection;11;1;9;0
WireConnection;184;0;4;1
WireConnection;13;0;11;0
WireConnection;14;3;13;0
WireConnection;223;0;220;0
WireConnection;223;1;221;3
WireConnection;185;0;4;1
WireConnection;186;0;184;0
WireConnection;224;0;223;0
WireConnection;225;0;222;1
WireConnection;15;0;14;0
WireConnection;15;1;16;0
WireConnection;15;2;185;0
WireConnection;15;3;186;0
WireConnection;171;0;15;0
WireConnection;226;0;225;0
WireConnection;226;1;224;0
WireConnection;228;0;226;0
WireConnection;217;0;214;0
WireConnection;217;2;215;0
WireConnection;217;1;216;2
WireConnection;173;0;128;0
WireConnection;173;1;172;0
WireConnection;218;1;217;0
WireConnection;229;0;228;0
WireConnection;229;1;227;0
WireConnection;237;0;235;0
WireConnection;237;1;173;0
WireConnection;219;0;218;1
WireConnection;219;1;229;0
WireConnection;55;0;236;0
WireConnection;55;1;237;1
WireConnection;55;2;49;0
WireConnection;244;0;219;0
WireConnection;241;0;55;0
WireConnection;241;1;244;0
WireConnection;241;2;243;0
WireConnection;231;0;241;0
ASEEND*/
//CHKSM=59B9DF835D901D4B23F5B19CBB2360E7C33D3E87