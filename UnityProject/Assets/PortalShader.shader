Shader "Unlit/PortalShader"
{
    Properties
    {
        _Color1 ("Color1", color) = (0,0,0,0)
        _Color2 ("Color2", color) = (0,0,0,0)
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Effect", 2D) = "white" {}
        _PortalVelocity ("Velocity", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
  
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
          
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _Color1;
            float4 _Color2;
            float4 _MainTex_ST;
            float _PortalVelocity;

            v2f vert (appdata v)
            {
                v2f o;
 
         
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                              
                float cX = 0.5;
                float cY = 0.5;
                float Angle = _Time.x * _PortalVelocity ;
                
                float nX = cos(Angle)*(i.uv.x - cX) - sin(Angle)*(i.uv.y - cY) + cX;
                float nY = sin(Angle)*(i.uv.x - cX) + cos(Angle)*(i.uv.y - cY) + cY;
                
                fixed4 col = tex2D(_MainTex, float2(nX, nY));
                col = col.r < 0.2 || col.g < 0.2 || col.b < 0.2? _Color2 : col;
                col *= tex2D(_NoiseTex, i.uv + float2(_SinTime.x, _CosTime.x)) * _Color1;
                
                return col;
            }
            ENDCG
        }
    }
}
