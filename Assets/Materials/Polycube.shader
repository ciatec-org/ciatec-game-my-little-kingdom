Shader "Meu Pequeno Reino/Polycube"
{
    Properties { _BaseColor("Cor", Color) = (0.435294,0.537255,0.592157,1) }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "Queue"="Geometry" "RenderType"="Opaque" }
        Pass
        {
            Tags { "LightMode"="SRPDefaultUnlit" }
            Cull Off ZWrite On ZTest LEqual
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            struct Attributes { float4 positionOS: POSITION; float3 normalOS: NORMAL; float2 uv:TEXCOORD0; };
            struct Varyings { float4 positionCS: SV_POSITION; float3 normalWS:TEXCOORD0; float2 uv:TEXCOORD1; };
            CBUFFER_START(UnityPerMaterial)
            half4 _BaseColor;
            CBUFFER_END
            Varyings vert(Attributes input)
            {
                Varyings o; o.positionCS=TransformObjectToHClip(input.positionOS.xyz);
                o.normalWS=TransformObjectToWorldNormal(input.normalOS);o.uv=input.uv;return o;
            }
            half4 frag(Varyings input):SV_Target
            {
                float light=.52+.48*max(0,dot(normalize(input.normalWS),normalize(float3(-.45,.8,-1))));
                half3 baseColor=_BaseColor.rgb*light;
                float2 edgeDistance=min(input.uv,1-input.uv)/max(fwidth(input.uv),float2(.00001,.00001));
                if(min(edgeDistance.x,edgeDistance.y)<.65) baseColor=lerp(baseColor,half3(.117647,.184314,.215686),.48);
                return half4(baseColor,1);
            }
            ENDHLSL
        }
    }
}
