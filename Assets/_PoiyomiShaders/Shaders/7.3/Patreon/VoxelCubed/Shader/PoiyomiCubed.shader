Shader ".poiyomi/Patreon/Cubed"
{
    Properties
    {
        [HideInInspector] shader_is_using_thry_editor ("", Float) = 0
        [HideInInspector] shader_master_label ("<color=#008080>Poiyomi³</color>", Float) = 0
        [HideInInspector] shader_presets ("poiToonPresets", Float) = 0
        
        
        [HideInInspector] footer_youtube ("youtube footer button", Float) = 0
        [HideInInspector] footer_twitter ("twitter footer button", Float) = 0
        [HideInInspector] footer_patreon ("patreon footer button", Float) = 0
        [HideInInspector] footer_discord ("discord footer button", Float) = 0
        [HideInInspector] footer_github ("github footer button", Float) = 0
        
        // Warning that only shows up when ThryEditor hasn't loaded
        [Header(POIYOMI SHADER UI FAILED TO LOAD)]
        [Header(.    This is caused by scripts failing to compile. It can be fixed.)]
        [Header(.          The inspector will look broken and will not work properly until fixed.)]
        [Header(.    Please check your console for script errors.)]
        [Header(.          You can filter by errors in the console window.)]
        [Header(.          Often the topmost error points to the erroring script.)]
        [Space(30)][Header(Common Error Causes)]
        [Header(.    Installing multiple Poiyomi Shader packages)]
        [Header(.          Make sure to delete the Poiyomi shader folder before you update Poiyomi.)]
        [Header(.          If a package came with Poiyomi this is bad practice and can cause issues.)]
        [Header(.          Delete the package and import it without any Poiyomi components.)]
        [Header(.    Bad VRCSDK installation (e.g. Both VCC and Standalone))]
        [Header(.          Delete the VRCSDK Folder in Assets if you are using the VCC.)]
        [Header(.          Avoid using third party SDKs. They can cause incompatibility.)]
        [Header(.    Script Errors in other scripts)]
        [Header(.          Outdated tools or prefabs can cause this.)]
        [Header(.          Update things that are throwing errors or move them outside the project.)]
        [Space(30)][Header(Visit Our Discord to Ask For Help)]
        [Space(5)]_ShaderUIWarning0 (" → discord.gg/poiyomi ←    We can help you get it fixed!                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         --{condition_showS:(0==1)}", Int) = -0
        [Space(1400)][Header(POIYOMI SHADER UI FAILED TO LOAD)]
        _ShaderUIWarning1 ("Please scroll up for more information!                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     --{condition_showS:(0==1)}", Int) = -0
        
        [HideInInspector] m_mainOptions ("Main", Float) = 0
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Desaturation ("Saturation", Range(-1, 1)) = 0
        _MainTex ("Texture", 2D) = "white" { }
        [Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3, DistortedUV1, 4)] _MainTextureUV ("Tex UV#", Int) = 0
        [Normal]_BumpMap ("Normal Map", 2D) = "bump" { }
        _BumpScale ("Normal Intensity", Range(0, 10)) = 1
        _AlphaMask ("Alpha Mask", 2D) = "white" { }
        _Clip ("Alpha Cuttoff", Range(0, 1.001)) = 0.5
        [HideInInspector] m_start_mainAdvanced ("Advanced", Float) = 0
        _GlobalPanSpeed ("Pan Speed XY", Vector) = (0, 0, 0, 0)
        [Normal]_DetailNormalMap ("Detail Map", 2D) = "bump" { }
        _DetailNormalMask ("Detail Mask", 2D) = "white" { }
        _DetailNormalMapScale ("Detail Intensity", Range(0, 10)) = 1
        [HideInInspector] m_end_mainAdvanced ("Advanced", Float) = 0
        
        [HideInInspector] m_CubedOptions ("Cubed Options", Float) = 0
        [Enum(Random, 0, Constructed, 1)] _AssemblyMode("Assembly Mode", Int) = 0
        _TriToCube ("Tri to Cube", Range(0,1)) = 0
        _SpawnAlpha ("Spawn Alpha", Range(0,1)) = 0
        _LocalSpawnOffset ("Local Spawn Offset", Vector) = (0, 0, 0, 0)
        _WorldSpawnOffset ("World Spawn Offset", Vector) = (0, 0, 0, 0)
        _Pieces ("Pieces", Float) = 20
        [HideInInspector] m_start_constructedMode ("Constructed Mode", Float) = 0
        _AssemblyBegin ("Assembly Begin", Vector) = (0, -1, 0, 0)
        _AssemblyEnd ("Assembly End", Vector) = (0, 1, 0, 0)
        [Enum(Off, 0, On, 1)] _CubeGradientDebug("Debug View", Int) = 0
        [HideInInspector] m_end_constructedMode ("Constructed Mode", Float) = 0
        [HideInInspector] m_start_explodeMode ("Explode Mode", Float) = 0
        [Enum(Off, 0, On, 1)] _ExplodeMode("Assembly Mode", Int) = 0
        _ExplodeFadeDistance ("Explode Fade Distance", Float) = .3
        [HideInInspector] m_end_explodeMode ("Explode Mode", Float) = 0

        [HideInInspector] m_metallicOptions ("Metallic", Float) = 0
        _CubeMap ("Baked CubeMap", Cube) = "" { }
        [ToggleUI]_SampleWorld ("Force Baked Cubemap", Range(0, 1)) = 0
        _PurelyAdditive ("Purely Additive", Range(0, 1)) = 0
        _MetallicMap ("Metallic Map", 2D) = "white" { }
        _Metallic ("Metallic", Range(0, 1)) = 0
        _SmoothnessMap ("Smoothness Map", 2D) = "white" { }
        [ToggleUI]_InvertSmoothness ("Invert Smoothness Map", Range(0, 1)) = 0
        _Smoothness ("Smoothness", Range(0, 1)) = 0
        
        [HideInInspector] m_matcapOptions ("Matcap / Sphere Textures", Float) = 0
        _Matcap ("Matcap", 2D) = "white" { }
        _MatcapMap ("Matcap Map", 2D) = "white" { }
        _MatcapColor ("Matcap Color", Color) = (1, 1, 1, 1)
        _MatcapStrength ("Matcap Strength", Range(0, 20)) = 1
        _ReplaceWithMatcap ("Replace With Matcap", Range(0, 1)) = 0
        _MultiplyMatcap ("Multiply Matcap", Range(0, 1)) = 0
        _AddMatcap ("Add Matcap", Range(0, 1)) = 0
        
        [HideInInspector] m_emissionOptions ("Emission", Float) = 0
        [HDR]_EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)
        _EmissionMap ("Emission Map", 2D) = "white" { }
        _EmissionMask ("Emission Mask", 2D) = "white" { }
        _EmissionScrollSpeed ("Emission Scroll Speed", Vector) = (0, 0, 0, 0)
        _EmissionStrength ("Emission Strength", Range(0, 20)) = 0
        
        [HideInInspector] m_start_blinkingEmissionOptions ("Blinking Emission", Float) = 0
        _EmissiveBlink_Min ("Emissive Blink Min", Float) = 1
        _EmissiveBlink_Max ("Emissive Blink Max", Float) = 1
        _EmissiveBlink_Velocity ("Emissive Blink Velocity", Float) = 4
        [HideInInspector] m_end_blinkingEmissionOptions ("Blinking Emission", Float) = 0
        
        [HideInInspector] m_start_scrollingEmissionOptions ("Scrolling Emission", Float) = 0
        [ToggleUI] _ScrollingEmission ("Enable Scrolling Emission", Float) = 0
        _EmissiveScroll_Direction ("Direction", Vector) = (0, -10, 0, 0)
        _EmissiveScroll_Width ("Width", Float) = 10
        _EmissiveScroll_Velocity ("Velocity", Float) = 10
        _EmissiveScroll_Interval ("Interval", Float) = 20
        [HideInInspector] m_end_scrollingEmissionOptions ("Scrolling Emission", Float) = 0
        
        [HideInInspector] m_fakeLightingOptions ("Lighting", Float) = 0
        [Enum(Natural, 0, Controlled, 1)] _LightingType("Lighting Type", Int) = 0
        [Gradient]_ToonRamp ("Lighting Ramp", 2D) = "white" { }
        _ShadowStrength ("Shadow Strength", Range(0, 1)) = 1
        _ShadowOffset ("Shadow Offset", Range(-1, 1)) = 0
        _MinBrightness ("Min Brightness", Range(0, 1)) = 0
        _MaxBrightness ("Max Brightness", Float) = 1
        _AOMap ("AO Map", 2D) = "white" { }
        _AOStrength ("AO Strength", Range(0, 1)) = 1
        [HideInInspector] m_start_lightingAdvanced ("Advanced", Float) = 0
        _IndirectContribution ("Indirect Contribution", Range(0, 1)) = 0
        _AdditiveSoftness ("Additive Softness", Range(0, 0.5)) = 0.05
        _AdditiveOffset ("Additive Offset", Range(-0.5, 0.5)) = 0
        _AttenuationMultiplier ("Attenuation", Range(0, 1)) = 0
        [HideInInspector] m_end_lightingAdvanced ("Advanced", Float) = 0
        
        [HideInInspector] m_specularHighlightsOptions ("Specular Highlights", Float) = 0
        [Enum(Off, 0, Realistic, 1, Toon, 2, soon.jpg, 3)] _SpecularType ("Specular Type", Int) = 0
        _SpecularTint ("Specular Tint", Color) = (1, 1, 1, 1)
        _SpecularSmoothness ("Smoothness", Range(0, 1)) = 0
        _SpecularMap ("Specular Map", 2D) = "white" { }
        [Enum(Alpha, 0, Grayscale, 1)] _SmoothnessFrom ("Smoothness From", Int) = 1
        _SpecularHighTexture ("Specular High Tex", 2D) = "white" { }
        [Enum(Lighting, 0, HighTexture, 1)] _SpecularColorFrom("Specular Color From", Int) = 0

        [HideInInspector] m_subsurfaceOptions ("Subsurface Scattering", Float) = 0
        _SSSColor ("Subsurface Color", Color) = (1, 1, 1, 1)
        _SSSThicknessMap ("Thickness Map", 2D) = "black" { }
        _SSSThicknessMod ("Thickness mod", Range(-1, 1)) = 0
        _SSSStrength ("Attenuation", Range(0, 1)) = 0
        _SSSPower ("Light Spread", Range(1, 100)) = 1
        _SSSDistortion ("Light Distortion", Range(0, 1)) = 0

        [HideInInspector] m_rimLightOptions ("Rim Lighting", Float) = 0
        _RimLightColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimWidth ("Rim Width", Range(0, 1)) = 0.8
        _RimSharpness ("Rim Sharpness", Range(0, 1)) = .25
        _RimStrength ("Rim Emission", Range(0, 20)) = 0
        _RimLightColorBias ("Rim Color Bias", Range(0, 1)) = 0
        _RimTex ("Rim Texture", 2D) = "white" { }
        _RimMask ("Rim Mask", 2D) = "white" { }
        _RimTexPanSpeed ("Rim Texture Pan Speed", Vector) = (0, 0, 0, 0)
        [HideInInspector] m_start_ShadowMix ("Shadow Mix", Float) = 0
        _ShadowMix ("Shadow Mix In", Range(0, 1)) = 0
        _ShadowMixThreshold ("Shadow Mix Threshold", Range(0, 1)) = .5
        _ShadowMixWidthMod ("Shadow Mix Width Mod", Range(0, 10)) = .5
        [HideInInspector] m_end_ShadowMix ("Shadow Mix", Float) = 0

        [HideInInspector] m_StencilPassOptions ("Stencil", Float) = 0
        [IntRange] _StencilRef ("Stencil Reference Value", Range(0, 255)) = 0
        [IntRange] _StencilReadMaskRef ("Stencil ReadMask Value", Range(0, 255)) = 0
        [IntRange] _StencilWriteMaskRef ("Stencil WriteMask Value", Range(0, 255)) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilPassOp ("Stencil Pass Op", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilFailOp ("Stencil Fail Op", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilZFailOp ("Stencil ZFail Op", Float) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilCompareFunction ("Stencil Compare Function", Float) = 8
        
        [HideInInspector] m_funOptions ("Fun", Float) = 0
        [Enum(ShowInBoth, 0, ShowOnlyInMirror, 1, DontShowInMirror, 2)] _Mirror ("Show in mirror", Int) = 0
        
        [HideInInspector] m_miscOptions ("Misc", Float) = 0
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
        [Enum(UnityEngine.Rendering.BlendMode)] _SourceBlend ("Source Blend", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)] _DestinationBlend ("Destination Blend", Float) = 10
        [Enum(Off, 0, On, 1)] _ZWrite ("ZWrite", Int) = 1
    
    }
    
    CustomEditor "Thry.ShaderEditor"
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        
        Pass
        {
            Name "MainPass"
            Tags { "LightMode" = "ForwardBase" }
            Stencil
            {
                Ref [_StencilRef]
                ReadMask [_StencilReadMaskRef]
                WriteMask [_StencilWriteMaskRef]
                Ref [_StencilRef]
                Comp [_StencilCompareFunction]
                Pass [_StencilPassOp]
                Fail [_StencilFailOp]
                ZFail [_StencilZFailOp]
            }
            ZWrite [_ZWrite]
            Cull [_Cull]
            ZTest [_ZTest]
            CGPROGRAM
            
            #pragma target 5.0
            #define FORWARD_BASE_PASS
            #pragma multi_compile_instancing
            #pragma multi_compile_fwdbase
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_fog
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #include "../Includes/PoiDefaultPass.cginc"
            ENDCG
            
        }
        
        Pass
        {
            Name "ForwardAddPass"
            Tags { "LightMode" = "ForwardAdd" }
            Stencil
            {
                Ref [_StencilRef]
                ReadMask [_StencilReadMaskRef]
                WriteMask [_StencilWriteMaskRef]
                Ref [_StencilRef]
                Comp [_StencilCompareFunction]
                Pass [_StencilPassOp]
                Fail [_StencilFailOp]
                ZFail [_StencilZFailOp]
            }
            ZWrite Off
            Blend One One
            Cull [_Cull]
            ZTest [_ZTest]
            CGPROGRAM
            
            #pragma target 5.0
            #define FORWARD_ADD_PASS
            #pragma multi_compile_instancing
            #pragma multi_compile_fwdadd_fullshadows
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #include "../Includes/PoiDefaultPass.cginc"
            ENDCG
            
        }
        /*
        Pass
        {
            Name "ShadowCasterPass"
            Tags { "LightMode" = "ShadowCaster" }
            Stencil
            {
                Ref [_StencilRef]
                ReadMask [_StencilReadMaskRef]
                WriteMask [_StencilWriteMaskRef]
                Ref [_StencilRef]
                Comp [_StencilCompareFunction]
                Pass [_StencilPassOp]
                Fail [_StencilFailOp]
                ZFail [_StencilZFailOp]
            }
            CGPROGRAM
            
            #pragma target 5.0
            #define CUTOUT
            #pragma multi_compile_instancing
            #pragma vertex vertShadowCaster
            #pragma fragment fragShadowCaster
            #include "../Includes/PoiPassShadow.cginc"
            ENDCG
            
        }
        */
    }
}
