
/****************************************************************************
Copyright (c) 2017 Mateusz Szymoński

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/

Shader "Sprites/UltimateSpriteMask"
{
	Properties
	{
		[PerRendererData] _MainTex("Alpha Texture", 2D) = "white" {} //Used as shape, getting from SpriteRendererComponent
		[NoScaleOffset] _ColorTex("Custom Texture", 2D) = "white" {} //Used as color in shape	
		_Coord("Scale: x/y  Offset: x/y", Vector) = (1,1,0,0) //Scale and offset of color texture
		_Color("Tint", Color) = (1,1,1,1) //Used to adjust color of _ColorTex
		_Opacity("Opacity", Range(0.0, 1.0)) = 0.5 //Opacity of color texture
		[MaterialToggle] AM("Alpha Mask Mode", Float) = 0 //Triggers alpha masking
		[MaterialToggle] ST("Static Texture Mode", Float) = 0 //Triggers static texture mode
		[MaterialToggle] PixelSnap("Pixel Snap", Float) = 0 //Triggers pixel snap
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile AM_OFF AM_ON
			#pragma multi_compile ST_OFF ST_ON
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
				float4 screenPos : TEXCOORD2;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				OUT.screenPos = ComputeScreenPos(OUT.vertex);
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif
				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _ColorTex;
			float4 _Coord;
			fixed _Opacity;
		
			fixed4 frag(v2f IN) : SV_Target
			{ 
				#ifdef ST_ON
				float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
				screenUV.xy *= _ScreenParams.xy / 1000;
				screenUV *= float2(_Coord.x, _Coord.y) / (_ScreenParams.x / 1000);
				fixed4 c1 = tex2D(_ColorTex, screenUV * 3 - float2(_Coord.z, _Coord.w)) * IN.color;
				c1.a = tex2D(_MainTex, IN.texcoord).a * tex2D(_ColorTex, screenUV * 3 - float2(_Coord.z, _Coord.w)).a;
				#endif
				
				#ifdef ST_OFF
				fixed4 c1 = tex2D(_ColorTex, IN.texcoord * float2(_Coord.x, _Coord.y) - float2(_Coord.z, _Coord.w)) * IN.color;
				c1.a = tex2D(_MainTex, IN.texcoord).a * tex2D(_ColorTex, IN.texcoord * float2(_Coord.x, _Coord.y) - float2(_Coord.z, _Coord.w)).a; 
				#endif

				c1.rgb *= c1.a;			
				fixed4 c2 = tex2D(_MainTex, IN.texcoord) * IN.color;
				c2.a = tex2D(_MainTex, IN.texcoord).a;
				fixed4 finC;

				#ifdef AM_ON
				finC = c2;
				finC.a = lerp(c2.a, c1.a, _Opacity);
				finC.rgb *= finC.a;


				//c2.rgb *= c2.a;
				//finC = lerp(c2, c1, 0);			
				//finC.a = lerp(c2.a, c1.a, _Opacity);
				float a = max(0,sign(finC.a-_Opacity));
				finC.a = a;
				//finC.rgb *= a;
				//finC = tex2D(_MainTex, IN.texcoord) * IN.color;
				//finC.rgb *= max(0,_Opacity*c1.a);
				#endif

				#ifdef AM_OFF
				c2.rgb *= c2.a;
				finC = lerp(c2, c1, _Opacity);			
				finC.a = lerp(c2.a, c1.a, _Opacity);
				#endif

				return finC;
			}

			ENDCG
		}
	}
}

