Shader "GlassesEffect"
{
    Properties
    {
        _MainTex( "Texture", 2D ) = "white" {}
        _Blursize( "Blursize", Float ) = 0.5
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert( appdata v )
            {
                v2f o;
                o.vertex = UnityObjectToClipPos( v.vertex );
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Blursize;

            fixed4 frag( v2f o ) : SV_Target
            {
                fixed4 color = fixed4( 0,0,0,0 );
                _Blursize /= 1000;
                for ( float y = -2.0; y <= 2.0; y++ )
                {
                    color += tex2D( _MainTex, o.uv + fixed2( 2.0, y ) * _Blursize ) * 0.06136;
                    color += tex2D( _MainTex, o.uv + fixed2( 1.0, y ) * _Blursize ) * 0.24477;
                    color += tex2D( _MainTex, o.uv + fixed2( 0.0, y ) * _Blursize ) * 0.38774;
                    color += tex2D( _MainTex, o.uv + fixed2( -1.0, y ) * _Blursize ) * 0.24477;
                    color += tex2D( _MainTex, o.uv + fixed2( -2.0, y ) * _Blursize ) * 0.06136;

                    //color += tex2D( _MainTex, o.uv + fixed2(  3.0, y ) * _Blursize ) * 0.00598;
                    //color += tex2D( _MainTex, o.uv + fixed2(  2.0, y ) * _Blursize ) * 0.060626;
                    //color += tex2D( _MainTex, o.uv + fixed2(  1.0, y ) * _Blursize ) * 0.241843;
                    //color += tex2D( _MainTex, o.uv + fixed2(  0.0, y ) * _Blursize ) * 0.383103;
                    //color += tex2D( _MainTex, o.uv + fixed2( -1.0, y ) * _Blursize ) * 0.241843;
                    //color += tex2D( _MainTex, o.uv + fixed2( -2.0, y ) * _Blursize ) * 0.060626;
                    //color += tex2D( _MainTex, o.uv + fixed2( -3.0, y ) * _Blursize ) * 0.00598;
                }
                for ( float x = -2.0; x <= 2.0; x++ )
                {
                    color += tex2D( _MainTex, o.uv + fixed2( x, 2.0 ) * _Blursize ) * 0.06136;
                    color += tex2D( _MainTex, o.uv + fixed2( x, 1.0 ) * _Blursize ) * 0.24477;
                    color += tex2D( _MainTex, o.uv + fixed2( x, 0.0 ) * _Blursize ) * 0.38774;
                    color += tex2D( _MainTex, o.uv + fixed2( x, -1.0 ) * _Blursize ) * 0.24477;
                    color += tex2D( _MainTex, o.uv + fixed2( x, -2.0 ) * _Blursize ) * 0.06136;

                    //color += tex2D( _MainTex, o.uv + fixed2( x,  3.0 ) * _Blursize ) * 0.00598;
                    //color += tex2D( _MainTex, o.uv + fixed2( x, 2.0 ) * _Blursize ) * 0.060626;
                    //color += tex2D( _MainTex, o.uv + fixed2( x, 1.0 ) * _Blursize ) * 0.241843;
                    //color += tex2D( _MainTex, o.uv + fixed2( x, 0.0 ) * _Blursize ) * 0.383103;
                    //color += tex2D( _MainTex, o.uv + fixed2( x, -1.0 ) * _Blursize ) * 0.241843;
                    //color += tex2D( _MainTex, o.uv + fixed2( x, -2.0 ) * _Blursize ) * 0.060626;
                    //color += tex2D( _MainTex, o.uv + fixed2( x, -3.0 ) * _Blursize ) * 0.00598;
                }
                
                return color * 0.10;
            }
            ENDCG
        }
    }
}
