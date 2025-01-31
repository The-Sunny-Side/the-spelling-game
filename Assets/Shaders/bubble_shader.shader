Shader "Custom/RealisticBubbleShaderV2"
{
    Properties
    {
        _MainTex ("Base Texture (RGB)", 2D) = "white" {}        
        _FresnelColor ("Fresnel Color", Color) = (0.8, 1.0, 1.0, 1.0)  
        _IridescenceScale ("Iridescence Scale", Range(0, 5)) = 1.5    
        _Smoothness ("Smoothness", Range(0, 1)) = 0.95                
        _Alpha ("Transparency", Range(0, 1)) = 0.6                    
        _ReflectionTint ("Reflection Tint", Color) = (1, 1, 1, 1)     
        _Thickness ("Thickness", Range(0.01, 0.2)) = 0.05            
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 300

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Back
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _FresnelColor;
            float _IridescenceScale;
            float _Smoothness;
            float _Alpha;
            float4 _ReflectionTint;
            float _Thickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.vertex.xy;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

                float fresnel = pow(1.0 - dot(viewDir, normalize(i.worldNormal)), 3.0);

                float angle = dot(i.worldNormal, viewDir);
                float iridescenceFactor = sin((angle + _Thickness) * _IridescenceScale * 3.14159) * 0.5 + 0.5;

                float3 iridescenceColor = lerp(
                float3(1.0, 0.8, 0.6),
                float3(0.6, 0.8, 1.0),
                iridescenceFactor
                ) * fresnel;

                float3 reflection = reflect(viewDir, i.worldNormal);
                float3 reflectionColor = _ReflectionTint.rgb * reflection;

                float thicknessEffect = _Thickness * (fresnel + iridescenceFactor);

                float3 finalColor = iridescenceColor + reflectionColor * _Smoothness;

                float alpha = _Alpha * (fresnel + thicknessEffect);

                return float4(finalColor, alpha);
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"
}
