// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MK4/Holo1"
{
	Properties
	{
		_Emission("Emission", 2D) = "black" {}
		_Composite("Composite", 2D) = "black" {}
		[HDR]_Color("Color", Color) = (1,0.8168357,0.05147058,0)
		[Toggle(_COLORBYTEXTURE_ON)] _ColorbyTexture("Color by Texture", Float) = 0
		_ColorTexture("Color Texture", 2D) = "white" {}
		_TextureExposition("Texture Exposition", Range( 0 , 1)) = 0
		_Opacity("Opacity", Range( 0 , 1)) = 0
		_Panner2("Panner2", Range( 0 , 2)) = 0
		_Vector0("Vector 0", Vector) = (0,1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" }
		LOD 100
		

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Cull Back
		Blend One One
		AlphaToMask Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#pragma shader_feature_local _COLORBYTEXTURE_ON


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float4 _Color;
			uniform sampler2D _ColorTexture;
			uniform float4 _ColorTexture_ST;
			uniform float _TextureExposition;
			uniform sampler2D _Composite;
			uniform sampler2D _Emission;
			uniform float4 _Composite_ST;
			uniform float _Panner2;
			uniform float2 _Vector0;
			uniform float _Opacity;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 uv_ColorTexture = i.ase_texcoord1.xy * _ColorTexture_ST.xy + _ColorTexture_ST.zw;
				#ifdef _COLORBYTEXTURE_ON
				float4 staticSwitch20 = ( tex2D( _ColorTexture, uv_ColorTexture ) * unity_ColorSpaceDouble * (0.5 + (_TextureExposition - 0.0) * (1.0 - 0.5) / (1.0 - 0.0)) );
				#else
				float4 staticSwitch20 = _Color;
				#endif
				float2 texCoord5 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner14 = ( 1.0 * _Time.y * float2( 0,0.5 ) + texCoord5);
				float4 tex2DNode2 = tex2D( _Emission, texCoord5 );
				float2 uv_Composite = i.ase_texcoord1.xy * _Composite_ST.xy + _Composite_ST.zw;
				float2 panner6 = ( ( _Time.y * _Panner2 ) * float2( -0.1,0 ) + texCoord5);
				float2 panner11 = ( 1.0 * _Time.y * _Vector0 + texCoord5);
				float4 clampResult18 = clamp( ( ( (0.95 + (sin( ( _Time.y * 60.0 ) ) - 0.0) * (1.0 - 0.95) / (1.0 - 0.0)) * ( tex2D( _Composite, panner14 ).g + ( ( tex2DNode2 + ( tex2D( _Composite, uv_Composite ).b * tex2D( _Composite, panner6 ).a ) ) + ( tex2DNode2.a * tex2D( _Composite, panner11 ).r ) ) ) ) * _Opacity ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
				
				
				finalColor = ( staticSwitch20 * clampResult18.r );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
0;0;1920;1019;-291.1272;378.3152;1;True;False
Node;AmplifyShaderEditor.TimeNode;38;-1854.585,360.4132;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;42;-1755.792,-283.1513;Float;False;Property;_Panner2;Panner2;8;0;Create;True;0;0;0;False;0;False;0;0.178;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1867.008,18.33636;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-1342.933,27.92215;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;6;-1047.953,-57.1539;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;3;-1186.206,187.6691;Float;True;Property;_Composite;Composite;1;0;Create;True;0;0;0;False;0;False;None;82b5b092718b55d478687a0fe695104c;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.Vector2Node;43;-1508.631,641.7675;Inherit;False;Property;_Vector0;Vector 0;9;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;4;-826.2941,160.6673;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;1;-1270.069,-510.6367;Float;True;Property;_Emission;Emission;0;0;Create;True;0;0;0;False;0;False;None;b5ae55455b266b5448daf584b906b5ec;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;8;-805.5918,-96.80281;Inherit;True;Property;_TextureSample2;Texture Sample 2;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;11;-1083.808,448.9518;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.2;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-791.0591,-340.942;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-400.6456,-59.30924;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;29;-331.9859,501.3998;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-726.7957,362.4066;Inherit;True;Property;_TextureSample3;Texture Sample 3;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;14;-989.2222,772.5317;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-108.1389,502.8822;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;60;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-281.0931,-197.0101;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-333.7194,235.031;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;15;-706.205,609.1072;Inherit;True;Property;_TextureSample4;Texture Sample 4;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;28;117.1457,474.3511;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-31.97456,-138.2542;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;105.2122,145.499;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;33;302.3656,437.0004;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.95;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-198.0858,-284.6392;Float;False;Property;_TextureExposition;Texture Exposition;5;0;Create;True;0;0;0;False;0;False;0;0.631;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;555.8214,247.8768;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;35;652.8076,384.9246;Float;True;Property;_Opacity;Opacity;6;0;Create;True;0;0;0;False;0;False;0;0.253;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;25;125.0689,-442.7341;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.5;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;21;-25.83435,-836.4863;Inherit;True;Property;_ColorTexture;Color Texture;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;831.97,196.6083;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorSpaceDouble;23;-41.15273,-627.1631;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;18;1031.49,203.1012;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;328.0502,-607.5162;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;17;172.2655,-216.6421;Float;False;Property;_Color;Color;2;1;[HDR];Create;True;0;0;0;False;0;False;1,0.8168357,0.05147058,0;20.86591,6.773226,2.621894,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;20;795.263,-215.5416;Float;True;Property;_ColorbyTexture;Color by Texture;3;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;62;1312.608,173.4697;Inherit;True;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;1609.608,-64.53027;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1807.436,565.4342;Float;False;Property;_Panner1;Panner1;7;0;Create;True;0;0;0;False;0;False;0;0.2606593;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;61;1931.674,-63.8606;Float;False;True;-1;2;ASEMaterialInspector;100;1;MK4/Holo1;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;4;1;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;True;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;2;False;-1;True;3;False;-1;True;False;0;False;-1;0;False;-1;True;1;RenderType=Transparent=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;41;0;38;2
WireConnection;41;1;42;0
WireConnection;6;0;5;0
WireConnection;6;1;41;0
WireConnection;4;0;3;0
WireConnection;4;1;6;0
WireConnection;8;0;3;0
WireConnection;11;0;5;0
WireConnection;11;2;43;0
WireConnection;2;0;1;0
WireConnection;2;1;5;0
WireConnection;9;0;8;3
WireConnection;9;1;4;4
WireConnection;10;0;3;0
WireConnection;10;1;11;0
WireConnection;14;0;5;0
WireConnection;30;0;29;2
WireConnection;7;0;2;0
WireConnection;7;1;9;0
WireConnection;12;0;2;4
WireConnection;12;1;10;1
WireConnection;15;0;3;0
WireConnection;15;1;14;0
WireConnection;28;0;30;0
WireConnection;13;0;7;0
WireConnection;13;1;12;0
WireConnection;16;0;15;2
WireConnection;16;1;13;0
WireConnection;33;0;28;0
WireConnection;31;0;33;0
WireConnection;31;1;16;0
WireConnection;25;0;24;0
WireConnection;34;0;31;0
WireConnection;34;1;35;0
WireConnection;18;0;34;0
WireConnection;22;0;21;0
WireConnection;22;1;23;0
WireConnection;22;2;25;0
WireConnection;20;1;17;0
WireConnection;20;0;22;0
WireConnection;62;0;18;0
WireConnection;65;0;20;0
WireConnection;65;1;62;0
WireConnection;61;0;65;0
ASEEND*/
//CHKSM=B27B6AFD7FC83FBD0EB15D93AF9721FB71AEEE8D