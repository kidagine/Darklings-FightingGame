// Made with Amplify Shader Editor v1.9.0.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Sprite Shaders Ultimate/3D Lit Cutout BuiltIn SSU"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_AlphaClip("Alpha Clip", Range( 0 , 1)) = 0.5
		_NormalMap("Normal Map", 2D) = "bump" {}
		_NormalIntensity("Normal Intensity", Float) = 1
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.5
		_Metallic("Metallic", Range( 0 , 1)) = 0
		[Toggle(_METALLICMAPTOGGLE_ON)] _MetallicMapToggle("Metallic Map Toggle", Float) = 0
		_MetallicMap("Metallic Map", 2D) = "white" {}
		[Toggle(_EMISSIONTOGGLE_ON)] _EmissionToggle("Emission Toggle", Float) = 0
		[HDR]_EmissionTint("Emission Tint", Color) = (2.996078,0.1568628,0.1568628,0)
		_EmissionMap("Emission Map", 2D) = "white" {}
		[Toggle(_VERTEXTINTFIRST_ON)] _VertexTintFirst("Vertex Tint First", Float) = 0
		[Toggle(_PIXELPERFECTSPACE_ON)] _PixelPerfectSpace("Pixel Perfect Space", Float) = 0
		[Toggle(_PIXELPERFECTUV_ON)] _PixelPerfectUV("Pixel Perfect UV", Float) = 0
		[Toggle(_SPRITESHEETFIX_ON)] _SpriteSheetFix("Sprite Sheet Fix", Float) = 0
		_SpriteSheetRect("Sprite Sheet Rect", Vector) = (0,0,1,1)
		[Toggle(_TILINGFIX_ON)] _TilingFix("Tiling Fix", Float) = 0
		[Toggle(_BAKEDMATERIAL_ON)] _BakedMaterial("Baked Material", Float) = 0
		[KeywordEnum(UV,UV_Raw,Object,Object_Scaled,World,UI_Graphic,Screen)] _ShaderSpace("Shader Space", Float) = 0
		_PixelsPerUnit("Pixels Per Unit", Float) = 100
		_ScreenWidthUnits("Screen Width Units", Float) = 10
		_RectWidth("Rect Width", Float) = 100
		_RectHeight("Rect Height", Float) = 100
		[KeywordEnum(None,Full,Mask,Dissolve,Spread)] _ShaderFading("Shader Fading", Float) = 0
		_FadingFade("Fading: Fade", Range( 0 , 1)) = 1
		_FadingPosition("Fading: Position", Vector) = (0,0,0,0)
		_FadingWidth("Fading: Width", Float) = 0.3
		_FadingNoiseFactor("Fading: Noise Factor", Float) = 0.2
		_FadingNoiseScale("Fading: Noise Scale", Vector) = (0.2,0.2,0,0)
		_FadingMask("Fading: Mask", 2D) = "white" {}
		[Toggle(_TOGGLEUNSCALEDTIME_ON)] _ToggleUnscaledTime("Toggle: Unscaled Time", Float) = 0
		[Toggle(_TOGGLECUSTOMTIME_ON)] _ToggleCustomTime("Toggle: Custom Time", Float) = 0
		_TimeValue("Time: Value", Float) = 0
		[Toggle(_TOGGLETIMESPEED_ON)] _ToggleTimeSpeed("Toggle: Time Speed", Float) = 0
		_TimeSpeed("Time: Speed", Float) = 1
		[Toggle(_TOGGLETIMEFPS_ON)] _ToggleTimeFPS("Toggle: Time FPS", Float) = 0
		_TimeFPS("Time: FPS", Float) = 5
		[Toggle(_TOGGLETIMEFREQUENCY_ON)] _ToggleTimeFrequency("Toggle: Time Frequency", Float) = 0
		_TimeFrequency("Time: Frequency", Float) = 2
		_TimeRange("Time: Range", Float) = 0.5
		_UberNoiseTexture("Uber Noise Texture", 2D) = "white" {}
		[Toggle(_ENABLESTRONGTINT_ON)] _EnableStrongTint("Enable Strong Tint", Float) = 0
		_StrongTintFade("Strong Tint: Fade", Range( 0 , 1)) = 1
		[HDR][NoAlpha]_StrongTintTint("Strong Tint: Tint", Color) = (1,1,1,1)
		[Toggle(_STRONGTINTCONTRASTTOGGLE_ON)] _StrongTintContrastToggle("Strong Tint: Contrast Toggle", Float) = 0
		_StrongTintContrast("Strong Tint: Contrast", Float) = 0
		[Toggle(_STRONGTINTMASKTOGGLE_ON)] _StrongTintMaskToggle("Strong Tint: Mask Toggle", Float) = 0
		_StrongTintMask("Strong Tint: Mask", 2D) = "white" {}
		[Toggle(_ENABLEADDCOLOR_ON)] _EnableAddColor("Enable Add Color", Float) = 0
		_AddColorFade("Add Color: Fade", Range( 0 , 1)) = 1
		[HDR]_AddColorColor("Add Color: Color", Color) = (2.996078,0,0,0)
		[Toggle(_ADDCOLORCONTRASTTOGGLE_ON)] _AddColorContrastToggle("Add Color: Contrast Toggle", Float) = 0
		_AddColorContrast("Add Color: Contrast", Float) = 0.5
		[Toggle(_ADDCOLORMASKTOGGLE_ON)] _AddColorMaskToggle("Add Color: Mask Toggle", Float) = 0
		_AddColorMask("Add Color: Mask", 2D) = "white" {}
		[Toggle(_ENABLEALPHATINT_ON)] _EnableAlphaTint("Enable Alpha Tint", Float) = 0
		_AlphaTintFade("Alpha Tint: Fade", Range( 0 , 1)) = 1
		[HDR]_AlphaTintColor("Alpha Tint: Color", Color) = (95.87451,5.019608,95.87451,0)
		_AlphaTintMinAlpha("Alpha Tint: Min Alpha", Range( 0 , 1)) = 0.02
		[Toggle(_ENABLESHADOW_ON)] _EnableShadow("Enable Shadow", Float) = 0
		_ShadowFade("Shadow: Fade", Range( 0 , 1)) = 1
		_ShadowOffset("Shadow: Offset", Vector) = (0.05,-0.05,0,0)
		_ShadowColor("Shadow: Color", Color) = (0,0,0,0)
		[Toggle(_ENABLEBRIGHTNESS_ON)] _EnableBrightness("Enable Brightness", Float) = 0
		_Brightness("Brightness", Float) = 1
		[Toggle(_ENABLECONTRAST_ON)] _EnableContrast("Enable Contrast", Float) = 0
		_Contrast("Contrast", Float) = 1
		[Toggle(_ENABLESATURATION_ON)] _EnableSaturation("Enable Saturation", Float) = 0
		_Saturation("Saturation", Float) = 1
		[Toggle(_ENABLEHUE_ON)] _EnableHue("Enable Hue", Float) = 0
		_Hue("Hue", Range( -1 , 1)) = 0
		[Toggle(_ENABLERECOLORRGB_ON)] _EnableRecolorRGB("Enable Recolor RGB", Float) = 0
		_RecolorRGBFade("Recolor RGB: Fade", Range( 0 , 1)) = 1
		[HDR]_RecolorRGBRedTint("Recolor RGB: Red Tint", Color) = (1,1,1,0.5019608)
		[HDR]_RecolorRGBGreenTint("Recolor RGB: Green Tint", Color) = (1,1,1,0.5019608)
		[HDR]_RecolorRGBBlueTint("Recolor RGB: Blue Tint", Color) = (1,1,1,0.5019608)
		[Toggle(_RECOLORRGBTEXTURETOGGLE_ON)] _RecolorRGBTextureToggle("Recolor RGB: Texture Toggle", Float) = 0
		_RecolorRGBTexture("Recolor RGB: Texture", 2D) = "white" {}
		[Toggle(_ENABLERECOLORRGBYCP_ON)] _EnableRecolorRGBYCP("Enable Recolor RGBYCP", Float) = 0
		_RecolorRGBYCPFade("Recolor RGBYCP: Fade", Range( 0 , 1)) = 1
		[HDR]_RecolorRGBYCPRedTint("Recolor RGBYCP: Red Tint", Color) = (1,1,1,0.5019608)
		[HDR]_RecolorRGBYCPYellowTint("Recolor RGBYCP: Yellow Tint", Color) = (1,1,1,0.5019608)
		[HDR]_RecolorRGBYCPGreenTint("Recolor RGBYCP: Green Tint", Color) = (1,1,1,0.5019608)
		[HDR]_RecolorRGBYCPCyanTint("Recolor RGBYCP: Cyan Tint", Color) = (1,1,1,0.5019608)
		[HDR]_RecolorRGBYCPBlueTint("Recolor RGBYCP: Blue Tint", Color) = (1,1,1,0.5019608)
		[HDR]_RecolorRGBYCPPurpleTint("Recolor RGBYCP: Purple Tint", Color) = (1,1,1,0.5019608)
		[Toggle(_RECOLORRGBYCPTEXTURETOGGLE_ON)] _RecolorRGBYCPTextureToggle("Recolor RGBYCP: Texture Toggle", Float) = 0
		_RecolorRGBYCPTexture("Recolor RGBYCP: Texture", 2D) = "white" {}
		[Toggle(_ENABLEINNEROUTLINE_ON)] _EnableInnerOutline("Enable Inner Outline", Float) = 0
		_InnerOutlineFade("Inner Outline: Fade", Range( 0 , 1)) = 1
		[HDR]_InnerOutlineColor("Inner Outline: Color", Color) = (11.98431,1.254902,1.254902,1)
		_InnerOutlineWidth("Inner Outline: Width", Float) = 0.02
		[Toggle(_INNEROUTLINEDISTORTIONTOGGLE_ON)] _InnerOutlineDistortionToggle("Inner Outline: Distortion Toggle", Float) = 0
		_InnerOutlineDistortionIntensity("Inner Outline: Distortion Intensity", Vector) = (0.01,0.01,0,0)
		_InnerOutlineNoiseScale("Inner Outline: Noise Scale", Vector) = (4,4,0,0)
		_InnerOutlineNoiseSpeed("Inner Outline: Noise Speed", Vector) = (0,0.1,0,0)
		[Toggle(_INNEROUTLINETEXTURETOGGLE_ON)] _InnerOutlineTextureToggle("Inner Outline: Texture Toggle", Float) = 0
		_InnerOutlineTintTexture("Inner Outline: Tint Texture", 2D) = "white" {}
		_InnerOutlineTextureSpeed("Inner Outline: Texture Speed", Vector) = (0.5,0,0,0)
		[Toggle(_INNEROUTLINEOUTLINEONLYTOGGLE_ON)] _InnerOutlineOutlineOnlyToggle("Inner Outline: Outline Only Toggle", Float) = 0
		[Toggle(_ENABLEOUTEROUTLINE_ON)] _EnableOuterOutline("Enable Outer Outline", Float) = 0
		_OuterOutlineFade("Outer Outline: Fade", Range( 0 , 1)) = 1
		[HDR]_OuterOutlineColor("Outer Outline: Color", Color) = (0,0,0,1)
		_OuterOutlineWidth("Outer Outline: Width", Float) = 0.04
		[Toggle(_OUTEROUTLINEDISTORTIONTOGGLE_ON)] _OuterOutlineDistortionToggle("Outer Outline: Distortion Toggle", Float) = 0
		_OuterOutlineDistortionIntensity("Outer Outline: Distortion Intensity", Vector) = (0.01,0.01,0,0)
		_OuterOutlineNoiseScale("Outer Outline: Noise Scale", Vector) = (4,4,0,0)
		_OuterOutlineNoiseSpeed("Outer Outline: Noise Speed", Vector) = (0,0.1,0,0)
		[Toggle(_OUTEROUTLINETEXTURETOGGLE_ON)] _OuterOutlineTextureToggle("Outer Outline: Texture Toggle", Float) = 0
		_OuterOutlineTintTexture("Outer Outline: Tint Texture", 2D) = "white" {}
		_OuterOutlineTextureSpeed("Outer Outline: Texture Speed", Vector) = (0.5,0,0,0)
		[Toggle(_OUTEROUTLINEOUTLINEONLYTOGGLE_ON)] _OuterOutlineOutlineOnlyToggle("Outer Outline: Outline Only Toggle", Float) = 0
		[Toggle(_ENABLEPIXELOUTLINE_ON)] _EnablePixelOutline("Enable Pixel Outline", Float) = 0
		_PixelOutlineFade("Pixel Outline: Fade", Range( 0 , 1)) = 1
		[HDR]_PixelOutlineColor("Pixel Outline: Color", Color) = (0,0,0,1)
		_PixelOutlineWidth("Pixel Outline: Width", Float) = 1
		[Toggle(_PIXELOUTLINETEXTURETOGGLE_ON)] _PixelOutlineTextureToggle("Pixel Outline: Texture Toggle", Float) = 0
		_PixelOutlineTintTexture("Pixel Outline: Tint Texture", 2D) = "white" {}
		_PixelOutlineTextureSpeed("Pixel Outline: Texture Speed", Vector) = (0.5,0,0,0)
		[Toggle(_PIXELOUTLINEOUTLINEONLYTOGGLE_ON)] _PixelOutlineOutlineOnlyToggle("Pixel Outline: Outline Only Toggle", Float) = 0
		[Toggle(_ENABLEADDHUE_ON)] _EnableAddHue("Enable Add Hue", Float) = 0
		_AddHueFade("Add Hue: Fade", Range( 0 , 1)) = 1
		_AddHueSpeed("Add Hue: Speed", Float) = 1
		_AddHueBrightness("Add Hue: Brightness", Float) = 2
		_AddHueSaturation("Add Hue: Saturation", Range( 0 , 1)) = 1
		_AddHueContrast("Add Hue: Contrast", Float) = 0.5
		[Toggle(_ADDHUEMASKTOGGLE_ON)] _AddHueMaskToggle("Add Hue: Mask Toggle", Float) = 0
		_AddHueMask("Add Hue: Mask", 2D) = "white" {}
		[Toggle(_ENABLEPINGPONGGLOW_ON)] _EnablePingPongGlow("Enable Ping-Pong Glow", Float) = 0
		_PingPongGlowFade("Ping-Pong Glow: Fade", Range( 0 , 1)) = 1
		[HDR]_PingPongGlowFrom("Ping-Pong Glow: From", Color) = (5.992157,0.1882353,0.1882353,0)
		[HDR]_PingPongGlowTo("Ping-Pong Glow: To", Color) = (0.1882353,0.1882353,5.992157,0)
		_PingPongGlowFrequency("Ping-Pong Glow: Frequency", Float) = 3
		_PingPongGlowContrast("Ping-Pong Glow: Contrast", Float) = 1
		[Toggle(_ENABLESHIFTHUE_ON)] _EnableShiftHue("Enable Shift Hue", Float) = 0
		_ShiftHueSpeed("Shift Hue: Speed", Float) = 0.5
		[Toggle(_ENABLEINKSPREAD_ON)] _EnableInkSpread("Enable Ink Spread", Float) = 0
		_InkSpreadFade("Ink Spread: Fade", Range( 0 , 1)) = 1
		[HDR]_InkSpreadColor("Ink Spread: Color", Color) = (8.47419,5.013525,0.08873497,0)
		_InkSpreadContrast("Ink Spread: Contrast", Float) = 2
		_InkSpreadDistance("Ink Spread: Distance", Float) = 3
		_InkSpreadPosition("Ink Spread: Position", Vector) = (0.5,-1,0,0)
		_InkSpreadWidth("Ink Spread: Width", Float) = 0.2
		_InkSpreadNoiseScale("Ink Spread: Noise Scale", Vector) = (0.4,0.4,0,0)
		_InkSpreadNoiseFactor("Ink Spread: Noise Factor", Float) = 0.5
		[Toggle(_ENABLEBLACKTINT_ON)] _EnableBlackTint("Enable Black Tint", Float) = 0
		_BlackTintFade("Black Tint: Fade", Range( 0 , 1)) = 1
		[HDR]_BlackTintColor("Black Tint: Color", Color) = (0,0,1,0)
		_BlackTintPower("Black Tint: Power", Float) = 4
		[Toggle(_ENABLESINEGLOW_ON)] _EnableSineGlow("Enable Sine Glow", Float) = 0
		_SineGlowFade("Sine Glow: Fade", Range( 0 , 1)) = 1
		[HDR]_SineGlowColor("Sine Glow: Color", Color) = (0,2.007843,2.996078,0)
		_SineGlowContrast("Sine Glow: Contrast", Float) = 1
		_SineGlowFrequency("Sine Glow: Frequency", Float) = 4
		_SineGlowMin("Sine Glow: Min", Float) = 0
		_SineGlowMax("Sine Glow: Max", Float) = 1
		[Toggle(_SINEGLOWMASKTOGGLE_ON)] _SineGlowMaskToggle("Sine Glow: Mask Toggle", Float) = 0
		_SineGlowMask("Sine Glow: Mask", 2D) = "white" {}
		[Toggle(_ENABLESPLITTONING_ON)] _EnableSplitToning("Enable Split Toning", Float) = 0
		_SplitToningFade("Split Toning: Fade", Range( 0 , 1)) = 1
		[HDR]_SplitToningHighlightsColor("Split Toning: Highlights Color", Color) = (1,0.1,0.1,0)
		[HDR]_SplitToningShadowsColor("Split Toning: Shadows Color", Color) = (0.1,0.4000002,1,0)
		_SplitToningContrast("Split Toning: Contrast", Float) = 1
		_SplitToningBalance("Split Toning: Balance", Float) = 1
		_SplitToningShift("Split Toning: Shift", Range( -1 , 1)) = 0
		[Toggle(_ENABLECOLORREPLACE_ON)] _EnableColorReplace("Enable Color Replace", Float) = 0
		_ColorReplaceFade("Color Replace: Fade", Range( 0 , 1)) = 1
		_ColorReplaceFromColor("Color Replace: From Color", Color) = (0,0,0,0)
		[HDR]_ColorReplaceToColor("Color Replace: To Color", Color) = (0,0,0.2,0)
		_ColorReplaceRange("Color Replace: Range", Float) = 0.05
		_ColorReplaceSmoothness("Color Replace: Smoothness", Float) = 0.1
		_ColorReplaceContrast("Color Replace: Contrast", Float) = 1
		[Toggle(_ENABLENEGATIVE_ON)] _EnableNegative("Enable Negative", Float) = 0
		_NegativeFade("Negative: Fade", Range( 0 , 1)) = 1
		[Toggle(_ENABLEHOLOGRAM_ON)] _EnableHologram("Enable Hologram", Float) = 0
		_HologramFade("Hologram: Fade", Range( 0 , 1)) = 1
		[HDR]_HologramTint("Hologram: Tint", Color) = (0.3137255,1.662745,2.996078,1)
		_HologramContrast("Hologram: Contrast", Float) = 1
		_HologramLineFrequency("Hologram: Line Frequency", Float) = 500
		_HologramLineGap("Hologram: Line Gap", Range( 0 , 5)) = 3
		_HologramLineSpeed("Hologram: Line Speed", Float) = 0.01
		_HologramMinAlpha("Hologram: Min Alpha", Range( 0 , 1)) = 0.2
		_HologramDistortionOffset("Hologram: Distortion Offset", Float) = 0.5
		_HologramDistortionSpeed("Hologram: Distortion Speed", Float) = 2
		_HologramDistortionDensity("Hologram: Distortion Density", Float) = 0.5
		_HologramDistortionScale("Hologram: Distortion Scale", Float) = 10
		[Toggle(_ENABLEGLITCH_ON)] _EnableGlitch("Enable Glitch", Float) = 0
		_GlitchFade("Glitch: Fade", Range( 0 , 1)) = 1
		_GlitchMaskMin("Glitch: Mask Min", Range( 0 , 1)) = 0.4
		_GlitchMaskScale("Glitch: Mask Scale", Vector) = (0,0.2,0,0)
		_GlitchMaskSpeed("Glitch: Mask Speed", Vector) = (0,4,0,0)
		_GlitchHueSpeed("Glitch: Hue Speed", Float) = 1
		_GlitchBrightness("Glitch: Brightness", Float) = 4
		_GlitchNoiseScale("Glitch: Noise Scale", Vector) = (0,3,0,0)
		_GlitchNoiseSpeed("Glitch: Noise Speed", Vector) = (0,1,0,0)
		_GlitchDistortion("Glitch: Distortion", Vector) = (0.1,0,0,0)
		_GlitchDistortionScale("Glitch: Distortion Scale", Vector) = (0,3,0,0)
		_GlitchDistortionSpeed("Glitch: Distortion Speed", Vector) = (0,1,0,0)
		[Toggle(_ENABLEFROZEN_ON)] _EnableFrozen("Enable Frozen", Float) = 0
		_FrozenFade("Frozen: Fade", Range( 0 , 1)) = 1
		[HDR]_FrozenTint("Frozen: Tint", Color) = (1.819608,4.611765,5.992157,0)
		_FrozenContrast("Frozen: Contrast", Float) = 2
		[HDR]_FrozenSnowColor("Frozen: Snow Color", Color) = (1.123529,1.373203,1.498039,0)
		_FrozenSnowContrast("Frozen: Snow Contrast", Float) = 1
		_FrozenSnowDensity("Frozen: Snow Density", Range( 0 , 1)) = 0.25
		_FrozenSnowScale("Frozen: Snow Scale", Vector) = (0.1,0.1,0,0)
		[HDR]_FrozenHighlightColor("Frozen: Highlight Color", Color) = (1.797647,4.604501,5.992157,1)
		_FrozenHighlightContrast("Frozen: Highlight Contrast", Float) = 2
		_FrozenHighlightDensity("Frozen: Highlight Density", Range( 0 , 1)) = 1
		_FrozenHighlightSpeed("Frozen: Highlight Speed", Vector) = (0.1,0.1,0,0)
		_FrozenHighlightScale("Frozen: Highlight Scale", Vector) = (0.2,0.2,0,0)
		_FrozenHighlightDistortion("Frozen: Highlight Distortion", Vector) = (0.5,0.5,0,0)
		_FrozenHighlightDistortionSpeed("Frozen: Highlight Distortion Speed", Vector) = (-0.05,-0.05,0,0)
		_FrozenHighlightDistortionScale("Frozen: Highlight Distortion Scale", Vector) = (0.2,0.2,0,0)
		[Toggle(_ENABLERAINBOW_ON)] _EnableRainbow("Enable Rainbow", Float) = 0
		_RainbowFade("Rainbow: Fade", Range( 0 , 1)) = 1
		_RainbowBrightness("Rainbow: Brightness", Float) = 2
		_RainbowSaturation("Rainbow: Saturation", Range( 0 , 1)) = 1
		_RainbowContrast("Rainbow: Contrast", Float) = 1
		_RainbowSpeed("Rainbow: Speed", Float) = 1
		_RainbowDensity("Rainbow: Density", Float) = 0.5
		_RainbowCenter("Rainbow: Center", Vector) = (0,0,0,0)
		_RainbowNoiseScale("Rainbow: Noise Scale", Vector) = (0.2,0.2,0,0)
		_RainbowNoiseFactor("Rainbow: Noise Factor", Float) = 0.2
		[Toggle(_ENABLECAMOUFLAGE_ON)] _EnableCamouflage("Enable Camouflage", Float) = 0
		_CamouflageFade("Camouflage: Fade", Range( 0 , 1)) = 1
		_CamouflageBaseColor("Camouflage: Base Color", Color) = (0.7450981,0.7254902,0.5686275,0)
		_CamouflageContrast("Camouflage: Contrast", Float) = 1
		_CamouflageColorA("Camouflage: Color A", Color) = (0.627451,0.5882353,0.4313726,0)
		_CamouflageDensityA("Camouflage: Density A", Range( 0 , 1)) = 0.4
		_CamouflageSmoothnessA("Camouflage: Smoothness A", Range( 0 , 1)) = 0.2
		_CamouflageNoiseScaleA("Camouflage: Noise Scale A", Vector) = (0.25,0.25,0,0)
		_CamouflageColorB("Camouflage: Color B", Color) = (0.4705882,0.4313726,0.3137255,0)
		_CamouflageDensityB("Camouflage: Density B", Range( 0 , 1)) = 0.4
		_CamouflageSmoothnessB("Camouflage: Smoothness B", Range( 0 , 1)) = 0.2
		_CamouflageNoiseScaleB("Camouflage: Noise Scale B", Vector) = (0.25,0.25,0,0)
		[Toggle(_CAMOUFLAGEANIMATIONTOGGLE_ON)] _CamouflageAnimationToggle("Camouflage: Animation Toggle", Float) = 0
		_CamouflageDistortionSpeed("Camouflage: Distortion Speed", Vector) = (0.1,0.1,0,0)
		_CamouflageDistortionIntensity("Camouflage: Distortion Intensity", Vector) = (0.1,0.1,0,0)
		_CamouflageDistortionScale("Camouflage: Distortion Scale", Vector) = (0.5,0.5,0,0)
		[Toggle(_ENABLEMETAL_ON)] _EnableMetal("Enable Metal", Float) = 0
		_MetalFade("Metal: Fade", Range( 0 , 1)) = 1
		[HDR]_MetalColor("Metal: Color", Color) = (5.992157,3.639216,0.3137255,1)
		_MetalContrast("Metal: Contrast", Float) = 2
		[HDR]_MetalHighlightColor("Metal: Highlight Color", Color) = (5.992157,3.796078,0.6588235,1)
		_MetalHighlightDensity("Metal: Highlight Density", Range( 0 , 1)) = 1
		_MetalHighlightContrast("Metal: Highlight Contrast", Float) = 2
		_MetalNoiseScale("Metal: Noise Scale", Vector) = (0.25,0.25,0,0)
		_MetalNoiseSpeed("Metal: Noise Speed", Vector) = (0.05,0.05,0,0)
		_MetalNoiseDistortionScale("Metal: Noise Distortion Scale", Vector) = (0.2,0.2,0,0)
		_MetalNoiseDistortionSpeed("Metal: Noise Distortion Speed", Vector) = (-0.05,-0.05,0,0)
		_MetalNoiseDistortion("Metal: Noise Distortion", Vector) = (0.5,0.5,0,0)
		[Toggle(_METALMASKTOGGLE_ON)] _MetalMaskToggle("Metal: Mask Toggle", Float) = 0
		[NoScaleOffset]_MetalMask("Metal: Mask", 2D) = "white" {}
		[Toggle(_ENABLESHINE_ON)] _EnableShine("Enable Shine", Float) = 0
		_ShineFade("Shine: Fade", Range( 0 , 1)) = 1
		[HDR]_ShineColor("Shine: Color", Color) = (11.98431,11.98431,11.98431,0)
		_ShineSaturation("Shine: Saturation", Range( 0 , 1)) = 0.5
		_ShineContrast("Shine: Contrast", Float) = 2
		_ShineWidth("Shine: Width", Range( 0.001 , 1)) = 0.1
		_ShineSpeed("Shine: Speed", Float) = 5
		_ShineRotation("Shine: Rotation", Range( 0 , 360)) = 30
		_ShineFrequency("Shine: Frequency", Float) = 0.3
		[Toggle(_SHINEMASKTOGGLE_ON)] _ShineMaskToggle("Shine: Mask Toggle", Float) = 0
		[NoScaleOffset]_ShineMask("Shine: Mask", 2D) = "white" {}
		[Toggle(_ENABLEBURN_ON)] _EnableBurn("Enable Burn", Float) = 0
		_BurnFade("Burn: Fade", Range( 0 , 1)) = 1
		_BurnPosition("Burn: Position", Vector) = (0,5,0,0)
		_BurnRadius("Burn: Radius", Float) = 5
		[HDR]_BurnEdgeColor("Burn: Edge Color", Color) = (11.98431,1.129412,0.1254902,0)
		_BurnWidth("Burn: Width", Float) = 0.1
		_BurnEdgeNoiseScale("Burn: Edge Noise Scale", Vector) = (0.3,0.3,0,0)
		_BurnEdgeNoiseFactor("Burn: Edge Noise Factor", Float) = 0.5
		[HDR]_BurnInsideColor("Burn: Inside Color", Color) = (0.75,0.5625,0.525,0)
		_BurnInsideContrast("Burn: Inside Contrast", Float) = 2
		[HDR]_BurnInsideNoiseColor("Burn: Inside Noise Color", Color) = (3084.047,257.0039,0,0)
		_BurnInsideNoiseFactor("Burn: Inside Noise Factor", Float) = 0.2
		_BurnInsideNoiseScale("Burn: Inside Noise Scale", Vector) = (0.5,0.5,0,0)
		_BurnSwirlFactor("Burn: Swirl Factor", Float) = 1
		_BurnSwirlNoiseScale("Burn: Swirl Noise Scale", Vector) = (0.1,0.1,0,0)
		[Toggle(_ENABLEPOISON_ON)] _EnablePoison("Enable Poison", Float) = 0
		_PoisonFade("Poison: Fade", Range( 0 , 1)) = 1
		[HDR]_PoisonColor("Poison: Color", Color) = (0.3137255,2.996078,0.3137255,0)
		_PoisonDensity("Poison: Density", Float) = 3
		_PoisonRecolorFactor("Poison: Recolor Factor", Range( 0 , 1)) = 0.5
		_PoisonShiftSpeed("Poison: Shift Speed", Float) = 0.2
		_PoisonNoiseBrightness("Poison: Noise Brightness", Float) = 2
		_PoisonNoiseScale("Poison: Noise Scale", Vector) = (0.2,0.2,0,0)
		_PoisonNoiseSpeed("Poison: Noise Speed", Vector) = (0,-0.2,0,0)
		[Toggle(_ENABLEENCHANTED_ON)] _EnableEnchanted("Enable Enchanted", Float) = 0
		_EnchantedFade("Enchanted: Fade", Range( 0 , 1)) = 1
		_EnchantedSpeed("Enchanted: Speed", Vector) = (0,1,0,0)
		_EnchantedScale("Enchanted: Scale", Vector) = (0.1,0.1,0,0)
		_EnchantedBrightness("Enchanted: Brightness", Float) = 1
		_EnchantedContrast("Enchanted: Contrast", Float) = 0.5
		_EnchantedReduce("Enchanted: Reduce", Range( 0 , 2)) = 0
		[Toggle(_ENCHANTEDRAINBOWTOGGLE_ON)] _EnchantedRainbowToggle("Enchanted: Rainbow Toggle", Float) = 0
		_EnchantedRainbowSpeed("Enchanted: Rainbow Speed", Float) = 0.5
		_EnchantedRainbowDensity("Enchanted: Rainbow Density", Float) = 0.5
		_EnchantedRainbowSaturation("Enchanted: Rainbow Saturation", Float) = 0.8
		[HDR]_EnchantedLowColor("Enchanted: Low Color", Color) = (2.996078,0,0,0)
		[HDR]_EnchantedHighColor("Enchanted: High Color", Color) = (0,0.7098798,4.237095,0)
		[Toggle(_ENCHANTEDLERPTOGGLE_ON)] _EnchantedLerpToggle("Enchanted: Lerp Toggle", Float) = 0
		[Toggle(_ENABLESHIFTING_ON)] _EnableShifting("Enable Shifting", Float) = 0
		_ShiftingFade("Shifting: Fade", Range( 0 , 1)) = 1
		_ShiftingSpeed("Shifting: Speed", Float) = 0.5
		_ShiftingDensity("Shifting: Density", Float) = 1.5
		_ShiftingBrightness("Shifting: Brightness", Float) = 1
		_ShiftingContrast("Shifting: Contrast", Float) = 0.5
		[Toggle(_SHIFTINGRAINBOWTOGGLE_ON)] _ShiftingRainbowToggle("Shifting: Rainbow Toggle", Float) = 0
		_ShiftingSaturation("Shifting: Saturation", Float) = 0.8
		[HDR]_ShiftingColorA("Shifting: Color A", Color) = (1.498039,0,0,0)
		[HDR]_ShiftingColorB("Shifting: Color B", Color) = (1.498039,0.7490196,0,0)
		[Toggle(_ENABLEFULLALPHADISSOLVE_ON)] _EnableFullAlphaDissolve("Enable Full Alpha Dissolve", Float) = 0
		_FullAlphaDissolveFade("Full Alpha Dissolve: Fade", Range( 0 , 1)) = 0.5
		_FullAlphaDissolveWidth("Full Alpha Dissolve: Width", Float) = 0.5
		_FullAlphaDissolveNoiseScale("Full Alpha Dissolve: Noise Scale", Vector) = (0.1,0.1,0,0)
		[Toggle(_ENABLEFULLGLOWDISSOLVE_ON)] _EnableFullGlowDissolve("Enable Full Glow Dissolve", Float) = 0
		_FullGlowDissolveFade("Full Glow Dissolve: Fade", Range( 0 , 1)) = 0.5
		_FullGlowDissolveWidth("Full Glow Dissolve: Width", Float) = 0.5
		[HDR]_FullGlowDissolveEdgeColor("Full Glow Dissolve: Edge Color", Color) = (11.98431,0.627451,0.627451,0)
		_FullGlowDissolveNoiseScale("Full Glow Dissolve: Noise Scale", Vector) = (0.1,0.1,0,0)
		[Toggle(_ENABLESOURCEALPHADISSOLVE_ON)] _EnableSourceAlphaDissolve("Enable Source Alpha Dissolve", Float) = 0
		_SourceAlphaDissolveFade("Source Alpha Dissolve: Fade", Float) = 1
		_SourceAlphaDissolvePosition("Source Alpha Dissolve: Position", Vector) = (0,0,0,0)
		_SourceAlphaDissolveWidth("Source Alpha Dissolve: Width", Float) = 0.2
		_SourceAlphaDissolveNoiseScale("Source Alpha Dissolve: Noise Scale", Vector) = (0.3,0.3,0,0)
		_SourceAlphaDissolveNoiseFactor("Source Alpha Dissolve: Noise Factor", Float) = 0.2
		[Toggle]_SourceAlphaDissolveInvert("Source Alpha Dissolve: Invert", Float) = 0
		[Toggle(_ENABLESOURCEGLOWDISSOLVE_ON)] _EnableSourceGlowDissolve("Enable Source Glow Dissolve", Float) = 0
		_SourceGlowDissolveFade("Source Glow Dissolve: Fade", Float) = 1
		_SourceGlowDissolvePosition("Source Glow Dissolve: Position", Vector) = (0,0,0,0)
		_SourceGlowDissolveWidth("Source Glow Dissolve: Width", Float) = 0.1
		[HDR]_SourceGlowDissolveEdgeColor("Source Glow Dissolve: Edge Color", Color) = (11.98431,0.627451,0.627451,0)
		_SourceGlowDissolveNoiseScale("Source Glow Dissolve: Noise Scale", Vector) = (0.3,0.3,0,0)
		_SourceGlowDissolveNoiseFactor("Source Glow Dissolve: Noise Factor", Float) = 0.2
		[Toggle]_SourceGlowDissolveInvert("Source Glow Dissolve: Invert", Float) = 0
		[Toggle(_ENABLEHALFTONE_ON)] _EnableHalftone("Enable Halftone", Float) = 0
		_HalftoneFade("Halftone: Fade", Float) = 1
		_HalftonePosition("Halftone: Position", Vector) = (0,0,0,0)
		_HalftoneTiling("Halftone: Tiling", Float) = 4
		_HalftoneFadeWidth("Halftone: Width", Float) = 1.5
		[Toggle]_HalftoneInvert("Halftone: Invert", Float) = 0
		[Toggle(_ENABLEDIRECTIONALALPHAFADE_ON)] _EnableDirectionalAlphaFade("Enable Directional Alpha Fade", Float) = 0
		_DirectionalAlphaFadeFade("Directional Alpha Fade: Fade", Float) = 0
		_DirectionalAlphaFadeRotation("Directional Alpha Fade: Rotation", Range( 0 , 360)) = 0
		_DirectionalAlphaFadeWidth("Directional Alpha Fade: Width", Float) = 0.2
		_DirectionalAlphaFadeNoiseScale("Directional Alpha Fade: Noise Scale", Vector) = (0.3,0.3,0,0)
		_DirectionalAlphaFadeNoiseFactor("Directional Alpha Fade: Noise Factor", Float) = 0.2
		[Toggle]_DirectionalAlphaFadeInvert("Directional Alpha Fade: Invert", Float) = 0
		[Toggle(_ENABLEDIRECTIONALGLOWFADE_ON)] _EnableDirectionalGlowFade("Enable Directional Glow Fade", Float) = 0
		_DirectionalGlowFadeFade("Directional Glow Fade: Fade", Float) = 0
		_DirectionalGlowFadeRotation("Directional Glow Fade: Rotation", Range( 0 , 360)) = 0
		[HDR]_DirectionalGlowFadeEdgeColor("Directional Glow Fade: Edge Color", Color) = (11.98431,0.6901961,0.6901961,0)
		_DirectionalGlowFadeWidth("Directional Glow Fade: Width", Float) = 0.1
		_DirectionalGlowFadeNoiseScale("Directional Glow Fade: Noise Scale", Vector) = (0.4,0.4,0,0)
		_DirectionalGlowFadeNoiseFactor("Directional Glow Fade: Noise Factor", Float) = 0.2
		[Toggle]_DirectionalGlowFadeInvert("Directional Glow Fade: Invert", Float) = 0
		[Toggle(_ENABLEDIRECTIONALDISTORTION_ON)] _EnableDirectionalDistortion("Enable Directional Distortion", Float) = 0
		_DirectionalDistortionFade("Directional Distortion: Fade", Float) = 0
		_DirectionalDistortionRotation("Directional Distortion: Rotation", Range( 0 , 360)) = 0
		_DirectionalDistortionWidth("Directional Distortion: Width", Float) = 0.5
		_DirectionalDistortionNoiseScale("Directional Distortion: Noise Scale", Vector) = (0.4,0.4,0,0)
		_DirectionalDistortionNoiseFactor("Directional Distortion: Noise Factor", Float) = 0.2
		_DirectionalDistortionDistortion("Directional Distortion: Distortion", Vector) = (0,0.1,0,0)
		_DirectionalDistortionRandomDirection("Directional Distortion: Random Direction", Range( 0 , 1)) = 0.1
		_DirectionalDistortionDistortionScale("Directional Distortion: Distortion Scale", Vector) = (1,1,0,0)
		[Toggle]_DirectionalDistortionInvert("Directional Distortion: Invert", Float) = 0
		[Toggle(_ENABLEFULLDISTORTION_ON)] _EnableFullDistortion("Enable Full Distortion", Float) = 0
		_FullDistortionFade("Full Distortion: Fade", Range( 0 , 1)) = 1
		_FullDistortionDistortion("Full Distortion: Distortion", Vector) = (0.2,0.2,0,0)
		_FullDistortionNoiseScale("Full Distortion: Noise Scale", Vector) = (0.5,0.5,0,0)
		[Toggle(_ENABLEPIXELATE_ON)] _EnablePixelate("Enable Pixelate", Float) = 0
		_PixelateFade("Pixelate: Fade", Range( 0 , 1)) = 1
		_PixelatePixelsPerUnit("Pixelate: Pixels Per Unit", Float) = 100
		_PixelatePixelDensity("Pixelate: Pixel Density", Float) = 16
		[Toggle(_ENABLESQUEEZE_ON)] _EnableSqueeze("Enable Squeeze", Float) = 0
		_SqueezeFade("Squeeze: Fade", Range( 0 , 1)) = 1
		_SqueezeScale("Squeeze: Scale", Vector) = (2,0,0,0)
		_SqueezePower("Squeeze: Power", Float) = 1
		_SqueezeCenter("Squeeze: Center", Vector) = (0.5,0.5,0,0)
		[Toggle(_ENABLEUVDISTORT_ON)] _EnableUVDistort("Enable UV Distort", Float) = 0
		_UVDistortFade("UV Distort: Fade", Range( 0 , 1)) = 1
		_UVDistortFrom("UV Distort: From", Vector) = (-0.02,-0.02,0,0)
		_UVDistortTo("UV Distort: To", Vector) = (0.02,0.02,0,0)
		_UVDistortSpeed("UV Distort: Speed", Vector) = (2,2,0,0)
		_UVDistortNoiseScale("UV Distort: Noise Scale", Vector) = (0.1,0.1,0,0)
		[Toggle(_UVDISTORTMASKTOGGLE_ON)] _UVDistortMaskToggle("UV Distort: Mask Toggle", Float) = 0
		[NoScaleOffset]_UVDistortMask("UV Distort: Mask", 2D) = "white" {}
		[Toggle(_ENABLEUVSCROLL_ON)] _EnableUVScroll("Enable UV Scroll", Float) = 0
		_UVScrollSpeed("UV Scroll: Speed", Vector) = (0.2,0,0,0)
		[Toggle(_ENABLEUVROTATE_ON)] _EnableUVRotate("Enable UV Rotate", Float) = 0
		_UVRotateSpeed("UV Rotate: Speed", Float) = 1
		_UVRotatePivot("UV Rotate: Pivot", Vector) = (0.5,0.5,0,0)
		[Toggle(_ENABLESINEROTATE_ON)] _EnableSineRotate("Enable Sine Rotate", Float) = 0
		_SineRotateFade("Sine Rotate: Fade", Range( 0 , 1)) = 1
		_SineRotateAngle("Sine Rotate: Angle", Float) = 15
		_SineRotateFrequency("Sine Rotate: Frequency", Float) = 1
		_SineRotatePivot("Sine Rotate: Pivot", Vector) = (0.5,0.5,0,0)
		[Toggle(_ENABLEWIGGLE_ON)] _EnableWiggle("Enable Wiggle", Float) = 0
		_WiggleFade("Wiggle: Fade", Range( 0 , 1)) = 1
		_WiggleSpeed("Wiggle: Speed", Float) = 2
		_WiggleFrequency("Wiggle: Frequency", Float) = 2
		_WiggleOffset("Wiggle: Offset", Float) = 0.02
		[Toggle(_WIGGLEFIXEDGROUNDTOGGLE_ON)] _WiggleFixedGroundToggle("Wiggle: Fixed Ground Toggle", Float) = 0
		[Toggle(_ENABLEUVSCALE_ON)] _EnableUVScale("Enable UV Scale", Float) = 0
		_UVScaleScale("UV Scale: Scale", Vector) = (1,1,0,0)
		_UVScalePivot("UV Scale: Pivot", Vector) = (0.5,0.5,0,0)
		[Toggle(_ENABLESINEMOVE_ON)] _EnableSineMove("Enable Sine Move", Float) = 0
		_SineMoveFade("Sine Move: Fade", Range( 0 , 1)) = 1
		_SineMoveOffset("Sine Move: Offset", Vector) = (0,0.5,0,0)
		_SineMoveFrequency("Sine Move: Frequency", Vector) = (1,1,0,0)
		[Toggle(_ENABLESINESCALE_ON)] _EnableSineScale("Enable Sine Scale", Float) = 0
		_SineScaleFrequency("Sine Scale: Frequency", Float) = 2
		_SineScaleFactor("Sine Scale: Factor", Vector) = (0.2,0.2,0,0)
		[Toggle(_ENABLEVIBRATE_ON)] _EnableVibrate("Enable Vibrate", Float) = 0
		_VibrateFade("Vibrate: Fade", Range( 0 , 1)) = 1
		_VibrateOffset("Vibrate: Offset", Float) = 0.04
		_VibrateFrequency("Vibrate: Frequency", Float) = 100
		_VibrateRotation("Vibrate: Rotation", Float) = 4
		[Toggle(_ENABLEWIND_ON)] _EnableWind("Enable Wind", Float) = 0
		_WindRotation("Wind: Rotation", Float) = 0
		_WindMaxRotation("Wind: Max Rotation", Float) = 2
		_WindRotationWindFactor("Wind: Rotation Wind Factor", Float) = 1
		_WindSquishFactor("Wind: Squish Factor", Float) = 0.3
		_WindSquishWindFactor("Wind: Squish Wind Factor", Range( 0 , 1)) = 0
		[Toggle(_WINDLOCALWIND_ON)] _WindLocalWind("Wind: Local Wind", Float) = 0
		_WindNoiseScale("Wind: Noise Scale", Float) = 0.1
		_WindNoiseSpeed("Wind: Noise Speed", Float) = 1
		_WindMinIntensity("Wind: Min Intensity", Float) = -0.4
		_WindMaxIntensity("Wind: Max Intensity", Float) = 0.4
		[Toggle(_WINDHIGHQUALITYNOISE_ON)] _WindHighQualityNoise("Wind: High Quality Noise", Float) = 0
		[Toggle(_WINDISPARALLAX_ON)] _WindIsParallax("Wind: Is Parallax", Float) = 0
		_WindXPosition("Wind: X Position", Float) = 0
		_WindFlip("Wind: Flip", Float) = 0
		[Toggle(_ENABLESQUISH_ON)] _EnableSquish("Enable Squish", Float) = 0
		_SquishFade("Squish: Fade", Range( 0 , 1)) = 1
		_SquishStretch("Squish: Stretch", Float) = 0.1
		_SquishSquish("Squish: Squish", Float) = 0.1
		_SquishFlip("Squish: Flip", Range( -1 , 0)) = 0
		[Toggle(_ENABLECHECKERBOARD_ON)] _EnableCheckerboard("Enable Checkerboard", Float) = 0
		_CheckerboardDarken("Checkerboard: Darken", Range( 0 , 1)) = 0.5
		_CheckerboardTiling("Checkerboard: Tiling", Float) = 1
		[Toggle(_ENABLEFLAME_ON)] _EnableFlame("Enable Flame", Float) = 0
		_FlameBrightness("Flame: Brightness", Float) = 10
		_FlameSmooth("Flame: Smooth", Float) = 2
		_FlameRadius("Flame: Radius", Float) = 0.2
		_FlameSpeed("Flame: Speed", Vector) = (0,-0.5,0,0)
		_FlameNoiseFactor("Flame: Noise Factor", Float) = 2.5
		_FlameNoiseHeightFactor("Flame: Noise Height Factor", Float) = 1.5
		_FlameNoiseScale("Flame: Noise Scale", Vector) = (1.2,0.8,0,0)
		[Toggle(_ENABLEGAUSSIANBLUR_ON)] _EnableGaussianBlur("Enable Gaussian Blur", Float) = 0
		_GaussianBlurFade("Gaussian Blur: Fade", Range( 0 , 1)) = 1
		_GaussianBlurOffset("Gaussian Blur: Offset", Float) = 0.5
		[Toggle(_ENABLESHARPEN_ON)] _EnableSharpen("Enable Sharpen", Float) = 0
		_SharpenFade("Sharpen: Fade", Range( 0 , 1)) = 1
		_SharpenFactor("Sharpen: Factor", Float) = 4
		_SharpenOffset("Sharpen: Offset", Float) = 2
		[Toggle(_ENABLESMOOTHPIXELART_ON)] _EnableSmoothPixelArt("Enable Smooth Pixel Art", Float) = 0
		[Toggle(_ENABLESMOKE_ON)] _EnableSmoke("Enable Smoke", Float) = 0
		_SmokeAlpha("Smoke: Alpha", Range( 0 , 1)) = 1
		_SmokeSmoothness("Smoke: Smoothness", Float) = 1
		_SmokeNoiseScale("Smoke: Noise Scale", Float) = 0.5
		_SmokeNoiseFactor("Smoke: Noise Factor", Range( 0 , 1)) = 0.4
		_SmokeDarkEdge("Smoke: Dark Edge", Range( 0 , 1.5)) = 1
		[Toggle]_SmokeVertexSeed("Smoke: Vertex Seed", Float) = 0
		[Toggle(_ENABLECUSTOMFADE_ON)] _EnableCustomFade("Enable Custom Fade", Float) = 0
		_CustomFadeFadeMask("Custom Fade: Fade Mask", 2D) = "white" {}
		_CustomFadeSmoothness("Custom Fade: Smoothness", Float) = 2
		_CustomFadeNoiseScale("Custom Fade: Noise Scale", Vector) = (1,1,0,0)
		_CustomFadeNoiseFactor("Custom Fade: Noise Factor", Range( 0 , 0.5)) = 0
		_CustomFadeAlpha("Custom Fade: Alpha", Range( 0 , 1)) = 1
		[Toggle(_ENABLEWORLDTILING_ON)] _EnableWorldTiling("Enable World Tiling", Float) = 0
		_WorldTilingScale("World Tiling: Scale", Vector) = (1,1,0,0)
		_WorldTilingOffset("World Tiling: Offset", Vector) = (0,0,0,0)
		_WorldTilingPixelsPerUnit("World Tiling: Pixels Per Unit", Float) = 100
		[Toggle(_ENABLESCREENTILING_ON)] _EnableScreenTiling("Enable Screen Tiling", Float) = 0
		_ScreenTilingScale("Screen Tiling: Scale", Vector) = (1,1,0,0)
		_ScreenTilingOffset("Screen Tiling: Offset", Vector) = (0,0,0,0)
		[ASEEnd]_ScreenTilingPixelsPerUnit("Screen Tiling: Pixels Per Unit", Float) = 100
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

		//_TransmissionShadow( "Transmission Shadow", Range( 0, 1 ) ) = 0.5
		//_TransStrength( "Trans Strength", Range( 0, 50 ) ) = 1
		//_TransNormal( "Trans Normal Distortion", Range( 0, 1 ) ) = 0.5
		//_TransScattering( "Trans Scattering", Range( 1, 50 ) ) = 2
		//_TransDirect( "Trans Direct", Range( 0, 1 ) ) = 0.9
		//_TransAmbient( "Trans Ambient", Range( 0, 1 ) ) = 0.1
		//_TransShadow( "Trans Shadow", Range( 0, 1 ) ) = 0.5
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
		//[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		//[ToggleOff] _GlossyReflections("Reflections", Float) = 1.0
	}
	
	SubShader
	{
		
		Tags { "RenderType"="Opaque" "Queue"="Geometry" "DisableBatching"="False" }
	LOD 0

		Cull Off
		AlphaToMask Off
		ZWrite On
		ZTest LEqual
		ColorMask RGBA
		
		Blend Off
		

		CGINCLUDE
		#pragma target 3.0

		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		ENDCG

		
		Pass
		{
			
			Name "ForwardBase"
			Tags { "LightMode"="ForwardBase" }
			
			Blend One Zero

			CGPROGRAM
			#define ASE_NEEDS_FRAG_SHADOWCOORDS
			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _ALPHATEST_ON 1

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#ifndef UNITY_PASS_FORWARDBASE
				#define UNITY_PASS_FORWARDBASE
			#endif
			#include "HLSLSupport.cginc"
			#ifndef UNITY_INSTANCED_LOD_FADE
				#define UNITY_INSTANCED_LOD_FADE
			#endif
			#ifndef UNITY_INSTANCED_SH
				#define UNITY_INSTANCED_SH
			#endif
			#ifndef UNITY_INSTANCED_LIGHTMAPSTS
				#define UNITY_INSTANCED_LIGHTMAPSTS
			#endif
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			#include "AutoLight.cginc"

			#include "UnityStandardUtils.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_SCREEN_POSITION
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_FRAG_POSITION
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _SHADERFADING_NONE _SHADERFADING_FULL _SHADERFADING_MASK _SHADERFADING_DISSOLVE _SHADERFADING_SPREAD
			#pragma shader_feature_local _ENABLESINESCALE_ON
			#pragma shader_feature _ENABLEVIBRATE_ON
			#pragma shader_feature _ENABLESINEMOVE_ON
			#pragma shader_feature _ENABLESQUISH_ON
			#pragma shader_feature _SPRITESHEETFIX_ON
			#pragma shader_feature_local _PIXELPERFECTUV_ON
			#pragma shader_feature _ENABLEWORLDTILING_ON
			#pragma shader_feature _ENABLESCREENTILING_ON
			#pragma shader_feature _TOGGLETIMEFREQUENCY_ON
			#pragma shader_feature _TOGGLETIMEFPS_ON
			#pragma shader_feature _TOGGLETIMESPEED_ON
			#pragma shader_feature _TOGGLEUNSCALEDTIME_ON
			#pragma shader_feature _TOGGLECUSTOMTIME_ON
			#pragma shader_feature _SHADERSPACE_UV _SHADERSPACE_UV_RAW _SHADERSPACE_OBJECT _SHADERSPACE_OBJECT_SCALED _SHADERSPACE_WORLD _SHADERSPACE_UI_GRAPHIC _SHADERSPACE_SCREEN
			#pragma shader_feature _PIXELPERFECTSPACE_ON
			#pragma shader_feature _BAKEDMATERIAL_ON
			#pragma shader_feature _VERTEXTINTFIRST_ON
			#pragma shader_feature _ENABLESHADOW_ON
			#pragma shader_feature _ENABLESTRONGTINT_ON
			#pragma shader_feature _ENABLEALPHATINT_ON
			#pragma shader_feature_local _ENABLEADDCOLOR_ON
			#pragma shader_feature_local _ENABLEHALFTONE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALGLOWFADE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALALPHAFADE_ON
			#pragma shader_feature_local _ENABLESOURCEGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLESOURCEALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALDISTORTION_ON
			#pragma shader_feature_local _ENABLEFULLDISTORTION_ON
			#pragma shader_feature _ENABLESHIFTING_ON
			#pragma shader_feature _ENABLEENCHANTED_ON
			#pragma shader_feature_local _ENABLEPOISON_ON
			#pragma shader_feature_local _ENABLESHINE_ON
			#pragma shader_feature_local _ENABLERAINBOW_ON
			#pragma shader_feature_local _ENABLEBURN_ON
			#pragma shader_feature_local _ENABLEFROZEN_ON
			#pragma shader_feature_local _ENABLEMETAL_ON
			#pragma shader_feature_local _ENABLECAMOUFLAGE_ON
			#pragma shader_feature_local _ENABLEGLITCH_ON
			#pragma shader_feature_local _ENABLEHOLOGRAM_ON
			#pragma shader_feature _ENABLEPINGPONGGLOW_ON
			#pragma shader_feature_local _ENABLEPIXELOUTLINE_ON
			#pragma shader_feature_local _ENABLEOUTEROUTLINE_ON
			#pragma shader_feature_local _ENABLEINNEROUTLINE_ON
			#pragma shader_feature_local _ENABLESATURATION_ON
			#pragma shader_feature_local _ENABLESINEGLOW_ON
			#pragma shader_feature_local _ENABLEADDHUE_ON
			#pragma shader_feature_local _ENABLESHIFTHUE_ON
			#pragma shader_feature_local _ENABLEINKSPREAD_ON
			#pragma shader_feature_local _ENABLEBLACKTINT_ON
			#pragma shader_feature_local _ENABLESPLITTONING_ON
			#pragma shader_feature_local _ENABLEHUE_ON
			#pragma shader_feature_local _ENABLEBRIGHTNESS_ON
			#pragma shader_feature_local _ENABLECONTRAST_ON
			#pragma shader_feature _ENABLENEGATIVE_ON
			#pragma shader_feature_local _ENABLECOLORREPLACE_ON
			#pragma shader_feature_local _ENABLERECOLORRGBYCP_ON
			#pragma shader_feature _ENABLERECOLORRGB_ON
			#pragma shader_feature_local _ENABLEFLAME_ON
			#pragma shader_feature_local _ENABLECHECKERBOARD_ON
			#pragma shader_feature_local _ENABLECUSTOMFADE_ON
			#pragma shader_feature_local _ENABLESMOKE_ON
			#pragma shader_feature _ENABLESHARPEN_ON
			#pragma shader_feature _ENABLEGAUSSIANBLUR_ON
			#pragma shader_feature _ENABLESMOOTHPIXELART_ON
			#pragma shader_feature_local _TILINGFIX_ON
			#pragma shader_feature _ENABLEWIGGLE_ON
			#pragma shader_feature_local _ENABLEUVSCALE_ON
			#pragma shader_feature_local _ENABLEPIXELATE_ON
			#pragma shader_feature_local _ENABLEUVSCROLL_ON
			#pragma shader_feature_local _ENABLEUVROTATE_ON
			#pragma shader_feature_local _ENABLESINEROTATE_ON
			#pragma shader_feature_local _ENABLESQUEEZE_ON
			#pragma shader_feature_local _ENABLEUVDISTORT_ON
			#pragma shader_feature_local _ENABLEWIND_ON
			#pragma shader_feature_local _WINDLOCALWIND_ON
			#pragma shader_feature_local _WINDHIGHQUALITYNOISE_ON
			#pragma shader_feature_local _WINDISPARALLAX_ON
			#pragma shader_feature _UVDISTORTMASKTOGGLE_ON
			#pragma shader_feature _WIGGLEFIXEDGROUNDTOGGLE_ON
			#pragma shader_feature _RECOLORRGBTEXTURETOGGLE_ON
			#pragma shader_feature _RECOLORRGBYCPTEXTURETOGGLE_ON
			#pragma shader_feature_local _ADDHUEMASKTOGGLE_ON
			#pragma shader_feature_local _SINEGLOWMASKTOGGLE_ON
			#pragma shader_feature _INNEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature_local _INNEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _INNEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _OUTEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature_local _OUTEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _PIXELOUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _CAMOUFLAGEANIMATIONTOGGLE_ON
			#pragma shader_feature _METALMASKTOGGLE_ON
			#pragma shader_feature _SHINEMASKTOGGLE_ON
			#pragma shader_feature _ENCHANTEDLERPTOGGLE_ON
			#pragma shader_feature _ENCHANTEDRAINBOWTOGGLE_ON
			#pragma shader_feature _SHIFTINGRAINBOWTOGGLE_ON
			#pragma shader_feature _ADDCOLORCONTRASTTOGGLE_ON
			#pragma shader_feature _ADDCOLORMASKTOGGLE_ON
			#pragma shader_feature _STRONGTINTCONTRASTTOGGLE_ON
			#pragma shader_feature _STRONGTINTMASKTOGGLE_ON
			#pragma shader_feature _EMISSIONTOGGLE_ON
			#pragma shader_feature _METALLICMAPTOGGLE_ON

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f {
				#if UNITY_VERSION >= 201810
					UNITY_POSITION(pos);
				#else
					float4 pos : SV_POSITION;
				#endif
				#if defined(LIGHTMAP_ON) || (!defined(LIGHTMAP_ON) && SHADER_TARGET >= 30)
					float4 lmap : TEXCOORD0;
				#endif
				#if !defined(LIGHTMAP_ON) && UNITY_SHOULD_SAMPLE_SH
					half3 sh : TEXCOORD1;
				#endif
				#if defined(UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS) && UNITY_VERSION >= 201810 && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_LIGHTING_COORDS(2,3)
				#elif defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if UNITY_VERSION >= 201710
						UNITY_SHADOW_COORDS(2)
					#else
						SHADOW_COORDS(2)
					#endif
				#endif
				#ifdef ASE_FOG
					UNITY_FOG_COORDS(4)
				#endif
				float4 tSpace0 : TEXCOORD5;
				float4 tSpace1 : TEXCOORD6;
				float4 tSpace2 : TEXCOORD7;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 screenPos : TEXCOORD8;
				#endif
				float4 ase_texcoord9 : TEXCOORD9;
				float4 ase_texcoord10 : TEXCOORD10;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishStretch;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingScale;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingOffset;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float _ScreenTilingPixelsPerUnit;
			#endif
			uniform sampler2D _MainTex;
			float4 _MainTex_TexelSize;
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingScale;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingOffset;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float _WorldTilingPixelsPerUnit;
			#endif
			uniform float4 _SpriteSheetRect;
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFade;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFlip;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishSquish;
			#endif
				#ifdef _TOGGLECUSTOMTIME_ON
			uniform float _TimeValue;
			#endif
			uniform float UnscaledTime;
				#ifdef _TOGGLETIMESPEED_ON
			uniform float _TimeSpeed;
			#endif
				#ifdef _TOGGLETIMEFPS_ON
			uniform float _TimeFPS;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeFrequency;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeRange;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveFrequency;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveOffset;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float _SineMoveFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFrequency;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateOffset;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateRotation;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float _SineScaleFrequency;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float2 _SineScaleFactor;
			#endif
			uniform float _FadingFade;
			uniform sampler2D _FadingMask;
			uniform float4 _FadingMask_ST;
			uniform float _FadingWidth;
			uniform sampler2D _UberNoiseTexture;
			uniform float _PixelsPerUnit;
			uniform float _RectWidth;
			uniform float _RectHeight;
			uniform float _ScreenWidthUnits;
			uniform float2 _FadingNoiseScale;
			uniform float2 _FadingPosition;
			uniform float _FadingNoiseFactor;
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotationWindFactor;
			#endif
			uniform float WindMinIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMinIntensity;
			#endif
			uniform float WindMaxIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMaxIntensity;
			#endif
				#ifdef _WINDISPARALLAX_ON
			uniform float _WindXPosition;
			#endif
			uniform float WindNoiseScale;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseScale;
			#endif
			uniform float WindTime;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseSpeed;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindMaxRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindFlip;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishFactor;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishWindFactor;
			#endif
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float _FullDistortionFade;
			#endif
			uniform float2 _FullDistortionNoiseScale;
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float2 _FullDistortionDistortion;
			#endif
			uniform float2 _DirectionalDistortionDistortionScale;
			uniform float _DirectionalDistortionRandomDirection;
			uniform float2 _DirectionalDistortionDistortion;
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
			uniform float _DirectionalDistortionInvert;
			#endif
			uniform float _DirectionalDistortionRotation;
			uniform float _DirectionalDistortionFade;
			uniform float2 _DirectionalDistortionNoiseScale;
			uniform float _DirectionalDistortionNoiseFactor;
			uniform float _DirectionalDistortionWidth;
			uniform float _HologramDistortionSpeed;
			uniform float _HologramDistortionDensity;
			uniform float _HologramDistortionScale;
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramDistortionOffset;
			#endif
			uniform float _HologramFade;
			uniform float2 _GlitchDistortionSpeed;
			uniform float2 _GlitchDistortionScale;
			uniform float2 _GlitchDistortion;
			uniform float2 _GlitchMaskSpeed;
			uniform float2 _GlitchMaskScale;
			uniform float _GlitchMaskMin;
			uniform float _GlitchFade;
			uniform float2 _UVDistortFrom;
			uniform float2 _UVDistortTo;
			uniform float2 _UVDistortSpeed;
			uniform float2 _UVDistortNoiseScale;
			uniform float _UVDistortFade;
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform sampler2D _UVDistortMask;
			#endif
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform float4 _UVDistortMask_ST;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeCenter;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezePower;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeScale;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezeFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFrequency;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateAngle;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float2 _SineRotatePivot;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float _UVRotateSpeed;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float2 _UVRotatePivot;
			#endif
				#ifdef _ENABLEUVSCROLL_ON
			uniform float2 _UVScrollSpeed;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelDensity;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelsPerUnit;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelateFade;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScalePivot;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScaleScale;
			#endif
			uniform float _WiggleFrequency;
			uniform float _WiggleSpeed;
			uniform float _WiggleOffset;
			uniform float _WiggleFade;
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurOffset;
			#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurFade;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenOffset;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFactor;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFade;
			#endif
			uniform float _SmokeVertexSeed;
			uniform float _SmokeNoiseScale;
			uniform float _SmokeNoiseFactor;
			uniform float _SmokeSmoothness;
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeDarkEdge;
			#endif
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeAlpha;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform sampler2D _CustomFadeFadeMask;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float2 _CustomFadeNoiseScale;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeNoiseFactor;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeSmoothness;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeAlpha;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardDarken;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardTiling;
			#endif
			uniform float2 _FlameSpeed;
			uniform float2 _FlameNoiseScale;
			uniform float _FlameNoiseHeightFactor;
			uniform float _FlameNoiseFactor;
			uniform float _FlameRadius;
			uniform float _FlameSmooth;
				#ifdef _ENABLEFLAME_ON
			uniform float _FlameBrightness;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBRedTint;
			#endif
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBTexture;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBGreenTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBBlueTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float _RecolorRGBFade;
			#endif
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBYCPTexture;
			#endif
			uniform float4 _RecolorRGBYCPPurpleTint;
			uniform float4 _RecolorRGBYCPBlueTint;
			uniform float4 _RecolorRGBYCPCyanTint;
			uniform float4 _RecolorRGBYCPGreenTint;
			uniform float4 _RecolorRGBYCPYellowTint;
			uniform float4 _RecolorRGBYCPRedTint;
				#ifdef _ENABLERECOLORRGBYCP_ON
			uniform float _RecolorRGBYCPFade;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceFromColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceContrast;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceToColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceSmoothness;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceRange;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceFade;
			#endif
				#ifdef _ENABLENEGATIVE_ON
			uniform float _NegativeFade;
			#endif
				#ifdef _ENABLECONTRAST_ON
			uniform float _Contrast;
			#endif
				#ifdef _ENABLEBRIGHTNESS_ON
			uniform float _Brightness;
			#endif
				#ifdef _ENABLEHUE_ON
			uniform float _Hue;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningShadowsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningHighlightsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningShift;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningBalance;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningContrast;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningFade;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float4 _BlackTintColor;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintPower;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float4 _InkSpreadColor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadContrast;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadDistance;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadPosition;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadNoiseScale;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadNoiseFactor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadWidth;
			#endif
				#ifdef _ENABLESHIFTHUE_ON
			uniform float _ShiftHueSpeed;
			#endif
			uniform float _AddHueSpeed;
			uniform float _AddHueSaturation;
			uniform float _AddHueBrightness;
				#ifdef _ENABLEADDHUE_ON
			uniform float _AddHueContrast;
			#endif
			uniform float _AddHueFade;
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform sampler2D _AddHueMask;
			#endif
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform float4 _AddHueMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowContrast;
			#endif
			uniform float4 _SineGlowColor;
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform sampler2D _SineGlowMask;
			#endif
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform float4 _SineGlowMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFade;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFrequency;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMax;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMin;
			#endif
				#ifdef _ENABLESATURATION_ON
			uniform float _Saturation;
			#endif
			uniform float4 _InnerOutlineColor;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _InnerOutlineTintTexture;
			#endif
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform float2 _InnerOutlineTextureSpeed;
			#endif
			uniform float _InnerOutlineFade;
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseSpeed;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseScale;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineDistortionIntensity;
			#endif
			uniform float _InnerOutlineWidth;
			uniform float4 _OuterOutlineColor;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _OuterOutlineTintTexture;
			#endif
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform float2 _OuterOutlineTextureSpeed;
			#endif
			uniform float _OuterOutlineFade;
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseSpeed;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseScale;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineDistortionIntensity;
			#endif
			uniform float _OuterOutlineWidth;
			uniform float4 _PixelOutlineColor;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform sampler2D _PixelOutlineTintTexture;
			#endif
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform float2 _PixelOutlineTextureSpeed;
			#endif
			uniform float _PixelOutlineFade;
			uniform float _PixelOutlineWidth;
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowFrom;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowTo;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFrequency;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFade;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float4 _HologramTint;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineSpeed;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineFrequency;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineGap;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramMinAlpha;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchBrightness;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseSpeed;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseScale;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchHueSpeed;
			#endif
			uniform float4 _CamouflageBaseColor;
			uniform float4 _CamouflageColorA;
			uniform float _CamouflageDensityA;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionSpeed;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionScale;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionIntensity;
			#endif
			uniform float2 _CamouflageNoiseScaleA;
			uniform float _CamouflageSmoothnessA;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float4 _CamouflageColorB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageDensityB;
			#endif
			uniform float2 _CamouflageNoiseScaleB;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageSmoothnessB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageContrast;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageFade;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalHighlightDensity;
			#endif
			uniform float2 _MetalNoiseDistortionSpeed;
			uniform float2 _MetalNoiseDistortionScale;
			uniform float2 _MetalNoiseDistortion;
			uniform float2 _MetalNoiseSpeed;
			uniform float2 _MetalNoiseScale;
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalHighlightColor;
			#endif
			uniform float _MetalHighlightContrast;
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalContrast;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalColor;
			#endif
			uniform float _MetalFade;
				#ifdef _METALMASKTOGGLE_ON
			uniform sampler2D _MetalMask;
			#endif
				#ifdef _METALMASKTOGGLE_ON
			uniform float4 _MetalMask_ST;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenTint;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenSnowColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenSnowScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortion;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenHighlightColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenFade;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideContrast;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideNoiseColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideNoiseFactor;
			#endif
			uniform float2 _BurnSwirlNoiseScale;
			uniform float _BurnSwirlFactor;
			uniform float2 _BurnInsideNoiseScale;
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnRadius;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnPosition;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnEdgeNoiseScale;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnEdgeNoiseFactor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnWidth;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnEdgeColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnFade;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowCenter;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowNoiseScale;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowNoiseFactor;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowDensity;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSpeed;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSaturation;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowBrightness;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowContrast;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowFade;
			#endif
			uniform float _ShineSaturation;
			uniform float _ShineContrast;
				#ifdef _ENABLESHINE_ON
			uniform float4 _ShineColor;
			#endif
			uniform float _ShineRotation;
			uniform float _ShineFrequency;
			uniform float _ShineSpeed;
			uniform float _ShineWidth;
			uniform float _ShineFade;
				#ifdef _SHINEMASKTOGGLE_ON
			uniform sampler2D _ShineMask;
			#endif
				#ifdef _SHINEMASKTOGGLE_ON
			uniform float4 _ShineMask_ST;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseScale;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonShiftSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonDensity;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float4 _PoisonColor;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonFade;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonNoiseBrightness;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonRecolorFactor;
			#endif
			uniform float4 _EnchantedLowColor;
			uniform float4 _EnchantedHighColor;
			uniform float2 _EnchantedSpeed;
			uniform float2 _EnchantedScale;
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowDensity;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSpeed;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSaturation;
			#endif
			uniform float _EnchantedContrast;
			uniform float _EnchantedBrightness;
			uniform float _EnchantedReduce;
			uniform float _EnchantedFade;
			uniform float4 _ShiftingColorA;
			uniform float4 _ShiftingColorB;
			uniform float _ShiftingSpeed;
			uniform float _ShiftingDensity;
			uniform float _ShiftingBrightness;
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
			uniform float _ShiftingSaturation;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingContrast;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingFade;
			#endif
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float _FullAlphaDissolveFade;
			#endif
			uniform float _FullAlphaDissolveWidth;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float2 _FullAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float4 _FullGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float2 _FullGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveFade;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveInvert;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveFade;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveFade;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float4 _SourceGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float2 _DirectionalAlphaFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeWidth;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float4 _DirectionalGlowFadeEdgeColor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float2 _DirectionalGlowFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeWidth;
			#endif
				#ifdef _ENABLEHALFTONE_ON
			uniform float _HalftoneInvert;
			#endif
			uniform float _HalftoneTiling;
			uniform float _HalftoneFade;
			uniform float2 _HalftonePosition;
			uniform float _HalftoneFadeWidth;
			uniform float4 _AddColorColor;
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform sampler2D _AddColorMask;
			#endif
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform float4 _AddColorMask_ST;
			#endif
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
			uniform float _AddColorContrast;
			#endif
				#ifdef _ENABLEADDCOLOR_ON
			uniform float _AddColorFade;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float4 _AlphaTintColor;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintMinAlpha;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintFade;
			#endif
			uniform float4 _StrongTintTint;
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform sampler2D _StrongTintMask;
			#endif
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform float4 _StrongTintMask_ST;
			#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
			uniform float _StrongTintContrast;
			#endif
				#ifdef _ENABLESTRONGTINT_ON
			uniform float _StrongTintFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float4 _ShadowColor;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float _ShadowFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float2 _ShadowOffset;
			#endif
			uniform sampler2D _NormalMap;
			uniform float _NormalIntensity;
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionTint;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform sampler2D _EmissionMap;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionMap_ST;
			#endif
			uniform float _Metallic;
			uniform sampler2D _MetallicMap;
			uniform float _Smoothness;
			uniform float _AlphaClip;

	
			float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
			{
				original -= center;
				float C = cos( angle );
				float S = sin( angle );
				float t = 1 - C;
				float m00 = t * u.x * u.x + C;
				float m01 = t * u.x * u.y - S * u.z;
				float m02 = t * u.x * u.z + S * u.y;
				float m10 = t * u.x * u.y + S * u.z;
				float m11 = t * u.y * u.y + C;
				float m12 = t * u.y * u.z - S * u.x;
				float m20 = t * u.x * u.z - S * u.y;
				float m21 = t * u.y * u.z + S * u.x;
				float m22 = t * u.z * u.z + C;
				float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
				return mul( finalMatrix, original ) + center;
			}
			
			float MyCustomExpression16_g11717( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11719( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float FastNoise101_g11668( float x )
			{
				float i = floor(x);
				float f = frac(x);
				float s = sign(frac(x/2.0)-0.5);
				    
				float k = 0.5+0.5*sin(i);
				return s*f*(f-1.0)*((16.0*k-4.0)*f*(f-1.0)-1.0);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			
			float MyCustomExpression16_g11670( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11671( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11674( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11673( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11679( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11680( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11721( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11676( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11732( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float4 texturePointSmooth( sampler2D tex, float4 textureTexelSize, float2 uvs )
			{
				float2 size;
				size.x = textureTexelSize.z;
				size.y = textureTexelSize.w;
				float2 pixel = float2(1.0,1.0) / size;
				uvs -= pixel * float2(0.5,0.5);
				float2 uv_pixels = uvs * size;
				float2 delta_pixel = frac(uv_pixels) - float2(0.5,0.5);
				float2 ddxy = fwidth(uv_pixels);
				float2 mip = log2(ddxy) - 0.5;
				float2 clampedUV = uvs + (clamp(delta_pixel / ddxy, 0.0, 1.0) - delta_pixel) * pixel;
				return tex2Dlod(tex, float4(clampedUV,0, min(mip.x, mip.y)));
			}
			
			float MyCustomExpression16_g11758( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11760( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11764( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 MyCustomExpression115_g11768( float3 In, float3 From, float3 To, float Fuzziness, float Range )
			{
				float Distance = distance(From, In);
				return lerp(To, In, saturate((Distance - Range) / max(Fuzziness, 0.001)));
			}
			
			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float MyCustomExpression16_g11787( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11774( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11798( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11805( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11838( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11835( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11837( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11828( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11830( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11823( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11825( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11826( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11819( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11817( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11818( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11813( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11841( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11845( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11843( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11852( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11860( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11862( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11858( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11854( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11856( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			

			v2f VertexFunction (appdata v  ) {
				UNITY_SETUP_INSTANCE_ID(v);
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 _ZeroVector = float2(0,0);
				float2 texCoord363 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLESCREENTILING_ON
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLEWORLDTILING_ON
				float2 staticSwitch2_g11657 = ( ( ( (ase_worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _PIXELPERFECTUV_ON
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				#ifdef _SPRITESHEETFIX_ON
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				#ifdef _ENABLESQUISH_ON
				float2 break77_g11879 = fixedUV475;
				float2 appendResult72_g11879 = (float2(( _SquishStretch * ( break77_g11879.x - 0.5 ) * _SquishFade ) , ( _SquishFade * ( break77_g11879.y + _SquishFlip ) * -_SquishSquish )));
				float2 staticSwitch198 = ( appendResult72_g11879 + _ZeroVector );
				#else
				float2 staticSwitch198 = _ZeroVector;
				#endif
				float2 temp_output_2_0_g11880 = staticSwitch198;
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				float temp_output_8_0_g11880 = shaderTime237;
				#ifdef _ENABLESINEMOVE_ON
				float2 staticSwitch4_g11880 = ( ( sin( ( temp_output_8_0_g11880 * _SineMoveFrequency ) ) * _SineMoveOffset * _SineMoveFade ) + temp_output_2_0_g11880 );
				#else
				float2 staticSwitch4_g11880 = temp_output_2_0_g11880;
				#endif
				#ifdef _ENABLEVIBRATE_ON
				float temp_output_30_0_g11881 = temp_output_8_0_g11880;
				float3 rotatedValue21_g11881 = RotateAroundAxis( float3( 0,0,0 ), float3( 0,1,0 ), float3( 0,0,1 ), ( temp_output_30_0_g11881 * _VibrateRotation ) );
				float2 staticSwitch6_g11880 = ( ( sin( ( _VibrateFrequency * temp_output_30_0_g11881 ) ) * _VibrateOffset * _VibrateFade * (rotatedValue21_g11881).xy ) + staticSwitch4_g11880 );
				#else
				float2 staticSwitch6_g11880 = staticSwitch4_g11880;
				#endif
				#ifdef _ENABLESINESCALE_ON
				float2 staticSwitch10_g11880 = ( staticSwitch6_g11880 + ( (v.vertex.xyz).xy * ( ( ( sin( ( _SineScaleFrequency * temp_output_8_0_g11880 ) ) + 1.0 ) * 0.5 ) * _SineScaleFactor ) ) );
				#else
				float2 staticSwitch10_g11880 = staticSwitch6_g11880;
				#endif
				float2 temp_output_424_0 = staticSwitch10_g11880;
				float2 uv_FadingMask = v.ase_texcoord.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2Dlod( _FadingMask, float4( uv_FadingMask, 0, 0.0) );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float2 texCoord435 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				#ifdef _PIXELPERFECTSPACE_ON
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (v.vertex.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (v.vertex.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (ase_worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float linValue16_g11717 = tex2Dlod( _UberNoiseTexture, float4( ( shaderPosition235 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2Dlod( _UberNoiseTexture, float4( ( temp_output_27_0_g11718 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult121 = lerp( float2( 0,0 ) , temp_output_424_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch142 = temp_output_424_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch142 = lerpResult121;
				#else
				float2 staticSwitch142 = temp_output_424_0;
				#endif
				
				o.ase_texcoord9.xy = v.ase_texcoord.xy;
				o.ase_texcoord10 = v.vertex;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord9.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = float3( staticSwitch142 ,  0.0 );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.vertex.w = 1;
				v.normal = v.normal;
				v.tangent = v.tangent;

				o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
				o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				#ifdef DYNAMICLIGHTMAP_ON
				o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#endif
				#ifdef LIGHTMAP_ON
				o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				#ifndef LIGHTMAP_ON
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						o.sh = 0;
						#ifdef VERTEXLIGHT_ON
						o.sh += Shade4PointLights (
							unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
							unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
							unity_4LightAtten0, worldPos, worldNormal);
						#endif
						o.sh = ShadeSHPerVertex (worldNormal, o.sh);
					#endif
				#endif

				#if UNITY_VERSION >= 201810 && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_TRANSFER_LIGHTING(o, v.texcoord1.xy);
				#elif defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if UNITY_VERSION >= 201710
						UNITY_TRANSFER_SHADOW(o, v.texcoord1.xy);
					#else
						TRANSFER_SHADOW(o);
					#endif
				#endif

				#ifdef ASE_FOG
					UNITY_TRANSFER_FOG(o,o.pos);
				#endif
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					o.screenPos = ComputeScreenPos(o.pos);
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( appdata v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.tangent = v.tangent;
				o.normal = v.normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				appdata o = (appdata) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
				o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			v2f vert ( appdata v )
			{
				return VertexFunction( v );
			}
			#endif
			
			fixed4 frag (v2f IN 
				#ifdef _DEPTHOFFSET_ON
				, out float outputDepth : SV_Depth
				#endif
				) : SV_Target 
			{
				UNITY_SETUP_INSTANCE_ID(IN);

				#ifdef LOD_FADE_CROSSFADE
					UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
				#endif

				#if defined(_SPECULAR_SETUP)
					SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
				#else
					SurfaceOutputStandard o = (SurfaceOutputStandard)0;
				#endif
				float3 WorldTangent = float3(IN.tSpace0.x,IN.tSpace1.x,IN.tSpace2.x);
				float3 WorldBiTangent = float3(IN.tSpace0.y,IN.tSpace1.y,IN.tSpace2.y);
				float3 WorldNormal = float3(IN.tSpace0.z,IN.tSpace1.z,IN.tSpace2.z);
				float3 worldPos = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
				#else
					half atten = 1;
				#endif
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 ScreenPos = IN.screenPos;
				#endif

				float2 texCoord363 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				float4 ase_screenPosNorm = ScreenPos / ScreenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				#ifdef _ENABLESCREENTILING_ON
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				#ifdef _ENABLEWORLDTILING_ON
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11657 = ( ( ( (worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				#ifdef _SPRITESHEETFIX_ON
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				float2 temp_output_3_0_g11667 = fixedUV475;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch117_g11668 = _WindMinIntensity;
				#else
				float staticSwitch117_g11668 = WindMinIntensity;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch118_g11668 = _WindMaxIntensity;
				#else
				float staticSwitch118_g11668 = WindMaxIntensity;
				#endif
				float4 transform62_g11668 = mul(unity_WorldToObject,float4( 0,0,0,1 ));
				#ifdef _WINDISPARALLAX_ON
				float staticSwitch111_g11668 = _WindXPosition;
				#else
				float staticSwitch111_g11668 = transform62_g11668.x;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch113_g11668 = _WindNoiseScale;
				#else
				float staticSwitch113_g11668 = WindNoiseScale;
				#endif
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch125_g11668 = ( shaderTime237 * _WindNoiseSpeed );
				#else
				float staticSwitch125_g11668 = WindTime;
				#endif
				float temp_output_50_0_g11668 = ( ( staticSwitch111_g11668 * staticSwitch113_g11668 ) + staticSwitch125_g11668 );
				float x101_g11668 = temp_output_50_0_g11668;
				float localFastNoise101_g11668 = FastNoise101_g11668( x101_g11668 );
				float2 temp_cast_0 = (temp_output_50_0_g11668).xx;
				float simplePerlin2D121_g11668 = snoise( temp_cast_0*0.5 );
				simplePerlin2D121_g11668 = simplePerlin2D121_g11668*0.5 + 0.5;
				#ifdef _WINDHIGHQUALITYNOISE_ON
				float staticSwitch123_g11668 = simplePerlin2D121_g11668;
				#else
				float staticSwitch123_g11668 = ( localFastNoise101_g11668 + 0.5 );
				#endif
				#ifdef _ENABLEWIND_ON
				float lerpResult86_g11668 = lerp( staticSwitch117_g11668 , staticSwitch118_g11668 , staticSwitch123_g11668);
				float clampResult29_g11668 = clamp( ( ( _WindRotationWindFactor * lerpResult86_g11668 ) + _WindRotation ) , -_WindMaxRotation , _WindMaxRotation );
				float2 temp_output_1_0_g11668 = temp_output_3_0_g11667;
				float temp_output_39_0_g11668 = ( temp_output_1_0_g11668.y + _WindFlip );
				float3 appendResult43_g11668 = (float3(0.5 , -_WindFlip , 0.0));
				float2 appendResult27_g11668 = (float2(0.0 , ( _WindSquishFactor * min( ( ( _WindSquishWindFactor * abs( lerpResult86_g11668 ) ) + abs( _WindRotation ) ) , _WindMaxRotation ) * temp_output_39_0_g11668 )));
				float3 rotatedValue19_g11668 = RotateAroundAxis( appendResult43_g11668, float3( ( appendResult27_g11668 + temp_output_1_0_g11668 ) ,  0.0 ), float3( 0,0,1 ), ( clampResult29_g11668 * temp_output_39_0_g11668 ) );
				float2 staticSwitch4_g11667 = (rotatedValue19_g11668).xy;
				#else
				float2 staticSwitch4_g11667 = temp_output_3_0_g11667;
				#endif
				float2 texCoord435 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _PIXELPERFECTSPACE_ON
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (IN.ase_texcoord10.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (IN.ase_texcoord10.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float2 temp_output_195_0_g11669 = shaderPosition235;
				float linValue16_g11670 = tex2D( _UberNoiseTexture, ( temp_output_195_0_g11669 * _FullDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11670 = MyCustomExpression16_g11670( linValue16_g11670 );
				float linValue16_g11671 = tex2D( _UberNoiseTexture, ( ( temp_output_195_0_g11669 + float2( 0.321,0.321 ) ) * _FullDistortionNoiseScale ) ).r;
				#ifdef _ENABLEFULLDISTORTION_ON
				float localMyCustomExpression16_g11671 = MyCustomExpression16_g11671( linValue16_g11671 );
				float2 appendResult189_g11669 = (float2(( localMyCustomExpression16_g11670 - 0.5 ) , ( localMyCustomExpression16_g11671 - 0.5 )));
				float2 staticSwitch83 = ( staticSwitch4_g11667 + ( ( 1.0 - _FullDistortionFade ) * appendResult189_g11669 * _FullDistortionDistortion ) );
				#else
				float2 staticSwitch83 = staticSwitch4_g11667;
				#endif
				float2 temp_output_182_0_g11672 = shaderPosition235;
				float linValue16_g11674 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionDistortionScale ) ).r;
				float localMyCustomExpression16_g11674 = MyCustomExpression16_g11674( linValue16_g11674 );
				float3 rotatedValue168_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( _DirectionalDistortionDistortion ,  0.0 ), float3( 0,0,1 ), ( ( ( localMyCustomExpression16_g11674 - 0.5 ) * 2.0 * _DirectionalDistortionRandomDirection ) * UNITY_PI ) );
				float3 rotatedValue136_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_182_0_g11672 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalDistortionRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11672 = rotatedValue136_g11672;
				float linValue16_g11673 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11673 = MyCustomExpression16_g11673( linValue16_g11673 );
				float clampResult154_g11672 = clamp( ( ( break130_g11672.x + break130_g11672.y + _DirectionalDistortionFade + ( localMyCustomExpression16_g11673 * _DirectionalDistortionNoiseFactor ) ) / max( _DirectionalDistortionWidth , 0.001 ) ) , 0.0 , 1.0 );
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float2 staticSwitch82 = ( staticSwitch83 + ( (rotatedValue168_g11672).xy * ( 1.0 - (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 )) ) ) );
				#else
				float2 staticSwitch82 = staticSwitch83;
				#endif
				float temp_output_8_0_g11677 = ( ( ( shaderTime237 * _HologramDistortionSpeed ) + worldPos.y ) / unity_OrthoParams.y );
				float2 temp_cast_4 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_5 = (_HologramDistortionDensity).xx;
				float linValue16_g11679 = tex2D( _UberNoiseTexture, ( temp_cast_4 * temp_cast_5 ) ).r;
				float localMyCustomExpression16_g11679 = MyCustomExpression16_g11679( linValue16_g11679 );
				float clampResult75_g11677 = clamp( localMyCustomExpression16_g11679 , 0.075 , 0.6 );
				float2 temp_cast_6 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_7 = (_HologramDistortionScale).xx;
				float linValue16_g11680 = tex2D( _UberNoiseTexture, ( temp_cast_6 * temp_cast_7 ) ).r;
				float localMyCustomExpression16_g11680 = MyCustomExpression16_g11680( linValue16_g11680 );
				float2 appendResult2_g11678 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float hologramFade182 = _HologramFade;
				#ifdef _ENABLEHOLOGRAM_ON
				float2 appendResult44_g11677 = (float2(( ( ( clampResult75_g11677 * ( localMyCustomExpression16_g11680 - 0.5 ) ) * _HologramDistortionOffset * ( 100.0 / appendResult2_g11678 ).x ) * hologramFade182 ) , 0.0));
				float2 staticSwitch59 = ( staticSwitch82 + appendResult44_g11677 );
				#else
				float2 staticSwitch59 = staticSwitch82;
				#endif
				float2 temp_output_18_0_g11675 = shaderPosition235;
				float2 glitchPosition154 = temp_output_18_0_g11675;
				float linValue16_g11721 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchDistortionSpeed * shaderTime237 ) ) * _GlitchDistortionScale ) ).r;
				float localMyCustomExpression16_g11721 = MyCustomExpression16_g11721( linValue16_g11721 );
				float linValue16_g11676 = tex2D( _UberNoiseTexture, ( ( temp_output_18_0_g11675 + ( _GlitchMaskSpeed * shaderTime237 ) ) * _GlitchMaskScale ) ).r;
				float localMyCustomExpression16_g11676 = MyCustomExpression16_g11676( linValue16_g11676 );
				float glitchFade152 = ( max( localMyCustomExpression16_g11676 , _GlitchMaskMin ) * _GlitchFade );
				#ifdef _ENABLEGLITCH_ON
				float2 staticSwitch62 = ( staticSwitch59 + ( ( localMyCustomExpression16_g11721 - 0.5 ) * _GlitchDistortion * glitchFade152 ) );
				#else
				float2 staticSwitch62 = staticSwitch59;
				#endif
				float2 temp_output_1_0_g11722 = staticSwitch62;
				float2 temp_output_26_0_g11722 = shaderPosition235;
				float temp_output_25_0_g11722 = shaderTime237;
				float linValue16_g11732 = tex2D( _UberNoiseTexture, ( ( temp_output_26_0_g11722 + ( _UVDistortSpeed * temp_output_25_0_g11722 ) ) * _UVDistortNoiseScale ) ).r;
				float localMyCustomExpression16_g11732 = MyCustomExpression16_g11732( linValue16_g11732 );
				float2 lerpResult21_g11729 = lerp( _UVDistortFrom , _UVDistortTo , localMyCustomExpression16_g11732);
				float2 appendResult2_g11731 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _UVDISTORTMASKTOGGLE_ON
				float2 uv_UVDistortMask = IN.ase_texcoord9.xy * _UVDistortMask_ST.xy + _UVDistortMask_ST.zw;
				float4 tex2DNode3_g11730 = tex2D( _UVDistortMask, uv_UVDistortMask );
				float staticSwitch29_g11729 = ( _UVDistortFade * ( tex2DNode3_g11730.r * tex2DNode3_g11730.a ) );
				#else
				float staticSwitch29_g11729 = _UVDistortFade;
				#endif
				#ifdef _ENABLEUVDISTORT_ON
				float2 staticSwitch5_g11722 = ( temp_output_1_0_g11722 + ( lerpResult21_g11729 * ( 100.0 / appendResult2_g11731 ) * staticSwitch29_g11729 ) );
				#else
				float2 staticSwitch5_g11722 = temp_output_1_0_g11722;
				#endif
				#ifdef _ENABLESQUEEZE_ON
				float2 temp_output_1_0_g11728 = staticSwitch5_g11722;
				float2 staticSwitch7_g11722 = ( temp_output_1_0_g11728 + ( ( temp_output_1_0_g11728 - _SqueezeCenter ) * pow( distance( temp_output_1_0_g11728 , _SqueezeCenter ) , _SqueezePower ) * _SqueezeScale * _SqueezeFade ) );
				#else
				float2 staticSwitch7_g11722 = staticSwitch5_g11722;
				#endif
				#ifdef _ENABLESINEROTATE_ON
				float3 rotatedValue36_g11727 = RotateAroundAxis( float3( _SineRotatePivot ,  0.0 ), float3( staticSwitch7_g11722 ,  0.0 ), float3( 0,0,1 ), ( sin( ( temp_output_25_0_g11722 * _SineRotateFrequency ) ) * ( ( _SineRotateAngle / 360.0 ) * UNITY_PI ) * _SineRotateFade ) );
				float2 staticSwitch9_g11722 = (rotatedValue36_g11727).xy;
				#else
				float2 staticSwitch9_g11722 = staticSwitch7_g11722;
				#endif
				#ifdef _ENABLEUVROTATE_ON
				float3 rotatedValue8_g11726 = RotateAroundAxis( float3( _UVRotatePivot ,  0.0 ), float3( staticSwitch9_g11722 ,  0.0 ), float3( 0,0,1 ), ( temp_output_25_0_g11722 * _UVRotateSpeed * UNITY_PI ) );
				float2 staticSwitch16_g11722 = (rotatedValue8_g11726).xy;
				#else
				float2 staticSwitch16_g11722 = staticSwitch9_g11722;
				#endif
				#ifdef _ENABLEUVSCROLL_ON
				float2 staticSwitch14_g11722 = ( ( _UVScrollSpeed * temp_output_25_0_g11722 ) + staticSwitch16_g11722 );
				#else
				float2 staticSwitch14_g11722 = staticSwitch16_g11722;
				#endif
				#ifdef _ENABLEPIXELATE_ON
				float2 appendResult35_g11724 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 MultFactor30_g11724 = ( ( _PixelatePixelDensity * ( appendResult35_g11724 / _PixelatePixelsPerUnit ) ) * ( 1.0 / max( _PixelateFade , 1E-05 ) ) );
				float2 clampResult46_g11724 = clamp( ( floor( ( MultFactor30_g11724 * ( staticSwitch14_g11722 + ( float2( 0.5,0.5 ) / MultFactor30_g11724 ) ) ) ) / MultFactor30_g11724 ) , float2( 0,0 ) , float2( 1,1 ) );
				float2 staticSwitch4_g11722 = clampResult46_g11724;
				#else
				float2 staticSwitch4_g11722 = staticSwitch14_g11722;
				#endif
				#ifdef _ENABLEUVSCALE_ON
				float2 staticSwitch24_g11722 = ( ( ( staticSwitch4_g11722 - _UVScalePivot ) / _UVScaleScale ) + _UVScalePivot );
				#else
				float2 staticSwitch24_g11722 = staticSwitch4_g11722;
				#endif
				float2 temp_output_1_0_g11733 = staticSwitch24_g11722;
				float temp_output_7_0_g11733 = ( sin( ( _WiggleFrequency * ( temp_output_26_0_g11722.y + ( _WiggleSpeed * temp_output_25_0_g11722 ) ) ) ) * _WiggleOffset * _WiggleFade );
				#ifdef _WIGGLEFIXEDGROUNDTOGGLE_ON
				float staticSwitch18_g11733 = ( temp_output_7_0_g11733 * temp_output_1_0_g11733.y );
				#else
				float staticSwitch18_g11733 = temp_output_7_0_g11733;
				#endif
				#ifdef _ENABLEWIGGLE_ON
				float2 appendResult12_g11733 = (float2(staticSwitch18_g11733 , 0.0));
				float2 staticSwitch13_g11733 = ( temp_output_1_0_g11733 + appendResult12_g11733 );
				#else
				float2 staticSwitch13_g11733 = temp_output_1_0_g11733;
				#endif
				float2 temp_output_484_0 = staticSwitch13_g11733;
				float2 texCoord131 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_FadingMask = IN.ase_texcoord9.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2D( _FadingMask, uv_FadingMask );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float linValue16_g11717 = tex2D( _UberNoiseTexture, ( shaderPosition235 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2D( _UberNoiseTexture, ( temp_output_27_0_g11718 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult130 = lerp( texCoord131 , temp_output_484_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch145 = temp_output_484_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch145 = lerpResult130;
				#else
				float2 staticSwitch145 = temp_output_484_0;
				#endif
				#ifdef _TILINGFIX_ON
				float2 staticSwitch485 = ( ( ( staticSwitch145 % float2( 1,1 ) ) + float2( 1,1 ) ) % float2( 1,1 ) );
				#else
				float2 staticSwitch485 = staticSwitch145;
				#endif
				#ifdef _SPRITESHEETFIX_ON
				float2 break14_g11734 = staticSwitch485;
				float2 break11_g11734 = float2( 0,0 );
				float2 break10_g11734 = float2( 1,1 );
				float2 break9_g11734 = spriteRectMin376;
				float2 break8_g11734 = spriteRectMax377;
				float2 appendResult15_g11734 = (float2((break9_g11734.x + (break14_g11734.x - break11_g11734.x) * (break8_g11734.x - break9_g11734.x) / (break10_g11734.x - break11_g11734.x)) , (break9_g11734.y + (break14_g11734.y - break11_g11734.y) * (break8_g11734.y - break9_g11734.y) / (break10_g11734.y - break11_g11734.y))));
				float2 staticSwitch371 = min( max( appendResult15_g11734 , spriteRectMin376 ) , spriteRectMax377 );
				#else
				float2 staticSwitch371 = staticSwitch485;
				#endif
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11735 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch427 = ( originalUV460 + ( floor( ( ( staticSwitch371 - uvAfterPixelArt450 ) * appendResult7_g11735 ) ) / appendResult7_g11735 ) );
				#else
				float2 staticSwitch427 = staticSwitch371;
				#endif
				float2 finalUV146 = staticSwitch427;
				float2 temp_output_1_0_g11736 = finalUV146;
				#ifdef _ENABLESMOOTHPIXELART_ON
				sampler2D tex3_g11737 = _MainTex;
				float4 textureTexelSize3_g11737 = _MainTex_TexelSize;
				float2 uvs3_g11737 = temp_output_1_0_g11736;
				float4 localtexturePointSmooth3_g11737 = texturePointSmooth( tex3_g11737 , textureTexelSize3_g11737 , uvs3_g11737 );
				float4 staticSwitch8_g11736 = localtexturePointSmooth3_g11737;
				#else
				float4 staticSwitch8_g11736 = tex2D( _MainTex, temp_output_1_0_g11736 );
				#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
				float temp_output_10_0_g11738 = ( _GaussianBlurOffset * _GaussianBlurFade * 0.005 );
				float temp_output_2_0_g11748 = temp_output_10_0_g11738;
				float2 appendResult16_g11748 = (float2(temp_output_2_0_g11748 , 0.0));
				float2 appendResult25_g11750 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11750 = ( appendResult16_g11748 * appendResult25_g11750 );
				float2 temp_output_7_0_g11738 = temp_output_1_0_g11736;
				float2 temp_output_1_0_g11748 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,1 ) ) );
				float2 temp_output_1_0_g11750 = temp_output_1_0_g11748;
				float2 appendResult17_g11748 = (float2(0.0 , temp_output_2_0_g11748));
				float2 appendResult25_g11749 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11749 = ( appendResult17_g11748 * appendResult25_g11749 );
				float2 temp_output_1_0_g11749 = temp_output_1_0_g11748;
				float temp_output_2_0_g11739 = temp_output_10_0_g11738;
				float2 appendResult16_g11739 = (float2(temp_output_2_0_g11739 , 0.0));
				float2 appendResult25_g11741 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11741 = ( appendResult16_g11739 * appendResult25_g11741 );
				float2 temp_output_1_0_g11739 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,1 ) ) );
				float2 temp_output_1_0_g11741 = temp_output_1_0_g11739;
				float2 appendResult17_g11739 = (float2(0.0 , temp_output_2_0_g11739));
				float2 appendResult25_g11740 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11740 = ( appendResult17_g11739 * appendResult25_g11740 );
				float2 temp_output_1_0_g11740 = temp_output_1_0_g11739;
				float temp_output_2_0_g11745 = temp_output_10_0_g11738;
				float2 appendResult16_g11745 = (float2(temp_output_2_0_g11745 , 0.0));
				float2 appendResult25_g11747 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11747 = ( appendResult16_g11745 * appendResult25_g11747 );
				float2 temp_output_1_0_g11745 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,-1 ) ) );
				float2 temp_output_1_0_g11747 = temp_output_1_0_g11745;
				float2 appendResult17_g11745 = (float2(0.0 , temp_output_2_0_g11745));
				float2 appendResult25_g11746 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11746 = ( appendResult17_g11745 * appendResult25_g11746 );
				float2 temp_output_1_0_g11746 = temp_output_1_0_g11745;
				float temp_output_2_0_g11742 = temp_output_10_0_g11738;
				float2 appendResult16_g11742 = (float2(temp_output_2_0_g11742 , 0.0));
				float2 appendResult25_g11744 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11744 = ( appendResult16_g11742 * appendResult25_g11744 );
				float2 temp_output_1_0_g11742 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,-1 ) ) );
				float2 temp_output_1_0_g11744 = temp_output_1_0_g11742;
				float2 appendResult17_g11742 = (float2(0.0 , temp_output_2_0_g11742));
				float2 appendResult25_g11743 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11743 = ( appendResult17_g11742 * appendResult25_g11743 );
				float2 temp_output_1_0_g11743 = temp_output_1_0_g11742;
				float4 staticSwitch3_g11736 = ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) ) ) ) * 0.0625 );
				#else
				float4 staticSwitch3_g11736 = staticSwitch8_g11736;
				#endif
				#ifdef _ENABLESHARPEN_ON
				float2 temp_output_1_0_g11751 = temp_output_1_0_g11736;
				float4 tex2DNode4_g11751 = tex2D( _MainTex, temp_output_1_0_g11751 );
				float temp_output_2_0_g11752 = _SharpenOffset;
				float2 appendResult16_g11752 = (float2(temp_output_2_0_g11752 , 0.0));
				float2 appendResult25_g11754 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11754 = ( appendResult16_g11752 * appendResult25_g11754 );
				float2 temp_output_1_0_g11752 = temp_output_1_0_g11751;
				float2 temp_output_1_0_g11754 = temp_output_1_0_g11752;
				float2 appendResult17_g11752 = (float2(0.0 , temp_output_2_0_g11752));
				float2 appendResult25_g11753 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11753 = ( appendResult17_g11752 * appendResult25_g11753 );
				float2 temp_output_1_0_g11753 = temp_output_1_0_g11752;
				float4 break22_g11751 = ( tex2DNode4_g11751 - ( ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) ) ) / 4.0 ) - tex2DNode4_g11751 ) * ( _SharpenFactor * _SharpenFade ) ) );
				float clampResult23_g11751 = clamp( break22_g11751.a , 0.0 , 1.0 );
				float4 appendResult24_g11751 = (float4(break22_g11751.r , break22_g11751.g , break22_g11751.b , clampResult23_g11751));
				float4 staticSwitch12_g11736 = appendResult24_g11751;
				#else
				float4 staticSwitch12_g11736 = staticSwitch3_g11736;
				#endif
				float4 temp_output_471_0 = staticSwitch12_g11736;
				#ifdef _VERTEXTINTFIRST_ON
				float4 temp_output_1_0_g11755 = temp_output_471_0;
				float4 appendResult8_g11755 = (float4(( (temp_output_1_0_g11755).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11755.a));
				float4 staticSwitch354 = appendResult8_g11755;
				#else
				float4 staticSwitch354 = temp_output_471_0;
				#endif
				float4 originalColor191 = staticSwitch354;
				float4 temp_output_1_0_g11756 = originalColor191;
				float4 temp_output_1_0_g11757 = temp_output_1_0_g11756;
				float2 temp_output_7_0_g11756 = finalUV146;
				float2 temp_output_43_0_g11757 = temp_output_7_0_g11756;
				float2 temp_cast_15 = (_SmokeNoiseScale).xx;
				float linValue16_g11758 = tex2D( _UberNoiseTexture, ( ( ( IN.ase_color.r * (( _SmokeVertexSeed )?( 5.0 ):( 0.0 )) ) + temp_output_43_0_g11757 ) * temp_cast_15 ) ).r;
				float localMyCustomExpression16_g11758 = MyCustomExpression16_g11758( linValue16_g11758 );
				float clampResult28_g11757 = clamp( ( ( ( localMyCustomExpression16_g11758 - 1.0 ) * _SmokeNoiseFactor ) + ( ( ( IN.ase_color.a / 2.5 ) - distance( temp_output_43_0_g11757 , float2( 0.5,0.5 ) ) ) * 2.5 * _SmokeSmoothness ) ) , 0.0 , 1.0 );
				#ifdef _ENABLESMOKE_ON
				float3 lerpResult34_g11757 = lerp( (temp_output_1_0_g11757).rgb , float3( 0,0,0 ) , ( ( 1.0 - clampResult28_g11757 ) * _SmokeDarkEdge ));
				float4 appendResult31_g11757 = (float4(lerpResult34_g11757 , ( clampResult28_g11757 * _SmokeAlpha * temp_output_1_0_g11757.a )));
				float4 staticSwitch2_g11756 = appendResult31_g11757;
				#else
				float4 staticSwitch2_g11756 = temp_output_1_0_g11756;
				#endif
				#ifdef _ENABLECUSTOMFADE_ON
				float4 temp_output_1_0_g11759 = staticSwitch2_g11756;
				float2 temp_output_57_0_g11759 = temp_output_7_0_g11756;
				float4 tex2DNode3_g11759 = tex2D( _CustomFadeFadeMask, temp_output_57_0_g11759 );
				float linValue16_g11760 = tex2D( _UberNoiseTexture, ( temp_output_57_0_g11759 * _CustomFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11760 = MyCustomExpression16_g11760( linValue16_g11760 );
				float clampResult37_g11759 = clamp( ( ( ( IN.ase_color.a * 2.0 ) - 1.0 ) + ( tex2DNode3_g11759.r + ( localMyCustomExpression16_g11760 * _CustomFadeNoiseFactor ) ) ) , 0.0 , 1.0 );
				float4 appendResult13_g11759 = (float4((temp_output_1_0_g11759).rgb , ( temp_output_1_0_g11759.a * pow( clampResult37_g11759 , ( _CustomFadeSmoothness / max( tex2DNode3_g11759.r , 0.05 ) ) ) * _CustomFadeAlpha )));
				float4 staticSwitch3_g11756 = appendResult13_g11759;
				#else
				float4 staticSwitch3_g11756 = staticSwitch2_g11756;
				#endif
				float4 temp_output_1_0_g11761 = staticSwitch3_g11756;
				#ifdef _ENABLECHECKERBOARD_ON
				float4 temp_output_1_0_g11762 = temp_output_1_0_g11761;
				float2 appendResult4_g11762 = (float2(worldPos.x , worldPos.y));
				float2 temp_output_44_0_g11762 = ( appendResult4_g11762 * _CheckerboardTiling * 0.5 );
				float2 break12_g11762 = step( ( ceil( temp_output_44_0_g11762 ) - temp_output_44_0_g11762 ) , float2( 0.5,0.5 ) );
				float4 appendResult42_g11762 = (float4(( (temp_output_1_0_g11762).rgb * min( ( _CheckerboardDarken + abs( ( -break12_g11762.x + break12_g11762.y ) ) ) , 1.0 ) ) , temp_output_1_0_g11762.a));
				float4 staticSwitch2_g11761 = appendResult42_g11762;
				#else
				float4 staticSwitch2_g11761 = temp_output_1_0_g11761;
				#endif
				float2 temp_output_75_0_g11763 = finalUV146;
				float linValue16_g11764 = tex2D( _UberNoiseTexture, ( ( ( shaderTime237 * _FlameSpeed ) + temp_output_75_0_g11763 ) * _FlameNoiseScale ) ).r;
				float localMyCustomExpression16_g11764 = MyCustomExpression16_g11764( linValue16_g11764 );
				float saferPower57_g11763 = abs( max( ( temp_output_75_0_g11763.y - 0.2 ) , 0.0 ) );
				float temp_output_47_0_g11763 = max( _FlameRadius , 0.01 );
				float clampResult70_g11763 = clamp( ( ( ( localMyCustomExpression16_g11764 * pow( saferPower57_g11763 , _FlameNoiseHeightFactor ) * _FlameNoiseFactor ) + ( ( temp_output_47_0_g11763 - distance( temp_output_75_0_g11763 , float2( 0.5,0.4 ) ) ) / temp_output_47_0_g11763 ) ) * _FlameSmooth ) , 0.0 , 1.0 );
				#ifdef _ENABLEFLAME_ON
				float temp_output_63_0_g11763 = ( clampResult70_g11763 * _FlameBrightness );
				float4 appendResult31_g11763 = (float4(temp_output_63_0_g11763 , temp_output_63_0_g11763 , temp_output_63_0_g11763 , clampResult70_g11763));
				float4 staticSwitch6_g11761 = ( appendResult31_g11763 * staticSwitch2_g11761 );
				#else
				float4 staticSwitch6_g11761 = staticSwitch2_g11761;
				#endif
				float4 temp_output_3_0_g11765 = staticSwitch6_g11761;
				float4 temp_output_1_0_g11792 = temp_output_3_0_g11765;
				float2 temp_output_1_0_g11765 = finalUV146;
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
				float4 staticSwitch81_g11792 = tex2D( _RecolorRGBTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch81_g11792 = temp_output_1_0_g11792;
				#endif
				#ifdef _ENABLERECOLORRGB_ON
				float4 break82_g11792 = staticSwitch81_g11792;
				float temp_output_63_0_g11792 = ( break82_g11792.r + break82_g11792.g + break82_g11792.b );
				float4 break71_g11792 = ( ( _RecolorRGBRedTint * ( break82_g11792.r / temp_output_63_0_g11792 ) ) + ( _RecolorRGBGreenTint * ( break82_g11792.g / temp_output_63_0_g11792 ) ) + ( ( break82_g11792.b / temp_output_63_0_g11792 ) * _RecolorRGBBlueTint ) );
				float3 appendResult56_g11792 = (float3(break71_g11792.r , break71_g11792.g , break71_g11792.b));
				float4 break2_g11793 = temp_output_1_0_g11792;
				float saferPower57_g11792 = abs( ( ( break2_g11793.x + break2_g11793.x + break2_g11793.y + break2_g11793.y + break2_g11793.y + break2_g11793.z ) / 6.0 ) );
				float3 lerpResult26_g11792 = lerp( (temp_output_1_0_g11792).rgb , ( appendResult56_g11792 * pow( saferPower57_g11792 , ( max( break71_g11792.a , 0.01 ) * 2.0 ) ) ) , ( min( ( temp_output_63_0_g11792 * 2.0 ) , 1.0 ) * _RecolorRGBFade ));
				float4 appendResult30_g11792 = (float4(lerpResult26_g11792 , temp_output_1_0_g11792.a));
				float4 staticSwitch43_g11765 = appendResult30_g11792;
				#else
				float4 staticSwitch43_g11765 = temp_output_3_0_g11765;
				#endif
				float4 temp_output_1_0_g11790 = staticSwitch43_g11765;
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
				float4 staticSwitch62_g11790 = tex2D( _RecolorRGBYCPTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch62_g11790 = temp_output_1_0_g11790;
				#endif
				float3 hsvTorgb33_g11790 = RGBToHSV( staticSwitch62_g11790.rgb );
				float temp_output_43_0_g11790 = ( ( hsvTorgb33_g11790.x + 0.08333334 ) % 1.0 );
				float4 ifLocalVar46_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.8333333 )
				ifLocalVar46_g11790 = _RecolorRGBYCPPurpleTint;
				else
				ifLocalVar46_g11790 = _RecolorRGBYCPBlueTint;
				float4 ifLocalVar44_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.6666667 )
				ifLocalVar44_g11790 = _RecolorRGBYCPCyanTint;
				else
				ifLocalVar44_g11790 = ifLocalVar46_g11790;
				float4 ifLocalVar47_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.3333333 )
				ifLocalVar47_g11790 = _RecolorRGBYCPYellowTint;
				else
				ifLocalVar47_g11790 = _RecolorRGBYCPGreenTint;
				float4 ifLocalVar45_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.1666667 )
				ifLocalVar45_g11790 = _RecolorRGBYCPRedTint;
				else
				ifLocalVar45_g11790 = ifLocalVar47_g11790;
				float4 ifLocalVar35_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.5 )
				ifLocalVar35_g11790 = ifLocalVar44_g11790;
				else
				ifLocalVar35_g11790 = ifLocalVar45_g11790;
				#ifdef _ENABLERECOLORRGBYCP_ON
				float4 break55_g11790 = ifLocalVar35_g11790;
				float3 appendResult56_g11790 = (float3(break55_g11790.r , break55_g11790.g , break55_g11790.b));
				float4 break2_g11791 = temp_output_1_0_g11790;
				float saferPower57_g11790 = abs( ( ( break2_g11791.x + break2_g11791.x + break2_g11791.y + break2_g11791.y + break2_g11791.y + break2_g11791.z ) / 6.0 ) );
				float3 lerpResult26_g11790 = lerp( (temp_output_1_0_g11790).rgb , ( appendResult56_g11790 * pow( saferPower57_g11790 , max( ( break55_g11790.a * 2.0 ) , 0.01 ) ) ) , ( hsvTorgb33_g11790.z * _RecolorRGBYCPFade ));
				float4 appendResult30_g11790 = (float4(lerpResult26_g11790 , temp_output_1_0_g11790.a));
				float4 staticSwitch9_g11765 = appendResult30_g11790;
				#else
				float4 staticSwitch9_g11765 = staticSwitch43_g11765;
				#endif
				#ifdef _ENABLECOLORREPLACE_ON
				float4 temp_output_1_0_g11768 = staticSwitch9_g11765;
				float3 temp_output_2_0_g11768 = (temp_output_1_0_g11768).rgb;
				float3 In115_g11768 = temp_output_2_0_g11768;
				float3 From115_g11768 = (_ColorReplaceFromColor).rgb;
				float4 break2_g11769 = temp_output_1_0_g11768;
				float3 To115_g11768 = ( pow( ( ( break2_g11769.x + break2_g11769.x + break2_g11769.y + break2_g11769.y + break2_g11769.y + break2_g11769.z ) / 6.0 ) , max( _ColorReplaceContrast , 0.0001 ) ) * (_ColorReplaceToColor).rgb );
				float Fuzziness115_g11768 = _ColorReplaceSmoothness;
				float Range115_g11768 = _ColorReplaceRange;
				float3 localMyCustomExpression115_g11768 = MyCustomExpression115_g11768( In115_g11768 , From115_g11768 , To115_g11768 , Fuzziness115_g11768 , Range115_g11768 );
				float3 lerpResult112_g11768 = lerp( temp_output_2_0_g11768 , localMyCustomExpression115_g11768 , _ColorReplaceFade);
				float4 appendResult4_g11768 = (float4(lerpResult112_g11768 , temp_output_1_0_g11768.a));
				float4 staticSwitch29_g11765 = appendResult4_g11768;
				#else
				float4 staticSwitch29_g11765 = staticSwitch9_g11765;
				#endif
				float4 temp_output_1_0_g11779 = staticSwitch29_g11765;
				#ifdef _ENABLENEGATIVE_ON
				float3 temp_output_9_0_g11779 = (temp_output_1_0_g11779).rgb;
				float3 lerpResult3_g11779 = lerp( temp_output_9_0_g11779 , ( 1.0 - temp_output_9_0_g11779 ) , _NegativeFade);
				float4 appendResult8_g11779 = (float4(lerpResult3_g11779 , temp_output_1_0_g11779.a));
				float4 staticSwitch4_g11779 = appendResult8_g11779;
				#else
				float4 staticSwitch4_g11779 = temp_output_1_0_g11779;
				#endif
				float4 temp_output_57_0_g11765 = staticSwitch4_g11779;
				#ifdef _ENABLECONTRAST_ON
				float4 temp_output_1_0_g11800 = temp_output_57_0_g11765;
				float3 saferPower5_g11800 = abs( (temp_output_1_0_g11800).rgb );
				float3 temp_cast_29 = (_Contrast).xxx;
				float4 appendResult4_g11800 = (float4(pow( saferPower5_g11800 , temp_cast_29 ) , temp_output_1_0_g11800.a));
				float4 staticSwitch32_g11765 = appendResult4_g11800;
				#else
				float4 staticSwitch32_g11765 = temp_output_57_0_g11765;
				#endif
				#ifdef _ENABLEBRIGHTNESS_ON
				float4 temp_output_2_0_g11777 = staticSwitch32_g11765;
				float4 appendResult6_g11777 = (float4(( (temp_output_2_0_g11777).rgb * _Brightness ) , temp_output_2_0_g11777.a));
				float4 staticSwitch33_g11765 = appendResult6_g11777;
				#else
				float4 staticSwitch33_g11765 = staticSwitch32_g11765;
				#endif
				#ifdef _ENABLEHUE_ON
				float4 temp_output_2_0_g11778 = staticSwitch33_g11765;
				float3 hsvTorgb1_g11778 = RGBToHSV( temp_output_2_0_g11778.rgb );
				float3 hsvTorgb3_g11778 = HSVToRGB( float3(( hsvTorgb1_g11778.x + _Hue ),hsvTorgb1_g11778.y,hsvTorgb1_g11778.z) );
				float4 appendResult8_g11778 = (float4(hsvTorgb3_g11778 , temp_output_2_0_g11778.a));
				float4 staticSwitch36_g11765 = appendResult8_g11778;
				#else
				float4 staticSwitch36_g11765 = staticSwitch33_g11765;
				#endif
				#ifdef _ENABLESPLITTONING_ON
				float4 temp_output_1_0_g11794 = staticSwitch36_g11765;
				float4 break2_g11795 = temp_output_1_0_g11794;
				float temp_output_3_0_g11794 = ( ( break2_g11795.x + break2_g11795.x + break2_g11795.y + break2_g11795.y + break2_g11795.y + break2_g11795.z ) / 6.0 );
				float clampResult25_g11794 = clamp( ( ( ( ( temp_output_3_0_g11794 + _SplitToningShift ) - 0.5 ) * _SplitToningBalance ) + 0.5 ) , 0.0 , 1.0 );
				float3 lerpResult6_g11794 = lerp( (_SplitToningShadowsColor).rgb , (_SplitToningHighlightsColor).rgb , clampResult25_g11794);
				float temp_output_9_0_g11796 = max( _SplitToningContrast , 0.0 );
				float saferPower7_g11796 = abs( ( temp_output_3_0_g11794 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11796 ) , 0.0 ) ) ) );
				float3 lerpResult11_g11794 = lerp( (temp_output_1_0_g11794).rgb , ( lerpResult6_g11794 * pow( saferPower7_g11796 , temp_output_9_0_g11796 ) ) , _SplitToningFade);
				float4 appendResult18_g11794 = (float4(lerpResult11_g11794 , temp_output_1_0_g11794.a));
				float4 staticSwitch30_g11765 = appendResult18_g11794;
				#else
				float4 staticSwitch30_g11765 = staticSwitch36_g11765;
				#endif
				#ifdef _ENABLEBLACKTINT_ON
				float4 temp_output_1_0_g11775 = staticSwitch30_g11765;
				float3 temp_output_4_0_g11775 = (temp_output_1_0_g11775).rgb;
				float4 break12_g11775 = temp_output_1_0_g11775;
				float3 lerpResult7_g11775 = lerp( temp_output_4_0_g11775 , ( temp_output_4_0_g11775 + (_BlackTintColor).rgb ) , pow( ( 1.0 - min( max( max( break12_g11775.r , break12_g11775.g ) , break12_g11775.b ) , 1.0 ) ) , max( _BlackTintPower , 0.001 ) ));
				float3 lerpResult13_g11775 = lerp( temp_output_4_0_g11775 , lerpResult7_g11775 , _BlackTintFade);
				float4 appendResult11_g11775 = (float4(lerpResult13_g11775 , break12_g11775.a));
				float4 staticSwitch20_g11765 = appendResult11_g11775;
				#else
				float4 staticSwitch20_g11765 = staticSwitch30_g11765;
				#endif
				#ifdef _ENABLEINKSPREAD_ON
				float4 temp_output_1_0_g11786 = staticSwitch20_g11765;
				float4 break2_g11788 = temp_output_1_0_g11786;
				float temp_output_9_0_g11789 = max( _InkSpreadContrast , 0.0 );
				float saferPower7_g11789 = abs( ( ( ( break2_g11788.x + break2_g11788.x + break2_g11788.y + break2_g11788.y + break2_g11788.y + break2_g11788.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11789 ) , 0.0 ) ) ) );
				float2 temp_output_65_0_g11786 = shaderPosition235;
				float linValue16_g11787 = tex2D( _UberNoiseTexture, ( temp_output_65_0_g11786 * _InkSpreadNoiseScale ) ).r;
				float localMyCustomExpression16_g11787 = MyCustomExpression16_g11787( linValue16_g11787 );
				float clampResult53_g11786 = clamp( ( ( ( _InkSpreadDistance - distance( _InkSpreadPosition , temp_output_65_0_g11786 ) ) + ( localMyCustomExpression16_g11787 * _InkSpreadNoiseFactor ) ) / max( _InkSpreadWidth , 0.001 ) ) , 0.0 , 1.0 );
				float3 lerpResult7_g11786 = lerp( (temp_output_1_0_g11786).rgb , ( (_InkSpreadColor).rgb * pow( saferPower7_g11789 , temp_output_9_0_g11789 ) ) , ( _InkSpreadFade * clampResult53_g11786 ));
				float4 appendResult9_g11786 = (float4(lerpResult7_g11786 , (temp_output_1_0_g11786).a));
				float4 staticSwitch17_g11765 = appendResult9_g11786;
				#else
				float4 staticSwitch17_g11765 = staticSwitch20_g11765;
				#endif
				float temp_output_39_0_g11765 = shaderTime237;
				#ifdef _ENABLESHIFTHUE_ON
				float4 temp_output_1_0_g11780 = staticSwitch17_g11765;
				float3 hsvTorgb15_g11780 = RGBToHSV( (temp_output_1_0_g11780).rgb );
				float3 hsvTorgb19_g11780 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _ShiftHueSpeed ) + hsvTorgb15_g11780.x ),hsvTorgb15_g11780.y,hsvTorgb15_g11780.z) );
				float4 appendResult6_g11780 = (float4(hsvTorgb19_g11780 , temp_output_1_0_g11780.a));
				float4 staticSwitch19_g11765 = appendResult6_g11780;
				#else
				float4 staticSwitch19_g11765 = staticSwitch17_g11765;
				#endif
				float3 hsvTorgb19_g11783 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _AddHueSpeed ) % 1.0 ),_AddHueSaturation,_AddHueBrightness) );
				float4 temp_output_1_0_g11783 = staticSwitch19_g11765;
				float4 break2_g11785 = temp_output_1_0_g11783;
				float saferPower27_g11783 = abs( ( ( break2_g11785.x + break2_g11785.x + break2_g11785.y + break2_g11785.y + break2_g11785.y + break2_g11785.z ) / 6.0 ) );
				#ifdef _ADDHUEMASKTOGGLE_ON
				float2 uv_AddHueMask = IN.ase_texcoord9.xy * _AddHueMask_ST.xy + _AddHueMask_ST.zw;
				float4 tex2DNode3_g11784 = tex2D( _AddHueMask, uv_AddHueMask );
				float staticSwitch33_g11783 = ( _AddHueFade * ( tex2DNode3_g11784.r * tex2DNode3_g11784.a ) );
				#else
				float staticSwitch33_g11783 = _AddHueFade;
				#endif
				#ifdef _ENABLEADDHUE_ON
				float4 appendResult6_g11783 = (float4(( ( hsvTorgb19_g11783 * pow( saferPower27_g11783 , max( _AddHueContrast , 0.001 ) ) * staticSwitch33_g11783 ) + (temp_output_1_0_g11783).rgb ) , temp_output_1_0_g11783.a));
				float4 staticSwitch23_g11765 = appendResult6_g11783;
				#else
				float4 staticSwitch23_g11765 = staticSwitch19_g11765;
				#endif
				float4 temp_output_1_0_g11781 = staticSwitch23_g11765;
				float4 break2_g11782 = temp_output_1_0_g11781;
				float3 temp_output_13_0_g11781 = (_SineGlowColor).rgb;
				#ifdef _SINEGLOWMASKTOGGLE_ON
				float2 uv_SineGlowMask = IN.ase_texcoord9.xy * _SineGlowMask_ST.xy + _SineGlowMask_ST.zw;
				float4 tex2DNode30_g11781 = tex2D( _SineGlowMask, uv_SineGlowMask );
				float3 staticSwitch27_g11781 = ( (tex2DNode30_g11781).rgb * temp_output_13_0_g11781 * tex2DNode30_g11781.a );
				#else
				float3 staticSwitch27_g11781 = temp_output_13_0_g11781;
				#endif
				#ifdef _ENABLESINEGLOW_ON
				float4 appendResult21_g11781 = (float4(( (temp_output_1_0_g11781).rgb + ( pow( ( ( break2_g11782.x + break2_g11782.x + break2_g11782.y + break2_g11782.y + break2_g11782.y + break2_g11782.z ) / 6.0 ) , max( _SineGlowContrast , 0.0 ) ) * staticSwitch27_g11781 * _SineGlowFade * ( ( ( sin( ( temp_output_39_0_g11765 * _SineGlowFrequency ) ) + 1.0 ) * ( _SineGlowMax - _SineGlowMin ) ) + _SineGlowMin ) ) ) , temp_output_1_0_g11781.a));
				float4 staticSwitch28_g11765 = appendResult21_g11781;
				#else
				float4 staticSwitch28_g11765 = staticSwitch23_g11765;
				#endif
				#ifdef _ENABLESATURATION_ON
				float4 temp_output_1_0_g11770 = staticSwitch28_g11765;
				float4 break2_g11771 = temp_output_1_0_g11770;
				float3 temp_cast_45 = (( ( break2_g11771.x + break2_g11771.x + break2_g11771.y + break2_g11771.y + break2_g11771.y + break2_g11771.z ) / 6.0 )).xxx;
				float3 lerpResult5_g11770 = lerp( temp_cast_45 , (temp_output_1_0_g11770).rgb , _Saturation);
				float4 appendResult8_g11770 = (float4(lerpResult5_g11770 , temp_output_1_0_g11770.a));
				float4 staticSwitch38_g11765 = appendResult8_g11770;
				#else
				float4 staticSwitch38_g11765 = staticSwitch28_g11765;
				#endif
				float4 temp_output_15_0_g11772 = staticSwitch38_g11765;
				float3 temp_output_82_0_g11772 = (_InnerOutlineColor).rgb;
				float2 temp_output_7_0_g11772 = temp_output_1_0_g11765;
				float temp_output_179_0_g11772 = temp_output_39_0_g11765;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch187_g11772 = ( (tex2D( _InnerOutlineTintTexture, ( temp_output_7_0_g11772 + ( _InnerOutlineTextureSpeed * temp_output_179_0_g11772 ) ) )).rgb * temp_output_82_0_g11772 );
				#else
				float3 staticSwitch187_g11772 = temp_output_82_0_g11772;
				#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11774 = tex2D( _UberNoiseTexture, ( ( ( temp_output_179_0_g11772 * _InnerOutlineNoiseSpeed ) + temp_output_7_0_g11772 ) * _InnerOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11774 = MyCustomExpression16_g11774( linValue16_g11774 );
				float2 staticSwitch169_g11772 = ( ( localMyCustomExpression16_g11774 - 0.5 ) * _InnerOutlineDistortionIntensity );
				#else
				float2 staticSwitch169_g11772 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11772 = ( staticSwitch169_g11772 + temp_output_7_0_g11772 );
				float2 appendResult2_g11773 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11772 = ( 100.0 / appendResult2_g11773 );
				float temp_output_178_0_g11772 = ( _InnerOutlineFade * ( 1.0 - min( min( min( min( min( min( min( tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11772 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) ) );
				float3 lerpResult176_g11772 = lerp( (temp_output_15_0_g11772).rgb , staticSwitch187_g11772 , temp_output_178_0_g11772);
				#ifdef _INNEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch188_g11772 = ( temp_output_178_0_g11772 * temp_output_15_0_g11772.a );
				#else
				float staticSwitch188_g11772 = temp_output_15_0_g11772.a;
				#endif
				#ifdef _ENABLEINNEROUTLINE_ON
				float4 appendResult177_g11772 = (float4(lerpResult176_g11772 , staticSwitch188_g11772));
				float4 staticSwitch12_g11765 = appendResult177_g11772;
				#else
				float4 staticSwitch12_g11765 = staticSwitch38_g11765;
				#endif
				float4 temp_output_15_0_g11797 = staticSwitch12_g11765;
				float3 temp_output_82_0_g11797 = (_OuterOutlineColor).rgb;
				float2 temp_output_7_0_g11797 = temp_output_1_0_g11765;
				float temp_output_186_0_g11797 = temp_output_39_0_g11765;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11797 = ( (tex2D( _OuterOutlineTintTexture, ( temp_output_7_0_g11797 + ( _OuterOutlineTextureSpeed * temp_output_186_0_g11797 ) ) )).rgb * temp_output_82_0_g11797 );
				#else
				float3 staticSwitch199_g11797 = temp_output_82_0_g11797;
				#endif
				float temp_output_182_0_g11797 = ( ( 1.0 - temp_output_15_0_g11797.a ) * min( ( _OuterOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11797 = 1.0;
				#else
				float staticSwitch203_g11797 = temp_output_182_0_g11797;
				#endif
				float3 lerpResult178_g11797 = lerp( (temp_output_15_0_g11797).rgb , staticSwitch199_g11797 , staticSwitch203_g11797);
				float3 lerpResult170_g11797 = lerp( lerpResult178_g11797 , staticSwitch199_g11797 , staticSwitch203_g11797);
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11798 = tex2D( _UberNoiseTexture, ( ( ( temp_output_186_0_g11797 * _OuterOutlineNoiseSpeed ) + temp_output_7_0_g11797 ) * _OuterOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11798 = MyCustomExpression16_g11798( linValue16_g11798 );
				float2 staticSwitch157_g11797 = ( ( localMyCustomExpression16_g11798 - 0.5 ) * _OuterOutlineDistortionIntensity );
				#else
				float2 staticSwitch157_g11797 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11797 = ( staticSwitch157_g11797 + temp_output_7_0_g11797 );
				float2 appendResult2_g11799 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11797 = ( 100.0 / appendResult2_g11799 );
				float lerpResult168_g11797 = lerp( temp_output_15_0_g11797.a , min( ( max( max( max( max( max( max( max( tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11797 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) * 3.0 ) , 1.0 ) , _OuterOutlineFade);
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11797 = ( temp_output_182_0_g11797 * lerpResult168_g11797 );
				#else
				float staticSwitch200_g11797 = lerpResult168_g11797;
				#endif
				#ifdef _ENABLEOUTEROUTLINE_ON
				float4 appendResult174_g11797 = (float4(lerpResult170_g11797 , staticSwitch200_g11797));
				float4 staticSwitch13_g11765 = appendResult174_g11797;
				#else
				float4 staticSwitch13_g11765 = staticSwitch12_g11765;
				#endif
				float4 temp_output_15_0_g11776 = staticSwitch13_g11765;
				float3 temp_output_82_0_g11776 = (_PixelOutlineColor).rgb;
				float2 temp_output_7_0_g11776 = temp_output_1_0_g11765;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11776 = ( (tex2D( _PixelOutlineTintTexture, ( temp_output_7_0_g11776 + ( _PixelOutlineTextureSpeed * temp_output_39_0_g11765 ) ) )).rgb * temp_output_82_0_g11776 );
				#else
				float3 staticSwitch199_g11776 = temp_output_82_0_g11776;
				#endif
				float temp_output_182_0_g11776 = ( ( 1.0 - temp_output_15_0_g11776.a ) * min( ( _PixelOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11776 = 1.0;
				#else
				float staticSwitch203_g11776 = temp_output_182_0_g11776;
				#endif
				float3 lerpResult178_g11776 = lerp( (temp_output_15_0_g11776).rgb , staticSwitch199_g11776 , staticSwitch203_g11776);
				float3 lerpResult170_g11776 = lerp( lerpResult178_g11776 , staticSwitch199_g11776 , staticSwitch203_g11776);
				float2 appendResult206_g11776 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_209_0_g11776 = ( float2( 1,1 ) / appendResult206_g11776 );
				float lerpResult168_g11776 = lerp( temp_output_15_0_g11776.a , min( ( max( max( max( tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,-1 ) ) * temp_output_209_0_g11776 ) ) ).a , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,1 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( -1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) * 3.0 ) , 1.0 ) , _PixelOutlineFade);
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11776 = ( temp_output_182_0_g11776 * lerpResult168_g11776 );
				#else
				float staticSwitch200_g11776 = lerpResult168_g11776;
				#endif
				#ifdef _ENABLEPIXELOUTLINE_ON
				float4 appendResult174_g11776 = (float4(lerpResult170_g11776 , staticSwitch200_g11776));
				float4 staticSwitch48_g11765 = appendResult174_g11776;
				#else
				float4 staticSwitch48_g11765 = staticSwitch13_g11765;
				#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
				float3 lerpResult15_g11766 = lerp( (_PingPongGlowFrom).rgb , (_PingPongGlowTo).rgb , ( ( sin( ( temp_output_39_0_g11765 * _PingPongGlowFrequency ) ) + 1.0 ) / 2.0 ));
				float4 temp_output_5_0_g11766 = staticSwitch48_g11765;
				float4 break2_g11767 = temp_output_5_0_g11766;
				float4 appendResult12_g11766 = (float4(( ( lerpResult15_g11766 * _PingPongGlowFade * pow( ( ( break2_g11767.x + break2_g11767.x + break2_g11767.y + break2_g11767.y + break2_g11767.y + break2_g11767.z ) / 6.0 ) , max( _PingPongGlowContrast , 0.0 ) ) ) + (temp_output_5_0_g11766).rgb ) , temp_output_5_0_g11766.a));
				float4 staticSwitch46_g11765 = appendResult12_g11766;
				#else
				float4 staticSwitch46_g11765 = staticSwitch48_g11765;
				#endif
				float4 temp_output_361_0 = staticSwitch46_g11765;
				#ifdef _ENABLEHOLOGRAM_ON
				float4 temp_output_1_0_g11801 = temp_output_361_0;
				float4 break2_g11802 = temp_output_1_0_g11801;
				float temp_output_9_0_g11803 = max( _HologramContrast , 0.0 );
				float saferPower7_g11803 = abs( ( ( ( break2_g11802.x + break2_g11802.x + break2_g11802.y + break2_g11802.y + break2_g11802.y + break2_g11802.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11803 ) , 0.0 ) ) ) );
				float4 appendResult22_g11801 = (float4(( (_HologramTint).rgb * pow( saferPower7_g11803 , temp_output_9_0_g11803 ) ) , ( max( pow( abs( sin( ( ( ( ( shaderTime237 * _HologramLineSpeed ) + worldPos.y ) / unity_OrthoParams.y ) * _HologramLineFrequency ) ) ) , _HologramLineGap ) , _HologramMinAlpha ) * temp_output_1_0_g11801.a )));
				float4 lerpResult37_g11801 = lerp( temp_output_1_0_g11801 , appendResult22_g11801 , hologramFade182);
				float4 staticSwitch56 = lerpResult37_g11801;
				#else
				float4 staticSwitch56 = temp_output_361_0;
				#endif
				#ifdef _ENABLEGLITCH_ON
				float4 temp_output_1_0_g11804 = staticSwitch56;
				float4 break2_g11806 = temp_output_1_0_g11804;
				float temp_output_34_0_g11804 = shaderTime237;
				float linValue16_g11805 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchNoiseSpeed * temp_output_34_0_g11804 ) ) * _GlitchNoiseScale ) ).r;
				float localMyCustomExpression16_g11805 = MyCustomExpression16_g11805( linValue16_g11805 );
				float3 hsvTorgb3_g11807 = HSVToRGB( float3(( localMyCustomExpression16_g11805 + ( temp_output_34_0_g11804 * _GlitchHueSpeed ) ),1.0,1.0) );
				float3 lerpResult23_g11804 = lerp( (temp_output_1_0_g11804).rgb , ( ( ( break2_g11806.x + break2_g11806.x + break2_g11806.y + break2_g11806.y + break2_g11806.y + break2_g11806.z ) / 6.0 ) * _GlitchBrightness * hsvTorgb3_g11807 ) , glitchFade152);
				float4 appendResult27_g11804 = (float4(lerpResult23_g11804 , temp_output_1_0_g11804.a));
				float4 staticSwitch57 = appendResult27_g11804;
				#else
				float4 staticSwitch57 = staticSwitch56;
				#endif
				float4 temp_output_3_0_g11808 = staticSwitch57;
				float4 temp_output_1_0_g11833 = temp_output_3_0_g11808;
				float2 temp_output_41_0_g11808 = shaderPosition235;
				float2 temp_output_99_0_g11833 = temp_output_41_0_g11808;
				float temp_output_40_0_g11808 = shaderTime237;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
				float linValue16_g11838 = tex2D( _UberNoiseTexture, ( ( ( temp_output_40_0_g11808 * _CamouflageDistortionSpeed ) + temp_output_99_0_g11833 ) * _CamouflageDistortionScale ) ).r;
				float localMyCustomExpression16_g11838 = MyCustomExpression16_g11838( linValue16_g11838 );
				float2 staticSwitch101_g11833 = ( ( ( localMyCustomExpression16_g11838 - 0.25 ) * _CamouflageDistortionIntensity ) + temp_output_99_0_g11833 );
				#else
				float2 staticSwitch101_g11833 = temp_output_99_0_g11833;
				#endif
				float linValue16_g11835 = tex2D( _UberNoiseTexture, ( staticSwitch101_g11833 * _CamouflageNoiseScaleA ) ).r;
				float localMyCustomExpression16_g11835 = MyCustomExpression16_g11835( linValue16_g11835 );
				float clampResult52_g11833 = clamp( ( ( _CamouflageDensityA - localMyCustomExpression16_g11835 ) / max( _CamouflageSmoothnessA , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult55_g11833 = lerp( _CamouflageBaseColor , ( _CamouflageColorA * clampResult52_g11833 ) , clampResult52_g11833);
				float linValue16_g11837 = tex2D( _UberNoiseTexture, ( ( staticSwitch101_g11833 + float2( 12.3,12.3 ) ) * _CamouflageNoiseScaleB ) ).r;
				#ifdef _ENABLECAMOUFLAGE_ON
				float localMyCustomExpression16_g11837 = MyCustomExpression16_g11837( linValue16_g11837 );
				float clampResult65_g11833 = clamp( ( ( _CamouflageDensityB - localMyCustomExpression16_g11837 ) / max( _CamouflageSmoothnessB , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult68_g11833 = lerp( lerpResult55_g11833 , ( _CamouflageColorB * clampResult65_g11833 ) , clampResult65_g11833);
				float4 break2_g11836 = temp_output_1_0_g11833;
				float temp_output_9_0_g11834 = max( _CamouflageContrast , 0.0 );
				float saferPower7_g11834 = abs( ( ( ( break2_g11836.x + break2_g11836.x + break2_g11836.y + break2_g11836.y + break2_g11836.y + break2_g11836.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11834 ) , 0.0 ) ) ) );
				float3 lerpResult4_g11833 = lerp( (temp_output_1_0_g11833).rgb , ( (lerpResult68_g11833).rgb * pow( saferPower7_g11834 , temp_output_9_0_g11834 ) ) , _CamouflageFade);
				float4 appendResult7_g11833 = (float4(lerpResult4_g11833 , temp_output_1_0_g11833.a));
				float4 staticSwitch26_g11808 = appendResult7_g11833;
				#else
				float4 staticSwitch26_g11808 = temp_output_3_0_g11808;
				#endif
				float4 temp_output_1_0_g11827 = staticSwitch26_g11808;
				float temp_output_59_0_g11827 = temp_output_40_0_g11808;
				float2 temp_output_58_0_g11827 = temp_output_41_0_g11808;
				float linValue16_g11828 = tex2D( _UberNoiseTexture, ( ( ( temp_output_59_0_g11827 * _MetalNoiseDistortionSpeed ) + temp_output_58_0_g11827 ) * _MetalNoiseDistortionScale ) ).r;
				float localMyCustomExpression16_g11828 = MyCustomExpression16_g11828( linValue16_g11828 );
				float linValue16_g11830 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11828 - 0.25 ) * _MetalNoiseDistortion ) + ( ( temp_output_59_0_g11827 * _MetalNoiseSpeed ) + temp_output_58_0_g11827 ) ) * _MetalNoiseScale ) ).r;
				float localMyCustomExpression16_g11830 = MyCustomExpression16_g11830( linValue16_g11830 );
				float4 break2_g11829 = temp_output_1_0_g11827;
				float temp_output_5_0_g11827 = ( ( break2_g11829.x + break2_g11829.x + break2_g11829.y + break2_g11829.y + break2_g11829.y + break2_g11829.z ) / 6.0 );
				float temp_output_9_0_g11831 = max( _MetalHighlightContrast , 0.0 );
				float saferPower7_g11831 = abs( ( temp_output_5_0_g11827 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11831 ) , 0.0 ) ) ) );
				float saferPower2_g11827 = abs( temp_output_5_0_g11827 );
				#ifdef _METALMASKTOGGLE_ON
				float2 uv_MetalMask = IN.ase_texcoord9.xy * _MetalMask_ST.xy + _MetalMask_ST.zw;
				float4 tex2DNode3_g11832 = tex2D( _MetalMask, uv_MetalMask );
				float staticSwitch60_g11827 = ( _MetalFade * ( tex2DNode3_g11832.r * tex2DNode3_g11832.a ) );
				#else
				float staticSwitch60_g11827 = _MetalFade;
				#endif
				#ifdef _ENABLEMETAL_ON
				float4 lerpResult45_g11827 = lerp( temp_output_1_0_g11827 , ( ( max( ( ( _MetalHighlightDensity - localMyCustomExpression16_g11830 ) / max( _MetalHighlightDensity , 0.01 ) ) , 0.0 ) * _MetalHighlightColor * pow( saferPower7_g11831 , temp_output_9_0_g11831 ) ) + ( pow( saferPower2_g11827 , _MetalContrast ) * _MetalColor ) ) , staticSwitch60_g11827);
				float4 appendResult8_g11827 = (float4((lerpResult45_g11827).rgb , (temp_output_1_0_g11827).a));
				float4 staticSwitch28_g11808 = appendResult8_g11827;
				#else
				float4 staticSwitch28_g11808 = staticSwitch26_g11808;
				#endif
				#ifdef _ENABLEFROZEN_ON
				float4 temp_output_1_0_g11821 = staticSwitch28_g11808;
				float4 break2_g11822 = temp_output_1_0_g11821;
				float temp_output_7_0_g11821 = ( ( break2_g11822.x + break2_g11822.x + break2_g11822.y + break2_g11822.y + break2_g11822.y + break2_g11822.z ) / 6.0 );
				float temp_output_9_0_g11824 = max( _FrozenContrast , 0.0 );
				float saferPower7_g11824 = abs( ( temp_output_7_0_g11821 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11824 ) , 0.0 ) ) ) );
				float saferPower20_g11821 = abs( temp_output_7_0_g11821 );
				float2 temp_output_72_0_g11821 = temp_output_41_0_g11808;
				float linValue16_g11823 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11821 * _FrozenSnowScale ) ).r;
				float localMyCustomExpression16_g11823 = MyCustomExpression16_g11823( linValue16_g11823 );
				float temp_output_73_0_g11821 = temp_output_40_0_g11808;
				float linValue16_g11825 = tex2D( _UberNoiseTexture, ( ( ( temp_output_73_0_g11821 * _FrozenHighlightDistortionSpeed ) + temp_output_72_0_g11821 ) * _FrozenHighlightDistortionScale ) ).r;
				float localMyCustomExpression16_g11825 = MyCustomExpression16_g11825( linValue16_g11825 );
				float linValue16_g11826 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11825 - 0.25 ) * _FrozenHighlightDistortion ) + ( ( temp_output_73_0_g11821 * _FrozenHighlightSpeed ) + temp_output_72_0_g11821 ) ) * _FrozenHighlightScale ) ).r;
				float localMyCustomExpression16_g11826 = MyCustomExpression16_g11826( linValue16_g11826 );
				float saferPower42_g11821 = abs( temp_output_7_0_g11821 );
				float3 lerpResult57_g11821 = lerp( (temp_output_1_0_g11821).rgb , ( ( pow( saferPower7_g11824 , temp_output_9_0_g11824 ) * (_FrozenTint).rgb ) + ( pow( saferPower20_g11821 , _FrozenSnowContrast ) * ( (_FrozenSnowColor).rgb * max( ( _FrozenSnowDensity - localMyCustomExpression16_g11823 ) , 0.0 ) ) ) + (( max( ( ( _FrozenHighlightDensity - localMyCustomExpression16_g11826 ) / max( _FrozenHighlightDensity , 0.01 ) ) , 0.0 ) * _FrozenHighlightColor * pow( saferPower42_g11821 , _FrozenHighlightContrast ) )).rgb ) , _FrozenFade);
				float4 appendResult26_g11821 = (float4(lerpResult57_g11821 , temp_output_1_0_g11821.a));
				float4 staticSwitch29_g11808 = appendResult26_g11821;
				#else
				float4 staticSwitch29_g11808 = staticSwitch28_g11808;
				#endif
				float4 temp_output_1_0_g11816 = staticSwitch29_g11808;
				float3 temp_output_28_0_g11816 = (temp_output_1_0_g11816).rgb;
				float4 break2_g11820 = float4( temp_output_28_0_g11816 , 0.0 );
				float saferPower21_g11816 = abs( ( ( break2_g11820.x + break2_g11820.x + break2_g11820.y + break2_g11820.y + break2_g11820.y + break2_g11820.z ) / 6.0 ) );
				float2 temp_output_72_0_g11816 = temp_output_41_0_g11808;
				float linValue16_g11819 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnSwirlNoiseScale ) ).r;
				float localMyCustomExpression16_g11819 = MyCustomExpression16_g11819( linValue16_g11819 );
				float linValue16_g11817 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11819 - 0.5 ) * float2( 1,1 ) * _BurnSwirlFactor ) + temp_output_72_0_g11816 ) * _BurnInsideNoiseScale ) ).r;
				#ifdef _ENABLEBURN_ON
				float localMyCustomExpression16_g11817 = MyCustomExpression16_g11817( linValue16_g11817 );
				float clampResult68_g11816 = clamp( ( _BurnInsideNoiseFactor - localMyCustomExpression16_g11817 ) , 0.0 , 1.0 );
				float linValue16_g11818 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnEdgeNoiseScale ) ).r;
				float localMyCustomExpression16_g11818 = MyCustomExpression16_g11818( linValue16_g11818 );
				float temp_output_15_0_g11816 = ( ( ( _BurnRadius - distance( temp_output_72_0_g11816 , _BurnPosition ) ) + ( localMyCustomExpression16_g11818 * _BurnEdgeNoiseFactor ) ) / max( _BurnWidth , 0.01 ) );
				float clampResult18_g11816 = clamp( temp_output_15_0_g11816 , 0.0 , 1.0 );
				float3 lerpResult29_g11816 = lerp( temp_output_28_0_g11816 , ( pow( saferPower21_g11816 , max( _BurnInsideContrast , 0.001 ) ) * ( ( (_BurnInsideNoiseColor).rgb * clampResult68_g11816 ) + (_BurnInsideColor).rgb ) ) , clampResult18_g11816);
				float3 lerpResult40_g11816 = lerp( temp_output_28_0_g11816 , ( lerpResult29_g11816 + ( ( step( temp_output_15_0_g11816 , 1.0 ) * step( 0.0 , temp_output_15_0_g11816 ) ) * (_BurnEdgeColor).rgb ) ) , _BurnFade);
				float4 appendResult43_g11816 = (float4(lerpResult40_g11816 , temp_output_1_0_g11816.a));
				float4 staticSwitch32_g11808 = appendResult43_g11816;
				#else
				float4 staticSwitch32_g11808 = staticSwitch29_g11808;
				#endif
				#ifdef _ENABLERAINBOW_ON
				float2 temp_output_42_0_g11812 = temp_output_41_0_g11808;
				float linValue16_g11813 = tex2D( _UberNoiseTexture, ( temp_output_42_0_g11812 * _RainbowNoiseScale ) ).r;
				float localMyCustomExpression16_g11813 = MyCustomExpression16_g11813( linValue16_g11813 );
				float3 hsvTorgb3_g11815 = HSVToRGB( float3(( ( ( distance( temp_output_42_0_g11812 , _RainbowCenter ) + ( localMyCustomExpression16_g11813 * _RainbowNoiseFactor ) ) * _RainbowDensity ) + ( _RainbowSpeed * temp_output_40_0_g11808 ) ),1.0,1.0) );
				float3 hsvTorgb36_g11812 = RGBToHSV( hsvTorgb3_g11815 );
				float3 hsvTorgb37_g11812 = HSVToRGB( float3(hsvTorgb36_g11812.x,_RainbowSaturation,( hsvTorgb36_g11812.z * _RainbowBrightness )) );
				float4 temp_output_1_0_g11812 = staticSwitch32_g11808;
				float4 break2_g11814 = temp_output_1_0_g11812;
				float saferPower24_g11812 = abs( ( ( break2_g11814.x + break2_g11814.x + break2_g11814.y + break2_g11814.y + break2_g11814.y + break2_g11814.z ) / 6.0 ) );
				float4 appendResult29_g11812 = (float4(( ( hsvTorgb37_g11812 * pow( saferPower24_g11812 , max( _RainbowContrast , 0.001 ) ) * _RainbowFade ) + (temp_output_1_0_g11812).rgb ) , temp_output_1_0_g11812.a));
				float4 staticSwitch34_g11808 = appendResult29_g11812;
				#else
				float4 staticSwitch34_g11808 = staticSwitch32_g11808;
				#endif
				float4 temp_output_1_0_g11809 = staticSwitch34_g11808;
				float3 temp_output_57_0_g11809 = (temp_output_1_0_g11809).rgb;
				float4 break2_g11810 = temp_output_1_0_g11809;
				float3 temp_cast_68 = (( ( break2_g11810.x + break2_g11810.x + break2_g11810.y + break2_g11810.y + break2_g11810.y + break2_g11810.z ) / 6.0 )).xxx;
				float3 lerpResult92_g11809 = lerp( temp_cast_68 , temp_output_57_0_g11809 , _ShineSaturation);
				float3 saferPower83_g11809 = abs( lerpResult92_g11809 );
				float3 temp_cast_69 = (max( _ShineContrast , 0.001 )).xxx;
				float3 rotatedValue69_g11809 = RotateAroundAxis( float3( 0,0,0 ), float3( ( _ShineFrequency * temp_output_41_0_g11808 ) ,  0.0 ), float3( 0,0,1 ), ( ( _ShineRotation / 180.0 ) * UNITY_PI ) );
				float temp_output_103_0_g11809 = ( _ShineFrequency * _ShineWidth );
				float clampResult80_g11809 = clamp( ( ( sin( ( rotatedValue69_g11809.x - ( temp_output_40_0_g11808 * _ShineSpeed * _ShineFrequency ) ) ) - ( 1.0 - temp_output_103_0_g11809 ) ) / temp_output_103_0_g11809 ) , 0.0 , 1.0 );
				#ifdef _SHINEMASKTOGGLE_ON
				float2 uv_ShineMask = IN.ase_texcoord9.xy * _ShineMask_ST.xy + _ShineMask_ST.zw;
				float4 tex2DNode3_g11811 = tex2D( _ShineMask, uv_ShineMask );
				float staticSwitch98_g11809 = ( _ShineFade * ( tex2DNode3_g11811.r * tex2DNode3_g11811.a ) );
				#else
				float staticSwitch98_g11809 = _ShineFade;
				#endif
				#ifdef _ENABLESHINE_ON
				float4 appendResult8_g11809 = (float4(( temp_output_57_0_g11809 + ( ( pow( saferPower83_g11809 , temp_cast_69 ) * (_ShineColor).rgb ) * clampResult80_g11809 * staticSwitch98_g11809 ) ) , (temp_output_1_0_g11809).a));
				float4 staticSwitch36_g11808 = appendResult8_g11809;
				#else
				float4 staticSwitch36_g11808 = staticSwitch34_g11808;
				#endif
				#ifdef _ENABLEPOISON_ON
				float temp_output_41_0_g11839 = temp_output_40_0_g11808;
				float linValue16_g11841 = tex2D( _UberNoiseTexture, ( ( ( temp_output_41_0_g11839 * _PoisonNoiseSpeed ) + temp_output_41_0_g11808 ) * _PoisonNoiseScale ) ).r;
				float localMyCustomExpression16_g11841 = MyCustomExpression16_g11841( linValue16_g11841 );
				float saferPower19_g11839 = abs( abs( ( ( ( localMyCustomExpression16_g11841 + ( temp_output_41_0_g11839 * _PoisonShiftSpeed ) ) % 1.0 ) + -0.5 ) ) );
				float3 temp_output_24_0_g11839 = (_PoisonColor).rgb;
				float4 temp_output_1_0_g11839 = staticSwitch36_g11808;
				float3 temp_output_28_0_g11839 = (temp_output_1_0_g11839).rgb;
				float4 break2_g11840 = float4( temp_output_28_0_g11839 , 0.0 );
				float3 lerpResult32_g11839 = lerp( temp_output_28_0_g11839 , ( temp_output_24_0_g11839 * ( ( break2_g11840.x + break2_g11840.x + break2_g11840.y + break2_g11840.y + break2_g11840.y + break2_g11840.z ) / 6.0 ) ) , ( _PoisonFade * _PoisonRecolorFactor ));
				float4 appendResult27_g11839 = (float4(( ( max( pow( saferPower19_g11839 , _PoisonDensity ) , 0.0 ) * temp_output_24_0_g11839 * _PoisonFade * _PoisonNoiseBrightness ) + lerpResult32_g11839 ) , temp_output_1_0_g11839.a));
				float4 staticSwitch39_g11808 = appendResult27_g11839;
				#else
				float4 staticSwitch39_g11808 = staticSwitch36_g11808;
				#endif
				float4 temp_output_10_0_g11842 = staticSwitch39_g11808;
				float3 temp_output_12_0_g11842 = (temp_output_10_0_g11842).rgb;
				float2 temp_output_2_0_g11842 = temp_output_41_0_g11808;
				float temp_output_1_0_g11842 = temp_output_40_0_g11808;
				float2 temp_output_6_0_g11842 = ( temp_output_1_0_g11842 * _EnchantedSpeed );
				float linValue16_g11845 = tex2D( _UberNoiseTexture, ( ( ( temp_output_2_0_g11842 - ( ( temp_output_6_0_g11842 + float2( 1.234,5.6789 ) ) * float2( 0.95,1.05 ) ) ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11845 = MyCustomExpression16_g11845( linValue16_g11845 );
				float linValue16_g11843 = tex2D( _UberNoiseTexture, ( ( ( temp_output_6_0_g11842 + temp_output_2_0_g11842 ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11843 = MyCustomExpression16_g11843( linValue16_g11843 );
				float temp_output_36_0_g11842 = ( localMyCustomExpression16_g11845 + localMyCustomExpression16_g11843 );
				float temp_output_43_0_g11842 = ( temp_output_36_0_g11842 * 0.5 );
				float3 lerpResult42_g11842 = lerp( (_EnchantedLowColor).rgb , (_EnchantedHighColor).rgb , temp_output_43_0_g11842);
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
				float3 hsvTorgb53_g11842 = HSVToRGB( float3(( ( temp_output_43_0_g11842 * _EnchantedRainbowDensity ) + ( _EnchantedRainbowSpeed * temp_output_1_0_g11842 ) ),_EnchantedRainbowSaturation,1.0) );
				float3 staticSwitch50_g11842 = hsvTorgb53_g11842;
				#else
				float3 staticSwitch50_g11842 = lerpResult42_g11842;
				#endif
				float4 break2_g11844 = temp_output_10_0_g11842;
				float saferPower24_g11842 = abs( ( ( break2_g11844.x + break2_g11844.x + break2_g11844.y + break2_g11844.y + break2_g11844.y + break2_g11844.z ) / 6.0 ) );
				float3 temp_output_40_0_g11842 = ( staticSwitch50_g11842 * pow( saferPower24_g11842 , _EnchantedContrast ) * _EnchantedBrightness );
				float temp_output_45_0_g11842 = ( max( ( temp_output_36_0_g11842 - _EnchantedReduce ) , 0.0 ) * _EnchantedFade );
				#ifdef _ENCHANTEDLERPTOGGLE_ON
				float3 lerpResult44_g11842 = lerp( temp_output_12_0_g11842 , temp_output_40_0_g11842 , temp_output_45_0_g11842);
				float3 staticSwitch47_g11842 = lerpResult44_g11842;
				#else
				float3 staticSwitch47_g11842 = ( temp_output_12_0_g11842 + ( temp_output_40_0_g11842 * temp_output_45_0_g11842 ) );
				#endif
				#ifdef _ENABLEENCHANTED_ON
				float4 appendResult19_g11842 = (float4(staticSwitch47_g11842 , temp_output_10_0_g11842.a));
				float4 staticSwitch11_g11842 = appendResult19_g11842;
				#else
				float4 staticSwitch11_g11842 = temp_output_10_0_g11842;
				#endif
				float4 temp_output_1_0_g11846 = staticSwitch11_g11842;
				float4 break5_g11846 = temp_output_1_0_g11846;
				float3 appendResult32_g11846 = (float3(break5_g11846.r , break5_g11846.g , break5_g11846.b));
				float4 break2_g11847 = temp_output_1_0_g11846;
				float temp_output_4_0_g11846 = ( ( break2_g11847.x + break2_g11847.x + break2_g11847.y + break2_g11847.y + break2_g11847.y + break2_g11847.z ) / 6.0 );
				float temp_output_11_0_g11846 = ( ( ( temp_output_4_0_g11846 + ( temp_output_40_0_g11808 * _ShiftingSpeed ) ) * _ShiftingDensity ) % 1.0 );
				float3 lerpResult20_g11846 = lerp( (_ShiftingColorA).rgb , (_ShiftingColorB).rgb , ( abs( ( temp_output_11_0_g11846 - 0.5 ) ) * 2.0 ));
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
				float3 hsvTorgb12_g11846 = HSVToRGB( float3(temp_output_11_0_g11846,_ShiftingSaturation,_ShiftingBrightness) );
				float3 staticSwitch26_g11846 = hsvTorgb12_g11846;
				#else
				float3 staticSwitch26_g11846 = ( lerpResult20_g11846 * _ShiftingBrightness );
				#endif
				#ifdef _ENABLESHIFTING_ON
				float3 lerpResult31_g11846 = lerp( appendResult32_g11846 , ( staticSwitch26_g11846 * pow( temp_output_4_0_g11846 , _ShiftingContrast ) ) , _ShiftingFade);
				float4 appendResult6_g11846 = (float4(lerpResult31_g11846 , break5_g11846.a));
				float4 staticSwitch33_g11846 = appendResult6_g11846;
				#else
				float4 staticSwitch33_g11846 = temp_output_1_0_g11846;
				#endif
				float4 temp_output_473_0 = staticSwitch33_g11846;
				#ifdef _ENABLEFULLDISTORTION_ON
				float4 break4_g11848 = temp_output_473_0;
				float fullDistortionAlpha164 = _FullDistortionFade;
				float4 appendResult5_g11848 = (float4(break4_g11848.r , break4_g11848.g , break4_g11848.b , ( break4_g11848.a * fullDistortionAlpha164 )));
				float4 staticSwitch77 = appendResult5_g11848;
				#else
				float4 staticSwitch77 = temp_output_473_0;
				#endif
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float4 break4_g11849 = staticSwitch77;
				float directionalDistortionAlpha167 = (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 ));
				float4 appendResult5_g11849 = (float4(break4_g11849.r , break4_g11849.g , break4_g11849.b , ( break4_g11849.a * directionalDistortionAlpha167 )));
				float4 staticSwitch75 = appendResult5_g11849;
				#else
				float4 staticSwitch75 = staticSwitch77;
				#endif
				float4 temp_output_1_0_g11850 = staticSwitch75;
				float4 temp_output_1_0_g11851 = temp_output_1_0_g11850;
				float temp_output_53_0_g11851 = max( _FullAlphaDissolveWidth , 0.001 );
				float2 temp_output_18_0_g11850 = shaderPosition235;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
				float linValue16_g11852 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11852 = MyCustomExpression16_g11852( linValue16_g11852 );
				float clampResult17_g11851 = clamp( ( ( ( _FullAlphaDissolveFade * ( 1.0 + temp_output_53_0_g11851 ) ) - localMyCustomExpression16_g11852 ) / temp_output_53_0_g11851 ) , 0.0 , 1.0 );
				float4 appendResult3_g11851 = (float4((temp_output_1_0_g11851).rgb , ( temp_output_1_0_g11851.a * clampResult17_g11851 )));
				float4 staticSwitch3_g11850 = appendResult3_g11851;
				#else
				float4 staticSwitch3_g11850 = temp_output_1_0_g11850;
				#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
				float linValue16_g11860 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11860 = MyCustomExpression16_g11860( linValue16_g11860 );
				float temp_output_5_0_g11859 = localMyCustomExpression16_g11860;
				float temp_output_61_0_g11859 = step( temp_output_5_0_g11859 , _FullGlowDissolveFade );
				float temp_output_53_0_g11859 = max( ( _FullGlowDissolveFade * _FullGlowDissolveWidth ) , 0.001 );
				float4 temp_output_1_0_g11859 = staticSwitch3_g11850;
				float4 appendResult3_g11859 = (float4(( ( (_FullGlowDissolveEdgeColor).rgb * ( temp_output_61_0_g11859 - step( temp_output_5_0_g11859 , ( ( _FullGlowDissolveFade * ( 1.01 + temp_output_53_0_g11859 ) ) - temp_output_53_0_g11859 ) ) ) ) + (temp_output_1_0_g11859).rgb ) , ( temp_output_1_0_g11859.a * temp_output_61_0_g11859 )));
				float4 staticSwitch5_g11850 = appendResult3_g11859;
				#else
				float4 staticSwitch5_g11850 = staticSwitch3_g11850;
				#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
				float4 temp_output_1_0_g11861 = staticSwitch5_g11850;
				float2 temp_output_76_0_g11861 = temp_output_18_0_g11850;
				float linValue16_g11862 = tex2D( _UberNoiseTexture, ( temp_output_76_0_g11861 * _SourceAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11862 = MyCustomExpression16_g11862( linValue16_g11862 );
				float clampResult17_g11861 = clamp( ( ( _SourceAlphaDissolveFade - ( distance( _SourceAlphaDissolvePosition , temp_output_76_0_g11861 ) + ( localMyCustomExpression16_g11862 * _SourceAlphaDissolveNoiseFactor ) ) ) / max( _SourceAlphaDissolveWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11861 = (float4((temp_output_1_0_g11861).rgb , ( temp_output_1_0_g11861.a * (( _SourceAlphaDissolveInvert )?( ( 1.0 - clampResult17_g11861 ) ):( clampResult17_g11861 )) )));
				float4 staticSwitch8_g11850 = appendResult3_g11861;
				#else
				float4 staticSwitch8_g11850 = staticSwitch5_g11850;
				#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
				float2 temp_output_90_0_g11857 = temp_output_18_0_g11850;
				float linValue16_g11858 = tex2D( _UberNoiseTexture, ( temp_output_90_0_g11857 * _SourceGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11858 = MyCustomExpression16_g11858( linValue16_g11858 );
				float temp_output_65_0_g11857 = ( distance( _SourceGlowDissolvePosition , temp_output_90_0_g11857 ) + ( localMyCustomExpression16_g11858 * _SourceGlowDissolveNoiseFactor ) );
				float temp_output_75_0_g11857 = step( temp_output_65_0_g11857 , _SourceGlowDissolveFade );
				float temp_output_76_0_g11857 = step( temp_output_65_0_g11857 , ( _SourceGlowDissolveFade - max( _SourceGlowDissolveWidth , 0.001 ) ) );
				float4 temp_output_1_0_g11857 = staticSwitch8_g11850;
				float4 appendResult3_g11857 = (float4(( ( max( ( temp_output_75_0_g11857 - temp_output_76_0_g11857 ) , 0.0 ) * (_SourceGlowDissolveEdgeColor).rgb ) + (temp_output_1_0_g11857).rgb ) , ( temp_output_1_0_g11857.a * (( _SourceGlowDissolveInvert )?( ( 1.0 - temp_output_76_0_g11857 ) ):( temp_output_75_0_g11857 )) )));
				float4 staticSwitch9_g11850 = appendResult3_g11857;
				#else
				float4 staticSwitch9_g11850 = staticSwitch8_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
				float4 temp_output_1_0_g11853 = staticSwitch9_g11850;
				float2 temp_output_161_0_g11853 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11853 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_161_0_g11853 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalAlphaFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11853 = rotatedValue136_g11853;
				float linValue16_g11854 = tex2D( _UberNoiseTexture, ( temp_output_161_0_g11853 * _DirectionalAlphaFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11854 = MyCustomExpression16_g11854( linValue16_g11854 );
				float clampResult154_g11853 = clamp( ( ( break130_g11853.x + break130_g11853.y + _DirectionalAlphaFadeFade + ( localMyCustomExpression16_g11854 * _DirectionalAlphaFadeNoiseFactor ) ) / max( _DirectionalAlphaFadeWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11853 = (float4((temp_output_1_0_g11853).rgb , ( temp_output_1_0_g11853.a * (( _DirectionalAlphaFadeInvert )?( ( 1.0 - clampResult154_g11853 ) ):( clampResult154_g11853 )) )));
				float4 staticSwitch11_g11850 = appendResult3_g11853;
				#else
				float4 staticSwitch11_g11850 = staticSwitch9_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
				float2 temp_output_171_0_g11855 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11855 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_171_0_g11855 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalGlowFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11855 = rotatedValue136_g11855;
				float linValue16_g11856 = tex2D( _UberNoiseTexture, ( temp_output_171_0_g11855 * _DirectionalGlowFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11856 = MyCustomExpression16_g11856( linValue16_g11856 );
				float temp_output_168_0_g11855 = max( ( ( break130_g11855.x + break130_g11855.y + _DirectionalGlowFadeFade + ( localMyCustomExpression16_g11856 * _DirectionalGlowFadeNoiseFactor ) ) / max( _DirectionalGlowFadeWidth , 0.001 ) ) , 0.0 );
				float temp_output_161_0_g11855 = step( 0.1 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) );
				float4 temp_output_1_0_g11855 = staticSwitch11_g11850;
				float clampResult154_g11855 = clamp( temp_output_161_0_g11855 , 0.0 , 1.0 );
				float4 appendResult3_g11855 = (float4(( ( (_DirectionalGlowFadeEdgeColor).rgb * ( temp_output_161_0_g11855 - step( 1.0 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) ) ) ) + (temp_output_1_0_g11855).rgb ) , ( temp_output_1_0_g11855.a * clampResult154_g11855 )));
				float4 staticSwitch15_g11850 = appendResult3_g11855;
				#else
				float4 staticSwitch15_g11850 = staticSwitch11_g11850;
				#endif
				float4 temp_output_1_0_g11863 = staticSwitch15_g11850;
				float2 temp_output_126_0_g11863 = temp_output_18_0_g11850;
				float temp_output_121_0_g11863 = max( ( ( _HalftoneFade - distance( _HalftonePosition , temp_output_126_0_g11863 ) ) / max( 0.01 , _HalftoneFadeWidth ) ) , 0.0 );
				float2 appendResult11_g11864 = (float2(temp_output_121_0_g11863 , temp_output_121_0_g11863));
				float temp_output_17_0_g11864 = length( ( (( ( abs( temp_output_126_0_g11863 ) * _HalftoneTiling ) % float2( 1,1 ) )*2.0 + -1.0) / appendResult11_g11864 ) );
				#ifdef _ENABLEHALFTONE_ON
				float clampResult17_g11863 = clamp( saturate( ( ( 1.0 - temp_output_17_0_g11864 ) / fwidth( temp_output_17_0_g11864 ) ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11863 = (float4((temp_output_1_0_g11863).rgb , ( temp_output_1_0_g11863.a * (( _HalftoneInvert )?( ( 1.0 - clampResult17_g11863 ) ):( clampResult17_g11863 )) )));
				float4 staticSwitch13_g11850 = appendResult3_g11863;
				#else
				float4 staticSwitch13_g11850 = staticSwitch15_g11850;
				#endif
				float3 temp_output_3_0_g11866 = (_AddColorColor).rgb;
				#ifdef _ADDCOLORMASKTOGGLE_ON
				float2 uv_AddColorMask = IN.ase_texcoord9.xy * _AddColorMask_ST.xy + _AddColorMask_ST.zw;
				float4 tex2DNode19_g11866 = tex2D( _AddColorMask, uv_AddColorMask );
				float3 staticSwitch16_g11866 = ( temp_output_3_0_g11866 * ( (tex2DNode19_g11866).rgb * tex2DNode19_g11866.a ) );
				#else
				float3 staticSwitch16_g11866 = temp_output_3_0_g11866;
				#endif
				float4 temp_output_1_0_g11866 = staticSwitch13_g11850;
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
				float4 break2_g11868 = temp_output_1_0_g11866;
				float temp_output_9_0_g11867 = max( _AddColorContrast , 0.0 );
				float saferPower7_g11867 = abs( ( ( ( break2_g11868.x + break2_g11868.x + break2_g11868.y + break2_g11868.y + break2_g11868.y + break2_g11868.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11867 ) , 0.0 ) ) ) );
				float3 staticSwitch17_g11866 = ( staticSwitch16_g11866 * pow( saferPower7_g11867 , temp_output_9_0_g11867 ) );
				#else
				float3 staticSwitch17_g11866 = staticSwitch16_g11866;
				#endif
				#ifdef _ENABLEADDCOLOR_ON
				float4 appendResult6_g11866 = (float4(( ( staticSwitch17_g11866 * _AddColorFade ) + (temp_output_1_0_g11866).rgb ) , temp_output_1_0_g11866.a));
				float4 staticSwitch5_g11865 = appendResult6_g11866;
				#else
				float4 staticSwitch5_g11865 = staticSwitch13_g11850;
				#endif
				#ifdef _ENABLEALPHATINT_ON
				float4 temp_output_1_0_g11869 = staticSwitch5_g11865;
				float3 lerpResult4_g11869 = lerp( (temp_output_1_0_g11869).rgb , (_AlphaTintColor).rgb , ( ( 1.0 - temp_output_1_0_g11869.a ) * step( _AlphaTintMinAlpha , temp_output_1_0_g11869.a ) * _AlphaTintFade ));
				float4 appendResult13_g11869 = (float4(lerpResult4_g11869 , temp_output_1_0_g11869.a));
				float4 staticSwitch11_g11865 = appendResult13_g11869;
				#else
				float4 staticSwitch11_g11865 = staticSwitch5_g11865;
				#endif
				float4 temp_output_1_0_g11870 = staticSwitch11_g11865;
				float3 temp_output_6_0_g11870 = (_StrongTintTint).rgb;
				#ifdef _STRONGTINTMASKTOGGLE_ON
				float2 uv_StrongTintMask = IN.ase_texcoord9.xy * _StrongTintMask_ST.xy + _StrongTintMask_ST.zw;
				float4 tex2DNode23_g11870 = tex2D( _StrongTintMask, uv_StrongTintMask );
				float3 staticSwitch21_g11870 = ( temp_output_6_0_g11870 * ( (tex2DNode23_g11870).rgb * tex2DNode23_g11870.a ) );
				#else
				float3 staticSwitch21_g11870 = temp_output_6_0_g11870;
				#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
				float4 break2_g11872 = temp_output_1_0_g11870;
				float temp_output_9_0_g11871 = max( _StrongTintContrast , 0.0 );
				float saferPower7_g11871 = abs( ( ( ( break2_g11872.x + break2_g11872.x + break2_g11872.y + break2_g11872.y + break2_g11872.y + break2_g11872.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11871 ) , 0.0 ) ) ) );
				float3 staticSwitch22_g11870 = ( pow( saferPower7_g11871 , temp_output_9_0_g11871 ) * staticSwitch21_g11870 );
				#else
				float3 staticSwitch22_g11870 = staticSwitch21_g11870;
				#endif
				#ifdef _ENABLESTRONGTINT_ON
				float3 lerpResult7_g11870 = lerp( (temp_output_1_0_g11870).rgb , staticSwitch22_g11870 , _StrongTintFade);
				float4 appendResult9_g11870 = (float4(lerpResult7_g11870 , (temp_output_1_0_g11870).a));
				float4 staticSwitch7_g11865 = appendResult9_g11870;
				#else
				float4 staticSwitch7_g11865 = staticSwitch11_g11865;
				#endif
				float4 temp_output_2_0_g11873 = staticSwitch7_g11865;
				#ifdef _ENABLESHADOW_ON
				float4 break4_g11875 = temp_output_2_0_g11873;
				float3 appendResult5_g11875 = (float3(break4_g11875.r , break4_g11875.g , break4_g11875.b));
				float2 appendResult2_g11874 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float4 appendResult85_g11873 = (float4(_ShadowColor.r , _ShadowColor.g , _ShadowColor.b , ( _ShadowFade * tex2D( _MainTex, ( finalUV146 - ( ( 100.0 / appendResult2_g11874 ) * _ShadowOffset ) ) ).a )));
				float4 break6_g11875 = appendResult85_g11873;
				float3 appendResult7_g11875 = (float3(break6_g11875.r , break6_g11875.g , break6_g11875.b));
				float temp_output_11_0_g11875 = ( ( 1.0 - break4_g11875.a ) * break6_g11875.a );
				float temp_output_32_0_g11875 = ( break4_g11875.a + temp_output_11_0_g11875 );
				float4 appendResult18_g11875 = (float4(( ( ( appendResult5_g11875 * break4_g11875.a ) + ( appendResult7_g11875 * temp_output_11_0_g11875 ) ) * ( 1.0 / max( temp_output_32_0_g11875 , 0.01 ) ) ) , temp_output_32_0_g11875));
				float4 staticSwitch82_g11873 = appendResult18_g11875;
				#else
				float4 staticSwitch82_g11873 = temp_output_2_0_g11873;
				#endif
				float4 break4_g11876 = staticSwitch82_g11873;
				#ifdef _ENABLECUSTOMFADE_ON
				float staticSwitch8_g11756 = 1.0;
				#else
				float staticSwitch8_g11756 = IN.ase_color.a;
				#endif
				#ifdef _ENABLESMOKE_ON
				float staticSwitch9_g11756 = 1.0;
				#else
				float staticSwitch9_g11756 = staticSwitch8_g11756;
				#endif
				float customVertexAlpha193 = staticSwitch9_g11756;
				float4 appendResult5_g11876 = (float4(break4_g11876.r , break4_g11876.g , break4_g11876.b , ( break4_g11876.a * customVertexAlpha193 )));
				float4 temp_output_344_0 = appendResult5_g11876;
				float4 temp_output_1_0_g11877 = temp_output_344_0;
				float4 appendResult8_g11877 = (float4(( (temp_output_1_0_g11877).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11877.a));
				#ifdef _VERTEXTINTFIRST_ON
				float4 staticSwitch342 = temp_output_344_0;
				#else
				float4 staticSwitch342 = appendResult8_g11877;
				#endif
				float4 lerpResult125 = lerp( ( originalColor191 * IN.ase_color ) , staticSwitch342 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float4 staticSwitch143 = staticSwitch342;
				#elif defined(_SHADERFADING_FULL)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_MASK)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_DISSOLVE)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_SPREAD)
				float4 staticSwitch143 = lerpResult125;
				#else
				float4 staticSwitch143 = staticSwitch342;
				#endif
				float4 temp_output_7_0_g11884 = staticSwitch143;
				#ifdef _BAKEDMATERIAL_ON
				float4 appendResult2_g11884 = (float4(( (temp_output_7_0_g11884).rgb / max( temp_output_7_0_g11884.a , 1E-05 ) ) , temp_output_7_0_g11884.a));
				float4 staticSwitch6_g11884 = appendResult2_g11884;
				#else
				float4 staticSwitch6_g11884 = temp_output_7_0_g11884;
				#endif
				float4 temp_output_340_0 = staticSwitch6_g11884;
				
				float2 temp_output_11_0_g11885 = finalUV146;
				
				#ifdef _EMISSIONTOGGLE_ON
				float3 appendResult20_g11885 = (float3(_EmissionTint.r , _EmissionTint.g , _EmissionTint.b));
				float2 uv_EmissionMap = IN.ase_texcoord9.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
				float4 tex2DNode17_g11885 = tex2D( _EmissionMap, uv_EmissionMap );
				float3 appendResult18_g11885 = (float3(tex2DNode17_g11885.r , tex2DNode17_g11885.g , tex2DNode17_g11885.b));
				float3 staticSwitch13_g11885 = ( appendResult20_g11885 * appendResult18_g11885 * tex2DNode17_g11885.a );
				#else
				float3 staticSwitch13_g11885 = float3(0,0,0);
				#endif
				
				float4 tex2DNode7_g11885 = tex2D( _MetallicMap, temp_output_11_0_g11885 );
				#ifdef _METALLICMAPTOGGLE_ON
				float staticSwitch23_g11885 = ( tex2DNode7_g11885.r * _Metallic );
				#else
				float staticSwitch23_g11885 = _Metallic;
				#endif
				
				#ifdef _METALLICMAPTOGGLE_ON
				float staticSwitch22_g11885 = ( _Smoothness * tex2DNode7_g11885.r );
				#else
				float staticSwitch22_g11885 = _Smoothness;
				#endif
				
				o.Albedo = temp_output_340_0.rgb;
				o.Normal = UnpackScaleNormal( tex2D( _NormalMap, temp_output_11_0_g11885 ), _NormalIntensity );
				o.Emission = staticSwitch13_g11885;
				#if defined(_SPECULAR_SETUP)
					o.Specular = fixed3( 0, 0, 0 );
				#else
					o.Metallic = staticSwitch23_g11885;
				#endif
				o.Smoothness = staticSwitch22_g11885;
				o.Occlusion = 1;
				o.Alpha = temp_output_340_0.a;
				float AlphaClipThreshold = _AlphaClip;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;				

				#ifdef _ALPHATEST_ON
					clip( o.Alpha - AlphaClipThreshold );
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = IN.pos.z;
				#endif

				#ifndef USING_DIRECTIONAL_LIGHT
					fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				#else
					fixed3 lightDir = _WorldSpaceLightPos0.xyz;
				#endif

				fixed4 c = 0;
				float3 worldN;
				worldN.x = dot(IN.tSpace0.xyz, o.Normal);
				worldN.y = dot(IN.tSpace1.xyz, o.Normal);
				worldN.z = dot(IN.tSpace2.xyz, o.Normal);
				worldN = normalize(worldN);
				o.Normal = worldN;

				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
				gi.indirect.diffuse = 0;
				gi.indirect.specular = 0;
				gi.light.color = _LightColor0.rgb;
				gi.light.dir = lightDir;

				UnityGIInput giInput;
				UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
				giInput.light = gi.light;
				giInput.worldPos = worldPos;
				giInput.worldViewDir = worldViewDir;
				giInput.atten = atten;
				#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
					giInput.lightmapUV = IN.lmap;
				#else
					giInput.lightmapUV = 0.0;
				#endif
				#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
					giInput.ambient = IN.sh;
				#else
					giInput.ambient.rgb = 0.0;
				#endif
				giInput.probeHDR[0] = unity_SpecCube0_HDR;
				giInput.probeHDR[1] = unity_SpecCube1_HDR;
				#if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
					giInput.boxMin[0] = unity_SpecCube0_BoxMin;
				#endif
				#ifdef UNITY_SPECCUBE_BOX_PROJECTION
					giInput.boxMax[0] = unity_SpecCube0_BoxMax;
					giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
					giInput.boxMax[1] = unity_SpecCube1_BoxMax;
					giInput.boxMin[1] = unity_SpecCube1_BoxMin;
					giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
				#endif
				
				#if defined(_SPECULAR_SETUP)
					LightingStandardSpecular_GI(o, giInput, gi);
				#else
					LightingStandard_GI( o, giInput, gi );
				#endif

				#ifdef ASE_BAKEDGI
					gi.indirect.diffuse = BakedGI;
				#endif

				#if UNITY_SHOULD_SAMPLE_SH && !defined(LIGHTMAP_ON) && defined(ASE_NO_AMBIENT)
					gi.indirect.diffuse = 0;
				#endif

				#if defined(_SPECULAR_SETUP)
					c += LightingStandardSpecular (o, worldViewDir, gi);
				#else
					c += LightingStandard( o, worldViewDir, gi );
				#endif
				
				#ifdef _TRANSMISSION_ASE
				{
					float shadow = _TransmissionShadow;
					#ifdef DIRECTIONAL
						float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
					#else
						float3 lightAtten = gi.light.color;
					#endif
					half3 transmission = max(0 , -dot(o.Normal, gi.light.dir)) * lightAtten * Transmission;
					c.rgb += o.Albedo * transmission;
				}
				#endif

				#ifdef _TRANSLUCENCY_ASE
				{
					float shadow = _TransShadow;
					float normal = _TransNormal;
					float scattering = _TransScattering;
					float direct = _TransDirect;
					float ambient = _TransAmbient;
					float strength = _TransStrength;

					#ifdef DIRECTIONAL
						float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
					#else
						float3 lightAtten = gi.light.color;
					#endif
					half3 lightDir = gi.light.dir + o.Normal * normal;
					half transVdotL = pow( saturate( dot( worldViewDir, -lightDir ) ), scattering );
					half3 translucency = lightAtten * (transVdotL * direct + gi.indirect.diffuse * ambient) * Translucency;
					c.rgb += o.Albedo * translucency * strength;
				}
				#endif

				//#ifdef _REFRACTION_ASE
				//	float4 projScreenPos = ScreenPos / ScreenPos.w;
				//	float3 refractionOffset = ( RefractionIndex - 1.0 ) * mul( UNITY_MATRIX_V, WorldNormal ).xyz * ( 1.0 - dot( WorldNormal, WorldViewDirection ) );
				//	projScreenPos.xy += refractionOffset.xy;
				//	float3 refraction = UNITY_SAMPLE_SCREENSPACE_TEXTURE( _GrabTexture, projScreenPos ) * RefractionColor;
				//	color.rgb = lerp( refraction, color.rgb, color.a );
				//	color.a = 1;
				//#endif

				c.rgb += o.Emission;

				#ifdef ASE_FOG
					UNITY_APPLY_FOG(IN.fogCoord, c);
				#endif
				return c;
			}
			ENDCG
		}

		
		Pass
		{
			
			Name "ForwardAdd"
			Tags { "LightMode"="ForwardAdd" }
			ZWrite Off
			Blend One One

			CGPROGRAM
			#define ASE_NEEDS_FRAG_SHADOWCOORDS
			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _ALPHATEST_ON 1

			#pragma vertex vert
			#pragma fragment frag
			#pragma skip_variants INSTANCING_ON
			#pragma multi_compile_fwdadd_fullshadows
			#ifndef UNITY_PASS_FORWARDADD
				#define UNITY_PASS_FORWARDADD
			#endif
			#include "HLSLSupport.cginc"
			#if !defined( UNITY_INSTANCED_LOD_FADE )
				#define UNITY_INSTANCED_LOD_FADE
			#endif
			#if !defined( UNITY_INSTANCED_SH )
				#define UNITY_INSTANCED_SH
			#endif
			#if !defined( UNITY_INSTANCED_LIGHTMAPSTS )
				#define UNITY_INSTANCED_LIGHTMAPSTS
			#endif
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			#include "AutoLight.cginc"

			#include "UnityStandardUtils.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_SCREEN_POSITION
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_FRAG_POSITION
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _SHADERFADING_NONE _SHADERFADING_FULL _SHADERFADING_MASK _SHADERFADING_DISSOLVE _SHADERFADING_SPREAD
			#pragma shader_feature_local _ENABLESINESCALE_ON
			#pragma shader_feature _ENABLEVIBRATE_ON
			#pragma shader_feature _ENABLESINEMOVE_ON
			#pragma shader_feature _ENABLESQUISH_ON
			#pragma shader_feature _SPRITESHEETFIX_ON
			#pragma shader_feature_local _PIXELPERFECTUV_ON
			#pragma shader_feature _ENABLEWORLDTILING_ON
			#pragma shader_feature _ENABLESCREENTILING_ON
			#pragma shader_feature _TOGGLETIMEFREQUENCY_ON
			#pragma shader_feature _TOGGLETIMEFPS_ON
			#pragma shader_feature _TOGGLETIMESPEED_ON
			#pragma shader_feature _TOGGLEUNSCALEDTIME_ON
			#pragma shader_feature _TOGGLECUSTOMTIME_ON
			#pragma shader_feature _SHADERSPACE_UV _SHADERSPACE_UV_RAW _SHADERSPACE_OBJECT _SHADERSPACE_OBJECT_SCALED _SHADERSPACE_WORLD _SHADERSPACE_UI_GRAPHIC _SHADERSPACE_SCREEN
			#pragma shader_feature _PIXELPERFECTSPACE_ON
			#pragma shader_feature _BAKEDMATERIAL_ON
			#pragma shader_feature _VERTEXTINTFIRST_ON
			#pragma shader_feature _ENABLESHADOW_ON
			#pragma shader_feature _ENABLESTRONGTINT_ON
			#pragma shader_feature _ENABLEALPHATINT_ON
			#pragma shader_feature_local _ENABLEADDCOLOR_ON
			#pragma shader_feature_local _ENABLEHALFTONE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALGLOWFADE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALALPHAFADE_ON
			#pragma shader_feature_local _ENABLESOURCEGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLESOURCEALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALDISTORTION_ON
			#pragma shader_feature_local _ENABLEFULLDISTORTION_ON
			#pragma shader_feature _ENABLESHIFTING_ON
			#pragma shader_feature _ENABLEENCHANTED_ON
			#pragma shader_feature_local _ENABLEPOISON_ON
			#pragma shader_feature_local _ENABLESHINE_ON
			#pragma shader_feature_local _ENABLERAINBOW_ON
			#pragma shader_feature_local _ENABLEBURN_ON
			#pragma shader_feature_local _ENABLEFROZEN_ON
			#pragma shader_feature_local _ENABLEMETAL_ON
			#pragma shader_feature_local _ENABLECAMOUFLAGE_ON
			#pragma shader_feature_local _ENABLEGLITCH_ON
			#pragma shader_feature_local _ENABLEHOLOGRAM_ON
			#pragma shader_feature _ENABLEPINGPONGGLOW_ON
			#pragma shader_feature_local _ENABLEPIXELOUTLINE_ON
			#pragma shader_feature_local _ENABLEOUTEROUTLINE_ON
			#pragma shader_feature_local _ENABLEINNEROUTLINE_ON
			#pragma shader_feature_local _ENABLESATURATION_ON
			#pragma shader_feature_local _ENABLESINEGLOW_ON
			#pragma shader_feature_local _ENABLEADDHUE_ON
			#pragma shader_feature_local _ENABLESHIFTHUE_ON
			#pragma shader_feature_local _ENABLEINKSPREAD_ON
			#pragma shader_feature_local _ENABLEBLACKTINT_ON
			#pragma shader_feature_local _ENABLESPLITTONING_ON
			#pragma shader_feature_local _ENABLEHUE_ON
			#pragma shader_feature_local _ENABLEBRIGHTNESS_ON
			#pragma shader_feature_local _ENABLECONTRAST_ON
			#pragma shader_feature _ENABLENEGATIVE_ON
			#pragma shader_feature_local _ENABLECOLORREPLACE_ON
			#pragma shader_feature_local _ENABLERECOLORRGBYCP_ON
			#pragma shader_feature _ENABLERECOLORRGB_ON
			#pragma shader_feature_local _ENABLEFLAME_ON
			#pragma shader_feature_local _ENABLECHECKERBOARD_ON
			#pragma shader_feature_local _ENABLECUSTOMFADE_ON
			#pragma shader_feature_local _ENABLESMOKE_ON
			#pragma shader_feature _ENABLESHARPEN_ON
			#pragma shader_feature _ENABLEGAUSSIANBLUR_ON
			#pragma shader_feature _ENABLESMOOTHPIXELART_ON
			#pragma shader_feature_local _TILINGFIX_ON
			#pragma shader_feature _ENABLEWIGGLE_ON
			#pragma shader_feature_local _ENABLEUVSCALE_ON
			#pragma shader_feature_local _ENABLEPIXELATE_ON
			#pragma shader_feature_local _ENABLEUVSCROLL_ON
			#pragma shader_feature_local _ENABLEUVROTATE_ON
			#pragma shader_feature_local _ENABLESINEROTATE_ON
			#pragma shader_feature_local _ENABLESQUEEZE_ON
			#pragma shader_feature_local _ENABLEUVDISTORT_ON
			#pragma shader_feature_local _ENABLEWIND_ON
			#pragma shader_feature_local _WINDLOCALWIND_ON
			#pragma shader_feature_local _WINDHIGHQUALITYNOISE_ON
			#pragma shader_feature_local _WINDISPARALLAX_ON
			#pragma shader_feature _UVDISTORTMASKTOGGLE_ON
			#pragma shader_feature _WIGGLEFIXEDGROUNDTOGGLE_ON
			#pragma shader_feature _RECOLORRGBTEXTURETOGGLE_ON
			#pragma shader_feature _RECOLORRGBYCPTEXTURETOGGLE_ON
			#pragma shader_feature_local _ADDHUEMASKTOGGLE_ON
			#pragma shader_feature_local _SINEGLOWMASKTOGGLE_ON
			#pragma shader_feature _INNEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature_local _INNEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _INNEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _OUTEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature_local _OUTEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _PIXELOUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _CAMOUFLAGEANIMATIONTOGGLE_ON
			#pragma shader_feature _METALMASKTOGGLE_ON
			#pragma shader_feature _SHINEMASKTOGGLE_ON
			#pragma shader_feature _ENCHANTEDLERPTOGGLE_ON
			#pragma shader_feature _ENCHANTEDRAINBOWTOGGLE_ON
			#pragma shader_feature _SHIFTINGRAINBOWTOGGLE_ON
			#pragma shader_feature _ADDCOLORCONTRASTTOGGLE_ON
			#pragma shader_feature _ADDCOLORMASKTOGGLE_ON
			#pragma shader_feature _STRONGTINTCONTRASTTOGGLE_ON
			#pragma shader_feature _STRONGTINTMASKTOGGLE_ON
			#pragma shader_feature _EMISSIONTOGGLE_ON
			#pragma shader_feature _METALLICMAPTOGGLE_ON

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct v2f {
				#if UNITY_VERSION >= 201810
					UNITY_POSITION(pos);
				#else
					float4 pos : SV_POSITION;
				#endif
				#if UNITY_VERSION >= 201810 && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_LIGHTING_COORDS(1,2)
				#elif defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if UNITY_VERSION >= 201710
						UNITY_SHADOW_COORDS(1)
					#else
						SHADOW_COORDS(1)
					#endif
				#endif
				#ifdef ASE_FOG
					UNITY_FOG_COORDS(3)
				#endif
				float4 tSpace0 : TEXCOORD5;
				float4 tSpace1 : TEXCOORD6;
				float4 tSpace2 : TEXCOORD7;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 screenPos : TEXCOORD8;
				#endif
				float4 ase_texcoord9 : TEXCOORD9;
				float4 ase_texcoord10 : TEXCOORD10;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#ifdef _TRANSMISSION_ASE
				float _TransmissionShadow;
			#endif
			#ifdef _TRANSLUCENCY_ASE
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishStretch;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingScale;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingOffset;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float _ScreenTilingPixelsPerUnit;
			#endif
			uniform sampler2D _MainTex;
			float4 _MainTex_TexelSize;
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingScale;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingOffset;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float _WorldTilingPixelsPerUnit;
			#endif
			uniform float4 _SpriteSheetRect;
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFade;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFlip;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishSquish;
			#endif
				#ifdef _TOGGLECUSTOMTIME_ON
			uniform float _TimeValue;
			#endif
			uniform float UnscaledTime;
				#ifdef _TOGGLETIMESPEED_ON
			uniform float _TimeSpeed;
			#endif
				#ifdef _TOGGLETIMEFPS_ON
			uniform float _TimeFPS;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeFrequency;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeRange;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveFrequency;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveOffset;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float _SineMoveFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFrequency;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateOffset;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateRotation;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float _SineScaleFrequency;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float2 _SineScaleFactor;
			#endif
			uniform float _FadingFade;
			uniform sampler2D _FadingMask;
			uniform float4 _FadingMask_ST;
			uniform float _FadingWidth;
			uniform sampler2D _UberNoiseTexture;
			uniform float _PixelsPerUnit;
			uniform float _RectWidth;
			uniform float _RectHeight;
			uniform float _ScreenWidthUnits;
			uniform float2 _FadingNoiseScale;
			uniform float2 _FadingPosition;
			uniform float _FadingNoiseFactor;
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotationWindFactor;
			#endif
			uniform float WindMinIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMinIntensity;
			#endif
			uniform float WindMaxIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMaxIntensity;
			#endif
				#ifdef _WINDISPARALLAX_ON
			uniform float _WindXPosition;
			#endif
			uniform float WindNoiseScale;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseScale;
			#endif
			uniform float WindTime;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseSpeed;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindMaxRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindFlip;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishFactor;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishWindFactor;
			#endif
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float _FullDistortionFade;
			#endif
			uniform float2 _FullDistortionNoiseScale;
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float2 _FullDistortionDistortion;
			#endif
			uniform float2 _DirectionalDistortionDistortionScale;
			uniform float _DirectionalDistortionRandomDirection;
			uniform float2 _DirectionalDistortionDistortion;
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
			uniform float _DirectionalDistortionInvert;
			#endif
			uniform float _DirectionalDistortionRotation;
			uniform float _DirectionalDistortionFade;
			uniform float2 _DirectionalDistortionNoiseScale;
			uniform float _DirectionalDistortionNoiseFactor;
			uniform float _DirectionalDistortionWidth;
			uniform float _HologramDistortionSpeed;
			uniform float _HologramDistortionDensity;
			uniform float _HologramDistortionScale;
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramDistortionOffset;
			#endif
			uniform float _HologramFade;
			uniform float2 _GlitchDistortionSpeed;
			uniform float2 _GlitchDistortionScale;
			uniform float2 _GlitchDistortion;
			uniform float2 _GlitchMaskSpeed;
			uniform float2 _GlitchMaskScale;
			uniform float _GlitchMaskMin;
			uniform float _GlitchFade;
			uniform float2 _UVDistortFrom;
			uniform float2 _UVDistortTo;
			uniform float2 _UVDistortSpeed;
			uniform float2 _UVDistortNoiseScale;
			uniform float _UVDistortFade;
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform sampler2D _UVDistortMask;
			#endif
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform float4 _UVDistortMask_ST;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeCenter;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezePower;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeScale;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezeFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFrequency;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateAngle;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float2 _SineRotatePivot;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float _UVRotateSpeed;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float2 _UVRotatePivot;
			#endif
				#ifdef _ENABLEUVSCROLL_ON
			uniform float2 _UVScrollSpeed;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelDensity;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelsPerUnit;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelateFade;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScalePivot;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScaleScale;
			#endif
			uniform float _WiggleFrequency;
			uniform float _WiggleSpeed;
			uniform float _WiggleOffset;
			uniform float _WiggleFade;
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurOffset;
			#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurFade;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenOffset;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFactor;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFade;
			#endif
			uniform float _SmokeVertexSeed;
			uniform float _SmokeNoiseScale;
			uniform float _SmokeNoiseFactor;
			uniform float _SmokeSmoothness;
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeDarkEdge;
			#endif
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeAlpha;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform sampler2D _CustomFadeFadeMask;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float2 _CustomFadeNoiseScale;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeNoiseFactor;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeSmoothness;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeAlpha;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardDarken;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardTiling;
			#endif
			uniform float2 _FlameSpeed;
			uniform float2 _FlameNoiseScale;
			uniform float _FlameNoiseHeightFactor;
			uniform float _FlameNoiseFactor;
			uniform float _FlameRadius;
			uniform float _FlameSmooth;
				#ifdef _ENABLEFLAME_ON
			uniform float _FlameBrightness;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBRedTint;
			#endif
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBTexture;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBGreenTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBBlueTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float _RecolorRGBFade;
			#endif
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBYCPTexture;
			#endif
			uniform float4 _RecolorRGBYCPPurpleTint;
			uniform float4 _RecolorRGBYCPBlueTint;
			uniform float4 _RecolorRGBYCPCyanTint;
			uniform float4 _RecolorRGBYCPGreenTint;
			uniform float4 _RecolorRGBYCPYellowTint;
			uniform float4 _RecolorRGBYCPRedTint;
				#ifdef _ENABLERECOLORRGBYCP_ON
			uniform float _RecolorRGBYCPFade;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceFromColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceContrast;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceToColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceSmoothness;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceRange;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceFade;
			#endif
				#ifdef _ENABLENEGATIVE_ON
			uniform float _NegativeFade;
			#endif
				#ifdef _ENABLECONTRAST_ON
			uniform float _Contrast;
			#endif
				#ifdef _ENABLEBRIGHTNESS_ON
			uniform float _Brightness;
			#endif
				#ifdef _ENABLEHUE_ON
			uniform float _Hue;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningShadowsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningHighlightsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningShift;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningBalance;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningContrast;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningFade;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float4 _BlackTintColor;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintPower;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float4 _InkSpreadColor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadContrast;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadDistance;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadPosition;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadNoiseScale;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadNoiseFactor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadWidth;
			#endif
				#ifdef _ENABLESHIFTHUE_ON
			uniform float _ShiftHueSpeed;
			#endif
			uniform float _AddHueSpeed;
			uniform float _AddHueSaturation;
			uniform float _AddHueBrightness;
				#ifdef _ENABLEADDHUE_ON
			uniform float _AddHueContrast;
			#endif
			uniform float _AddHueFade;
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform sampler2D _AddHueMask;
			#endif
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform float4 _AddHueMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowContrast;
			#endif
			uniform float4 _SineGlowColor;
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform sampler2D _SineGlowMask;
			#endif
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform float4 _SineGlowMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFade;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFrequency;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMax;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMin;
			#endif
				#ifdef _ENABLESATURATION_ON
			uniform float _Saturation;
			#endif
			uniform float4 _InnerOutlineColor;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _InnerOutlineTintTexture;
			#endif
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform float2 _InnerOutlineTextureSpeed;
			#endif
			uniform float _InnerOutlineFade;
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseSpeed;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseScale;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineDistortionIntensity;
			#endif
			uniform float _InnerOutlineWidth;
			uniform float4 _OuterOutlineColor;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _OuterOutlineTintTexture;
			#endif
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform float2 _OuterOutlineTextureSpeed;
			#endif
			uniform float _OuterOutlineFade;
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseSpeed;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseScale;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineDistortionIntensity;
			#endif
			uniform float _OuterOutlineWidth;
			uniform float4 _PixelOutlineColor;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform sampler2D _PixelOutlineTintTexture;
			#endif
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform float2 _PixelOutlineTextureSpeed;
			#endif
			uniform float _PixelOutlineFade;
			uniform float _PixelOutlineWidth;
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowFrom;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowTo;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFrequency;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFade;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float4 _HologramTint;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineSpeed;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineFrequency;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineGap;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramMinAlpha;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchBrightness;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseSpeed;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseScale;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchHueSpeed;
			#endif
			uniform float4 _CamouflageBaseColor;
			uniform float4 _CamouflageColorA;
			uniform float _CamouflageDensityA;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionSpeed;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionScale;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionIntensity;
			#endif
			uniform float2 _CamouflageNoiseScaleA;
			uniform float _CamouflageSmoothnessA;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float4 _CamouflageColorB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageDensityB;
			#endif
			uniform float2 _CamouflageNoiseScaleB;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageSmoothnessB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageContrast;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageFade;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalHighlightDensity;
			#endif
			uniform float2 _MetalNoiseDistortionSpeed;
			uniform float2 _MetalNoiseDistortionScale;
			uniform float2 _MetalNoiseDistortion;
			uniform float2 _MetalNoiseSpeed;
			uniform float2 _MetalNoiseScale;
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalHighlightColor;
			#endif
			uniform float _MetalHighlightContrast;
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalContrast;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalColor;
			#endif
			uniform float _MetalFade;
				#ifdef _METALMASKTOGGLE_ON
			uniform sampler2D _MetalMask;
			#endif
				#ifdef _METALMASKTOGGLE_ON
			uniform float4 _MetalMask_ST;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenTint;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenSnowColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenSnowScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortion;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenHighlightColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenFade;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideContrast;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideNoiseColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideNoiseFactor;
			#endif
			uniform float2 _BurnSwirlNoiseScale;
			uniform float _BurnSwirlFactor;
			uniform float2 _BurnInsideNoiseScale;
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnRadius;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnPosition;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnEdgeNoiseScale;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnEdgeNoiseFactor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnWidth;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnEdgeColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnFade;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowCenter;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowNoiseScale;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowNoiseFactor;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowDensity;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSpeed;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSaturation;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowBrightness;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowContrast;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowFade;
			#endif
			uniform float _ShineSaturation;
			uniform float _ShineContrast;
				#ifdef _ENABLESHINE_ON
			uniform float4 _ShineColor;
			#endif
			uniform float _ShineRotation;
			uniform float _ShineFrequency;
			uniform float _ShineSpeed;
			uniform float _ShineWidth;
			uniform float _ShineFade;
				#ifdef _SHINEMASKTOGGLE_ON
			uniform sampler2D _ShineMask;
			#endif
				#ifdef _SHINEMASKTOGGLE_ON
			uniform float4 _ShineMask_ST;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseScale;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonShiftSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonDensity;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float4 _PoisonColor;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonFade;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonNoiseBrightness;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonRecolorFactor;
			#endif
			uniform float4 _EnchantedLowColor;
			uniform float4 _EnchantedHighColor;
			uniform float2 _EnchantedSpeed;
			uniform float2 _EnchantedScale;
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowDensity;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSpeed;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSaturation;
			#endif
			uniform float _EnchantedContrast;
			uniform float _EnchantedBrightness;
			uniform float _EnchantedReduce;
			uniform float _EnchantedFade;
			uniform float4 _ShiftingColorA;
			uniform float4 _ShiftingColorB;
			uniform float _ShiftingSpeed;
			uniform float _ShiftingDensity;
			uniform float _ShiftingBrightness;
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
			uniform float _ShiftingSaturation;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingContrast;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingFade;
			#endif
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float _FullAlphaDissolveFade;
			#endif
			uniform float _FullAlphaDissolveWidth;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float2 _FullAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float4 _FullGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float2 _FullGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveFade;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveInvert;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveFade;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveFade;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float4 _SourceGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float2 _DirectionalAlphaFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeWidth;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float4 _DirectionalGlowFadeEdgeColor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float2 _DirectionalGlowFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeWidth;
			#endif
				#ifdef _ENABLEHALFTONE_ON
			uniform float _HalftoneInvert;
			#endif
			uniform float _HalftoneTiling;
			uniform float _HalftoneFade;
			uniform float2 _HalftonePosition;
			uniform float _HalftoneFadeWidth;
			uniform float4 _AddColorColor;
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform sampler2D _AddColorMask;
			#endif
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform float4 _AddColorMask_ST;
			#endif
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
			uniform float _AddColorContrast;
			#endif
				#ifdef _ENABLEADDCOLOR_ON
			uniform float _AddColorFade;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float4 _AlphaTintColor;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintMinAlpha;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintFade;
			#endif
			uniform float4 _StrongTintTint;
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform sampler2D _StrongTintMask;
			#endif
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform float4 _StrongTintMask_ST;
			#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
			uniform float _StrongTintContrast;
			#endif
				#ifdef _ENABLESTRONGTINT_ON
			uniform float _StrongTintFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float4 _ShadowColor;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float _ShadowFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float2 _ShadowOffset;
			#endif
			uniform sampler2D _NormalMap;
			uniform float _NormalIntensity;
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionTint;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform sampler2D _EmissionMap;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionMap_ST;
			#endif
			uniform float _Metallic;
			uniform sampler2D _MetallicMap;
			uniform float _Smoothness;
			uniform float _AlphaClip;

	
			float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
			{
				original -= center;
				float C = cos( angle );
				float S = sin( angle );
				float t = 1 - C;
				float m00 = t * u.x * u.x + C;
				float m01 = t * u.x * u.y - S * u.z;
				float m02 = t * u.x * u.z + S * u.y;
				float m10 = t * u.x * u.y + S * u.z;
				float m11 = t * u.y * u.y + C;
				float m12 = t * u.y * u.z - S * u.x;
				float m20 = t * u.x * u.z - S * u.y;
				float m21 = t * u.y * u.z + S * u.x;
				float m22 = t * u.z * u.z + C;
				float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
				return mul( finalMatrix, original ) + center;
			}
			
			float MyCustomExpression16_g11717( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11719( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float FastNoise101_g11668( float x )
			{
				float i = floor(x);
				float f = frac(x);
				float s = sign(frac(x/2.0)-0.5);
				    
				float k = 0.5+0.5*sin(i);
				return s*f*(f-1.0)*((16.0*k-4.0)*f*(f-1.0)-1.0);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			
			float MyCustomExpression16_g11670( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11671( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11674( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11673( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11679( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11680( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11721( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11676( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11732( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float4 texturePointSmooth( sampler2D tex, float4 textureTexelSize, float2 uvs )
			{
				float2 size;
				size.x = textureTexelSize.z;
				size.y = textureTexelSize.w;
				float2 pixel = float2(1.0,1.0) / size;
				uvs -= pixel * float2(0.5,0.5);
				float2 uv_pixels = uvs * size;
				float2 delta_pixel = frac(uv_pixels) - float2(0.5,0.5);
				float2 ddxy = fwidth(uv_pixels);
				float2 mip = log2(ddxy) - 0.5;
				float2 clampedUV = uvs + (clamp(delta_pixel / ddxy, 0.0, 1.0) - delta_pixel) * pixel;
				return tex2Dlod(tex, float4(clampedUV,0, min(mip.x, mip.y)));
			}
			
			float MyCustomExpression16_g11758( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11760( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11764( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 MyCustomExpression115_g11768( float3 In, float3 From, float3 To, float Fuzziness, float Range )
			{
				float Distance = distance(From, In);
				return lerp(To, In, saturate((Distance - Range) / max(Fuzziness, 0.001)));
			}
			
			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float MyCustomExpression16_g11787( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11774( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11798( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11805( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11838( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11835( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11837( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11828( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11830( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11823( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11825( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11826( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11819( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11817( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11818( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11813( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11841( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11845( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11843( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11852( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11860( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11862( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11858( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11854( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11856( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			

			v2f VertexFunction (appdata v  ) {
				UNITY_SETUP_INSTANCE_ID(v);
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 _ZeroVector = float2(0,0);
				float2 texCoord363 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLESCREENTILING_ON
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLEWORLDTILING_ON
				float2 staticSwitch2_g11657 = ( ( ( (ase_worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _PIXELPERFECTUV_ON
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				#ifdef _SPRITESHEETFIX_ON
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				#ifdef _ENABLESQUISH_ON
				float2 break77_g11879 = fixedUV475;
				float2 appendResult72_g11879 = (float2(( _SquishStretch * ( break77_g11879.x - 0.5 ) * _SquishFade ) , ( _SquishFade * ( break77_g11879.y + _SquishFlip ) * -_SquishSquish )));
				float2 staticSwitch198 = ( appendResult72_g11879 + _ZeroVector );
				#else
				float2 staticSwitch198 = _ZeroVector;
				#endif
				float2 temp_output_2_0_g11880 = staticSwitch198;
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				float temp_output_8_0_g11880 = shaderTime237;
				#ifdef _ENABLESINEMOVE_ON
				float2 staticSwitch4_g11880 = ( ( sin( ( temp_output_8_0_g11880 * _SineMoveFrequency ) ) * _SineMoveOffset * _SineMoveFade ) + temp_output_2_0_g11880 );
				#else
				float2 staticSwitch4_g11880 = temp_output_2_0_g11880;
				#endif
				#ifdef _ENABLEVIBRATE_ON
				float temp_output_30_0_g11881 = temp_output_8_0_g11880;
				float3 rotatedValue21_g11881 = RotateAroundAxis( float3( 0,0,0 ), float3( 0,1,0 ), float3( 0,0,1 ), ( temp_output_30_0_g11881 * _VibrateRotation ) );
				float2 staticSwitch6_g11880 = ( ( sin( ( _VibrateFrequency * temp_output_30_0_g11881 ) ) * _VibrateOffset * _VibrateFade * (rotatedValue21_g11881).xy ) + staticSwitch4_g11880 );
				#else
				float2 staticSwitch6_g11880 = staticSwitch4_g11880;
				#endif
				#ifdef _ENABLESINESCALE_ON
				float2 staticSwitch10_g11880 = ( staticSwitch6_g11880 + ( (v.vertex.xyz).xy * ( ( ( sin( ( _SineScaleFrequency * temp_output_8_0_g11880 ) ) + 1.0 ) * 0.5 ) * _SineScaleFactor ) ) );
				#else
				float2 staticSwitch10_g11880 = staticSwitch6_g11880;
				#endif
				float2 temp_output_424_0 = staticSwitch10_g11880;
				float2 uv_FadingMask = v.ase_texcoord.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2Dlod( _FadingMask, float4( uv_FadingMask, 0, 0.0) );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float2 texCoord435 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				#ifdef _PIXELPERFECTSPACE_ON
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (v.vertex.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (v.vertex.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (ase_worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float linValue16_g11717 = tex2Dlod( _UberNoiseTexture, float4( ( shaderPosition235 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2Dlod( _UberNoiseTexture, float4( ( temp_output_27_0_g11718 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult121 = lerp( float2( 0,0 ) , temp_output_424_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch142 = temp_output_424_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch142 = lerpResult121;
				#else
				float2 staticSwitch142 = temp_output_424_0;
				#endif
				
				o.ase_texcoord9.xy = v.ase_texcoord.xy;
				o.ase_texcoord10 = v.vertex;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord9.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = float3( staticSwitch142 ,  0.0 );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.vertex.w = 1;
				v.normal = v.normal;
				v.tangent = v.tangent;

				o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
				o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				#if UNITY_VERSION >= 201810 && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_TRANSFER_LIGHTING(o, v.texcoord1.xy);
				#elif defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if UNITY_VERSION >= 201710
						UNITY_TRANSFER_SHADOW(o, v.texcoord1.xy);
					#else
						TRANSFER_SHADOW(o);
					#endif
				#endif

				#ifdef ASE_FOG
					UNITY_TRANSFER_FOG(o,o.pos);
				#endif
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					o.screenPos = ComputeScreenPos(o.pos);
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( appdata v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.tangent = v.tangent;
				o.normal = v.normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				appdata o = (appdata) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
				o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			v2f vert ( appdata v )
			{
				return VertexFunction( v );
			}
			#endif

			fixed4 frag ( v2f IN 
				#ifdef _DEPTHOFFSET_ON
				, out float outputDepth : SV_Depth
				#endif
				) : SV_Target 
			{
				UNITY_SETUP_INSTANCE_ID(IN);

				#ifdef LOD_FADE_CROSSFADE
					UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
				#endif

				#if defined(_SPECULAR_SETUP)
					SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
				#else
					SurfaceOutputStandard o = (SurfaceOutputStandard)0;
				#endif
				float3 WorldTangent = float3(IN.tSpace0.x,IN.tSpace1.x,IN.tSpace2.x);
				float3 WorldBiTangent = float3(IN.tSpace0.y,IN.tSpace1.y,IN.tSpace2.y);
				float3 WorldNormal = float3(IN.tSpace0.z,IN.tSpace1.z,IN.tSpace2.z);
				float3 worldPos = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
				#else
					half atten = 1;
				#endif
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 ScreenPos = IN.screenPos;
				#endif


				float2 texCoord363 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				float4 ase_screenPosNorm = ScreenPos / ScreenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				#ifdef _ENABLESCREENTILING_ON
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				#ifdef _ENABLEWORLDTILING_ON
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11657 = ( ( ( (worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				#ifdef _SPRITESHEETFIX_ON
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				float2 temp_output_3_0_g11667 = fixedUV475;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch117_g11668 = _WindMinIntensity;
				#else
				float staticSwitch117_g11668 = WindMinIntensity;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch118_g11668 = _WindMaxIntensity;
				#else
				float staticSwitch118_g11668 = WindMaxIntensity;
				#endif
				float4 transform62_g11668 = mul(unity_WorldToObject,float4( 0,0,0,1 ));
				#ifdef _WINDISPARALLAX_ON
				float staticSwitch111_g11668 = _WindXPosition;
				#else
				float staticSwitch111_g11668 = transform62_g11668.x;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch113_g11668 = _WindNoiseScale;
				#else
				float staticSwitch113_g11668 = WindNoiseScale;
				#endif
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch125_g11668 = ( shaderTime237 * _WindNoiseSpeed );
				#else
				float staticSwitch125_g11668 = WindTime;
				#endif
				float temp_output_50_0_g11668 = ( ( staticSwitch111_g11668 * staticSwitch113_g11668 ) + staticSwitch125_g11668 );
				float x101_g11668 = temp_output_50_0_g11668;
				float localFastNoise101_g11668 = FastNoise101_g11668( x101_g11668 );
				float2 temp_cast_0 = (temp_output_50_0_g11668).xx;
				float simplePerlin2D121_g11668 = snoise( temp_cast_0*0.5 );
				simplePerlin2D121_g11668 = simplePerlin2D121_g11668*0.5 + 0.5;
				#ifdef _WINDHIGHQUALITYNOISE_ON
				float staticSwitch123_g11668 = simplePerlin2D121_g11668;
				#else
				float staticSwitch123_g11668 = ( localFastNoise101_g11668 + 0.5 );
				#endif
				#ifdef _ENABLEWIND_ON
				float lerpResult86_g11668 = lerp( staticSwitch117_g11668 , staticSwitch118_g11668 , staticSwitch123_g11668);
				float clampResult29_g11668 = clamp( ( ( _WindRotationWindFactor * lerpResult86_g11668 ) + _WindRotation ) , -_WindMaxRotation , _WindMaxRotation );
				float2 temp_output_1_0_g11668 = temp_output_3_0_g11667;
				float temp_output_39_0_g11668 = ( temp_output_1_0_g11668.y + _WindFlip );
				float3 appendResult43_g11668 = (float3(0.5 , -_WindFlip , 0.0));
				float2 appendResult27_g11668 = (float2(0.0 , ( _WindSquishFactor * min( ( ( _WindSquishWindFactor * abs( lerpResult86_g11668 ) ) + abs( _WindRotation ) ) , _WindMaxRotation ) * temp_output_39_0_g11668 )));
				float3 rotatedValue19_g11668 = RotateAroundAxis( appendResult43_g11668, float3( ( appendResult27_g11668 + temp_output_1_0_g11668 ) ,  0.0 ), float3( 0,0,1 ), ( clampResult29_g11668 * temp_output_39_0_g11668 ) );
				float2 staticSwitch4_g11667 = (rotatedValue19_g11668).xy;
				#else
				float2 staticSwitch4_g11667 = temp_output_3_0_g11667;
				#endif
				float2 texCoord435 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _PIXELPERFECTSPACE_ON
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (IN.ase_texcoord10.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (IN.ase_texcoord10.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float2 temp_output_195_0_g11669 = shaderPosition235;
				float linValue16_g11670 = tex2D( _UberNoiseTexture, ( temp_output_195_0_g11669 * _FullDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11670 = MyCustomExpression16_g11670( linValue16_g11670 );
				float linValue16_g11671 = tex2D( _UberNoiseTexture, ( ( temp_output_195_0_g11669 + float2( 0.321,0.321 ) ) * _FullDistortionNoiseScale ) ).r;
				#ifdef _ENABLEFULLDISTORTION_ON
				float localMyCustomExpression16_g11671 = MyCustomExpression16_g11671( linValue16_g11671 );
				float2 appendResult189_g11669 = (float2(( localMyCustomExpression16_g11670 - 0.5 ) , ( localMyCustomExpression16_g11671 - 0.5 )));
				float2 staticSwitch83 = ( staticSwitch4_g11667 + ( ( 1.0 - _FullDistortionFade ) * appendResult189_g11669 * _FullDistortionDistortion ) );
				#else
				float2 staticSwitch83 = staticSwitch4_g11667;
				#endif
				float2 temp_output_182_0_g11672 = shaderPosition235;
				float linValue16_g11674 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionDistortionScale ) ).r;
				float localMyCustomExpression16_g11674 = MyCustomExpression16_g11674( linValue16_g11674 );
				float3 rotatedValue168_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( _DirectionalDistortionDistortion ,  0.0 ), float3( 0,0,1 ), ( ( ( localMyCustomExpression16_g11674 - 0.5 ) * 2.0 * _DirectionalDistortionRandomDirection ) * UNITY_PI ) );
				float3 rotatedValue136_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_182_0_g11672 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalDistortionRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11672 = rotatedValue136_g11672;
				float linValue16_g11673 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11673 = MyCustomExpression16_g11673( linValue16_g11673 );
				float clampResult154_g11672 = clamp( ( ( break130_g11672.x + break130_g11672.y + _DirectionalDistortionFade + ( localMyCustomExpression16_g11673 * _DirectionalDistortionNoiseFactor ) ) / max( _DirectionalDistortionWidth , 0.001 ) ) , 0.0 , 1.0 );
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float2 staticSwitch82 = ( staticSwitch83 + ( (rotatedValue168_g11672).xy * ( 1.0 - (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 )) ) ) );
				#else
				float2 staticSwitch82 = staticSwitch83;
				#endif
				float temp_output_8_0_g11677 = ( ( ( shaderTime237 * _HologramDistortionSpeed ) + worldPos.y ) / unity_OrthoParams.y );
				float2 temp_cast_4 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_5 = (_HologramDistortionDensity).xx;
				float linValue16_g11679 = tex2D( _UberNoiseTexture, ( temp_cast_4 * temp_cast_5 ) ).r;
				float localMyCustomExpression16_g11679 = MyCustomExpression16_g11679( linValue16_g11679 );
				float clampResult75_g11677 = clamp( localMyCustomExpression16_g11679 , 0.075 , 0.6 );
				float2 temp_cast_6 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_7 = (_HologramDistortionScale).xx;
				float linValue16_g11680 = tex2D( _UberNoiseTexture, ( temp_cast_6 * temp_cast_7 ) ).r;
				float localMyCustomExpression16_g11680 = MyCustomExpression16_g11680( linValue16_g11680 );
				float2 appendResult2_g11678 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float hologramFade182 = _HologramFade;
				#ifdef _ENABLEHOLOGRAM_ON
				float2 appendResult44_g11677 = (float2(( ( ( clampResult75_g11677 * ( localMyCustomExpression16_g11680 - 0.5 ) ) * _HologramDistortionOffset * ( 100.0 / appendResult2_g11678 ).x ) * hologramFade182 ) , 0.0));
				float2 staticSwitch59 = ( staticSwitch82 + appendResult44_g11677 );
				#else
				float2 staticSwitch59 = staticSwitch82;
				#endif
				float2 temp_output_18_0_g11675 = shaderPosition235;
				float2 glitchPosition154 = temp_output_18_0_g11675;
				float linValue16_g11721 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchDistortionSpeed * shaderTime237 ) ) * _GlitchDistortionScale ) ).r;
				float localMyCustomExpression16_g11721 = MyCustomExpression16_g11721( linValue16_g11721 );
				float linValue16_g11676 = tex2D( _UberNoiseTexture, ( ( temp_output_18_0_g11675 + ( _GlitchMaskSpeed * shaderTime237 ) ) * _GlitchMaskScale ) ).r;
				float localMyCustomExpression16_g11676 = MyCustomExpression16_g11676( linValue16_g11676 );
				float glitchFade152 = ( max( localMyCustomExpression16_g11676 , _GlitchMaskMin ) * _GlitchFade );
				#ifdef _ENABLEGLITCH_ON
				float2 staticSwitch62 = ( staticSwitch59 + ( ( localMyCustomExpression16_g11721 - 0.5 ) * _GlitchDistortion * glitchFade152 ) );
				#else
				float2 staticSwitch62 = staticSwitch59;
				#endif
				float2 temp_output_1_0_g11722 = staticSwitch62;
				float2 temp_output_26_0_g11722 = shaderPosition235;
				float temp_output_25_0_g11722 = shaderTime237;
				float linValue16_g11732 = tex2D( _UberNoiseTexture, ( ( temp_output_26_0_g11722 + ( _UVDistortSpeed * temp_output_25_0_g11722 ) ) * _UVDistortNoiseScale ) ).r;
				float localMyCustomExpression16_g11732 = MyCustomExpression16_g11732( linValue16_g11732 );
				float2 lerpResult21_g11729 = lerp( _UVDistortFrom , _UVDistortTo , localMyCustomExpression16_g11732);
				float2 appendResult2_g11731 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _UVDISTORTMASKTOGGLE_ON
				float2 uv_UVDistortMask = IN.ase_texcoord9.xy * _UVDistortMask_ST.xy + _UVDistortMask_ST.zw;
				float4 tex2DNode3_g11730 = tex2D( _UVDistortMask, uv_UVDistortMask );
				float staticSwitch29_g11729 = ( _UVDistortFade * ( tex2DNode3_g11730.r * tex2DNode3_g11730.a ) );
				#else
				float staticSwitch29_g11729 = _UVDistortFade;
				#endif
				#ifdef _ENABLEUVDISTORT_ON
				float2 staticSwitch5_g11722 = ( temp_output_1_0_g11722 + ( lerpResult21_g11729 * ( 100.0 / appendResult2_g11731 ) * staticSwitch29_g11729 ) );
				#else
				float2 staticSwitch5_g11722 = temp_output_1_0_g11722;
				#endif
				#ifdef _ENABLESQUEEZE_ON
				float2 temp_output_1_0_g11728 = staticSwitch5_g11722;
				float2 staticSwitch7_g11722 = ( temp_output_1_0_g11728 + ( ( temp_output_1_0_g11728 - _SqueezeCenter ) * pow( distance( temp_output_1_0_g11728 , _SqueezeCenter ) , _SqueezePower ) * _SqueezeScale * _SqueezeFade ) );
				#else
				float2 staticSwitch7_g11722 = staticSwitch5_g11722;
				#endif
				#ifdef _ENABLESINEROTATE_ON
				float3 rotatedValue36_g11727 = RotateAroundAxis( float3( _SineRotatePivot ,  0.0 ), float3( staticSwitch7_g11722 ,  0.0 ), float3( 0,0,1 ), ( sin( ( temp_output_25_0_g11722 * _SineRotateFrequency ) ) * ( ( _SineRotateAngle / 360.0 ) * UNITY_PI ) * _SineRotateFade ) );
				float2 staticSwitch9_g11722 = (rotatedValue36_g11727).xy;
				#else
				float2 staticSwitch9_g11722 = staticSwitch7_g11722;
				#endif
				#ifdef _ENABLEUVROTATE_ON
				float3 rotatedValue8_g11726 = RotateAroundAxis( float3( _UVRotatePivot ,  0.0 ), float3( staticSwitch9_g11722 ,  0.0 ), float3( 0,0,1 ), ( temp_output_25_0_g11722 * _UVRotateSpeed * UNITY_PI ) );
				float2 staticSwitch16_g11722 = (rotatedValue8_g11726).xy;
				#else
				float2 staticSwitch16_g11722 = staticSwitch9_g11722;
				#endif
				#ifdef _ENABLEUVSCROLL_ON
				float2 staticSwitch14_g11722 = ( ( _UVScrollSpeed * temp_output_25_0_g11722 ) + staticSwitch16_g11722 );
				#else
				float2 staticSwitch14_g11722 = staticSwitch16_g11722;
				#endif
				#ifdef _ENABLEPIXELATE_ON
				float2 appendResult35_g11724 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 MultFactor30_g11724 = ( ( _PixelatePixelDensity * ( appendResult35_g11724 / _PixelatePixelsPerUnit ) ) * ( 1.0 / max( _PixelateFade , 1E-05 ) ) );
				float2 clampResult46_g11724 = clamp( ( floor( ( MultFactor30_g11724 * ( staticSwitch14_g11722 + ( float2( 0.5,0.5 ) / MultFactor30_g11724 ) ) ) ) / MultFactor30_g11724 ) , float2( 0,0 ) , float2( 1,1 ) );
				float2 staticSwitch4_g11722 = clampResult46_g11724;
				#else
				float2 staticSwitch4_g11722 = staticSwitch14_g11722;
				#endif
				#ifdef _ENABLEUVSCALE_ON
				float2 staticSwitch24_g11722 = ( ( ( staticSwitch4_g11722 - _UVScalePivot ) / _UVScaleScale ) + _UVScalePivot );
				#else
				float2 staticSwitch24_g11722 = staticSwitch4_g11722;
				#endif
				float2 temp_output_1_0_g11733 = staticSwitch24_g11722;
				float temp_output_7_0_g11733 = ( sin( ( _WiggleFrequency * ( temp_output_26_0_g11722.y + ( _WiggleSpeed * temp_output_25_0_g11722 ) ) ) ) * _WiggleOffset * _WiggleFade );
				#ifdef _WIGGLEFIXEDGROUNDTOGGLE_ON
				float staticSwitch18_g11733 = ( temp_output_7_0_g11733 * temp_output_1_0_g11733.y );
				#else
				float staticSwitch18_g11733 = temp_output_7_0_g11733;
				#endif
				#ifdef _ENABLEWIGGLE_ON
				float2 appendResult12_g11733 = (float2(staticSwitch18_g11733 , 0.0));
				float2 staticSwitch13_g11733 = ( temp_output_1_0_g11733 + appendResult12_g11733 );
				#else
				float2 staticSwitch13_g11733 = temp_output_1_0_g11733;
				#endif
				float2 temp_output_484_0 = staticSwitch13_g11733;
				float2 texCoord131 = IN.ase_texcoord9.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_FadingMask = IN.ase_texcoord9.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2D( _FadingMask, uv_FadingMask );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float linValue16_g11717 = tex2D( _UberNoiseTexture, ( shaderPosition235 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2D( _UberNoiseTexture, ( temp_output_27_0_g11718 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult130 = lerp( texCoord131 , temp_output_484_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch145 = temp_output_484_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch145 = lerpResult130;
				#else
				float2 staticSwitch145 = temp_output_484_0;
				#endif
				#ifdef _TILINGFIX_ON
				float2 staticSwitch485 = ( ( ( staticSwitch145 % float2( 1,1 ) ) + float2( 1,1 ) ) % float2( 1,1 ) );
				#else
				float2 staticSwitch485 = staticSwitch145;
				#endif
				#ifdef _SPRITESHEETFIX_ON
				float2 break14_g11734 = staticSwitch485;
				float2 break11_g11734 = float2( 0,0 );
				float2 break10_g11734 = float2( 1,1 );
				float2 break9_g11734 = spriteRectMin376;
				float2 break8_g11734 = spriteRectMax377;
				float2 appendResult15_g11734 = (float2((break9_g11734.x + (break14_g11734.x - break11_g11734.x) * (break8_g11734.x - break9_g11734.x) / (break10_g11734.x - break11_g11734.x)) , (break9_g11734.y + (break14_g11734.y - break11_g11734.y) * (break8_g11734.y - break9_g11734.y) / (break10_g11734.y - break11_g11734.y))));
				float2 staticSwitch371 = min( max( appendResult15_g11734 , spriteRectMin376 ) , spriteRectMax377 );
				#else
				float2 staticSwitch371 = staticSwitch485;
				#endif
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11735 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch427 = ( originalUV460 + ( floor( ( ( staticSwitch371 - uvAfterPixelArt450 ) * appendResult7_g11735 ) ) / appendResult7_g11735 ) );
				#else
				float2 staticSwitch427 = staticSwitch371;
				#endif
				float2 finalUV146 = staticSwitch427;
				float2 temp_output_1_0_g11736 = finalUV146;
				#ifdef _ENABLESMOOTHPIXELART_ON
				sampler2D tex3_g11737 = _MainTex;
				float4 textureTexelSize3_g11737 = _MainTex_TexelSize;
				float2 uvs3_g11737 = temp_output_1_0_g11736;
				float4 localtexturePointSmooth3_g11737 = texturePointSmooth( tex3_g11737 , textureTexelSize3_g11737 , uvs3_g11737 );
				float4 staticSwitch8_g11736 = localtexturePointSmooth3_g11737;
				#else
				float4 staticSwitch8_g11736 = tex2D( _MainTex, temp_output_1_0_g11736 );
				#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
				float temp_output_10_0_g11738 = ( _GaussianBlurOffset * _GaussianBlurFade * 0.005 );
				float temp_output_2_0_g11748 = temp_output_10_0_g11738;
				float2 appendResult16_g11748 = (float2(temp_output_2_0_g11748 , 0.0));
				float2 appendResult25_g11750 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11750 = ( appendResult16_g11748 * appendResult25_g11750 );
				float2 temp_output_7_0_g11738 = temp_output_1_0_g11736;
				float2 temp_output_1_0_g11748 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,1 ) ) );
				float2 temp_output_1_0_g11750 = temp_output_1_0_g11748;
				float2 appendResult17_g11748 = (float2(0.0 , temp_output_2_0_g11748));
				float2 appendResult25_g11749 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11749 = ( appendResult17_g11748 * appendResult25_g11749 );
				float2 temp_output_1_0_g11749 = temp_output_1_0_g11748;
				float temp_output_2_0_g11739 = temp_output_10_0_g11738;
				float2 appendResult16_g11739 = (float2(temp_output_2_0_g11739 , 0.0));
				float2 appendResult25_g11741 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11741 = ( appendResult16_g11739 * appendResult25_g11741 );
				float2 temp_output_1_0_g11739 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,1 ) ) );
				float2 temp_output_1_0_g11741 = temp_output_1_0_g11739;
				float2 appendResult17_g11739 = (float2(0.0 , temp_output_2_0_g11739));
				float2 appendResult25_g11740 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11740 = ( appendResult17_g11739 * appendResult25_g11740 );
				float2 temp_output_1_0_g11740 = temp_output_1_0_g11739;
				float temp_output_2_0_g11745 = temp_output_10_0_g11738;
				float2 appendResult16_g11745 = (float2(temp_output_2_0_g11745 , 0.0));
				float2 appendResult25_g11747 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11747 = ( appendResult16_g11745 * appendResult25_g11747 );
				float2 temp_output_1_0_g11745 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,-1 ) ) );
				float2 temp_output_1_0_g11747 = temp_output_1_0_g11745;
				float2 appendResult17_g11745 = (float2(0.0 , temp_output_2_0_g11745));
				float2 appendResult25_g11746 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11746 = ( appendResult17_g11745 * appendResult25_g11746 );
				float2 temp_output_1_0_g11746 = temp_output_1_0_g11745;
				float temp_output_2_0_g11742 = temp_output_10_0_g11738;
				float2 appendResult16_g11742 = (float2(temp_output_2_0_g11742 , 0.0));
				float2 appendResult25_g11744 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11744 = ( appendResult16_g11742 * appendResult25_g11744 );
				float2 temp_output_1_0_g11742 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,-1 ) ) );
				float2 temp_output_1_0_g11744 = temp_output_1_0_g11742;
				float2 appendResult17_g11742 = (float2(0.0 , temp_output_2_0_g11742));
				float2 appendResult25_g11743 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11743 = ( appendResult17_g11742 * appendResult25_g11743 );
				float2 temp_output_1_0_g11743 = temp_output_1_0_g11742;
				float4 staticSwitch3_g11736 = ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) ) ) ) * 0.0625 );
				#else
				float4 staticSwitch3_g11736 = staticSwitch8_g11736;
				#endif
				#ifdef _ENABLESHARPEN_ON
				float2 temp_output_1_0_g11751 = temp_output_1_0_g11736;
				float4 tex2DNode4_g11751 = tex2D( _MainTex, temp_output_1_0_g11751 );
				float temp_output_2_0_g11752 = _SharpenOffset;
				float2 appendResult16_g11752 = (float2(temp_output_2_0_g11752 , 0.0));
				float2 appendResult25_g11754 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11754 = ( appendResult16_g11752 * appendResult25_g11754 );
				float2 temp_output_1_0_g11752 = temp_output_1_0_g11751;
				float2 temp_output_1_0_g11754 = temp_output_1_0_g11752;
				float2 appendResult17_g11752 = (float2(0.0 , temp_output_2_0_g11752));
				float2 appendResult25_g11753 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11753 = ( appendResult17_g11752 * appendResult25_g11753 );
				float2 temp_output_1_0_g11753 = temp_output_1_0_g11752;
				float4 break22_g11751 = ( tex2DNode4_g11751 - ( ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) ) ) / 4.0 ) - tex2DNode4_g11751 ) * ( _SharpenFactor * _SharpenFade ) ) );
				float clampResult23_g11751 = clamp( break22_g11751.a , 0.0 , 1.0 );
				float4 appendResult24_g11751 = (float4(break22_g11751.r , break22_g11751.g , break22_g11751.b , clampResult23_g11751));
				float4 staticSwitch12_g11736 = appendResult24_g11751;
				#else
				float4 staticSwitch12_g11736 = staticSwitch3_g11736;
				#endif
				float4 temp_output_471_0 = staticSwitch12_g11736;
				#ifdef _VERTEXTINTFIRST_ON
				float4 temp_output_1_0_g11755 = temp_output_471_0;
				float4 appendResult8_g11755 = (float4(( (temp_output_1_0_g11755).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11755.a));
				float4 staticSwitch354 = appendResult8_g11755;
				#else
				float4 staticSwitch354 = temp_output_471_0;
				#endif
				float4 originalColor191 = staticSwitch354;
				float4 temp_output_1_0_g11756 = originalColor191;
				float4 temp_output_1_0_g11757 = temp_output_1_0_g11756;
				float2 temp_output_7_0_g11756 = finalUV146;
				float2 temp_output_43_0_g11757 = temp_output_7_0_g11756;
				float2 temp_cast_15 = (_SmokeNoiseScale).xx;
				float linValue16_g11758 = tex2D( _UberNoiseTexture, ( ( ( IN.ase_color.r * (( _SmokeVertexSeed )?( 5.0 ):( 0.0 )) ) + temp_output_43_0_g11757 ) * temp_cast_15 ) ).r;
				float localMyCustomExpression16_g11758 = MyCustomExpression16_g11758( linValue16_g11758 );
				float clampResult28_g11757 = clamp( ( ( ( localMyCustomExpression16_g11758 - 1.0 ) * _SmokeNoiseFactor ) + ( ( ( IN.ase_color.a / 2.5 ) - distance( temp_output_43_0_g11757 , float2( 0.5,0.5 ) ) ) * 2.5 * _SmokeSmoothness ) ) , 0.0 , 1.0 );
				#ifdef _ENABLESMOKE_ON
				float3 lerpResult34_g11757 = lerp( (temp_output_1_0_g11757).rgb , float3( 0,0,0 ) , ( ( 1.0 - clampResult28_g11757 ) * _SmokeDarkEdge ));
				float4 appendResult31_g11757 = (float4(lerpResult34_g11757 , ( clampResult28_g11757 * _SmokeAlpha * temp_output_1_0_g11757.a )));
				float4 staticSwitch2_g11756 = appendResult31_g11757;
				#else
				float4 staticSwitch2_g11756 = temp_output_1_0_g11756;
				#endif
				#ifdef _ENABLECUSTOMFADE_ON
				float4 temp_output_1_0_g11759 = staticSwitch2_g11756;
				float2 temp_output_57_0_g11759 = temp_output_7_0_g11756;
				float4 tex2DNode3_g11759 = tex2D( _CustomFadeFadeMask, temp_output_57_0_g11759 );
				float linValue16_g11760 = tex2D( _UberNoiseTexture, ( temp_output_57_0_g11759 * _CustomFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11760 = MyCustomExpression16_g11760( linValue16_g11760 );
				float clampResult37_g11759 = clamp( ( ( ( IN.ase_color.a * 2.0 ) - 1.0 ) + ( tex2DNode3_g11759.r + ( localMyCustomExpression16_g11760 * _CustomFadeNoiseFactor ) ) ) , 0.0 , 1.0 );
				float4 appendResult13_g11759 = (float4((temp_output_1_0_g11759).rgb , ( temp_output_1_0_g11759.a * pow( clampResult37_g11759 , ( _CustomFadeSmoothness / max( tex2DNode3_g11759.r , 0.05 ) ) ) * _CustomFadeAlpha )));
				float4 staticSwitch3_g11756 = appendResult13_g11759;
				#else
				float4 staticSwitch3_g11756 = staticSwitch2_g11756;
				#endif
				float4 temp_output_1_0_g11761 = staticSwitch3_g11756;
				#ifdef _ENABLECHECKERBOARD_ON
				float4 temp_output_1_0_g11762 = temp_output_1_0_g11761;
				float2 appendResult4_g11762 = (float2(worldPos.x , worldPos.y));
				float2 temp_output_44_0_g11762 = ( appendResult4_g11762 * _CheckerboardTiling * 0.5 );
				float2 break12_g11762 = step( ( ceil( temp_output_44_0_g11762 ) - temp_output_44_0_g11762 ) , float2( 0.5,0.5 ) );
				float4 appendResult42_g11762 = (float4(( (temp_output_1_0_g11762).rgb * min( ( _CheckerboardDarken + abs( ( -break12_g11762.x + break12_g11762.y ) ) ) , 1.0 ) ) , temp_output_1_0_g11762.a));
				float4 staticSwitch2_g11761 = appendResult42_g11762;
				#else
				float4 staticSwitch2_g11761 = temp_output_1_0_g11761;
				#endif
				float2 temp_output_75_0_g11763 = finalUV146;
				float linValue16_g11764 = tex2D( _UberNoiseTexture, ( ( ( shaderTime237 * _FlameSpeed ) + temp_output_75_0_g11763 ) * _FlameNoiseScale ) ).r;
				float localMyCustomExpression16_g11764 = MyCustomExpression16_g11764( linValue16_g11764 );
				float saferPower57_g11763 = abs( max( ( temp_output_75_0_g11763.y - 0.2 ) , 0.0 ) );
				float temp_output_47_0_g11763 = max( _FlameRadius , 0.01 );
				float clampResult70_g11763 = clamp( ( ( ( localMyCustomExpression16_g11764 * pow( saferPower57_g11763 , _FlameNoiseHeightFactor ) * _FlameNoiseFactor ) + ( ( temp_output_47_0_g11763 - distance( temp_output_75_0_g11763 , float2( 0.5,0.4 ) ) ) / temp_output_47_0_g11763 ) ) * _FlameSmooth ) , 0.0 , 1.0 );
				#ifdef _ENABLEFLAME_ON
				float temp_output_63_0_g11763 = ( clampResult70_g11763 * _FlameBrightness );
				float4 appendResult31_g11763 = (float4(temp_output_63_0_g11763 , temp_output_63_0_g11763 , temp_output_63_0_g11763 , clampResult70_g11763));
				float4 staticSwitch6_g11761 = ( appendResult31_g11763 * staticSwitch2_g11761 );
				#else
				float4 staticSwitch6_g11761 = staticSwitch2_g11761;
				#endif
				float4 temp_output_3_0_g11765 = staticSwitch6_g11761;
				float4 temp_output_1_0_g11792 = temp_output_3_0_g11765;
				float2 temp_output_1_0_g11765 = finalUV146;
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
				float4 staticSwitch81_g11792 = tex2D( _RecolorRGBTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch81_g11792 = temp_output_1_0_g11792;
				#endif
				#ifdef _ENABLERECOLORRGB_ON
				float4 break82_g11792 = staticSwitch81_g11792;
				float temp_output_63_0_g11792 = ( break82_g11792.r + break82_g11792.g + break82_g11792.b );
				float4 break71_g11792 = ( ( _RecolorRGBRedTint * ( break82_g11792.r / temp_output_63_0_g11792 ) ) + ( _RecolorRGBGreenTint * ( break82_g11792.g / temp_output_63_0_g11792 ) ) + ( ( break82_g11792.b / temp_output_63_0_g11792 ) * _RecolorRGBBlueTint ) );
				float3 appendResult56_g11792 = (float3(break71_g11792.r , break71_g11792.g , break71_g11792.b));
				float4 break2_g11793 = temp_output_1_0_g11792;
				float saferPower57_g11792 = abs( ( ( break2_g11793.x + break2_g11793.x + break2_g11793.y + break2_g11793.y + break2_g11793.y + break2_g11793.z ) / 6.0 ) );
				float3 lerpResult26_g11792 = lerp( (temp_output_1_0_g11792).rgb , ( appendResult56_g11792 * pow( saferPower57_g11792 , ( max( break71_g11792.a , 0.01 ) * 2.0 ) ) ) , ( min( ( temp_output_63_0_g11792 * 2.0 ) , 1.0 ) * _RecolorRGBFade ));
				float4 appendResult30_g11792 = (float4(lerpResult26_g11792 , temp_output_1_0_g11792.a));
				float4 staticSwitch43_g11765 = appendResult30_g11792;
				#else
				float4 staticSwitch43_g11765 = temp_output_3_0_g11765;
				#endif
				float4 temp_output_1_0_g11790 = staticSwitch43_g11765;
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
				float4 staticSwitch62_g11790 = tex2D( _RecolorRGBYCPTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch62_g11790 = temp_output_1_0_g11790;
				#endif
				float3 hsvTorgb33_g11790 = RGBToHSV( staticSwitch62_g11790.rgb );
				float temp_output_43_0_g11790 = ( ( hsvTorgb33_g11790.x + 0.08333334 ) % 1.0 );
				float4 ifLocalVar46_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.8333333 )
				ifLocalVar46_g11790 = _RecolorRGBYCPPurpleTint;
				else
				ifLocalVar46_g11790 = _RecolorRGBYCPBlueTint;
				float4 ifLocalVar44_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.6666667 )
				ifLocalVar44_g11790 = _RecolorRGBYCPCyanTint;
				else
				ifLocalVar44_g11790 = ifLocalVar46_g11790;
				float4 ifLocalVar47_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.3333333 )
				ifLocalVar47_g11790 = _RecolorRGBYCPYellowTint;
				else
				ifLocalVar47_g11790 = _RecolorRGBYCPGreenTint;
				float4 ifLocalVar45_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.1666667 )
				ifLocalVar45_g11790 = _RecolorRGBYCPRedTint;
				else
				ifLocalVar45_g11790 = ifLocalVar47_g11790;
				float4 ifLocalVar35_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.5 )
				ifLocalVar35_g11790 = ifLocalVar44_g11790;
				else
				ifLocalVar35_g11790 = ifLocalVar45_g11790;
				#ifdef _ENABLERECOLORRGBYCP_ON
				float4 break55_g11790 = ifLocalVar35_g11790;
				float3 appendResult56_g11790 = (float3(break55_g11790.r , break55_g11790.g , break55_g11790.b));
				float4 break2_g11791 = temp_output_1_0_g11790;
				float saferPower57_g11790 = abs( ( ( break2_g11791.x + break2_g11791.x + break2_g11791.y + break2_g11791.y + break2_g11791.y + break2_g11791.z ) / 6.0 ) );
				float3 lerpResult26_g11790 = lerp( (temp_output_1_0_g11790).rgb , ( appendResult56_g11790 * pow( saferPower57_g11790 , max( ( break55_g11790.a * 2.0 ) , 0.01 ) ) ) , ( hsvTorgb33_g11790.z * _RecolorRGBYCPFade ));
				float4 appendResult30_g11790 = (float4(lerpResult26_g11790 , temp_output_1_0_g11790.a));
				float4 staticSwitch9_g11765 = appendResult30_g11790;
				#else
				float4 staticSwitch9_g11765 = staticSwitch43_g11765;
				#endif
				#ifdef _ENABLECOLORREPLACE_ON
				float4 temp_output_1_0_g11768 = staticSwitch9_g11765;
				float3 temp_output_2_0_g11768 = (temp_output_1_0_g11768).rgb;
				float3 In115_g11768 = temp_output_2_0_g11768;
				float3 From115_g11768 = (_ColorReplaceFromColor).rgb;
				float4 break2_g11769 = temp_output_1_0_g11768;
				float3 To115_g11768 = ( pow( ( ( break2_g11769.x + break2_g11769.x + break2_g11769.y + break2_g11769.y + break2_g11769.y + break2_g11769.z ) / 6.0 ) , max( _ColorReplaceContrast , 0.0001 ) ) * (_ColorReplaceToColor).rgb );
				float Fuzziness115_g11768 = _ColorReplaceSmoothness;
				float Range115_g11768 = _ColorReplaceRange;
				float3 localMyCustomExpression115_g11768 = MyCustomExpression115_g11768( In115_g11768 , From115_g11768 , To115_g11768 , Fuzziness115_g11768 , Range115_g11768 );
				float3 lerpResult112_g11768 = lerp( temp_output_2_0_g11768 , localMyCustomExpression115_g11768 , _ColorReplaceFade);
				float4 appendResult4_g11768 = (float4(lerpResult112_g11768 , temp_output_1_0_g11768.a));
				float4 staticSwitch29_g11765 = appendResult4_g11768;
				#else
				float4 staticSwitch29_g11765 = staticSwitch9_g11765;
				#endif
				float4 temp_output_1_0_g11779 = staticSwitch29_g11765;
				#ifdef _ENABLENEGATIVE_ON
				float3 temp_output_9_0_g11779 = (temp_output_1_0_g11779).rgb;
				float3 lerpResult3_g11779 = lerp( temp_output_9_0_g11779 , ( 1.0 - temp_output_9_0_g11779 ) , _NegativeFade);
				float4 appendResult8_g11779 = (float4(lerpResult3_g11779 , temp_output_1_0_g11779.a));
				float4 staticSwitch4_g11779 = appendResult8_g11779;
				#else
				float4 staticSwitch4_g11779 = temp_output_1_0_g11779;
				#endif
				float4 temp_output_57_0_g11765 = staticSwitch4_g11779;
				#ifdef _ENABLECONTRAST_ON
				float4 temp_output_1_0_g11800 = temp_output_57_0_g11765;
				float3 saferPower5_g11800 = abs( (temp_output_1_0_g11800).rgb );
				float3 temp_cast_29 = (_Contrast).xxx;
				float4 appendResult4_g11800 = (float4(pow( saferPower5_g11800 , temp_cast_29 ) , temp_output_1_0_g11800.a));
				float4 staticSwitch32_g11765 = appendResult4_g11800;
				#else
				float4 staticSwitch32_g11765 = temp_output_57_0_g11765;
				#endif
				#ifdef _ENABLEBRIGHTNESS_ON
				float4 temp_output_2_0_g11777 = staticSwitch32_g11765;
				float4 appendResult6_g11777 = (float4(( (temp_output_2_0_g11777).rgb * _Brightness ) , temp_output_2_0_g11777.a));
				float4 staticSwitch33_g11765 = appendResult6_g11777;
				#else
				float4 staticSwitch33_g11765 = staticSwitch32_g11765;
				#endif
				#ifdef _ENABLEHUE_ON
				float4 temp_output_2_0_g11778 = staticSwitch33_g11765;
				float3 hsvTorgb1_g11778 = RGBToHSV( temp_output_2_0_g11778.rgb );
				float3 hsvTorgb3_g11778 = HSVToRGB( float3(( hsvTorgb1_g11778.x + _Hue ),hsvTorgb1_g11778.y,hsvTorgb1_g11778.z) );
				float4 appendResult8_g11778 = (float4(hsvTorgb3_g11778 , temp_output_2_0_g11778.a));
				float4 staticSwitch36_g11765 = appendResult8_g11778;
				#else
				float4 staticSwitch36_g11765 = staticSwitch33_g11765;
				#endif
				#ifdef _ENABLESPLITTONING_ON
				float4 temp_output_1_0_g11794 = staticSwitch36_g11765;
				float4 break2_g11795 = temp_output_1_0_g11794;
				float temp_output_3_0_g11794 = ( ( break2_g11795.x + break2_g11795.x + break2_g11795.y + break2_g11795.y + break2_g11795.y + break2_g11795.z ) / 6.0 );
				float clampResult25_g11794 = clamp( ( ( ( ( temp_output_3_0_g11794 + _SplitToningShift ) - 0.5 ) * _SplitToningBalance ) + 0.5 ) , 0.0 , 1.0 );
				float3 lerpResult6_g11794 = lerp( (_SplitToningShadowsColor).rgb , (_SplitToningHighlightsColor).rgb , clampResult25_g11794);
				float temp_output_9_0_g11796 = max( _SplitToningContrast , 0.0 );
				float saferPower7_g11796 = abs( ( temp_output_3_0_g11794 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11796 ) , 0.0 ) ) ) );
				float3 lerpResult11_g11794 = lerp( (temp_output_1_0_g11794).rgb , ( lerpResult6_g11794 * pow( saferPower7_g11796 , temp_output_9_0_g11796 ) ) , _SplitToningFade);
				float4 appendResult18_g11794 = (float4(lerpResult11_g11794 , temp_output_1_0_g11794.a));
				float4 staticSwitch30_g11765 = appendResult18_g11794;
				#else
				float4 staticSwitch30_g11765 = staticSwitch36_g11765;
				#endif
				#ifdef _ENABLEBLACKTINT_ON
				float4 temp_output_1_0_g11775 = staticSwitch30_g11765;
				float3 temp_output_4_0_g11775 = (temp_output_1_0_g11775).rgb;
				float4 break12_g11775 = temp_output_1_0_g11775;
				float3 lerpResult7_g11775 = lerp( temp_output_4_0_g11775 , ( temp_output_4_0_g11775 + (_BlackTintColor).rgb ) , pow( ( 1.0 - min( max( max( break12_g11775.r , break12_g11775.g ) , break12_g11775.b ) , 1.0 ) ) , max( _BlackTintPower , 0.001 ) ));
				float3 lerpResult13_g11775 = lerp( temp_output_4_0_g11775 , lerpResult7_g11775 , _BlackTintFade);
				float4 appendResult11_g11775 = (float4(lerpResult13_g11775 , break12_g11775.a));
				float4 staticSwitch20_g11765 = appendResult11_g11775;
				#else
				float4 staticSwitch20_g11765 = staticSwitch30_g11765;
				#endif
				#ifdef _ENABLEINKSPREAD_ON
				float4 temp_output_1_0_g11786 = staticSwitch20_g11765;
				float4 break2_g11788 = temp_output_1_0_g11786;
				float temp_output_9_0_g11789 = max( _InkSpreadContrast , 0.0 );
				float saferPower7_g11789 = abs( ( ( ( break2_g11788.x + break2_g11788.x + break2_g11788.y + break2_g11788.y + break2_g11788.y + break2_g11788.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11789 ) , 0.0 ) ) ) );
				float2 temp_output_65_0_g11786 = shaderPosition235;
				float linValue16_g11787 = tex2D( _UberNoiseTexture, ( temp_output_65_0_g11786 * _InkSpreadNoiseScale ) ).r;
				float localMyCustomExpression16_g11787 = MyCustomExpression16_g11787( linValue16_g11787 );
				float clampResult53_g11786 = clamp( ( ( ( _InkSpreadDistance - distance( _InkSpreadPosition , temp_output_65_0_g11786 ) ) + ( localMyCustomExpression16_g11787 * _InkSpreadNoiseFactor ) ) / max( _InkSpreadWidth , 0.001 ) ) , 0.0 , 1.0 );
				float3 lerpResult7_g11786 = lerp( (temp_output_1_0_g11786).rgb , ( (_InkSpreadColor).rgb * pow( saferPower7_g11789 , temp_output_9_0_g11789 ) ) , ( _InkSpreadFade * clampResult53_g11786 ));
				float4 appendResult9_g11786 = (float4(lerpResult7_g11786 , (temp_output_1_0_g11786).a));
				float4 staticSwitch17_g11765 = appendResult9_g11786;
				#else
				float4 staticSwitch17_g11765 = staticSwitch20_g11765;
				#endif
				float temp_output_39_0_g11765 = shaderTime237;
				#ifdef _ENABLESHIFTHUE_ON
				float4 temp_output_1_0_g11780 = staticSwitch17_g11765;
				float3 hsvTorgb15_g11780 = RGBToHSV( (temp_output_1_0_g11780).rgb );
				float3 hsvTorgb19_g11780 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _ShiftHueSpeed ) + hsvTorgb15_g11780.x ),hsvTorgb15_g11780.y,hsvTorgb15_g11780.z) );
				float4 appendResult6_g11780 = (float4(hsvTorgb19_g11780 , temp_output_1_0_g11780.a));
				float4 staticSwitch19_g11765 = appendResult6_g11780;
				#else
				float4 staticSwitch19_g11765 = staticSwitch17_g11765;
				#endif
				float3 hsvTorgb19_g11783 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _AddHueSpeed ) % 1.0 ),_AddHueSaturation,_AddHueBrightness) );
				float4 temp_output_1_0_g11783 = staticSwitch19_g11765;
				float4 break2_g11785 = temp_output_1_0_g11783;
				float saferPower27_g11783 = abs( ( ( break2_g11785.x + break2_g11785.x + break2_g11785.y + break2_g11785.y + break2_g11785.y + break2_g11785.z ) / 6.0 ) );
				#ifdef _ADDHUEMASKTOGGLE_ON
				float2 uv_AddHueMask = IN.ase_texcoord9.xy * _AddHueMask_ST.xy + _AddHueMask_ST.zw;
				float4 tex2DNode3_g11784 = tex2D( _AddHueMask, uv_AddHueMask );
				float staticSwitch33_g11783 = ( _AddHueFade * ( tex2DNode3_g11784.r * tex2DNode3_g11784.a ) );
				#else
				float staticSwitch33_g11783 = _AddHueFade;
				#endif
				#ifdef _ENABLEADDHUE_ON
				float4 appendResult6_g11783 = (float4(( ( hsvTorgb19_g11783 * pow( saferPower27_g11783 , max( _AddHueContrast , 0.001 ) ) * staticSwitch33_g11783 ) + (temp_output_1_0_g11783).rgb ) , temp_output_1_0_g11783.a));
				float4 staticSwitch23_g11765 = appendResult6_g11783;
				#else
				float4 staticSwitch23_g11765 = staticSwitch19_g11765;
				#endif
				float4 temp_output_1_0_g11781 = staticSwitch23_g11765;
				float4 break2_g11782 = temp_output_1_0_g11781;
				float3 temp_output_13_0_g11781 = (_SineGlowColor).rgb;
				#ifdef _SINEGLOWMASKTOGGLE_ON
				float2 uv_SineGlowMask = IN.ase_texcoord9.xy * _SineGlowMask_ST.xy + _SineGlowMask_ST.zw;
				float4 tex2DNode30_g11781 = tex2D( _SineGlowMask, uv_SineGlowMask );
				float3 staticSwitch27_g11781 = ( (tex2DNode30_g11781).rgb * temp_output_13_0_g11781 * tex2DNode30_g11781.a );
				#else
				float3 staticSwitch27_g11781 = temp_output_13_0_g11781;
				#endif
				#ifdef _ENABLESINEGLOW_ON
				float4 appendResult21_g11781 = (float4(( (temp_output_1_0_g11781).rgb + ( pow( ( ( break2_g11782.x + break2_g11782.x + break2_g11782.y + break2_g11782.y + break2_g11782.y + break2_g11782.z ) / 6.0 ) , max( _SineGlowContrast , 0.0 ) ) * staticSwitch27_g11781 * _SineGlowFade * ( ( ( sin( ( temp_output_39_0_g11765 * _SineGlowFrequency ) ) + 1.0 ) * ( _SineGlowMax - _SineGlowMin ) ) + _SineGlowMin ) ) ) , temp_output_1_0_g11781.a));
				float4 staticSwitch28_g11765 = appendResult21_g11781;
				#else
				float4 staticSwitch28_g11765 = staticSwitch23_g11765;
				#endif
				#ifdef _ENABLESATURATION_ON
				float4 temp_output_1_0_g11770 = staticSwitch28_g11765;
				float4 break2_g11771 = temp_output_1_0_g11770;
				float3 temp_cast_45 = (( ( break2_g11771.x + break2_g11771.x + break2_g11771.y + break2_g11771.y + break2_g11771.y + break2_g11771.z ) / 6.0 )).xxx;
				float3 lerpResult5_g11770 = lerp( temp_cast_45 , (temp_output_1_0_g11770).rgb , _Saturation);
				float4 appendResult8_g11770 = (float4(lerpResult5_g11770 , temp_output_1_0_g11770.a));
				float4 staticSwitch38_g11765 = appendResult8_g11770;
				#else
				float4 staticSwitch38_g11765 = staticSwitch28_g11765;
				#endif
				float4 temp_output_15_0_g11772 = staticSwitch38_g11765;
				float3 temp_output_82_0_g11772 = (_InnerOutlineColor).rgb;
				float2 temp_output_7_0_g11772 = temp_output_1_0_g11765;
				float temp_output_179_0_g11772 = temp_output_39_0_g11765;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch187_g11772 = ( (tex2D( _InnerOutlineTintTexture, ( temp_output_7_0_g11772 + ( _InnerOutlineTextureSpeed * temp_output_179_0_g11772 ) ) )).rgb * temp_output_82_0_g11772 );
				#else
				float3 staticSwitch187_g11772 = temp_output_82_0_g11772;
				#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11774 = tex2D( _UberNoiseTexture, ( ( ( temp_output_179_0_g11772 * _InnerOutlineNoiseSpeed ) + temp_output_7_0_g11772 ) * _InnerOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11774 = MyCustomExpression16_g11774( linValue16_g11774 );
				float2 staticSwitch169_g11772 = ( ( localMyCustomExpression16_g11774 - 0.5 ) * _InnerOutlineDistortionIntensity );
				#else
				float2 staticSwitch169_g11772 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11772 = ( staticSwitch169_g11772 + temp_output_7_0_g11772 );
				float2 appendResult2_g11773 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11772 = ( 100.0 / appendResult2_g11773 );
				float temp_output_178_0_g11772 = ( _InnerOutlineFade * ( 1.0 - min( min( min( min( min( min( min( tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11772 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) ) );
				float3 lerpResult176_g11772 = lerp( (temp_output_15_0_g11772).rgb , staticSwitch187_g11772 , temp_output_178_0_g11772);
				#ifdef _INNEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch188_g11772 = ( temp_output_178_0_g11772 * temp_output_15_0_g11772.a );
				#else
				float staticSwitch188_g11772 = temp_output_15_0_g11772.a;
				#endif
				#ifdef _ENABLEINNEROUTLINE_ON
				float4 appendResult177_g11772 = (float4(lerpResult176_g11772 , staticSwitch188_g11772));
				float4 staticSwitch12_g11765 = appendResult177_g11772;
				#else
				float4 staticSwitch12_g11765 = staticSwitch38_g11765;
				#endif
				float4 temp_output_15_0_g11797 = staticSwitch12_g11765;
				float3 temp_output_82_0_g11797 = (_OuterOutlineColor).rgb;
				float2 temp_output_7_0_g11797 = temp_output_1_0_g11765;
				float temp_output_186_0_g11797 = temp_output_39_0_g11765;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11797 = ( (tex2D( _OuterOutlineTintTexture, ( temp_output_7_0_g11797 + ( _OuterOutlineTextureSpeed * temp_output_186_0_g11797 ) ) )).rgb * temp_output_82_0_g11797 );
				#else
				float3 staticSwitch199_g11797 = temp_output_82_0_g11797;
				#endif
				float temp_output_182_0_g11797 = ( ( 1.0 - temp_output_15_0_g11797.a ) * min( ( _OuterOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11797 = 1.0;
				#else
				float staticSwitch203_g11797 = temp_output_182_0_g11797;
				#endif
				float3 lerpResult178_g11797 = lerp( (temp_output_15_0_g11797).rgb , staticSwitch199_g11797 , staticSwitch203_g11797);
				float3 lerpResult170_g11797 = lerp( lerpResult178_g11797 , staticSwitch199_g11797 , staticSwitch203_g11797);
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11798 = tex2D( _UberNoiseTexture, ( ( ( temp_output_186_0_g11797 * _OuterOutlineNoiseSpeed ) + temp_output_7_0_g11797 ) * _OuterOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11798 = MyCustomExpression16_g11798( linValue16_g11798 );
				float2 staticSwitch157_g11797 = ( ( localMyCustomExpression16_g11798 - 0.5 ) * _OuterOutlineDistortionIntensity );
				#else
				float2 staticSwitch157_g11797 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11797 = ( staticSwitch157_g11797 + temp_output_7_0_g11797 );
				float2 appendResult2_g11799 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11797 = ( 100.0 / appendResult2_g11799 );
				float lerpResult168_g11797 = lerp( temp_output_15_0_g11797.a , min( ( max( max( max( max( max( max( max( tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11797 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) * 3.0 ) , 1.0 ) , _OuterOutlineFade);
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11797 = ( temp_output_182_0_g11797 * lerpResult168_g11797 );
				#else
				float staticSwitch200_g11797 = lerpResult168_g11797;
				#endif
				#ifdef _ENABLEOUTEROUTLINE_ON
				float4 appendResult174_g11797 = (float4(lerpResult170_g11797 , staticSwitch200_g11797));
				float4 staticSwitch13_g11765 = appendResult174_g11797;
				#else
				float4 staticSwitch13_g11765 = staticSwitch12_g11765;
				#endif
				float4 temp_output_15_0_g11776 = staticSwitch13_g11765;
				float3 temp_output_82_0_g11776 = (_PixelOutlineColor).rgb;
				float2 temp_output_7_0_g11776 = temp_output_1_0_g11765;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11776 = ( (tex2D( _PixelOutlineTintTexture, ( temp_output_7_0_g11776 + ( _PixelOutlineTextureSpeed * temp_output_39_0_g11765 ) ) )).rgb * temp_output_82_0_g11776 );
				#else
				float3 staticSwitch199_g11776 = temp_output_82_0_g11776;
				#endif
				float temp_output_182_0_g11776 = ( ( 1.0 - temp_output_15_0_g11776.a ) * min( ( _PixelOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11776 = 1.0;
				#else
				float staticSwitch203_g11776 = temp_output_182_0_g11776;
				#endif
				float3 lerpResult178_g11776 = lerp( (temp_output_15_0_g11776).rgb , staticSwitch199_g11776 , staticSwitch203_g11776);
				float3 lerpResult170_g11776 = lerp( lerpResult178_g11776 , staticSwitch199_g11776 , staticSwitch203_g11776);
				float2 appendResult206_g11776 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_209_0_g11776 = ( float2( 1,1 ) / appendResult206_g11776 );
				float lerpResult168_g11776 = lerp( temp_output_15_0_g11776.a , min( ( max( max( max( tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,-1 ) ) * temp_output_209_0_g11776 ) ) ).a , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,1 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( -1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) * 3.0 ) , 1.0 ) , _PixelOutlineFade);
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11776 = ( temp_output_182_0_g11776 * lerpResult168_g11776 );
				#else
				float staticSwitch200_g11776 = lerpResult168_g11776;
				#endif
				#ifdef _ENABLEPIXELOUTLINE_ON
				float4 appendResult174_g11776 = (float4(lerpResult170_g11776 , staticSwitch200_g11776));
				float4 staticSwitch48_g11765 = appendResult174_g11776;
				#else
				float4 staticSwitch48_g11765 = staticSwitch13_g11765;
				#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
				float3 lerpResult15_g11766 = lerp( (_PingPongGlowFrom).rgb , (_PingPongGlowTo).rgb , ( ( sin( ( temp_output_39_0_g11765 * _PingPongGlowFrequency ) ) + 1.0 ) / 2.0 ));
				float4 temp_output_5_0_g11766 = staticSwitch48_g11765;
				float4 break2_g11767 = temp_output_5_0_g11766;
				float4 appendResult12_g11766 = (float4(( ( lerpResult15_g11766 * _PingPongGlowFade * pow( ( ( break2_g11767.x + break2_g11767.x + break2_g11767.y + break2_g11767.y + break2_g11767.y + break2_g11767.z ) / 6.0 ) , max( _PingPongGlowContrast , 0.0 ) ) ) + (temp_output_5_0_g11766).rgb ) , temp_output_5_0_g11766.a));
				float4 staticSwitch46_g11765 = appendResult12_g11766;
				#else
				float4 staticSwitch46_g11765 = staticSwitch48_g11765;
				#endif
				float4 temp_output_361_0 = staticSwitch46_g11765;
				#ifdef _ENABLEHOLOGRAM_ON
				float4 temp_output_1_0_g11801 = temp_output_361_0;
				float4 break2_g11802 = temp_output_1_0_g11801;
				float temp_output_9_0_g11803 = max( _HologramContrast , 0.0 );
				float saferPower7_g11803 = abs( ( ( ( break2_g11802.x + break2_g11802.x + break2_g11802.y + break2_g11802.y + break2_g11802.y + break2_g11802.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11803 ) , 0.0 ) ) ) );
				float4 appendResult22_g11801 = (float4(( (_HologramTint).rgb * pow( saferPower7_g11803 , temp_output_9_0_g11803 ) ) , ( max( pow( abs( sin( ( ( ( ( shaderTime237 * _HologramLineSpeed ) + worldPos.y ) / unity_OrthoParams.y ) * _HologramLineFrequency ) ) ) , _HologramLineGap ) , _HologramMinAlpha ) * temp_output_1_0_g11801.a )));
				float4 lerpResult37_g11801 = lerp( temp_output_1_0_g11801 , appendResult22_g11801 , hologramFade182);
				float4 staticSwitch56 = lerpResult37_g11801;
				#else
				float4 staticSwitch56 = temp_output_361_0;
				#endif
				#ifdef _ENABLEGLITCH_ON
				float4 temp_output_1_0_g11804 = staticSwitch56;
				float4 break2_g11806 = temp_output_1_0_g11804;
				float temp_output_34_0_g11804 = shaderTime237;
				float linValue16_g11805 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchNoiseSpeed * temp_output_34_0_g11804 ) ) * _GlitchNoiseScale ) ).r;
				float localMyCustomExpression16_g11805 = MyCustomExpression16_g11805( linValue16_g11805 );
				float3 hsvTorgb3_g11807 = HSVToRGB( float3(( localMyCustomExpression16_g11805 + ( temp_output_34_0_g11804 * _GlitchHueSpeed ) ),1.0,1.0) );
				float3 lerpResult23_g11804 = lerp( (temp_output_1_0_g11804).rgb , ( ( ( break2_g11806.x + break2_g11806.x + break2_g11806.y + break2_g11806.y + break2_g11806.y + break2_g11806.z ) / 6.0 ) * _GlitchBrightness * hsvTorgb3_g11807 ) , glitchFade152);
				float4 appendResult27_g11804 = (float4(lerpResult23_g11804 , temp_output_1_0_g11804.a));
				float4 staticSwitch57 = appendResult27_g11804;
				#else
				float4 staticSwitch57 = staticSwitch56;
				#endif
				float4 temp_output_3_0_g11808 = staticSwitch57;
				float4 temp_output_1_0_g11833 = temp_output_3_0_g11808;
				float2 temp_output_41_0_g11808 = shaderPosition235;
				float2 temp_output_99_0_g11833 = temp_output_41_0_g11808;
				float temp_output_40_0_g11808 = shaderTime237;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
				float linValue16_g11838 = tex2D( _UberNoiseTexture, ( ( ( temp_output_40_0_g11808 * _CamouflageDistortionSpeed ) + temp_output_99_0_g11833 ) * _CamouflageDistortionScale ) ).r;
				float localMyCustomExpression16_g11838 = MyCustomExpression16_g11838( linValue16_g11838 );
				float2 staticSwitch101_g11833 = ( ( ( localMyCustomExpression16_g11838 - 0.25 ) * _CamouflageDistortionIntensity ) + temp_output_99_0_g11833 );
				#else
				float2 staticSwitch101_g11833 = temp_output_99_0_g11833;
				#endif
				float linValue16_g11835 = tex2D( _UberNoiseTexture, ( staticSwitch101_g11833 * _CamouflageNoiseScaleA ) ).r;
				float localMyCustomExpression16_g11835 = MyCustomExpression16_g11835( linValue16_g11835 );
				float clampResult52_g11833 = clamp( ( ( _CamouflageDensityA - localMyCustomExpression16_g11835 ) / max( _CamouflageSmoothnessA , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult55_g11833 = lerp( _CamouflageBaseColor , ( _CamouflageColorA * clampResult52_g11833 ) , clampResult52_g11833);
				float linValue16_g11837 = tex2D( _UberNoiseTexture, ( ( staticSwitch101_g11833 + float2( 12.3,12.3 ) ) * _CamouflageNoiseScaleB ) ).r;
				#ifdef _ENABLECAMOUFLAGE_ON
				float localMyCustomExpression16_g11837 = MyCustomExpression16_g11837( linValue16_g11837 );
				float clampResult65_g11833 = clamp( ( ( _CamouflageDensityB - localMyCustomExpression16_g11837 ) / max( _CamouflageSmoothnessB , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult68_g11833 = lerp( lerpResult55_g11833 , ( _CamouflageColorB * clampResult65_g11833 ) , clampResult65_g11833);
				float4 break2_g11836 = temp_output_1_0_g11833;
				float temp_output_9_0_g11834 = max( _CamouflageContrast , 0.0 );
				float saferPower7_g11834 = abs( ( ( ( break2_g11836.x + break2_g11836.x + break2_g11836.y + break2_g11836.y + break2_g11836.y + break2_g11836.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11834 ) , 0.0 ) ) ) );
				float3 lerpResult4_g11833 = lerp( (temp_output_1_0_g11833).rgb , ( (lerpResult68_g11833).rgb * pow( saferPower7_g11834 , temp_output_9_0_g11834 ) ) , _CamouflageFade);
				float4 appendResult7_g11833 = (float4(lerpResult4_g11833 , temp_output_1_0_g11833.a));
				float4 staticSwitch26_g11808 = appendResult7_g11833;
				#else
				float4 staticSwitch26_g11808 = temp_output_3_0_g11808;
				#endif
				float4 temp_output_1_0_g11827 = staticSwitch26_g11808;
				float temp_output_59_0_g11827 = temp_output_40_0_g11808;
				float2 temp_output_58_0_g11827 = temp_output_41_0_g11808;
				float linValue16_g11828 = tex2D( _UberNoiseTexture, ( ( ( temp_output_59_0_g11827 * _MetalNoiseDistortionSpeed ) + temp_output_58_0_g11827 ) * _MetalNoiseDistortionScale ) ).r;
				float localMyCustomExpression16_g11828 = MyCustomExpression16_g11828( linValue16_g11828 );
				float linValue16_g11830 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11828 - 0.25 ) * _MetalNoiseDistortion ) + ( ( temp_output_59_0_g11827 * _MetalNoiseSpeed ) + temp_output_58_0_g11827 ) ) * _MetalNoiseScale ) ).r;
				float localMyCustomExpression16_g11830 = MyCustomExpression16_g11830( linValue16_g11830 );
				float4 break2_g11829 = temp_output_1_0_g11827;
				float temp_output_5_0_g11827 = ( ( break2_g11829.x + break2_g11829.x + break2_g11829.y + break2_g11829.y + break2_g11829.y + break2_g11829.z ) / 6.0 );
				float temp_output_9_0_g11831 = max( _MetalHighlightContrast , 0.0 );
				float saferPower7_g11831 = abs( ( temp_output_5_0_g11827 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11831 ) , 0.0 ) ) ) );
				float saferPower2_g11827 = abs( temp_output_5_0_g11827 );
				#ifdef _METALMASKTOGGLE_ON
				float2 uv_MetalMask = IN.ase_texcoord9.xy * _MetalMask_ST.xy + _MetalMask_ST.zw;
				float4 tex2DNode3_g11832 = tex2D( _MetalMask, uv_MetalMask );
				float staticSwitch60_g11827 = ( _MetalFade * ( tex2DNode3_g11832.r * tex2DNode3_g11832.a ) );
				#else
				float staticSwitch60_g11827 = _MetalFade;
				#endif
				#ifdef _ENABLEMETAL_ON
				float4 lerpResult45_g11827 = lerp( temp_output_1_0_g11827 , ( ( max( ( ( _MetalHighlightDensity - localMyCustomExpression16_g11830 ) / max( _MetalHighlightDensity , 0.01 ) ) , 0.0 ) * _MetalHighlightColor * pow( saferPower7_g11831 , temp_output_9_0_g11831 ) ) + ( pow( saferPower2_g11827 , _MetalContrast ) * _MetalColor ) ) , staticSwitch60_g11827);
				float4 appendResult8_g11827 = (float4((lerpResult45_g11827).rgb , (temp_output_1_0_g11827).a));
				float4 staticSwitch28_g11808 = appendResult8_g11827;
				#else
				float4 staticSwitch28_g11808 = staticSwitch26_g11808;
				#endif
				#ifdef _ENABLEFROZEN_ON
				float4 temp_output_1_0_g11821 = staticSwitch28_g11808;
				float4 break2_g11822 = temp_output_1_0_g11821;
				float temp_output_7_0_g11821 = ( ( break2_g11822.x + break2_g11822.x + break2_g11822.y + break2_g11822.y + break2_g11822.y + break2_g11822.z ) / 6.0 );
				float temp_output_9_0_g11824 = max( _FrozenContrast , 0.0 );
				float saferPower7_g11824 = abs( ( temp_output_7_0_g11821 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11824 ) , 0.0 ) ) ) );
				float saferPower20_g11821 = abs( temp_output_7_0_g11821 );
				float2 temp_output_72_0_g11821 = temp_output_41_0_g11808;
				float linValue16_g11823 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11821 * _FrozenSnowScale ) ).r;
				float localMyCustomExpression16_g11823 = MyCustomExpression16_g11823( linValue16_g11823 );
				float temp_output_73_0_g11821 = temp_output_40_0_g11808;
				float linValue16_g11825 = tex2D( _UberNoiseTexture, ( ( ( temp_output_73_0_g11821 * _FrozenHighlightDistortionSpeed ) + temp_output_72_0_g11821 ) * _FrozenHighlightDistortionScale ) ).r;
				float localMyCustomExpression16_g11825 = MyCustomExpression16_g11825( linValue16_g11825 );
				float linValue16_g11826 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11825 - 0.25 ) * _FrozenHighlightDistortion ) + ( ( temp_output_73_0_g11821 * _FrozenHighlightSpeed ) + temp_output_72_0_g11821 ) ) * _FrozenHighlightScale ) ).r;
				float localMyCustomExpression16_g11826 = MyCustomExpression16_g11826( linValue16_g11826 );
				float saferPower42_g11821 = abs( temp_output_7_0_g11821 );
				float3 lerpResult57_g11821 = lerp( (temp_output_1_0_g11821).rgb , ( ( pow( saferPower7_g11824 , temp_output_9_0_g11824 ) * (_FrozenTint).rgb ) + ( pow( saferPower20_g11821 , _FrozenSnowContrast ) * ( (_FrozenSnowColor).rgb * max( ( _FrozenSnowDensity - localMyCustomExpression16_g11823 ) , 0.0 ) ) ) + (( max( ( ( _FrozenHighlightDensity - localMyCustomExpression16_g11826 ) / max( _FrozenHighlightDensity , 0.01 ) ) , 0.0 ) * _FrozenHighlightColor * pow( saferPower42_g11821 , _FrozenHighlightContrast ) )).rgb ) , _FrozenFade);
				float4 appendResult26_g11821 = (float4(lerpResult57_g11821 , temp_output_1_0_g11821.a));
				float4 staticSwitch29_g11808 = appendResult26_g11821;
				#else
				float4 staticSwitch29_g11808 = staticSwitch28_g11808;
				#endif
				float4 temp_output_1_0_g11816 = staticSwitch29_g11808;
				float3 temp_output_28_0_g11816 = (temp_output_1_0_g11816).rgb;
				float4 break2_g11820 = float4( temp_output_28_0_g11816 , 0.0 );
				float saferPower21_g11816 = abs( ( ( break2_g11820.x + break2_g11820.x + break2_g11820.y + break2_g11820.y + break2_g11820.y + break2_g11820.z ) / 6.0 ) );
				float2 temp_output_72_0_g11816 = temp_output_41_0_g11808;
				float linValue16_g11819 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnSwirlNoiseScale ) ).r;
				float localMyCustomExpression16_g11819 = MyCustomExpression16_g11819( linValue16_g11819 );
				float linValue16_g11817 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11819 - 0.5 ) * float2( 1,1 ) * _BurnSwirlFactor ) + temp_output_72_0_g11816 ) * _BurnInsideNoiseScale ) ).r;
				#ifdef _ENABLEBURN_ON
				float localMyCustomExpression16_g11817 = MyCustomExpression16_g11817( linValue16_g11817 );
				float clampResult68_g11816 = clamp( ( _BurnInsideNoiseFactor - localMyCustomExpression16_g11817 ) , 0.0 , 1.0 );
				float linValue16_g11818 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnEdgeNoiseScale ) ).r;
				float localMyCustomExpression16_g11818 = MyCustomExpression16_g11818( linValue16_g11818 );
				float temp_output_15_0_g11816 = ( ( ( _BurnRadius - distance( temp_output_72_0_g11816 , _BurnPosition ) ) + ( localMyCustomExpression16_g11818 * _BurnEdgeNoiseFactor ) ) / max( _BurnWidth , 0.01 ) );
				float clampResult18_g11816 = clamp( temp_output_15_0_g11816 , 0.0 , 1.0 );
				float3 lerpResult29_g11816 = lerp( temp_output_28_0_g11816 , ( pow( saferPower21_g11816 , max( _BurnInsideContrast , 0.001 ) ) * ( ( (_BurnInsideNoiseColor).rgb * clampResult68_g11816 ) + (_BurnInsideColor).rgb ) ) , clampResult18_g11816);
				float3 lerpResult40_g11816 = lerp( temp_output_28_0_g11816 , ( lerpResult29_g11816 + ( ( step( temp_output_15_0_g11816 , 1.0 ) * step( 0.0 , temp_output_15_0_g11816 ) ) * (_BurnEdgeColor).rgb ) ) , _BurnFade);
				float4 appendResult43_g11816 = (float4(lerpResult40_g11816 , temp_output_1_0_g11816.a));
				float4 staticSwitch32_g11808 = appendResult43_g11816;
				#else
				float4 staticSwitch32_g11808 = staticSwitch29_g11808;
				#endif
				#ifdef _ENABLERAINBOW_ON
				float2 temp_output_42_0_g11812 = temp_output_41_0_g11808;
				float linValue16_g11813 = tex2D( _UberNoiseTexture, ( temp_output_42_0_g11812 * _RainbowNoiseScale ) ).r;
				float localMyCustomExpression16_g11813 = MyCustomExpression16_g11813( linValue16_g11813 );
				float3 hsvTorgb3_g11815 = HSVToRGB( float3(( ( ( distance( temp_output_42_0_g11812 , _RainbowCenter ) + ( localMyCustomExpression16_g11813 * _RainbowNoiseFactor ) ) * _RainbowDensity ) + ( _RainbowSpeed * temp_output_40_0_g11808 ) ),1.0,1.0) );
				float3 hsvTorgb36_g11812 = RGBToHSV( hsvTorgb3_g11815 );
				float3 hsvTorgb37_g11812 = HSVToRGB( float3(hsvTorgb36_g11812.x,_RainbowSaturation,( hsvTorgb36_g11812.z * _RainbowBrightness )) );
				float4 temp_output_1_0_g11812 = staticSwitch32_g11808;
				float4 break2_g11814 = temp_output_1_0_g11812;
				float saferPower24_g11812 = abs( ( ( break2_g11814.x + break2_g11814.x + break2_g11814.y + break2_g11814.y + break2_g11814.y + break2_g11814.z ) / 6.0 ) );
				float4 appendResult29_g11812 = (float4(( ( hsvTorgb37_g11812 * pow( saferPower24_g11812 , max( _RainbowContrast , 0.001 ) ) * _RainbowFade ) + (temp_output_1_0_g11812).rgb ) , temp_output_1_0_g11812.a));
				float4 staticSwitch34_g11808 = appendResult29_g11812;
				#else
				float4 staticSwitch34_g11808 = staticSwitch32_g11808;
				#endif
				float4 temp_output_1_0_g11809 = staticSwitch34_g11808;
				float3 temp_output_57_0_g11809 = (temp_output_1_0_g11809).rgb;
				float4 break2_g11810 = temp_output_1_0_g11809;
				float3 temp_cast_68 = (( ( break2_g11810.x + break2_g11810.x + break2_g11810.y + break2_g11810.y + break2_g11810.y + break2_g11810.z ) / 6.0 )).xxx;
				float3 lerpResult92_g11809 = lerp( temp_cast_68 , temp_output_57_0_g11809 , _ShineSaturation);
				float3 saferPower83_g11809 = abs( lerpResult92_g11809 );
				float3 temp_cast_69 = (max( _ShineContrast , 0.001 )).xxx;
				float3 rotatedValue69_g11809 = RotateAroundAxis( float3( 0,0,0 ), float3( ( _ShineFrequency * temp_output_41_0_g11808 ) ,  0.0 ), float3( 0,0,1 ), ( ( _ShineRotation / 180.0 ) * UNITY_PI ) );
				float temp_output_103_0_g11809 = ( _ShineFrequency * _ShineWidth );
				float clampResult80_g11809 = clamp( ( ( sin( ( rotatedValue69_g11809.x - ( temp_output_40_0_g11808 * _ShineSpeed * _ShineFrequency ) ) ) - ( 1.0 - temp_output_103_0_g11809 ) ) / temp_output_103_0_g11809 ) , 0.0 , 1.0 );
				#ifdef _SHINEMASKTOGGLE_ON
				float2 uv_ShineMask = IN.ase_texcoord9.xy * _ShineMask_ST.xy + _ShineMask_ST.zw;
				float4 tex2DNode3_g11811 = tex2D( _ShineMask, uv_ShineMask );
				float staticSwitch98_g11809 = ( _ShineFade * ( tex2DNode3_g11811.r * tex2DNode3_g11811.a ) );
				#else
				float staticSwitch98_g11809 = _ShineFade;
				#endif
				#ifdef _ENABLESHINE_ON
				float4 appendResult8_g11809 = (float4(( temp_output_57_0_g11809 + ( ( pow( saferPower83_g11809 , temp_cast_69 ) * (_ShineColor).rgb ) * clampResult80_g11809 * staticSwitch98_g11809 ) ) , (temp_output_1_0_g11809).a));
				float4 staticSwitch36_g11808 = appendResult8_g11809;
				#else
				float4 staticSwitch36_g11808 = staticSwitch34_g11808;
				#endif
				#ifdef _ENABLEPOISON_ON
				float temp_output_41_0_g11839 = temp_output_40_0_g11808;
				float linValue16_g11841 = tex2D( _UberNoiseTexture, ( ( ( temp_output_41_0_g11839 * _PoisonNoiseSpeed ) + temp_output_41_0_g11808 ) * _PoisonNoiseScale ) ).r;
				float localMyCustomExpression16_g11841 = MyCustomExpression16_g11841( linValue16_g11841 );
				float saferPower19_g11839 = abs( abs( ( ( ( localMyCustomExpression16_g11841 + ( temp_output_41_0_g11839 * _PoisonShiftSpeed ) ) % 1.0 ) + -0.5 ) ) );
				float3 temp_output_24_0_g11839 = (_PoisonColor).rgb;
				float4 temp_output_1_0_g11839 = staticSwitch36_g11808;
				float3 temp_output_28_0_g11839 = (temp_output_1_0_g11839).rgb;
				float4 break2_g11840 = float4( temp_output_28_0_g11839 , 0.0 );
				float3 lerpResult32_g11839 = lerp( temp_output_28_0_g11839 , ( temp_output_24_0_g11839 * ( ( break2_g11840.x + break2_g11840.x + break2_g11840.y + break2_g11840.y + break2_g11840.y + break2_g11840.z ) / 6.0 ) ) , ( _PoisonFade * _PoisonRecolorFactor ));
				float4 appendResult27_g11839 = (float4(( ( max( pow( saferPower19_g11839 , _PoisonDensity ) , 0.0 ) * temp_output_24_0_g11839 * _PoisonFade * _PoisonNoiseBrightness ) + lerpResult32_g11839 ) , temp_output_1_0_g11839.a));
				float4 staticSwitch39_g11808 = appendResult27_g11839;
				#else
				float4 staticSwitch39_g11808 = staticSwitch36_g11808;
				#endif
				float4 temp_output_10_0_g11842 = staticSwitch39_g11808;
				float3 temp_output_12_0_g11842 = (temp_output_10_0_g11842).rgb;
				float2 temp_output_2_0_g11842 = temp_output_41_0_g11808;
				float temp_output_1_0_g11842 = temp_output_40_0_g11808;
				float2 temp_output_6_0_g11842 = ( temp_output_1_0_g11842 * _EnchantedSpeed );
				float linValue16_g11845 = tex2D( _UberNoiseTexture, ( ( ( temp_output_2_0_g11842 - ( ( temp_output_6_0_g11842 + float2( 1.234,5.6789 ) ) * float2( 0.95,1.05 ) ) ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11845 = MyCustomExpression16_g11845( linValue16_g11845 );
				float linValue16_g11843 = tex2D( _UberNoiseTexture, ( ( ( temp_output_6_0_g11842 + temp_output_2_0_g11842 ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11843 = MyCustomExpression16_g11843( linValue16_g11843 );
				float temp_output_36_0_g11842 = ( localMyCustomExpression16_g11845 + localMyCustomExpression16_g11843 );
				float temp_output_43_0_g11842 = ( temp_output_36_0_g11842 * 0.5 );
				float3 lerpResult42_g11842 = lerp( (_EnchantedLowColor).rgb , (_EnchantedHighColor).rgb , temp_output_43_0_g11842);
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
				float3 hsvTorgb53_g11842 = HSVToRGB( float3(( ( temp_output_43_0_g11842 * _EnchantedRainbowDensity ) + ( _EnchantedRainbowSpeed * temp_output_1_0_g11842 ) ),_EnchantedRainbowSaturation,1.0) );
				float3 staticSwitch50_g11842 = hsvTorgb53_g11842;
				#else
				float3 staticSwitch50_g11842 = lerpResult42_g11842;
				#endif
				float4 break2_g11844 = temp_output_10_0_g11842;
				float saferPower24_g11842 = abs( ( ( break2_g11844.x + break2_g11844.x + break2_g11844.y + break2_g11844.y + break2_g11844.y + break2_g11844.z ) / 6.0 ) );
				float3 temp_output_40_0_g11842 = ( staticSwitch50_g11842 * pow( saferPower24_g11842 , _EnchantedContrast ) * _EnchantedBrightness );
				float temp_output_45_0_g11842 = ( max( ( temp_output_36_0_g11842 - _EnchantedReduce ) , 0.0 ) * _EnchantedFade );
				#ifdef _ENCHANTEDLERPTOGGLE_ON
				float3 lerpResult44_g11842 = lerp( temp_output_12_0_g11842 , temp_output_40_0_g11842 , temp_output_45_0_g11842);
				float3 staticSwitch47_g11842 = lerpResult44_g11842;
				#else
				float3 staticSwitch47_g11842 = ( temp_output_12_0_g11842 + ( temp_output_40_0_g11842 * temp_output_45_0_g11842 ) );
				#endif
				#ifdef _ENABLEENCHANTED_ON
				float4 appendResult19_g11842 = (float4(staticSwitch47_g11842 , temp_output_10_0_g11842.a));
				float4 staticSwitch11_g11842 = appendResult19_g11842;
				#else
				float4 staticSwitch11_g11842 = temp_output_10_0_g11842;
				#endif
				float4 temp_output_1_0_g11846 = staticSwitch11_g11842;
				float4 break5_g11846 = temp_output_1_0_g11846;
				float3 appendResult32_g11846 = (float3(break5_g11846.r , break5_g11846.g , break5_g11846.b));
				float4 break2_g11847 = temp_output_1_0_g11846;
				float temp_output_4_0_g11846 = ( ( break2_g11847.x + break2_g11847.x + break2_g11847.y + break2_g11847.y + break2_g11847.y + break2_g11847.z ) / 6.0 );
				float temp_output_11_0_g11846 = ( ( ( temp_output_4_0_g11846 + ( temp_output_40_0_g11808 * _ShiftingSpeed ) ) * _ShiftingDensity ) % 1.0 );
				float3 lerpResult20_g11846 = lerp( (_ShiftingColorA).rgb , (_ShiftingColorB).rgb , ( abs( ( temp_output_11_0_g11846 - 0.5 ) ) * 2.0 ));
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
				float3 hsvTorgb12_g11846 = HSVToRGB( float3(temp_output_11_0_g11846,_ShiftingSaturation,_ShiftingBrightness) );
				float3 staticSwitch26_g11846 = hsvTorgb12_g11846;
				#else
				float3 staticSwitch26_g11846 = ( lerpResult20_g11846 * _ShiftingBrightness );
				#endif
				#ifdef _ENABLESHIFTING_ON
				float3 lerpResult31_g11846 = lerp( appendResult32_g11846 , ( staticSwitch26_g11846 * pow( temp_output_4_0_g11846 , _ShiftingContrast ) ) , _ShiftingFade);
				float4 appendResult6_g11846 = (float4(lerpResult31_g11846 , break5_g11846.a));
				float4 staticSwitch33_g11846 = appendResult6_g11846;
				#else
				float4 staticSwitch33_g11846 = temp_output_1_0_g11846;
				#endif
				float4 temp_output_473_0 = staticSwitch33_g11846;
				#ifdef _ENABLEFULLDISTORTION_ON
				float4 break4_g11848 = temp_output_473_0;
				float fullDistortionAlpha164 = _FullDistortionFade;
				float4 appendResult5_g11848 = (float4(break4_g11848.r , break4_g11848.g , break4_g11848.b , ( break4_g11848.a * fullDistortionAlpha164 )));
				float4 staticSwitch77 = appendResult5_g11848;
				#else
				float4 staticSwitch77 = temp_output_473_0;
				#endif
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float4 break4_g11849 = staticSwitch77;
				float directionalDistortionAlpha167 = (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 ));
				float4 appendResult5_g11849 = (float4(break4_g11849.r , break4_g11849.g , break4_g11849.b , ( break4_g11849.a * directionalDistortionAlpha167 )));
				float4 staticSwitch75 = appendResult5_g11849;
				#else
				float4 staticSwitch75 = staticSwitch77;
				#endif
				float4 temp_output_1_0_g11850 = staticSwitch75;
				float4 temp_output_1_0_g11851 = temp_output_1_0_g11850;
				float temp_output_53_0_g11851 = max( _FullAlphaDissolveWidth , 0.001 );
				float2 temp_output_18_0_g11850 = shaderPosition235;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
				float linValue16_g11852 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11852 = MyCustomExpression16_g11852( linValue16_g11852 );
				float clampResult17_g11851 = clamp( ( ( ( _FullAlphaDissolveFade * ( 1.0 + temp_output_53_0_g11851 ) ) - localMyCustomExpression16_g11852 ) / temp_output_53_0_g11851 ) , 0.0 , 1.0 );
				float4 appendResult3_g11851 = (float4((temp_output_1_0_g11851).rgb , ( temp_output_1_0_g11851.a * clampResult17_g11851 )));
				float4 staticSwitch3_g11850 = appendResult3_g11851;
				#else
				float4 staticSwitch3_g11850 = temp_output_1_0_g11850;
				#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
				float linValue16_g11860 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11860 = MyCustomExpression16_g11860( linValue16_g11860 );
				float temp_output_5_0_g11859 = localMyCustomExpression16_g11860;
				float temp_output_61_0_g11859 = step( temp_output_5_0_g11859 , _FullGlowDissolveFade );
				float temp_output_53_0_g11859 = max( ( _FullGlowDissolveFade * _FullGlowDissolveWidth ) , 0.001 );
				float4 temp_output_1_0_g11859 = staticSwitch3_g11850;
				float4 appendResult3_g11859 = (float4(( ( (_FullGlowDissolveEdgeColor).rgb * ( temp_output_61_0_g11859 - step( temp_output_5_0_g11859 , ( ( _FullGlowDissolveFade * ( 1.01 + temp_output_53_0_g11859 ) ) - temp_output_53_0_g11859 ) ) ) ) + (temp_output_1_0_g11859).rgb ) , ( temp_output_1_0_g11859.a * temp_output_61_0_g11859 )));
				float4 staticSwitch5_g11850 = appendResult3_g11859;
				#else
				float4 staticSwitch5_g11850 = staticSwitch3_g11850;
				#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
				float4 temp_output_1_0_g11861 = staticSwitch5_g11850;
				float2 temp_output_76_0_g11861 = temp_output_18_0_g11850;
				float linValue16_g11862 = tex2D( _UberNoiseTexture, ( temp_output_76_0_g11861 * _SourceAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11862 = MyCustomExpression16_g11862( linValue16_g11862 );
				float clampResult17_g11861 = clamp( ( ( _SourceAlphaDissolveFade - ( distance( _SourceAlphaDissolvePosition , temp_output_76_0_g11861 ) + ( localMyCustomExpression16_g11862 * _SourceAlphaDissolveNoiseFactor ) ) ) / max( _SourceAlphaDissolveWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11861 = (float4((temp_output_1_0_g11861).rgb , ( temp_output_1_0_g11861.a * (( _SourceAlphaDissolveInvert )?( ( 1.0 - clampResult17_g11861 ) ):( clampResult17_g11861 )) )));
				float4 staticSwitch8_g11850 = appendResult3_g11861;
				#else
				float4 staticSwitch8_g11850 = staticSwitch5_g11850;
				#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
				float2 temp_output_90_0_g11857 = temp_output_18_0_g11850;
				float linValue16_g11858 = tex2D( _UberNoiseTexture, ( temp_output_90_0_g11857 * _SourceGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11858 = MyCustomExpression16_g11858( linValue16_g11858 );
				float temp_output_65_0_g11857 = ( distance( _SourceGlowDissolvePosition , temp_output_90_0_g11857 ) + ( localMyCustomExpression16_g11858 * _SourceGlowDissolveNoiseFactor ) );
				float temp_output_75_0_g11857 = step( temp_output_65_0_g11857 , _SourceGlowDissolveFade );
				float temp_output_76_0_g11857 = step( temp_output_65_0_g11857 , ( _SourceGlowDissolveFade - max( _SourceGlowDissolveWidth , 0.001 ) ) );
				float4 temp_output_1_0_g11857 = staticSwitch8_g11850;
				float4 appendResult3_g11857 = (float4(( ( max( ( temp_output_75_0_g11857 - temp_output_76_0_g11857 ) , 0.0 ) * (_SourceGlowDissolveEdgeColor).rgb ) + (temp_output_1_0_g11857).rgb ) , ( temp_output_1_0_g11857.a * (( _SourceGlowDissolveInvert )?( ( 1.0 - temp_output_76_0_g11857 ) ):( temp_output_75_0_g11857 )) )));
				float4 staticSwitch9_g11850 = appendResult3_g11857;
				#else
				float4 staticSwitch9_g11850 = staticSwitch8_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
				float4 temp_output_1_0_g11853 = staticSwitch9_g11850;
				float2 temp_output_161_0_g11853 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11853 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_161_0_g11853 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalAlphaFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11853 = rotatedValue136_g11853;
				float linValue16_g11854 = tex2D( _UberNoiseTexture, ( temp_output_161_0_g11853 * _DirectionalAlphaFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11854 = MyCustomExpression16_g11854( linValue16_g11854 );
				float clampResult154_g11853 = clamp( ( ( break130_g11853.x + break130_g11853.y + _DirectionalAlphaFadeFade + ( localMyCustomExpression16_g11854 * _DirectionalAlphaFadeNoiseFactor ) ) / max( _DirectionalAlphaFadeWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11853 = (float4((temp_output_1_0_g11853).rgb , ( temp_output_1_0_g11853.a * (( _DirectionalAlphaFadeInvert )?( ( 1.0 - clampResult154_g11853 ) ):( clampResult154_g11853 )) )));
				float4 staticSwitch11_g11850 = appendResult3_g11853;
				#else
				float4 staticSwitch11_g11850 = staticSwitch9_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
				float2 temp_output_171_0_g11855 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11855 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_171_0_g11855 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalGlowFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11855 = rotatedValue136_g11855;
				float linValue16_g11856 = tex2D( _UberNoiseTexture, ( temp_output_171_0_g11855 * _DirectionalGlowFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11856 = MyCustomExpression16_g11856( linValue16_g11856 );
				float temp_output_168_0_g11855 = max( ( ( break130_g11855.x + break130_g11855.y + _DirectionalGlowFadeFade + ( localMyCustomExpression16_g11856 * _DirectionalGlowFadeNoiseFactor ) ) / max( _DirectionalGlowFadeWidth , 0.001 ) ) , 0.0 );
				float temp_output_161_0_g11855 = step( 0.1 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) );
				float4 temp_output_1_0_g11855 = staticSwitch11_g11850;
				float clampResult154_g11855 = clamp( temp_output_161_0_g11855 , 0.0 , 1.0 );
				float4 appendResult3_g11855 = (float4(( ( (_DirectionalGlowFadeEdgeColor).rgb * ( temp_output_161_0_g11855 - step( 1.0 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) ) ) ) + (temp_output_1_0_g11855).rgb ) , ( temp_output_1_0_g11855.a * clampResult154_g11855 )));
				float4 staticSwitch15_g11850 = appendResult3_g11855;
				#else
				float4 staticSwitch15_g11850 = staticSwitch11_g11850;
				#endif
				float4 temp_output_1_0_g11863 = staticSwitch15_g11850;
				float2 temp_output_126_0_g11863 = temp_output_18_0_g11850;
				float temp_output_121_0_g11863 = max( ( ( _HalftoneFade - distance( _HalftonePosition , temp_output_126_0_g11863 ) ) / max( 0.01 , _HalftoneFadeWidth ) ) , 0.0 );
				float2 appendResult11_g11864 = (float2(temp_output_121_0_g11863 , temp_output_121_0_g11863));
				float temp_output_17_0_g11864 = length( ( (( ( abs( temp_output_126_0_g11863 ) * _HalftoneTiling ) % float2( 1,1 ) )*2.0 + -1.0) / appendResult11_g11864 ) );
				#ifdef _ENABLEHALFTONE_ON
				float clampResult17_g11863 = clamp( saturate( ( ( 1.0 - temp_output_17_0_g11864 ) / fwidth( temp_output_17_0_g11864 ) ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11863 = (float4((temp_output_1_0_g11863).rgb , ( temp_output_1_0_g11863.a * (( _HalftoneInvert )?( ( 1.0 - clampResult17_g11863 ) ):( clampResult17_g11863 )) )));
				float4 staticSwitch13_g11850 = appendResult3_g11863;
				#else
				float4 staticSwitch13_g11850 = staticSwitch15_g11850;
				#endif
				float3 temp_output_3_0_g11866 = (_AddColorColor).rgb;
				#ifdef _ADDCOLORMASKTOGGLE_ON
				float2 uv_AddColorMask = IN.ase_texcoord9.xy * _AddColorMask_ST.xy + _AddColorMask_ST.zw;
				float4 tex2DNode19_g11866 = tex2D( _AddColorMask, uv_AddColorMask );
				float3 staticSwitch16_g11866 = ( temp_output_3_0_g11866 * ( (tex2DNode19_g11866).rgb * tex2DNode19_g11866.a ) );
				#else
				float3 staticSwitch16_g11866 = temp_output_3_0_g11866;
				#endif
				float4 temp_output_1_0_g11866 = staticSwitch13_g11850;
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
				float4 break2_g11868 = temp_output_1_0_g11866;
				float temp_output_9_0_g11867 = max( _AddColorContrast , 0.0 );
				float saferPower7_g11867 = abs( ( ( ( break2_g11868.x + break2_g11868.x + break2_g11868.y + break2_g11868.y + break2_g11868.y + break2_g11868.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11867 ) , 0.0 ) ) ) );
				float3 staticSwitch17_g11866 = ( staticSwitch16_g11866 * pow( saferPower7_g11867 , temp_output_9_0_g11867 ) );
				#else
				float3 staticSwitch17_g11866 = staticSwitch16_g11866;
				#endif
				#ifdef _ENABLEADDCOLOR_ON
				float4 appendResult6_g11866 = (float4(( ( staticSwitch17_g11866 * _AddColorFade ) + (temp_output_1_0_g11866).rgb ) , temp_output_1_0_g11866.a));
				float4 staticSwitch5_g11865 = appendResult6_g11866;
				#else
				float4 staticSwitch5_g11865 = staticSwitch13_g11850;
				#endif
				#ifdef _ENABLEALPHATINT_ON
				float4 temp_output_1_0_g11869 = staticSwitch5_g11865;
				float3 lerpResult4_g11869 = lerp( (temp_output_1_0_g11869).rgb , (_AlphaTintColor).rgb , ( ( 1.0 - temp_output_1_0_g11869.a ) * step( _AlphaTintMinAlpha , temp_output_1_0_g11869.a ) * _AlphaTintFade ));
				float4 appendResult13_g11869 = (float4(lerpResult4_g11869 , temp_output_1_0_g11869.a));
				float4 staticSwitch11_g11865 = appendResult13_g11869;
				#else
				float4 staticSwitch11_g11865 = staticSwitch5_g11865;
				#endif
				float4 temp_output_1_0_g11870 = staticSwitch11_g11865;
				float3 temp_output_6_0_g11870 = (_StrongTintTint).rgb;
				#ifdef _STRONGTINTMASKTOGGLE_ON
				float2 uv_StrongTintMask = IN.ase_texcoord9.xy * _StrongTintMask_ST.xy + _StrongTintMask_ST.zw;
				float4 tex2DNode23_g11870 = tex2D( _StrongTintMask, uv_StrongTintMask );
				float3 staticSwitch21_g11870 = ( temp_output_6_0_g11870 * ( (tex2DNode23_g11870).rgb * tex2DNode23_g11870.a ) );
				#else
				float3 staticSwitch21_g11870 = temp_output_6_0_g11870;
				#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
				float4 break2_g11872 = temp_output_1_0_g11870;
				float temp_output_9_0_g11871 = max( _StrongTintContrast , 0.0 );
				float saferPower7_g11871 = abs( ( ( ( break2_g11872.x + break2_g11872.x + break2_g11872.y + break2_g11872.y + break2_g11872.y + break2_g11872.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11871 ) , 0.0 ) ) ) );
				float3 staticSwitch22_g11870 = ( pow( saferPower7_g11871 , temp_output_9_0_g11871 ) * staticSwitch21_g11870 );
				#else
				float3 staticSwitch22_g11870 = staticSwitch21_g11870;
				#endif
				#ifdef _ENABLESTRONGTINT_ON
				float3 lerpResult7_g11870 = lerp( (temp_output_1_0_g11870).rgb , staticSwitch22_g11870 , _StrongTintFade);
				float4 appendResult9_g11870 = (float4(lerpResult7_g11870 , (temp_output_1_0_g11870).a));
				float4 staticSwitch7_g11865 = appendResult9_g11870;
				#else
				float4 staticSwitch7_g11865 = staticSwitch11_g11865;
				#endif
				float4 temp_output_2_0_g11873 = staticSwitch7_g11865;
				#ifdef _ENABLESHADOW_ON
				float4 break4_g11875 = temp_output_2_0_g11873;
				float3 appendResult5_g11875 = (float3(break4_g11875.r , break4_g11875.g , break4_g11875.b));
				float2 appendResult2_g11874 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float4 appendResult85_g11873 = (float4(_ShadowColor.r , _ShadowColor.g , _ShadowColor.b , ( _ShadowFade * tex2D( _MainTex, ( finalUV146 - ( ( 100.0 / appendResult2_g11874 ) * _ShadowOffset ) ) ).a )));
				float4 break6_g11875 = appendResult85_g11873;
				float3 appendResult7_g11875 = (float3(break6_g11875.r , break6_g11875.g , break6_g11875.b));
				float temp_output_11_0_g11875 = ( ( 1.0 - break4_g11875.a ) * break6_g11875.a );
				float temp_output_32_0_g11875 = ( break4_g11875.a + temp_output_11_0_g11875 );
				float4 appendResult18_g11875 = (float4(( ( ( appendResult5_g11875 * break4_g11875.a ) + ( appendResult7_g11875 * temp_output_11_0_g11875 ) ) * ( 1.0 / max( temp_output_32_0_g11875 , 0.01 ) ) ) , temp_output_32_0_g11875));
				float4 staticSwitch82_g11873 = appendResult18_g11875;
				#else
				float4 staticSwitch82_g11873 = temp_output_2_0_g11873;
				#endif
				float4 break4_g11876 = staticSwitch82_g11873;
				#ifdef _ENABLECUSTOMFADE_ON
				float staticSwitch8_g11756 = 1.0;
				#else
				float staticSwitch8_g11756 = IN.ase_color.a;
				#endif
				#ifdef _ENABLESMOKE_ON
				float staticSwitch9_g11756 = 1.0;
				#else
				float staticSwitch9_g11756 = staticSwitch8_g11756;
				#endif
				float customVertexAlpha193 = staticSwitch9_g11756;
				float4 appendResult5_g11876 = (float4(break4_g11876.r , break4_g11876.g , break4_g11876.b , ( break4_g11876.a * customVertexAlpha193 )));
				float4 temp_output_344_0 = appendResult5_g11876;
				float4 temp_output_1_0_g11877 = temp_output_344_0;
				float4 appendResult8_g11877 = (float4(( (temp_output_1_0_g11877).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11877.a));
				#ifdef _VERTEXTINTFIRST_ON
				float4 staticSwitch342 = temp_output_344_0;
				#else
				float4 staticSwitch342 = appendResult8_g11877;
				#endif
				float4 lerpResult125 = lerp( ( originalColor191 * IN.ase_color ) , staticSwitch342 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float4 staticSwitch143 = staticSwitch342;
				#elif defined(_SHADERFADING_FULL)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_MASK)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_DISSOLVE)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_SPREAD)
				float4 staticSwitch143 = lerpResult125;
				#else
				float4 staticSwitch143 = staticSwitch342;
				#endif
				float4 temp_output_7_0_g11884 = staticSwitch143;
				#ifdef _BAKEDMATERIAL_ON
				float4 appendResult2_g11884 = (float4(( (temp_output_7_0_g11884).rgb / max( temp_output_7_0_g11884.a , 1E-05 ) ) , temp_output_7_0_g11884.a));
				float4 staticSwitch6_g11884 = appendResult2_g11884;
				#else
				float4 staticSwitch6_g11884 = temp_output_7_0_g11884;
				#endif
				float4 temp_output_340_0 = staticSwitch6_g11884;
				
				float2 temp_output_11_0_g11885 = finalUV146;
				
				#ifdef _EMISSIONTOGGLE_ON
				float3 appendResult20_g11885 = (float3(_EmissionTint.r , _EmissionTint.g , _EmissionTint.b));
				float2 uv_EmissionMap = IN.ase_texcoord9.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
				float4 tex2DNode17_g11885 = tex2D( _EmissionMap, uv_EmissionMap );
				float3 appendResult18_g11885 = (float3(tex2DNode17_g11885.r , tex2DNode17_g11885.g , tex2DNode17_g11885.b));
				float3 staticSwitch13_g11885 = ( appendResult20_g11885 * appendResult18_g11885 * tex2DNode17_g11885.a );
				#else
				float3 staticSwitch13_g11885 = float3(0,0,0);
				#endif
				
				float4 tex2DNode7_g11885 = tex2D( _MetallicMap, temp_output_11_0_g11885 );
				#ifdef _METALLICMAPTOGGLE_ON
				float staticSwitch23_g11885 = ( tex2DNode7_g11885.r * _Metallic );
				#else
				float staticSwitch23_g11885 = _Metallic;
				#endif
				
				#ifdef _METALLICMAPTOGGLE_ON
				float staticSwitch22_g11885 = ( _Smoothness * tex2DNode7_g11885.r );
				#else
				float staticSwitch22_g11885 = _Smoothness;
				#endif
				
				o.Albedo = temp_output_340_0.rgb;
				o.Normal = UnpackScaleNormal( tex2D( _NormalMap, temp_output_11_0_g11885 ), _NormalIntensity );
				o.Emission = staticSwitch13_g11885;
				#if defined(_SPECULAR_SETUP)
					o.Specular = fixed3( 0, 0, 0 );
				#else
					o.Metallic = staticSwitch23_g11885;
				#endif
				o.Smoothness = staticSwitch22_g11885;
				o.Occlusion = 1;
				o.Alpha = temp_output_340_0.a;
				float AlphaClipThreshold = _AlphaClip;
				float3 Transmission = 1;
				float3 Translucency = 1;		

				#ifdef _ALPHATEST_ON
					clip( o.Alpha - AlphaClipThreshold );
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = IN.pos.z;
				#endif

				#ifndef USING_DIRECTIONAL_LIGHT
					fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				#else
					fixed3 lightDir = _WorldSpaceLightPos0.xyz;
				#endif

				fixed4 c = 0;
				float3 worldN;
				worldN.x = dot(IN.tSpace0.xyz, o.Normal);
				worldN.y = dot(IN.tSpace1.xyz, o.Normal);
				worldN.z = dot(IN.tSpace2.xyz, o.Normal);
				worldN = normalize(worldN);
				o.Normal = worldN;

				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
				gi.indirect.diffuse = 0;
				gi.indirect.specular = 0;
				gi.light.color = _LightColor0.rgb;
				gi.light.dir = lightDir;
				gi.light.color *= atten;

				#if defined(_SPECULAR_SETUP)
					c += LightingStandardSpecular( o, worldViewDir, gi );
				#else
					c += LightingStandard( o, worldViewDir, gi );
				#endif
				
				#ifdef _TRANSMISSION_ASE
				{
					float shadow = _TransmissionShadow;
					#ifdef DIRECTIONAL
						float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
					#else
						float3 lightAtten = gi.light.color;
					#endif
					half3 transmission = max(0 , -dot(o.Normal, gi.light.dir)) * lightAtten * Transmission;
					c.rgb += o.Albedo * transmission;
				}
				#endif

				#ifdef _TRANSLUCENCY_ASE
				{
					float shadow = _TransShadow;
					float normal = _TransNormal;
					float scattering = _TransScattering;
					float direct = _TransDirect;
					float ambient = _TransAmbient;
					float strength = _TransStrength;

					#ifdef DIRECTIONAL
						float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
					#else
						float3 lightAtten = gi.light.color;
					#endif
					half3 lightDir = gi.light.dir + o.Normal * normal;
					half transVdotL = pow( saturate( dot( worldViewDir, -lightDir ) ), scattering );
					half3 translucency = lightAtten * (transVdotL * direct + gi.indirect.diffuse * ambient) * Translucency;
					c.rgb += o.Albedo * translucency * strength;
				}
				#endif

				//#ifdef _REFRACTION_ASE
				//	float4 projScreenPos = ScreenPos / ScreenPos.w;
				//	float3 refractionOffset = ( RefractionIndex - 1.0 ) * mul( UNITY_MATRIX_V, WorldNormal ).xyz * ( 1.0 - dot( WorldNormal, WorldViewDirection ) );
				//	projScreenPos.xy += refractionOffset.xy;
				//	float3 refraction = UNITY_SAMPLE_SCREENSPACE_TEXTURE( _GrabTexture, projScreenPos ) * RefractionColor;
				//	color.rgb = lerp( refraction, color.rgb, color.a );
				//	color.a = 1;
				//#endif

				#ifdef ASE_FOG
					UNITY_APPLY_FOG(IN.fogCoord, c);
				#endif
				return c;
			}
			ENDCG
		}

		
		Pass
		{
			
			Name "Deferred"
			Tags { "LightMode"="Deferred" }

			AlphaToMask Off

			CGPROGRAM
			#define ASE_NEEDS_FRAG_SHADOWCOORDS
			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _ALPHATEST_ON 1

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma exclude_renderers nomrt 
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#pragma multi_compile_prepassfinal
			#ifndef UNITY_PASS_DEFERRED
				#define UNITY_PASS_DEFERRED
			#endif
			#include "HLSLSupport.cginc"
			#if !defined( UNITY_INSTANCED_LOD_FADE )
				#define UNITY_INSTANCED_LOD_FADE
			#endif
			#if !defined( UNITY_INSTANCED_SH )
				#define UNITY_INSTANCED_SH
			#endif
			#if !defined( UNITY_INSTANCED_LIGHTMAPSTS )
				#define UNITY_INSTANCED_LIGHTMAPSTS
			#endif
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"

			#include "UnityStandardUtils.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_FRAG_POSITION
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _SHADERFADING_NONE _SHADERFADING_FULL _SHADERFADING_MASK _SHADERFADING_DISSOLVE _SHADERFADING_SPREAD
			#pragma shader_feature_local _ENABLESINESCALE_ON
			#pragma shader_feature _ENABLEVIBRATE_ON
			#pragma shader_feature _ENABLESINEMOVE_ON
			#pragma shader_feature _ENABLESQUISH_ON
			#pragma shader_feature _SPRITESHEETFIX_ON
			#pragma shader_feature_local _PIXELPERFECTUV_ON
			#pragma shader_feature _ENABLEWORLDTILING_ON
			#pragma shader_feature _ENABLESCREENTILING_ON
			#pragma shader_feature _TOGGLETIMEFREQUENCY_ON
			#pragma shader_feature _TOGGLETIMEFPS_ON
			#pragma shader_feature _TOGGLETIMESPEED_ON
			#pragma shader_feature _TOGGLEUNSCALEDTIME_ON
			#pragma shader_feature _TOGGLECUSTOMTIME_ON
			#pragma shader_feature _SHADERSPACE_UV _SHADERSPACE_UV_RAW _SHADERSPACE_OBJECT _SHADERSPACE_OBJECT_SCALED _SHADERSPACE_WORLD _SHADERSPACE_UI_GRAPHIC _SHADERSPACE_SCREEN
			#pragma shader_feature _PIXELPERFECTSPACE_ON
			#pragma shader_feature _BAKEDMATERIAL_ON
			#pragma shader_feature _VERTEXTINTFIRST_ON
			#pragma shader_feature _ENABLESHADOW_ON
			#pragma shader_feature _ENABLESTRONGTINT_ON
			#pragma shader_feature _ENABLEALPHATINT_ON
			#pragma shader_feature_local _ENABLEADDCOLOR_ON
			#pragma shader_feature_local _ENABLEHALFTONE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALGLOWFADE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALALPHAFADE_ON
			#pragma shader_feature_local _ENABLESOURCEGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLESOURCEALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALDISTORTION_ON
			#pragma shader_feature_local _ENABLEFULLDISTORTION_ON
			#pragma shader_feature _ENABLESHIFTING_ON
			#pragma shader_feature _ENABLEENCHANTED_ON
			#pragma shader_feature_local _ENABLEPOISON_ON
			#pragma shader_feature_local _ENABLESHINE_ON
			#pragma shader_feature_local _ENABLERAINBOW_ON
			#pragma shader_feature_local _ENABLEBURN_ON
			#pragma shader_feature_local _ENABLEFROZEN_ON
			#pragma shader_feature_local _ENABLEMETAL_ON
			#pragma shader_feature_local _ENABLECAMOUFLAGE_ON
			#pragma shader_feature_local _ENABLEGLITCH_ON
			#pragma shader_feature_local _ENABLEHOLOGRAM_ON
			#pragma shader_feature _ENABLEPINGPONGGLOW_ON
			#pragma shader_feature_local _ENABLEPIXELOUTLINE_ON
			#pragma shader_feature_local _ENABLEOUTEROUTLINE_ON
			#pragma shader_feature_local _ENABLEINNEROUTLINE_ON
			#pragma shader_feature_local _ENABLESATURATION_ON
			#pragma shader_feature_local _ENABLESINEGLOW_ON
			#pragma shader_feature_local _ENABLEADDHUE_ON
			#pragma shader_feature_local _ENABLESHIFTHUE_ON
			#pragma shader_feature_local _ENABLEINKSPREAD_ON
			#pragma shader_feature_local _ENABLEBLACKTINT_ON
			#pragma shader_feature_local _ENABLESPLITTONING_ON
			#pragma shader_feature_local _ENABLEHUE_ON
			#pragma shader_feature_local _ENABLEBRIGHTNESS_ON
			#pragma shader_feature_local _ENABLECONTRAST_ON
			#pragma shader_feature _ENABLENEGATIVE_ON
			#pragma shader_feature_local _ENABLECOLORREPLACE_ON
			#pragma shader_feature_local _ENABLERECOLORRGBYCP_ON
			#pragma shader_feature _ENABLERECOLORRGB_ON
			#pragma shader_feature_local _ENABLEFLAME_ON
			#pragma shader_feature_local _ENABLECHECKERBOARD_ON
			#pragma shader_feature_local _ENABLECUSTOMFADE_ON
			#pragma shader_feature_local _ENABLESMOKE_ON
			#pragma shader_feature _ENABLESHARPEN_ON
			#pragma shader_feature _ENABLEGAUSSIANBLUR_ON
			#pragma shader_feature _ENABLESMOOTHPIXELART_ON
			#pragma shader_feature_local _TILINGFIX_ON
			#pragma shader_feature _ENABLEWIGGLE_ON
			#pragma shader_feature_local _ENABLEUVSCALE_ON
			#pragma shader_feature_local _ENABLEPIXELATE_ON
			#pragma shader_feature_local _ENABLEUVSCROLL_ON
			#pragma shader_feature_local _ENABLEUVROTATE_ON
			#pragma shader_feature_local _ENABLESINEROTATE_ON
			#pragma shader_feature_local _ENABLESQUEEZE_ON
			#pragma shader_feature_local _ENABLEUVDISTORT_ON
			#pragma shader_feature_local _ENABLEWIND_ON
			#pragma shader_feature_local _WINDLOCALWIND_ON
			#pragma shader_feature_local _WINDHIGHQUALITYNOISE_ON
			#pragma shader_feature_local _WINDISPARALLAX_ON
			#pragma shader_feature _UVDISTORTMASKTOGGLE_ON
			#pragma shader_feature _WIGGLEFIXEDGROUNDTOGGLE_ON
			#pragma shader_feature _RECOLORRGBTEXTURETOGGLE_ON
			#pragma shader_feature _RECOLORRGBYCPTEXTURETOGGLE_ON
			#pragma shader_feature_local _ADDHUEMASKTOGGLE_ON
			#pragma shader_feature_local _SINEGLOWMASKTOGGLE_ON
			#pragma shader_feature _INNEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature_local _INNEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _INNEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _OUTEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature_local _OUTEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _PIXELOUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _CAMOUFLAGEANIMATIONTOGGLE_ON
			#pragma shader_feature _METALMASKTOGGLE_ON
			#pragma shader_feature _SHINEMASKTOGGLE_ON
			#pragma shader_feature _ENCHANTEDLERPTOGGLE_ON
			#pragma shader_feature _ENCHANTEDRAINBOWTOGGLE_ON
			#pragma shader_feature _SHIFTINGRAINBOWTOGGLE_ON
			#pragma shader_feature _ADDCOLORCONTRASTTOGGLE_ON
			#pragma shader_feature _ADDCOLORMASKTOGGLE_ON
			#pragma shader_feature _STRONGTINTCONTRASTTOGGLE_ON
			#pragma shader_feature _STRONGTINTMASKTOGGLE_ON
			#pragma shader_feature _EMISSIONTOGGLE_ON
			#pragma shader_feature _METALLICMAPTOGGLE_ON

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				#if UNITY_VERSION >= 201810
					UNITY_POSITION(pos);
				#else
					float4 pos : SV_POSITION;
				#endif
				float4 lmap : TEXCOORD2;
				#ifndef LIGHTMAP_ON
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						half3 sh : TEXCOORD3;
					#endif
				#else
					#ifdef DIRLIGHTMAP_OFF
						float4 lmapFadePos : TEXCOORD4;
					#endif
				#endif
				float4 tSpace0 : TEXCOORD5;
				float4 tSpace1 : TEXCOORD6;
				float4 tSpace2 : TEXCOORD7;
				float4 ase_texcoord8 : TEXCOORD8;
				float4 ase_texcoord9 : TEXCOORD9;
				float4 ase_texcoord10 : TEXCOORD10;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#ifdef LIGHTMAP_ON
			float4 unity_LightmapFade;
			#endif
			fixed4 unity_Ambient;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishStretch;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingScale;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingOffset;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float _ScreenTilingPixelsPerUnit;
			#endif
			uniform sampler2D _MainTex;
			float4 _MainTex_TexelSize;
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingScale;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingOffset;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float _WorldTilingPixelsPerUnit;
			#endif
			uniform float4 _SpriteSheetRect;
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFade;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFlip;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishSquish;
			#endif
				#ifdef _TOGGLECUSTOMTIME_ON
			uniform float _TimeValue;
			#endif
			uniform float UnscaledTime;
				#ifdef _TOGGLETIMESPEED_ON
			uniform float _TimeSpeed;
			#endif
				#ifdef _TOGGLETIMEFPS_ON
			uniform float _TimeFPS;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeFrequency;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeRange;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveFrequency;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveOffset;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float _SineMoveFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFrequency;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateOffset;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateRotation;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float _SineScaleFrequency;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float2 _SineScaleFactor;
			#endif
			uniform float _FadingFade;
			uniform sampler2D _FadingMask;
			uniform float4 _FadingMask_ST;
			uniform float _FadingWidth;
			uniform sampler2D _UberNoiseTexture;
			uniform float _PixelsPerUnit;
			uniform float _RectWidth;
			uniform float _RectHeight;
			uniform float _ScreenWidthUnits;
			uniform float2 _FadingNoiseScale;
			uniform float2 _FadingPosition;
			uniform float _FadingNoiseFactor;
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotationWindFactor;
			#endif
			uniform float WindMinIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMinIntensity;
			#endif
			uniform float WindMaxIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMaxIntensity;
			#endif
				#ifdef _WINDISPARALLAX_ON
			uniform float _WindXPosition;
			#endif
			uniform float WindNoiseScale;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseScale;
			#endif
			uniform float WindTime;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseSpeed;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindMaxRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindFlip;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishFactor;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishWindFactor;
			#endif
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float _FullDistortionFade;
			#endif
			uniform float2 _FullDistortionNoiseScale;
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float2 _FullDistortionDistortion;
			#endif
			uniform float2 _DirectionalDistortionDistortionScale;
			uniform float _DirectionalDistortionRandomDirection;
			uniform float2 _DirectionalDistortionDistortion;
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
			uniform float _DirectionalDistortionInvert;
			#endif
			uniform float _DirectionalDistortionRotation;
			uniform float _DirectionalDistortionFade;
			uniform float2 _DirectionalDistortionNoiseScale;
			uniform float _DirectionalDistortionNoiseFactor;
			uniform float _DirectionalDistortionWidth;
			uniform float _HologramDistortionSpeed;
			uniform float _HologramDistortionDensity;
			uniform float _HologramDistortionScale;
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramDistortionOffset;
			#endif
			uniform float _HologramFade;
			uniform float2 _GlitchDistortionSpeed;
			uniform float2 _GlitchDistortionScale;
			uniform float2 _GlitchDistortion;
			uniform float2 _GlitchMaskSpeed;
			uniform float2 _GlitchMaskScale;
			uniform float _GlitchMaskMin;
			uniform float _GlitchFade;
			uniform float2 _UVDistortFrom;
			uniform float2 _UVDistortTo;
			uniform float2 _UVDistortSpeed;
			uniform float2 _UVDistortNoiseScale;
			uniform float _UVDistortFade;
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform sampler2D _UVDistortMask;
			#endif
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform float4 _UVDistortMask_ST;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeCenter;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezePower;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeScale;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezeFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFrequency;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateAngle;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float2 _SineRotatePivot;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float _UVRotateSpeed;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float2 _UVRotatePivot;
			#endif
				#ifdef _ENABLEUVSCROLL_ON
			uniform float2 _UVScrollSpeed;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelDensity;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelsPerUnit;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelateFade;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScalePivot;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScaleScale;
			#endif
			uniform float _WiggleFrequency;
			uniform float _WiggleSpeed;
			uniform float _WiggleOffset;
			uniform float _WiggleFade;
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurOffset;
			#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurFade;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenOffset;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFactor;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFade;
			#endif
			uniform float _SmokeVertexSeed;
			uniform float _SmokeNoiseScale;
			uniform float _SmokeNoiseFactor;
			uniform float _SmokeSmoothness;
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeDarkEdge;
			#endif
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeAlpha;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform sampler2D _CustomFadeFadeMask;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float2 _CustomFadeNoiseScale;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeNoiseFactor;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeSmoothness;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeAlpha;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardDarken;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardTiling;
			#endif
			uniform float2 _FlameSpeed;
			uniform float2 _FlameNoiseScale;
			uniform float _FlameNoiseHeightFactor;
			uniform float _FlameNoiseFactor;
			uniform float _FlameRadius;
			uniform float _FlameSmooth;
				#ifdef _ENABLEFLAME_ON
			uniform float _FlameBrightness;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBRedTint;
			#endif
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBTexture;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBGreenTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBBlueTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float _RecolorRGBFade;
			#endif
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBYCPTexture;
			#endif
			uniform float4 _RecolorRGBYCPPurpleTint;
			uniform float4 _RecolorRGBYCPBlueTint;
			uniform float4 _RecolorRGBYCPCyanTint;
			uniform float4 _RecolorRGBYCPGreenTint;
			uniform float4 _RecolorRGBYCPYellowTint;
			uniform float4 _RecolorRGBYCPRedTint;
				#ifdef _ENABLERECOLORRGBYCP_ON
			uniform float _RecolorRGBYCPFade;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceFromColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceContrast;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceToColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceSmoothness;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceRange;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceFade;
			#endif
				#ifdef _ENABLENEGATIVE_ON
			uniform float _NegativeFade;
			#endif
				#ifdef _ENABLECONTRAST_ON
			uniform float _Contrast;
			#endif
				#ifdef _ENABLEBRIGHTNESS_ON
			uniform float _Brightness;
			#endif
				#ifdef _ENABLEHUE_ON
			uniform float _Hue;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningShadowsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningHighlightsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningShift;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningBalance;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningContrast;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningFade;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float4 _BlackTintColor;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintPower;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float4 _InkSpreadColor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadContrast;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadDistance;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadPosition;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadNoiseScale;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadNoiseFactor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadWidth;
			#endif
				#ifdef _ENABLESHIFTHUE_ON
			uniform float _ShiftHueSpeed;
			#endif
			uniform float _AddHueSpeed;
			uniform float _AddHueSaturation;
			uniform float _AddHueBrightness;
				#ifdef _ENABLEADDHUE_ON
			uniform float _AddHueContrast;
			#endif
			uniform float _AddHueFade;
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform sampler2D _AddHueMask;
			#endif
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform float4 _AddHueMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowContrast;
			#endif
			uniform float4 _SineGlowColor;
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform sampler2D _SineGlowMask;
			#endif
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform float4 _SineGlowMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFade;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFrequency;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMax;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMin;
			#endif
				#ifdef _ENABLESATURATION_ON
			uniform float _Saturation;
			#endif
			uniform float4 _InnerOutlineColor;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _InnerOutlineTintTexture;
			#endif
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform float2 _InnerOutlineTextureSpeed;
			#endif
			uniform float _InnerOutlineFade;
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseSpeed;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseScale;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineDistortionIntensity;
			#endif
			uniform float _InnerOutlineWidth;
			uniform float4 _OuterOutlineColor;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _OuterOutlineTintTexture;
			#endif
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform float2 _OuterOutlineTextureSpeed;
			#endif
			uniform float _OuterOutlineFade;
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseSpeed;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseScale;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineDistortionIntensity;
			#endif
			uniform float _OuterOutlineWidth;
			uniform float4 _PixelOutlineColor;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform sampler2D _PixelOutlineTintTexture;
			#endif
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform float2 _PixelOutlineTextureSpeed;
			#endif
			uniform float _PixelOutlineFade;
			uniform float _PixelOutlineWidth;
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowFrom;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowTo;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFrequency;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFade;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float4 _HologramTint;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineSpeed;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineFrequency;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineGap;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramMinAlpha;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchBrightness;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseSpeed;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseScale;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchHueSpeed;
			#endif
			uniform float4 _CamouflageBaseColor;
			uniform float4 _CamouflageColorA;
			uniform float _CamouflageDensityA;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionSpeed;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionScale;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionIntensity;
			#endif
			uniform float2 _CamouflageNoiseScaleA;
			uniform float _CamouflageSmoothnessA;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float4 _CamouflageColorB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageDensityB;
			#endif
			uniform float2 _CamouflageNoiseScaleB;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageSmoothnessB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageContrast;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageFade;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalHighlightDensity;
			#endif
			uniform float2 _MetalNoiseDistortionSpeed;
			uniform float2 _MetalNoiseDistortionScale;
			uniform float2 _MetalNoiseDistortion;
			uniform float2 _MetalNoiseSpeed;
			uniform float2 _MetalNoiseScale;
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalHighlightColor;
			#endif
			uniform float _MetalHighlightContrast;
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalContrast;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalColor;
			#endif
			uniform float _MetalFade;
				#ifdef _METALMASKTOGGLE_ON
			uniform sampler2D _MetalMask;
			#endif
				#ifdef _METALMASKTOGGLE_ON
			uniform float4 _MetalMask_ST;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenTint;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenSnowColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenSnowScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortion;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenHighlightColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenFade;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideContrast;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideNoiseColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideNoiseFactor;
			#endif
			uniform float2 _BurnSwirlNoiseScale;
			uniform float _BurnSwirlFactor;
			uniform float2 _BurnInsideNoiseScale;
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnRadius;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnPosition;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnEdgeNoiseScale;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnEdgeNoiseFactor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnWidth;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnEdgeColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnFade;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowCenter;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowNoiseScale;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowNoiseFactor;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowDensity;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSpeed;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSaturation;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowBrightness;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowContrast;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowFade;
			#endif
			uniform float _ShineSaturation;
			uniform float _ShineContrast;
				#ifdef _ENABLESHINE_ON
			uniform float4 _ShineColor;
			#endif
			uniform float _ShineRotation;
			uniform float _ShineFrequency;
			uniform float _ShineSpeed;
			uniform float _ShineWidth;
			uniform float _ShineFade;
				#ifdef _SHINEMASKTOGGLE_ON
			uniform sampler2D _ShineMask;
			#endif
				#ifdef _SHINEMASKTOGGLE_ON
			uniform float4 _ShineMask_ST;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseScale;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonShiftSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonDensity;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float4 _PoisonColor;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonFade;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonNoiseBrightness;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonRecolorFactor;
			#endif
			uniform float4 _EnchantedLowColor;
			uniform float4 _EnchantedHighColor;
			uniform float2 _EnchantedSpeed;
			uniform float2 _EnchantedScale;
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowDensity;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSpeed;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSaturation;
			#endif
			uniform float _EnchantedContrast;
			uniform float _EnchantedBrightness;
			uniform float _EnchantedReduce;
			uniform float _EnchantedFade;
			uniform float4 _ShiftingColorA;
			uniform float4 _ShiftingColorB;
			uniform float _ShiftingSpeed;
			uniform float _ShiftingDensity;
			uniform float _ShiftingBrightness;
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
			uniform float _ShiftingSaturation;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingContrast;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingFade;
			#endif
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float _FullAlphaDissolveFade;
			#endif
			uniform float _FullAlphaDissolveWidth;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float2 _FullAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float4 _FullGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float2 _FullGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveFade;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveInvert;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveFade;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveFade;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float4 _SourceGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float2 _DirectionalAlphaFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeWidth;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float4 _DirectionalGlowFadeEdgeColor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float2 _DirectionalGlowFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeWidth;
			#endif
				#ifdef _ENABLEHALFTONE_ON
			uniform float _HalftoneInvert;
			#endif
			uniform float _HalftoneTiling;
			uniform float _HalftoneFade;
			uniform float2 _HalftonePosition;
			uniform float _HalftoneFadeWidth;
			uniform float4 _AddColorColor;
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform sampler2D _AddColorMask;
			#endif
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform float4 _AddColorMask_ST;
			#endif
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
			uniform float _AddColorContrast;
			#endif
				#ifdef _ENABLEADDCOLOR_ON
			uniform float _AddColorFade;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float4 _AlphaTintColor;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintMinAlpha;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintFade;
			#endif
			uniform float4 _StrongTintTint;
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform sampler2D _StrongTintMask;
			#endif
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform float4 _StrongTintMask_ST;
			#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
			uniform float _StrongTintContrast;
			#endif
				#ifdef _ENABLESTRONGTINT_ON
			uniform float _StrongTintFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float4 _ShadowColor;
			#endif
			uniform float _ShadowFade;
				#ifdef _ENABLESHADOW_ON
			uniform float2 _ShadowOffset;
			#endif
			uniform sampler2D _NormalMap;
			uniform float _NormalIntensity;
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionTint;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform sampler2D _EmissionMap;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionMap_ST;
			#endif
			uniform float _Metallic;
			uniform sampler2D _MetallicMap;
			uniform float _Smoothness;
			uniform float _AlphaClip;

	
			float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
			{
				original -= center;
				float C = cos( angle );
				float S = sin( angle );
				float t = 1 - C;
				float m00 = t * u.x * u.x + C;
				float m01 = t * u.x * u.y - S * u.z;
				float m02 = t * u.x * u.z + S * u.y;
				float m10 = t * u.x * u.y + S * u.z;
				float m11 = t * u.y * u.y + C;
				float m12 = t * u.y * u.z - S * u.x;
				float m20 = t * u.x * u.z - S * u.y;
				float m21 = t * u.y * u.z + S * u.x;
				float m22 = t * u.z * u.z + C;
				float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
				return mul( finalMatrix, original ) + center;
			}
			
			float MyCustomExpression16_g11717( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11719( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float FastNoise101_g11668( float x )
			{
				float i = floor(x);
				float f = frac(x);
				float s = sign(frac(x/2.0)-0.5);
				    
				float k = 0.5+0.5*sin(i);
				return s*f*(f-1.0)*((16.0*k-4.0)*f*(f-1.0)-1.0);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			
			float MyCustomExpression16_g11670( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11671( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11674( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11673( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11679( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11680( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11721( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11676( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11732( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float4 texturePointSmooth( sampler2D tex, float4 textureTexelSize, float2 uvs )
			{
				float2 size;
				size.x = textureTexelSize.z;
				size.y = textureTexelSize.w;
				float2 pixel = float2(1.0,1.0) / size;
				uvs -= pixel * float2(0.5,0.5);
				float2 uv_pixels = uvs * size;
				float2 delta_pixel = frac(uv_pixels) - float2(0.5,0.5);
				float2 ddxy = fwidth(uv_pixels);
				float2 mip = log2(ddxy) - 0.5;
				float2 clampedUV = uvs + (clamp(delta_pixel / ddxy, 0.0, 1.0) - delta_pixel) * pixel;
				return tex2Dlod(tex, float4(clampedUV,0, min(mip.x, mip.y)));
			}
			
			float MyCustomExpression16_g11758( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11760( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11764( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 MyCustomExpression115_g11768( float3 In, float3 From, float3 To, float Fuzziness, float Range )
			{
				float Distance = distance(From, In);
				return lerp(To, In, saturate((Distance - Range) / max(Fuzziness, 0.001)));
			}
			
			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float MyCustomExpression16_g11787( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11774( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11798( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11805( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11838( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11835( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11837( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11828( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11830( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11823( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11825( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11826( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11819( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11817( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11818( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11813( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11841( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11845( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11843( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11852( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11860( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11862( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11858( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11854( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11856( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			

			v2f VertexFunction (appdata v  ) {
				UNITY_SETUP_INSTANCE_ID(v);
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 _ZeroVector = float2(0,0);
				float2 texCoord363 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLESCREENTILING_ON
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLEWORLDTILING_ON
				float2 staticSwitch2_g11657 = ( ( ( (ase_worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _PIXELPERFECTUV_ON
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				#ifdef _SPRITESHEETFIX_ON
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				#ifdef _ENABLESQUISH_ON
				float2 break77_g11879 = fixedUV475;
				float2 appendResult72_g11879 = (float2(( _SquishStretch * ( break77_g11879.x - 0.5 ) * _SquishFade ) , ( _SquishFade * ( break77_g11879.y + _SquishFlip ) * -_SquishSquish )));
				float2 staticSwitch198 = ( appendResult72_g11879 + _ZeroVector );
				#else
				float2 staticSwitch198 = _ZeroVector;
				#endif
				float2 temp_output_2_0_g11880 = staticSwitch198;
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				float temp_output_8_0_g11880 = shaderTime237;
				#ifdef _ENABLESINEMOVE_ON
				float2 staticSwitch4_g11880 = ( ( sin( ( temp_output_8_0_g11880 * _SineMoveFrequency ) ) * _SineMoveOffset * _SineMoveFade ) + temp_output_2_0_g11880 );
				#else
				float2 staticSwitch4_g11880 = temp_output_2_0_g11880;
				#endif
				#ifdef _ENABLEVIBRATE_ON
				float temp_output_30_0_g11881 = temp_output_8_0_g11880;
				float3 rotatedValue21_g11881 = RotateAroundAxis( float3( 0,0,0 ), float3( 0,1,0 ), float3( 0,0,1 ), ( temp_output_30_0_g11881 * _VibrateRotation ) );
				float2 staticSwitch6_g11880 = ( ( sin( ( _VibrateFrequency * temp_output_30_0_g11881 ) ) * _VibrateOffset * _VibrateFade * (rotatedValue21_g11881).xy ) + staticSwitch4_g11880 );
				#else
				float2 staticSwitch6_g11880 = staticSwitch4_g11880;
				#endif
				#ifdef _ENABLESINESCALE_ON
				float2 staticSwitch10_g11880 = ( staticSwitch6_g11880 + ( (v.vertex.xyz).xy * ( ( ( sin( ( _SineScaleFrequency * temp_output_8_0_g11880 ) ) + 1.0 ) * 0.5 ) * _SineScaleFactor ) ) );
				#else
				float2 staticSwitch10_g11880 = staticSwitch6_g11880;
				#endif
				float2 temp_output_424_0 = staticSwitch10_g11880;
				float2 uv_FadingMask = v.ase_texcoord.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2Dlod( _FadingMask, float4( uv_FadingMask, 0, 0.0) );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float2 texCoord435 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				#ifdef _PIXELPERFECTSPACE_ON
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (v.vertex.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (v.vertex.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (ase_worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float linValue16_g11717 = tex2Dlod( _UberNoiseTexture, float4( ( shaderPosition235 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2Dlod( _UberNoiseTexture, float4( ( temp_output_27_0_g11718 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult121 = lerp( float2( 0,0 ) , temp_output_424_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch142 = temp_output_424_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch142 = lerpResult121;
				#else
				float2 staticSwitch142 = temp_output_424_0;
				#endif
				
				o.ase_texcoord9 = screenPos;
				
				o.ase_texcoord8.xy = v.ase_texcoord.xy;
				o.ase_texcoord10 = v.vertex;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord8.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = float3( staticSwitch142 ,  0.0 );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.vertex.w = 1;
				v.normal = v.normal;
				v.tangent = v.tangent;

				o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
				o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				#ifdef DYNAMICLIGHTMAP_ON
					o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#else
					o.lmap.zw = 0;
				#endif
				#ifdef LIGHTMAP_ON
					o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					#ifdef DIRLIGHTMAP_OFF
						o.lmapFadePos.xyz = (mul(unity_ObjectToWorld, v.vertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w;
						o.lmapFadePos.w = (-UnityObjectToViewPos(v.vertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w);
					#endif
				#else
					o.lmap.xy = 0;
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						o.sh = 0;
						o.sh = ShadeSHPerVertex (worldNormal, o.sh);
					#endif
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( appdata v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.tangent = v.tangent;
				o.normal = v.normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				appdata o = (appdata) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
				o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			v2f vert ( appdata v )
			{
				return VertexFunction( v );
			}
			#endif

			void frag (v2f IN 
				, out half4 outGBuffer0 : SV_Target0
				, out half4 outGBuffer1 : SV_Target1
				, out half4 outGBuffer2 : SV_Target2
				, out half4 outEmission : SV_Target3
				#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
				, out half4 outShadowMask : SV_Target4
				#endif
				#ifdef _DEPTHOFFSET_ON
				, out float outputDepth : SV_Depth
				#endif
			) 
			{
				UNITY_SETUP_INSTANCE_ID(IN);

				#ifdef LOD_FADE_CROSSFADE
					UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
				#endif

				#if defined(_SPECULAR_SETUP)
					SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
				#else
					SurfaceOutputStandard o = (SurfaceOutputStandard)0;
				#endif
				float3 WorldTangent = float3(IN.tSpace0.x,IN.tSpace1.x,IN.tSpace2.x);
				float3 WorldBiTangent = float3(IN.tSpace0.y,IN.tSpace1.y,IN.tSpace2.y);
				float3 WorldNormal = float3(IN.tSpace0.z,IN.tSpace1.z,IN.tSpace2.z);
				float3 worldPos = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				half atten = 1;

				float2 texCoord363 = IN.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float4 screenPos = IN.ase_texcoord9;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				#ifdef _ENABLESCREENTILING_ON
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				#ifdef _ENABLEWORLDTILING_ON
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11657 = ( ( ( (worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				#ifdef _SPRITESHEETFIX_ON
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				float2 temp_output_3_0_g11667 = fixedUV475;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch117_g11668 = _WindMinIntensity;
				#else
				float staticSwitch117_g11668 = WindMinIntensity;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch118_g11668 = _WindMaxIntensity;
				#else
				float staticSwitch118_g11668 = WindMaxIntensity;
				#endif
				float4 transform62_g11668 = mul(unity_WorldToObject,float4( 0,0,0,1 ));
				#ifdef _WINDISPARALLAX_ON
				float staticSwitch111_g11668 = _WindXPosition;
				#else
				float staticSwitch111_g11668 = transform62_g11668.x;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch113_g11668 = _WindNoiseScale;
				#else
				float staticSwitch113_g11668 = WindNoiseScale;
				#endif
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch125_g11668 = ( shaderTime237 * _WindNoiseSpeed );
				#else
				float staticSwitch125_g11668 = WindTime;
				#endif
				float temp_output_50_0_g11668 = ( ( staticSwitch111_g11668 * staticSwitch113_g11668 ) + staticSwitch125_g11668 );
				float x101_g11668 = temp_output_50_0_g11668;
				float localFastNoise101_g11668 = FastNoise101_g11668( x101_g11668 );
				float2 temp_cast_0 = (temp_output_50_0_g11668).xx;
				float simplePerlin2D121_g11668 = snoise( temp_cast_0*0.5 );
				simplePerlin2D121_g11668 = simplePerlin2D121_g11668*0.5 + 0.5;
				#ifdef _WINDHIGHQUALITYNOISE_ON
				float staticSwitch123_g11668 = simplePerlin2D121_g11668;
				#else
				float staticSwitch123_g11668 = ( localFastNoise101_g11668 + 0.5 );
				#endif
				#ifdef _ENABLEWIND_ON
				float lerpResult86_g11668 = lerp( staticSwitch117_g11668 , staticSwitch118_g11668 , staticSwitch123_g11668);
				float clampResult29_g11668 = clamp( ( ( _WindRotationWindFactor * lerpResult86_g11668 ) + _WindRotation ) , -_WindMaxRotation , _WindMaxRotation );
				float2 temp_output_1_0_g11668 = temp_output_3_0_g11667;
				float temp_output_39_0_g11668 = ( temp_output_1_0_g11668.y + _WindFlip );
				float3 appendResult43_g11668 = (float3(0.5 , -_WindFlip , 0.0));
				float2 appendResult27_g11668 = (float2(0.0 , ( _WindSquishFactor * min( ( ( _WindSquishWindFactor * abs( lerpResult86_g11668 ) ) + abs( _WindRotation ) ) , _WindMaxRotation ) * temp_output_39_0_g11668 )));
				float3 rotatedValue19_g11668 = RotateAroundAxis( appendResult43_g11668, float3( ( appendResult27_g11668 + temp_output_1_0_g11668 ) ,  0.0 ), float3( 0,0,1 ), ( clampResult29_g11668 * temp_output_39_0_g11668 ) );
				float2 staticSwitch4_g11667 = (rotatedValue19_g11668).xy;
				#else
				float2 staticSwitch4_g11667 = temp_output_3_0_g11667;
				#endif
				float2 texCoord435 = IN.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _PIXELPERFECTSPACE_ON
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = IN.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (IN.ase_texcoord10.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (IN.ase_texcoord10.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float2 temp_output_195_0_g11669 = shaderPosition235;
				float linValue16_g11670 = tex2D( _UberNoiseTexture, ( temp_output_195_0_g11669 * _FullDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11670 = MyCustomExpression16_g11670( linValue16_g11670 );
				float linValue16_g11671 = tex2D( _UberNoiseTexture, ( ( temp_output_195_0_g11669 + float2( 0.321,0.321 ) ) * _FullDistortionNoiseScale ) ).r;
				#ifdef _ENABLEFULLDISTORTION_ON
				float localMyCustomExpression16_g11671 = MyCustomExpression16_g11671( linValue16_g11671 );
				float2 appendResult189_g11669 = (float2(( localMyCustomExpression16_g11670 - 0.5 ) , ( localMyCustomExpression16_g11671 - 0.5 )));
				float2 staticSwitch83 = ( staticSwitch4_g11667 + ( ( 1.0 - _FullDistortionFade ) * appendResult189_g11669 * _FullDistortionDistortion ) );
				#else
				float2 staticSwitch83 = staticSwitch4_g11667;
				#endif
				float2 temp_output_182_0_g11672 = shaderPosition235;
				float linValue16_g11674 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionDistortionScale ) ).r;
				float localMyCustomExpression16_g11674 = MyCustomExpression16_g11674( linValue16_g11674 );
				float3 rotatedValue168_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( _DirectionalDistortionDistortion ,  0.0 ), float3( 0,0,1 ), ( ( ( localMyCustomExpression16_g11674 - 0.5 ) * 2.0 * _DirectionalDistortionRandomDirection ) * UNITY_PI ) );
				float3 rotatedValue136_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_182_0_g11672 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalDistortionRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11672 = rotatedValue136_g11672;
				float linValue16_g11673 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11673 = MyCustomExpression16_g11673( linValue16_g11673 );
				float clampResult154_g11672 = clamp( ( ( break130_g11672.x + break130_g11672.y + _DirectionalDistortionFade + ( localMyCustomExpression16_g11673 * _DirectionalDistortionNoiseFactor ) ) / max( _DirectionalDistortionWidth , 0.001 ) ) , 0.0 , 1.0 );
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float2 staticSwitch82 = ( staticSwitch83 + ( (rotatedValue168_g11672).xy * ( 1.0 - (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 )) ) ) );
				#else
				float2 staticSwitch82 = staticSwitch83;
				#endif
				float temp_output_8_0_g11677 = ( ( ( shaderTime237 * _HologramDistortionSpeed ) + worldPos.y ) / unity_OrthoParams.y );
				float2 temp_cast_4 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_5 = (_HologramDistortionDensity).xx;
				float linValue16_g11679 = tex2D( _UberNoiseTexture, ( temp_cast_4 * temp_cast_5 ) ).r;
				float localMyCustomExpression16_g11679 = MyCustomExpression16_g11679( linValue16_g11679 );
				float clampResult75_g11677 = clamp( localMyCustomExpression16_g11679 , 0.075 , 0.6 );
				float2 temp_cast_6 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_7 = (_HologramDistortionScale).xx;
				float linValue16_g11680 = tex2D( _UberNoiseTexture, ( temp_cast_6 * temp_cast_7 ) ).r;
				float localMyCustomExpression16_g11680 = MyCustomExpression16_g11680( linValue16_g11680 );
				float2 appendResult2_g11678 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float hologramFade182 = _HologramFade;
				#ifdef _ENABLEHOLOGRAM_ON
				float2 appendResult44_g11677 = (float2(( ( ( clampResult75_g11677 * ( localMyCustomExpression16_g11680 - 0.5 ) ) * _HologramDistortionOffset * ( 100.0 / appendResult2_g11678 ).x ) * hologramFade182 ) , 0.0));
				float2 staticSwitch59 = ( staticSwitch82 + appendResult44_g11677 );
				#else
				float2 staticSwitch59 = staticSwitch82;
				#endif
				float2 temp_output_18_0_g11675 = shaderPosition235;
				float2 glitchPosition154 = temp_output_18_0_g11675;
				float linValue16_g11721 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchDistortionSpeed * shaderTime237 ) ) * _GlitchDistortionScale ) ).r;
				float localMyCustomExpression16_g11721 = MyCustomExpression16_g11721( linValue16_g11721 );
				float linValue16_g11676 = tex2D( _UberNoiseTexture, ( ( temp_output_18_0_g11675 + ( _GlitchMaskSpeed * shaderTime237 ) ) * _GlitchMaskScale ) ).r;
				float localMyCustomExpression16_g11676 = MyCustomExpression16_g11676( linValue16_g11676 );
				float glitchFade152 = ( max( localMyCustomExpression16_g11676 , _GlitchMaskMin ) * _GlitchFade );
				#ifdef _ENABLEGLITCH_ON
				float2 staticSwitch62 = ( staticSwitch59 + ( ( localMyCustomExpression16_g11721 - 0.5 ) * _GlitchDistortion * glitchFade152 ) );
				#else
				float2 staticSwitch62 = staticSwitch59;
				#endif
				float2 temp_output_1_0_g11722 = staticSwitch62;
				float2 temp_output_26_0_g11722 = shaderPosition235;
				float temp_output_25_0_g11722 = shaderTime237;
				float linValue16_g11732 = tex2D( _UberNoiseTexture, ( ( temp_output_26_0_g11722 + ( _UVDistortSpeed * temp_output_25_0_g11722 ) ) * _UVDistortNoiseScale ) ).r;
				float localMyCustomExpression16_g11732 = MyCustomExpression16_g11732( linValue16_g11732 );
				float2 lerpResult21_g11729 = lerp( _UVDistortFrom , _UVDistortTo , localMyCustomExpression16_g11732);
				float2 appendResult2_g11731 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _UVDISTORTMASKTOGGLE_ON
				float2 uv_UVDistortMask = IN.ase_texcoord8.xy * _UVDistortMask_ST.xy + _UVDistortMask_ST.zw;
				float4 tex2DNode3_g11730 = tex2D( _UVDistortMask, uv_UVDistortMask );
				float staticSwitch29_g11729 = ( _UVDistortFade * ( tex2DNode3_g11730.r * tex2DNode3_g11730.a ) );
				#else
				float staticSwitch29_g11729 = _UVDistortFade;
				#endif
				#ifdef _ENABLEUVDISTORT_ON
				float2 staticSwitch5_g11722 = ( temp_output_1_0_g11722 + ( lerpResult21_g11729 * ( 100.0 / appendResult2_g11731 ) * staticSwitch29_g11729 ) );
				#else
				float2 staticSwitch5_g11722 = temp_output_1_0_g11722;
				#endif
				#ifdef _ENABLESQUEEZE_ON
				float2 temp_output_1_0_g11728 = staticSwitch5_g11722;
				float2 staticSwitch7_g11722 = ( temp_output_1_0_g11728 + ( ( temp_output_1_0_g11728 - _SqueezeCenter ) * pow( distance( temp_output_1_0_g11728 , _SqueezeCenter ) , _SqueezePower ) * _SqueezeScale * _SqueezeFade ) );
				#else
				float2 staticSwitch7_g11722 = staticSwitch5_g11722;
				#endif
				#ifdef _ENABLESINEROTATE_ON
				float3 rotatedValue36_g11727 = RotateAroundAxis( float3( _SineRotatePivot ,  0.0 ), float3( staticSwitch7_g11722 ,  0.0 ), float3( 0,0,1 ), ( sin( ( temp_output_25_0_g11722 * _SineRotateFrequency ) ) * ( ( _SineRotateAngle / 360.0 ) * UNITY_PI ) * _SineRotateFade ) );
				float2 staticSwitch9_g11722 = (rotatedValue36_g11727).xy;
				#else
				float2 staticSwitch9_g11722 = staticSwitch7_g11722;
				#endif
				#ifdef _ENABLEUVROTATE_ON
				float3 rotatedValue8_g11726 = RotateAroundAxis( float3( _UVRotatePivot ,  0.0 ), float3( staticSwitch9_g11722 ,  0.0 ), float3( 0,0,1 ), ( temp_output_25_0_g11722 * _UVRotateSpeed * UNITY_PI ) );
				float2 staticSwitch16_g11722 = (rotatedValue8_g11726).xy;
				#else
				float2 staticSwitch16_g11722 = staticSwitch9_g11722;
				#endif
				#ifdef _ENABLEUVSCROLL_ON
				float2 staticSwitch14_g11722 = ( ( _UVScrollSpeed * temp_output_25_0_g11722 ) + staticSwitch16_g11722 );
				#else
				float2 staticSwitch14_g11722 = staticSwitch16_g11722;
				#endif
				#ifdef _ENABLEPIXELATE_ON
				float2 appendResult35_g11724 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 MultFactor30_g11724 = ( ( _PixelatePixelDensity * ( appendResult35_g11724 / _PixelatePixelsPerUnit ) ) * ( 1.0 / max( _PixelateFade , 1E-05 ) ) );
				float2 clampResult46_g11724 = clamp( ( floor( ( MultFactor30_g11724 * ( staticSwitch14_g11722 + ( float2( 0.5,0.5 ) / MultFactor30_g11724 ) ) ) ) / MultFactor30_g11724 ) , float2( 0,0 ) , float2( 1,1 ) );
				float2 staticSwitch4_g11722 = clampResult46_g11724;
				#else
				float2 staticSwitch4_g11722 = staticSwitch14_g11722;
				#endif
				#ifdef _ENABLEUVSCALE_ON
				float2 staticSwitch24_g11722 = ( ( ( staticSwitch4_g11722 - _UVScalePivot ) / _UVScaleScale ) + _UVScalePivot );
				#else
				float2 staticSwitch24_g11722 = staticSwitch4_g11722;
				#endif
				float2 temp_output_1_0_g11733 = staticSwitch24_g11722;
				float temp_output_7_0_g11733 = ( sin( ( _WiggleFrequency * ( temp_output_26_0_g11722.y + ( _WiggleSpeed * temp_output_25_0_g11722 ) ) ) ) * _WiggleOffset * _WiggleFade );
				#ifdef _WIGGLEFIXEDGROUNDTOGGLE_ON
				float staticSwitch18_g11733 = ( temp_output_7_0_g11733 * temp_output_1_0_g11733.y );
				#else
				float staticSwitch18_g11733 = temp_output_7_0_g11733;
				#endif
				#ifdef _ENABLEWIGGLE_ON
				float2 appendResult12_g11733 = (float2(staticSwitch18_g11733 , 0.0));
				float2 staticSwitch13_g11733 = ( temp_output_1_0_g11733 + appendResult12_g11733 );
				#else
				float2 staticSwitch13_g11733 = temp_output_1_0_g11733;
				#endif
				float2 temp_output_484_0 = staticSwitch13_g11733;
				float2 texCoord131 = IN.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_FadingMask = IN.ase_texcoord8.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2D( _FadingMask, uv_FadingMask );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float linValue16_g11717 = tex2D( _UberNoiseTexture, ( shaderPosition235 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2D( _UberNoiseTexture, ( temp_output_27_0_g11718 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult130 = lerp( texCoord131 , temp_output_484_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch145 = temp_output_484_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch145 = lerpResult130;
				#else
				float2 staticSwitch145 = temp_output_484_0;
				#endif
				#ifdef _TILINGFIX_ON
				float2 staticSwitch485 = ( ( ( staticSwitch145 % float2( 1,1 ) ) + float2( 1,1 ) ) % float2( 1,1 ) );
				#else
				float2 staticSwitch485 = staticSwitch145;
				#endif
				#ifdef _SPRITESHEETFIX_ON
				float2 break14_g11734 = staticSwitch485;
				float2 break11_g11734 = float2( 0,0 );
				float2 break10_g11734 = float2( 1,1 );
				float2 break9_g11734 = spriteRectMin376;
				float2 break8_g11734 = spriteRectMax377;
				float2 appendResult15_g11734 = (float2((break9_g11734.x + (break14_g11734.x - break11_g11734.x) * (break8_g11734.x - break9_g11734.x) / (break10_g11734.x - break11_g11734.x)) , (break9_g11734.y + (break14_g11734.y - break11_g11734.y) * (break8_g11734.y - break9_g11734.y) / (break10_g11734.y - break11_g11734.y))));
				float2 staticSwitch371 = min( max( appendResult15_g11734 , spriteRectMin376 ) , spriteRectMax377 );
				#else
				float2 staticSwitch371 = staticSwitch485;
				#endif
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11735 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch427 = ( originalUV460 + ( floor( ( ( staticSwitch371 - uvAfterPixelArt450 ) * appendResult7_g11735 ) ) / appendResult7_g11735 ) );
				#else
				float2 staticSwitch427 = staticSwitch371;
				#endif
				float2 finalUV146 = staticSwitch427;
				float2 temp_output_1_0_g11736 = finalUV146;
				#ifdef _ENABLESMOOTHPIXELART_ON
				sampler2D tex3_g11737 = _MainTex;
				float4 textureTexelSize3_g11737 = _MainTex_TexelSize;
				float2 uvs3_g11737 = temp_output_1_0_g11736;
				float4 localtexturePointSmooth3_g11737 = texturePointSmooth( tex3_g11737 , textureTexelSize3_g11737 , uvs3_g11737 );
				float4 staticSwitch8_g11736 = localtexturePointSmooth3_g11737;
				#else
				float4 staticSwitch8_g11736 = tex2D( _MainTex, temp_output_1_0_g11736 );
				#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
				float temp_output_10_0_g11738 = ( _GaussianBlurOffset * _GaussianBlurFade * 0.005 );
				float temp_output_2_0_g11748 = temp_output_10_0_g11738;
				float2 appendResult16_g11748 = (float2(temp_output_2_0_g11748 , 0.0));
				float2 appendResult25_g11750 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11750 = ( appendResult16_g11748 * appendResult25_g11750 );
				float2 temp_output_7_0_g11738 = temp_output_1_0_g11736;
				float2 temp_output_1_0_g11748 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,1 ) ) );
				float2 temp_output_1_0_g11750 = temp_output_1_0_g11748;
				float2 appendResult17_g11748 = (float2(0.0 , temp_output_2_0_g11748));
				float2 appendResult25_g11749 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11749 = ( appendResult17_g11748 * appendResult25_g11749 );
				float2 temp_output_1_0_g11749 = temp_output_1_0_g11748;
				float temp_output_2_0_g11739 = temp_output_10_0_g11738;
				float2 appendResult16_g11739 = (float2(temp_output_2_0_g11739 , 0.0));
				float2 appendResult25_g11741 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11741 = ( appendResult16_g11739 * appendResult25_g11741 );
				float2 temp_output_1_0_g11739 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,1 ) ) );
				float2 temp_output_1_0_g11741 = temp_output_1_0_g11739;
				float2 appendResult17_g11739 = (float2(0.0 , temp_output_2_0_g11739));
				float2 appendResult25_g11740 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11740 = ( appendResult17_g11739 * appendResult25_g11740 );
				float2 temp_output_1_0_g11740 = temp_output_1_0_g11739;
				float temp_output_2_0_g11745 = temp_output_10_0_g11738;
				float2 appendResult16_g11745 = (float2(temp_output_2_0_g11745 , 0.0));
				float2 appendResult25_g11747 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11747 = ( appendResult16_g11745 * appendResult25_g11747 );
				float2 temp_output_1_0_g11745 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,-1 ) ) );
				float2 temp_output_1_0_g11747 = temp_output_1_0_g11745;
				float2 appendResult17_g11745 = (float2(0.0 , temp_output_2_0_g11745));
				float2 appendResult25_g11746 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11746 = ( appendResult17_g11745 * appendResult25_g11746 );
				float2 temp_output_1_0_g11746 = temp_output_1_0_g11745;
				float temp_output_2_0_g11742 = temp_output_10_0_g11738;
				float2 appendResult16_g11742 = (float2(temp_output_2_0_g11742 , 0.0));
				float2 appendResult25_g11744 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11744 = ( appendResult16_g11742 * appendResult25_g11744 );
				float2 temp_output_1_0_g11742 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,-1 ) ) );
				float2 temp_output_1_0_g11744 = temp_output_1_0_g11742;
				float2 appendResult17_g11742 = (float2(0.0 , temp_output_2_0_g11742));
				float2 appendResult25_g11743 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11743 = ( appendResult17_g11742 * appendResult25_g11743 );
				float2 temp_output_1_0_g11743 = temp_output_1_0_g11742;
				float4 staticSwitch3_g11736 = ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) ) ) ) * 0.0625 );
				#else
				float4 staticSwitch3_g11736 = staticSwitch8_g11736;
				#endif
				#ifdef _ENABLESHARPEN_ON
				float2 temp_output_1_0_g11751 = temp_output_1_0_g11736;
				float4 tex2DNode4_g11751 = tex2D( _MainTex, temp_output_1_0_g11751 );
				float temp_output_2_0_g11752 = _SharpenOffset;
				float2 appendResult16_g11752 = (float2(temp_output_2_0_g11752 , 0.0));
				float2 appendResult25_g11754 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11754 = ( appendResult16_g11752 * appendResult25_g11754 );
				float2 temp_output_1_0_g11752 = temp_output_1_0_g11751;
				float2 temp_output_1_0_g11754 = temp_output_1_0_g11752;
				float2 appendResult17_g11752 = (float2(0.0 , temp_output_2_0_g11752));
				float2 appendResult25_g11753 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11753 = ( appendResult17_g11752 * appendResult25_g11753 );
				float2 temp_output_1_0_g11753 = temp_output_1_0_g11752;
				float4 break22_g11751 = ( tex2DNode4_g11751 - ( ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) ) ) / 4.0 ) - tex2DNode4_g11751 ) * ( _SharpenFactor * _SharpenFade ) ) );
				float clampResult23_g11751 = clamp( break22_g11751.a , 0.0 , 1.0 );
				float4 appendResult24_g11751 = (float4(break22_g11751.r , break22_g11751.g , break22_g11751.b , clampResult23_g11751));
				float4 staticSwitch12_g11736 = appendResult24_g11751;
				#else
				float4 staticSwitch12_g11736 = staticSwitch3_g11736;
				#endif
				float4 temp_output_471_0 = staticSwitch12_g11736;
				#ifdef _VERTEXTINTFIRST_ON
				float4 temp_output_1_0_g11755 = temp_output_471_0;
				float4 appendResult8_g11755 = (float4(( (temp_output_1_0_g11755).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11755.a));
				float4 staticSwitch354 = appendResult8_g11755;
				#else
				float4 staticSwitch354 = temp_output_471_0;
				#endif
				float4 originalColor191 = staticSwitch354;
				float4 temp_output_1_0_g11756 = originalColor191;
				float4 temp_output_1_0_g11757 = temp_output_1_0_g11756;
				float2 temp_output_7_0_g11756 = finalUV146;
				float2 temp_output_43_0_g11757 = temp_output_7_0_g11756;
				float2 temp_cast_15 = (_SmokeNoiseScale).xx;
				float linValue16_g11758 = tex2D( _UberNoiseTexture, ( ( ( IN.ase_color.r * (( _SmokeVertexSeed )?( 5.0 ):( 0.0 )) ) + temp_output_43_0_g11757 ) * temp_cast_15 ) ).r;
				float localMyCustomExpression16_g11758 = MyCustomExpression16_g11758( linValue16_g11758 );
				float clampResult28_g11757 = clamp( ( ( ( localMyCustomExpression16_g11758 - 1.0 ) * _SmokeNoiseFactor ) + ( ( ( IN.ase_color.a / 2.5 ) - distance( temp_output_43_0_g11757 , float2( 0.5,0.5 ) ) ) * 2.5 * _SmokeSmoothness ) ) , 0.0 , 1.0 );
				#ifdef _ENABLESMOKE_ON
				float3 lerpResult34_g11757 = lerp( (temp_output_1_0_g11757).rgb , float3( 0,0,0 ) , ( ( 1.0 - clampResult28_g11757 ) * _SmokeDarkEdge ));
				float4 appendResult31_g11757 = (float4(lerpResult34_g11757 , ( clampResult28_g11757 * _SmokeAlpha * temp_output_1_0_g11757.a )));
				float4 staticSwitch2_g11756 = appendResult31_g11757;
				#else
				float4 staticSwitch2_g11756 = temp_output_1_0_g11756;
				#endif
				#ifdef _ENABLECUSTOMFADE_ON
				float4 temp_output_1_0_g11759 = staticSwitch2_g11756;
				float2 temp_output_57_0_g11759 = temp_output_7_0_g11756;
				float4 tex2DNode3_g11759 = tex2D( _CustomFadeFadeMask, temp_output_57_0_g11759 );
				float linValue16_g11760 = tex2D( _UberNoiseTexture, ( temp_output_57_0_g11759 * _CustomFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11760 = MyCustomExpression16_g11760( linValue16_g11760 );
				float clampResult37_g11759 = clamp( ( ( ( IN.ase_color.a * 2.0 ) - 1.0 ) + ( tex2DNode3_g11759.r + ( localMyCustomExpression16_g11760 * _CustomFadeNoiseFactor ) ) ) , 0.0 , 1.0 );
				float4 appendResult13_g11759 = (float4((temp_output_1_0_g11759).rgb , ( temp_output_1_0_g11759.a * pow( clampResult37_g11759 , ( _CustomFadeSmoothness / max( tex2DNode3_g11759.r , 0.05 ) ) ) * _CustomFadeAlpha )));
				float4 staticSwitch3_g11756 = appendResult13_g11759;
				#else
				float4 staticSwitch3_g11756 = staticSwitch2_g11756;
				#endif
				float4 temp_output_1_0_g11761 = staticSwitch3_g11756;
				#ifdef _ENABLECHECKERBOARD_ON
				float4 temp_output_1_0_g11762 = temp_output_1_0_g11761;
				float2 appendResult4_g11762 = (float2(worldPos.x , worldPos.y));
				float2 temp_output_44_0_g11762 = ( appendResult4_g11762 * _CheckerboardTiling * 0.5 );
				float2 break12_g11762 = step( ( ceil( temp_output_44_0_g11762 ) - temp_output_44_0_g11762 ) , float2( 0.5,0.5 ) );
				float4 appendResult42_g11762 = (float4(( (temp_output_1_0_g11762).rgb * min( ( _CheckerboardDarken + abs( ( -break12_g11762.x + break12_g11762.y ) ) ) , 1.0 ) ) , temp_output_1_0_g11762.a));
				float4 staticSwitch2_g11761 = appendResult42_g11762;
				#else
				float4 staticSwitch2_g11761 = temp_output_1_0_g11761;
				#endif
				float2 temp_output_75_0_g11763 = finalUV146;
				float linValue16_g11764 = tex2D( _UberNoiseTexture, ( ( ( shaderTime237 * _FlameSpeed ) + temp_output_75_0_g11763 ) * _FlameNoiseScale ) ).r;
				float localMyCustomExpression16_g11764 = MyCustomExpression16_g11764( linValue16_g11764 );
				float saferPower57_g11763 = abs( max( ( temp_output_75_0_g11763.y - 0.2 ) , 0.0 ) );
				float temp_output_47_0_g11763 = max( _FlameRadius , 0.01 );
				float clampResult70_g11763 = clamp( ( ( ( localMyCustomExpression16_g11764 * pow( saferPower57_g11763 , _FlameNoiseHeightFactor ) * _FlameNoiseFactor ) + ( ( temp_output_47_0_g11763 - distance( temp_output_75_0_g11763 , float2( 0.5,0.4 ) ) ) / temp_output_47_0_g11763 ) ) * _FlameSmooth ) , 0.0 , 1.0 );
				#ifdef _ENABLEFLAME_ON
				float temp_output_63_0_g11763 = ( clampResult70_g11763 * _FlameBrightness );
				float4 appendResult31_g11763 = (float4(temp_output_63_0_g11763 , temp_output_63_0_g11763 , temp_output_63_0_g11763 , clampResult70_g11763));
				float4 staticSwitch6_g11761 = ( appendResult31_g11763 * staticSwitch2_g11761 );
				#else
				float4 staticSwitch6_g11761 = staticSwitch2_g11761;
				#endif
				float4 temp_output_3_0_g11765 = staticSwitch6_g11761;
				float4 temp_output_1_0_g11792 = temp_output_3_0_g11765;
				float2 temp_output_1_0_g11765 = finalUV146;
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
				float4 staticSwitch81_g11792 = tex2D( _RecolorRGBTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch81_g11792 = temp_output_1_0_g11792;
				#endif
				#ifdef _ENABLERECOLORRGB_ON
				float4 break82_g11792 = staticSwitch81_g11792;
				float temp_output_63_0_g11792 = ( break82_g11792.r + break82_g11792.g + break82_g11792.b );
				float4 break71_g11792 = ( ( _RecolorRGBRedTint * ( break82_g11792.r / temp_output_63_0_g11792 ) ) + ( _RecolorRGBGreenTint * ( break82_g11792.g / temp_output_63_0_g11792 ) ) + ( ( break82_g11792.b / temp_output_63_0_g11792 ) * _RecolorRGBBlueTint ) );
				float3 appendResult56_g11792 = (float3(break71_g11792.r , break71_g11792.g , break71_g11792.b));
				float4 break2_g11793 = temp_output_1_0_g11792;
				float saferPower57_g11792 = abs( ( ( break2_g11793.x + break2_g11793.x + break2_g11793.y + break2_g11793.y + break2_g11793.y + break2_g11793.z ) / 6.0 ) );
				float3 lerpResult26_g11792 = lerp( (temp_output_1_0_g11792).rgb , ( appendResult56_g11792 * pow( saferPower57_g11792 , ( max( break71_g11792.a , 0.01 ) * 2.0 ) ) ) , ( min( ( temp_output_63_0_g11792 * 2.0 ) , 1.0 ) * _RecolorRGBFade ));
				float4 appendResult30_g11792 = (float4(lerpResult26_g11792 , temp_output_1_0_g11792.a));
				float4 staticSwitch43_g11765 = appendResult30_g11792;
				#else
				float4 staticSwitch43_g11765 = temp_output_3_0_g11765;
				#endif
				float4 temp_output_1_0_g11790 = staticSwitch43_g11765;
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
				float4 staticSwitch62_g11790 = tex2D( _RecolorRGBYCPTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch62_g11790 = temp_output_1_0_g11790;
				#endif
				float3 hsvTorgb33_g11790 = RGBToHSV( staticSwitch62_g11790.rgb );
				float temp_output_43_0_g11790 = ( ( hsvTorgb33_g11790.x + 0.08333334 ) % 1.0 );
				float4 ifLocalVar46_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.8333333 )
				ifLocalVar46_g11790 = _RecolorRGBYCPPurpleTint;
				else
				ifLocalVar46_g11790 = _RecolorRGBYCPBlueTint;
				float4 ifLocalVar44_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.6666667 )
				ifLocalVar44_g11790 = _RecolorRGBYCPCyanTint;
				else
				ifLocalVar44_g11790 = ifLocalVar46_g11790;
				float4 ifLocalVar47_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.3333333 )
				ifLocalVar47_g11790 = _RecolorRGBYCPYellowTint;
				else
				ifLocalVar47_g11790 = _RecolorRGBYCPGreenTint;
				float4 ifLocalVar45_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.1666667 )
				ifLocalVar45_g11790 = _RecolorRGBYCPRedTint;
				else
				ifLocalVar45_g11790 = ifLocalVar47_g11790;
				float4 ifLocalVar35_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.5 )
				ifLocalVar35_g11790 = ifLocalVar44_g11790;
				else
				ifLocalVar35_g11790 = ifLocalVar45_g11790;
				#ifdef _ENABLERECOLORRGBYCP_ON
				float4 break55_g11790 = ifLocalVar35_g11790;
				float3 appendResult56_g11790 = (float3(break55_g11790.r , break55_g11790.g , break55_g11790.b));
				float4 break2_g11791 = temp_output_1_0_g11790;
				float saferPower57_g11790 = abs( ( ( break2_g11791.x + break2_g11791.x + break2_g11791.y + break2_g11791.y + break2_g11791.y + break2_g11791.z ) / 6.0 ) );
				float3 lerpResult26_g11790 = lerp( (temp_output_1_0_g11790).rgb , ( appendResult56_g11790 * pow( saferPower57_g11790 , max( ( break55_g11790.a * 2.0 ) , 0.01 ) ) ) , ( hsvTorgb33_g11790.z * _RecolorRGBYCPFade ));
				float4 appendResult30_g11790 = (float4(lerpResult26_g11790 , temp_output_1_0_g11790.a));
				float4 staticSwitch9_g11765 = appendResult30_g11790;
				#else
				float4 staticSwitch9_g11765 = staticSwitch43_g11765;
				#endif
				#ifdef _ENABLECOLORREPLACE_ON
				float4 temp_output_1_0_g11768 = staticSwitch9_g11765;
				float3 temp_output_2_0_g11768 = (temp_output_1_0_g11768).rgb;
				float3 In115_g11768 = temp_output_2_0_g11768;
				float3 From115_g11768 = (_ColorReplaceFromColor).rgb;
				float4 break2_g11769 = temp_output_1_0_g11768;
				float3 To115_g11768 = ( pow( ( ( break2_g11769.x + break2_g11769.x + break2_g11769.y + break2_g11769.y + break2_g11769.y + break2_g11769.z ) / 6.0 ) , max( _ColorReplaceContrast , 0.0001 ) ) * (_ColorReplaceToColor).rgb );
				float Fuzziness115_g11768 = _ColorReplaceSmoothness;
				float Range115_g11768 = _ColorReplaceRange;
				float3 localMyCustomExpression115_g11768 = MyCustomExpression115_g11768( In115_g11768 , From115_g11768 , To115_g11768 , Fuzziness115_g11768 , Range115_g11768 );
				float3 lerpResult112_g11768 = lerp( temp_output_2_0_g11768 , localMyCustomExpression115_g11768 , _ColorReplaceFade);
				float4 appendResult4_g11768 = (float4(lerpResult112_g11768 , temp_output_1_0_g11768.a));
				float4 staticSwitch29_g11765 = appendResult4_g11768;
				#else
				float4 staticSwitch29_g11765 = staticSwitch9_g11765;
				#endif
				float4 temp_output_1_0_g11779 = staticSwitch29_g11765;
				#ifdef _ENABLENEGATIVE_ON
				float3 temp_output_9_0_g11779 = (temp_output_1_0_g11779).rgb;
				float3 lerpResult3_g11779 = lerp( temp_output_9_0_g11779 , ( 1.0 - temp_output_9_0_g11779 ) , _NegativeFade);
				float4 appendResult8_g11779 = (float4(lerpResult3_g11779 , temp_output_1_0_g11779.a));
				float4 staticSwitch4_g11779 = appendResult8_g11779;
				#else
				float4 staticSwitch4_g11779 = temp_output_1_0_g11779;
				#endif
				float4 temp_output_57_0_g11765 = staticSwitch4_g11779;
				#ifdef _ENABLECONTRAST_ON
				float4 temp_output_1_0_g11800 = temp_output_57_0_g11765;
				float3 saferPower5_g11800 = abs( (temp_output_1_0_g11800).rgb );
				float3 temp_cast_29 = (_Contrast).xxx;
				float4 appendResult4_g11800 = (float4(pow( saferPower5_g11800 , temp_cast_29 ) , temp_output_1_0_g11800.a));
				float4 staticSwitch32_g11765 = appendResult4_g11800;
				#else
				float4 staticSwitch32_g11765 = temp_output_57_0_g11765;
				#endif
				#ifdef _ENABLEBRIGHTNESS_ON
				float4 temp_output_2_0_g11777 = staticSwitch32_g11765;
				float4 appendResult6_g11777 = (float4(( (temp_output_2_0_g11777).rgb * _Brightness ) , temp_output_2_0_g11777.a));
				float4 staticSwitch33_g11765 = appendResult6_g11777;
				#else
				float4 staticSwitch33_g11765 = staticSwitch32_g11765;
				#endif
				#ifdef _ENABLEHUE_ON
				float4 temp_output_2_0_g11778 = staticSwitch33_g11765;
				float3 hsvTorgb1_g11778 = RGBToHSV( temp_output_2_0_g11778.rgb );
				float3 hsvTorgb3_g11778 = HSVToRGB( float3(( hsvTorgb1_g11778.x + _Hue ),hsvTorgb1_g11778.y,hsvTorgb1_g11778.z) );
				float4 appendResult8_g11778 = (float4(hsvTorgb3_g11778 , temp_output_2_0_g11778.a));
				float4 staticSwitch36_g11765 = appendResult8_g11778;
				#else
				float4 staticSwitch36_g11765 = staticSwitch33_g11765;
				#endif
				#ifdef _ENABLESPLITTONING_ON
				float4 temp_output_1_0_g11794 = staticSwitch36_g11765;
				float4 break2_g11795 = temp_output_1_0_g11794;
				float temp_output_3_0_g11794 = ( ( break2_g11795.x + break2_g11795.x + break2_g11795.y + break2_g11795.y + break2_g11795.y + break2_g11795.z ) / 6.0 );
				float clampResult25_g11794 = clamp( ( ( ( ( temp_output_3_0_g11794 + _SplitToningShift ) - 0.5 ) * _SplitToningBalance ) + 0.5 ) , 0.0 , 1.0 );
				float3 lerpResult6_g11794 = lerp( (_SplitToningShadowsColor).rgb , (_SplitToningHighlightsColor).rgb , clampResult25_g11794);
				float temp_output_9_0_g11796 = max( _SplitToningContrast , 0.0 );
				float saferPower7_g11796 = abs( ( temp_output_3_0_g11794 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11796 ) , 0.0 ) ) ) );
				float3 lerpResult11_g11794 = lerp( (temp_output_1_0_g11794).rgb , ( lerpResult6_g11794 * pow( saferPower7_g11796 , temp_output_9_0_g11796 ) ) , _SplitToningFade);
				float4 appendResult18_g11794 = (float4(lerpResult11_g11794 , temp_output_1_0_g11794.a));
				float4 staticSwitch30_g11765 = appendResult18_g11794;
				#else
				float4 staticSwitch30_g11765 = staticSwitch36_g11765;
				#endif
				#ifdef _ENABLEBLACKTINT_ON
				float4 temp_output_1_0_g11775 = staticSwitch30_g11765;
				float3 temp_output_4_0_g11775 = (temp_output_1_0_g11775).rgb;
				float4 break12_g11775 = temp_output_1_0_g11775;
				float3 lerpResult7_g11775 = lerp( temp_output_4_0_g11775 , ( temp_output_4_0_g11775 + (_BlackTintColor).rgb ) , pow( ( 1.0 - min( max( max( break12_g11775.r , break12_g11775.g ) , break12_g11775.b ) , 1.0 ) ) , max( _BlackTintPower , 0.001 ) ));
				float3 lerpResult13_g11775 = lerp( temp_output_4_0_g11775 , lerpResult7_g11775 , _BlackTintFade);
				float4 appendResult11_g11775 = (float4(lerpResult13_g11775 , break12_g11775.a));
				float4 staticSwitch20_g11765 = appendResult11_g11775;
				#else
				float4 staticSwitch20_g11765 = staticSwitch30_g11765;
				#endif
				#ifdef _ENABLEINKSPREAD_ON
				float4 temp_output_1_0_g11786 = staticSwitch20_g11765;
				float4 break2_g11788 = temp_output_1_0_g11786;
				float temp_output_9_0_g11789 = max( _InkSpreadContrast , 0.0 );
				float saferPower7_g11789 = abs( ( ( ( break2_g11788.x + break2_g11788.x + break2_g11788.y + break2_g11788.y + break2_g11788.y + break2_g11788.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11789 ) , 0.0 ) ) ) );
				float2 temp_output_65_0_g11786 = shaderPosition235;
				float linValue16_g11787 = tex2D( _UberNoiseTexture, ( temp_output_65_0_g11786 * _InkSpreadNoiseScale ) ).r;
				float localMyCustomExpression16_g11787 = MyCustomExpression16_g11787( linValue16_g11787 );
				float clampResult53_g11786 = clamp( ( ( ( _InkSpreadDistance - distance( _InkSpreadPosition , temp_output_65_0_g11786 ) ) + ( localMyCustomExpression16_g11787 * _InkSpreadNoiseFactor ) ) / max( _InkSpreadWidth , 0.001 ) ) , 0.0 , 1.0 );
				float3 lerpResult7_g11786 = lerp( (temp_output_1_0_g11786).rgb , ( (_InkSpreadColor).rgb * pow( saferPower7_g11789 , temp_output_9_0_g11789 ) ) , ( _InkSpreadFade * clampResult53_g11786 ));
				float4 appendResult9_g11786 = (float4(lerpResult7_g11786 , (temp_output_1_0_g11786).a));
				float4 staticSwitch17_g11765 = appendResult9_g11786;
				#else
				float4 staticSwitch17_g11765 = staticSwitch20_g11765;
				#endif
				float temp_output_39_0_g11765 = shaderTime237;
				#ifdef _ENABLESHIFTHUE_ON
				float4 temp_output_1_0_g11780 = staticSwitch17_g11765;
				float3 hsvTorgb15_g11780 = RGBToHSV( (temp_output_1_0_g11780).rgb );
				float3 hsvTorgb19_g11780 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _ShiftHueSpeed ) + hsvTorgb15_g11780.x ),hsvTorgb15_g11780.y,hsvTorgb15_g11780.z) );
				float4 appendResult6_g11780 = (float4(hsvTorgb19_g11780 , temp_output_1_0_g11780.a));
				float4 staticSwitch19_g11765 = appendResult6_g11780;
				#else
				float4 staticSwitch19_g11765 = staticSwitch17_g11765;
				#endif
				float3 hsvTorgb19_g11783 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _AddHueSpeed ) % 1.0 ),_AddHueSaturation,_AddHueBrightness) );
				float4 temp_output_1_0_g11783 = staticSwitch19_g11765;
				float4 break2_g11785 = temp_output_1_0_g11783;
				float saferPower27_g11783 = abs( ( ( break2_g11785.x + break2_g11785.x + break2_g11785.y + break2_g11785.y + break2_g11785.y + break2_g11785.z ) / 6.0 ) );
				#ifdef _ADDHUEMASKTOGGLE_ON
				float2 uv_AddHueMask = IN.ase_texcoord8.xy * _AddHueMask_ST.xy + _AddHueMask_ST.zw;
				float4 tex2DNode3_g11784 = tex2D( _AddHueMask, uv_AddHueMask );
				float staticSwitch33_g11783 = ( _AddHueFade * ( tex2DNode3_g11784.r * tex2DNode3_g11784.a ) );
				#else
				float staticSwitch33_g11783 = _AddHueFade;
				#endif
				#ifdef _ENABLEADDHUE_ON
				float4 appendResult6_g11783 = (float4(( ( hsvTorgb19_g11783 * pow( saferPower27_g11783 , max( _AddHueContrast , 0.001 ) ) * staticSwitch33_g11783 ) + (temp_output_1_0_g11783).rgb ) , temp_output_1_0_g11783.a));
				float4 staticSwitch23_g11765 = appendResult6_g11783;
				#else
				float4 staticSwitch23_g11765 = staticSwitch19_g11765;
				#endif
				float4 temp_output_1_0_g11781 = staticSwitch23_g11765;
				float4 break2_g11782 = temp_output_1_0_g11781;
				float3 temp_output_13_0_g11781 = (_SineGlowColor).rgb;
				#ifdef _SINEGLOWMASKTOGGLE_ON
				float2 uv_SineGlowMask = IN.ase_texcoord8.xy * _SineGlowMask_ST.xy + _SineGlowMask_ST.zw;
				float4 tex2DNode30_g11781 = tex2D( _SineGlowMask, uv_SineGlowMask );
				float3 staticSwitch27_g11781 = ( (tex2DNode30_g11781).rgb * temp_output_13_0_g11781 * tex2DNode30_g11781.a );
				#else
				float3 staticSwitch27_g11781 = temp_output_13_0_g11781;
				#endif
				#ifdef _ENABLESINEGLOW_ON
				float4 appendResult21_g11781 = (float4(( (temp_output_1_0_g11781).rgb + ( pow( ( ( break2_g11782.x + break2_g11782.x + break2_g11782.y + break2_g11782.y + break2_g11782.y + break2_g11782.z ) / 6.0 ) , max( _SineGlowContrast , 0.0 ) ) * staticSwitch27_g11781 * _SineGlowFade * ( ( ( sin( ( temp_output_39_0_g11765 * _SineGlowFrequency ) ) + 1.0 ) * ( _SineGlowMax - _SineGlowMin ) ) + _SineGlowMin ) ) ) , temp_output_1_0_g11781.a));
				float4 staticSwitch28_g11765 = appendResult21_g11781;
				#else
				float4 staticSwitch28_g11765 = staticSwitch23_g11765;
				#endif
				#ifdef _ENABLESATURATION_ON
				float4 temp_output_1_0_g11770 = staticSwitch28_g11765;
				float4 break2_g11771 = temp_output_1_0_g11770;
				float3 temp_cast_45 = (( ( break2_g11771.x + break2_g11771.x + break2_g11771.y + break2_g11771.y + break2_g11771.y + break2_g11771.z ) / 6.0 )).xxx;
				float3 lerpResult5_g11770 = lerp( temp_cast_45 , (temp_output_1_0_g11770).rgb , _Saturation);
				float4 appendResult8_g11770 = (float4(lerpResult5_g11770 , temp_output_1_0_g11770.a));
				float4 staticSwitch38_g11765 = appendResult8_g11770;
				#else
				float4 staticSwitch38_g11765 = staticSwitch28_g11765;
				#endif
				float4 temp_output_15_0_g11772 = staticSwitch38_g11765;
				float3 temp_output_82_0_g11772 = (_InnerOutlineColor).rgb;
				float2 temp_output_7_0_g11772 = temp_output_1_0_g11765;
				float temp_output_179_0_g11772 = temp_output_39_0_g11765;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch187_g11772 = ( (tex2D( _InnerOutlineTintTexture, ( temp_output_7_0_g11772 + ( _InnerOutlineTextureSpeed * temp_output_179_0_g11772 ) ) )).rgb * temp_output_82_0_g11772 );
				#else
				float3 staticSwitch187_g11772 = temp_output_82_0_g11772;
				#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11774 = tex2D( _UberNoiseTexture, ( ( ( temp_output_179_0_g11772 * _InnerOutlineNoiseSpeed ) + temp_output_7_0_g11772 ) * _InnerOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11774 = MyCustomExpression16_g11774( linValue16_g11774 );
				float2 staticSwitch169_g11772 = ( ( localMyCustomExpression16_g11774 - 0.5 ) * _InnerOutlineDistortionIntensity );
				#else
				float2 staticSwitch169_g11772 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11772 = ( staticSwitch169_g11772 + temp_output_7_0_g11772 );
				float2 appendResult2_g11773 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11772 = ( 100.0 / appendResult2_g11773 );
				float temp_output_178_0_g11772 = ( _InnerOutlineFade * ( 1.0 - min( min( min( min( min( min( min( tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11772 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) ) );
				float3 lerpResult176_g11772 = lerp( (temp_output_15_0_g11772).rgb , staticSwitch187_g11772 , temp_output_178_0_g11772);
				#ifdef _INNEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch188_g11772 = ( temp_output_178_0_g11772 * temp_output_15_0_g11772.a );
				#else
				float staticSwitch188_g11772 = temp_output_15_0_g11772.a;
				#endif
				#ifdef _ENABLEINNEROUTLINE_ON
				float4 appendResult177_g11772 = (float4(lerpResult176_g11772 , staticSwitch188_g11772));
				float4 staticSwitch12_g11765 = appendResult177_g11772;
				#else
				float4 staticSwitch12_g11765 = staticSwitch38_g11765;
				#endif
				float4 temp_output_15_0_g11797 = staticSwitch12_g11765;
				float3 temp_output_82_0_g11797 = (_OuterOutlineColor).rgb;
				float2 temp_output_7_0_g11797 = temp_output_1_0_g11765;
				float temp_output_186_0_g11797 = temp_output_39_0_g11765;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11797 = ( (tex2D( _OuterOutlineTintTexture, ( temp_output_7_0_g11797 + ( _OuterOutlineTextureSpeed * temp_output_186_0_g11797 ) ) )).rgb * temp_output_82_0_g11797 );
				#else
				float3 staticSwitch199_g11797 = temp_output_82_0_g11797;
				#endif
				float temp_output_182_0_g11797 = ( ( 1.0 - temp_output_15_0_g11797.a ) * min( ( _OuterOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11797 = 1.0;
				#else
				float staticSwitch203_g11797 = temp_output_182_0_g11797;
				#endif
				float3 lerpResult178_g11797 = lerp( (temp_output_15_0_g11797).rgb , staticSwitch199_g11797 , staticSwitch203_g11797);
				float3 lerpResult170_g11797 = lerp( lerpResult178_g11797 , staticSwitch199_g11797 , staticSwitch203_g11797);
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11798 = tex2D( _UberNoiseTexture, ( ( ( temp_output_186_0_g11797 * _OuterOutlineNoiseSpeed ) + temp_output_7_0_g11797 ) * _OuterOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11798 = MyCustomExpression16_g11798( linValue16_g11798 );
				float2 staticSwitch157_g11797 = ( ( localMyCustomExpression16_g11798 - 0.5 ) * _OuterOutlineDistortionIntensity );
				#else
				float2 staticSwitch157_g11797 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11797 = ( staticSwitch157_g11797 + temp_output_7_0_g11797 );
				float2 appendResult2_g11799 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11797 = ( 100.0 / appendResult2_g11799 );
				float lerpResult168_g11797 = lerp( temp_output_15_0_g11797.a , min( ( max( max( max( max( max( max( max( tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11797 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) * 3.0 ) , 1.0 ) , _OuterOutlineFade);
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11797 = ( temp_output_182_0_g11797 * lerpResult168_g11797 );
				#else
				float staticSwitch200_g11797 = lerpResult168_g11797;
				#endif
				#ifdef _ENABLEOUTEROUTLINE_ON
				float4 appendResult174_g11797 = (float4(lerpResult170_g11797 , staticSwitch200_g11797));
				float4 staticSwitch13_g11765 = appendResult174_g11797;
				#else
				float4 staticSwitch13_g11765 = staticSwitch12_g11765;
				#endif
				float4 temp_output_15_0_g11776 = staticSwitch13_g11765;
				float3 temp_output_82_0_g11776 = (_PixelOutlineColor).rgb;
				float2 temp_output_7_0_g11776 = temp_output_1_0_g11765;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11776 = ( (tex2D( _PixelOutlineTintTexture, ( temp_output_7_0_g11776 + ( _PixelOutlineTextureSpeed * temp_output_39_0_g11765 ) ) )).rgb * temp_output_82_0_g11776 );
				#else
				float3 staticSwitch199_g11776 = temp_output_82_0_g11776;
				#endif
				float temp_output_182_0_g11776 = ( ( 1.0 - temp_output_15_0_g11776.a ) * min( ( _PixelOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11776 = 1.0;
				#else
				float staticSwitch203_g11776 = temp_output_182_0_g11776;
				#endif
				float3 lerpResult178_g11776 = lerp( (temp_output_15_0_g11776).rgb , staticSwitch199_g11776 , staticSwitch203_g11776);
				float3 lerpResult170_g11776 = lerp( lerpResult178_g11776 , staticSwitch199_g11776 , staticSwitch203_g11776);
				float2 appendResult206_g11776 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_209_0_g11776 = ( float2( 1,1 ) / appendResult206_g11776 );
				float lerpResult168_g11776 = lerp( temp_output_15_0_g11776.a , min( ( max( max( max( tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,-1 ) ) * temp_output_209_0_g11776 ) ) ).a , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,1 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( -1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) * 3.0 ) , 1.0 ) , _PixelOutlineFade);
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11776 = ( temp_output_182_0_g11776 * lerpResult168_g11776 );
				#else
				float staticSwitch200_g11776 = lerpResult168_g11776;
				#endif
				#ifdef _ENABLEPIXELOUTLINE_ON
				float4 appendResult174_g11776 = (float4(lerpResult170_g11776 , staticSwitch200_g11776));
				float4 staticSwitch48_g11765 = appendResult174_g11776;
				#else
				float4 staticSwitch48_g11765 = staticSwitch13_g11765;
				#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
				float3 lerpResult15_g11766 = lerp( (_PingPongGlowFrom).rgb , (_PingPongGlowTo).rgb , ( ( sin( ( temp_output_39_0_g11765 * _PingPongGlowFrequency ) ) + 1.0 ) / 2.0 ));
				float4 temp_output_5_0_g11766 = staticSwitch48_g11765;
				float4 break2_g11767 = temp_output_5_0_g11766;
				float4 appendResult12_g11766 = (float4(( ( lerpResult15_g11766 * _PingPongGlowFade * pow( ( ( break2_g11767.x + break2_g11767.x + break2_g11767.y + break2_g11767.y + break2_g11767.y + break2_g11767.z ) / 6.0 ) , max( _PingPongGlowContrast , 0.0 ) ) ) + (temp_output_5_0_g11766).rgb ) , temp_output_5_0_g11766.a));
				float4 staticSwitch46_g11765 = appendResult12_g11766;
				#else
				float4 staticSwitch46_g11765 = staticSwitch48_g11765;
				#endif
				float4 temp_output_361_0 = staticSwitch46_g11765;
				#ifdef _ENABLEHOLOGRAM_ON
				float4 temp_output_1_0_g11801 = temp_output_361_0;
				float4 break2_g11802 = temp_output_1_0_g11801;
				float temp_output_9_0_g11803 = max( _HologramContrast , 0.0 );
				float saferPower7_g11803 = abs( ( ( ( break2_g11802.x + break2_g11802.x + break2_g11802.y + break2_g11802.y + break2_g11802.y + break2_g11802.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11803 ) , 0.0 ) ) ) );
				float4 appendResult22_g11801 = (float4(( (_HologramTint).rgb * pow( saferPower7_g11803 , temp_output_9_0_g11803 ) ) , ( max( pow( abs( sin( ( ( ( ( shaderTime237 * _HologramLineSpeed ) + worldPos.y ) / unity_OrthoParams.y ) * _HologramLineFrequency ) ) ) , _HologramLineGap ) , _HologramMinAlpha ) * temp_output_1_0_g11801.a )));
				float4 lerpResult37_g11801 = lerp( temp_output_1_0_g11801 , appendResult22_g11801 , hologramFade182);
				float4 staticSwitch56 = lerpResult37_g11801;
				#else
				float4 staticSwitch56 = temp_output_361_0;
				#endif
				#ifdef _ENABLEGLITCH_ON
				float4 temp_output_1_0_g11804 = staticSwitch56;
				float4 break2_g11806 = temp_output_1_0_g11804;
				float temp_output_34_0_g11804 = shaderTime237;
				float linValue16_g11805 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchNoiseSpeed * temp_output_34_0_g11804 ) ) * _GlitchNoiseScale ) ).r;
				float localMyCustomExpression16_g11805 = MyCustomExpression16_g11805( linValue16_g11805 );
				float3 hsvTorgb3_g11807 = HSVToRGB( float3(( localMyCustomExpression16_g11805 + ( temp_output_34_0_g11804 * _GlitchHueSpeed ) ),1.0,1.0) );
				float3 lerpResult23_g11804 = lerp( (temp_output_1_0_g11804).rgb , ( ( ( break2_g11806.x + break2_g11806.x + break2_g11806.y + break2_g11806.y + break2_g11806.y + break2_g11806.z ) / 6.0 ) * _GlitchBrightness * hsvTorgb3_g11807 ) , glitchFade152);
				float4 appendResult27_g11804 = (float4(lerpResult23_g11804 , temp_output_1_0_g11804.a));
				float4 staticSwitch57 = appendResult27_g11804;
				#else
				float4 staticSwitch57 = staticSwitch56;
				#endif
				float4 temp_output_3_0_g11808 = staticSwitch57;
				float4 temp_output_1_0_g11833 = temp_output_3_0_g11808;
				float2 temp_output_41_0_g11808 = shaderPosition235;
				float2 temp_output_99_0_g11833 = temp_output_41_0_g11808;
				float temp_output_40_0_g11808 = shaderTime237;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
				float linValue16_g11838 = tex2D( _UberNoiseTexture, ( ( ( temp_output_40_0_g11808 * _CamouflageDistortionSpeed ) + temp_output_99_0_g11833 ) * _CamouflageDistortionScale ) ).r;
				float localMyCustomExpression16_g11838 = MyCustomExpression16_g11838( linValue16_g11838 );
				float2 staticSwitch101_g11833 = ( ( ( localMyCustomExpression16_g11838 - 0.25 ) * _CamouflageDistortionIntensity ) + temp_output_99_0_g11833 );
				#else
				float2 staticSwitch101_g11833 = temp_output_99_0_g11833;
				#endif
				float linValue16_g11835 = tex2D( _UberNoiseTexture, ( staticSwitch101_g11833 * _CamouflageNoiseScaleA ) ).r;
				float localMyCustomExpression16_g11835 = MyCustomExpression16_g11835( linValue16_g11835 );
				float clampResult52_g11833 = clamp( ( ( _CamouflageDensityA - localMyCustomExpression16_g11835 ) / max( _CamouflageSmoothnessA , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult55_g11833 = lerp( _CamouflageBaseColor , ( _CamouflageColorA * clampResult52_g11833 ) , clampResult52_g11833);
				float linValue16_g11837 = tex2D( _UberNoiseTexture, ( ( staticSwitch101_g11833 + float2( 12.3,12.3 ) ) * _CamouflageNoiseScaleB ) ).r;
				#ifdef _ENABLECAMOUFLAGE_ON
				float localMyCustomExpression16_g11837 = MyCustomExpression16_g11837( linValue16_g11837 );
				float clampResult65_g11833 = clamp( ( ( _CamouflageDensityB - localMyCustomExpression16_g11837 ) / max( _CamouflageSmoothnessB , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult68_g11833 = lerp( lerpResult55_g11833 , ( _CamouflageColorB * clampResult65_g11833 ) , clampResult65_g11833);
				float4 break2_g11836 = temp_output_1_0_g11833;
				float temp_output_9_0_g11834 = max( _CamouflageContrast , 0.0 );
				float saferPower7_g11834 = abs( ( ( ( break2_g11836.x + break2_g11836.x + break2_g11836.y + break2_g11836.y + break2_g11836.y + break2_g11836.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11834 ) , 0.0 ) ) ) );
				float3 lerpResult4_g11833 = lerp( (temp_output_1_0_g11833).rgb , ( (lerpResult68_g11833).rgb * pow( saferPower7_g11834 , temp_output_9_0_g11834 ) ) , _CamouflageFade);
				float4 appendResult7_g11833 = (float4(lerpResult4_g11833 , temp_output_1_0_g11833.a));
				float4 staticSwitch26_g11808 = appendResult7_g11833;
				#else
				float4 staticSwitch26_g11808 = temp_output_3_0_g11808;
				#endif
				float4 temp_output_1_0_g11827 = staticSwitch26_g11808;
				float temp_output_59_0_g11827 = temp_output_40_0_g11808;
				float2 temp_output_58_0_g11827 = temp_output_41_0_g11808;
				float linValue16_g11828 = tex2D( _UberNoiseTexture, ( ( ( temp_output_59_0_g11827 * _MetalNoiseDistortionSpeed ) + temp_output_58_0_g11827 ) * _MetalNoiseDistortionScale ) ).r;
				float localMyCustomExpression16_g11828 = MyCustomExpression16_g11828( linValue16_g11828 );
				float linValue16_g11830 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11828 - 0.25 ) * _MetalNoiseDistortion ) + ( ( temp_output_59_0_g11827 * _MetalNoiseSpeed ) + temp_output_58_0_g11827 ) ) * _MetalNoiseScale ) ).r;
				float localMyCustomExpression16_g11830 = MyCustomExpression16_g11830( linValue16_g11830 );
				float4 break2_g11829 = temp_output_1_0_g11827;
				float temp_output_5_0_g11827 = ( ( break2_g11829.x + break2_g11829.x + break2_g11829.y + break2_g11829.y + break2_g11829.y + break2_g11829.z ) / 6.0 );
				float temp_output_9_0_g11831 = max( _MetalHighlightContrast , 0.0 );
				float saferPower7_g11831 = abs( ( temp_output_5_0_g11827 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11831 ) , 0.0 ) ) ) );
				float saferPower2_g11827 = abs( temp_output_5_0_g11827 );
				#ifdef _METALMASKTOGGLE_ON
				float2 uv_MetalMask = IN.ase_texcoord8.xy * _MetalMask_ST.xy + _MetalMask_ST.zw;
				float4 tex2DNode3_g11832 = tex2D( _MetalMask, uv_MetalMask );
				float staticSwitch60_g11827 = ( _MetalFade * ( tex2DNode3_g11832.r * tex2DNode3_g11832.a ) );
				#else
				float staticSwitch60_g11827 = _MetalFade;
				#endif
				#ifdef _ENABLEMETAL_ON
				float4 lerpResult45_g11827 = lerp( temp_output_1_0_g11827 , ( ( max( ( ( _MetalHighlightDensity - localMyCustomExpression16_g11830 ) / max( _MetalHighlightDensity , 0.01 ) ) , 0.0 ) * _MetalHighlightColor * pow( saferPower7_g11831 , temp_output_9_0_g11831 ) ) + ( pow( saferPower2_g11827 , _MetalContrast ) * _MetalColor ) ) , staticSwitch60_g11827);
				float4 appendResult8_g11827 = (float4((lerpResult45_g11827).rgb , (temp_output_1_0_g11827).a));
				float4 staticSwitch28_g11808 = appendResult8_g11827;
				#else
				float4 staticSwitch28_g11808 = staticSwitch26_g11808;
				#endif
				#ifdef _ENABLEFROZEN_ON
				float4 temp_output_1_0_g11821 = staticSwitch28_g11808;
				float4 break2_g11822 = temp_output_1_0_g11821;
				float temp_output_7_0_g11821 = ( ( break2_g11822.x + break2_g11822.x + break2_g11822.y + break2_g11822.y + break2_g11822.y + break2_g11822.z ) / 6.0 );
				float temp_output_9_0_g11824 = max( _FrozenContrast , 0.0 );
				float saferPower7_g11824 = abs( ( temp_output_7_0_g11821 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11824 ) , 0.0 ) ) ) );
				float saferPower20_g11821 = abs( temp_output_7_0_g11821 );
				float2 temp_output_72_0_g11821 = temp_output_41_0_g11808;
				float linValue16_g11823 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11821 * _FrozenSnowScale ) ).r;
				float localMyCustomExpression16_g11823 = MyCustomExpression16_g11823( linValue16_g11823 );
				float temp_output_73_0_g11821 = temp_output_40_0_g11808;
				float linValue16_g11825 = tex2D( _UberNoiseTexture, ( ( ( temp_output_73_0_g11821 * _FrozenHighlightDistortionSpeed ) + temp_output_72_0_g11821 ) * _FrozenHighlightDistortionScale ) ).r;
				float localMyCustomExpression16_g11825 = MyCustomExpression16_g11825( linValue16_g11825 );
				float linValue16_g11826 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11825 - 0.25 ) * _FrozenHighlightDistortion ) + ( ( temp_output_73_0_g11821 * _FrozenHighlightSpeed ) + temp_output_72_0_g11821 ) ) * _FrozenHighlightScale ) ).r;
				float localMyCustomExpression16_g11826 = MyCustomExpression16_g11826( linValue16_g11826 );
				float saferPower42_g11821 = abs( temp_output_7_0_g11821 );
				float3 lerpResult57_g11821 = lerp( (temp_output_1_0_g11821).rgb , ( ( pow( saferPower7_g11824 , temp_output_9_0_g11824 ) * (_FrozenTint).rgb ) + ( pow( saferPower20_g11821 , _FrozenSnowContrast ) * ( (_FrozenSnowColor).rgb * max( ( _FrozenSnowDensity - localMyCustomExpression16_g11823 ) , 0.0 ) ) ) + (( max( ( ( _FrozenHighlightDensity - localMyCustomExpression16_g11826 ) / max( _FrozenHighlightDensity , 0.01 ) ) , 0.0 ) * _FrozenHighlightColor * pow( saferPower42_g11821 , _FrozenHighlightContrast ) )).rgb ) , _FrozenFade);
				float4 appendResult26_g11821 = (float4(lerpResult57_g11821 , temp_output_1_0_g11821.a));
				float4 staticSwitch29_g11808 = appendResult26_g11821;
				#else
				float4 staticSwitch29_g11808 = staticSwitch28_g11808;
				#endif
				float4 temp_output_1_0_g11816 = staticSwitch29_g11808;
				float3 temp_output_28_0_g11816 = (temp_output_1_0_g11816).rgb;
				float4 break2_g11820 = float4( temp_output_28_0_g11816 , 0.0 );
				float saferPower21_g11816 = abs( ( ( break2_g11820.x + break2_g11820.x + break2_g11820.y + break2_g11820.y + break2_g11820.y + break2_g11820.z ) / 6.0 ) );
				float2 temp_output_72_0_g11816 = temp_output_41_0_g11808;
				float linValue16_g11819 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnSwirlNoiseScale ) ).r;
				float localMyCustomExpression16_g11819 = MyCustomExpression16_g11819( linValue16_g11819 );
				float linValue16_g11817 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11819 - 0.5 ) * float2( 1,1 ) * _BurnSwirlFactor ) + temp_output_72_0_g11816 ) * _BurnInsideNoiseScale ) ).r;
				#ifdef _ENABLEBURN_ON
				float localMyCustomExpression16_g11817 = MyCustomExpression16_g11817( linValue16_g11817 );
				float clampResult68_g11816 = clamp( ( _BurnInsideNoiseFactor - localMyCustomExpression16_g11817 ) , 0.0 , 1.0 );
				float linValue16_g11818 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnEdgeNoiseScale ) ).r;
				float localMyCustomExpression16_g11818 = MyCustomExpression16_g11818( linValue16_g11818 );
				float temp_output_15_0_g11816 = ( ( ( _BurnRadius - distance( temp_output_72_0_g11816 , _BurnPosition ) ) + ( localMyCustomExpression16_g11818 * _BurnEdgeNoiseFactor ) ) / max( _BurnWidth , 0.01 ) );
				float clampResult18_g11816 = clamp( temp_output_15_0_g11816 , 0.0 , 1.0 );
				float3 lerpResult29_g11816 = lerp( temp_output_28_0_g11816 , ( pow( saferPower21_g11816 , max( _BurnInsideContrast , 0.001 ) ) * ( ( (_BurnInsideNoiseColor).rgb * clampResult68_g11816 ) + (_BurnInsideColor).rgb ) ) , clampResult18_g11816);
				float3 lerpResult40_g11816 = lerp( temp_output_28_0_g11816 , ( lerpResult29_g11816 + ( ( step( temp_output_15_0_g11816 , 1.0 ) * step( 0.0 , temp_output_15_0_g11816 ) ) * (_BurnEdgeColor).rgb ) ) , _BurnFade);
				float4 appendResult43_g11816 = (float4(lerpResult40_g11816 , temp_output_1_0_g11816.a));
				float4 staticSwitch32_g11808 = appendResult43_g11816;
				#else
				float4 staticSwitch32_g11808 = staticSwitch29_g11808;
				#endif
				#ifdef _ENABLERAINBOW_ON
				float2 temp_output_42_0_g11812 = temp_output_41_0_g11808;
				float linValue16_g11813 = tex2D( _UberNoiseTexture, ( temp_output_42_0_g11812 * _RainbowNoiseScale ) ).r;
				float localMyCustomExpression16_g11813 = MyCustomExpression16_g11813( linValue16_g11813 );
				float3 hsvTorgb3_g11815 = HSVToRGB( float3(( ( ( distance( temp_output_42_0_g11812 , _RainbowCenter ) + ( localMyCustomExpression16_g11813 * _RainbowNoiseFactor ) ) * _RainbowDensity ) + ( _RainbowSpeed * temp_output_40_0_g11808 ) ),1.0,1.0) );
				float3 hsvTorgb36_g11812 = RGBToHSV( hsvTorgb3_g11815 );
				float3 hsvTorgb37_g11812 = HSVToRGB( float3(hsvTorgb36_g11812.x,_RainbowSaturation,( hsvTorgb36_g11812.z * _RainbowBrightness )) );
				float4 temp_output_1_0_g11812 = staticSwitch32_g11808;
				float4 break2_g11814 = temp_output_1_0_g11812;
				float saferPower24_g11812 = abs( ( ( break2_g11814.x + break2_g11814.x + break2_g11814.y + break2_g11814.y + break2_g11814.y + break2_g11814.z ) / 6.0 ) );
				float4 appendResult29_g11812 = (float4(( ( hsvTorgb37_g11812 * pow( saferPower24_g11812 , max( _RainbowContrast , 0.001 ) ) * _RainbowFade ) + (temp_output_1_0_g11812).rgb ) , temp_output_1_0_g11812.a));
				float4 staticSwitch34_g11808 = appendResult29_g11812;
				#else
				float4 staticSwitch34_g11808 = staticSwitch32_g11808;
				#endif
				float4 temp_output_1_0_g11809 = staticSwitch34_g11808;
				float3 temp_output_57_0_g11809 = (temp_output_1_0_g11809).rgb;
				float4 break2_g11810 = temp_output_1_0_g11809;
				float3 temp_cast_68 = (( ( break2_g11810.x + break2_g11810.x + break2_g11810.y + break2_g11810.y + break2_g11810.y + break2_g11810.z ) / 6.0 )).xxx;
				float3 lerpResult92_g11809 = lerp( temp_cast_68 , temp_output_57_0_g11809 , _ShineSaturation);
				float3 saferPower83_g11809 = abs( lerpResult92_g11809 );
				float3 temp_cast_69 = (max( _ShineContrast , 0.001 )).xxx;
				float3 rotatedValue69_g11809 = RotateAroundAxis( float3( 0,0,0 ), float3( ( _ShineFrequency * temp_output_41_0_g11808 ) ,  0.0 ), float3( 0,0,1 ), ( ( _ShineRotation / 180.0 ) * UNITY_PI ) );
				float temp_output_103_0_g11809 = ( _ShineFrequency * _ShineWidth );
				float clampResult80_g11809 = clamp( ( ( sin( ( rotatedValue69_g11809.x - ( temp_output_40_0_g11808 * _ShineSpeed * _ShineFrequency ) ) ) - ( 1.0 - temp_output_103_0_g11809 ) ) / temp_output_103_0_g11809 ) , 0.0 , 1.0 );
				#ifdef _SHINEMASKTOGGLE_ON
				float2 uv_ShineMask = IN.ase_texcoord8.xy * _ShineMask_ST.xy + _ShineMask_ST.zw;
				float4 tex2DNode3_g11811 = tex2D( _ShineMask, uv_ShineMask );
				float staticSwitch98_g11809 = ( _ShineFade * ( tex2DNode3_g11811.r * tex2DNode3_g11811.a ) );
				#else
				float staticSwitch98_g11809 = _ShineFade;
				#endif
				#ifdef _ENABLESHINE_ON
				float4 appendResult8_g11809 = (float4(( temp_output_57_0_g11809 + ( ( pow( saferPower83_g11809 , temp_cast_69 ) * (_ShineColor).rgb ) * clampResult80_g11809 * staticSwitch98_g11809 ) ) , (temp_output_1_0_g11809).a));
				float4 staticSwitch36_g11808 = appendResult8_g11809;
				#else
				float4 staticSwitch36_g11808 = staticSwitch34_g11808;
				#endif
				#ifdef _ENABLEPOISON_ON
				float temp_output_41_0_g11839 = temp_output_40_0_g11808;
				float linValue16_g11841 = tex2D( _UberNoiseTexture, ( ( ( temp_output_41_0_g11839 * _PoisonNoiseSpeed ) + temp_output_41_0_g11808 ) * _PoisonNoiseScale ) ).r;
				float localMyCustomExpression16_g11841 = MyCustomExpression16_g11841( linValue16_g11841 );
				float saferPower19_g11839 = abs( abs( ( ( ( localMyCustomExpression16_g11841 + ( temp_output_41_0_g11839 * _PoisonShiftSpeed ) ) % 1.0 ) + -0.5 ) ) );
				float3 temp_output_24_0_g11839 = (_PoisonColor).rgb;
				float4 temp_output_1_0_g11839 = staticSwitch36_g11808;
				float3 temp_output_28_0_g11839 = (temp_output_1_0_g11839).rgb;
				float4 break2_g11840 = float4( temp_output_28_0_g11839 , 0.0 );
				float3 lerpResult32_g11839 = lerp( temp_output_28_0_g11839 , ( temp_output_24_0_g11839 * ( ( break2_g11840.x + break2_g11840.x + break2_g11840.y + break2_g11840.y + break2_g11840.y + break2_g11840.z ) / 6.0 ) ) , ( _PoisonFade * _PoisonRecolorFactor ));
				float4 appendResult27_g11839 = (float4(( ( max( pow( saferPower19_g11839 , _PoisonDensity ) , 0.0 ) * temp_output_24_0_g11839 * _PoisonFade * _PoisonNoiseBrightness ) + lerpResult32_g11839 ) , temp_output_1_0_g11839.a));
				float4 staticSwitch39_g11808 = appendResult27_g11839;
				#else
				float4 staticSwitch39_g11808 = staticSwitch36_g11808;
				#endif
				float4 temp_output_10_0_g11842 = staticSwitch39_g11808;
				float3 temp_output_12_0_g11842 = (temp_output_10_0_g11842).rgb;
				float2 temp_output_2_0_g11842 = temp_output_41_0_g11808;
				float temp_output_1_0_g11842 = temp_output_40_0_g11808;
				float2 temp_output_6_0_g11842 = ( temp_output_1_0_g11842 * _EnchantedSpeed );
				float linValue16_g11845 = tex2D( _UberNoiseTexture, ( ( ( temp_output_2_0_g11842 - ( ( temp_output_6_0_g11842 + float2( 1.234,5.6789 ) ) * float2( 0.95,1.05 ) ) ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11845 = MyCustomExpression16_g11845( linValue16_g11845 );
				float linValue16_g11843 = tex2D( _UberNoiseTexture, ( ( ( temp_output_6_0_g11842 + temp_output_2_0_g11842 ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11843 = MyCustomExpression16_g11843( linValue16_g11843 );
				float temp_output_36_0_g11842 = ( localMyCustomExpression16_g11845 + localMyCustomExpression16_g11843 );
				float temp_output_43_0_g11842 = ( temp_output_36_0_g11842 * 0.5 );
				float3 lerpResult42_g11842 = lerp( (_EnchantedLowColor).rgb , (_EnchantedHighColor).rgb , temp_output_43_0_g11842);
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
				float3 hsvTorgb53_g11842 = HSVToRGB( float3(( ( temp_output_43_0_g11842 * _EnchantedRainbowDensity ) + ( _EnchantedRainbowSpeed * temp_output_1_0_g11842 ) ),_EnchantedRainbowSaturation,1.0) );
				float3 staticSwitch50_g11842 = hsvTorgb53_g11842;
				#else
				float3 staticSwitch50_g11842 = lerpResult42_g11842;
				#endif
				float4 break2_g11844 = temp_output_10_0_g11842;
				float saferPower24_g11842 = abs( ( ( break2_g11844.x + break2_g11844.x + break2_g11844.y + break2_g11844.y + break2_g11844.y + break2_g11844.z ) / 6.0 ) );
				float3 temp_output_40_0_g11842 = ( staticSwitch50_g11842 * pow( saferPower24_g11842 , _EnchantedContrast ) * _EnchantedBrightness );
				float temp_output_45_0_g11842 = ( max( ( temp_output_36_0_g11842 - _EnchantedReduce ) , 0.0 ) * _EnchantedFade );
				#ifdef _ENCHANTEDLERPTOGGLE_ON
				float3 lerpResult44_g11842 = lerp( temp_output_12_0_g11842 , temp_output_40_0_g11842 , temp_output_45_0_g11842);
				float3 staticSwitch47_g11842 = lerpResult44_g11842;
				#else
				float3 staticSwitch47_g11842 = ( temp_output_12_0_g11842 + ( temp_output_40_0_g11842 * temp_output_45_0_g11842 ) );
				#endif
				#ifdef _ENABLEENCHANTED_ON
				float4 appendResult19_g11842 = (float4(staticSwitch47_g11842 , temp_output_10_0_g11842.a));
				float4 staticSwitch11_g11842 = appendResult19_g11842;
				#else
				float4 staticSwitch11_g11842 = temp_output_10_0_g11842;
				#endif
				float4 temp_output_1_0_g11846 = staticSwitch11_g11842;
				float4 break5_g11846 = temp_output_1_0_g11846;
				float3 appendResult32_g11846 = (float3(break5_g11846.r , break5_g11846.g , break5_g11846.b));
				float4 break2_g11847 = temp_output_1_0_g11846;
				float temp_output_4_0_g11846 = ( ( break2_g11847.x + break2_g11847.x + break2_g11847.y + break2_g11847.y + break2_g11847.y + break2_g11847.z ) / 6.0 );
				float temp_output_11_0_g11846 = ( ( ( temp_output_4_0_g11846 + ( temp_output_40_0_g11808 * _ShiftingSpeed ) ) * _ShiftingDensity ) % 1.0 );
				float3 lerpResult20_g11846 = lerp( (_ShiftingColorA).rgb , (_ShiftingColorB).rgb , ( abs( ( temp_output_11_0_g11846 - 0.5 ) ) * 2.0 ));
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
				float3 hsvTorgb12_g11846 = HSVToRGB( float3(temp_output_11_0_g11846,_ShiftingSaturation,_ShiftingBrightness) );
				float3 staticSwitch26_g11846 = hsvTorgb12_g11846;
				#else
				float3 staticSwitch26_g11846 = ( lerpResult20_g11846 * _ShiftingBrightness );
				#endif
				#ifdef _ENABLESHIFTING_ON
				float3 lerpResult31_g11846 = lerp( appendResult32_g11846 , ( staticSwitch26_g11846 * pow( temp_output_4_0_g11846 , _ShiftingContrast ) ) , _ShiftingFade);
				float4 appendResult6_g11846 = (float4(lerpResult31_g11846 , break5_g11846.a));
				float4 staticSwitch33_g11846 = appendResult6_g11846;
				#else
				float4 staticSwitch33_g11846 = temp_output_1_0_g11846;
				#endif
				float4 temp_output_473_0 = staticSwitch33_g11846;
				#ifdef _ENABLEFULLDISTORTION_ON
				float4 break4_g11848 = temp_output_473_0;
				float fullDistortionAlpha164 = _FullDistortionFade;
				float4 appendResult5_g11848 = (float4(break4_g11848.r , break4_g11848.g , break4_g11848.b , ( break4_g11848.a * fullDistortionAlpha164 )));
				float4 staticSwitch77 = appendResult5_g11848;
				#else
				float4 staticSwitch77 = temp_output_473_0;
				#endif
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float4 break4_g11849 = staticSwitch77;
				float directionalDistortionAlpha167 = (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 ));
				float4 appendResult5_g11849 = (float4(break4_g11849.r , break4_g11849.g , break4_g11849.b , ( break4_g11849.a * directionalDistortionAlpha167 )));
				float4 staticSwitch75 = appendResult5_g11849;
				#else
				float4 staticSwitch75 = staticSwitch77;
				#endif
				float4 temp_output_1_0_g11850 = staticSwitch75;
				float4 temp_output_1_0_g11851 = temp_output_1_0_g11850;
				float temp_output_53_0_g11851 = max( _FullAlphaDissolveWidth , 0.001 );
				float2 temp_output_18_0_g11850 = shaderPosition235;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
				float linValue16_g11852 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11852 = MyCustomExpression16_g11852( linValue16_g11852 );
				float clampResult17_g11851 = clamp( ( ( ( _FullAlphaDissolveFade * ( 1.0 + temp_output_53_0_g11851 ) ) - localMyCustomExpression16_g11852 ) / temp_output_53_0_g11851 ) , 0.0 , 1.0 );
				float4 appendResult3_g11851 = (float4((temp_output_1_0_g11851).rgb , ( temp_output_1_0_g11851.a * clampResult17_g11851 )));
				float4 staticSwitch3_g11850 = appendResult3_g11851;
				#else
				float4 staticSwitch3_g11850 = temp_output_1_0_g11850;
				#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
				float linValue16_g11860 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11860 = MyCustomExpression16_g11860( linValue16_g11860 );
				float temp_output_5_0_g11859 = localMyCustomExpression16_g11860;
				float temp_output_61_0_g11859 = step( temp_output_5_0_g11859 , _FullGlowDissolveFade );
				float temp_output_53_0_g11859 = max( ( _FullGlowDissolveFade * _FullGlowDissolveWidth ) , 0.001 );
				float4 temp_output_1_0_g11859 = staticSwitch3_g11850;
				float4 appendResult3_g11859 = (float4(( ( (_FullGlowDissolveEdgeColor).rgb * ( temp_output_61_0_g11859 - step( temp_output_5_0_g11859 , ( ( _FullGlowDissolveFade * ( 1.01 + temp_output_53_0_g11859 ) ) - temp_output_53_0_g11859 ) ) ) ) + (temp_output_1_0_g11859).rgb ) , ( temp_output_1_0_g11859.a * temp_output_61_0_g11859 )));
				float4 staticSwitch5_g11850 = appendResult3_g11859;
				#else
				float4 staticSwitch5_g11850 = staticSwitch3_g11850;
				#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
				float4 temp_output_1_0_g11861 = staticSwitch5_g11850;
				float2 temp_output_76_0_g11861 = temp_output_18_0_g11850;
				float linValue16_g11862 = tex2D( _UberNoiseTexture, ( temp_output_76_0_g11861 * _SourceAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11862 = MyCustomExpression16_g11862( linValue16_g11862 );
				float clampResult17_g11861 = clamp( ( ( _SourceAlphaDissolveFade - ( distance( _SourceAlphaDissolvePosition , temp_output_76_0_g11861 ) + ( localMyCustomExpression16_g11862 * _SourceAlphaDissolveNoiseFactor ) ) ) / max( _SourceAlphaDissolveWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11861 = (float4((temp_output_1_0_g11861).rgb , ( temp_output_1_0_g11861.a * (( _SourceAlphaDissolveInvert )?( ( 1.0 - clampResult17_g11861 ) ):( clampResult17_g11861 )) )));
				float4 staticSwitch8_g11850 = appendResult3_g11861;
				#else
				float4 staticSwitch8_g11850 = staticSwitch5_g11850;
				#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
				float2 temp_output_90_0_g11857 = temp_output_18_0_g11850;
				float linValue16_g11858 = tex2D( _UberNoiseTexture, ( temp_output_90_0_g11857 * _SourceGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11858 = MyCustomExpression16_g11858( linValue16_g11858 );
				float temp_output_65_0_g11857 = ( distance( _SourceGlowDissolvePosition , temp_output_90_0_g11857 ) + ( localMyCustomExpression16_g11858 * _SourceGlowDissolveNoiseFactor ) );
				float temp_output_75_0_g11857 = step( temp_output_65_0_g11857 , _SourceGlowDissolveFade );
				float temp_output_76_0_g11857 = step( temp_output_65_0_g11857 , ( _SourceGlowDissolveFade - max( _SourceGlowDissolveWidth , 0.001 ) ) );
				float4 temp_output_1_0_g11857 = staticSwitch8_g11850;
				float4 appendResult3_g11857 = (float4(( ( max( ( temp_output_75_0_g11857 - temp_output_76_0_g11857 ) , 0.0 ) * (_SourceGlowDissolveEdgeColor).rgb ) + (temp_output_1_0_g11857).rgb ) , ( temp_output_1_0_g11857.a * (( _SourceGlowDissolveInvert )?( ( 1.0 - temp_output_76_0_g11857 ) ):( temp_output_75_0_g11857 )) )));
				float4 staticSwitch9_g11850 = appendResult3_g11857;
				#else
				float4 staticSwitch9_g11850 = staticSwitch8_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
				float4 temp_output_1_0_g11853 = staticSwitch9_g11850;
				float2 temp_output_161_0_g11853 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11853 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_161_0_g11853 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalAlphaFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11853 = rotatedValue136_g11853;
				float linValue16_g11854 = tex2D( _UberNoiseTexture, ( temp_output_161_0_g11853 * _DirectionalAlphaFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11854 = MyCustomExpression16_g11854( linValue16_g11854 );
				float clampResult154_g11853 = clamp( ( ( break130_g11853.x + break130_g11853.y + _DirectionalAlphaFadeFade + ( localMyCustomExpression16_g11854 * _DirectionalAlphaFadeNoiseFactor ) ) / max( _DirectionalAlphaFadeWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11853 = (float4((temp_output_1_0_g11853).rgb , ( temp_output_1_0_g11853.a * (( _DirectionalAlphaFadeInvert )?( ( 1.0 - clampResult154_g11853 ) ):( clampResult154_g11853 )) )));
				float4 staticSwitch11_g11850 = appendResult3_g11853;
				#else
				float4 staticSwitch11_g11850 = staticSwitch9_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
				float2 temp_output_171_0_g11855 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11855 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_171_0_g11855 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalGlowFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11855 = rotatedValue136_g11855;
				float linValue16_g11856 = tex2D( _UberNoiseTexture, ( temp_output_171_0_g11855 * _DirectionalGlowFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11856 = MyCustomExpression16_g11856( linValue16_g11856 );
				float temp_output_168_0_g11855 = max( ( ( break130_g11855.x + break130_g11855.y + _DirectionalGlowFadeFade + ( localMyCustomExpression16_g11856 * _DirectionalGlowFadeNoiseFactor ) ) / max( _DirectionalGlowFadeWidth , 0.001 ) ) , 0.0 );
				float temp_output_161_0_g11855 = step( 0.1 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) );
				float4 temp_output_1_0_g11855 = staticSwitch11_g11850;
				float clampResult154_g11855 = clamp( temp_output_161_0_g11855 , 0.0 , 1.0 );
				float4 appendResult3_g11855 = (float4(( ( (_DirectionalGlowFadeEdgeColor).rgb * ( temp_output_161_0_g11855 - step( 1.0 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) ) ) ) + (temp_output_1_0_g11855).rgb ) , ( temp_output_1_0_g11855.a * clampResult154_g11855 )));
				float4 staticSwitch15_g11850 = appendResult3_g11855;
				#else
				float4 staticSwitch15_g11850 = staticSwitch11_g11850;
				#endif
				float4 temp_output_1_0_g11863 = staticSwitch15_g11850;
				float2 temp_output_126_0_g11863 = temp_output_18_0_g11850;
				float temp_output_121_0_g11863 = max( ( ( _HalftoneFade - distance( _HalftonePosition , temp_output_126_0_g11863 ) ) / max( 0.01 , _HalftoneFadeWidth ) ) , 0.0 );
				float2 appendResult11_g11864 = (float2(temp_output_121_0_g11863 , temp_output_121_0_g11863));
				float temp_output_17_0_g11864 = length( ( (( ( abs( temp_output_126_0_g11863 ) * _HalftoneTiling ) % float2( 1,1 ) )*2.0 + -1.0) / appendResult11_g11864 ) );
				#ifdef _ENABLEHALFTONE_ON
				float clampResult17_g11863 = clamp( saturate( ( ( 1.0 - temp_output_17_0_g11864 ) / fwidth( temp_output_17_0_g11864 ) ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11863 = (float4((temp_output_1_0_g11863).rgb , ( temp_output_1_0_g11863.a * (( _HalftoneInvert )?( ( 1.0 - clampResult17_g11863 ) ):( clampResult17_g11863 )) )));
				float4 staticSwitch13_g11850 = appendResult3_g11863;
				#else
				float4 staticSwitch13_g11850 = staticSwitch15_g11850;
				#endif
				float3 temp_output_3_0_g11866 = (_AddColorColor).rgb;
				#ifdef _ADDCOLORMASKTOGGLE_ON
				float2 uv_AddColorMask = IN.ase_texcoord8.xy * _AddColorMask_ST.xy + _AddColorMask_ST.zw;
				float4 tex2DNode19_g11866 = tex2D( _AddColorMask, uv_AddColorMask );
				float3 staticSwitch16_g11866 = ( temp_output_3_0_g11866 * ( (tex2DNode19_g11866).rgb * tex2DNode19_g11866.a ) );
				#else
				float3 staticSwitch16_g11866 = temp_output_3_0_g11866;
				#endif
				float4 temp_output_1_0_g11866 = staticSwitch13_g11850;
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
				float4 break2_g11868 = temp_output_1_0_g11866;
				float temp_output_9_0_g11867 = max( _AddColorContrast , 0.0 );
				float saferPower7_g11867 = abs( ( ( ( break2_g11868.x + break2_g11868.x + break2_g11868.y + break2_g11868.y + break2_g11868.y + break2_g11868.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11867 ) , 0.0 ) ) ) );
				float3 staticSwitch17_g11866 = ( staticSwitch16_g11866 * pow( saferPower7_g11867 , temp_output_9_0_g11867 ) );
				#else
				float3 staticSwitch17_g11866 = staticSwitch16_g11866;
				#endif
				#ifdef _ENABLEADDCOLOR_ON
				float4 appendResult6_g11866 = (float4(( ( staticSwitch17_g11866 * _AddColorFade ) + (temp_output_1_0_g11866).rgb ) , temp_output_1_0_g11866.a));
				float4 staticSwitch5_g11865 = appendResult6_g11866;
				#else
				float4 staticSwitch5_g11865 = staticSwitch13_g11850;
				#endif
				#ifdef _ENABLEALPHATINT_ON
				float4 temp_output_1_0_g11869 = staticSwitch5_g11865;
				float3 lerpResult4_g11869 = lerp( (temp_output_1_0_g11869).rgb , (_AlphaTintColor).rgb , ( ( 1.0 - temp_output_1_0_g11869.a ) * step( _AlphaTintMinAlpha , temp_output_1_0_g11869.a ) * _AlphaTintFade ));
				float4 appendResult13_g11869 = (float4(lerpResult4_g11869 , temp_output_1_0_g11869.a));
				float4 staticSwitch11_g11865 = appendResult13_g11869;
				#else
				float4 staticSwitch11_g11865 = staticSwitch5_g11865;
				#endif
				float4 temp_output_1_0_g11870 = staticSwitch11_g11865;
				float3 temp_output_6_0_g11870 = (_StrongTintTint).rgb;
				#ifdef _STRONGTINTMASKTOGGLE_ON
				float2 uv_StrongTintMask = IN.ase_texcoord8.xy * _StrongTintMask_ST.xy + _StrongTintMask_ST.zw;
				float4 tex2DNode23_g11870 = tex2D( _StrongTintMask, uv_StrongTintMask );
				float3 staticSwitch21_g11870 = ( temp_output_6_0_g11870 * ( (tex2DNode23_g11870).rgb * tex2DNode23_g11870.a ) );
				#else
				float3 staticSwitch21_g11870 = temp_output_6_0_g11870;
				#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
				float4 break2_g11872 = temp_output_1_0_g11870;
				float temp_output_9_0_g11871 = max( _StrongTintContrast , 0.0 );
				float saferPower7_g11871 = abs( ( ( ( break2_g11872.x + break2_g11872.x + break2_g11872.y + break2_g11872.y + break2_g11872.y + break2_g11872.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11871 ) , 0.0 ) ) ) );
				float3 staticSwitch22_g11870 = ( pow( saferPower7_g11871 , temp_output_9_0_g11871 ) * staticSwitch21_g11870 );
				#else
				float3 staticSwitch22_g11870 = staticSwitch21_g11870;
				#endif
				#ifdef _ENABLESTRONGTINT_ON
				float3 lerpResult7_g11870 = lerp( (temp_output_1_0_g11870).rgb , staticSwitch22_g11870 , _StrongTintFade);
				float4 appendResult9_g11870 = (float4(lerpResult7_g11870 , (temp_output_1_0_g11870).a));
				float4 staticSwitch7_g11865 = appendResult9_g11870;
				#else
				float4 staticSwitch7_g11865 = staticSwitch11_g11865;
				#endif
				float4 temp_output_2_0_g11873 = staticSwitch7_g11865;
				#ifdef _ENABLESHADOW_ON
				float4 break4_g11875 = temp_output_2_0_g11873;
				float3 appendResult5_g11875 = (float3(break4_g11875.r , break4_g11875.g , break4_g11875.b));
				float2 appendResult2_g11874 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float4 appendResult85_g11873 = (float4(_ShadowColor.r , _ShadowColor.g , _ShadowColor.b , ( _ShadowFade * tex2D( _MainTex, ( finalUV146 - ( ( 100.0 / appendResult2_g11874 ) * _ShadowOffset ) ) ).a )));
				float4 break6_g11875 = appendResult85_g11873;
				float3 appendResult7_g11875 = (float3(break6_g11875.r , break6_g11875.g , break6_g11875.b));
				float temp_output_11_0_g11875 = ( ( 1.0 - break4_g11875.a ) * break6_g11875.a );
				float temp_output_32_0_g11875 = ( break4_g11875.a + temp_output_11_0_g11875 );
				float4 appendResult18_g11875 = (float4(( ( ( appendResult5_g11875 * break4_g11875.a ) + ( appendResult7_g11875 * temp_output_11_0_g11875 ) ) * ( 1.0 / max( temp_output_32_0_g11875 , 0.01 ) ) ) , temp_output_32_0_g11875));
				float4 staticSwitch82_g11873 = appendResult18_g11875;
				#else
				float4 staticSwitch82_g11873 = temp_output_2_0_g11873;
				#endif
				float4 break4_g11876 = staticSwitch82_g11873;
				#ifdef _ENABLECUSTOMFADE_ON
				float staticSwitch8_g11756 = 1.0;
				#else
				float staticSwitch8_g11756 = IN.ase_color.a;
				#endif
				#ifdef _ENABLESMOKE_ON
				float staticSwitch9_g11756 = 1.0;
				#else
				float staticSwitch9_g11756 = staticSwitch8_g11756;
				#endif
				float customVertexAlpha193 = staticSwitch9_g11756;
				float4 appendResult5_g11876 = (float4(break4_g11876.r , break4_g11876.g , break4_g11876.b , ( break4_g11876.a * customVertexAlpha193 )));
				float4 temp_output_344_0 = appendResult5_g11876;
				float4 temp_output_1_0_g11877 = temp_output_344_0;
				float4 appendResult8_g11877 = (float4(( (temp_output_1_0_g11877).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11877.a));
				#ifdef _VERTEXTINTFIRST_ON
				float4 staticSwitch342 = temp_output_344_0;
				#else
				float4 staticSwitch342 = appendResult8_g11877;
				#endif
				float4 lerpResult125 = lerp( ( originalColor191 * IN.ase_color ) , staticSwitch342 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float4 staticSwitch143 = staticSwitch342;
				#elif defined(_SHADERFADING_FULL)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_MASK)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_DISSOLVE)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_SPREAD)
				float4 staticSwitch143 = lerpResult125;
				#else
				float4 staticSwitch143 = staticSwitch342;
				#endif
				float4 temp_output_7_0_g11884 = staticSwitch143;
				#ifdef _BAKEDMATERIAL_ON
				float4 appendResult2_g11884 = (float4(( (temp_output_7_0_g11884).rgb / max( temp_output_7_0_g11884.a , 1E-05 ) ) , temp_output_7_0_g11884.a));
				float4 staticSwitch6_g11884 = appendResult2_g11884;
				#else
				float4 staticSwitch6_g11884 = temp_output_7_0_g11884;
				#endif
				float4 temp_output_340_0 = staticSwitch6_g11884;
				
				float2 temp_output_11_0_g11885 = finalUV146;
				
				#ifdef _EMISSIONTOGGLE_ON
				float3 appendResult20_g11885 = (float3(_EmissionTint.r , _EmissionTint.g , _EmissionTint.b));
				float2 uv_EmissionMap = IN.ase_texcoord8.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
				float4 tex2DNode17_g11885 = tex2D( _EmissionMap, uv_EmissionMap );
				float3 appendResult18_g11885 = (float3(tex2DNode17_g11885.r , tex2DNode17_g11885.g , tex2DNode17_g11885.b));
				float3 staticSwitch13_g11885 = ( appendResult20_g11885 * appendResult18_g11885 * tex2DNode17_g11885.a );
				#else
				float3 staticSwitch13_g11885 = float3(0,0,0);
				#endif
				
				float4 tex2DNode7_g11885 = tex2D( _MetallicMap, temp_output_11_0_g11885 );
				#ifdef _METALLICMAPTOGGLE_ON
				float staticSwitch23_g11885 = ( tex2DNode7_g11885.r * _Metallic );
				#else
				float staticSwitch23_g11885 = _Metallic;
				#endif
				
				#ifdef _METALLICMAPTOGGLE_ON
				float staticSwitch22_g11885 = ( _Smoothness * tex2DNode7_g11885.r );
				#else
				float staticSwitch22_g11885 = _Smoothness;
				#endif
				
				o.Albedo = temp_output_340_0.rgb;
				o.Normal = UnpackScaleNormal( tex2D( _NormalMap, temp_output_11_0_g11885 ), _NormalIntensity );
				o.Emission = staticSwitch13_g11885;
				#if defined(_SPECULAR_SETUP)
					o.Specular = fixed3( 0, 0, 0 );
				#else
					o.Metallic = staticSwitch23_g11885;
				#endif
				o.Smoothness = staticSwitch22_g11885;
				o.Occlusion = 1;
				o.Alpha = temp_output_340_0.a;
				float AlphaClipThreshold = _AlphaClip;
				float3 BakedGI = 0;

				#ifdef _ALPHATEST_ON
					clip( o.Alpha - AlphaClipThreshold );
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = IN.pos.z;
				#endif

				#ifndef USING_DIRECTIONAL_LIGHT
					fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				#else
					fixed3 lightDir = _WorldSpaceLightPos0.xyz;
				#endif

				float3 worldN;
				worldN.x = dot(IN.tSpace0.xyz, o.Normal);
				worldN.y = dot(IN.tSpace1.xyz, o.Normal);
				worldN.z = dot(IN.tSpace2.xyz, o.Normal);
				worldN = normalize(worldN);
				o.Normal = worldN;

				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
				gi.indirect.diffuse = 0;
				gi.indirect.specular = 0;
				gi.light.color = 0;
				gi.light.dir = half3(0,1,0);

				UnityGIInput giInput;
				UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
				giInput.light = gi.light;
				giInput.worldPos = worldPos;
				giInput.worldViewDir = worldViewDir;
				giInput.atten = atten;
				#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
					giInput.lightmapUV = IN.lmap;
				#else
					giInput.lightmapUV = 0.0;
				#endif
				#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
					giInput.ambient = IN.sh;
				#else
					giInput.ambient.rgb = 0.0;
				#endif
				giInput.probeHDR[0] = unity_SpecCube0_HDR;
				giInput.probeHDR[1] = unity_SpecCube1_HDR;
				#if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
					giInput.boxMin[0] = unity_SpecCube0_BoxMin;
				#endif
				#ifdef UNITY_SPECCUBE_BOX_PROJECTION
					giInput.boxMax[0] = unity_SpecCube0_BoxMax;
					giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
					giInput.boxMax[1] = unity_SpecCube1_BoxMax;
					giInput.boxMin[1] = unity_SpecCube1_BoxMin;
					giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
				#endif

				#if defined(_SPECULAR_SETUP)
					LightingStandardSpecular_GI( o, giInput, gi );
				#else
					LightingStandard_GI( o, giInput, gi );
				#endif

				#ifdef ASE_BAKEDGI
					gi.indirect.diffuse = BakedGI;
				#endif

				#if UNITY_SHOULD_SAMPLE_SH && !defined(LIGHTMAP_ON) && defined(ASE_NO_AMBIENT)
					gi.indirect.diffuse = 0;
				#endif

				#if defined(_SPECULAR_SETUP)
					outEmission = LightingStandardSpecular_Deferred( o, worldViewDir, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
				#else
					outEmission = LightingStandard_Deferred( o, worldViewDir, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
				#endif

				#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
					outShadowMask = UnityGetRawBakedOcclusions (IN.lmap.xy, float3(0, 0, 0));
				#endif
				#ifndef UNITY_HDR_ON
					outEmission.rgb = exp2(-outEmission.rgb);
				#endif
			}
			ENDCG
		}

		
		Pass
		{
			
			Name "Meta"
			Tags { "LightMode"="Meta" }
			Cull Off

			CGPROGRAM
			#define ASE_NEEDS_FRAG_SHADOWCOORDS
			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _ALPHATEST_ON 1

			#pragma vertex vert
			#pragma fragment frag
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#pragma shader_feature EDITOR_VISUALIZATION
			#ifndef UNITY_PASS_META
				#define UNITY_PASS_META
			#endif
			#include "HLSLSupport.cginc"
			#if !defined( UNITY_INSTANCED_LOD_FADE )
				#define UNITY_INSTANCED_LOD_FADE
			#endif
			#if !defined( UNITY_INSTANCED_SH )
				#define UNITY_INSTANCED_SH
			#endif
			#if !defined( UNITY_INSTANCED_LIGHTMAPSTS )
				#define UNITY_INSTANCED_LIGHTMAPSTS
			#endif
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			#include "UnityMetaPass.cginc"

			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_POSITION
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _SHADERFADING_NONE _SHADERFADING_FULL _SHADERFADING_MASK _SHADERFADING_DISSOLVE _SHADERFADING_SPREAD
			#pragma shader_feature_local _ENABLESINESCALE_ON
			#pragma shader_feature _ENABLEVIBRATE_ON
			#pragma shader_feature _ENABLESINEMOVE_ON
			#pragma shader_feature _ENABLESQUISH_ON
			#pragma shader_feature _SPRITESHEETFIX_ON
			#pragma shader_feature_local _PIXELPERFECTUV_ON
			#pragma shader_feature _ENABLEWORLDTILING_ON
			#pragma shader_feature _ENABLESCREENTILING_ON
			#pragma shader_feature _TOGGLETIMEFREQUENCY_ON
			#pragma shader_feature _TOGGLETIMEFPS_ON
			#pragma shader_feature _TOGGLETIMESPEED_ON
			#pragma shader_feature _TOGGLEUNSCALEDTIME_ON
			#pragma shader_feature _TOGGLECUSTOMTIME_ON
			#pragma shader_feature _SHADERSPACE_UV _SHADERSPACE_UV_RAW _SHADERSPACE_OBJECT _SHADERSPACE_OBJECT_SCALED _SHADERSPACE_WORLD _SHADERSPACE_UI_GRAPHIC _SHADERSPACE_SCREEN
			#pragma shader_feature _PIXELPERFECTSPACE_ON
			#pragma shader_feature _BAKEDMATERIAL_ON
			#pragma shader_feature _VERTEXTINTFIRST_ON
			#pragma shader_feature _ENABLESHADOW_ON
			#pragma shader_feature _ENABLESTRONGTINT_ON
			#pragma shader_feature _ENABLEALPHATINT_ON
			#pragma shader_feature_local _ENABLEADDCOLOR_ON
			#pragma shader_feature_local _ENABLEHALFTONE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALGLOWFADE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALALPHAFADE_ON
			#pragma shader_feature_local _ENABLESOURCEGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLESOURCEALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALDISTORTION_ON
			#pragma shader_feature_local _ENABLEFULLDISTORTION_ON
			#pragma shader_feature _ENABLESHIFTING_ON
			#pragma shader_feature _ENABLEENCHANTED_ON
			#pragma shader_feature_local _ENABLEPOISON_ON
			#pragma shader_feature_local _ENABLESHINE_ON
			#pragma shader_feature_local _ENABLERAINBOW_ON
			#pragma shader_feature_local _ENABLEBURN_ON
			#pragma shader_feature_local _ENABLEFROZEN_ON
			#pragma shader_feature_local _ENABLEMETAL_ON
			#pragma shader_feature_local _ENABLECAMOUFLAGE_ON
			#pragma shader_feature_local _ENABLEGLITCH_ON
			#pragma shader_feature_local _ENABLEHOLOGRAM_ON
			#pragma shader_feature _ENABLEPINGPONGGLOW_ON
			#pragma shader_feature_local _ENABLEPIXELOUTLINE_ON
			#pragma shader_feature_local _ENABLEOUTEROUTLINE_ON
			#pragma shader_feature_local _ENABLEINNEROUTLINE_ON
			#pragma shader_feature_local _ENABLESATURATION_ON
			#pragma shader_feature_local _ENABLESINEGLOW_ON
			#pragma shader_feature_local _ENABLEADDHUE_ON
			#pragma shader_feature_local _ENABLESHIFTHUE_ON
			#pragma shader_feature_local _ENABLEINKSPREAD_ON
			#pragma shader_feature_local _ENABLEBLACKTINT_ON
			#pragma shader_feature_local _ENABLESPLITTONING_ON
			#pragma shader_feature_local _ENABLEHUE_ON
			#pragma shader_feature_local _ENABLEBRIGHTNESS_ON
			#pragma shader_feature_local _ENABLECONTRAST_ON
			#pragma shader_feature _ENABLENEGATIVE_ON
			#pragma shader_feature_local _ENABLECOLORREPLACE_ON
			#pragma shader_feature_local _ENABLERECOLORRGBYCP_ON
			#pragma shader_feature _ENABLERECOLORRGB_ON
			#pragma shader_feature_local _ENABLEFLAME_ON
			#pragma shader_feature_local _ENABLECHECKERBOARD_ON
			#pragma shader_feature_local _ENABLECUSTOMFADE_ON
			#pragma shader_feature_local _ENABLESMOKE_ON
			#pragma shader_feature _ENABLESHARPEN_ON
			#pragma shader_feature _ENABLEGAUSSIANBLUR_ON
			#pragma shader_feature _ENABLESMOOTHPIXELART_ON
			#pragma shader_feature_local _TILINGFIX_ON
			#pragma shader_feature _ENABLEWIGGLE_ON
			#pragma shader_feature_local _ENABLEUVSCALE_ON
			#pragma shader_feature_local _ENABLEPIXELATE_ON
			#pragma shader_feature_local _ENABLEUVSCROLL_ON
			#pragma shader_feature_local _ENABLEUVROTATE_ON
			#pragma shader_feature_local _ENABLESINEROTATE_ON
			#pragma shader_feature_local _ENABLESQUEEZE_ON
			#pragma shader_feature_local _ENABLEUVDISTORT_ON
			#pragma shader_feature_local _ENABLEWIND_ON
			#pragma shader_feature_local _WINDLOCALWIND_ON
			#pragma shader_feature_local _WINDHIGHQUALITYNOISE_ON
			#pragma shader_feature_local _WINDISPARALLAX_ON
			#pragma shader_feature _UVDISTORTMASKTOGGLE_ON
			#pragma shader_feature _WIGGLEFIXEDGROUNDTOGGLE_ON
			#pragma shader_feature _RECOLORRGBTEXTURETOGGLE_ON
			#pragma shader_feature _RECOLORRGBYCPTEXTURETOGGLE_ON
			#pragma shader_feature_local _ADDHUEMASKTOGGLE_ON
			#pragma shader_feature_local _SINEGLOWMASKTOGGLE_ON
			#pragma shader_feature _INNEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature_local _INNEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _INNEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _OUTEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature_local _OUTEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _PIXELOUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _CAMOUFLAGEANIMATIONTOGGLE_ON
			#pragma shader_feature _METALMASKTOGGLE_ON
			#pragma shader_feature _SHINEMASKTOGGLE_ON
			#pragma shader_feature _ENCHANTEDLERPTOGGLE_ON
			#pragma shader_feature _ENCHANTEDRAINBOWTOGGLE_ON
			#pragma shader_feature _SHIFTINGRAINBOWTOGGLE_ON
			#pragma shader_feature _ADDCOLORCONTRASTTOGGLE_ON
			#pragma shader_feature _ADDCOLORMASKTOGGLE_ON
			#pragma shader_feature _STRONGTINTCONTRASTTOGGLE_ON
			#pragma shader_feature _STRONGTINTMASKTOGGLE_ON
			#pragma shader_feature _EMISSIONTOGGLE_ON

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct v2f {
				#if UNITY_VERSION >= 201810
					UNITY_POSITION(pos);
				#else
					float4 pos : SV_POSITION;
				#endif
				#ifdef EDITOR_VISUALIZATION
					float2 vizUV : TEXCOORD1;
					float4 lightCoord : TEXCOORD2;
				#endif
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishStretch;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingScale;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingOffset;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float _ScreenTilingPixelsPerUnit;
			#endif
			uniform sampler2D _MainTex;
			float4 _MainTex_TexelSize;
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingScale;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingOffset;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float _WorldTilingPixelsPerUnit;
			#endif
			uniform float4 _SpriteSheetRect;
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFade;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFlip;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishSquish;
			#endif
				#ifdef _TOGGLECUSTOMTIME_ON
			uniform float _TimeValue;
			#endif
			uniform float UnscaledTime;
				#ifdef _TOGGLETIMESPEED_ON
			uniform float _TimeSpeed;
			#endif
				#ifdef _TOGGLETIMEFPS_ON
			uniform float _TimeFPS;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeFrequency;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeRange;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveFrequency;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveOffset;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float _SineMoveFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFrequency;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateOffset;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateRotation;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float _SineScaleFrequency;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float2 _SineScaleFactor;
			#endif
			uniform float _FadingFade;
			uniform sampler2D _FadingMask;
			uniform float4 _FadingMask_ST;
			uniform float _FadingWidth;
			uniform sampler2D _UberNoiseTexture;
			uniform float _PixelsPerUnit;
			uniform float _RectWidth;
			uniform float _RectHeight;
			uniform float _ScreenWidthUnits;
			uniform float2 _FadingNoiseScale;
			uniform float2 _FadingPosition;
			uniform float _FadingNoiseFactor;
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotationWindFactor;
			#endif
			uniform float WindMinIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMinIntensity;
			#endif
			uniform float WindMaxIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMaxIntensity;
			#endif
				#ifdef _WINDISPARALLAX_ON
			uniform float _WindXPosition;
			#endif
			uniform float WindNoiseScale;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseScale;
			#endif
			uniform float WindTime;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseSpeed;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindMaxRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindFlip;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishFactor;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishWindFactor;
			#endif
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float _FullDistortionFade;
			#endif
			uniform float2 _FullDistortionNoiseScale;
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float2 _FullDistortionDistortion;
			#endif
			uniform float2 _DirectionalDistortionDistortionScale;
			uniform float _DirectionalDistortionRandomDirection;
			uniform float2 _DirectionalDistortionDistortion;
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
			uniform float _DirectionalDistortionInvert;
			#endif
			uniform float _DirectionalDistortionRotation;
			uniform float _DirectionalDistortionFade;
			uniform float2 _DirectionalDistortionNoiseScale;
			uniform float _DirectionalDistortionNoiseFactor;
			uniform float _DirectionalDistortionWidth;
			uniform float _HologramDistortionSpeed;
			uniform float _HologramDistortionDensity;
			uniform float _HologramDistortionScale;
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramDistortionOffset;
			#endif
			uniform float _HologramFade;
			uniform float2 _GlitchDistortionSpeed;
			uniform float2 _GlitchDistortionScale;
			uniform float2 _GlitchDistortion;
			uniform float2 _GlitchMaskSpeed;
			uniform float2 _GlitchMaskScale;
			uniform float _GlitchMaskMin;
			uniform float _GlitchFade;
			uniform float2 _UVDistortFrom;
			uniform float2 _UVDistortTo;
			uniform float2 _UVDistortSpeed;
			uniform float2 _UVDistortNoiseScale;
			uniform float _UVDistortFade;
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform sampler2D _UVDistortMask;
			#endif
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform float4 _UVDistortMask_ST;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeCenter;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezePower;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeScale;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezeFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFrequency;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateAngle;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float2 _SineRotatePivot;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float _UVRotateSpeed;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float2 _UVRotatePivot;
			#endif
				#ifdef _ENABLEUVSCROLL_ON
			uniform float2 _UVScrollSpeed;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelDensity;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelsPerUnit;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelateFade;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScalePivot;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScaleScale;
			#endif
			uniform float _WiggleFrequency;
			uniform float _WiggleSpeed;
			uniform float _WiggleOffset;
			uniform float _WiggleFade;
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurOffset;
			#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurFade;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenOffset;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFactor;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFade;
			#endif
			uniform float _SmokeVertexSeed;
			uniform float _SmokeNoiseScale;
			uniform float _SmokeNoiseFactor;
			uniform float _SmokeSmoothness;
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeDarkEdge;
			#endif
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeAlpha;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform sampler2D _CustomFadeFadeMask;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float2 _CustomFadeNoiseScale;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeNoiseFactor;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeSmoothness;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeAlpha;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardDarken;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardTiling;
			#endif
			uniform float2 _FlameSpeed;
			uniform float2 _FlameNoiseScale;
			uniform float _FlameNoiseHeightFactor;
			uniform float _FlameNoiseFactor;
			uniform float _FlameRadius;
			uniform float _FlameSmooth;
				#ifdef _ENABLEFLAME_ON
			uniform float _FlameBrightness;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBRedTint;
			#endif
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBTexture;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBGreenTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBBlueTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float _RecolorRGBFade;
			#endif
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBYCPTexture;
			#endif
			uniform float4 _RecolorRGBYCPPurpleTint;
			uniform float4 _RecolorRGBYCPBlueTint;
			uniform float4 _RecolorRGBYCPCyanTint;
			uniform float4 _RecolorRGBYCPGreenTint;
			uniform float4 _RecolorRGBYCPYellowTint;
			uniform float4 _RecolorRGBYCPRedTint;
				#ifdef _ENABLERECOLORRGBYCP_ON
			uniform float _RecolorRGBYCPFade;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceFromColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceContrast;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceToColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceSmoothness;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceRange;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceFade;
			#endif
				#ifdef _ENABLENEGATIVE_ON
			uniform float _NegativeFade;
			#endif
				#ifdef _ENABLECONTRAST_ON
			uniform float _Contrast;
			#endif
				#ifdef _ENABLEBRIGHTNESS_ON
			uniform float _Brightness;
			#endif
				#ifdef _ENABLEHUE_ON
			uniform float _Hue;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningShadowsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningHighlightsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningShift;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningBalance;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningContrast;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningFade;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float4 _BlackTintColor;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintPower;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float4 _InkSpreadColor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadContrast;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadDistance;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadPosition;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadNoiseScale;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadNoiseFactor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadWidth;
			#endif
				#ifdef _ENABLESHIFTHUE_ON
			uniform float _ShiftHueSpeed;
			#endif
			uniform float _AddHueSpeed;
			uniform float _AddHueSaturation;
			uniform float _AddHueBrightness;
				#ifdef _ENABLEADDHUE_ON
			uniform float _AddHueContrast;
			#endif
			uniform float _AddHueFade;
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform sampler2D _AddHueMask;
			#endif
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform float4 _AddHueMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowContrast;
			#endif
			uniform float4 _SineGlowColor;
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform sampler2D _SineGlowMask;
			#endif
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform float4 _SineGlowMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFade;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFrequency;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMax;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMin;
			#endif
				#ifdef _ENABLESATURATION_ON
			uniform float _Saturation;
			#endif
			uniform float4 _InnerOutlineColor;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _InnerOutlineTintTexture;
			#endif
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform float2 _InnerOutlineTextureSpeed;
			#endif
			uniform float _InnerOutlineFade;
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseSpeed;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseScale;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineDistortionIntensity;
			#endif
			uniform float _InnerOutlineWidth;
			uniform float4 _OuterOutlineColor;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _OuterOutlineTintTexture;
			#endif
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform float2 _OuterOutlineTextureSpeed;
			#endif
			uniform float _OuterOutlineFade;
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseSpeed;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseScale;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineDistortionIntensity;
			#endif
			uniform float _OuterOutlineWidth;
			uniform float4 _PixelOutlineColor;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform sampler2D _PixelOutlineTintTexture;
			#endif
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform float2 _PixelOutlineTextureSpeed;
			#endif
			uniform float _PixelOutlineFade;
			uniform float _PixelOutlineWidth;
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowFrom;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowTo;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFrequency;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFade;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float4 _HologramTint;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineSpeed;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineFrequency;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineGap;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramMinAlpha;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchBrightness;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseSpeed;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseScale;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchHueSpeed;
			#endif
			uniform float4 _CamouflageBaseColor;
			uniform float4 _CamouflageColorA;
			uniform float _CamouflageDensityA;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionSpeed;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionScale;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionIntensity;
			#endif
			uniform float2 _CamouflageNoiseScaleA;
			uniform float _CamouflageSmoothnessA;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float4 _CamouflageColorB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageDensityB;
			#endif
			uniform float2 _CamouflageNoiseScaleB;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageSmoothnessB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageContrast;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageFade;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalHighlightDensity;
			#endif
			uniform float2 _MetalNoiseDistortionSpeed;
			uniform float2 _MetalNoiseDistortionScale;
			uniform float2 _MetalNoiseDistortion;
			uniform float2 _MetalNoiseSpeed;
			uniform float2 _MetalNoiseScale;
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalHighlightColor;
			#endif
			uniform float _MetalHighlightContrast;
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalContrast;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalColor;
			#endif
			uniform float _MetalFade;
				#ifdef _METALMASKTOGGLE_ON
			uniform sampler2D _MetalMask;
			#endif
				#ifdef _METALMASKTOGGLE_ON
			uniform float4 _MetalMask_ST;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenTint;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenSnowColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenSnowScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortion;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenHighlightColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenFade;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideContrast;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideNoiseColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideNoiseFactor;
			#endif
			uniform float2 _BurnSwirlNoiseScale;
			uniform float _BurnSwirlFactor;
			uniform float2 _BurnInsideNoiseScale;
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnRadius;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnPosition;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnEdgeNoiseScale;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnEdgeNoiseFactor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnWidth;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnEdgeColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnFade;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowCenter;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowNoiseScale;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowNoiseFactor;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowDensity;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSpeed;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSaturation;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowBrightness;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowContrast;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowFade;
			#endif
			uniform float _ShineSaturation;
			uniform float _ShineContrast;
				#ifdef _ENABLESHINE_ON
			uniform float4 _ShineColor;
			#endif
			uniform float _ShineRotation;
			uniform float _ShineFrequency;
			uniform float _ShineSpeed;
			uniform float _ShineWidth;
			uniform float _ShineFade;
				#ifdef _SHINEMASKTOGGLE_ON
			uniform sampler2D _ShineMask;
			#endif
				#ifdef _SHINEMASKTOGGLE_ON
			uniform float4 _ShineMask_ST;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseScale;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonShiftSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonDensity;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float4 _PoisonColor;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonFade;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonNoiseBrightness;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonRecolorFactor;
			#endif
			uniform float4 _EnchantedLowColor;
			uniform float4 _EnchantedHighColor;
			uniform float2 _EnchantedSpeed;
			uniform float2 _EnchantedScale;
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowDensity;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSpeed;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSaturation;
			#endif
			uniform float _EnchantedContrast;
			uniform float _EnchantedBrightness;
			uniform float _EnchantedReduce;
			uniform float _EnchantedFade;
			uniform float4 _ShiftingColorA;
			uniform float4 _ShiftingColorB;
			uniform float _ShiftingSpeed;
			uniform float _ShiftingDensity;
			uniform float _ShiftingBrightness;
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
			uniform float _ShiftingSaturation;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingContrast;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingFade;
			#endif
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float _FullAlphaDissolveFade;
			#endif
			uniform float _FullAlphaDissolveWidth;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float2 _FullAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float4 _FullGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float2 _FullGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveFade;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveInvert;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveFade;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveFade;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float4 _SourceGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float2 _DirectionalAlphaFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeWidth;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float4 _DirectionalGlowFadeEdgeColor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float2 _DirectionalGlowFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeWidth;
			#endif
				#ifdef _ENABLEHALFTONE_ON
			uniform float _HalftoneInvert;
			#endif
			uniform float _HalftoneTiling;
			uniform float _HalftoneFade;
			uniform float2 _HalftonePosition;
			uniform float _HalftoneFadeWidth;
			uniform float4 _AddColorColor;
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform sampler2D _AddColorMask;
			#endif
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform float4 _AddColorMask_ST;
			#endif
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
			uniform float _AddColorContrast;
			#endif
				#ifdef _ENABLEADDCOLOR_ON
			uniform float _AddColorFade;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float4 _AlphaTintColor;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintMinAlpha;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintFade;
			#endif
			uniform float4 _StrongTintTint;
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform sampler2D _StrongTintMask;
			#endif
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform float4 _StrongTintMask_ST;
			#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
			uniform float _StrongTintContrast;
			#endif
				#ifdef _ENABLESTRONGTINT_ON
			uniform float _StrongTintFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float4 _ShadowColor;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float _ShadowFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float2 _ShadowOffset;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionTint;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform sampler2D _EmissionMap;
			#endif
				#ifdef _EMISSIONTOGGLE_ON
			uniform float4 _EmissionMap_ST;
			#endif
			uniform float _AlphaClip;

	
			float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
			{
				original -= center;
				float C = cos( angle );
				float S = sin( angle );
				float t = 1 - C;
				float m00 = t * u.x * u.x + C;
				float m01 = t * u.x * u.y - S * u.z;
				float m02 = t * u.x * u.z + S * u.y;
				float m10 = t * u.x * u.y + S * u.z;
				float m11 = t * u.y * u.y + C;
				float m12 = t * u.y * u.z - S * u.x;
				float m20 = t * u.x * u.z - S * u.y;
				float m21 = t * u.y * u.z + S * u.x;
				float m22 = t * u.z * u.z + C;
				float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
				return mul( finalMatrix, original ) + center;
			}
			
			float MyCustomExpression16_g11717( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11719( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float FastNoise101_g11668( float x )
			{
				float i = floor(x);
				float f = frac(x);
				float s = sign(frac(x/2.0)-0.5);
				    
				float k = 0.5+0.5*sin(i);
				return s*f*(f-1.0)*((16.0*k-4.0)*f*(f-1.0)-1.0);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			
			float MyCustomExpression16_g11670( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11671( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11674( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11673( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11679( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11680( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11721( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11676( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11732( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float4 texturePointSmooth( sampler2D tex, float4 textureTexelSize, float2 uvs )
			{
				float2 size;
				size.x = textureTexelSize.z;
				size.y = textureTexelSize.w;
				float2 pixel = float2(1.0,1.0) / size;
				uvs -= pixel * float2(0.5,0.5);
				float2 uv_pixels = uvs * size;
				float2 delta_pixel = frac(uv_pixels) - float2(0.5,0.5);
				float2 ddxy = fwidth(uv_pixels);
				float2 mip = log2(ddxy) - 0.5;
				float2 clampedUV = uvs + (clamp(delta_pixel / ddxy, 0.0, 1.0) - delta_pixel) * pixel;
				return tex2Dlod(tex, float4(clampedUV,0, min(mip.x, mip.y)));
			}
			
			float MyCustomExpression16_g11758( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11760( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11764( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 MyCustomExpression115_g11768( float3 In, float3 From, float3 To, float Fuzziness, float Range )
			{
				float Distance = distance(From, In);
				return lerp(To, In, saturate((Distance - Range) / max(Fuzziness, 0.001)));
			}
			
			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float MyCustomExpression16_g11787( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11774( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11798( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11805( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11838( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11835( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11837( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11828( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11830( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11823( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11825( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11826( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11819( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11817( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11818( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11813( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11841( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11845( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11843( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11852( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11860( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11862( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11858( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11854( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11856( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			

			v2f VertexFunction (appdata v  ) {
				UNITY_SETUP_INSTANCE_ID(v);
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 _ZeroVector = float2(0,0);
				float2 texCoord363 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLESCREENTILING_ON
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLEWORLDTILING_ON
				float2 staticSwitch2_g11657 = ( ( ( (ase_worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _PIXELPERFECTUV_ON
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				#ifdef _SPRITESHEETFIX_ON
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				#ifdef _ENABLESQUISH_ON
				float2 break77_g11879 = fixedUV475;
				float2 appendResult72_g11879 = (float2(( _SquishStretch * ( break77_g11879.x - 0.5 ) * _SquishFade ) , ( _SquishFade * ( break77_g11879.y + _SquishFlip ) * -_SquishSquish )));
				float2 staticSwitch198 = ( appendResult72_g11879 + _ZeroVector );
				#else
				float2 staticSwitch198 = _ZeroVector;
				#endif
				float2 temp_output_2_0_g11880 = staticSwitch198;
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				float temp_output_8_0_g11880 = shaderTime237;
				#ifdef _ENABLESINEMOVE_ON
				float2 staticSwitch4_g11880 = ( ( sin( ( temp_output_8_0_g11880 * _SineMoveFrequency ) ) * _SineMoveOffset * _SineMoveFade ) + temp_output_2_0_g11880 );
				#else
				float2 staticSwitch4_g11880 = temp_output_2_0_g11880;
				#endif
				#ifdef _ENABLEVIBRATE_ON
				float temp_output_30_0_g11881 = temp_output_8_0_g11880;
				float3 rotatedValue21_g11881 = RotateAroundAxis( float3( 0,0,0 ), float3( 0,1,0 ), float3( 0,0,1 ), ( temp_output_30_0_g11881 * _VibrateRotation ) );
				float2 staticSwitch6_g11880 = ( ( sin( ( _VibrateFrequency * temp_output_30_0_g11881 ) ) * _VibrateOffset * _VibrateFade * (rotatedValue21_g11881).xy ) + staticSwitch4_g11880 );
				#else
				float2 staticSwitch6_g11880 = staticSwitch4_g11880;
				#endif
				#ifdef _ENABLESINESCALE_ON
				float2 staticSwitch10_g11880 = ( staticSwitch6_g11880 + ( (v.vertex.xyz).xy * ( ( ( sin( ( _SineScaleFrequency * temp_output_8_0_g11880 ) ) + 1.0 ) * 0.5 ) * _SineScaleFactor ) ) );
				#else
				float2 staticSwitch10_g11880 = staticSwitch6_g11880;
				#endif
				float2 temp_output_424_0 = staticSwitch10_g11880;
				float2 uv_FadingMask = v.ase_texcoord.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2Dlod( _FadingMask, float4( uv_FadingMask, 0, 0.0) );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float2 texCoord435 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				#ifdef _PIXELPERFECTSPACE_ON
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (v.vertex.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (v.vertex.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (ase_worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float linValue16_g11717 = tex2Dlod( _UberNoiseTexture, float4( ( shaderPosition235 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2Dlod( _UberNoiseTexture, float4( ( temp_output_27_0_g11718 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult121 = lerp( float2( 0,0 ) , temp_output_424_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch142 = temp_output_424_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch142 = lerpResult121;
				#else
				float2 staticSwitch142 = temp_output_424_0;
				#endif
				
				o.ase_texcoord4 = screenPos;
				o.ase_texcoord5.xyz = ase_worldPos;
				
				o.ase_texcoord3.xy = v.ase_texcoord.xy;
				o.ase_texcoord6 = v.vertex;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;
				o.ase_texcoord5.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = float3( staticSwitch142 ,  0.0 );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.vertex.w = 1;
				v.normal = v.normal;
				v.tangent = v.tangent;

				#ifdef EDITOR_VISUALIZATION
					o.vizUV = 0;
					o.lightCoord = 0;
					if (unity_VisualizationMode == EDITORVIZ_TEXTURE)
						o.vizUV = UnityMetaVizUV(unity_EditorViz_UVIndex, v.texcoord.xy, v.texcoord1.xy, v.texcoord2.xy, unity_EditorViz_Texture_ST);
					else if (unity_VisualizationMode == EDITORVIZ_SHOWLIGHTMASK)
					{
						o.vizUV = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
						o.lightCoord = mul(unity_EditorViz_WorldToLight, mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)));
					}
				#endif

				o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);

				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( appdata v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.tangent = v.tangent;
				o.normal = v.normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				appdata o = (appdata) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
				o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			v2f vert ( appdata v )
			{
				return VertexFunction( v );
			}
			#endif

			fixed4 frag (v2f IN 
				#ifdef _DEPTHOFFSET_ON
				, out float outputDepth : SV_Depth
				#endif
				) : SV_Target 
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				
				#ifdef LOD_FADE_CROSSFADE
					UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
				#endif

				#if defined(_SPECULAR_SETUP)
					SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
				#else
					SurfaceOutputStandard o = (SurfaceOutputStandard)0;
				#endif
				
				float2 texCoord363 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float4 screenPos = IN.ase_texcoord4;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				#ifdef _ENABLESCREENTILING_ON
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				float3 ase_worldPos = IN.ase_texcoord5.xyz;
				#ifdef _ENABLEWORLDTILING_ON
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11657 = ( ( ( (ase_worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				#ifdef _SPRITESHEETFIX_ON
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				float2 temp_output_3_0_g11667 = fixedUV475;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch117_g11668 = _WindMinIntensity;
				#else
				float staticSwitch117_g11668 = WindMinIntensity;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch118_g11668 = _WindMaxIntensity;
				#else
				float staticSwitch118_g11668 = WindMaxIntensity;
				#endif
				float4 transform62_g11668 = mul(unity_WorldToObject,float4( 0,0,0,1 ));
				#ifdef _WINDISPARALLAX_ON
				float staticSwitch111_g11668 = _WindXPosition;
				#else
				float staticSwitch111_g11668 = transform62_g11668.x;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch113_g11668 = _WindNoiseScale;
				#else
				float staticSwitch113_g11668 = WindNoiseScale;
				#endif
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch125_g11668 = ( shaderTime237 * _WindNoiseSpeed );
				#else
				float staticSwitch125_g11668 = WindTime;
				#endif
				float temp_output_50_0_g11668 = ( ( staticSwitch111_g11668 * staticSwitch113_g11668 ) + staticSwitch125_g11668 );
				float x101_g11668 = temp_output_50_0_g11668;
				float localFastNoise101_g11668 = FastNoise101_g11668( x101_g11668 );
				float2 temp_cast_0 = (temp_output_50_0_g11668).xx;
				float simplePerlin2D121_g11668 = snoise( temp_cast_0*0.5 );
				simplePerlin2D121_g11668 = simplePerlin2D121_g11668*0.5 + 0.5;
				#ifdef _WINDHIGHQUALITYNOISE_ON
				float staticSwitch123_g11668 = simplePerlin2D121_g11668;
				#else
				float staticSwitch123_g11668 = ( localFastNoise101_g11668 + 0.5 );
				#endif
				#ifdef _ENABLEWIND_ON
				float lerpResult86_g11668 = lerp( staticSwitch117_g11668 , staticSwitch118_g11668 , staticSwitch123_g11668);
				float clampResult29_g11668 = clamp( ( ( _WindRotationWindFactor * lerpResult86_g11668 ) + _WindRotation ) , -_WindMaxRotation , _WindMaxRotation );
				float2 temp_output_1_0_g11668 = temp_output_3_0_g11667;
				float temp_output_39_0_g11668 = ( temp_output_1_0_g11668.y + _WindFlip );
				float3 appendResult43_g11668 = (float3(0.5 , -_WindFlip , 0.0));
				float2 appendResult27_g11668 = (float2(0.0 , ( _WindSquishFactor * min( ( ( _WindSquishWindFactor * abs( lerpResult86_g11668 ) ) + abs( _WindRotation ) ) , _WindMaxRotation ) * temp_output_39_0_g11668 )));
				float3 rotatedValue19_g11668 = RotateAroundAxis( appendResult43_g11668, float3( ( appendResult27_g11668 + temp_output_1_0_g11668 ) ,  0.0 ), float3( 0,0,1 ), ( clampResult29_g11668 * temp_output_39_0_g11668 ) );
				float2 staticSwitch4_g11667 = (rotatedValue19_g11668).xy;
				#else
				float2 staticSwitch4_g11667 = temp_output_3_0_g11667;
				#endif
				float2 texCoord435 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _PIXELPERFECTSPACE_ON
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (IN.ase_texcoord6.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (IN.ase_texcoord6.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (ase_worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float2 temp_output_195_0_g11669 = shaderPosition235;
				float linValue16_g11670 = tex2D( _UberNoiseTexture, ( temp_output_195_0_g11669 * _FullDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11670 = MyCustomExpression16_g11670( linValue16_g11670 );
				float linValue16_g11671 = tex2D( _UberNoiseTexture, ( ( temp_output_195_0_g11669 + float2( 0.321,0.321 ) ) * _FullDistortionNoiseScale ) ).r;
				#ifdef _ENABLEFULLDISTORTION_ON
				float localMyCustomExpression16_g11671 = MyCustomExpression16_g11671( linValue16_g11671 );
				float2 appendResult189_g11669 = (float2(( localMyCustomExpression16_g11670 - 0.5 ) , ( localMyCustomExpression16_g11671 - 0.5 )));
				float2 staticSwitch83 = ( staticSwitch4_g11667 + ( ( 1.0 - _FullDistortionFade ) * appendResult189_g11669 * _FullDistortionDistortion ) );
				#else
				float2 staticSwitch83 = staticSwitch4_g11667;
				#endif
				float2 temp_output_182_0_g11672 = shaderPosition235;
				float linValue16_g11674 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionDistortionScale ) ).r;
				float localMyCustomExpression16_g11674 = MyCustomExpression16_g11674( linValue16_g11674 );
				float3 rotatedValue168_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( _DirectionalDistortionDistortion ,  0.0 ), float3( 0,0,1 ), ( ( ( localMyCustomExpression16_g11674 - 0.5 ) * 2.0 * _DirectionalDistortionRandomDirection ) * UNITY_PI ) );
				float3 rotatedValue136_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_182_0_g11672 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalDistortionRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11672 = rotatedValue136_g11672;
				float linValue16_g11673 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11673 = MyCustomExpression16_g11673( linValue16_g11673 );
				float clampResult154_g11672 = clamp( ( ( break130_g11672.x + break130_g11672.y + _DirectionalDistortionFade + ( localMyCustomExpression16_g11673 * _DirectionalDistortionNoiseFactor ) ) / max( _DirectionalDistortionWidth , 0.001 ) ) , 0.0 , 1.0 );
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float2 staticSwitch82 = ( staticSwitch83 + ( (rotatedValue168_g11672).xy * ( 1.0 - (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 )) ) ) );
				#else
				float2 staticSwitch82 = staticSwitch83;
				#endif
				float temp_output_8_0_g11677 = ( ( ( shaderTime237 * _HologramDistortionSpeed ) + ase_worldPos.y ) / unity_OrthoParams.y );
				float2 temp_cast_4 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_5 = (_HologramDistortionDensity).xx;
				float linValue16_g11679 = tex2D( _UberNoiseTexture, ( temp_cast_4 * temp_cast_5 ) ).r;
				float localMyCustomExpression16_g11679 = MyCustomExpression16_g11679( linValue16_g11679 );
				float clampResult75_g11677 = clamp( localMyCustomExpression16_g11679 , 0.075 , 0.6 );
				float2 temp_cast_6 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_7 = (_HologramDistortionScale).xx;
				float linValue16_g11680 = tex2D( _UberNoiseTexture, ( temp_cast_6 * temp_cast_7 ) ).r;
				float localMyCustomExpression16_g11680 = MyCustomExpression16_g11680( linValue16_g11680 );
				float2 appendResult2_g11678 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float hologramFade182 = _HologramFade;
				#ifdef _ENABLEHOLOGRAM_ON
				float2 appendResult44_g11677 = (float2(( ( ( clampResult75_g11677 * ( localMyCustomExpression16_g11680 - 0.5 ) ) * _HologramDistortionOffset * ( 100.0 / appendResult2_g11678 ).x ) * hologramFade182 ) , 0.0));
				float2 staticSwitch59 = ( staticSwitch82 + appendResult44_g11677 );
				#else
				float2 staticSwitch59 = staticSwitch82;
				#endif
				float2 temp_output_18_0_g11675 = shaderPosition235;
				float2 glitchPosition154 = temp_output_18_0_g11675;
				float linValue16_g11721 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchDistortionSpeed * shaderTime237 ) ) * _GlitchDistortionScale ) ).r;
				float localMyCustomExpression16_g11721 = MyCustomExpression16_g11721( linValue16_g11721 );
				float linValue16_g11676 = tex2D( _UberNoiseTexture, ( ( temp_output_18_0_g11675 + ( _GlitchMaskSpeed * shaderTime237 ) ) * _GlitchMaskScale ) ).r;
				float localMyCustomExpression16_g11676 = MyCustomExpression16_g11676( linValue16_g11676 );
				float glitchFade152 = ( max( localMyCustomExpression16_g11676 , _GlitchMaskMin ) * _GlitchFade );
				#ifdef _ENABLEGLITCH_ON
				float2 staticSwitch62 = ( staticSwitch59 + ( ( localMyCustomExpression16_g11721 - 0.5 ) * _GlitchDistortion * glitchFade152 ) );
				#else
				float2 staticSwitch62 = staticSwitch59;
				#endif
				float2 temp_output_1_0_g11722 = staticSwitch62;
				float2 temp_output_26_0_g11722 = shaderPosition235;
				float temp_output_25_0_g11722 = shaderTime237;
				float linValue16_g11732 = tex2D( _UberNoiseTexture, ( ( temp_output_26_0_g11722 + ( _UVDistortSpeed * temp_output_25_0_g11722 ) ) * _UVDistortNoiseScale ) ).r;
				float localMyCustomExpression16_g11732 = MyCustomExpression16_g11732( linValue16_g11732 );
				float2 lerpResult21_g11729 = lerp( _UVDistortFrom , _UVDistortTo , localMyCustomExpression16_g11732);
				float2 appendResult2_g11731 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _UVDISTORTMASKTOGGLE_ON
				float2 uv_UVDistortMask = IN.ase_texcoord3.xy * _UVDistortMask_ST.xy + _UVDistortMask_ST.zw;
				float4 tex2DNode3_g11730 = tex2D( _UVDistortMask, uv_UVDistortMask );
				float staticSwitch29_g11729 = ( _UVDistortFade * ( tex2DNode3_g11730.r * tex2DNode3_g11730.a ) );
				#else
				float staticSwitch29_g11729 = _UVDistortFade;
				#endif
				#ifdef _ENABLEUVDISTORT_ON
				float2 staticSwitch5_g11722 = ( temp_output_1_0_g11722 + ( lerpResult21_g11729 * ( 100.0 / appendResult2_g11731 ) * staticSwitch29_g11729 ) );
				#else
				float2 staticSwitch5_g11722 = temp_output_1_0_g11722;
				#endif
				#ifdef _ENABLESQUEEZE_ON
				float2 temp_output_1_0_g11728 = staticSwitch5_g11722;
				float2 staticSwitch7_g11722 = ( temp_output_1_0_g11728 + ( ( temp_output_1_0_g11728 - _SqueezeCenter ) * pow( distance( temp_output_1_0_g11728 , _SqueezeCenter ) , _SqueezePower ) * _SqueezeScale * _SqueezeFade ) );
				#else
				float2 staticSwitch7_g11722 = staticSwitch5_g11722;
				#endif
				#ifdef _ENABLESINEROTATE_ON
				float3 rotatedValue36_g11727 = RotateAroundAxis( float3( _SineRotatePivot ,  0.0 ), float3( staticSwitch7_g11722 ,  0.0 ), float3( 0,0,1 ), ( sin( ( temp_output_25_0_g11722 * _SineRotateFrequency ) ) * ( ( _SineRotateAngle / 360.0 ) * UNITY_PI ) * _SineRotateFade ) );
				float2 staticSwitch9_g11722 = (rotatedValue36_g11727).xy;
				#else
				float2 staticSwitch9_g11722 = staticSwitch7_g11722;
				#endif
				#ifdef _ENABLEUVROTATE_ON
				float3 rotatedValue8_g11726 = RotateAroundAxis( float3( _UVRotatePivot ,  0.0 ), float3( staticSwitch9_g11722 ,  0.0 ), float3( 0,0,1 ), ( temp_output_25_0_g11722 * _UVRotateSpeed * UNITY_PI ) );
				float2 staticSwitch16_g11722 = (rotatedValue8_g11726).xy;
				#else
				float2 staticSwitch16_g11722 = staticSwitch9_g11722;
				#endif
				#ifdef _ENABLEUVSCROLL_ON
				float2 staticSwitch14_g11722 = ( ( _UVScrollSpeed * temp_output_25_0_g11722 ) + staticSwitch16_g11722 );
				#else
				float2 staticSwitch14_g11722 = staticSwitch16_g11722;
				#endif
				#ifdef _ENABLEPIXELATE_ON
				float2 appendResult35_g11724 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 MultFactor30_g11724 = ( ( _PixelatePixelDensity * ( appendResult35_g11724 / _PixelatePixelsPerUnit ) ) * ( 1.0 / max( _PixelateFade , 1E-05 ) ) );
				float2 clampResult46_g11724 = clamp( ( floor( ( MultFactor30_g11724 * ( staticSwitch14_g11722 + ( float2( 0.5,0.5 ) / MultFactor30_g11724 ) ) ) ) / MultFactor30_g11724 ) , float2( 0,0 ) , float2( 1,1 ) );
				float2 staticSwitch4_g11722 = clampResult46_g11724;
				#else
				float2 staticSwitch4_g11722 = staticSwitch14_g11722;
				#endif
				#ifdef _ENABLEUVSCALE_ON
				float2 staticSwitch24_g11722 = ( ( ( staticSwitch4_g11722 - _UVScalePivot ) / _UVScaleScale ) + _UVScalePivot );
				#else
				float2 staticSwitch24_g11722 = staticSwitch4_g11722;
				#endif
				float2 temp_output_1_0_g11733 = staticSwitch24_g11722;
				float temp_output_7_0_g11733 = ( sin( ( _WiggleFrequency * ( temp_output_26_0_g11722.y + ( _WiggleSpeed * temp_output_25_0_g11722 ) ) ) ) * _WiggleOffset * _WiggleFade );
				#ifdef _WIGGLEFIXEDGROUNDTOGGLE_ON
				float staticSwitch18_g11733 = ( temp_output_7_0_g11733 * temp_output_1_0_g11733.y );
				#else
				float staticSwitch18_g11733 = temp_output_7_0_g11733;
				#endif
				#ifdef _ENABLEWIGGLE_ON
				float2 appendResult12_g11733 = (float2(staticSwitch18_g11733 , 0.0));
				float2 staticSwitch13_g11733 = ( temp_output_1_0_g11733 + appendResult12_g11733 );
				#else
				float2 staticSwitch13_g11733 = temp_output_1_0_g11733;
				#endif
				float2 temp_output_484_0 = staticSwitch13_g11733;
				float2 texCoord131 = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_FadingMask = IN.ase_texcoord3.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2D( _FadingMask, uv_FadingMask );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float linValue16_g11717 = tex2D( _UberNoiseTexture, ( shaderPosition235 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2D( _UberNoiseTexture, ( temp_output_27_0_g11718 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult130 = lerp( texCoord131 , temp_output_484_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch145 = temp_output_484_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch145 = lerpResult130;
				#else
				float2 staticSwitch145 = temp_output_484_0;
				#endif
				#ifdef _TILINGFIX_ON
				float2 staticSwitch485 = ( ( ( staticSwitch145 % float2( 1,1 ) ) + float2( 1,1 ) ) % float2( 1,1 ) );
				#else
				float2 staticSwitch485 = staticSwitch145;
				#endif
				#ifdef _SPRITESHEETFIX_ON
				float2 break14_g11734 = staticSwitch485;
				float2 break11_g11734 = float2( 0,0 );
				float2 break10_g11734 = float2( 1,1 );
				float2 break9_g11734 = spriteRectMin376;
				float2 break8_g11734 = spriteRectMax377;
				float2 appendResult15_g11734 = (float2((break9_g11734.x + (break14_g11734.x - break11_g11734.x) * (break8_g11734.x - break9_g11734.x) / (break10_g11734.x - break11_g11734.x)) , (break9_g11734.y + (break14_g11734.y - break11_g11734.y) * (break8_g11734.y - break9_g11734.y) / (break10_g11734.y - break11_g11734.y))));
				float2 staticSwitch371 = min( max( appendResult15_g11734 , spriteRectMin376 ) , spriteRectMax377 );
				#else
				float2 staticSwitch371 = staticSwitch485;
				#endif
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11735 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch427 = ( originalUV460 + ( floor( ( ( staticSwitch371 - uvAfterPixelArt450 ) * appendResult7_g11735 ) ) / appendResult7_g11735 ) );
				#else
				float2 staticSwitch427 = staticSwitch371;
				#endif
				float2 finalUV146 = staticSwitch427;
				float2 temp_output_1_0_g11736 = finalUV146;
				#ifdef _ENABLESMOOTHPIXELART_ON
				sampler2D tex3_g11737 = _MainTex;
				float4 textureTexelSize3_g11737 = _MainTex_TexelSize;
				float2 uvs3_g11737 = temp_output_1_0_g11736;
				float4 localtexturePointSmooth3_g11737 = texturePointSmooth( tex3_g11737 , textureTexelSize3_g11737 , uvs3_g11737 );
				float4 staticSwitch8_g11736 = localtexturePointSmooth3_g11737;
				#else
				float4 staticSwitch8_g11736 = tex2D( _MainTex, temp_output_1_0_g11736 );
				#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
				float temp_output_10_0_g11738 = ( _GaussianBlurOffset * _GaussianBlurFade * 0.005 );
				float temp_output_2_0_g11748 = temp_output_10_0_g11738;
				float2 appendResult16_g11748 = (float2(temp_output_2_0_g11748 , 0.0));
				float2 appendResult25_g11750 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11750 = ( appendResult16_g11748 * appendResult25_g11750 );
				float2 temp_output_7_0_g11738 = temp_output_1_0_g11736;
				float2 temp_output_1_0_g11748 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,1 ) ) );
				float2 temp_output_1_0_g11750 = temp_output_1_0_g11748;
				float2 appendResult17_g11748 = (float2(0.0 , temp_output_2_0_g11748));
				float2 appendResult25_g11749 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11749 = ( appendResult17_g11748 * appendResult25_g11749 );
				float2 temp_output_1_0_g11749 = temp_output_1_0_g11748;
				float temp_output_2_0_g11739 = temp_output_10_0_g11738;
				float2 appendResult16_g11739 = (float2(temp_output_2_0_g11739 , 0.0));
				float2 appendResult25_g11741 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11741 = ( appendResult16_g11739 * appendResult25_g11741 );
				float2 temp_output_1_0_g11739 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,1 ) ) );
				float2 temp_output_1_0_g11741 = temp_output_1_0_g11739;
				float2 appendResult17_g11739 = (float2(0.0 , temp_output_2_0_g11739));
				float2 appendResult25_g11740 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11740 = ( appendResult17_g11739 * appendResult25_g11740 );
				float2 temp_output_1_0_g11740 = temp_output_1_0_g11739;
				float temp_output_2_0_g11745 = temp_output_10_0_g11738;
				float2 appendResult16_g11745 = (float2(temp_output_2_0_g11745 , 0.0));
				float2 appendResult25_g11747 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11747 = ( appendResult16_g11745 * appendResult25_g11747 );
				float2 temp_output_1_0_g11745 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,-1 ) ) );
				float2 temp_output_1_0_g11747 = temp_output_1_0_g11745;
				float2 appendResult17_g11745 = (float2(0.0 , temp_output_2_0_g11745));
				float2 appendResult25_g11746 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11746 = ( appendResult17_g11745 * appendResult25_g11746 );
				float2 temp_output_1_0_g11746 = temp_output_1_0_g11745;
				float temp_output_2_0_g11742 = temp_output_10_0_g11738;
				float2 appendResult16_g11742 = (float2(temp_output_2_0_g11742 , 0.0));
				float2 appendResult25_g11744 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11744 = ( appendResult16_g11742 * appendResult25_g11744 );
				float2 temp_output_1_0_g11742 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,-1 ) ) );
				float2 temp_output_1_0_g11744 = temp_output_1_0_g11742;
				float2 appendResult17_g11742 = (float2(0.0 , temp_output_2_0_g11742));
				float2 appendResult25_g11743 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11743 = ( appendResult17_g11742 * appendResult25_g11743 );
				float2 temp_output_1_0_g11743 = temp_output_1_0_g11742;
				float4 staticSwitch3_g11736 = ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) ) ) ) * 0.0625 );
				#else
				float4 staticSwitch3_g11736 = staticSwitch8_g11736;
				#endif
				#ifdef _ENABLESHARPEN_ON
				float2 temp_output_1_0_g11751 = temp_output_1_0_g11736;
				float4 tex2DNode4_g11751 = tex2D( _MainTex, temp_output_1_0_g11751 );
				float temp_output_2_0_g11752 = _SharpenOffset;
				float2 appendResult16_g11752 = (float2(temp_output_2_0_g11752 , 0.0));
				float2 appendResult25_g11754 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11754 = ( appendResult16_g11752 * appendResult25_g11754 );
				float2 temp_output_1_0_g11752 = temp_output_1_0_g11751;
				float2 temp_output_1_0_g11754 = temp_output_1_0_g11752;
				float2 appendResult17_g11752 = (float2(0.0 , temp_output_2_0_g11752));
				float2 appendResult25_g11753 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11753 = ( appendResult17_g11752 * appendResult25_g11753 );
				float2 temp_output_1_0_g11753 = temp_output_1_0_g11752;
				float4 break22_g11751 = ( tex2DNode4_g11751 - ( ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) ) ) / 4.0 ) - tex2DNode4_g11751 ) * ( _SharpenFactor * _SharpenFade ) ) );
				float clampResult23_g11751 = clamp( break22_g11751.a , 0.0 , 1.0 );
				float4 appendResult24_g11751 = (float4(break22_g11751.r , break22_g11751.g , break22_g11751.b , clampResult23_g11751));
				float4 staticSwitch12_g11736 = appendResult24_g11751;
				#else
				float4 staticSwitch12_g11736 = staticSwitch3_g11736;
				#endif
				float4 temp_output_471_0 = staticSwitch12_g11736;
				#ifdef _VERTEXTINTFIRST_ON
				float4 temp_output_1_0_g11755 = temp_output_471_0;
				float4 appendResult8_g11755 = (float4(( (temp_output_1_0_g11755).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11755.a));
				float4 staticSwitch354 = appendResult8_g11755;
				#else
				float4 staticSwitch354 = temp_output_471_0;
				#endif
				float4 originalColor191 = staticSwitch354;
				float4 temp_output_1_0_g11756 = originalColor191;
				float4 temp_output_1_0_g11757 = temp_output_1_0_g11756;
				float2 temp_output_7_0_g11756 = finalUV146;
				float2 temp_output_43_0_g11757 = temp_output_7_0_g11756;
				float2 temp_cast_15 = (_SmokeNoiseScale).xx;
				float linValue16_g11758 = tex2D( _UberNoiseTexture, ( ( ( IN.ase_color.r * (( _SmokeVertexSeed )?( 5.0 ):( 0.0 )) ) + temp_output_43_0_g11757 ) * temp_cast_15 ) ).r;
				float localMyCustomExpression16_g11758 = MyCustomExpression16_g11758( linValue16_g11758 );
				float clampResult28_g11757 = clamp( ( ( ( localMyCustomExpression16_g11758 - 1.0 ) * _SmokeNoiseFactor ) + ( ( ( IN.ase_color.a / 2.5 ) - distance( temp_output_43_0_g11757 , float2( 0.5,0.5 ) ) ) * 2.5 * _SmokeSmoothness ) ) , 0.0 , 1.0 );
				#ifdef _ENABLESMOKE_ON
				float3 lerpResult34_g11757 = lerp( (temp_output_1_0_g11757).rgb , float3( 0,0,0 ) , ( ( 1.0 - clampResult28_g11757 ) * _SmokeDarkEdge ));
				float4 appendResult31_g11757 = (float4(lerpResult34_g11757 , ( clampResult28_g11757 * _SmokeAlpha * temp_output_1_0_g11757.a )));
				float4 staticSwitch2_g11756 = appendResult31_g11757;
				#else
				float4 staticSwitch2_g11756 = temp_output_1_0_g11756;
				#endif
				#ifdef _ENABLECUSTOMFADE_ON
				float4 temp_output_1_0_g11759 = staticSwitch2_g11756;
				float2 temp_output_57_0_g11759 = temp_output_7_0_g11756;
				float4 tex2DNode3_g11759 = tex2D( _CustomFadeFadeMask, temp_output_57_0_g11759 );
				float linValue16_g11760 = tex2D( _UberNoiseTexture, ( temp_output_57_0_g11759 * _CustomFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11760 = MyCustomExpression16_g11760( linValue16_g11760 );
				float clampResult37_g11759 = clamp( ( ( ( IN.ase_color.a * 2.0 ) - 1.0 ) + ( tex2DNode3_g11759.r + ( localMyCustomExpression16_g11760 * _CustomFadeNoiseFactor ) ) ) , 0.0 , 1.0 );
				float4 appendResult13_g11759 = (float4((temp_output_1_0_g11759).rgb , ( temp_output_1_0_g11759.a * pow( clampResult37_g11759 , ( _CustomFadeSmoothness / max( tex2DNode3_g11759.r , 0.05 ) ) ) * _CustomFadeAlpha )));
				float4 staticSwitch3_g11756 = appendResult13_g11759;
				#else
				float4 staticSwitch3_g11756 = staticSwitch2_g11756;
				#endif
				float4 temp_output_1_0_g11761 = staticSwitch3_g11756;
				#ifdef _ENABLECHECKERBOARD_ON
				float4 temp_output_1_0_g11762 = temp_output_1_0_g11761;
				float2 appendResult4_g11762 = (float2(ase_worldPos.x , ase_worldPos.y));
				float2 temp_output_44_0_g11762 = ( appendResult4_g11762 * _CheckerboardTiling * 0.5 );
				float2 break12_g11762 = step( ( ceil( temp_output_44_0_g11762 ) - temp_output_44_0_g11762 ) , float2( 0.5,0.5 ) );
				float4 appendResult42_g11762 = (float4(( (temp_output_1_0_g11762).rgb * min( ( _CheckerboardDarken + abs( ( -break12_g11762.x + break12_g11762.y ) ) ) , 1.0 ) ) , temp_output_1_0_g11762.a));
				float4 staticSwitch2_g11761 = appendResult42_g11762;
				#else
				float4 staticSwitch2_g11761 = temp_output_1_0_g11761;
				#endif
				float2 temp_output_75_0_g11763 = finalUV146;
				float linValue16_g11764 = tex2D( _UberNoiseTexture, ( ( ( shaderTime237 * _FlameSpeed ) + temp_output_75_0_g11763 ) * _FlameNoiseScale ) ).r;
				float localMyCustomExpression16_g11764 = MyCustomExpression16_g11764( linValue16_g11764 );
				float saferPower57_g11763 = abs( max( ( temp_output_75_0_g11763.y - 0.2 ) , 0.0 ) );
				float temp_output_47_0_g11763 = max( _FlameRadius , 0.01 );
				float clampResult70_g11763 = clamp( ( ( ( localMyCustomExpression16_g11764 * pow( saferPower57_g11763 , _FlameNoiseHeightFactor ) * _FlameNoiseFactor ) + ( ( temp_output_47_0_g11763 - distance( temp_output_75_0_g11763 , float2( 0.5,0.4 ) ) ) / temp_output_47_0_g11763 ) ) * _FlameSmooth ) , 0.0 , 1.0 );
				#ifdef _ENABLEFLAME_ON
				float temp_output_63_0_g11763 = ( clampResult70_g11763 * _FlameBrightness );
				float4 appendResult31_g11763 = (float4(temp_output_63_0_g11763 , temp_output_63_0_g11763 , temp_output_63_0_g11763 , clampResult70_g11763));
				float4 staticSwitch6_g11761 = ( appendResult31_g11763 * staticSwitch2_g11761 );
				#else
				float4 staticSwitch6_g11761 = staticSwitch2_g11761;
				#endif
				float4 temp_output_3_0_g11765 = staticSwitch6_g11761;
				float4 temp_output_1_0_g11792 = temp_output_3_0_g11765;
				float2 temp_output_1_0_g11765 = finalUV146;
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
				float4 staticSwitch81_g11792 = tex2D( _RecolorRGBTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch81_g11792 = temp_output_1_0_g11792;
				#endif
				#ifdef _ENABLERECOLORRGB_ON
				float4 break82_g11792 = staticSwitch81_g11792;
				float temp_output_63_0_g11792 = ( break82_g11792.r + break82_g11792.g + break82_g11792.b );
				float4 break71_g11792 = ( ( _RecolorRGBRedTint * ( break82_g11792.r / temp_output_63_0_g11792 ) ) + ( _RecolorRGBGreenTint * ( break82_g11792.g / temp_output_63_0_g11792 ) ) + ( ( break82_g11792.b / temp_output_63_0_g11792 ) * _RecolorRGBBlueTint ) );
				float3 appendResult56_g11792 = (float3(break71_g11792.r , break71_g11792.g , break71_g11792.b));
				float4 break2_g11793 = temp_output_1_0_g11792;
				float saferPower57_g11792 = abs( ( ( break2_g11793.x + break2_g11793.x + break2_g11793.y + break2_g11793.y + break2_g11793.y + break2_g11793.z ) / 6.0 ) );
				float3 lerpResult26_g11792 = lerp( (temp_output_1_0_g11792).rgb , ( appendResult56_g11792 * pow( saferPower57_g11792 , ( max( break71_g11792.a , 0.01 ) * 2.0 ) ) ) , ( min( ( temp_output_63_0_g11792 * 2.0 ) , 1.0 ) * _RecolorRGBFade ));
				float4 appendResult30_g11792 = (float4(lerpResult26_g11792 , temp_output_1_0_g11792.a));
				float4 staticSwitch43_g11765 = appendResult30_g11792;
				#else
				float4 staticSwitch43_g11765 = temp_output_3_0_g11765;
				#endif
				float4 temp_output_1_0_g11790 = staticSwitch43_g11765;
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
				float4 staticSwitch62_g11790 = tex2D( _RecolorRGBYCPTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch62_g11790 = temp_output_1_0_g11790;
				#endif
				float3 hsvTorgb33_g11790 = RGBToHSV( staticSwitch62_g11790.rgb );
				float temp_output_43_0_g11790 = ( ( hsvTorgb33_g11790.x + 0.08333334 ) % 1.0 );
				float4 ifLocalVar46_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.8333333 )
				ifLocalVar46_g11790 = _RecolorRGBYCPPurpleTint;
				else
				ifLocalVar46_g11790 = _RecolorRGBYCPBlueTint;
				float4 ifLocalVar44_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.6666667 )
				ifLocalVar44_g11790 = _RecolorRGBYCPCyanTint;
				else
				ifLocalVar44_g11790 = ifLocalVar46_g11790;
				float4 ifLocalVar47_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.3333333 )
				ifLocalVar47_g11790 = _RecolorRGBYCPYellowTint;
				else
				ifLocalVar47_g11790 = _RecolorRGBYCPGreenTint;
				float4 ifLocalVar45_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.1666667 )
				ifLocalVar45_g11790 = _RecolorRGBYCPRedTint;
				else
				ifLocalVar45_g11790 = ifLocalVar47_g11790;
				float4 ifLocalVar35_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.5 )
				ifLocalVar35_g11790 = ifLocalVar44_g11790;
				else
				ifLocalVar35_g11790 = ifLocalVar45_g11790;
				#ifdef _ENABLERECOLORRGBYCP_ON
				float4 break55_g11790 = ifLocalVar35_g11790;
				float3 appendResult56_g11790 = (float3(break55_g11790.r , break55_g11790.g , break55_g11790.b));
				float4 break2_g11791 = temp_output_1_0_g11790;
				float saferPower57_g11790 = abs( ( ( break2_g11791.x + break2_g11791.x + break2_g11791.y + break2_g11791.y + break2_g11791.y + break2_g11791.z ) / 6.0 ) );
				float3 lerpResult26_g11790 = lerp( (temp_output_1_0_g11790).rgb , ( appendResult56_g11790 * pow( saferPower57_g11790 , max( ( break55_g11790.a * 2.0 ) , 0.01 ) ) ) , ( hsvTorgb33_g11790.z * _RecolorRGBYCPFade ));
				float4 appendResult30_g11790 = (float4(lerpResult26_g11790 , temp_output_1_0_g11790.a));
				float4 staticSwitch9_g11765 = appendResult30_g11790;
				#else
				float4 staticSwitch9_g11765 = staticSwitch43_g11765;
				#endif
				#ifdef _ENABLECOLORREPLACE_ON
				float4 temp_output_1_0_g11768 = staticSwitch9_g11765;
				float3 temp_output_2_0_g11768 = (temp_output_1_0_g11768).rgb;
				float3 In115_g11768 = temp_output_2_0_g11768;
				float3 From115_g11768 = (_ColorReplaceFromColor).rgb;
				float4 break2_g11769 = temp_output_1_0_g11768;
				float3 To115_g11768 = ( pow( ( ( break2_g11769.x + break2_g11769.x + break2_g11769.y + break2_g11769.y + break2_g11769.y + break2_g11769.z ) / 6.0 ) , max( _ColorReplaceContrast , 0.0001 ) ) * (_ColorReplaceToColor).rgb );
				float Fuzziness115_g11768 = _ColorReplaceSmoothness;
				float Range115_g11768 = _ColorReplaceRange;
				float3 localMyCustomExpression115_g11768 = MyCustomExpression115_g11768( In115_g11768 , From115_g11768 , To115_g11768 , Fuzziness115_g11768 , Range115_g11768 );
				float3 lerpResult112_g11768 = lerp( temp_output_2_0_g11768 , localMyCustomExpression115_g11768 , _ColorReplaceFade);
				float4 appendResult4_g11768 = (float4(lerpResult112_g11768 , temp_output_1_0_g11768.a));
				float4 staticSwitch29_g11765 = appendResult4_g11768;
				#else
				float4 staticSwitch29_g11765 = staticSwitch9_g11765;
				#endif
				float4 temp_output_1_0_g11779 = staticSwitch29_g11765;
				#ifdef _ENABLENEGATIVE_ON
				float3 temp_output_9_0_g11779 = (temp_output_1_0_g11779).rgb;
				float3 lerpResult3_g11779 = lerp( temp_output_9_0_g11779 , ( 1.0 - temp_output_9_0_g11779 ) , _NegativeFade);
				float4 appendResult8_g11779 = (float4(lerpResult3_g11779 , temp_output_1_0_g11779.a));
				float4 staticSwitch4_g11779 = appendResult8_g11779;
				#else
				float4 staticSwitch4_g11779 = temp_output_1_0_g11779;
				#endif
				float4 temp_output_57_0_g11765 = staticSwitch4_g11779;
				#ifdef _ENABLECONTRAST_ON
				float4 temp_output_1_0_g11800 = temp_output_57_0_g11765;
				float3 saferPower5_g11800 = abs( (temp_output_1_0_g11800).rgb );
				float3 temp_cast_29 = (_Contrast).xxx;
				float4 appendResult4_g11800 = (float4(pow( saferPower5_g11800 , temp_cast_29 ) , temp_output_1_0_g11800.a));
				float4 staticSwitch32_g11765 = appendResult4_g11800;
				#else
				float4 staticSwitch32_g11765 = temp_output_57_0_g11765;
				#endif
				#ifdef _ENABLEBRIGHTNESS_ON
				float4 temp_output_2_0_g11777 = staticSwitch32_g11765;
				float4 appendResult6_g11777 = (float4(( (temp_output_2_0_g11777).rgb * _Brightness ) , temp_output_2_0_g11777.a));
				float4 staticSwitch33_g11765 = appendResult6_g11777;
				#else
				float4 staticSwitch33_g11765 = staticSwitch32_g11765;
				#endif
				#ifdef _ENABLEHUE_ON
				float4 temp_output_2_0_g11778 = staticSwitch33_g11765;
				float3 hsvTorgb1_g11778 = RGBToHSV( temp_output_2_0_g11778.rgb );
				float3 hsvTorgb3_g11778 = HSVToRGB( float3(( hsvTorgb1_g11778.x + _Hue ),hsvTorgb1_g11778.y,hsvTorgb1_g11778.z) );
				float4 appendResult8_g11778 = (float4(hsvTorgb3_g11778 , temp_output_2_0_g11778.a));
				float4 staticSwitch36_g11765 = appendResult8_g11778;
				#else
				float4 staticSwitch36_g11765 = staticSwitch33_g11765;
				#endif
				#ifdef _ENABLESPLITTONING_ON
				float4 temp_output_1_0_g11794 = staticSwitch36_g11765;
				float4 break2_g11795 = temp_output_1_0_g11794;
				float temp_output_3_0_g11794 = ( ( break2_g11795.x + break2_g11795.x + break2_g11795.y + break2_g11795.y + break2_g11795.y + break2_g11795.z ) / 6.0 );
				float clampResult25_g11794 = clamp( ( ( ( ( temp_output_3_0_g11794 + _SplitToningShift ) - 0.5 ) * _SplitToningBalance ) + 0.5 ) , 0.0 , 1.0 );
				float3 lerpResult6_g11794 = lerp( (_SplitToningShadowsColor).rgb , (_SplitToningHighlightsColor).rgb , clampResult25_g11794);
				float temp_output_9_0_g11796 = max( _SplitToningContrast , 0.0 );
				float saferPower7_g11796 = abs( ( temp_output_3_0_g11794 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11796 ) , 0.0 ) ) ) );
				float3 lerpResult11_g11794 = lerp( (temp_output_1_0_g11794).rgb , ( lerpResult6_g11794 * pow( saferPower7_g11796 , temp_output_9_0_g11796 ) ) , _SplitToningFade);
				float4 appendResult18_g11794 = (float4(lerpResult11_g11794 , temp_output_1_0_g11794.a));
				float4 staticSwitch30_g11765 = appendResult18_g11794;
				#else
				float4 staticSwitch30_g11765 = staticSwitch36_g11765;
				#endif
				#ifdef _ENABLEBLACKTINT_ON
				float4 temp_output_1_0_g11775 = staticSwitch30_g11765;
				float3 temp_output_4_0_g11775 = (temp_output_1_0_g11775).rgb;
				float4 break12_g11775 = temp_output_1_0_g11775;
				float3 lerpResult7_g11775 = lerp( temp_output_4_0_g11775 , ( temp_output_4_0_g11775 + (_BlackTintColor).rgb ) , pow( ( 1.0 - min( max( max( break12_g11775.r , break12_g11775.g ) , break12_g11775.b ) , 1.0 ) ) , max( _BlackTintPower , 0.001 ) ));
				float3 lerpResult13_g11775 = lerp( temp_output_4_0_g11775 , lerpResult7_g11775 , _BlackTintFade);
				float4 appendResult11_g11775 = (float4(lerpResult13_g11775 , break12_g11775.a));
				float4 staticSwitch20_g11765 = appendResult11_g11775;
				#else
				float4 staticSwitch20_g11765 = staticSwitch30_g11765;
				#endif
				#ifdef _ENABLEINKSPREAD_ON
				float4 temp_output_1_0_g11786 = staticSwitch20_g11765;
				float4 break2_g11788 = temp_output_1_0_g11786;
				float temp_output_9_0_g11789 = max( _InkSpreadContrast , 0.0 );
				float saferPower7_g11789 = abs( ( ( ( break2_g11788.x + break2_g11788.x + break2_g11788.y + break2_g11788.y + break2_g11788.y + break2_g11788.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11789 ) , 0.0 ) ) ) );
				float2 temp_output_65_0_g11786 = shaderPosition235;
				float linValue16_g11787 = tex2D( _UberNoiseTexture, ( temp_output_65_0_g11786 * _InkSpreadNoiseScale ) ).r;
				float localMyCustomExpression16_g11787 = MyCustomExpression16_g11787( linValue16_g11787 );
				float clampResult53_g11786 = clamp( ( ( ( _InkSpreadDistance - distance( _InkSpreadPosition , temp_output_65_0_g11786 ) ) + ( localMyCustomExpression16_g11787 * _InkSpreadNoiseFactor ) ) / max( _InkSpreadWidth , 0.001 ) ) , 0.0 , 1.0 );
				float3 lerpResult7_g11786 = lerp( (temp_output_1_0_g11786).rgb , ( (_InkSpreadColor).rgb * pow( saferPower7_g11789 , temp_output_9_0_g11789 ) ) , ( _InkSpreadFade * clampResult53_g11786 ));
				float4 appendResult9_g11786 = (float4(lerpResult7_g11786 , (temp_output_1_0_g11786).a));
				float4 staticSwitch17_g11765 = appendResult9_g11786;
				#else
				float4 staticSwitch17_g11765 = staticSwitch20_g11765;
				#endif
				float temp_output_39_0_g11765 = shaderTime237;
				#ifdef _ENABLESHIFTHUE_ON
				float4 temp_output_1_0_g11780 = staticSwitch17_g11765;
				float3 hsvTorgb15_g11780 = RGBToHSV( (temp_output_1_0_g11780).rgb );
				float3 hsvTorgb19_g11780 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _ShiftHueSpeed ) + hsvTorgb15_g11780.x ),hsvTorgb15_g11780.y,hsvTorgb15_g11780.z) );
				float4 appendResult6_g11780 = (float4(hsvTorgb19_g11780 , temp_output_1_0_g11780.a));
				float4 staticSwitch19_g11765 = appendResult6_g11780;
				#else
				float4 staticSwitch19_g11765 = staticSwitch17_g11765;
				#endif
				float3 hsvTorgb19_g11783 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _AddHueSpeed ) % 1.0 ),_AddHueSaturation,_AddHueBrightness) );
				float4 temp_output_1_0_g11783 = staticSwitch19_g11765;
				float4 break2_g11785 = temp_output_1_0_g11783;
				float saferPower27_g11783 = abs( ( ( break2_g11785.x + break2_g11785.x + break2_g11785.y + break2_g11785.y + break2_g11785.y + break2_g11785.z ) / 6.0 ) );
				#ifdef _ADDHUEMASKTOGGLE_ON
				float2 uv_AddHueMask = IN.ase_texcoord3.xy * _AddHueMask_ST.xy + _AddHueMask_ST.zw;
				float4 tex2DNode3_g11784 = tex2D( _AddHueMask, uv_AddHueMask );
				float staticSwitch33_g11783 = ( _AddHueFade * ( tex2DNode3_g11784.r * tex2DNode3_g11784.a ) );
				#else
				float staticSwitch33_g11783 = _AddHueFade;
				#endif
				#ifdef _ENABLEADDHUE_ON
				float4 appendResult6_g11783 = (float4(( ( hsvTorgb19_g11783 * pow( saferPower27_g11783 , max( _AddHueContrast , 0.001 ) ) * staticSwitch33_g11783 ) + (temp_output_1_0_g11783).rgb ) , temp_output_1_0_g11783.a));
				float4 staticSwitch23_g11765 = appendResult6_g11783;
				#else
				float4 staticSwitch23_g11765 = staticSwitch19_g11765;
				#endif
				float4 temp_output_1_0_g11781 = staticSwitch23_g11765;
				float4 break2_g11782 = temp_output_1_0_g11781;
				float3 temp_output_13_0_g11781 = (_SineGlowColor).rgb;
				#ifdef _SINEGLOWMASKTOGGLE_ON
				float2 uv_SineGlowMask = IN.ase_texcoord3.xy * _SineGlowMask_ST.xy + _SineGlowMask_ST.zw;
				float4 tex2DNode30_g11781 = tex2D( _SineGlowMask, uv_SineGlowMask );
				float3 staticSwitch27_g11781 = ( (tex2DNode30_g11781).rgb * temp_output_13_0_g11781 * tex2DNode30_g11781.a );
				#else
				float3 staticSwitch27_g11781 = temp_output_13_0_g11781;
				#endif
				#ifdef _ENABLESINEGLOW_ON
				float4 appendResult21_g11781 = (float4(( (temp_output_1_0_g11781).rgb + ( pow( ( ( break2_g11782.x + break2_g11782.x + break2_g11782.y + break2_g11782.y + break2_g11782.y + break2_g11782.z ) / 6.0 ) , max( _SineGlowContrast , 0.0 ) ) * staticSwitch27_g11781 * _SineGlowFade * ( ( ( sin( ( temp_output_39_0_g11765 * _SineGlowFrequency ) ) + 1.0 ) * ( _SineGlowMax - _SineGlowMin ) ) + _SineGlowMin ) ) ) , temp_output_1_0_g11781.a));
				float4 staticSwitch28_g11765 = appendResult21_g11781;
				#else
				float4 staticSwitch28_g11765 = staticSwitch23_g11765;
				#endif
				#ifdef _ENABLESATURATION_ON
				float4 temp_output_1_0_g11770 = staticSwitch28_g11765;
				float4 break2_g11771 = temp_output_1_0_g11770;
				float3 temp_cast_45 = (( ( break2_g11771.x + break2_g11771.x + break2_g11771.y + break2_g11771.y + break2_g11771.y + break2_g11771.z ) / 6.0 )).xxx;
				float3 lerpResult5_g11770 = lerp( temp_cast_45 , (temp_output_1_0_g11770).rgb , _Saturation);
				float4 appendResult8_g11770 = (float4(lerpResult5_g11770 , temp_output_1_0_g11770.a));
				float4 staticSwitch38_g11765 = appendResult8_g11770;
				#else
				float4 staticSwitch38_g11765 = staticSwitch28_g11765;
				#endif
				float4 temp_output_15_0_g11772 = staticSwitch38_g11765;
				float3 temp_output_82_0_g11772 = (_InnerOutlineColor).rgb;
				float2 temp_output_7_0_g11772 = temp_output_1_0_g11765;
				float temp_output_179_0_g11772 = temp_output_39_0_g11765;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch187_g11772 = ( (tex2D( _InnerOutlineTintTexture, ( temp_output_7_0_g11772 + ( _InnerOutlineTextureSpeed * temp_output_179_0_g11772 ) ) )).rgb * temp_output_82_0_g11772 );
				#else
				float3 staticSwitch187_g11772 = temp_output_82_0_g11772;
				#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11774 = tex2D( _UberNoiseTexture, ( ( ( temp_output_179_0_g11772 * _InnerOutlineNoiseSpeed ) + temp_output_7_0_g11772 ) * _InnerOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11774 = MyCustomExpression16_g11774( linValue16_g11774 );
				float2 staticSwitch169_g11772 = ( ( localMyCustomExpression16_g11774 - 0.5 ) * _InnerOutlineDistortionIntensity );
				#else
				float2 staticSwitch169_g11772 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11772 = ( staticSwitch169_g11772 + temp_output_7_0_g11772 );
				float2 appendResult2_g11773 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11772 = ( 100.0 / appendResult2_g11773 );
				float temp_output_178_0_g11772 = ( _InnerOutlineFade * ( 1.0 - min( min( min( min( min( min( min( tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11772 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) ) );
				float3 lerpResult176_g11772 = lerp( (temp_output_15_0_g11772).rgb , staticSwitch187_g11772 , temp_output_178_0_g11772);
				#ifdef _INNEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch188_g11772 = ( temp_output_178_0_g11772 * temp_output_15_0_g11772.a );
				#else
				float staticSwitch188_g11772 = temp_output_15_0_g11772.a;
				#endif
				#ifdef _ENABLEINNEROUTLINE_ON
				float4 appendResult177_g11772 = (float4(lerpResult176_g11772 , staticSwitch188_g11772));
				float4 staticSwitch12_g11765 = appendResult177_g11772;
				#else
				float4 staticSwitch12_g11765 = staticSwitch38_g11765;
				#endif
				float4 temp_output_15_0_g11797 = staticSwitch12_g11765;
				float3 temp_output_82_0_g11797 = (_OuterOutlineColor).rgb;
				float2 temp_output_7_0_g11797 = temp_output_1_0_g11765;
				float temp_output_186_0_g11797 = temp_output_39_0_g11765;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11797 = ( (tex2D( _OuterOutlineTintTexture, ( temp_output_7_0_g11797 + ( _OuterOutlineTextureSpeed * temp_output_186_0_g11797 ) ) )).rgb * temp_output_82_0_g11797 );
				#else
				float3 staticSwitch199_g11797 = temp_output_82_0_g11797;
				#endif
				float temp_output_182_0_g11797 = ( ( 1.0 - temp_output_15_0_g11797.a ) * min( ( _OuterOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11797 = 1.0;
				#else
				float staticSwitch203_g11797 = temp_output_182_0_g11797;
				#endif
				float3 lerpResult178_g11797 = lerp( (temp_output_15_0_g11797).rgb , staticSwitch199_g11797 , staticSwitch203_g11797);
				float3 lerpResult170_g11797 = lerp( lerpResult178_g11797 , staticSwitch199_g11797 , staticSwitch203_g11797);
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11798 = tex2D( _UberNoiseTexture, ( ( ( temp_output_186_0_g11797 * _OuterOutlineNoiseSpeed ) + temp_output_7_0_g11797 ) * _OuterOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11798 = MyCustomExpression16_g11798( linValue16_g11798 );
				float2 staticSwitch157_g11797 = ( ( localMyCustomExpression16_g11798 - 0.5 ) * _OuterOutlineDistortionIntensity );
				#else
				float2 staticSwitch157_g11797 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11797 = ( staticSwitch157_g11797 + temp_output_7_0_g11797 );
				float2 appendResult2_g11799 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11797 = ( 100.0 / appendResult2_g11799 );
				float lerpResult168_g11797 = lerp( temp_output_15_0_g11797.a , min( ( max( max( max( max( max( max( max( tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11797 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) * 3.0 ) , 1.0 ) , _OuterOutlineFade);
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11797 = ( temp_output_182_0_g11797 * lerpResult168_g11797 );
				#else
				float staticSwitch200_g11797 = lerpResult168_g11797;
				#endif
				#ifdef _ENABLEOUTEROUTLINE_ON
				float4 appendResult174_g11797 = (float4(lerpResult170_g11797 , staticSwitch200_g11797));
				float4 staticSwitch13_g11765 = appendResult174_g11797;
				#else
				float4 staticSwitch13_g11765 = staticSwitch12_g11765;
				#endif
				float4 temp_output_15_0_g11776 = staticSwitch13_g11765;
				float3 temp_output_82_0_g11776 = (_PixelOutlineColor).rgb;
				float2 temp_output_7_0_g11776 = temp_output_1_0_g11765;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11776 = ( (tex2D( _PixelOutlineTintTexture, ( temp_output_7_0_g11776 + ( _PixelOutlineTextureSpeed * temp_output_39_0_g11765 ) ) )).rgb * temp_output_82_0_g11776 );
				#else
				float3 staticSwitch199_g11776 = temp_output_82_0_g11776;
				#endif
				float temp_output_182_0_g11776 = ( ( 1.0 - temp_output_15_0_g11776.a ) * min( ( _PixelOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11776 = 1.0;
				#else
				float staticSwitch203_g11776 = temp_output_182_0_g11776;
				#endif
				float3 lerpResult178_g11776 = lerp( (temp_output_15_0_g11776).rgb , staticSwitch199_g11776 , staticSwitch203_g11776);
				float3 lerpResult170_g11776 = lerp( lerpResult178_g11776 , staticSwitch199_g11776 , staticSwitch203_g11776);
				float2 appendResult206_g11776 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_209_0_g11776 = ( float2( 1,1 ) / appendResult206_g11776 );
				float lerpResult168_g11776 = lerp( temp_output_15_0_g11776.a , min( ( max( max( max( tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,-1 ) ) * temp_output_209_0_g11776 ) ) ).a , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,1 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( -1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) * 3.0 ) , 1.0 ) , _PixelOutlineFade);
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11776 = ( temp_output_182_0_g11776 * lerpResult168_g11776 );
				#else
				float staticSwitch200_g11776 = lerpResult168_g11776;
				#endif
				#ifdef _ENABLEPIXELOUTLINE_ON
				float4 appendResult174_g11776 = (float4(lerpResult170_g11776 , staticSwitch200_g11776));
				float4 staticSwitch48_g11765 = appendResult174_g11776;
				#else
				float4 staticSwitch48_g11765 = staticSwitch13_g11765;
				#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
				float3 lerpResult15_g11766 = lerp( (_PingPongGlowFrom).rgb , (_PingPongGlowTo).rgb , ( ( sin( ( temp_output_39_0_g11765 * _PingPongGlowFrequency ) ) + 1.0 ) / 2.0 ));
				float4 temp_output_5_0_g11766 = staticSwitch48_g11765;
				float4 break2_g11767 = temp_output_5_0_g11766;
				float4 appendResult12_g11766 = (float4(( ( lerpResult15_g11766 * _PingPongGlowFade * pow( ( ( break2_g11767.x + break2_g11767.x + break2_g11767.y + break2_g11767.y + break2_g11767.y + break2_g11767.z ) / 6.0 ) , max( _PingPongGlowContrast , 0.0 ) ) ) + (temp_output_5_0_g11766).rgb ) , temp_output_5_0_g11766.a));
				float4 staticSwitch46_g11765 = appendResult12_g11766;
				#else
				float4 staticSwitch46_g11765 = staticSwitch48_g11765;
				#endif
				float4 temp_output_361_0 = staticSwitch46_g11765;
				#ifdef _ENABLEHOLOGRAM_ON
				float4 temp_output_1_0_g11801 = temp_output_361_0;
				float4 break2_g11802 = temp_output_1_0_g11801;
				float temp_output_9_0_g11803 = max( _HologramContrast , 0.0 );
				float saferPower7_g11803 = abs( ( ( ( break2_g11802.x + break2_g11802.x + break2_g11802.y + break2_g11802.y + break2_g11802.y + break2_g11802.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11803 ) , 0.0 ) ) ) );
				float4 appendResult22_g11801 = (float4(( (_HologramTint).rgb * pow( saferPower7_g11803 , temp_output_9_0_g11803 ) ) , ( max( pow( abs( sin( ( ( ( ( shaderTime237 * _HologramLineSpeed ) + ase_worldPos.y ) / unity_OrthoParams.y ) * _HologramLineFrequency ) ) ) , _HologramLineGap ) , _HologramMinAlpha ) * temp_output_1_0_g11801.a )));
				float4 lerpResult37_g11801 = lerp( temp_output_1_0_g11801 , appendResult22_g11801 , hologramFade182);
				float4 staticSwitch56 = lerpResult37_g11801;
				#else
				float4 staticSwitch56 = temp_output_361_0;
				#endif
				#ifdef _ENABLEGLITCH_ON
				float4 temp_output_1_0_g11804 = staticSwitch56;
				float4 break2_g11806 = temp_output_1_0_g11804;
				float temp_output_34_0_g11804 = shaderTime237;
				float linValue16_g11805 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchNoiseSpeed * temp_output_34_0_g11804 ) ) * _GlitchNoiseScale ) ).r;
				float localMyCustomExpression16_g11805 = MyCustomExpression16_g11805( linValue16_g11805 );
				float3 hsvTorgb3_g11807 = HSVToRGB( float3(( localMyCustomExpression16_g11805 + ( temp_output_34_0_g11804 * _GlitchHueSpeed ) ),1.0,1.0) );
				float3 lerpResult23_g11804 = lerp( (temp_output_1_0_g11804).rgb , ( ( ( break2_g11806.x + break2_g11806.x + break2_g11806.y + break2_g11806.y + break2_g11806.y + break2_g11806.z ) / 6.0 ) * _GlitchBrightness * hsvTorgb3_g11807 ) , glitchFade152);
				float4 appendResult27_g11804 = (float4(lerpResult23_g11804 , temp_output_1_0_g11804.a));
				float4 staticSwitch57 = appendResult27_g11804;
				#else
				float4 staticSwitch57 = staticSwitch56;
				#endif
				float4 temp_output_3_0_g11808 = staticSwitch57;
				float4 temp_output_1_0_g11833 = temp_output_3_0_g11808;
				float2 temp_output_41_0_g11808 = shaderPosition235;
				float2 temp_output_99_0_g11833 = temp_output_41_0_g11808;
				float temp_output_40_0_g11808 = shaderTime237;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
				float linValue16_g11838 = tex2D( _UberNoiseTexture, ( ( ( temp_output_40_0_g11808 * _CamouflageDistortionSpeed ) + temp_output_99_0_g11833 ) * _CamouflageDistortionScale ) ).r;
				float localMyCustomExpression16_g11838 = MyCustomExpression16_g11838( linValue16_g11838 );
				float2 staticSwitch101_g11833 = ( ( ( localMyCustomExpression16_g11838 - 0.25 ) * _CamouflageDistortionIntensity ) + temp_output_99_0_g11833 );
				#else
				float2 staticSwitch101_g11833 = temp_output_99_0_g11833;
				#endif
				float linValue16_g11835 = tex2D( _UberNoiseTexture, ( staticSwitch101_g11833 * _CamouflageNoiseScaleA ) ).r;
				float localMyCustomExpression16_g11835 = MyCustomExpression16_g11835( linValue16_g11835 );
				float clampResult52_g11833 = clamp( ( ( _CamouflageDensityA - localMyCustomExpression16_g11835 ) / max( _CamouflageSmoothnessA , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult55_g11833 = lerp( _CamouflageBaseColor , ( _CamouflageColorA * clampResult52_g11833 ) , clampResult52_g11833);
				float linValue16_g11837 = tex2D( _UberNoiseTexture, ( ( staticSwitch101_g11833 + float2( 12.3,12.3 ) ) * _CamouflageNoiseScaleB ) ).r;
				#ifdef _ENABLECAMOUFLAGE_ON
				float localMyCustomExpression16_g11837 = MyCustomExpression16_g11837( linValue16_g11837 );
				float clampResult65_g11833 = clamp( ( ( _CamouflageDensityB - localMyCustomExpression16_g11837 ) / max( _CamouflageSmoothnessB , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult68_g11833 = lerp( lerpResult55_g11833 , ( _CamouflageColorB * clampResult65_g11833 ) , clampResult65_g11833);
				float4 break2_g11836 = temp_output_1_0_g11833;
				float temp_output_9_0_g11834 = max( _CamouflageContrast , 0.0 );
				float saferPower7_g11834 = abs( ( ( ( break2_g11836.x + break2_g11836.x + break2_g11836.y + break2_g11836.y + break2_g11836.y + break2_g11836.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11834 ) , 0.0 ) ) ) );
				float3 lerpResult4_g11833 = lerp( (temp_output_1_0_g11833).rgb , ( (lerpResult68_g11833).rgb * pow( saferPower7_g11834 , temp_output_9_0_g11834 ) ) , _CamouflageFade);
				float4 appendResult7_g11833 = (float4(lerpResult4_g11833 , temp_output_1_0_g11833.a));
				float4 staticSwitch26_g11808 = appendResult7_g11833;
				#else
				float4 staticSwitch26_g11808 = temp_output_3_0_g11808;
				#endif
				float4 temp_output_1_0_g11827 = staticSwitch26_g11808;
				float temp_output_59_0_g11827 = temp_output_40_0_g11808;
				float2 temp_output_58_0_g11827 = temp_output_41_0_g11808;
				float linValue16_g11828 = tex2D( _UberNoiseTexture, ( ( ( temp_output_59_0_g11827 * _MetalNoiseDistortionSpeed ) + temp_output_58_0_g11827 ) * _MetalNoiseDistortionScale ) ).r;
				float localMyCustomExpression16_g11828 = MyCustomExpression16_g11828( linValue16_g11828 );
				float linValue16_g11830 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11828 - 0.25 ) * _MetalNoiseDistortion ) + ( ( temp_output_59_0_g11827 * _MetalNoiseSpeed ) + temp_output_58_0_g11827 ) ) * _MetalNoiseScale ) ).r;
				float localMyCustomExpression16_g11830 = MyCustomExpression16_g11830( linValue16_g11830 );
				float4 break2_g11829 = temp_output_1_0_g11827;
				float temp_output_5_0_g11827 = ( ( break2_g11829.x + break2_g11829.x + break2_g11829.y + break2_g11829.y + break2_g11829.y + break2_g11829.z ) / 6.0 );
				float temp_output_9_0_g11831 = max( _MetalHighlightContrast , 0.0 );
				float saferPower7_g11831 = abs( ( temp_output_5_0_g11827 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11831 ) , 0.0 ) ) ) );
				float saferPower2_g11827 = abs( temp_output_5_0_g11827 );
				#ifdef _METALMASKTOGGLE_ON
				float2 uv_MetalMask = IN.ase_texcoord3.xy * _MetalMask_ST.xy + _MetalMask_ST.zw;
				float4 tex2DNode3_g11832 = tex2D( _MetalMask, uv_MetalMask );
				float staticSwitch60_g11827 = ( _MetalFade * ( tex2DNode3_g11832.r * tex2DNode3_g11832.a ) );
				#else
				float staticSwitch60_g11827 = _MetalFade;
				#endif
				#ifdef _ENABLEMETAL_ON
				float4 lerpResult45_g11827 = lerp( temp_output_1_0_g11827 , ( ( max( ( ( _MetalHighlightDensity - localMyCustomExpression16_g11830 ) / max( _MetalHighlightDensity , 0.01 ) ) , 0.0 ) * _MetalHighlightColor * pow( saferPower7_g11831 , temp_output_9_0_g11831 ) ) + ( pow( saferPower2_g11827 , _MetalContrast ) * _MetalColor ) ) , staticSwitch60_g11827);
				float4 appendResult8_g11827 = (float4((lerpResult45_g11827).rgb , (temp_output_1_0_g11827).a));
				float4 staticSwitch28_g11808 = appendResult8_g11827;
				#else
				float4 staticSwitch28_g11808 = staticSwitch26_g11808;
				#endif
				#ifdef _ENABLEFROZEN_ON
				float4 temp_output_1_0_g11821 = staticSwitch28_g11808;
				float4 break2_g11822 = temp_output_1_0_g11821;
				float temp_output_7_0_g11821 = ( ( break2_g11822.x + break2_g11822.x + break2_g11822.y + break2_g11822.y + break2_g11822.y + break2_g11822.z ) / 6.0 );
				float temp_output_9_0_g11824 = max( _FrozenContrast , 0.0 );
				float saferPower7_g11824 = abs( ( temp_output_7_0_g11821 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11824 ) , 0.0 ) ) ) );
				float saferPower20_g11821 = abs( temp_output_7_0_g11821 );
				float2 temp_output_72_0_g11821 = temp_output_41_0_g11808;
				float linValue16_g11823 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11821 * _FrozenSnowScale ) ).r;
				float localMyCustomExpression16_g11823 = MyCustomExpression16_g11823( linValue16_g11823 );
				float temp_output_73_0_g11821 = temp_output_40_0_g11808;
				float linValue16_g11825 = tex2D( _UberNoiseTexture, ( ( ( temp_output_73_0_g11821 * _FrozenHighlightDistortionSpeed ) + temp_output_72_0_g11821 ) * _FrozenHighlightDistortionScale ) ).r;
				float localMyCustomExpression16_g11825 = MyCustomExpression16_g11825( linValue16_g11825 );
				float linValue16_g11826 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11825 - 0.25 ) * _FrozenHighlightDistortion ) + ( ( temp_output_73_0_g11821 * _FrozenHighlightSpeed ) + temp_output_72_0_g11821 ) ) * _FrozenHighlightScale ) ).r;
				float localMyCustomExpression16_g11826 = MyCustomExpression16_g11826( linValue16_g11826 );
				float saferPower42_g11821 = abs( temp_output_7_0_g11821 );
				float3 lerpResult57_g11821 = lerp( (temp_output_1_0_g11821).rgb , ( ( pow( saferPower7_g11824 , temp_output_9_0_g11824 ) * (_FrozenTint).rgb ) + ( pow( saferPower20_g11821 , _FrozenSnowContrast ) * ( (_FrozenSnowColor).rgb * max( ( _FrozenSnowDensity - localMyCustomExpression16_g11823 ) , 0.0 ) ) ) + (( max( ( ( _FrozenHighlightDensity - localMyCustomExpression16_g11826 ) / max( _FrozenHighlightDensity , 0.01 ) ) , 0.0 ) * _FrozenHighlightColor * pow( saferPower42_g11821 , _FrozenHighlightContrast ) )).rgb ) , _FrozenFade);
				float4 appendResult26_g11821 = (float4(lerpResult57_g11821 , temp_output_1_0_g11821.a));
				float4 staticSwitch29_g11808 = appendResult26_g11821;
				#else
				float4 staticSwitch29_g11808 = staticSwitch28_g11808;
				#endif
				float4 temp_output_1_0_g11816 = staticSwitch29_g11808;
				float3 temp_output_28_0_g11816 = (temp_output_1_0_g11816).rgb;
				float4 break2_g11820 = float4( temp_output_28_0_g11816 , 0.0 );
				float saferPower21_g11816 = abs( ( ( break2_g11820.x + break2_g11820.x + break2_g11820.y + break2_g11820.y + break2_g11820.y + break2_g11820.z ) / 6.0 ) );
				float2 temp_output_72_0_g11816 = temp_output_41_0_g11808;
				float linValue16_g11819 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnSwirlNoiseScale ) ).r;
				float localMyCustomExpression16_g11819 = MyCustomExpression16_g11819( linValue16_g11819 );
				float linValue16_g11817 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11819 - 0.5 ) * float2( 1,1 ) * _BurnSwirlFactor ) + temp_output_72_0_g11816 ) * _BurnInsideNoiseScale ) ).r;
				#ifdef _ENABLEBURN_ON
				float localMyCustomExpression16_g11817 = MyCustomExpression16_g11817( linValue16_g11817 );
				float clampResult68_g11816 = clamp( ( _BurnInsideNoiseFactor - localMyCustomExpression16_g11817 ) , 0.0 , 1.0 );
				float linValue16_g11818 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnEdgeNoiseScale ) ).r;
				float localMyCustomExpression16_g11818 = MyCustomExpression16_g11818( linValue16_g11818 );
				float temp_output_15_0_g11816 = ( ( ( _BurnRadius - distance( temp_output_72_0_g11816 , _BurnPosition ) ) + ( localMyCustomExpression16_g11818 * _BurnEdgeNoiseFactor ) ) / max( _BurnWidth , 0.01 ) );
				float clampResult18_g11816 = clamp( temp_output_15_0_g11816 , 0.0 , 1.0 );
				float3 lerpResult29_g11816 = lerp( temp_output_28_0_g11816 , ( pow( saferPower21_g11816 , max( _BurnInsideContrast , 0.001 ) ) * ( ( (_BurnInsideNoiseColor).rgb * clampResult68_g11816 ) + (_BurnInsideColor).rgb ) ) , clampResult18_g11816);
				float3 lerpResult40_g11816 = lerp( temp_output_28_0_g11816 , ( lerpResult29_g11816 + ( ( step( temp_output_15_0_g11816 , 1.0 ) * step( 0.0 , temp_output_15_0_g11816 ) ) * (_BurnEdgeColor).rgb ) ) , _BurnFade);
				float4 appendResult43_g11816 = (float4(lerpResult40_g11816 , temp_output_1_0_g11816.a));
				float4 staticSwitch32_g11808 = appendResult43_g11816;
				#else
				float4 staticSwitch32_g11808 = staticSwitch29_g11808;
				#endif
				#ifdef _ENABLERAINBOW_ON
				float2 temp_output_42_0_g11812 = temp_output_41_0_g11808;
				float linValue16_g11813 = tex2D( _UberNoiseTexture, ( temp_output_42_0_g11812 * _RainbowNoiseScale ) ).r;
				float localMyCustomExpression16_g11813 = MyCustomExpression16_g11813( linValue16_g11813 );
				float3 hsvTorgb3_g11815 = HSVToRGB( float3(( ( ( distance( temp_output_42_0_g11812 , _RainbowCenter ) + ( localMyCustomExpression16_g11813 * _RainbowNoiseFactor ) ) * _RainbowDensity ) + ( _RainbowSpeed * temp_output_40_0_g11808 ) ),1.0,1.0) );
				float3 hsvTorgb36_g11812 = RGBToHSV( hsvTorgb3_g11815 );
				float3 hsvTorgb37_g11812 = HSVToRGB( float3(hsvTorgb36_g11812.x,_RainbowSaturation,( hsvTorgb36_g11812.z * _RainbowBrightness )) );
				float4 temp_output_1_0_g11812 = staticSwitch32_g11808;
				float4 break2_g11814 = temp_output_1_0_g11812;
				float saferPower24_g11812 = abs( ( ( break2_g11814.x + break2_g11814.x + break2_g11814.y + break2_g11814.y + break2_g11814.y + break2_g11814.z ) / 6.0 ) );
				float4 appendResult29_g11812 = (float4(( ( hsvTorgb37_g11812 * pow( saferPower24_g11812 , max( _RainbowContrast , 0.001 ) ) * _RainbowFade ) + (temp_output_1_0_g11812).rgb ) , temp_output_1_0_g11812.a));
				float4 staticSwitch34_g11808 = appendResult29_g11812;
				#else
				float4 staticSwitch34_g11808 = staticSwitch32_g11808;
				#endif
				float4 temp_output_1_0_g11809 = staticSwitch34_g11808;
				float3 temp_output_57_0_g11809 = (temp_output_1_0_g11809).rgb;
				float4 break2_g11810 = temp_output_1_0_g11809;
				float3 temp_cast_68 = (( ( break2_g11810.x + break2_g11810.x + break2_g11810.y + break2_g11810.y + break2_g11810.y + break2_g11810.z ) / 6.0 )).xxx;
				float3 lerpResult92_g11809 = lerp( temp_cast_68 , temp_output_57_0_g11809 , _ShineSaturation);
				float3 saferPower83_g11809 = abs( lerpResult92_g11809 );
				float3 temp_cast_69 = (max( _ShineContrast , 0.001 )).xxx;
				float3 rotatedValue69_g11809 = RotateAroundAxis( float3( 0,0,0 ), float3( ( _ShineFrequency * temp_output_41_0_g11808 ) ,  0.0 ), float3( 0,0,1 ), ( ( _ShineRotation / 180.0 ) * UNITY_PI ) );
				float temp_output_103_0_g11809 = ( _ShineFrequency * _ShineWidth );
				float clampResult80_g11809 = clamp( ( ( sin( ( rotatedValue69_g11809.x - ( temp_output_40_0_g11808 * _ShineSpeed * _ShineFrequency ) ) ) - ( 1.0 - temp_output_103_0_g11809 ) ) / temp_output_103_0_g11809 ) , 0.0 , 1.0 );
				#ifdef _SHINEMASKTOGGLE_ON
				float2 uv_ShineMask = IN.ase_texcoord3.xy * _ShineMask_ST.xy + _ShineMask_ST.zw;
				float4 tex2DNode3_g11811 = tex2D( _ShineMask, uv_ShineMask );
				float staticSwitch98_g11809 = ( _ShineFade * ( tex2DNode3_g11811.r * tex2DNode3_g11811.a ) );
				#else
				float staticSwitch98_g11809 = _ShineFade;
				#endif
				#ifdef _ENABLESHINE_ON
				float4 appendResult8_g11809 = (float4(( temp_output_57_0_g11809 + ( ( pow( saferPower83_g11809 , temp_cast_69 ) * (_ShineColor).rgb ) * clampResult80_g11809 * staticSwitch98_g11809 ) ) , (temp_output_1_0_g11809).a));
				float4 staticSwitch36_g11808 = appendResult8_g11809;
				#else
				float4 staticSwitch36_g11808 = staticSwitch34_g11808;
				#endif
				#ifdef _ENABLEPOISON_ON
				float temp_output_41_0_g11839 = temp_output_40_0_g11808;
				float linValue16_g11841 = tex2D( _UberNoiseTexture, ( ( ( temp_output_41_0_g11839 * _PoisonNoiseSpeed ) + temp_output_41_0_g11808 ) * _PoisonNoiseScale ) ).r;
				float localMyCustomExpression16_g11841 = MyCustomExpression16_g11841( linValue16_g11841 );
				float saferPower19_g11839 = abs( abs( ( ( ( localMyCustomExpression16_g11841 + ( temp_output_41_0_g11839 * _PoisonShiftSpeed ) ) % 1.0 ) + -0.5 ) ) );
				float3 temp_output_24_0_g11839 = (_PoisonColor).rgb;
				float4 temp_output_1_0_g11839 = staticSwitch36_g11808;
				float3 temp_output_28_0_g11839 = (temp_output_1_0_g11839).rgb;
				float4 break2_g11840 = float4( temp_output_28_0_g11839 , 0.0 );
				float3 lerpResult32_g11839 = lerp( temp_output_28_0_g11839 , ( temp_output_24_0_g11839 * ( ( break2_g11840.x + break2_g11840.x + break2_g11840.y + break2_g11840.y + break2_g11840.y + break2_g11840.z ) / 6.0 ) ) , ( _PoisonFade * _PoisonRecolorFactor ));
				float4 appendResult27_g11839 = (float4(( ( max( pow( saferPower19_g11839 , _PoisonDensity ) , 0.0 ) * temp_output_24_0_g11839 * _PoisonFade * _PoisonNoiseBrightness ) + lerpResult32_g11839 ) , temp_output_1_0_g11839.a));
				float4 staticSwitch39_g11808 = appendResult27_g11839;
				#else
				float4 staticSwitch39_g11808 = staticSwitch36_g11808;
				#endif
				float4 temp_output_10_0_g11842 = staticSwitch39_g11808;
				float3 temp_output_12_0_g11842 = (temp_output_10_0_g11842).rgb;
				float2 temp_output_2_0_g11842 = temp_output_41_0_g11808;
				float temp_output_1_0_g11842 = temp_output_40_0_g11808;
				float2 temp_output_6_0_g11842 = ( temp_output_1_0_g11842 * _EnchantedSpeed );
				float linValue16_g11845 = tex2D( _UberNoiseTexture, ( ( ( temp_output_2_0_g11842 - ( ( temp_output_6_0_g11842 + float2( 1.234,5.6789 ) ) * float2( 0.95,1.05 ) ) ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11845 = MyCustomExpression16_g11845( linValue16_g11845 );
				float linValue16_g11843 = tex2D( _UberNoiseTexture, ( ( ( temp_output_6_0_g11842 + temp_output_2_0_g11842 ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11843 = MyCustomExpression16_g11843( linValue16_g11843 );
				float temp_output_36_0_g11842 = ( localMyCustomExpression16_g11845 + localMyCustomExpression16_g11843 );
				float temp_output_43_0_g11842 = ( temp_output_36_0_g11842 * 0.5 );
				float3 lerpResult42_g11842 = lerp( (_EnchantedLowColor).rgb , (_EnchantedHighColor).rgb , temp_output_43_0_g11842);
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
				float3 hsvTorgb53_g11842 = HSVToRGB( float3(( ( temp_output_43_0_g11842 * _EnchantedRainbowDensity ) + ( _EnchantedRainbowSpeed * temp_output_1_0_g11842 ) ),_EnchantedRainbowSaturation,1.0) );
				float3 staticSwitch50_g11842 = hsvTorgb53_g11842;
				#else
				float3 staticSwitch50_g11842 = lerpResult42_g11842;
				#endif
				float4 break2_g11844 = temp_output_10_0_g11842;
				float saferPower24_g11842 = abs( ( ( break2_g11844.x + break2_g11844.x + break2_g11844.y + break2_g11844.y + break2_g11844.y + break2_g11844.z ) / 6.0 ) );
				float3 temp_output_40_0_g11842 = ( staticSwitch50_g11842 * pow( saferPower24_g11842 , _EnchantedContrast ) * _EnchantedBrightness );
				float temp_output_45_0_g11842 = ( max( ( temp_output_36_0_g11842 - _EnchantedReduce ) , 0.0 ) * _EnchantedFade );
				#ifdef _ENCHANTEDLERPTOGGLE_ON
				float3 lerpResult44_g11842 = lerp( temp_output_12_0_g11842 , temp_output_40_0_g11842 , temp_output_45_0_g11842);
				float3 staticSwitch47_g11842 = lerpResult44_g11842;
				#else
				float3 staticSwitch47_g11842 = ( temp_output_12_0_g11842 + ( temp_output_40_0_g11842 * temp_output_45_0_g11842 ) );
				#endif
				#ifdef _ENABLEENCHANTED_ON
				float4 appendResult19_g11842 = (float4(staticSwitch47_g11842 , temp_output_10_0_g11842.a));
				float4 staticSwitch11_g11842 = appendResult19_g11842;
				#else
				float4 staticSwitch11_g11842 = temp_output_10_0_g11842;
				#endif
				float4 temp_output_1_0_g11846 = staticSwitch11_g11842;
				float4 break5_g11846 = temp_output_1_0_g11846;
				float3 appendResult32_g11846 = (float3(break5_g11846.r , break5_g11846.g , break5_g11846.b));
				float4 break2_g11847 = temp_output_1_0_g11846;
				float temp_output_4_0_g11846 = ( ( break2_g11847.x + break2_g11847.x + break2_g11847.y + break2_g11847.y + break2_g11847.y + break2_g11847.z ) / 6.0 );
				float temp_output_11_0_g11846 = ( ( ( temp_output_4_0_g11846 + ( temp_output_40_0_g11808 * _ShiftingSpeed ) ) * _ShiftingDensity ) % 1.0 );
				float3 lerpResult20_g11846 = lerp( (_ShiftingColorA).rgb , (_ShiftingColorB).rgb , ( abs( ( temp_output_11_0_g11846 - 0.5 ) ) * 2.0 ));
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
				float3 hsvTorgb12_g11846 = HSVToRGB( float3(temp_output_11_0_g11846,_ShiftingSaturation,_ShiftingBrightness) );
				float3 staticSwitch26_g11846 = hsvTorgb12_g11846;
				#else
				float3 staticSwitch26_g11846 = ( lerpResult20_g11846 * _ShiftingBrightness );
				#endif
				#ifdef _ENABLESHIFTING_ON
				float3 lerpResult31_g11846 = lerp( appendResult32_g11846 , ( staticSwitch26_g11846 * pow( temp_output_4_0_g11846 , _ShiftingContrast ) ) , _ShiftingFade);
				float4 appendResult6_g11846 = (float4(lerpResult31_g11846 , break5_g11846.a));
				float4 staticSwitch33_g11846 = appendResult6_g11846;
				#else
				float4 staticSwitch33_g11846 = temp_output_1_0_g11846;
				#endif
				float4 temp_output_473_0 = staticSwitch33_g11846;
				#ifdef _ENABLEFULLDISTORTION_ON
				float4 break4_g11848 = temp_output_473_0;
				float fullDistortionAlpha164 = _FullDistortionFade;
				float4 appendResult5_g11848 = (float4(break4_g11848.r , break4_g11848.g , break4_g11848.b , ( break4_g11848.a * fullDistortionAlpha164 )));
				float4 staticSwitch77 = appendResult5_g11848;
				#else
				float4 staticSwitch77 = temp_output_473_0;
				#endif
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float4 break4_g11849 = staticSwitch77;
				float directionalDistortionAlpha167 = (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 ));
				float4 appendResult5_g11849 = (float4(break4_g11849.r , break4_g11849.g , break4_g11849.b , ( break4_g11849.a * directionalDistortionAlpha167 )));
				float4 staticSwitch75 = appendResult5_g11849;
				#else
				float4 staticSwitch75 = staticSwitch77;
				#endif
				float4 temp_output_1_0_g11850 = staticSwitch75;
				float4 temp_output_1_0_g11851 = temp_output_1_0_g11850;
				float temp_output_53_0_g11851 = max( _FullAlphaDissolveWidth , 0.001 );
				float2 temp_output_18_0_g11850 = shaderPosition235;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
				float linValue16_g11852 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11852 = MyCustomExpression16_g11852( linValue16_g11852 );
				float clampResult17_g11851 = clamp( ( ( ( _FullAlphaDissolveFade * ( 1.0 + temp_output_53_0_g11851 ) ) - localMyCustomExpression16_g11852 ) / temp_output_53_0_g11851 ) , 0.0 , 1.0 );
				float4 appendResult3_g11851 = (float4((temp_output_1_0_g11851).rgb , ( temp_output_1_0_g11851.a * clampResult17_g11851 )));
				float4 staticSwitch3_g11850 = appendResult3_g11851;
				#else
				float4 staticSwitch3_g11850 = temp_output_1_0_g11850;
				#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
				float linValue16_g11860 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11860 = MyCustomExpression16_g11860( linValue16_g11860 );
				float temp_output_5_0_g11859 = localMyCustomExpression16_g11860;
				float temp_output_61_0_g11859 = step( temp_output_5_0_g11859 , _FullGlowDissolveFade );
				float temp_output_53_0_g11859 = max( ( _FullGlowDissolveFade * _FullGlowDissolveWidth ) , 0.001 );
				float4 temp_output_1_0_g11859 = staticSwitch3_g11850;
				float4 appendResult3_g11859 = (float4(( ( (_FullGlowDissolveEdgeColor).rgb * ( temp_output_61_0_g11859 - step( temp_output_5_0_g11859 , ( ( _FullGlowDissolveFade * ( 1.01 + temp_output_53_0_g11859 ) ) - temp_output_53_0_g11859 ) ) ) ) + (temp_output_1_0_g11859).rgb ) , ( temp_output_1_0_g11859.a * temp_output_61_0_g11859 )));
				float4 staticSwitch5_g11850 = appendResult3_g11859;
				#else
				float4 staticSwitch5_g11850 = staticSwitch3_g11850;
				#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
				float4 temp_output_1_0_g11861 = staticSwitch5_g11850;
				float2 temp_output_76_0_g11861 = temp_output_18_0_g11850;
				float linValue16_g11862 = tex2D( _UberNoiseTexture, ( temp_output_76_0_g11861 * _SourceAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11862 = MyCustomExpression16_g11862( linValue16_g11862 );
				float clampResult17_g11861 = clamp( ( ( _SourceAlphaDissolveFade - ( distance( _SourceAlphaDissolvePosition , temp_output_76_0_g11861 ) + ( localMyCustomExpression16_g11862 * _SourceAlphaDissolveNoiseFactor ) ) ) / max( _SourceAlphaDissolveWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11861 = (float4((temp_output_1_0_g11861).rgb , ( temp_output_1_0_g11861.a * (( _SourceAlphaDissolveInvert )?( ( 1.0 - clampResult17_g11861 ) ):( clampResult17_g11861 )) )));
				float4 staticSwitch8_g11850 = appendResult3_g11861;
				#else
				float4 staticSwitch8_g11850 = staticSwitch5_g11850;
				#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
				float2 temp_output_90_0_g11857 = temp_output_18_0_g11850;
				float linValue16_g11858 = tex2D( _UberNoiseTexture, ( temp_output_90_0_g11857 * _SourceGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11858 = MyCustomExpression16_g11858( linValue16_g11858 );
				float temp_output_65_0_g11857 = ( distance( _SourceGlowDissolvePosition , temp_output_90_0_g11857 ) + ( localMyCustomExpression16_g11858 * _SourceGlowDissolveNoiseFactor ) );
				float temp_output_75_0_g11857 = step( temp_output_65_0_g11857 , _SourceGlowDissolveFade );
				float temp_output_76_0_g11857 = step( temp_output_65_0_g11857 , ( _SourceGlowDissolveFade - max( _SourceGlowDissolveWidth , 0.001 ) ) );
				float4 temp_output_1_0_g11857 = staticSwitch8_g11850;
				float4 appendResult3_g11857 = (float4(( ( max( ( temp_output_75_0_g11857 - temp_output_76_0_g11857 ) , 0.0 ) * (_SourceGlowDissolveEdgeColor).rgb ) + (temp_output_1_0_g11857).rgb ) , ( temp_output_1_0_g11857.a * (( _SourceGlowDissolveInvert )?( ( 1.0 - temp_output_76_0_g11857 ) ):( temp_output_75_0_g11857 )) )));
				float4 staticSwitch9_g11850 = appendResult3_g11857;
				#else
				float4 staticSwitch9_g11850 = staticSwitch8_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
				float4 temp_output_1_0_g11853 = staticSwitch9_g11850;
				float2 temp_output_161_0_g11853 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11853 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_161_0_g11853 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalAlphaFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11853 = rotatedValue136_g11853;
				float linValue16_g11854 = tex2D( _UberNoiseTexture, ( temp_output_161_0_g11853 * _DirectionalAlphaFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11854 = MyCustomExpression16_g11854( linValue16_g11854 );
				float clampResult154_g11853 = clamp( ( ( break130_g11853.x + break130_g11853.y + _DirectionalAlphaFadeFade + ( localMyCustomExpression16_g11854 * _DirectionalAlphaFadeNoiseFactor ) ) / max( _DirectionalAlphaFadeWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11853 = (float4((temp_output_1_0_g11853).rgb , ( temp_output_1_0_g11853.a * (( _DirectionalAlphaFadeInvert )?( ( 1.0 - clampResult154_g11853 ) ):( clampResult154_g11853 )) )));
				float4 staticSwitch11_g11850 = appendResult3_g11853;
				#else
				float4 staticSwitch11_g11850 = staticSwitch9_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
				float2 temp_output_171_0_g11855 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11855 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_171_0_g11855 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalGlowFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11855 = rotatedValue136_g11855;
				float linValue16_g11856 = tex2D( _UberNoiseTexture, ( temp_output_171_0_g11855 * _DirectionalGlowFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11856 = MyCustomExpression16_g11856( linValue16_g11856 );
				float temp_output_168_0_g11855 = max( ( ( break130_g11855.x + break130_g11855.y + _DirectionalGlowFadeFade + ( localMyCustomExpression16_g11856 * _DirectionalGlowFadeNoiseFactor ) ) / max( _DirectionalGlowFadeWidth , 0.001 ) ) , 0.0 );
				float temp_output_161_0_g11855 = step( 0.1 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) );
				float4 temp_output_1_0_g11855 = staticSwitch11_g11850;
				float clampResult154_g11855 = clamp( temp_output_161_0_g11855 , 0.0 , 1.0 );
				float4 appendResult3_g11855 = (float4(( ( (_DirectionalGlowFadeEdgeColor).rgb * ( temp_output_161_0_g11855 - step( 1.0 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) ) ) ) + (temp_output_1_0_g11855).rgb ) , ( temp_output_1_0_g11855.a * clampResult154_g11855 )));
				float4 staticSwitch15_g11850 = appendResult3_g11855;
				#else
				float4 staticSwitch15_g11850 = staticSwitch11_g11850;
				#endif
				float4 temp_output_1_0_g11863 = staticSwitch15_g11850;
				float2 temp_output_126_0_g11863 = temp_output_18_0_g11850;
				float temp_output_121_0_g11863 = max( ( ( _HalftoneFade - distance( _HalftonePosition , temp_output_126_0_g11863 ) ) / max( 0.01 , _HalftoneFadeWidth ) ) , 0.0 );
				float2 appendResult11_g11864 = (float2(temp_output_121_0_g11863 , temp_output_121_0_g11863));
				float temp_output_17_0_g11864 = length( ( (( ( abs( temp_output_126_0_g11863 ) * _HalftoneTiling ) % float2( 1,1 ) )*2.0 + -1.0) / appendResult11_g11864 ) );
				#ifdef _ENABLEHALFTONE_ON
				float clampResult17_g11863 = clamp( saturate( ( ( 1.0 - temp_output_17_0_g11864 ) / fwidth( temp_output_17_0_g11864 ) ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11863 = (float4((temp_output_1_0_g11863).rgb , ( temp_output_1_0_g11863.a * (( _HalftoneInvert )?( ( 1.0 - clampResult17_g11863 ) ):( clampResult17_g11863 )) )));
				float4 staticSwitch13_g11850 = appendResult3_g11863;
				#else
				float4 staticSwitch13_g11850 = staticSwitch15_g11850;
				#endif
				float3 temp_output_3_0_g11866 = (_AddColorColor).rgb;
				#ifdef _ADDCOLORMASKTOGGLE_ON
				float2 uv_AddColorMask = IN.ase_texcoord3.xy * _AddColorMask_ST.xy + _AddColorMask_ST.zw;
				float4 tex2DNode19_g11866 = tex2D( _AddColorMask, uv_AddColorMask );
				float3 staticSwitch16_g11866 = ( temp_output_3_0_g11866 * ( (tex2DNode19_g11866).rgb * tex2DNode19_g11866.a ) );
				#else
				float3 staticSwitch16_g11866 = temp_output_3_0_g11866;
				#endif
				float4 temp_output_1_0_g11866 = staticSwitch13_g11850;
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
				float4 break2_g11868 = temp_output_1_0_g11866;
				float temp_output_9_0_g11867 = max( _AddColorContrast , 0.0 );
				float saferPower7_g11867 = abs( ( ( ( break2_g11868.x + break2_g11868.x + break2_g11868.y + break2_g11868.y + break2_g11868.y + break2_g11868.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11867 ) , 0.0 ) ) ) );
				float3 staticSwitch17_g11866 = ( staticSwitch16_g11866 * pow( saferPower7_g11867 , temp_output_9_0_g11867 ) );
				#else
				float3 staticSwitch17_g11866 = staticSwitch16_g11866;
				#endif
				#ifdef _ENABLEADDCOLOR_ON
				float4 appendResult6_g11866 = (float4(( ( staticSwitch17_g11866 * _AddColorFade ) + (temp_output_1_0_g11866).rgb ) , temp_output_1_0_g11866.a));
				float4 staticSwitch5_g11865 = appendResult6_g11866;
				#else
				float4 staticSwitch5_g11865 = staticSwitch13_g11850;
				#endif
				#ifdef _ENABLEALPHATINT_ON
				float4 temp_output_1_0_g11869 = staticSwitch5_g11865;
				float3 lerpResult4_g11869 = lerp( (temp_output_1_0_g11869).rgb , (_AlphaTintColor).rgb , ( ( 1.0 - temp_output_1_0_g11869.a ) * step( _AlphaTintMinAlpha , temp_output_1_0_g11869.a ) * _AlphaTintFade ));
				float4 appendResult13_g11869 = (float4(lerpResult4_g11869 , temp_output_1_0_g11869.a));
				float4 staticSwitch11_g11865 = appendResult13_g11869;
				#else
				float4 staticSwitch11_g11865 = staticSwitch5_g11865;
				#endif
				float4 temp_output_1_0_g11870 = staticSwitch11_g11865;
				float3 temp_output_6_0_g11870 = (_StrongTintTint).rgb;
				#ifdef _STRONGTINTMASKTOGGLE_ON
				float2 uv_StrongTintMask = IN.ase_texcoord3.xy * _StrongTintMask_ST.xy + _StrongTintMask_ST.zw;
				float4 tex2DNode23_g11870 = tex2D( _StrongTintMask, uv_StrongTintMask );
				float3 staticSwitch21_g11870 = ( temp_output_6_0_g11870 * ( (tex2DNode23_g11870).rgb * tex2DNode23_g11870.a ) );
				#else
				float3 staticSwitch21_g11870 = temp_output_6_0_g11870;
				#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
				float4 break2_g11872 = temp_output_1_0_g11870;
				float temp_output_9_0_g11871 = max( _StrongTintContrast , 0.0 );
				float saferPower7_g11871 = abs( ( ( ( break2_g11872.x + break2_g11872.x + break2_g11872.y + break2_g11872.y + break2_g11872.y + break2_g11872.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11871 ) , 0.0 ) ) ) );
				float3 staticSwitch22_g11870 = ( pow( saferPower7_g11871 , temp_output_9_0_g11871 ) * staticSwitch21_g11870 );
				#else
				float3 staticSwitch22_g11870 = staticSwitch21_g11870;
				#endif
				#ifdef _ENABLESTRONGTINT_ON
				float3 lerpResult7_g11870 = lerp( (temp_output_1_0_g11870).rgb , staticSwitch22_g11870 , _StrongTintFade);
				float4 appendResult9_g11870 = (float4(lerpResult7_g11870 , (temp_output_1_0_g11870).a));
				float4 staticSwitch7_g11865 = appendResult9_g11870;
				#else
				float4 staticSwitch7_g11865 = staticSwitch11_g11865;
				#endif
				float4 temp_output_2_0_g11873 = staticSwitch7_g11865;
				#ifdef _ENABLESHADOW_ON
				float4 break4_g11875 = temp_output_2_0_g11873;
				float3 appendResult5_g11875 = (float3(break4_g11875.r , break4_g11875.g , break4_g11875.b));
				float2 appendResult2_g11874 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float4 appendResult85_g11873 = (float4(_ShadowColor.r , _ShadowColor.g , _ShadowColor.b , ( _ShadowFade * tex2D( _MainTex, ( finalUV146 - ( ( 100.0 / appendResult2_g11874 ) * _ShadowOffset ) ) ).a )));
				float4 break6_g11875 = appendResult85_g11873;
				float3 appendResult7_g11875 = (float3(break6_g11875.r , break6_g11875.g , break6_g11875.b));
				float temp_output_11_0_g11875 = ( ( 1.0 - break4_g11875.a ) * break6_g11875.a );
				float temp_output_32_0_g11875 = ( break4_g11875.a + temp_output_11_0_g11875 );
				float4 appendResult18_g11875 = (float4(( ( ( appendResult5_g11875 * break4_g11875.a ) + ( appendResult7_g11875 * temp_output_11_0_g11875 ) ) * ( 1.0 / max( temp_output_32_0_g11875 , 0.01 ) ) ) , temp_output_32_0_g11875));
				float4 staticSwitch82_g11873 = appendResult18_g11875;
				#else
				float4 staticSwitch82_g11873 = temp_output_2_0_g11873;
				#endif
				float4 break4_g11876 = staticSwitch82_g11873;
				#ifdef _ENABLECUSTOMFADE_ON
				float staticSwitch8_g11756 = 1.0;
				#else
				float staticSwitch8_g11756 = IN.ase_color.a;
				#endif
				#ifdef _ENABLESMOKE_ON
				float staticSwitch9_g11756 = 1.0;
				#else
				float staticSwitch9_g11756 = staticSwitch8_g11756;
				#endif
				float customVertexAlpha193 = staticSwitch9_g11756;
				float4 appendResult5_g11876 = (float4(break4_g11876.r , break4_g11876.g , break4_g11876.b , ( break4_g11876.a * customVertexAlpha193 )));
				float4 temp_output_344_0 = appendResult5_g11876;
				float4 temp_output_1_0_g11877 = temp_output_344_0;
				float4 appendResult8_g11877 = (float4(( (temp_output_1_0_g11877).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11877.a));
				#ifdef _VERTEXTINTFIRST_ON
				float4 staticSwitch342 = temp_output_344_0;
				#else
				float4 staticSwitch342 = appendResult8_g11877;
				#endif
				float4 lerpResult125 = lerp( ( originalColor191 * IN.ase_color ) , staticSwitch342 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float4 staticSwitch143 = staticSwitch342;
				#elif defined(_SHADERFADING_FULL)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_MASK)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_DISSOLVE)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_SPREAD)
				float4 staticSwitch143 = lerpResult125;
				#else
				float4 staticSwitch143 = staticSwitch342;
				#endif
				float4 temp_output_7_0_g11884 = staticSwitch143;
				#ifdef _BAKEDMATERIAL_ON
				float4 appendResult2_g11884 = (float4(( (temp_output_7_0_g11884).rgb / max( temp_output_7_0_g11884.a , 1E-05 ) ) , temp_output_7_0_g11884.a));
				float4 staticSwitch6_g11884 = appendResult2_g11884;
				#else
				float4 staticSwitch6_g11884 = temp_output_7_0_g11884;
				#endif
				float4 temp_output_340_0 = staticSwitch6_g11884;
				
				#ifdef _EMISSIONTOGGLE_ON
				float3 appendResult20_g11885 = (float3(_EmissionTint.r , _EmissionTint.g , _EmissionTint.b));
				float2 uv_EmissionMap = IN.ase_texcoord3.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
				float4 tex2DNode17_g11885 = tex2D( _EmissionMap, uv_EmissionMap );
				float3 appendResult18_g11885 = (float3(tex2DNode17_g11885.r , tex2DNode17_g11885.g , tex2DNode17_g11885.b));
				float3 staticSwitch13_g11885 = ( appendResult20_g11885 * appendResult18_g11885 * tex2DNode17_g11885.a );
				#else
				float3 staticSwitch13_g11885 = float3(0,0,0);
				#endif
				
				o.Albedo = temp_output_340_0.rgb;
				o.Normal = fixed3( 0, 0, 1 );
				o.Emission = staticSwitch13_g11885;
				o.Alpha = temp_output_340_0.a;
				float AlphaClipThreshold = _AlphaClip;

				#ifdef _ALPHATEST_ON
					clip( o.Alpha - AlphaClipThreshold );
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = IN.pos.z;
				#endif

				UnityMetaInput metaIN;
				UNITY_INITIALIZE_OUTPUT(UnityMetaInput, metaIN);
				metaIN.Albedo = o.Albedo;
				metaIN.Emission = o.Emission;
				#ifdef EDITOR_VISUALIZATION
					metaIN.VizUV = IN.vizUV;
					metaIN.LightCoord = IN.lightCoord;
				#endif
				return UnityMetaFragment(metaIN);
			}
			ENDCG
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }
			ZWrite On
			ZTest LEqual
			AlphaToMask Off

			CGPROGRAM
			#define ASE_NEEDS_FRAG_SHADOWCOORDS
			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _ALPHATEST_ON 1

			#pragma vertex vert
			#pragma fragment frag
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#pragma multi_compile_shadowcaster
			#ifndef UNITY_PASS_SHADOWCASTER
				#define UNITY_PASS_SHADOWCASTER
			#endif
			#include "HLSLSupport.cginc"
			#ifndef UNITY_INSTANCED_LOD_FADE
				#define UNITY_INSTANCED_LOD_FADE
			#endif
			#ifndef UNITY_INSTANCED_SH
				#define UNITY_INSTANCED_SH
			#endif
			#ifndef UNITY_INSTANCED_LIGHTMAPSTS
				#define UNITY_INSTANCED_LIGHTMAPSTS
			#endif
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"

			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_POSITION
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _SHADERFADING_NONE _SHADERFADING_FULL _SHADERFADING_MASK _SHADERFADING_DISSOLVE _SHADERFADING_SPREAD
			#pragma shader_feature_local _ENABLESINESCALE_ON
			#pragma shader_feature _ENABLEVIBRATE_ON
			#pragma shader_feature _ENABLESINEMOVE_ON
			#pragma shader_feature _ENABLESQUISH_ON
			#pragma shader_feature _SPRITESHEETFIX_ON
			#pragma shader_feature_local _PIXELPERFECTUV_ON
			#pragma shader_feature _ENABLEWORLDTILING_ON
			#pragma shader_feature _ENABLESCREENTILING_ON
			#pragma shader_feature _TOGGLETIMEFREQUENCY_ON
			#pragma shader_feature _TOGGLETIMEFPS_ON
			#pragma shader_feature _TOGGLETIMESPEED_ON
			#pragma shader_feature _TOGGLEUNSCALEDTIME_ON
			#pragma shader_feature _TOGGLECUSTOMTIME_ON
			#pragma shader_feature _SHADERSPACE_UV _SHADERSPACE_UV_RAW _SHADERSPACE_OBJECT _SHADERSPACE_OBJECT_SCALED _SHADERSPACE_WORLD _SHADERSPACE_UI_GRAPHIC _SHADERSPACE_SCREEN
			#pragma shader_feature _PIXELPERFECTSPACE_ON
			#pragma shader_feature _BAKEDMATERIAL_ON
			#pragma shader_feature _VERTEXTINTFIRST_ON
			#pragma shader_feature _ENABLESHADOW_ON
			#pragma shader_feature _ENABLESTRONGTINT_ON
			#pragma shader_feature _ENABLEALPHATINT_ON
			#pragma shader_feature_local _ENABLEADDCOLOR_ON
			#pragma shader_feature_local _ENABLEHALFTONE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALGLOWFADE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALALPHAFADE_ON
			#pragma shader_feature_local _ENABLESOURCEGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLESOURCEALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLGLOWDISSOLVE_ON
			#pragma shader_feature_local _ENABLEFULLALPHADISSOLVE_ON
			#pragma shader_feature_local _ENABLEDIRECTIONALDISTORTION_ON
			#pragma shader_feature_local _ENABLEFULLDISTORTION_ON
			#pragma shader_feature _ENABLESHIFTING_ON
			#pragma shader_feature _ENABLEENCHANTED_ON
			#pragma shader_feature_local _ENABLEPOISON_ON
			#pragma shader_feature_local _ENABLESHINE_ON
			#pragma shader_feature_local _ENABLERAINBOW_ON
			#pragma shader_feature_local _ENABLEBURN_ON
			#pragma shader_feature_local _ENABLEFROZEN_ON
			#pragma shader_feature_local _ENABLEMETAL_ON
			#pragma shader_feature_local _ENABLECAMOUFLAGE_ON
			#pragma shader_feature_local _ENABLEGLITCH_ON
			#pragma shader_feature_local _ENABLEHOLOGRAM_ON
			#pragma shader_feature _ENABLEPINGPONGGLOW_ON
			#pragma shader_feature_local _ENABLEPIXELOUTLINE_ON
			#pragma shader_feature_local _ENABLEOUTEROUTLINE_ON
			#pragma shader_feature_local _ENABLEINNEROUTLINE_ON
			#pragma shader_feature_local _ENABLESATURATION_ON
			#pragma shader_feature_local _ENABLESINEGLOW_ON
			#pragma shader_feature_local _ENABLEADDHUE_ON
			#pragma shader_feature_local _ENABLESHIFTHUE_ON
			#pragma shader_feature_local _ENABLEINKSPREAD_ON
			#pragma shader_feature_local _ENABLEBLACKTINT_ON
			#pragma shader_feature_local _ENABLESPLITTONING_ON
			#pragma shader_feature_local _ENABLEHUE_ON
			#pragma shader_feature_local _ENABLEBRIGHTNESS_ON
			#pragma shader_feature_local _ENABLECONTRAST_ON
			#pragma shader_feature _ENABLENEGATIVE_ON
			#pragma shader_feature_local _ENABLECOLORREPLACE_ON
			#pragma shader_feature_local _ENABLERECOLORRGBYCP_ON
			#pragma shader_feature _ENABLERECOLORRGB_ON
			#pragma shader_feature_local _ENABLEFLAME_ON
			#pragma shader_feature_local _ENABLECHECKERBOARD_ON
			#pragma shader_feature_local _ENABLECUSTOMFADE_ON
			#pragma shader_feature_local _ENABLESMOKE_ON
			#pragma shader_feature _ENABLESHARPEN_ON
			#pragma shader_feature _ENABLEGAUSSIANBLUR_ON
			#pragma shader_feature _ENABLESMOOTHPIXELART_ON
			#pragma shader_feature_local _TILINGFIX_ON
			#pragma shader_feature _ENABLEWIGGLE_ON
			#pragma shader_feature_local _ENABLEUVSCALE_ON
			#pragma shader_feature_local _ENABLEPIXELATE_ON
			#pragma shader_feature_local _ENABLEUVSCROLL_ON
			#pragma shader_feature_local _ENABLEUVROTATE_ON
			#pragma shader_feature_local _ENABLESINEROTATE_ON
			#pragma shader_feature_local _ENABLESQUEEZE_ON
			#pragma shader_feature_local _ENABLEUVDISTORT_ON
			#pragma shader_feature_local _ENABLEWIND_ON
			#pragma shader_feature_local _WINDLOCALWIND_ON
			#pragma shader_feature_local _WINDHIGHQUALITYNOISE_ON
			#pragma shader_feature_local _WINDISPARALLAX_ON
			#pragma shader_feature _UVDISTORTMASKTOGGLE_ON
			#pragma shader_feature _WIGGLEFIXEDGROUNDTOGGLE_ON
			#pragma shader_feature _RECOLORRGBTEXTURETOGGLE_ON
			#pragma shader_feature _RECOLORRGBYCPTEXTURETOGGLE_ON
			#pragma shader_feature_local _ADDHUEMASKTOGGLE_ON
			#pragma shader_feature_local _SINEGLOWMASKTOGGLE_ON
			#pragma shader_feature _INNEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature_local _INNEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _INNEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _OUTEROUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature_local _OUTEROUTLINEDISTORTIONTOGGLE_ON
			#pragma shader_feature _PIXELOUTLINETEXTURETOGGLE_ON
			#pragma shader_feature _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
			#pragma shader_feature _CAMOUFLAGEANIMATIONTOGGLE_ON
			#pragma shader_feature _METALMASKTOGGLE_ON
			#pragma shader_feature _SHINEMASKTOGGLE_ON
			#pragma shader_feature _ENCHANTEDLERPTOGGLE_ON
			#pragma shader_feature _ENCHANTEDRAINBOWTOGGLE_ON
			#pragma shader_feature _SHIFTINGRAINBOWTOGGLE_ON
			#pragma shader_feature _ADDCOLORCONTRASTTOGGLE_ON
			#pragma shader_feature _ADDCOLORMASKTOGGLE_ON
			#pragma shader_feature _STRONGTINTCONTRASTTOGGLE_ON
			#pragma shader_feature _STRONGTINTMASKTOGGLE_ON

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				V2F_SHADOW_CASTER;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#ifdef UNITY_STANDARD_USE_DITHER_MASK
				sampler3D _DitherMaskLOD;
			#endif
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishStretch;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingScale;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float2 _ScreenTilingOffset;
			#endif
				#ifdef _ENABLESCREENTILING_ON
			uniform float _ScreenTilingPixelsPerUnit;
			#endif
			uniform sampler2D _MainTex;
			float4 _MainTex_TexelSize;
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingScale;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float2 _WorldTilingOffset;
			#endif
				#ifdef _ENABLEWORLDTILING_ON
			uniform float _WorldTilingPixelsPerUnit;
			#endif
			uniform float4 _SpriteSheetRect;
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFade;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishFlip;
			#endif
				#ifdef _ENABLESQUISH_ON
			uniform float _SquishSquish;
			#endif
				#ifdef _TOGGLECUSTOMTIME_ON
			uniform float _TimeValue;
			#endif
			uniform float UnscaledTime;
				#ifdef _TOGGLETIMESPEED_ON
			uniform float _TimeSpeed;
			#endif
				#ifdef _TOGGLETIMEFPS_ON
			uniform float _TimeFPS;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeFrequency;
			#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
			uniform float _TimeRange;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveFrequency;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float2 _SineMoveOffset;
			#endif
				#ifdef _ENABLESINEMOVE_ON
			uniform float _SineMoveFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFrequency;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateOffset;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateFade;
			#endif
				#ifdef _ENABLEVIBRATE_ON
			uniform float _VibrateRotation;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float _SineScaleFrequency;
			#endif
				#ifdef _ENABLESINESCALE_ON
			uniform float2 _SineScaleFactor;
			#endif
			uniform float _FadingFade;
			uniform sampler2D _FadingMask;
			uniform float4 _FadingMask_ST;
			uniform float _FadingWidth;
			uniform sampler2D _UberNoiseTexture;
			uniform float _PixelsPerUnit;
			uniform float _RectWidth;
			uniform float _RectHeight;
			uniform float _ScreenWidthUnits;
			uniform float2 _FadingNoiseScale;
			uniform float2 _FadingPosition;
			uniform float _FadingNoiseFactor;
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotationWindFactor;
			#endif
			uniform float WindMinIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMinIntensity;
			#endif
			uniform float WindMaxIntensity;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindMaxIntensity;
			#endif
				#ifdef _WINDISPARALLAX_ON
			uniform float _WindXPosition;
			#endif
			uniform float WindNoiseScale;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseScale;
			#endif
			uniform float WindTime;
				#ifdef _WINDLOCALWIND_ON
			uniform float _WindNoiseSpeed;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindMaxRotation;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindFlip;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishFactor;
			#endif
				#ifdef _ENABLEWIND_ON
			uniform float _WindSquishWindFactor;
			#endif
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float _FullDistortionFade;
			#endif
			uniform float2 _FullDistortionNoiseScale;
				#ifdef _ENABLEFULLDISTORTION_ON
			uniform float2 _FullDistortionDistortion;
			#endif
			uniform float2 _DirectionalDistortionDistortionScale;
			uniform float _DirectionalDistortionRandomDirection;
			uniform float2 _DirectionalDistortionDistortion;
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
			uniform float _DirectionalDistortionInvert;
			#endif
			uniform float _DirectionalDistortionRotation;
			uniform float _DirectionalDistortionFade;
			uniform float2 _DirectionalDistortionNoiseScale;
			uniform float _DirectionalDistortionNoiseFactor;
			uniform float _DirectionalDistortionWidth;
			uniform float _HologramDistortionSpeed;
			uniform float _HologramDistortionDensity;
			uniform float _HologramDistortionScale;
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramDistortionOffset;
			#endif
			uniform float _HologramFade;
			uniform float2 _GlitchDistortionSpeed;
			uniform float2 _GlitchDistortionScale;
			uniform float2 _GlitchDistortion;
			uniform float2 _GlitchMaskSpeed;
			uniform float2 _GlitchMaskScale;
			uniform float _GlitchMaskMin;
			uniform float _GlitchFade;
			uniform float2 _UVDistortFrom;
			uniform float2 _UVDistortTo;
			uniform float2 _UVDistortSpeed;
			uniform float2 _UVDistortNoiseScale;
			uniform float _UVDistortFade;
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform sampler2D _UVDistortMask;
			#endif
				#ifdef _UVDISTORTMASKTOGGLE_ON
			uniform float4 _UVDistortMask_ST;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeCenter;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezePower;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float2 _SqueezeScale;
			#endif
				#ifdef _ENABLESQUEEZE_ON
			uniform float _SqueezeFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFrequency;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateAngle;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float _SineRotateFade;
			#endif
				#ifdef _ENABLESINEROTATE_ON
			uniform float2 _SineRotatePivot;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float _UVRotateSpeed;
			#endif
				#ifdef _ENABLEUVROTATE_ON
			uniform float2 _UVRotatePivot;
			#endif
				#ifdef _ENABLEUVSCROLL_ON
			uniform float2 _UVScrollSpeed;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelDensity;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelatePixelsPerUnit;
			#endif
				#ifdef _ENABLEPIXELATE_ON
			uniform float _PixelateFade;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScalePivot;
			#endif
				#ifdef _ENABLEUVSCALE_ON
			uniform float2 _UVScaleScale;
			#endif
			uniform float _WiggleFrequency;
			uniform float _WiggleSpeed;
			uniform float _WiggleOffset;
			uniform float _WiggleFade;
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurOffset;
			#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
			uniform float _GaussianBlurFade;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenOffset;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFactor;
			#endif
				#ifdef _ENABLESHARPEN_ON
			uniform float _SharpenFade;
			#endif
			uniform float _SmokeVertexSeed;
			uniform float _SmokeNoiseScale;
			uniform float _SmokeNoiseFactor;
			uniform float _SmokeSmoothness;
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeDarkEdge;
			#endif
				#ifdef _ENABLESMOKE_ON
			uniform float _SmokeAlpha;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform sampler2D _CustomFadeFadeMask;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float2 _CustomFadeNoiseScale;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeNoiseFactor;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeSmoothness;
			#endif
				#ifdef _ENABLECUSTOMFADE_ON
			uniform float _CustomFadeAlpha;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardDarken;
			#endif
				#ifdef _ENABLECHECKERBOARD_ON
			uniform float _CheckerboardTiling;
			#endif
			uniform float2 _FlameSpeed;
			uniform float2 _FlameNoiseScale;
			uniform float _FlameNoiseHeightFactor;
			uniform float _FlameNoiseFactor;
			uniform float _FlameRadius;
			uniform float _FlameSmooth;
				#ifdef _ENABLEFLAME_ON
			uniform float _FlameBrightness;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBRedTint;
			#endif
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBTexture;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBGreenTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float4 _RecolorRGBBlueTint;
			#endif
				#ifdef _ENABLERECOLORRGB_ON
			uniform float _RecolorRGBFade;
			#endif
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
			uniform sampler2D _RecolorRGBYCPTexture;
			#endif
			uniform float4 _RecolorRGBYCPPurpleTint;
			uniform float4 _RecolorRGBYCPBlueTint;
			uniform float4 _RecolorRGBYCPCyanTint;
			uniform float4 _RecolorRGBYCPGreenTint;
			uniform float4 _RecolorRGBYCPYellowTint;
			uniform float4 _RecolorRGBYCPRedTint;
				#ifdef _ENABLERECOLORRGBYCP_ON
			uniform float _RecolorRGBYCPFade;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceFromColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceContrast;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float4 _ColorReplaceToColor;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceSmoothness;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceRange;
			#endif
				#ifdef _ENABLECOLORREPLACE_ON
			uniform float _ColorReplaceFade;
			#endif
				#ifdef _ENABLENEGATIVE_ON
			uniform float _NegativeFade;
			#endif
				#ifdef _ENABLECONTRAST_ON
			uniform float _Contrast;
			#endif
				#ifdef _ENABLEBRIGHTNESS_ON
			uniform float _Brightness;
			#endif
				#ifdef _ENABLEHUE_ON
			uniform float _Hue;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningShadowsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float4 _SplitToningHighlightsColor;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningShift;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningBalance;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningContrast;
			#endif
				#ifdef _ENABLESPLITTONING_ON
			uniform float _SplitToningFade;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float4 _BlackTintColor;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintPower;
			#endif
				#ifdef _ENABLEBLACKTINT_ON
			uniform float _BlackTintFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float4 _InkSpreadColor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadContrast;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadFade;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadDistance;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadPosition;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float2 _InkSpreadNoiseScale;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadNoiseFactor;
			#endif
				#ifdef _ENABLEINKSPREAD_ON
			uniform float _InkSpreadWidth;
			#endif
				#ifdef _ENABLESHIFTHUE_ON
			uniform float _ShiftHueSpeed;
			#endif
			uniform float _AddHueSpeed;
			uniform float _AddHueSaturation;
			uniform float _AddHueBrightness;
				#ifdef _ENABLEADDHUE_ON
			uniform float _AddHueContrast;
			#endif
			uniform float _AddHueFade;
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform sampler2D _AddHueMask;
			#endif
				#ifdef _ADDHUEMASKTOGGLE_ON
			uniform float4 _AddHueMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowContrast;
			#endif
			uniform float4 _SineGlowColor;
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform sampler2D _SineGlowMask;
			#endif
				#ifdef _SINEGLOWMASKTOGGLE_ON
			uniform float4 _SineGlowMask_ST;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFade;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowFrequency;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMax;
			#endif
				#ifdef _ENABLESINEGLOW_ON
			uniform float _SineGlowMin;
			#endif
				#ifdef _ENABLESATURATION_ON
			uniform float _Saturation;
			#endif
			uniform float4 _InnerOutlineColor;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _InnerOutlineTintTexture;
			#endif
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
			uniform float2 _InnerOutlineTextureSpeed;
			#endif
			uniform float _InnerOutlineFade;
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseSpeed;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineNoiseScale;
			#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _InnerOutlineDistortionIntensity;
			#endif
			uniform float _InnerOutlineWidth;
			uniform float4 _OuterOutlineColor;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform sampler2D _OuterOutlineTintTexture;
			#endif
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
			uniform float2 _OuterOutlineTextureSpeed;
			#endif
			uniform float _OuterOutlineFade;
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseSpeed;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineNoiseScale;
			#endif
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
			uniform float2 _OuterOutlineDistortionIntensity;
			#endif
			uniform float _OuterOutlineWidth;
			uniform float4 _PixelOutlineColor;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform sampler2D _PixelOutlineTintTexture;
			#endif
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
			uniform float2 _PixelOutlineTextureSpeed;
			#endif
			uniform float _PixelOutlineFade;
			uniform float _PixelOutlineWidth;
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowFrom;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float4 _PingPongGlowTo;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFrequency;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowFade;
			#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
			uniform float _PingPongGlowContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float4 _HologramTint;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramContrast;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineSpeed;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineFrequency;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramLineGap;
			#endif
				#ifdef _ENABLEHOLOGRAM_ON
			uniform float _HologramMinAlpha;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchBrightness;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseSpeed;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float2 _GlitchNoiseScale;
			#endif
				#ifdef _ENABLEGLITCH_ON
			uniform float _GlitchHueSpeed;
			#endif
			uniform float4 _CamouflageBaseColor;
			uniform float4 _CamouflageColorA;
			uniform float _CamouflageDensityA;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionSpeed;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionScale;
			#endif
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
			uniform float2 _CamouflageDistortionIntensity;
			#endif
			uniform float2 _CamouflageNoiseScaleA;
			uniform float _CamouflageSmoothnessA;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float4 _CamouflageColorB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageDensityB;
			#endif
			uniform float2 _CamouflageNoiseScaleB;
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageSmoothnessB;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageContrast;
			#endif
				#ifdef _ENABLECAMOUFLAGE_ON
			uniform float _CamouflageFade;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalHighlightDensity;
			#endif
			uniform float2 _MetalNoiseDistortionSpeed;
			uniform float2 _MetalNoiseDistortionScale;
			uniform float2 _MetalNoiseDistortion;
			uniform float2 _MetalNoiseSpeed;
			uniform float2 _MetalNoiseScale;
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalHighlightColor;
			#endif
			uniform float _MetalHighlightContrast;
				#ifdef _ENABLEMETAL_ON
			uniform float _MetalContrast;
			#endif
				#ifdef _ENABLEMETAL_ON
			uniform float4 _MetalColor;
			#endif
			uniform float _MetalFade;
				#ifdef _METALMASKTOGGLE_ON
			uniform sampler2D _MetalMask;
			#endif
				#ifdef _METALMASKTOGGLE_ON
			uniform float4 _MetalMask_ST;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenTint;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenSnowColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenSnowDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenSnowScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightDensity;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortionScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightDistortion;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightSpeed;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float2 _FrozenHighlightScale;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float4 _FrozenHighlightColor;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenHighlightContrast;
			#endif
				#ifdef _ENABLEFROZEN_ON
			uniform float _FrozenFade;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideContrast;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideNoiseColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnInsideNoiseFactor;
			#endif
			uniform float2 _BurnSwirlNoiseScale;
			uniform float _BurnSwirlFactor;
			uniform float2 _BurnInsideNoiseScale;
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnInsideColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnRadius;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnPosition;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float2 _BurnEdgeNoiseScale;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnEdgeNoiseFactor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnWidth;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float4 _BurnEdgeColor;
			#endif
				#ifdef _ENABLEBURN_ON
			uniform float _BurnFade;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowCenter;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float2 _RainbowNoiseScale;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowNoiseFactor;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowDensity;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSpeed;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowSaturation;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowBrightness;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowContrast;
			#endif
				#ifdef _ENABLERAINBOW_ON
			uniform float _RainbowFade;
			#endif
			uniform float _ShineSaturation;
			uniform float _ShineContrast;
				#ifdef _ENABLESHINE_ON
			uniform float4 _ShineColor;
			#endif
			uniform float _ShineRotation;
			uniform float _ShineFrequency;
			uniform float _ShineSpeed;
			uniform float _ShineWidth;
			uniform float _ShineFade;
				#ifdef _SHINEMASKTOGGLE_ON
			uniform sampler2D _ShineMask;
			#endif
				#ifdef _SHINEMASKTOGGLE_ON
			uniform float4 _ShineMask_ST;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float2 _PoisonNoiseScale;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonShiftSpeed;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonDensity;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float4 _PoisonColor;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonFade;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonNoiseBrightness;
			#endif
				#ifdef _ENABLEPOISON_ON
			uniform float _PoisonRecolorFactor;
			#endif
			uniform float4 _EnchantedLowColor;
			uniform float4 _EnchantedHighColor;
			uniform float2 _EnchantedSpeed;
			uniform float2 _EnchantedScale;
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowDensity;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSpeed;
			#endif
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
			uniform float _EnchantedRainbowSaturation;
			#endif
			uniform float _EnchantedContrast;
			uniform float _EnchantedBrightness;
			uniform float _EnchantedReduce;
			uniform float _EnchantedFade;
			uniform float4 _ShiftingColorA;
			uniform float4 _ShiftingColorB;
			uniform float _ShiftingSpeed;
			uniform float _ShiftingDensity;
			uniform float _ShiftingBrightness;
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
			uniform float _ShiftingSaturation;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingContrast;
			#endif
				#ifdef _ENABLESHIFTING_ON
			uniform float _ShiftingFade;
			#endif
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float _FullAlphaDissolveFade;
			#endif
			uniform float _FullAlphaDissolveWidth;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
			uniform float2 _FullAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float4 _FullGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float2 _FullGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveFade;
			#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
			uniform float _FullGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveInvert;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveFade;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float2 _SourceAlphaDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
			uniform float _SourceAlphaDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolvePosition;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float2 _SourceGlowDissolveNoiseScale;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveNoiseFactor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveFade;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveWidth;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float4 _SourceGlowDissolveEdgeColor;
			#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
			uniform float _SourceGlowDissolveInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float2 _DirectionalAlphaFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
			uniform float _DirectionalAlphaFadeWidth;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float4 _DirectionalGlowFadeEdgeColor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeInvert;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeRotation;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeFade;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float2 _DirectionalGlowFadeNoiseScale;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeNoiseFactor;
			#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
			uniform float _DirectionalGlowFadeWidth;
			#endif
				#ifdef _ENABLEHALFTONE_ON
			uniform float _HalftoneInvert;
			#endif
			uniform float _HalftoneTiling;
			uniform float _HalftoneFade;
			uniform float2 _HalftonePosition;
			uniform float _HalftoneFadeWidth;
			uniform float4 _AddColorColor;
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform sampler2D _AddColorMask;
			#endif
				#ifdef _ADDCOLORMASKTOGGLE_ON
			uniform float4 _AddColorMask_ST;
			#endif
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
			uniform float _AddColorContrast;
			#endif
				#ifdef _ENABLEADDCOLOR_ON
			uniform float _AddColorFade;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float4 _AlphaTintColor;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintMinAlpha;
			#endif
				#ifdef _ENABLEALPHATINT_ON
			uniform float _AlphaTintFade;
			#endif
			uniform float4 _StrongTintTint;
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform sampler2D _StrongTintMask;
			#endif
				#ifdef _STRONGTINTMASKTOGGLE_ON
			uniform float4 _StrongTintMask_ST;
			#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
			uniform float _StrongTintContrast;
			#endif
				#ifdef _ENABLESTRONGTINT_ON
			uniform float _StrongTintFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float4 _ShadowColor;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float _ShadowFade;
			#endif
				#ifdef _ENABLESHADOW_ON
			uniform float2 _ShadowOffset;
			#endif
			uniform float _AlphaClip;

	
			float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
			{
				original -= center;
				float C = cos( angle );
				float S = sin( angle );
				float t = 1 - C;
				float m00 = t * u.x * u.x + C;
				float m01 = t * u.x * u.y - S * u.z;
				float m02 = t * u.x * u.z + S * u.y;
				float m10 = t * u.x * u.y + S * u.z;
				float m11 = t * u.y * u.y + C;
				float m12 = t * u.y * u.z - S * u.x;
				float m20 = t * u.x * u.z - S * u.y;
				float m21 = t * u.y * u.z + S * u.x;
				float m22 = t * u.z * u.z + C;
				float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
				return mul( finalMatrix, original ) + center;
			}
			
			float MyCustomExpression16_g11717( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11719( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float FastNoise101_g11668( float x )
			{
				float i = floor(x);
				float f = frac(x);
				float s = sign(frac(x/2.0)-0.5);
				    
				float k = 0.5+0.5*sin(i);
				return s*f*(f-1.0)*((16.0*k-4.0)*f*(f-1.0)-1.0);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			
			float MyCustomExpression16_g11670( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11671( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11674( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11673( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11679( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11680( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11721( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11676( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11732( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float4 texturePointSmooth( sampler2D tex, float4 textureTexelSize, float2 uvs )
			{
				float2 size;
				size.x = textureTexelSize.z;
				size.y = textureTexelSize.w;
				float2 pixel = float2(1.0,1.0) / size;
				uvs -= pixel * float2(0.5,0.5);
				float2 uv_pixels = uvs * size;
				float2 delta_pixel = frac(uv_pixels) - float2(0.5,0.5);
				float2 ddxy = fwidth(uv_pixels);
				float2 mip = log2(ddxy) - 0.5;
				float2 clampedUV = uvs + (clamp(delta_pixel / ddxy, 0.0, 1.0) - delta_pixel) * pixel;
				return tex2Dlod(tex, float4(clampedUV,0, min(mip.x, mip.y)));
			}
			
			float MyCustomExpression16_g11758( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11760( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11764( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 MyCustomExpression115_g11768( float3 In, float3 From, float3 To, float Fuzziness, float Range )
			{
				float Distance = distance(From, In);
				return lerp(To, In, saturate((Distance - Range) / max(Fuzziness, 0.001)));
			}
			
			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float MyCustomExpression16_g11787( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11774( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11798( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11805( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11838( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11835( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11837( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11828( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11830( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11823( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11825( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11826( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11819( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11817( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11818( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11813( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11841( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11845( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11843( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11852( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11860( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11862( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11858( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11854( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			
			float MyCustomExpression16_g11856( float linValue )
			{
				#ifdef UNITY_COLORSPACE_GAMMA
				return linValue;
				#else
				linValue = max(linValue, half3(0.h, 0.h, 0.h));
				return max(1.055h * pow(linValue, 0.416666667h) - 0.055h, 0.h);
				#endif
			}
			

			v2f VertexFunction (appdata v  ) {
				UNITY_SETUP_INSTANCE_ID(v);
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float2 _ZeroVector = float2(0,0);
				float2 texCoord363 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLESCREENTILING_ON
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				#ifdef _ENABLEWORLDTILING_ON
				float2 staticSwitch2_g11657 = ( ( ( (ase_worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _PIXELPERFECTUV_ON
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				#ifdef _SPRITESHEETFIX_ON
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				#ifdef _ENABLESQUISH_ON
				float2 break77_g11879 = fixedUV475;
				float2 appendResult72_g11879 = (float2(( _SquishStretch * ( break77_g11879.x - 0.5 ) * _SquishFade ) , ( _SquishFade * ( break77_g11879.y + _SquishFlip ) * -_SquishSquish )));
				float2 staticSwitch198 = ( appendResult72_g11879 + _ZeroVector );
				#else
				float2 staticSwitch198 = _ZeroVector;
				#endif
				float2 temp_output_2_0_g11880 = staticSwitch198;
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				float temp_output_8_0_g11880 = shaderTime237;
				#ifdef _ENABLESINEMOVE_ON
				float2 staticSwitch4_g11880 = ( ( sin( ( temp_output_8_0_g11880 * _SineMoveFrequency ) ) * _SineMoveOffset * _SineMoveFade ) + temp_output_2_0_g11880 );
				#else
				float2 staticSwitch4_g11880 = temp_output_2_0_g11880;
				#endif
				#ifdef _ENABLEVIBRATE_ON
				float temp_output_30_0_g11881 = temp_output_8_0_g11880;
				float3 rotatedValue21_g11881 = RotateAroundAxis( float3( 0,0,0 ), float3( 0,1,0 ), float3( 0,0,1 ), ( temp_output_30_0_g11881 * _VibrateRotation ) );
				float2 staticSwitch6_g11880 = ( ( sin( ( _VibrateFrequency * temp_output_30_0_g11881 ) ) * _VibrateOffset * _VibrateFade * (rotatedValue21_g11881).xy ) + staticSwitch4_g11880 );
				#else
				float2 staticSwitch6_g11880 = staticSwitch4_g11880;
				#endif
				#ifdef _ENABLESINESCALE_ON
				float2 staticSwitch10_g11880 = ( staticSwitch6_g11880 + ( (v.vertex.xyz).xy * ( ( ( sin( ( _SineScaleFrequency * temp_output_8_0_g11880 ) ) + 1.0 ) * 0.5 ) * _SineScaleFactor ) ) );
				#else
				float2 staticSwitch10_g11880 = staticSwitch6_g11880;
				#endif
				float2 temp_output_424_0 = staticSwitch10_g11880;
				float2 uv_FadingMask = v.ase_texcoord.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2Dlod( _FadingMask, float4( uv_FadingMask, 0, 0.0) );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float2 texCoord435 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				#ifdef _PIXELPERFECTSPACE_ON
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (v.vertex.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (v.vertex.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (ase_worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float linValue16_g11717 = tex2Dlod( _UberNoiseTexture, float4( ( shaderPosition235 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2Dlod( _UberNoiseTexture, float4( ( temp_output_27_0_g11718 * _FadingNoiseScale ), 0, 0.0) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult121 = lerp( float2( 0,0 ) , temp_output_424_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch142 = temp_output_424_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch142 = lerpResult121;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch142 = lerpResult121;
				#else
				float2 staticSwitch142 = temp_output_424_0;
				#endif
				
				o.ase_texcoord3 = screenPos;
				o.ase_texcoord4.xyz = ase_worldPos;
				
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord5 = v.vertex;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				o.ase_texcoord4.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = float3( staticSwitch142 ,  0.0 );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.vertex.w = 1;
				v.normal = v.normal;
				v.tangent = v.tangent;

				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( appdata v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.tangent = v.tangent;
				o.normal = v.normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				appdata o = (appdata) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
				o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			v2f vert ( appdata v )
			{
				return VertexFunction( v );
			}
			#endif

			fixed4 frag (v2f IN 
				#ifdef _DEPTHOFFSET_ON
				, out float outputDepth : SV_Depth
				#endif
				#if !defined( CAN_SKIP_VPOS )
				, UNITY_VPOS_TYPE vpos : VPOS
				#endif
				) : SV_Target 
			{
				UNITY_SETUP_INSTANCE_ID(IN);

				#ifdef LOD_FADE_CROSSFADE
					UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
				#endif

				#if defined(_SPECULAR_SETUP)
					SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
				#else
					SurfaceOutputStandard o = (SurfaceOutputStandard)0;
				#endif

				float2 texCoord363 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float4 screenPos = IN.ase_texcoord3;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				#ifdef _ENABLESCREENTILING_ON
				float2 appendResult16_g11656 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11656 = ( ( ( (( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / 10.0 ) )).xy * _ScreenTilingScale ) + _ScreenTilingOffset ) * ( _ScreenTilingPixelsPerUnit * appendResult16_g11656 ) );
				#else
				float2 staticSwitch2_g11656 = texCoord363;
				#endif
				float3 ase_worldPos = IN.ase_texcoord4.xyz;
				#ifdef _ENABLEWORLDTILING_ON
				float2 appendResult16_g11657 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 staticSwitch2_g11657 = ( ( ( (ase_worldPos).xy * _WorldTilingScale ) + _WorldTilingOffset ) * ( _WorldTilingPixelsPerUnit * appendResult16_g11657 ) );
				#else
				float2 staticSwitch2_g11657 = staticSwitch2_g11656;
				#endif
				float2 originalUV460 = staticSwitch2_g11657;
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11658 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch449 = ( floor( ( originalUV460 * appendResult7_g11658 ) ) / appendResult7_g11658 );
				#else
				float2 staticSwitch449 = originalUV460;
				#endif
				float2 uvAfterPixelArt450 = staticSwitch449;
				float2 break14_g11664 = uvAfterPixelArt450;
				float2 appendResult374 = (float2(_SpriteSheetRect.x , _SpriteSheetRect.y));
				float2 spriteRectMin376 = appendResult374;
				float2 break11_g11664 = spriteRectMin376;
				float2 appendResult375 = (float2(_SpriteSheetRect.z , _SpriteSheetRect.w));
				float2 spriteRectMax377 = appendResult375;
				#ifdef _SPRITESHEETFIX_ON
				float2 break10_g11664 = spriteRectMax377;
				float2 break9_g11664 = float2( 0,0 );
				float2 break8_g11664 = float2( 1,1 );
				float2 appendResult15_g11664 = (float2((break9_g11664.x + (break14_g11664.x - break11_g11664.x) * (break8_g11664.x - break9_g11664.x) / (break10_g11664.x - break11_g11664.x)) , (break9_g11664.y + (break14_g11664.y - break11_g11664.y) * (break8_g11664.y - break9_g11664.y) / (break10_g11664.y - break11_g11664.y))));
				float2 staticSwitch366 = appendResult15_g11664;
				#else
				float2 staticSwitch366 = uvAfterPixelArt450;
				#endif
				float2 fixedUV475 = staticSwitch366;
				float2 temp_output_3_0_g11667 = fixedUV475;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch117_g11668 = _WindMinIntensity;
				#else
				float staticSwitch117_g11668 = WindMinIntensity;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch118_g11668 = _WindMaxIntensity;
				#else
				float staticSwitch118_g11668 = WindMaxIntensity;
				#endif
				float4 transform62_g11668 = mul(unity_WorldToObject,float4( 0,0,0,1 ));
				#ifdef _WINDISPARALLAX_ON
				float staticSwitch111_g11668 = _WindXPosition;
				#else
				float staticSwitch111_g11668 = transform62_g11668.x;
				#endif
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch113_g11668 = _WindNoiseScale;
				#else
				float staticSwitch113_g11668 = WindNoiseScale;
				#endif
				#ifdef _TOGGLECUSTOMTIME_ON
				float staticSwitch44_g11665 = _TimeValue;
				#else
				float staticSwitch44_g11665 = _Time.y;
				#endif
				#ifdef _TOGGLEUNSCALEDTIME_ON
				float staticSwitch34_g11665 = UnscaledTime;
				#else
				float staticSwitch34_g11665 = staticSwitch44_g11665;
				#endif
				#ifdef _TOGGLETIMESPEED_ON
				float staticSwitch37_g11665 = ( staticSwitch34_g11665 * _TimeSpeed );
				#else
				float staticSwitch37_g11665 = staticSwitch34_g11665;
				#endif
				#ifdef _TOGGLETIMEFPS_ON
				float staticSwitch38_g11665 = ( floor( ( staticSwitch37_g11665 * _TimeFPS ) ) / _TimeFPS );
				#else
				float staticSwitch38_g11665 = staticSwitch37_g11665;
				#endif
				#ifdef _TOGGLETIMEFREQUENCY_ON
				float staticSwitch42_g11665 = ( ( sin( ( staticSwitch38_g11665 * _TimeFrequency ) ) * _TimeRange ) + 100.0 );
				#else
				float staticSwitch42_g11665 = staticSwitch38_g11665;
				#endif
				float shaderTime237 = staticSwitch42_g11665;
				#ifdef _WINDLOCALWIND_ON
				float staticSwitch125_g11668 = ( shaderTime237 * _WindNoiseSpeed );
				#else
				float staticSwitch125_g11668 = WindTime;
				#endif
				float temp_output_50_0_g11668 = ( ( staticSwitch111_g11668 * staticSwitch113_g11668 ) + staticSwitch125_g11668 );
				float x101_g11668 = temp_output_50_0_g11668;
				float localFastNoise101_g11668 = FastNoise101_g11668( x101_g11668 );
				float2 temp_cast_0 = (temp_output_50_0_g11668).xx;
				float simplePerlin2D121_g11668 = snoise( temp_cast_0*0.5 );
				simplePerlin2D121_g11668 = simplePerlin2D121_g11668*0.5 + 0.5;
				#ifdef _WINDHIGHQUALITYNOISE_ON
				float staticSwitch123_g11668 = simplePerlin2D121_g11668;
				#else
				float staticSwitch123_g11668 = ( localFastNoise101_g11668 + 0.5 );
				#endif
				#ifdef _ENABLEWIND_ON
				float lerpResult86_g11668 = lerp( staticSwitch117_g11668 , staticSwitch118_g11668 , staticSwitch123_g11668);
				float clampResult29_g11668 = clamp( ( ( _WindRotationWindFactor * lerpResult86_g11668 ) + _WindRotation ) , -_WindMaxRotation , _WindMaxRotation );
				float2 temp_output_1_0_g11668 = temp_output_3_0_g11667;
				float temp_output_39_0_g11668 = ( temp_output_1_0_g11668.y + _WindFlip );
				float3 appendResult43_g11668 = (float3(0.5 , -_WindFlip , 0.0));
				float2 appendResult27_g11668 = (float2(0.0 , ( _WindSquishFactor * min( ( ( _WindSquishWindFactor * abs( lerpResult86_g11668 ) ) + abs( _WindRotation ) ) , _WindMaxRotation ) * temp_output_39_0_g11668 )));
				float3 rotatedValue19_g11668 = RotateAroundAxis( appendResult43_g11668, float3( ( appendResult27_g11668 + temp_output_1_0_g11668 ) ,  0.0 ), float3( 0,0,1 ), ( clampResult29_g11668 * temp_output_39_0_g11668 ) );
				float2 staticSwitch4_g11667 = (rotatedValue19_g11668).xy;
				#else
				float2 staticSwitch4_g11667 = temp_output_3_0_g11667;
				#endif
				float2 texCoord435 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _PIXELPERFECTSPACE_ON
				float2 temp_output_432_0 = (_MainTex_TexelSize).zw;
				float2 staticSwitch437 = ( floor( ( texCoord435 * temp_output_432_0 ) ) / temp_output_432_0 );
				#else
				float2 staticSwitch437 = texCoord435;
				#endif
				float2 temp_output_61_0_g11666 = staticSwitch437;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float2 texCoord23_g11666 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult28_g11666 = (float2(_RectWidth , _RectHeight));
				#if defined(_SHADERSPACE_UV)
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#elif defined(_SHADERSPACE_UV_RAW)
				float2 staticSwitch1_g11666 = temp_output_61_0_g11666;
				#elif defined(_SHADERSPACE_OBJECT)
				float2 staticSwitch1_g11666 = (IN.ase_texcoord5.xyz).xy;
				#elif defined(_SHADERSPACE_OBJECT_SCALED)
				float2 staticSwitch1_g11666 = ( (IN.ase_texcoord5.xyz).xy * (ase_objectScale).xy );
				#elif defined(_SHADERSPACE_WORLD)
				float2 staticSwitch1_g11666 = (ase_worldPos).xy;
				#elif defined(_SHADERSPACE_UI_GRAPHIC)
				float2 staticSwitch1_g11666 = ( texCoord23_g11666 * ( appendResult28_g11666 / _PixelsPerUnit ) );
				#elif defined(_SHADERSPACE_SCREEN)
				float2 staticSwitch1_g11666 = ( ( (ase_screenPosNorm).xy * (_ScreenParams).xy ) / ( _ScreenParams.x / _ScreenWidthUnits ) );
				#else
				float2 staticSwitch1_g11666 = ( temp_output_61_0_g11666 / ( _PixelsPerUnit * (_MainTex_TexelSize).xy ) );
				#endif
				float2 shaderPosition235 = staticSwitch1_g11666;
				float2 temp_output_195_0_g11669 = shaderPosition235;
				float linValue16_g11670 = tex2D( _UberNoiseTexture, ( temp_output_195_0_g11669 * _FullDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11670 = MyCustomExpression16_g11670( linValue16_g11670 );
				float linValue16_g11671 = tex2D( _UberNoiseTexture, ( ( temp_output_195_0_g11669 + float2( 0.321,0.321 ) ) * _FullDistortionNoiseScale ) ).r;
				#ifdef _ENABLEFULLDISTORTION_ON
				float localMyCustomExpression16_g11671 = MyCustomExpression16_g11671( linValue16_g11671 );
				float2 appendResult189_g11669 = (float2(( localMyCustomExpression16_g11670 - 0.5 ) , ( localMyCustomExpression16_g11671 - 0.5 )));
				float2 staticSwitch83 = ( staticSwitch4_g11667 + ( ( 1.0 - _FullDistortionFade ) * appendResult189_g11669 * _FullDistortionDistortion ) );
				#else
				float2 staticSwitch83 = staticSwitch4_g11667;
				#endif
				float2 temp_output_182_0_g11672 = shaderPosition235;
				float linValue16_g11674 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionDistortionScale ) ).r;
				float localMyCustomExpression16_g11674 = MyCustomExpression16_g11674( linValue16_g11674 );
				float3 rotatedValue168_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( _DirectionalDistortionDistortion ,  0.0 ), float3( 0,0,1 ), ( ( ( localMyCustomExpression16_g11674 - 0.5 ) * 2.0 * _DirectionalDistortionRandomDirection ) * UNITY_PI ) );
				float3 rotatedValue136_g11672 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_182_0_g11672 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalDistortionRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11672 = rotatedValue136_g11672;
				float linValue16_g11673 = tex2D( _UberNoiseTexture, ( temp_output_182_0_g11672 * _DirectionalDistortionNoiseScale ) ).r;
				float localMyCustomExpression16_g11673 = MyCustomExpression16_g11673( linValue16_g11673 );
				float clampResult154_g11672 = clamp( ( ( break130_g11672.x + break130_g11672.y + _DirectionalDistortionFade + ( localMyCustomExpression16_g11673 * _DirectionalDistortionNoiseFactor ) ) / max( _DirectionalDistortionWidth , 0.001 ) ) , 0.0 , 1.0 );
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float2 staticSwitch82 = ( staticSwitch83 + ( (rotatedValue168_g11672).xy * ( 1.0 - (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 )) ) ) );
				#else
				float2 staticSwitch82 = staticSwitch83;
				#endif
				float temp_output_8_0_g11677 = ( ( ( shaderTime237 * _HologramDistortionSpeed ) + ase_worldPos.y ) / unity_OrthoParams.y );
				float2 temp_cast_4 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_5 = (_HologramDistortionDensity).xx;
				float linValue16_g11679 = tex2D( _UberNoiseTexture, ( temp_cast_4 * temp_cast_5 ) ).r;
				float localMyCustomExpression16_g11679 = MyCustomExpression16_g11679( linValue16_g11679 );
				float clampResult75_g11677 = clamp( localMyCustomExpression16_g11679 , 0.075 , 0.6 );
				float2 temp_cast_6 = (temp_output_8_0_g11677).xx;
				float2 temp_cast_7 = (_HologramDistortionScale).xx;
				float linValue16_g11680 = tex2D( _UberNoiseTexture, ( temp_cast_6 * temp_cast_7 ) ).r;
				float localMyCustomExpression16_g11680 = MyCustomExpression16_g11680( linValue16_g11680 );
				float2 appendResult2_g11678 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float hologramFade182 = _HologramFade;
				#ifdef _ENABLEHOLOGRAM_ON
				float2 appendResult44_g11677 = (float2(( ( ( clampResult75_g11677 * ( localMyCustomExpression16_g11680 - 0.5 ) ) * _HologramDistortionOffset * ( 100.0 / appendResult2_g11678 ).x ) * hologramFade182 ) , 0.0));
				float2 staticSwitch59 = ( staticSwitch82 + appendResult44_g11677 );
				#else
				float2 staticSwitch59 = staticSwitch82;
				#endif
				float2 temp_output_18_0_g11675 = shaderPosition235;
				float2 glitchPosition154 = temp_output_18_0_g11675;
				float linValue16_g11721 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchDistortionSpeed * shaderTime237 ) ) * _GlitchDistortionScale ) ).r;
				float localMyCustomExpression16_g11721 = MyCustomExpression16_g11721( linValue16_g11721 );
				float linValue16_g11676 = tex2D( _UberNoiseTexture, ( ( temp_output_18_0_g11675 + ( _GlitchMaskSpeed * shaderTime237 ) ) * _GlitchMaskScale ) ).r;
				float localMyCustomExpression16_g11676 = MyCustomExpression16_g11676( linValue16_g11676 );
				float glitchFade152 = ( max( localMyCustomExpression16_g11676 , _GlitchMaskMin ) * _GlitchFade );
				#ifdef _ENABLEGLITCH_ON
				float2 staticSwitch62 = ( staticSwitch59 + ( ( localMyCustomExpression16_g11721 - 0.5 ) * _GlitchDistortion * glitchFade152 ) );
				#else
				float2 staticSwitch62 = staticSwitch59;
				#endif
				float2 temp_output_1_0_g11722 = staticSwitch62;
				float2 temp_output_26_0_g11722 = shaderPosition235;
				float temp_output_25_0_g11722 = shaderTime237;
				float linValue16_g11732 = tex2D( _UberNoiseTexture, ( ( temp_output_26_0_g11722 + ( _UVDistortSpeed * temp_output_25_0_g11722 ) ) * _UVDistortNoiseScale ) ).r;
				float localMyCustomExpression16_g11732 = MyCustomExpression16_g11732( linValue16_g11732 );
				float2 lerpResult21_g11729 = lerp( _UVDistortFrom , _UVDistortTo , localMyCustomExpression16_g11732);
				float2 appendResult2_g11731 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				#ifdef _UVDISTORTMASKTOGGLE_ON
				float2 uv_UVDistortMask = IN.ase_texcoord2.xy * _UVDistortMask_ST.xy + _UVDistortMask_ST.zw;
				float4 tex2DNode3_g11730 = tex2D( _UVDistortMask, uv_UVDistortMask );
				float staticSwitch29_g11729 = ( _UVDistortFade * ( tex2DNode3_g11730.r * tex2DNode3_g11730.a ) );
				#else
				float staticSwitch29_g11729 = _UVDistortFade;
				#endif
				#ifdef _ENABLEUVDISTORT_ON
				float2 staticSwitch5_g11722 = ( temp_output_1_0_g11722 + ( lerpResult21_g11729 * ( 100.0 / appendResult2_g11731 ) * staticSwitch29_g11729 ) );
				#else
				float2 staticSwitch5_g11722 = temp_output_1_0_g11722;
				#endif
				#ifdef _ENABLESQUEEZE_ON
				float2 temp_output_1_0_g11728 = staticSwitch5_g11722;
				float2 staticSwitch7_g11722 = ( temp_output_1_0_g11728 + ( ( temp_output_1_0_g11728 - _SqueezeCenter ) * pow( distance( temp_output_1_0_g11728 , _SqueezeCenter ) , _SqueezePower ) * _SqueezeScale * _SqueezeFade ) );
				#else
				float2 staticSwitch7_g11722 = staticSwitch5_g11722;
				#endif
				#ifdef _ENABLESINEROTATE_ON
				float3 rotatedValue36_g11727 = RotateAroundAxis( float3( _SineRotatePivot ,  0.0 ), float3( staticSwitch7_g11722 ,  0.0 ), float3( 0,0,1 ), ( sin( ( temp_output_25_0_g11722 * _SineRotateFrequency ) ) * ( ( _SineRotateAngle / 360.0 ) * UNITY_PI ) * _SineRotateFade ) );
				float2 staticSwitch9_g11722 = (rotatedValue36_g11727).xy;
				#else
				float2 staticSwitch9_g11722 = staticSwitch7_g11722;
				#endif
				#ifdef _ENABLEUVROTATE_ON
				float3 rotatedValue8_g11726 = RotateAroundAxis( float3( _UVRotatePivot ,  0.0 ), float3( staticSwitch9_g11722 ,  0.0 ), float3( 0,0,1 ), ( temp_output_25_0_g11722 * _UVRotateSpeed * UNITY_PI ) );
				float2 staticSwitch16_g11722 = (rotatedValue8_g11726).xy;
				#else
				float2 staticSwitch16_g11722 = staticSwitch9_g11722;
				#endif
				#ifdef _ENABLEUVSCROLL_ON
				float2 staticSwitch14_g11722 = ( ( _UVScrollSpeed * temp_output_25_0_g11722 ) + staticSwitch16_g11722 );
				#else
				float2 staticSwitch14_g11722 = staticSwitch16_g11722;
				#endif
				#ifdef _ENABLEPIXELATE_ON
				float2 appendResult35_g11724 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 MultFactor30_g11724 = ( ( _PixelatePixelDensity * ( appendResult35_g11724 / _PixelatePixelsPerUnit ) ) * ( 1.0 / max( _PixelateFade , 1E-05 ) ) );
				float2 clampResult46_g11724 = clamp( ( floor( ( MultFactor30_g11724 * ( staticSwitch14_g11722 + ( float2( 0.5,0.5 ) / MultFactor30_g11724 ) ) ) ) / MultFactor30_g11724 ) , float2( 0,0 ) , float2( 1,1 ) );
				float2 staticSwitch4_g11722 = clampResult46_g11724;
				#else
				float2 staticSwitch4_g11722 = staticSwitch14_g11722;
				#endif
				#ifdef _ENABLEUVSCALE_ON
				float2 staticSwitch24_g11722 = ( ( ( staticSwitch4_g11722 - _UVScalePivot ) / _UVScaleScale ) + _UVScalePivot );
				#else
				float2 staticSwitch24_g11722 = staticSwitch4_g11722;
				#endif
				float2 temp_output_1_0_g11733 = staticSwitch24_g11722;
				float temp_output_7_0_g11733 = ( sin( ( _WiggleFrequency * ( temp_output_26_0_g11722.y + ( _WiggleSpeed * temp_output_25_0_g11722 ) ) ) ) * _WiggleOffset * _WiggleFade );
				#ifdef _WIGGLEFIXEDGROUNDTOGGLE_ON
				float staticSwitch18_g11733 = ( temp_output_7_0_g11733 * temp_output_1_0_g11733.y );
				#else
				float staticSwitch18_g11733 = temp_output_7_0_g11733;
				#endif
				#ifdef _ENABLEWIGGLE_ON
				float2 appendResult12_g11733 = (float2(staticSwitch18_g11733 , 0.0));
				float2 staticSwitch13_g11733 = ( temp_output_1_0_g11733 + appendResult12_g11733 );
				#else
				float2 staticSwitch13_g11733 = temp_output_1_0_g11733;
				#endif
				float2 temp_output_484_0 = staticSwitch13_g11733;
				float2 texCoord131 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_FadingMask = IN.ase_texcoord2.xy * _FadingMask_ST.xy + _FadingMask_ST.zw;
				float4 tex2DNode3_g11715 = tex2D( _FadingMask, uv_FadingMask );
				float temp_output_4_0_g11716 = max( _FadingWidth , 0.001 );
				float linValue16_g11717 = tex2D( _UberNoiseTexture, ( shaderPosition235 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11717 = MyCustomExpression16_g11717( linValue16_g11717 );
				float clampResult14_g11716 = clamp( ( ( ( _FadingFade * ( 1.0 + temp_output_4_0_g11716 ) ) - localMyCustomExpression16_g11717 ) / temp_output_4_0_g11716 ) , 0.0 , 1.0 );
				float2 temp_output_27_0_g11718 = shaderPosition235;
				float linValue16_g11719 = tex2D( _UberNoiseTexture, ( temp_output_27_0_g11718 * _FadingNoiseScale ) ).r;
				float localMyCustomExpression16_g11719 = MyCustomExpression16_g11719( linValue16_g11719 );
				float clampResult3_g11718 = clamp( ( ( _FadingFade - ( distance( _FadingPosition , temp_output_27_0_g11718 ) + ( localMyCustomExpression16_g11719 * _FadingNoiseFactor ) ) ) / max( _FadingWidth , 0.001 ) ) , 0.0 , 1.0 );
				#if defined(_SHADERFADING_NONE)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_FULL)
				float staticSwitch139 = _FadingFade;
				#elif defined(_SHADERFADING_MASK)
				float staticSwitch139 = ( _FadingFade * ( tex2DNode3_g11715.r * tex2DNode3_g11715.a ) );
				#elif defined(_SHADERFADING_DISSOLVE)
				float staticSwitch139 = clampResult14_g11716;
				#elif defined(_SHADERFADING_SPREAD)
				float staticSwitch139 = clampResult3_g11718;
				#else
				float staticSwitch139 = _FadingFade;
				#endif
				float fullFade123 = staticSwitch139;
				float2 lerpResult130 = lerp( texCoord131 , temp_output_484_0 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float2 staticSwitch145 = temp_output_484_0;
				#elif defined(_SHADERFADING_FULL)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_MASK)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_DISSOLVE)
				float2 staticSwitch145 = lerpResult130;
				#elif defined(_SHADERFADING_SPREAD)
				float2 staticSwitch145 = lerpResult130;
				#else
				float2 staticSwitch145 = temp_output_484_0;
				#endif
				#ifdef _TILINGFIX_ON
				float2 staticSwitch485 = ( ( ( staticSwitch145 % float2( 1,1 ) ) + float2( 1,1 ) ) % float2( 1,1 ) );
				#else
				float2 staticSwitch485 = staticSwitch145;
				#endif
				#ifdef _SPRITESHEETFIX_ON
				float2 break14_g11734 = staticSwitch485;
				float2 break11_g11734 = float2( 0,0 );
				float2 break10_g11734 = float2( 1,1 );
				float2 break9_g11734 = spriteRectMin376;
				float2 break8_g11734 = spriteRectMax377;
				float2 appendResult15_g11734 = (float2((break9_g11734.x + (break14_g11734.x - break11_g11734.x) * (break8_g11734.x - break9_g11734.x) / (break10_g11734.x - break11_g11734.x)) , (break9_g11734.y + (break14_g11734.y - break11_g11734.y) * (break8_g11734.y - break9_g11734.y) / (break10_g11734.y - break11_g11734.y))));
				float2 staticSwitch371 = min( max( appendResult15_g11734 , spriteRectMin376 ) , spriteRectMax377 );
				#else
				float2 staticSwitch371 = staticSwitch485;
				#endif
				#ifdef _PIXELPERFECTUV_ON
				float2 appendResult7_g11735 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 staticSwitch427 = ( originalUV460 + ( floor( ( ( staticSwitch371 - uvAfterPixelArt450 ) * appendResult7_g11735 ) ) / appendResult7_g11735 ) );
				#else
				float2 staticSwitch427 = staticSwitch371;
				#endif
				float2 finalUV146 = staticSwitch427;
				float2 temp_output_1_0_g11736 = finalUV146;
				#ifdef _ENABLESMOOTHPIXELART_ON
				sampler2D tex3_g11737 = _MainTex;
				float4 textureTexelSize3_g11737 = _MainTex_TexelSize;
				float2 uvs3_g11737 = temp_output_1_0_g11736;
				float4 localtexturePointSmooth3_g11737 = texturePointSmooth( tex3_g11737 , textureTexelSize3_g11737 , uvs3_g11737 );
				float4 staticSwitch8_g11736 = localtexturePointSmooth3_g11737;
				#else
				float4 staticSwitch8_g11736 = tex2D( _MainTex, temp_output_1_0_g11736 );
				#endif
				#ifdef _ENABLEGAUSSIANBLUR_ON
				float temp_output_10_0_g11738 = ( _GaussianBlurOffset * _GaussianBlurFade * 0.005 );
				float temp_output_2_0_g11748 = temp_output_10_0_g11738;
				float2 appendResult16_g11748 = (float2(temp_output_2_0_g11748 , 0.0));
				float2 appendResult25_g11750 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11750 = ( appendResult16_g11748 * appendResult25_g11750 );
				float2 temp_output_7_0_g11738 = temp_output_1_0_g11736;
				float2 temp_output_1_0_g11748 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,1 ) ) );
				float2 temp_output_1_0_g11750 = temp_output_1_0_g11748;
				float2 appendResult17_g11748 = (float2(0.0 , temp_output_2_0_g11748));
				float2 appendResult25_g11749 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11749 = ( appendResult17_g11748 * appendResult25_g11749 );
				float2 temp_output_1_0_g11749 = temp_output_1_0_g11748;
				float temp_output_2_0_g11739 = temp_output_10_0_g11738;
				float2 appendResult16_g11739 = (float2(temp_output_2_0_g11739 , 0.0));
				float2 appendResult25_g11741 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11741 = ( appendResult16_g11739 * appendResult25_g11741 );
				float2 temp_output_1_0_g11739 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,1 ) ) );
				float2 temp_output_1_0_g11741 = temp_output_1_0_g11739;
				float2 appendResult17_g11739 = (float2(0.0 , temp_output_2_0_g11739));
				float2 appendResult25_g11740 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11740 = ( appendResult17_g11739 * appendResult25_g11740 );
				float2 temp_output_1_0_g11740 = temp_output_1_0_g11739;
				float temp_output_2_0_g11745 = temp_output_10_0_g11738;
				float2 appendResult16_g11745 = (float2(temp_output_2_0_g11745 , 0.0));
				float2 appendResult25_g11747 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11747 = ( appendResult16_g11745 * appendResult25_g11747 );
				float2 temp_output_1_0_g11745 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( -1,-1 ) ) );
				float2 temp_output_1_0_g11747 = temp_output_1_0_g11745;
				float2 appendResult17_g11745 = (float2(0.0 , temp_output_2_0_g11745));
				float2 appendResult25_g11746 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11746 = ( appendResult17_g11745 * appendResult25_g11746 );
				float2 temp_output_1_0_g11746 = temp_output_1_0_g11745;
				float temp_output_2_0_g11742 = temp_output_10_0_g11738;
				float2 appendResult16_g11742 = (float2(temp_output_2_0_g11742 , 0.0));
				float2 appendResult25_g11744 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11744 = ( appendResult16_g11742 * appendResult25_g11744 );
				float2 temp_output_1_0_g11742 = ( temp_output_7_0_g11738 + ( temp_output_10_0_g11738 * float2( 1,-1 ) ) );
				float2 temp_output_1_0_g11744 = temp_output_1_0_g11742;
				float2 appendResult17_g11742 = (float2(0.0 , temp_output_2_0_g11742));
				float2 appendResult25_g11743 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11743 = ( appendResult17_g11742 * appendResult25_g11743 );
				float2 temp_output_1_0_g11743 = temp_output_1_0_g11742;
				float4 staticSwitch3_g11736 = ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11750 + temp_output_1_0_g11750 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11749 + temp_output_1_0_g11749 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11741 + temp_output_1_0_g11741 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11740 + temp_output_1_0_g11740 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11747 + temp_output_1_0_g11747 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11746 + temp_output_1_0_g11746 ) ) ) ) + ( ( tex2D( _MainTex, ( temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11744 + temp_output_1_0_g11744 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11743 + temp_output_1_0_g11743 ) ) ) ) ) * 0.0625 );
				#else
				float4 staticSwitch3_g11736 = staticSwitch8_g11736;
				#endif
				#ifdef _ENABLESHARPEN_ON
				float2 temp_output_1_0_g11751 = temp_output_1_0_g11736;
				float4 tex2DNode4_g11751 = tex2D( _MainTex, temp_output_1_0_g11751 );
				float temp_output_2_0_g11752 = _SharpenOffset;
				float2 appendResult16_g11752 = (float2(temp_output_2_0_g11752 , 0.0));
				float2 appendResult25_g11754 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11754 = ( appendResult16_g11752 * appendResult25_g11754 );
				float2 temp_output_1_0_g11752 = temp_output_1_0_g11751;
				float2 temp_output_1_0_g11754 = temp_output_1_0_g11752;
				float2 appendResult17_g11752 = (float2(0.0 , temp_output_2_0_g11752));
				float2 appendResult25_g11753 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 temp_output_26_0_g11753 = ( appendResult17_g11752 * appendResult25_g11753 );
				float2 temp_output_1_0_g11753 = temp_output_1_0_g11752;
				float4 break22_g11751 = ( tex2DNode4_g11751 - ( ( ( ( ( tex2D( _MainTex, ( temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11754 + temp_output_1_0_g11754 ) ) ) + ( tex2D( _MainTex, ( temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) + tex2D( _MainTex, ( -temp_output_26_0_g11753 + temp_output_1_0_g11753 ) ) ) ) / 4.0 ) - tex2DNode4_g11751 ) * ( _SharpenFactor * _SharpenFade ) ) );
				float clampResult23_g11751 = clamp( break22_g11751.a , 0.0 , 1.0 );
				float4 appendResult24_g11751 = (float4(break22_g11751.r , break22_g11751.g , break22_g11751.b , clampResult23_g11751));
				float4 staticSwitch12_g11736 = appendResult24_g11751;
				#else
				float4 staticSwitch12_g11736 = staticSwitch3_g11736;
				#endif
				float4 temp_output_471_0 = staticSwitch12_g11736;
				#ifdef _VERTEXTINTFIRST_ON
				float4 temp_output_1_0_g11755 = temp_output_471_0;
				float4 appendResult8_g11755 = (float4(( (temp_output_1_0_g11755).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11755.a));
				float4 staticSwitch354 = appendResult8_g11755;
				#else
				float4 staticSwitch354 = temp_output_471_0;
				#endif
				float4 originalColor191 = staticSwitch354;
				float4 temp_output_1_0_g11756 = originalColor191;
				float4 temp_output_1_0_g11757 = temp_output_1_0_g11756;
				float2 temp_output_7_0_g11756 = finalUV146;
				float2 temp_output_43_0_g11757 = temp_output_7_0_g11756;
				float2 temp_cast_15 = (_SmokeNoiseScale).xx;
				float linValue16_g11758 = tex2D( _UberNoiseTexture, ( ( ( IN.ase_color.r * (( _SmokeVertexSeed )?( 5.0 ):( 0.0 )) ) + temp_output_43_0_g11757 ) * temp_cast_15 ) ).r;
				float localMyCustomExpression16_g11758 = MyCustomExpression16_g11758( linValue16_g11758 );
				float clampResult28_g11757 = clamp( ( ( ( localMyCustomExpression16_g11758 - 1.0 ) * _SmokeNoiseFactor ) + ( ( ( IN.ase_color.a / 2.5 ) - distance( temp_output_43_0_g11757 , float2( 0.5,0.5 ) ) ) * 2.5 * _SmokeSmoothness ) ) , 0.0 , 1.0 );
				#ifdef _ENABLESMOKE_ON
				float3 lerpResult34_g11757 = lerp( (temp_output_1_0_g11757).rgb , float3( 0,0,0 ) , ( ( 1.0 - clampResult28_g11757 ) * _SmokeDarkEdge ));
				float4 appendResult31_g11757 = (float4(lerpResult34_g11757 , ( clampResult28_g11757 * _SmokeAlpha * temp_output_1_0_g11757.a )));
				float4 staticSwitch2_g11756 = appendResult31_g11757;
				#else
				float4 staticSwitch2_g11756 = temp_output_1_0_g11756;
				#endif
				#ifdef _ENABLECUSTOMFADE_ON
				float4 temp_output_1_0_g11759 = staticSwitch2_g11756;
				float2 temp_output_57_0_g11759 = temp_output_7_0_g11756;
				float4 tex2DNode3_g11759 = tex2D( _CustomFadeFadeMask, temp_output_57_0_g11759 );
				float linValue16_g11760 = tex2D( _UberNoiseTexture, ( temp_output_57_0_g11759 * _CustomFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11760 = MyCustomExpression16_g11760( linValue16_g11760 );
				float clampResult37_g11759 = clamp( ( ( ( IN.ase_color.a * 2.0 ) - 1.0 ) + ( tex2DNode3_g11759.r + ( localMyCustomExpression16_g11760 * _CustomFadeNoiseFactor ) ) ) , 0.0 , 1.0 );
				float4 appendResult13_g11759 = (float4((temp_output_1_0_g11759).rgb , ( temp_output_1_0_g11759.a * pow( clampResult37_g11759 , ( _CustomFadeSmoothness / max( tex2DNode3_g11759.r , 0.05 ) ) ) * _CustomFadeAlpha )));
				float4 staticSwitch3_g11756 = appendResult13_g11759;
				#else
				float4 staticSwitch3_g11756 = staticSwitch2_g11756;
				#endif
				float4 temp_output_1_0_g11761 = staticSwitch3_g11756;
				#ifdef _ENABLECHECKERBOARD_ON
				float4 temp_output_1_0_g11762 = temp_output_1_0_g11761;
				float2 appendResult4_g11762 = (float2(ase_worldPos.x , ase_worldPos.y));
				float2 temp_output_44_0_g11762 = ( appendResult4_g11762 * _CheckerboardTiling * 0.5 );
				float2 break12_g11762 = step( ( ceil( temp_output_44_0_g11762 ) - temp_output_44_0_g11762 ) , float2( 0.5,0.5 ) );
				float4 appendResult42_g11762 = (float4(( (temp_output_1_0_g11762).rgb * min( ( _CheckerboardDarken + abs( ( -break12_g11762.x + break12_g11762.y ) ) ) , 1.0 ) ) , temp_output_1_0_g11762.a));
				float4 staticSwitch2_g11761 = appendResult42_g11762;
				#else
				float4 staticSwitch2_g11761 = temp_output_1_0_g11761;
				#endif
				float2 temp_output_75_0_g11763 = finalUV146;
				float linValue16_g11764 = tex2D( _UberNoiseTexture, ( ( ( shaderTime237 * _FlameSpeed ) + temp_output_75_0_g11763 ) * _FlameNoiseScale ) ).r;
				float localMyCustomExpression16_g11764 = MyCustomExpression16_g11764( linValue16_g11764 );
				float saferPower57_g11763 = abs( max( ( temp_output_75_0_g11763.y - 0.2 ) , 0.0 ) );
				float temp_output_47_0_g11763 = max( _FlameRadius , 0.01 );
				float clampResult70_g11763 = clamp( ( ( ( localMyCustomExpression16_g11764 * pow( saferPower57_g11763 , _FlameNoiseHeightFactor ) * _FlameNoiseFactor ) + ( ( temp_output_47_0_g11763 - distance( temp_output_75_0_g11763 , float2( 0.5,0.4 ) ) ) / temp_output_47_0_g11763 ) ) * _FlameSmooth ) , 0.0 , 1.0 );
				#ifdef _ENABLEFLAME_ON
				float temp_output_63_0_g11763 = ( clampResult70_g11763 * _FlameBrightness );
				float4 appendResult31_g11763 = (float4(temp_output_63_0_g11763 , temp_output_63_0_g11763 , temp_output_63_0_g11763 , clampResult70_g11763));
				float4 staticSwitch6_g11761 = ( appendResult31_g11763 * staticSwitch2_g11761 );
				#else
				float4 staticSwitch6_g11761 = staticSwitch2_g11761;
				#endif
				float4 temp_output_3_0_g11765 = staticSwitch6_g11761;
				float4 temp_output_1_0_g11792 = temp_output_3_0_g11765;
				float2 temp_output_1_0_g11765 = finalUV146;
				#ifdef _RECOLORRGBTEXTURETOGGLE_ON
				float4 staticSwitch81_g11792 = tex2D( _RecolorRGBTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch81_g11792 = temp_output_1_0_g11792;
				#endif
				#ifdef _ENABLERECOLORRGB_ON
				float4 break82_g11792 = staticSwitch81_g11792;
				float temp_output_63_0_g11792 = ( break82_g11792.r + break82_g11792.g + break82_g11792.b );
				float4 break71_g11792 = ( ( _RecolorRGBRedTint * ( break82_g11792.r / temp_output_63_0_g11792 ) ) + ( _RecolorRGBGreenTint * ( break82_g11792.g / temp_output_63_0_g11792 ) ) + ( ( break82_g11792.b / temp_output_63_0_g11792 ) * _RecolorRGBBlueTint ) );
				float3 appendResult56_g11792 = (float3(break71_g11792.r , break71_g11792.g , break71_g11792.b));
				float4 break2_g11793 = temp_output_1_0_g11792;
				float saferPower57_g11792 = abs( ( ( break2_g11793.x + break2_g11793.x + break2_g11793.y + break2_g11793.y + break2_g11793.y + break2_g11793.z ) / 6.0 ) );
				float3 lerpResult26_g11792 = lerp( (temp_output_1_0_g11792).rgb , ( appendResult56_g11792 * pow( saferPower57_g11792 , ( max( break71_g11792.a , 0.01 ) * 2.0 ) ) ) , ( min( ( temp_output_63_0_g11792 * 2.0 ) , 1.0 ) * _RecolorRGBFade ));
				float4 appendResult30_g11792 = (float4(lerpResult26_g11792 , temp_output_1_0_g11792.a));
				float4 staticSwitch43_g11765 = appendResult30_g11792;
				#else
				float4 staticSwitch43_g11765 = temp_output_3_0_g11765;
				#endif
				float4 temp_output_1_0_g11790 = staticSwitch43_g11765;
				#ifdef _RECOLORRGBYCPTEXTURETOGGLE_ON
				float4 staticSwitch62_g11790 = tex2D( _RecolorRGBYCPTexture, temp_output_1_0_g11765 );
				#else
				float4 staticSwitch62_g11790 = temp_output_1_0_g11790;
				#endif
				float3 hsvTorgb33_g11790 = RGBToHSV( staticSwitch62_g11790.rgb );
				float temp_output_43_0_g11790 = ( ( hsvTorgb33_g11790.x + 0.08333334 ) % 1.0 );
				float4 ifLocalVar46_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.8333333 )
				ifLocalVar46_g11790 = _RecolorRGBYCPPurpleTint;
				else
				ifLocalVar46_g11790 = _RecolorRGBYCPBlueTint;
				float4 ifLocalVar44_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.6666667 )
				ifLocalVar44_g11790 = _RecolorRGBYCPCyanTint;
				else
				ifLocalVar44_g11790 = ifLocalVar46_g11790;
				float4 ifLocalVar47_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.3333333 )
				ifLocalVar47_g11790 = _RecolorRGBYCPYellowTint;
				else
				ifLocalVar47_g11790 = _RecolorRGBYCPGreenTint;
				float4 ifLocalVar45_g11790 = 0;
				if( temp_output_43_0_g11790 <= 0.1666667 )
				ifLocalVar45_g11790 = _RecolorRGBYCPRedTint;
				else
				ifLocalVar45_g11790 = ifLocalVar47_g11790;
				float4 ifLocalVar35_g11790 = 0;
				if( temp_output_43_0_g11790 >= 0.5 )
				ifLocalVar35_g11790 = ifLocalVar44_g11790;
				else
				ifLocalVar35_g11790 = ifLocalVar45_g11790;
				#ifdef _ENABLERECOLORRGBYCP_ON
				float4 break55_g11790 = ifLocalVar35_g11790;
				float3 appendResult56_g11790 = (float3(break55_g11790.r , break55_g11790.g , break55_g11790.b));
				float4 break2_g11791 = temp_output_1_0_g11790;
				float saferPower57_g11790 = abs( ( ( break2_g11791.x + break2_g11791.x + break2_g11791.y + break2_g11791.y + break2_g11791.y + break2_g11791.z ) / 6.0 ) );
				float3 lerpResult26_g11790 = lerp( (temp_output_1_0_g11790).rgb , ( appendResult56_g11790 * pow( saferPower57_g11790 , max( ( break55_g11790.a * 2.0 ) , 0.01 ) ) ) , ( hsvTorgb33_g11790.z * _RecolorRGBYCPFade ));
				float4 appendResult30_g11790 = (float4(lerpResult26_g11790 , temp_output_1_0_g11790.a));
				float4 staticSwitch9_g11765 = appendResult30_g11790;
				#else
				float4 staticSwitch9_g11765 = staticSwitch43_g11765;
				#endif
				#ifdef _ENABLECOLORREPLACE_ON
				float4 temp_output_1_0_g11768 = staticSwitch9_g11765;
				float3 temp_output_2_0_g11768 = (temp_output_1_0_g11768).rgb;
				float3 In115_g11768 = temp_output_2_0_g11768;
				float3 From115_g11768 = (_ColorReplaceFromColor).rgb;
				float4 break2_g11769 = temp_output_1_0_g11768;
				float3 To115_g11768 = ( pow( ( ( break2_g11769.x + break2_g11769.x + break2_g11769.y + break2_g11769.y + break2_g11769.y + break2_g11769.z ) / 6.0 ) , max( _ColorReplaceContrast , 0.0001 ) ) * (_ColorReplaceToColor).rgb );
				float Fuzziness115_g11768 = _ColorReplaceSmoothness;
				float Range115_g11768 = _ColorReplaceRange;
				float3 localMyCustomExpression115_g11768 = MyCustomExpression115_g11768( In115_g11768 , From115_g11768 , To115_g11768 , Fuzziness115_g11768 , Range115_g11768 );
				float3 lerpResult112_g11768 = lerp( temp_output_2_0_g11768 , localMyCustomExpression115_g11768 , _ColorReplaceFade);
				float4 appendResult4_g11768 = (float4(lerpResult112_g11768 , temp_output_1_0_g11768.a));
				float4 staticSwitch29_g11765 = appendResult4_g11768;
				#else
				float4 staticSwitch29_g11765 = staticSwitch9_g11765;
				#endif
				float4 temp_output_1_0_g11779 = staticSwitch29_g11765;
				#ifdef _ENABLENEGATIVE_ON
				float3 temp_output_9_0_g11779 = (temp_output_1_0_g11779).rgb;
				float3 lerpResult3_g11779 = lerp( temp_output_9_0_g11779 , ( 1.0 - temp_output_9_0_g11779 ) , _NegativeFade);
				float4 appendResult8_g11779 = (float4(lerpResult3_g11779 , temp_output_1_0_g11779.a));
				float4 staticSwitch4_g11779 = appendResult8_g11779;
				#else
				float4 staticSwitch4_g11779 = temp_output_1_0_g11779;
				#endif
				float4 temp_output_57_0_g11765 = staticSwitch4_g11779;
				#ifdef _ENABLECONTRAST_ON
				float4 temp_output_1_0_g11800 = temp_output_57_0_g11765;
				float3 saferPower5_g11800 = abs( (temp_output_1_0_g11800).rgb );
				float3 temp_cast_29 = (_Contrast).xxx;
				float4 appendResult4_g11800 = (float4(pow( saferPower5_g11800 , temp_cast_29 ) , temp_output_1_0_g11800.a));
				float4 staticSwitch32_g11765 = appendResult4_g11800;
				#else
				float4 staticSwitch32_g11765 = temp_output_57_0_g11765;
				#endif
				#ifdef _ENABLEBRIGHTNESS_ON
				float4 temp_output_2_0_g11777 = staticSwitch32_g11765;
				float4 appendResult6_g11777 = (float4(( (temp_output_2_0_g11777).rgb * _Brightness ) , temp_output_2_0_g11777.a));
				float4 staticSwitch33_g11765 = appendResult6_g11777;
				#else
				float4 staticSwitch33_g11765 = staticSwitch32_g11765;
				#endif
				#ifdef _ENABLEHUE_ON
				float4 temp_output_2_0_g11778 = staticSwitch33_g11765;
				float3 hsvTorgb1_g11778 = RGBToHSV( temp_output_2_0_g11778.rgb );
				float3 hsvTorgb3_g11778 = HSVToRGB( float3(( hsvTorgb1_g11778.x + _Hue ),hsvTorgb1_g11778.y,hsvTorgb1_g11778.z) );
				float4 appendResult8_g11778 = (float4(hsvTorgb3_g11778 , temp_output_2_0_g11778.a));
				float4 staticSwitch36_g11765 = appendResult8_g11778;
				#else
				float4 staticSwitch36_g11765 = staticSwitch33_g11765;
				#endif
				#ifdef _ENABLESPLITTONING_ON
				float4 temp_output_1_0_g11794 = staticSwitch36_g11765;
				float4 break2_g11795 = temp_output_1_0_g11794;
				float temp_output_3_0_g11794 = ( ( break2_g11795.x + break2_g11795.x + break2_g11795.y + break2_g11795.y + break2_g11795.y + break2_g11795.z ) / 6.0 );
				float clampResult25_g11794 = clamp( ( ( ( ( temp_output_3_0_g11794 + _SplitToningShift ) - 0.5 ) * _SplitToningBalance ) + 0.5 ) , 0.0 , 1.0 );
				float3 lerpResult6_g11794 = lerp( (_SplitToningShadowsColor).rgb , (_SplitToningHighlightsColor).rgb , clampResult25_g11794);
				float temp_output_9_0_g11796 = max( _SplitToningContrast , 0.0 );
				float saferPower7_g11796 = abs( ( temp_output_3_0_g11794 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11796 ) , 0.0 ) ) ) );
				float3 lerpResult11_g11794 = lerp( (temp_output_1_0_g11794).rgb , ( lerpResult6_g11794 * pow( saferPower7_g11796 , temp_output_9_0_g11796 ) ) , _SplitToningFade);
				float4 appendResult18_g11794 = (float4(lerpResult11_g11794 , temp_output_1_0_g11794.a));
				float4 staticSwitch30_g11765 = appendResult18_g11794;
				#else
				float4 staticSwitch30_g11765 = staticSwitch36_g11765;
				#endif
				#ifdef _ENABLEBLACKTINT_ON
				float4 temp_output_1_0_g11775 = staticSwitch30_g11765;
				float3 temp_output_4_0_g11775 = (temp_output_1_0_g11775).rgb;
				float4 break12_g11775 = temp_output_1_0_g11775;
				float3 lerpResult7_g11775 = lerp( temp_output_4_0_g11775 , ( temp_output_4_0_g11775 + (_BlackTintColor).rgb ) , pow( ( 1.0 - min( max( max( break12_g11775.r , break12_g11775.g ) , break12_g11775.b ) , 1.0 ) ) , max( _BlackTintPower , 0.001 ) ));
				float3 lerpResult13_g11775 = lerp( temp_output_4_0_g11775 , lerpResult7_g11775 , _BlackTintFade);
				float4 appendResult11_g11775 = (float4(lerpResult13_g11775 , break12_g11775.a));
				float4 staticSwitch20_g11765 = appendResult11_g11775;
				#else
				float4 staticSwitch20_g11765 = staticSwitch30_g11765;
				#endif
				#ifdef _ENABLEINKSPREAD_ON
				float4 temp_output_1_0_g11786 = staticSwitch20_g11765;
				float4 break2_g11788 = temp_output_1_0_g11786;
				float temp_output_9_0_g11789 = max( _InkSpreadContrast , 0.0 );
				float saferPower7_g11789 = abs( ( ( ( break2_g11788.x + break2_g11788.x + break2_g11788.y + break2_g11788.y + break2_g11788.y + break2_g11788.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11789 ) , 0.0 ) ) ) );
				float2 temp_output_65_0_g11786 = shaderPosition235;
				float linValue16_g11787 = tex2D( _UberNoiseTexture, ( temp_output_65_0_g11786 * _InkSpreadNoiseScale ) ).r;
				float localMyCustomExpression16_g11787 = MyCustomExpression16_g11787( linValue16_g11787 );
				float clampResult53_g11786 = clamp( ( ( ( _InkSpreadDistance - distance( _InkSpreadPosition , temp_output_65_0_g11786 ) ) + ( localMyCustomExpression16_g11787 * _InkSpreadNoiseFactor ) ) / max( _InkSpreadWidth , 0.001 ) ) , 0.0 , 1.0 );
				float3 lerpResult7_g11786 = lerp( (temp_output_1_0_g11786).rgb , ( (_InkSpreadColor).rgb * pow( saferPower7_g11789 , temp_output_9_0_g11789 ) ) , ( _InkSpreadFade * clampResult53_g11786 ));
				float4 appendResult9_g11786 = (float4(lerpResult7_g11786 , (temp_output_1_0_g11786).a));
				float4 staticSwitch17_g11765 = appendResult9_g11786;
				#else
				float4 staticSwitch17_g11765 = staticSwitch20_g11765;
				#endif
				float temp_output_39_0_g11765 = shaderTime237;
				#ifdef _ENABLESHIFTHUE_ON
				float4 temp_output_1_0_g11780 = staticSwitch17_g11765;
				float3 hsvTorgb15_g11780 = RGBToHSV( (temp_output_1_0_g11780).rgb );
				float3 hsvTorgb19_g11780 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _ShiftHueSpeed ) + hsvTorgb15_g11780.x ),hsvTorgb15_g11780.y,hsvTorgb15_g11780.z) );
				float4 appendResult6_g11780 = (float4(hsvTorgb19_g11780 , temp_output_1_0_g11780.a));
				float4 staticSwitch19_g11765 = appendResult6_g11780;
				#else
				float4 staticSwitch19_g11765 = staticSwitch17_g11765;
				#endif
				float3 hsvTorgb19_g11783 = HSVToRGB( float3(( ( temp_output_39_0_g11765 * _AddHueSpeed ) % 1.0 ),_AddHueSaturation,_AddHueBrightness) );
				float4 temp_output_1_0_g11783 = staticSwitch19_g11765;
				float4 break2_g11785 = temp_output_1_0_g11783;
				float saferPower27_g11783 = abs( ( ( break2_g11785.x + break2_g11785.x + break2_g11785.y + break2_g11785.y + break2_g11785.y + break2_g11785.z ) / 6.0 ) );
				#ifdef _ADDHUEMASKTOGGLE_ON
				float2 uv_AddHueMask = IN.ase_texcoord2.xy * _AddHueMask_ST.xy + _AddHueMask_ST.zw;
				float4 tex2DNode3_g11784 = tex2D( _AddHueMask, uv_AddHueMask );
				float staticSwitch33_g11783 = ( _AddHueFade * ( tex2DNode3_g11784.r * tex2DNode3_g11784.a ) );
				#else
				float staticSwitch33_g11783 = _AddHueFade;
				#endif
				#ifdef _ENABLEADDHUE_ON
				float4 appendResult6_g11783 = (float4(( ( hsvTorgb19_g11783 * pow( saferPower27_g11783 , max( _AddHueContrast , 0.001 ) ) * staticSwitch33_g11783 ) + (temp_output_1_0_g11783).rgb ) , temp_output_1_0_g11783.a));
				float4 staticSwitch23_g11765 = appendResult6_g11783;
				#else
				float4 staticSwitch23_g11765 = staticSwitch19_g11765;
				#endif
				float4 temp_output_1_0_g11781 = staticSwitch23_g11765;
				float4 break2_g11782 = temp_output_1_0_g11781;
				float3 temp_output_13_0_g11781 = (_SineGlowColor).rgb;
				#ifdef _SINEGLOWMASKTOGGLE_ON
				float2 uv_SineGlowMask = IN.ase_texcoord2.xy * _SineGlowMask_ST.xy + _SineGlowMask_ST.zw;
				float4 tex2DNode30_g11781 = tex2D( _SineGlowMask, uv_SineGlowMask );
				float3 staticSwitch27_g11781 = ( (tex2DNode30_g11781).rgb * temp_output_13_0_g11781 * tex2DNode30_g11781.a );
				#else
				float3 staticSwitch27_g11781 = temp_output_13_0_g11781;
				#endif
				#ifdef _ENABLESINEGLOW_ON
				float4 appendResult21_g11781 = (float4(( (temp_output_1_0_g11781).rgb + ( pow( ( ( break2_g11782.x + break2_g11782.x + break2_g11782.y + break2_g11782.y + break2_g11782.y + break2_g11782.z ) / 6.0 ) , max( _SineGlowContrast , 0.0 ) ) * staticSwitch27_g11781 * _SineGlowFade * ( ( ( sin( ( temp_output_39_0_g11765 * _SineGlowFrequency ) ) + 1.0 ) * ( _SineGlowMax - _SineGlowMin ) ) + _SineGlowMin ) ) ) , temp_output_1_0_g11781.a));
				float4 staticSwitch28_g11765 = appendResult21_g11781;
				#else
				float4 staticSwitch28_g11765 = staticSwitch23_g11765;
				#endif
				#ifdef _ENABLESATURATION_ON
				float4 temp_output_1_0_g11770 = staticSwitch28_g11765;
				float4 break2_g11771 = temp_output_1_0_g11770;
				float3 temp_cast_45 = (( ( break2_g11771.x + break2_g11771.x + break2_g11771.y + break2_g11771.y + break2_g11771.y + break2_g11771.z ) / 6.0 )).xxx;
				float3 lerpResult5_g11770 = lerp( temp_cast_45 , (temp_output_1_0_g11770).rgb , _Saturation);
				float4 appendResult8_g11770 = (float4(lerpResult5_g11770 , temp_output_1_0_g11770.a));
				float4 staticSwitch38_g11765 = appendResult8_g11770;
				#else
				float4 staticSwitch38_g11765 = staticSwitch28_g11765;
				#endif
				float4 temp_output_15_0_g11772 = staticSwitch38_g11765;
				float3 temp_output_82_0_g11772 = (_InnerOutlineColor).rgb;
				float2 temp_output_7_0_g11772 = temp_output_1_0_g11765;
				float temp_output_179_0_g11772 = temp_output_39_0_g11765;
				#ifdef _INNEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch187_g11772 = ( (tex2D( _InnerOutlineTintTexture, ( temp_output_7_0_g11772 + ( _InnerOutlineTextureSpeed * temp_output_179_0_g11772 ) ) )).rgb * temp_output_82_0_g11772 );
				#else
				float3 staticSwitch187_g11772 = temp_output_82_0_g11772;
				#endif
				#ifdef _INNEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11774 = tex2D( _UberNoiseTexture, ( ( ( temp_output_179_0_g11772 * _InnerOutlineNoiseSpeed ) + temp_output_7_0_g11772 ) * _InnerOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11774 = MyCustomExpression16_g11774( linValue16_g11774 );
				float2 staticSwitch169_g11772 = ( ( localMyCustomExpression16_g11774 - 0.5 ) * _InnerOutlineDistortionIntensity );
				#else
				float2 staticSwitch169_g11772 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11772 = ( staticSwitch169_g11772 + temp_output_7_0_g11772 );
				float2 appendResult2_g11773 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11772 = ( 100.0 / appendResult2_g11773 );
				float temp_output_178_0_g11772 = ( _InnerOutlineFade * ( 1.0 - min( min( min( min( min( min( min( tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11772 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11772 + ( ( _InnerOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11772 ) ) ).a ) ) );
				float3 lerpResult176_g11772 = lerp( (temp_output_15_0_g11772).rgb , staticSwitch187_g11772 , temp_output_178_0_g11772);
				#ifdef _INNEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch188_g11772 = ( temp_output_178_0_g11772 * temp_output_15_0_g11772.a );
				#else
				float staticSwitch188_g11772 = temp_output_15_0_g11772.a;
				#endif
				#ifdef _ENABLEINNEROUTLINE_ON
				float4 appendResult177_g11772 = (float4(lerpResult176_g11772 , staticSwitch188_g11772));
				float4 staticSwitch12_g11765 = appendResult177_g11772;
				#else
				float4 staticSwitch12_g11765 = staticSwitch38_g11765;
				#endif
				float4 temp_output_15_0_g11797 = staticSwitch12_g11765;
				float3 temp_output_82_0_g11797 = (_OuterOutlineColor).rgb;
				float2 temp_output_7_0_g11797 = temp_output_1_0_g11765;
				float temp_output_186_0_g11797 = temp_output_39_0_g11765;
				#ifdef _OUTEROUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11797 = ( (tex2D( _OuterOutlineTintTexture, ( temp_output_7_0_g11797 + ( _OuterOutlineTextureSpeed * temp_output_186_0_g11797 ) ) )).rgb * temp_output_82_0_g11797 );
				#else
				float3 staticSwitch199_g11797 = temp_output_82_0_g11797;
				#endif
				float temp_output_182_0_g11797 = ( ( 1.0 - temp_output_15_0_g11797.a ) * min( ( _OuterOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11797 = 1.0;
				#else
				float staticSwitch203_g11797 = temp_output_182_0_g11797;
				#endif
				float3 lerpResult178_g11797 = lerp( (temp_output_15_0_g11797).rgb , staticSwitch199_g11797 , staticSwitch203_g11797);
				float3 lerpResult170_g11797 = lerp( lerpResult178_g11797 , staticSwitch199_g11797 , staticSwitch203_g11797);
				#ifdef _OUTEROUTLINEDISTORTIONTOGGLE_ON
				float linValue16_g11798 = tex2D( _UberNoiseTexture, ( ( ( temp_output_186_0_g11797 * _OuterOutlineNoiseSpeed ) + temp_output_7_0_g11797 ) * _OuterOutlineNoiseScale ) ).r;
				float localMyCustomExpression16_g11798 = MyCustomExpression16_g11798( linValue16_g11798 );
				float2 staticSwitch157_g11797 = ( ( localMyCustomExpression16_g11798 - 0.5 ) * _OuterOutlineDistortionIntensity );
				#else
				float2 staticSwitch157_g11797 = float2( 0,0 );
				#endif
				float2 temp_output_131_0_g11797 = ( staticSwitch157_g11797 + temp_output_7_0_g11797 );
				float2 appendResult2_g11799 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_25_0_g11797 = ( 100.0 / appendResult2_g11799 );
				float lerpResult168_g11797 = lerp( temp_output_15_0_g11797.a , min( ( max( max( max( max( max( max( max( tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,-1 ) ) * temp_output_25_0_g11797 ) ) ).a , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0,1 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 1,0 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( 0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) , tex2D( _MainTex, ( temp_output_131_0_g11797 + ( ( _OuterOutlineWidth * float2( -0.705,-0.705 ) ) * temp_output_25_0_g11797 ) ) ).a ) * 3.0 ) , 1.0 ) , _OuterOutlineFade);
				#ifdef _OUTEROUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11797 = ( temp_output_182_0_g11797 * lerpResult168_g11797 );
				#else
				float staticSwitch200_g11797 = lerpResult168_g11797;
				#endif
				#ifdef _ENABLEOUTEROUTLINE_ON
				float4 appendResult174_g11797 = (float4(lerpResult170_g11797 , staticSwitch200_g11797));
				float4 staticSwitch13_g11765 = appendResult174_g11797;
				#else
				float4 staticSwitch13_g11765 = staticSwitch12_g11765;
				#endif
				float4 temp_output_15_0_g11776 = staticSwitch13_g11765;
				float3 temp_output_82_0_g11776 = (_PixelOutlineColor).rgb;
				float2 temp_output_7_0_g11776 = temp_output_1_0_g11765;
				#ifdef _PIXELOUTLINETEXTURETOGGLE_ON
				float3 staticSwitch199_g11776 = ( (tex2D( _PixelOutlineTintTexture, ( temp_output_7_0_g11776 + ( _PixelOutlineTextureSpeed * temp_output_39_0_g11765 ) ) )).rgb * temp_output_82_0_g11776 );
				#else
				float3 staticSwitch199_g11776 = temp_output_82_0_g11776;
				#endif
				float temp_output_182_0_g11776 = ( ( 1.0 - temp_output_15_0_g11776.a ) * min( ( _PixelOutlineFade * 3.0 ) , 1.0 ) );
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch203_g11776 = 1.0;
				#else
				float staticSwitch203_g11776 = temp_output_182_0_g11776;
				#endif
				float3 lerpResult178_g11776 = lerp( (temp_output_15_0_g11776).rgb , staticSwitch199_g11776 , staticSwitch203_g11776);
				float3 lerpResult170_g11776 = lerp( lerpResult178_g11776 , staticSwitch199_g11776 , staticSwitch203_g11776);
				float2 appendResult206_g11776 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float2 temp_output_209_0_g11776 = ( float2( 1,1 ) / appendResult206_g11776 );
				float lerpResult168_g11776 = lerp( temp_output_15_0_g11776.a , min( ( max( max( max( tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,-1 ) ) * temp_output_209_0_g11776 ) ) ).a , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 0,1 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( -1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) , tex2D( _MainTex, ( temp_output_7_0_g11776 + ( ( _PixelOutlineWidth * float2( 1,0 ) ) * temp_output_209_0_g11776 ) ) ).a ) * 3.0 ) , 1.0 ) , _PixelOutlineFade);
				#ifdef _PIXELOUTLINEOUTLINEONLYTOGGLE_ON
				float staticSwitch200_g11776 = ( temp_output_182_0_g11776 * lerpResult168_g11776 );
				#else
				float staticSwitch200_g11776 = lerpResult168_g11776;
				#endif
				#ifdef _ENABLEPIXELOUTLINE_ON
				float4 appendResult174_g11776 = (float4(lerpResult170_g11776 , staticSwitch200_g11776));
				float4 staticSwitch48_g11765 = appendResult174_g11776;
				#else
				float4 staticSwitch48_g11765 = staticSwitch13_g11765;
				#endif
				#ifdef _ENABLEPINGPONGGLOW_ON
				float3 lerpResult15_g11766 = lerp( (_PingPongGlowFrom).rgb , (_PingPongGlowTo).rgb , ( ( sin( ( temp_output_39_0_g11765 * _PingPongGlowFrequency ) ) + 1.0 ) / 2.0 ));
				float4 temp_output_5_0_g11766 = staticSwitch48_g11765;
				float4 break2_g11767 = temp_output_5_0_g11766;
				float4 appendResult12_g11766 = (float4(( ( lerpResult15_g11766 * _PingPongGlowFade * pow( ( ( break2_g11767.x + break2_g11767.x + break2_g11767.y + break2_g11767.y + break2_g11767.y + break2_g11767.z ) / 6.0 ) , max( _PingPongGlowContrast , 0.0 ) ) ) + (temp_output_5_0_g11766).rgb ) , temp_output_5_0_g11766.a));
				float4 staticSwitch46_g11765 = appendResult12_g11766;
				#else
				float4 staticSwitch46_g11765 = staticSwitch48_g11765;
				#endif
				float4 temp_output_361_0 = staticSwitch46_g11765;
				#ifdef _ENABLEHOLOGRAM_ON
				float4 temp_output_1_0_g11801 = temp_output_361_0;
				float4 break2_g11802 = temp_output_1_0_g11801;
				float temp_output_9_0_g11803 = max( _HologramContrast , 0.0 );
				float saferPower7_g11803 = abs( ( ( ( break2_g11802.x + break2_g11802.x + break2_g11802.y + break2_g11802.y + break2_g11802.y + break2_g11802.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11803 ) , 0.0 ) ) ) );
				float4 appendResult22_g11801 = (float4(( (_HologramTint).rgb * pow( saferPower7_g11803 , temp_output_9_0_g11803 ) ) , ( max( pow( abs( sin( ( ( ( ( shaderTime237 * _HologramLineSpeed ) + ase_worldPos.y ) / unity_OrthoParams.y ) * _HologramLineFrequency ) ) ) , _HologramLineGap ) , _HologramMinAlpha ) * temp_output_1_0_g11801.a )));
				float4 lerpResult37_g11801 = lerp( temp_output_1_0_g11801 , appendResult22_g11801 , hologramFade182);
				float4 staticSwitch56 = lerpResult37_g11801;
				#else
				float4 staticSwitch56 = temp_output_361_0;
				#endif
				#ifdef _ENABLEGLITCH_ON
				float4 temp_output_1_0_g11804 = staticSwitch56;
				float4 break2_g11806 = temp_output_1_0_g11804;
				float temp_output_34_0_g11804 = shaderTime237;
				float linValue16_g11805 = tex2D( _UberNoiseTexture, ( ( glitchPosition154 + ( _GlitchNoiseSpeed * temp_output_34_0_g11804 ) ) * _GlitchNoiseScale ) ).r;
				float localMyCustomExpression16_g11805 = MyCustomExpression16_g11805( linValue16_g11805 );
				float3 hsvTorgb3_g11807 = HSVToRGB( float3(( localMyCustomExpression16_g11805 + ( temp_output_34_0_g11804 * _GlitchHueSpeed ) ),1.0,1.0) );
				float3 lerpResult23_g11804 = lerp( (temp_output_1_0_g11804).rgb , ( ( ( break2_g11806.x + break2_g11806.x + break2_g11806.y + break2_g11806.y + break2_g11806.y + break2_g11806.z ) / 6.0 ) * _GlitchBrightness * hsvTorgb3_g11807 ) , glitchFade152);
				float4 appendResult27_g11804 = (float4(lerpResult23_g11804 , temp_output_1_0_g11804.a));
				float4 staticSwitch57 = appendResult27_g11804;
				#else
				float4 staticSwitch57 = staticSwitch56;
				#endif
				float4 temp_output_3_0_g11808 = staticSwitch57;
				float4 temp_output_1_0_g11833 = temp_output_3_0_g11808;
				float2 temp_output_41_0_g11808 = shaderPosition235;
				float2 temp_output_99_0_g11833 = temp_output_41_0_g11808;
				float temp_output_40_0_g11808 = shaderTime237;
				#ifdef _CAMOUFLAGEANIMATIONTOGGLE_ON
				float linValue16_g11838 = tex2D( _UberNoiseTexture, ( ( ( temp_output_40_0_g11808 * _CamouflageDistortionSpeed ) + temp_output_99_0_g11833 ) * _CamouflageDistortionScale ) ).r;
				float localMyCustomExpression16_g11838 = MyCustomExpression16_g11838( linValue16_g11838 );
				float2 staticSwitch101_g11833 = ( ( ( localMyCustomExpression16_g11838 - 0.25 ) * _CamouflageDistortionIntensity ) + temp_output_99_0_g11833 );
				#else
				float2 staticSwitch101_g11833 = temp_output_99_0_g11833;
				#endif
				float linValue16_g11835 = tex2D( _UberNoiseTexture, ( staticSwitch101_g11833 * _CamouflageNoiseScaleA ) ).r;
				float localMyCustomExpression16_g11835 = MyCustomExpression16_g11835( linValue16_g11835 );
				float clampResult52_g11833 = clamp( ( ( _CamouflageDensityA - localMyCustomExpression16_g11835 ) / max( _CamouflageSmoothnessA , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult55_g11833 = lerp( _CamouflageBaseColor , ( _CamouflageColorA * clampResult52_g11833 ) , clampResult52_g11833);
				float linValue16_g11837 = tex2D( _UberNoiseTexture, ( ( staticSwitch101_g11833 + float2( 12.3,12.3 ) ) * _CamouflageNoiseScaleB ) ).r;
				#ifdef _ENABLECAMOUFLAGE_ON
				float localMyCustomExpression16_g11837 = MyCustomExpression16_g11837( linValue16_g11837 );
				float clampResult65_g11833 = clamp( ( ( _CamouflageDensityB - localMyCustomExpression16_g11837 ) / max( _CamouflageSmoothnessB , 0.005 ) ) , 0.0 , 1.0 );
				float4 lerpResult68_g11833 = lerp( lerpResult55_g11833 , ( _CamouflageColorB * clampResult65_g11833 ) , clampResult65_g11833);
				float4 break2_g11836 = temp_output_1_0_g11833;
				float temp_output_9_0_g11834 = max( _CamouflageContrast , 0.0 );
				float saferPower7_g11834 = abs( ( ( ( break2_g11836.x + break2_g11836.x + break2_g11836.y + break2_g11836.y + break2_g11836.y + break2_g11836.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11834 ) , 0.0 ) ) ) );
				float3 lerpResult4_g11833 = lerp( (temp_output_1_0_g11833).rgb , ( (lerpResult68_g11833).rgb * pow( saferPower7_g11834 , temp_output_9_0_g11834 ) ) , _CamouflageFade);
				float4 appendResult7_g11833 = (float4(lerpResult4_g11833 , temp_output_1_0_g11833.a));
				float4 staticSwitch26_g11808 = appendResult7_g11833;
				#else
				float4 staticSwitch26_g11808 = temp_output_3_0_g11808;
				#endif
				float4 temp_output_1_0_g11827 = staticSwitch26_g11808;
				float temp_output_59_0_g11827 = temp_output_40_0_g11808;
				float2 temp_output_58_0_g11827 = temp_output_41_0_g11808;
				float linValue16_g11828 = tex2D( _UberNoiseTexture, ( ( ( temp_output_59_0_g11827 * _MetalNoiseDistortionSpeed ) + temp_output_58_0_g11827 ) * _MetalNoiseDistortionScale ) ).r;
				float localMyCustomExpression16_g11828 = MyCustomExpression16_g11828( linValue16_g11828 );
				float linValue16_g11830 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11828 - 0.25 ) * _MetalNoiseDistortion ) + ( ( temp_output_59_0_g11827 * _MetalNoiseSpeed ) + temp_output_58_0_g11827 ) ) * _MetalNoiseScale ) ).r;
				float localMyCustomExpression16_g11830 = MyCustomExpression16_g11830( linValue16_g11830 );
				float4 break2_g11829 = temp_output_1_0_g11827;
				float temp_output_5_0_g11827 = ( ( break2_g11829.x + break2_g11829.x + break2_g11829.y + break2_g11829.y + break2_g11829.y + break2_g11829.z ) / 6.0 );
				float temp_output_9_0_g11831 = max( _MetalHighlightContrast , 0.0 );
				float saferPower7_g11831 = abs( ( temp_output_5_0_g11827 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11831 ) , 0.0 ) ) ) );
				float saferPower2_g11827 = abs( temp_output_5_0_g11827 );
				#ifdef _METALMASKTOGGLE_ON
				float2 uv_MetalMask = IN.ase_texcoord2.xy * _MetalMask_ST.xy + _MetalMask_ST.zw;
				float4 tex2DNode3_g11832 = tex2D( _MetalMask, uv_MetalMask );
				float staticSwitch60_g11827 = ( _MetalFade * ( tex2DNode3_g11832.r * tex2DNode3_g11832.a ) );
				#else
				float staticSwitch60_g11827 = _MetalFade;
				#endif
				#ifdef _ENABLEMETAL_ON
				float4 lerpResult45_g11827 = lerp( temp_output_1_0_g11827 , ( ( max( ( ( _MetalHighlightDensity - localMyCustomExpression16_g11830 ) / max( _MetalHighlightDensity , 0.01 ) ) , 0.0 ) * _MetalHighlightColor * pow( saferPower7_g11831 , temp_output_9_0_g11831 ) ) + ( pow( saferPower2_g11827 , _MetalContrast ) * _MetalColor ) ) , staticSwitch60_g11827);
				float4 appendResult8_g11827 = (float4((lerpResult45_g11827).rgb , (temp_output_1_0_g11827).a));
				float4 staticSwitch28_g11808 = appendResult8_g11827;
				#else
				float4 staticSwitch28_g11808 = staticSwitch26_g11808;
				#endif
				#ifdef _ENABLEFROZEN_ON
				float4 temp_output_1_0_g11821 = staticSwitch28_g11808;
				float4 break2_g11822 = temp_output_1_0_g11821;
				float temp_output_7_0_g11821 = ( ( break2_g11822.x + break2_g11822.x + break2_g11822.y + break2_g11822.y + break2_g11822.y + break2_g11822.z ) / 6.0 );
				float temp_output_9_0_g11824 = max( _FrozenContrast , 0.0 );
				float saferPower7_g11824 = abs( ( temp_output_7_0_g11821 + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11824 ) , 0.0 ) ) ) );
				float saferPower20_g11821 = abs( temp_output_7_0_g11821 );
				float2 temp_output_72_0_g11821 = temp_output_41_0_g11808;
				float linValue16_g11823 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11821 * _FrozenSnowScale ) ).r;
				float localMyCustomExpression16_g11823 = MyCustomExpression16_g11823( linValue16_g11823 );
				float temp_output_73_0_g11821 = temp_output_40_0_g11808;
				float linValue16_g11825 = tex2D( _UberNoiseTexture, ( ( ( temp_output_73_0_g11821 * _FrozenHighlightDistortionSpeed ) + temp_output_72_0_g11821 ) * _FrozenHighlightDistortionScale ) ).r;
				float localMyCustomExpression16_g11825 = MyCustomExpression16_g11825( linValue16_g11825 );
				float linValue16_g11826 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11825 - 0.25 ) * _FrozenHighlightDistortion ) + ( ( temp_output_73_0_g11821 * _FrozenHighlightSpeed ) + temp_output_72_0_g11821 ) ) * _FrozenHighlightScale ) ).r;
				float localMyCustomExpression16_g11826 = MyCustomExpression16_g11826( linValue16_g11826 );
				float saferPower42_g11821 = abs( temp_output_7_0_g11821 );
				float3 lerpResult57_g11821 = lerp( (temp_output_1_0_g11821).rgb , ( ( pow( saferPower7_g11824 , temp_output_9_0_g11824 ) * (_FrozenTint).rgb ) + ( pow( saferPower20_g11821 , _FrozenSnowContrast ) * ( (_FrozenSnowColor).rgb * max( ( _FrozenSnowDensity - localMyCustomExpression16_g11823 ) , 0.0 ) ) ) + (( max( ( ( _FrozenHighlightDensity - localMyCustomExpression16_g11826 ) / max( _FrozenHighlightDensity , 0.01 ) ) , 0.0 ) * _FrozenHighlightColor * pow( saferPower42_g11821 , _FrozenHighlightContrast ) )).rgb ) , _FrozenFade);
				float4 appendResult26_g11821 = (float4(lerpResult57_g11821 , temp_output_1_0_g11821.a));
				float4 staticSwitch29_g11808 = appendResult26_g11821;
				#else
				float4 staticSwitch29_g11808 = staticSwitch28_g11808;
				#endif
				float4 temp_output_1_0_g11816 = staticSwitch29_g11808;
				float3 temp_output_28_0_g11816 = (temp_output_1_0_g11816).rgb;
				float4 break2_g11820 = float4( temp_output_28_0_g11816 , 0.0 );
				float saferPower21_g11816 = abs( ( ( break2_g11820.x + break2_g11820.x + break2_g11820.y + break2_g11820.y + break2_g11820.y + break2_g11820.z ) / 6.0 ) );
				float2 temp_output_72_0_g11816 = temp_output_41_0_g11808;
				float linValue16_g11819 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnSwirlNoiseScale ) ).r;
				float localMyCustomExpression16_g11819 = MyCustomExpression16_g11819( linValue16_g11819 );
				float linValue16_g11817 = tex2D( _UberNoiseTexture, ( ( ( ( localMyCustomExpression16_g11819 - 0.5 ) * float2( 1,1 ) * _BurnSwirlFactor ) + temp_output_72_0_g11816 ) * _BurnInsideNoiseScale ) ).r;
				#ifdef _ENABLEBURN_ON
				float localMyCustomExpression16_g11817 = MyCustomExpression16_g11817( linValue16_g11817 );
				float clampResult68_g11816 = clamp( ( _BurnInsideNoiseFactor - localMyCustomExpression16_g11817 ) , 0.0 , 1.0 );
				float linValue16_g11818 = tex2D( _UberNoiseTexture, ( temp_output_72_0_g11816 * _BurnEdgeNoiseScale ) ).r;
				float localMyCustomExpression16_g11818 = MyCustomExpression16_g11818( linValue16_g11818 );
				float temp_output_15_0_g11816 = ( ( ( _BurnRadius - distance( temp_output_72_0_g11816 , _BurnPosition ) ) + ( localMyCustomExpression16_g11818 * _BurnEdgeNoiseFactor ) ) / max( _BurnWidth , 0.01 ) );
				float clampResult18_g11816 = clamp( temp_output_15_0_g11816 , 0.0 , 1.0 );
				float3 lerpResult29_g11816 = lerp( temp_output_28_0_g11816 , ( pow( saferPower21_g11816 , max( _BurnInsideContrast , 0.001 ) ) * ( ( (_BurnInsideNoiseColor).rgb * clampResult68_g11816 ) + (_BurnInsideColor).rgb ) ) , clampResult18_g11816);
				float3 lerpResult40_g11816 = lerp( temp_output_28_0_g11816 , ( lerpResult29_g11816 + ( ( step( temp_output_15_0_g11816 , 1.0 ) * step( 0.0 , temp_output_15_0_g11816 ) ) * (_BurnEdgeColor).rgb ) ) , _BurnFade);
				float4 appendResult43_g11816 = (float4(lerpResult40_g11816 , temp_output_1_0_g11816.a));
				float4 staticSwitch32_g11808 = appendResult43_g11816;
				#else
				float4 staticSwitch32_g11808 = staticSwitch29_g11808;
				#endif
				#ifdef _ENABLERAINBOW_ON
				float2 temp_output_42_0_g11812 = temp_output_41_0_g11808;
				float linValue16_g11813 = tex2D( _UberNoiseTexture, ( temp_output_42_0_g11812 * _RainbowNoiseScale ) ).r;
				float localMyCustomExpression16_g11813 = MyCustomExpression16_g11813( linValue16_g11813 );
				float3 hsvTorgb3_g11815 = HSVToRGB( float3(( ( ( distance( temp_output_42_0_g11812 , _RainbowCenter ) + ( localMyCustomExpression16_g11813 * _RainbowNoiseFactor ) ) * _RainbowDensity ) + ( _RainbowSpeed * temp_output_40_0_g11808 ) ),1.0,1.0) );
				float3 hsvTorgb36_g11812 = RGBToHSV( hsvTorgb3_g11815 );
				float3 hsvTorgb37_g11812 = HSVToRGB( float3(hsvTorgb36_g11812.x,_RainbowSaturation,( hsvTorgb36_g11812.z * _RainbowBrightness )) );
				float4 temp_output_1_0_g11812 = staticSwitch32_g11808;
				float4 break2_g11814 = temp_output_1_0_g11812;
				float saferPower24_g11812 = abs( ( ( break2_g11814.x + break2_g11814.x + break2_g11814.y + break2_g11814.y + break2_g11814.y + break2_g11814.z ) / 6.0 ) );
				float4 appendResult29_g11812 = (float4(( ( hsvTorgb37_g11812 * pow( saferPower24_g11812 , max( _RainbowContrast , 0.001 ) ) * _RainbowFade ) + (temp_output_1_0_g11812).rgb ) , temp_output_1_0_g11812.a));
				float4 staticSwitch34_g11808 = appendResult29_g11812;
				#else
				float4 staticSwitch34_g11808 = staticSwitch32_g11808;
				#endif
				float4 temp_output_1_0_g11809 = staticSwitch34_g11808;
				float3 temp_output_57_0_g11809 = (temp_output_1_0_g11809).rgb;
				float4 break2_g11810 = temp_output_1_0_g11809;
				float3 temp_cast_68 = (( ( break2_g11810.x + break2_g11810.x + break2_g11810.y + break2_g11810.y + break2_g11810.y + break2_g11810.z ) / 6.0 )).xxx;
				float3 lerpResult92_g11809 = lerp( temp_cast_68 , temp_output_57_0_g11809 , _ShineSaturation);
				float3 saferPower83_g11809 = abs( lerpResult92_g11809 );
				float3 temp_cast_69 = (max( _ShineContrast , 0.001 )).xxx;
				float3 rotatedValue69_g11809 = RotateAroundAxis( float3( 0,0,0 ), float3( ( _ShineFrequency * temp_output_41_0_g11808 ) ,  0.0 ), float3( 0,0,1 ), ( ( _ShineRotation / 180.0 ) * UNITY_PI ) );
				float temp_output_103_0_g11809 = ( _ShineFrequency * _ShineWidth );
				float clampResult80_g11809 = clamp( ( ( sin( ( rotatedValue69_g11809.x - ( temp_output_40_0_g11808 * _ShineSpeed * _ShineFrequency ) ) ) - ( 1.0 - temp_output_103_0_g11809 ) ) / temp_output_103_0_g11809 ) , 0.0 , 1.0 );
				#ifdef _SHINEMASKTOGGLE_ON
				float2 uv_ShineMask = IN.ase_texcoord2.xy * _ShineMask_ST.xy + _ShineMask_ST.zw;
				float4 tex2DNode3_g11811 = tex2D( _ShineMask, uv_ShineMask );
				float staticSwitch98_g11809 = ( _ShineFade * ( tex2DNode3_g11811.r * tex2DNode3_g11811.a ) );
				#else
				float staticSwitch98_g11809 = _ShineFade;
				#endif
				#ifdef _ENABLESHINE_ON
				float4 appendResult8_g11809 = (float4(( temp_output_57_0_g11809 + ( ( pow( saferPower83_g11809 , temp_cast_69 ) * (_ShineColor).rgb ) * clampResult80_g11809 * staticSwitch98_g11809 ) ) , (temp_output_1_0_g11809).a));
				float4 staticSwitch36_g11808 = appendResult8_g11809;
				#else
				float4 staticSwitch36_g11808 = staticSwitch34_g11808;
				#endif
				#ifdef _ENABLEPOISON_ON
				float temp_output_41_0_g11839 = temp_output_40_0_g11808;
				float linValue16_g11841 = tex2D( _UberNoiseTexture, ( ( ( temp_output_41_0_g11839 * _PoisonNoiseSpeed ) + temp_output_41_0_g11808 ) * _PoisonNoiseScale ) ).r;
				float localMyCustomExpression16_g11841 = MyCustomExpression16_g11841( linValue16_g11841 );
				float saferPower19_g11839 = abs( abs( ( ( ( localMyCustomExpression16_g11841 + ( temp_output_41_0_g11839 * _PoisonShiftSpeed ) ) % 1.0 ) + -0.5 ) ) );
				float3 temp_output_24_0_g11839 = (_PoisonColor).rgb;
				float4 temp_output_1_0_g11839 = staticSwitch36_g11808;
				float3 temp_output_28_0_g11839 = (temp_output_1_0_g11839).rgb;
				float4 break2_g11840 = float4( temp_output_28_0_g11839 , 0.0 );
				float3 lerpResult32_g11839 = lerp( temp_output_28_0_g11839 , ( temp_output_24_0_g11839 * ( ( break2_g11840.x + break2_g11840.x + break2_g11840.y + break2_g11840.y + break2_g11840.y + break2_g11840.z ) / 6.0 ) ) , ( _PoisonFade * _PoisonRecolorFactor ));
				float4 appendResult27_g11839 = (float4(( ( max( pow( saferPower19_g11839 , _PoisonDensity ) , 0.0 ) * temp_output_24_0_g11839 * _PoisonFade * _PoisonNoiseBrightness ) + lerpResult32_g11839 ) , temp_output_1_0_g11839.a));
				float4 staticSwitch39_g11808 = appendResult27_g11839;
				#else
				float4 staticSwitch39_g11808 = staticSwitch36_g11808;
				#endif
				float4 temp_output_10_0_g11842 = staticSwitch39_g11808;
				float3 temp_output_12_0_g11842 = (temp_output_10_0_g11842).rgb;
				float2 temp_output_2_0_g11842 = temp_output_41_0_g11808;
				float temp_output_1_0_g11842 = temp_output_40_0_g11808;
				float2 temp_output_6_0_g11842 = ( temp_output_1_0_g11842 * _EnchantedSpeed );
				float linValue16_g11845 = tex2D( _UberNoiseTexture, ( ( ( temp_output_2_0_g11842 - ( ( temp_output_6_0_g11842 + float2( 1.234,5.6789 ) ) * float2( 0.95,1.05 ) ) ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11845 = MyCustomExpression16_g11845( linValue16_g11845 );
				float linValue16_g11843 = tex2D( _UberNoiseTexture, ( ( ( temp_output_6_0_g11842 + temp_output_2_0_g11842 ) * _EnchantedScale ) * float2( 1,1 ) ) ).r;
				float localMyCustomExpression16_g11843 = MyCustomExpression16_g11843( linValue16_g11843 );
				float temp_output_36_0_g11842 = ( localMyCustomExpression16_g11845 + localMyCustomExpression16_g11843 );
				float temp_output_43_0_g11842 = ( temp_output_36_0_g11842 * 0.5 );
				float3 lerpResult42_g11842 = lerp( (_EnchantedLowColor).rgb , (_EnchantedHighColor).rgb , temp_output_43_0_g11842);
				#ifdef _ENCHANTEDRAINBOWTOGGLE_ON
				float3 hsvTorgb53_g11842 = HSVToRGB( float3(( ( temp_output_43_0_g11842 * _EnchantedRainbowDensity ) + ( _EnchantedRainbowSpeed * temp_output_1_0_g11842 ) ),_EnchantedRainbowSaturation,1.0) );
				float3 staticSwitch50_g11842 = hsvTorgb53_g11842;
				#else
				float3 staticSwitch50_g11842 = lerpResult42_g11842;
				#endif
				float4 break2_g11844 = temp_output_10_0_g11842;
				float saferPower24_g11842 = abs( ( ( break2_g11844.x + break2_g11844.x + break2_g11844.y + break2_g11844.y + break2_g11844.y + break2_g11844.z ) / 6.0 ) );
				float3 temp_output_40_0_g11842 = ( staticSwitch50_g11842 * pow( saferPower24_g11842 , _EnchantedContrast ) * _EnchantedBrightness );
				float temp_output_45_0_g11842 = ( max( ( temp_output_36_0_g11842 - _EnchantedReduce ) , 0.0 ) * _EnchantedFade );
				#ifdef _ENCHANTEDLERPTOGGLE_ON
				float3 lerpResult44_g11842 = lerp( temp_output_12_0_g11842 , temp_output_40_0_g11842 , temp_output_45_0_g11842);
				float3 staticSwitch47_g11842 = lerpResult44_g11842;
				#else
				float3 staticSwitch47_g11842 = ( temp_output_12_0_g11842 + ( temp_output_40_0_g11842 * temp_output_45_0_g11842 ) );
				#endif
				#ifdef _ENABLEENCHANTED_ON
				float4 appendResult19_g11842 = (float4(staticSwitch47_g11842 , temp_output_10_0_g11842.a));
				float4 staticSwitch11_g11842 = appendResult19_g11842;
				#else
				float4 staticSwitch11_g11842 = temp_output_10_0_g11842;
				#endif
				float4 temp_output_1_0_g11846 = staticSwitch11_g11842;
				float4 break5_g11846 = temp_output_1_0_g11846;
				float3 appendResult32_g11846 = (float3(break5_g11846.r , break5_g11846.g , break5_g11846.b));
				float4 break2_g11847 = temp_output_1_0_g11846;
				float temp_output_4_0_g11846 = ( ( break2_g11847.x + break2_g11847.x + break2_g11847.y + break2_g11847.y + break2_g11847.y + break2_g11847.z ) / 6.0 );
				float temp_output_11_0_g11846 = ( ( ( temp_output_4_0_g11846 + ( temp_output_40_0_g11808 * _ShiftingSpeed ) ) * _ShiftingDensity ) % 1.0 );
				float3 lerpResult20_g11846 = lerp( (_ShiftingColorA).rgb , (_ShiftingColorB).rgb , ( abs( ( temp_output_11_0_g11846 - 0.5 ) ) * 2.0 ));
				#ifdef _SHIFTINGRAINBOWTOGGLE_ON
				float3 hsvTorgb12_g11846 = HSVToRGB( float3(temp_output_11_0_g11846,_ShiftingSaturation,_ShiftingBrightness) );
				float3 staticSwitch26_g11846 = hsvTorgb12_g11846;
				#else
				float3 staticSwitch26_g11846 = ( lerpResult20_g11846 * _ShiftingBrightness );
				#endif
				#ifdef _ENABLESHIFTING_ON
				float3 lerpResult31_g11846 = lerp( appendResult32_g11846 , ( staticSwitch26_g11846 * pow( temp_output_4_0_g11846 , _ShiftingContrast ) ) , _ShiftingFade);
				float4 appendResult6_g11846 = (float4(lerpResult31_g11846 , break5_g11846.a));
				float4 staticSwitch33_g11846 = appendResult6_g11846;
				#else
				float4 staticSwitch33_g11846 = temp_output_1_0_g11846;
				#endif
				float4 temp_output_473_0 = staticSwitch33_g11846;
				#ifdef _ENABLEFULLDISTORTION_ON
				float4 break4_g11848 = temp_output_473_0;
				float fullDistortionAlpha164 = _FullDistortionFade;
				float4 appendResult5_g11848 = (float4(break4_g11848.r , break4_g11848.g , break4_g11848.b , ( break4_g11848.a * fullDistortionAlpha164 )));
				float4 staticSwitch77 = appendResult5_g11848;
				#else
				float4 staticSwitch77 = temp_output_473_0;
				#endif
				#ifdef _ENABLEDIRECTIONALDISTORTION_ON
				float4 break4_g11849 = staticSwitch77;
				float directionalDistortionAlpha167 = (( _DirectionalDistortionInvert )?( ( 1.0 - clampResult154_g11672 ) ):( clampResult154_g11672 ));
				float4 appendResult5_g11849 = (float4(break4_g11849.r , break4_g11849.g , break4_g11849.b , ( break4_g11849.a * directionalDistortionAlpha167 )));
				float4 staticSwitch75 = appendResult5_g11849;
				#else
				float4 staticSwitch75 = staticSwitch77;
				#endif
				float4 temp_output_1_0_g11850 = staticSwitch75;
				float4 temp_output_1_0_g11851 = temp_output_1_0_g11850;
				float temp_output_53_0_g11851 = max( _FullAlphaDissolveWidth , 0.001 );
				float2 temp_output_18_0_g11850 = shaderPosition235;
				#ifdef _ENABLEFULLALPHADISSOLVE_ON
				float linValue16_g11852 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11852 = MyCustomExpression16_g11852( linValue16_g11852 );
				float clampResult17_g11851 = clamp( ( ( ( _FullAlphaDissolveFade * ( 1.0 + temp_output_53_0_g11851 ) ) - localMyCustomExpression16_g11852 ) / temp_output_53_0_g11851 ) , 0.0 , 1.0 );
				float4 appendResult3_g11851 = (float4((temp_output_1_0_g11851).rgb , ( temp_output_1_0_g11851.a * clampResult17_g11851 )));
				float4 staticSwitch3_g11850 = appendResult3_g11851;
				#else
				float4 staticSwitch3_g11850 = temp_output_1_0_g11850;
				#endif
				#ifdef _ENABLEFULLGLOWDISSOLVE_ON
				float linValue16_g11860 = tex2D( _UberNoiseTexture, ( temp_output_18_0_g11850 * _FullGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11860 = MyCustomExpression16_g11860( linValue16_g11860 );
				float temp_output_5_0_g11859 = localMyCustomExpression16_g11860;
				float temp_output_61_0_g11859 = step( temp_output_5_0_g11859 , _FullGlowDissolveFade );
				float temp_output_53_0_g11859 = max( ( _FullGlowDissolveFade * _FullGlowDissolveWidth ) , 0.001 );
				float4 temp_output_1_0_g11859 = staticSwitch3_g11850;
				float4 appendResult3_g11859 = (float4(( ( (_FullGlowDissolveEdgeColor).rgb * ( temp_output_61_0_g11859 - step( temp_output_5_0_g11859 , ( ( _FullGlowDissolveFade * ( 1.01 + temp_output_53_0_g11859 ) ) - temp_output_53_0_g11859 ) ) ) ) + (temp_output_1_0_g11859).rgb ) , ( temp_output_1_0_g11859.a * temp_output_61_0_g11859 )));
				float4 staticSwitch5_g11850 = appendResult3_g11859;
				#else
				float4 staticSwitch5_g11850 = staticSwitch3_g11850;
				#endif
				#ifdef _ENABLESOURCEALPHADISSOLVE_ON
				float4 temp_output_1_0_g11861 = staticSwitch5_g11850;
				float2 temp_output_76_0_g11861 = temp_output_18_0_g11850;
				float linValue16_g11862 = tex2D( _UberNoiseTexture, ( temp_output_76_0_g11861 * _SourceAlphaDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11862 = MyCustomExpression16_g11862( linValue16_g11862 );
				float clampResult17_g11861 = clamp( ( ( _SourceAlphaDissolveFade - ( distance( _SourceAlphaDissolvePosition , temp_output_76_0_g11861 ) + ( localMyCustomExpression16_g11862 * _SourceAlphaDissolveNoiseFactor ) ) ) / max( _SourceAlphaDissolveWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11861 = (float4((temp_output_1_0_g11861).rgb , ( temp_output_1_0_g11861.a * (( _SourceAlphaDissolveInvert )?( ( 1.0 - clampResult17_g11861 ) ):( clampResult17_g11861 )) )));
				float4 staticSwitch8_g11850 = appendResult3_g11861;
				#else
				float4 staticSwitch8_g11850 = staticSwitch5_g11850;
				#endif
				#ifdef _ENABLESOURCEGLOWDISSOLVE_ON
				float2 temp_output_90_0_g11857 = temp_output_18_0_g11850;
				float linValue16_g11858 = tex2D( _UberNoiseTexture, ( temp_output_90_0_g11857 * _SourceGlowDissolveNoiseScale ) ).r;
				float localMyCustomExpression16_g11858 = MyCustomExpression16_g11858( linValue16_g11858 );
				float temp_output_65_0_g11857 = ( distance( _SourceGlowDissolvePosition , temp_output_90_0_g11857 ) + ( localMyCustomExpression16_g11858 * _SourceGlowDissolveNoiseFactor ) );
				float temp_output_75_0_g11857 = step( temp_output_65_0_g11857 , _SourceGlowDissolveFade );
				float temp_output_76_0_g11857 = step( temp_output_65_0_g11857 , ( _SourceGlowDissolveFade - max( _SourceGlowDissolveWidth , 0.001 ) ) );
				float4 temp_output_1_0_g11857 = staticSwitch8_g11850;
				float4 appendResult3_g11857 = (float4(( ( max( ( temp_output_75_0_g11857 - temp_output_76_0_g11857 ) , 0.0 ) * (_SourceGlowDissolveEdgeColor).rgb ) + (temp_output_1_0_g11857).rgb ) , ( temp_output_1_0_g11857.a * (( _SourceGlowDissolveInvert )?( ( 1.0 - temp_output_76_0_g11857 ) ):( temp_output_75_0_g11857 )) )));
				float4 staticSwitch9_g11850 = appendResult3_g11857;
				#else
				float4 staticSwitch9_g11850 = staticSwitch8_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALALPHAFADE_ON
				float4 temp_output_1_0_g11853 = staticSwitch9_g11850;
				float2 temp_output_161_0_g11853 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11853 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_161_0_g11853 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalAlphaFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11853 = rotatedValue136_g11853;
				float linValue16_g11854 = tex2D( _UberNoiseTexture, ( temp_output_161_0_g11853 * _DirectionalAlphaFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11854 = MyCustomExpression16_g11854( linValue16_g11854 );
				float clampResult154_g11853 = clamp( ( ( break130_g11853.x + break130_g11853.y + _DirectionalAlphaFadeFade + ( localMyCustomExpression16_g11854 * _DirectionalAlphaFadeNoiseFactor ) ) / max( _DirectionalAlphaFadeWidth , 0.001 ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11853 = (float4((temp_output_1_0_g11853).rgb , ( temp_output_1_0_g11853.a * (( _DirectionalAlphaFadeInvert )?( ( 1.0 - clampResult154_g11853 ) ):( clampResult154_g11853 )) )));
				float4 staticSwitch11_g11850 = appendResult3_g11853;
				#else
				float4 staticSwitch11_g11850 = staticSwitch9_g11850;
				#endif
				#ifdef _ENABLEDIRECTIONALGLOWFADE_ON
				float2 temp_output_171_0_g11855 = temp_output_18_0_g11850;
				float3 rotatedValue136_g11855 = RotateAroundAxis( float3( 0,0,0 ), float3( temp_output_171_0_g11855 ,  0.0 ), float3( 0,0,1 ), ( ( ( _DirectionalGlowFadeRotation / 180.0 ) + -0.25 ) * UNITY_PI ) );
				float3 break130_g11855 = rotatedValue136_g11855;
				float linValue16_g11856 = tex2D( _UberNoiseTexture, ( temp_output_171_0_g11855 * _DirectionalGlowFadeNoiseScale ) ).r;
				float localMyCustomExpression16_g11856 = MyCustomExpression16_g11856( linValue16_g11856 );
				float temp_output_168_0_g11855 = max( ( ( break130_g11855.x + break130_g11855.y + _DirectionalGlowFadeFade + ( localMyCustomExpression16_g11856 * _DirectionalGlowFadeNoiseFactor ) ) / max( _DirectionalGlowFadeWidth , 0.001 ) ) , 0.0 );
				float temp_output_161_0_g11855 = step( 0.1 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) );
				float4 temp_output_1_0_g11855 = staticSwitch11_g11850;
				float clampResult154_g11855 = clamp( temp_output_161_0_g11855 , 0.0 , 1.0 );
				float4 appendResult3_g11855 = (float4(( ( (_DirectionalGlowFadeEdgeColor).rgb * ( temp_output_161_0_g11855 - step( 1.0 , (( _DirectionalGlowFadeInvert )?( ( 1.0 - temp_output_168_0_g11855 ) ):( temp_output_168_0_g11855 )) ) ) ) + (temp_output_1_0_g11855).rgb ) , ( temp_output_1_0_g11855.a * clampResult154_g11855 )));
				float4 staticSwitch15_g11850 = appendResult3_g11855;
				#else
				float4 staticSwitch15_g11850 = staticSwitch11_g11850;
				#endif
				float4 temp_output_1_0_g11863 = staticSwitch15_g11850;
				float2 temp_output_126_0_g11863 = temp_output_18_0_g11850;
				float temp_output_121_0_g11863 = max( ( ( _HalftoneFade - distance( _HalftonePosition , temp_output_126_0_g11863 ) ) / max( 0.01 , _HalftoneFadeWidth ) ) , 0.0 );
				float2 appendResult11_g11864 = (float2(temp_output_121_0_g11863 , temp_output_121_0_g11863));
				float temp_output_17_0_g11864 = length( ( (( ( abs( temp_output_126_0_g11863 ) * _HalftoneTiling ) % float2( 1,1 ) )*2.0 + -1.0) / appendResult11_g11864 ) );
				#ifdef _ENABLEHALFTONE_ON
				float clampResult17_g11863 = clamp( saturate( ( ( 1.0 - temp_output_17_0_g11864 ) / fwidth( temp_output_17_0_g11864 ) ) ) , 0.0 , 1.0 );
				float4 appendResult3_g11863 = (float4((temp_output_1_0_g11863).rgb , ( temp_output_1_0_g11863.a * (( _HalftoneInvert )?( ( 1.0 - clampResult17_g11863 ) ):( clampResult17_g11863 )) )));
				float4 staticSwitch13_g11850 = appendResult3_g11863;
				#else
				float4 staticSwitch13_g11850 = staticSwitch15_g11850;
				#endif
				float3 temp_output_3_0_g11866 = (_AddColorColor).rgb;
				#ifdef _ADDCOLORMASKTOGGLE_ON
				float2 uv_AddColorMask = IN.ase_texcoord2.xy * _AddColorMask_ST.xy + _AddColorMask_ST.zw;
				float4 tex2DNode19_g11866 = tex2D( _AddColorMask, uv_AddColorMask );
				float3 staticSwitch16_g11866 = ( temp_output_3_0_g11866 * ( (tex2DNode19_g11866).rgb * tex2DNode19_g11866.a ) );
				#else
				float3 staticSwitch16_g11866 = temp_output_3_0_g11866;
				#endif
				float4 temp_output_1_0_g11866 = staticSwitch13_g11850;
				#ifdef _ADDCOLORCONTRASTTOGGLE_ON
				float4 break2_g11868 = temp_output_1_0_g11866;
				float temp_output_9_0_g11867 = max( _AddColorContrast , 0.0 );
				float saferPower7_g11867 = abs( ( ( ( break2_g11868.x + break2_g11868.x + break2_g11868.y + break2_g11868.y + break2_g11868.y + break2_g11868.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11867 ) , 0.0 ) ) ) );
				float3 staticSwitch17_g11866 = ( staticSwitch16_g11866 * pow( saferPower7_g11867 , temp_output_9_0_g11867 ) );
				#else
				float3 staticSwitch17_g11866 = staticSwitch16_g11866;
				#endif
				#ifdef _ENABLEADDCOLOR_ON
				float4 appendResult6_g11866 = (float4(( ( staticSwitch17_g11866 * _AddColorFade ) + (temp_output_1_0_g11866).rgb ) , temp_output_1_0_g11866.a));
				float4 staticSwitch5_g11865 = appendResult6_g11866;
				#else
				float4 staticSwitch5_g11865 = staticSwitch13_g11850;
				#endif
				#ifdef _ENABLEALPHATINT_ON
				float4 temp_output_1_0_g11869 = staticSwitch5_g11865;
				float3 lerpResult4_g11869 = lerp( (temp_output_1_0_g11869).rgb , (_AlphaTintColor).rgb , ( ( 1.0 - temp_output_1_0_g11869.a ) * step( _AlphaTintMinAlpha , temp_output_1_0_g11869.a ) * _AlphaTintFade ));
				float4 appendResult13_g11869 = (float4(lerpResult4_g11869 , temp_output_1_0_g11869.a));
				float4 staticSwitch11_g11865 = appendResult13_g11869;
				#else
				float4 staticSwitch11_g11865 = staticSwitch5_g11865;
				#endif
				float4 temp_output_1_0_g11870 = staticSwitch11_g11865;
				float3 temp_output_6_0_g11870 = (_StrongTintTint).rgb;
				#ifdef _STRONGTINTMASKTOGGLE_ON
				float2 uv_StrongTintMask = IN.ase_texcoord2.xy * _StrongTintMask_ST.xy + _StrongTintMask_ST.zw;
				float4 tex2DNode23_g11870 = tex2D( _StrongTintMask, uv_StrongTintMask );
				float3 staticSwitch21_g11870 = ( temp_output_6_0_g11870 * ( (tex2DNode23_g11870).rgb * tex2DNode23_g11870.a ) );
				#else
				float3 staticSwitch21_g11870 = temp_output_6_0_g11870;
				#endif
				#ifdef _STRONGTINTCONTRASTTOGGLE_ON
				float4 break2_g11872 = temp_output_1_0_g11870;
				float temp_output_9_0_g11871 = max( _StrongTintContrast , 0.0 );
				float saferPower7_g11871 = abs( ( ( ( break2_g11872.x + break2_g11872.x + break2_g11872.y + break2_g11872.y + break2_g11872.y + break2_g11872.z ) / 6.0 ) + ( 0.1 * max( ( 1.0 - temp_output_9_0_g11871 ) , 0.0 ) ) ) );
				float3 staticSwitch22_g11870 = ( pow( saferPower7_g11871 , temp_output_9_0_g11871 ) * staticSwitch21_g11870 );
				#else
				float3 staticSwitch22_g11870 = staticSwitch21_g11870;
				#endif
				#ifdef _ENABLESTRONGTINT_ON
				float3 lerpResult7_g11870 = lerp( (temp_output_1_0_g11870).rgb , staticSwitch22_g11870 , _StrongTintFade);
				float4 appendResult9_g11870 = (float4(lerpResult7_g11870 , (temp_output_1_0_g11870).a));
				float4 staticSwitch7_g11865 = appendResult9_g11870;
				#else
				float4 staticSwitch7_g11865 = staticSwitch11_g11865;
				#endif
				float4 temp_output_2_0_g11873 = staticSwitch7_g11865;
				#ifdef _ENABLESHADOW_ON
				float4 break4_g11875 = temp_output_2_0_g11873;
				float3 appendResult5_g11875 = (float3(break4_g11875.r , break4_g11875.g , break4_g11875.b));
				float2 appendResult2_g11874 = (float2(_MainTex_TexelSize.z , _MainTex_TexelSize.w));
				float4 appendResult85_g11873 = (float4(_ShadowColor.r , _ShadowColor.g , _ShadowColor.b , ( _ShadowFade * tex2D( _MainTex, ( finalUV146 - ( ( 100.0 / appendResult2_g11874 ) * _ShadowOffset ) ) ).a )));
				float4 break6_g11875 = appendResult85_g11873;
				float3 appendResult7_g11875 = (float3(break6_g11875.r , break6_g11875.g , break6_g11875.b));
				float temp_output_11_0_g11875 = ( ( 1.0 - break4_g11875.a ) * break6_g11875.a );
				float temp_output_32_0_g11875 = ( break4_g11875.a + temp_output_11_0_g11875 );
				float4 appendResult18_g11875 = (float4(( ( ( appendResult5_g11875 * break4_g11875.a ) + ( appendResult7_g11875 * temp_output_11_0_g11875 ) ) * ( 1.0 / max( temp_output_32_0_g11875 , 0.01 ) ) ) , temp_output_32_0_g11875));
				float4 staticSwitch82_g11873 = appendResult18_g11875;
				#else
				float4 staticSwitch82_g11873 = temp_output_2_0_g11873;
				#endif
				float4 break4_g11876 = staticSwitch82_g11873;
				#ifdef _ENABLECUSTOMFADE_ON
				float staticSwitch8_g11756 = 1.0;
				#else
				float staticSwitch8_g11756 = IN.ase_color.a;
				#endif
				#ifdef _ENABLESMOKE_ON
				float staticSwitch9_g11756 = 1.0;
				#else
				float staticSwitch9_g11756 = staticSwitch8_g11756;
				#endif
				float customVertexAlpha193 = staticSwitch9_g11756;
				float4 appendResult5_g11876 = (float4(break4_g11876.r , break4_g11876.g , break4_g11876.b , ( break4_g11876.a * customVertexAlpha193 )));
				float4 temp_output_344_0 = appendResult5_g11876;
				float4 temp_output_1_0_g11877 = temp_output_344_0;
				float4 appendResult8_g11877 = (float4(( (temp_output_1_0_g11877).rgb * (IN.ase_color).rgb ) , temp_output_1_0_g11877.a));
				#ifdef _VERTEXTINTFIRST_ON
				float4 staticSwitch342 = temp_output_344_0;
				#else
				float4 staticSwitch342 = appendResult8_g11877;
				#endif
				float4 lerpResult125 = lerp( ( originalColor191 * IN.ase_color ) , staticSwitch342 , fullFade123);
				#if defined(_SHADERFADING_NONE)
				float4 staticSwitch143 = staticSwitch342;
				#elif defined(_SHADERFADING_FULL)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_MASK)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_DISSOLVE)
				float4 staticSwitch143 = lerpResult125;
				#elif defined(_SHADERFADING_SPREAD)
				float4 staticSwitch143 = lerpResult125;
				#else
				float4 staticSwitch143 = staticSwitch342;
				#endif
				float4 temp_output_7_0_g11884 = staticSwitch143;
				#ifdef _BAKEDMATERIAL_ON
				float4 appendResult2_g11884 = (float4(( (temp_output_7_0_g11884).rgb / max( temp_output_7_0_g11884.a , 1E-05 ) ) , temp_output_7_0_g11884.a));
				float4 staticSwitch6_g11884 = appendResult2_g11884;
				#else
				float4 staticSwitch6_g11884 = temp_output_7_0_g11884;
				#endif
				float4 temp_output_340_0 = staticSwitch6_g11884;
				
				o.Normal = fixed3( 0, 0, 1 );
				o.Occlusion = 1;
				o.Alpha = temp_output_340_0.a;
				float AlphaClipThreshold = _AlphaClip;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_SHADOW_ON
					if (unity_LightShadowBias.z != 0.0)
						clip(o.Alpha - AlphaClipThresholdShadow);
					#ifdef _ALPHATEST_ON
					else
						clip(o.Alpha - AlphaClipThreshold);
					#endif
				#else
					#ifdef _ALPHATEST_ON
						clip(o.Alpha - AlphaClipThreshold);
					#endif
				#endif

				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif

				#ifdef UNITY_STANDARD_USE_DITHER_MASK
					half alphaRef = tex3D(_DitherMaskLOD, float3(vpos.xy*0.25,o.Alpha*0.9375)).a;
					clip(alphaRef - 0.01);
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = IN.pos.z;
				#endif

				SHADOW_CASTER_FRAGMENT(IN)
			}
			ENDCG
		}
		
	}
	CustomEditor "SpriteShadersUltimate.SSUShaderGUI"
	
	Fallback Off
}
/*ASEBEGIN
Version=19002
248;166;1837;1046;-5453.095;233.3275;1;True;False
Node;AmplifyShaderEditor.TexturePropertyNode;502;1144.955,1000.087;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RelayNode;105;1425.709,1004.581;Inherit;False;1;0;SAMPLER2D;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;157;1638.393,1000.485;Inherit;False;spriteTexture;-1;True;1;0;SAMPLER2D;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;411;-3081.191,-3627.324;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;363;-3105.9,-3835.053;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;483;-2758.191,-3709.324;Inherit;False;_ScreenTiling;576;;11656;f5939d1b891718b468aa402ddf2c75e0;0;2;1;FLOAT2;0,0;False;12;SAMPLER2D;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;482;-2458.77,-3622.03;Inherit;False;_WorldTiling;571;;11657;5075a3cd4854af640aa8d277732c8893;0;2;1;FLOAT2;0,0;False;12;SAMPLER2D;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexelSizeNode;438;1931.978,1144.547;Inherit;False;-1;1;0;SAMPLER2D;;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;460;-2186.147,-3620.607;Inherit;False;originalUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;432;2227.661,1200.654;Inherit;False;False;False;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;435;2252.064,903.7132;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;373;-2003.491,-3335.527;Inherit;False;Property;_SpriteSheetRect;Sprite Sheet Rect;16;0;Create;True;0;0;0;False;0;False;0,0,1,1;0,0,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;469;-1956.314,-3488.134;Inherit;False;_PixelArtUV_1;-1;;11658;0e4f4d9760e013e4ea49a4cc7c42c155;0;2;1;FLOAT2;0,0;False;2;SAMPLER2D;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;449;-1697.482,-3566.466;Inherit;False;Property;_Keyword1;Keyword 1;14;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;427;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;374;-1729.456,-3326.936;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;375;-1731.491,-3207.527;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;434;2534.932,1066.922;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;450;-1448.75,-3526.053;Inherit;False;uvAfterPixelArt;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;376;-1493.884,-3326.405;Inherit;False;spriteRectMin;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;377;-1494.584,-3224.105;Inherit;False;spriteRectMax;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FloorOpNode;433;2745.127,1095.848;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;436;2945.127,1149.848;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;341;2716.61,1541.034;Inherit;False;ShaderTime;33;;11665;06a15e67904f217499045f361bad56e7;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;370;-1198.361,-3311.903;Inherit;False;Remap2D;-1;;11664;f79f855c0a5c94649b58f3d8127375ae;0;5;13;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;1,1;False;5;FLOAT2;0,0;False;6;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;437;3093.581,1032.267;Inherit;False;Property;_PixelPerfectSpace;Pixel Perfect Space;13;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;441;3148.675,1260.326;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.StaticSwitch;366;-905.3608,-3353.903;Inherit;False;Property;_SpriteSheetFix;Sprite Sheet Fix;15;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;101;2238.417,1562.63;Inherit;True;Property;_UberNoiseTexture;Uber Noise Texture;44;0;Create;True;0;0;0;False;0;False;b8d18cd117976254d94a812a0bfc336e;b8d18cd117976254d94a812a0bfc336e;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RegisterLocalVarNode;237;2948.598,1594.428;Inherit;False;shaderTime;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;439;3389.449,1182.896;Inherit;False;ShaderSpace;20;;11666;be729ef05db9c224caec82a3516038dc;0;2;61;FLOAT2;0,0;False;3;SAMPLER2D;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;475;-584.1226,-3343.809;Inherit;False;fixedUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RelayNode;99;2506.903,1635.005;Inherit;False;1;0;SAMPLER2D;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;477;-582.6621,-3244.321;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;235;3662.626,1220.644;Inherit;False;shaderPosition;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;159;2660.32,1715.519;Inherit;False;uberNoiseTexture;-1;True;1;0;SAMPLER2D;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;365;-372.9774,-3275.82;Inherit;False;_UberInteractive;504;;11667;f8a4d7008519ad249b29e4a9381f963f;0;2;9;FLOAT;0;False;3;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;184;-340.3444,-2931.6;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;253;-334.9839,-3014.146;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RelayNode;84;-178.5299,-3184.649;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;79;-47.4363,-2946.944;Inherit;False;_FullDistortion;438;;11669;62960fe27c1c398408207bb462ffd10e;0;3;195;FLOAT2;0,0;False;160;FLOAT2;0,0;False;194;SAMPLER2D;;False;2;FLOAT2;174;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;83;355.5295,-3077.305;Inherit;False;Property;_EnableShine;Enable Shine;437;0;Create;True;0;0;0;False;0;False;1;0;0;True;;Toggle;2;Key0;Key1;Reference;77;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;254;496.0161,-2937.146;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;188;476.926,-2831.78;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;81;747.3577,-2918.135;Inherit;False;_DirectionalDistortion;426;;11672;30e6ac39427ee11419083602d572972f;0;3;182;FLOAT2;0,0;False;160;FLOAT2;0,0;False;181;SAMPLER2D;;False;2;FLOAT2;174;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;244;685.5539,-740.5018;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;160;658.3505,-580.0461;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.StaticSwitch;82;1064.56,-3052.917;Inherit;False;Property;_EnableShine;Enable Shine;425;0;Create;True;0;0;0;False;0;False;1;0;0;True;;Toggle;2;Key0;Key1;Reference;75;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;243;665.8691,-664.6964;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;53;1165.579,-2580.498;Inherit;False;Property;_HologramFade;Hologram: Fade;206;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;255;1530.016,-2640.146;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;182;1537.252,-2554.561;Inherit;False;hologramFade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;173;1547.656,-2458.612;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RelayNode;38;1602.103,-2721.81;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;174;1562.277,-2362.367;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;102;919.0109,-667.4209;Inherit;False;_GlitchPre;221;;11675;b8ad29d751d87bd4d9cbf14898be6163;0;3;19;FLOAT;0;False;18;FLOAT2;0,0;False;16;SAMPLER2D;;False;2;FLOAT;15;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;204;4801.255,1506.884;Inherit;True;Property;_FadingMask;Fading: Mask;32;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;210;4416.092,2114.768;Inherit;False;Property;_FadingWidth;Fading: Width;29;0;Create;True;0;0;0;False;0;False;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;219;4515.66,1836.189;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;230;4630.415,2681.855;Inherit;False;Property;_FadingNoiseFactor;Fading: Noise Factor;30;0;Create;True;0;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;208;4401.642,1937.264;Inherit;False;Property;_FadingNoiseScale;Fading: Noise Scale;31;0;Create;True;0;0;0;False;0;False;0.2,0.2;0.2,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RegisterLocalVarNode;152;1246.533,-755.1426;Inherit;False;glitchFade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;228;4626.359,2471.966;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.Vector2Node;229;4660.594,2553.942;Inherit;False;Property;_FadingPosition;Fading: Position;28;0;Create;True;0;0;0;False;0;False;0,0;0.2,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;261;4535.53,1746.504;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;154;1243.538,-600.6849;Inherit;False;glitchPosition;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;52;1839.482,-2552.931;Inherit;False;_HologramUV;214;;11677;7c71b1b031ffcbe48805e17b94671163;0;5;77;FLOAT;0;False;55;FLOAT;0;False;76;SAMPLER2D;;False;37;FLOAT2;0,0;False;39;SAMPLER2D;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;122;3995.099,1279.389;Inherit;False;Property;_FadingFade;Fading: Fade;27;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;175;2375.652,-2434.015;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;231;4899.456,2440.854;Inherit;False;_UberSpreadFade;-1;;11718;777ca8ab10170fb48b24b7cd1c44f075;0;7;27;FLOAT2;0,0;False;22;FLOAT;0;False;18;SAMPLER2D;0;False;25;FLOAT2;0,0;False;23;FLOAT2;0,0;False;21;FLOAT;0;False;26;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;223;4905.316,1828.167;Inherit;False;_UberDissolveFade;-1;;11716;cb957eb9b67f4f243aa8ba0547208263;0;5;21;FLOAT2;0,0;False;1;FLOAT;0;False;16;SAMPLER2D;0;False;18;FLOAT2;0,0;False;20;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;256;2394.016,-2741.146;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;59;2242.011,-2636.393;Inherit;False;Property;_EnableShine;Enable Shine;205;0;Create;True;0;0;0;False;0;False;1;0;0;True;;Toggle;2;Key0;Key1;Reference;56;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;153;2413.1,-2367.982;Inherit;False;152;glitchFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;155;2400.558,-2511.178;Inherit;False;154;glitchPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;292;5091.561,1459.408;Inherit;False;ShaderMasker;-1;;11715;3d25b55dbfdd24f48b9bd371bdde0e97;0;2;1;FLOAT;0;False;2;SAMPLER2D;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;103;2715.721,-2556.586;Inherit;False;_GlitchUV;233;;11720;2addb21417fb5d745a5abfe02cbcd453;0;5;23;FLOAT;0;False;13;FLOAT2;0,0;False;22;SAMPLER2D;;False;3;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;139;5964.391,1365.661;Inherit;False;Property;_ShaderFading;Shader Fading;26;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;5;None;Full;Mask;Dissolve;Spread;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;62;3040.934,-2600.272;Inherit;False;Property;_EnableShine;Enable Shine;220;0;Create;True;0;0;0;False;0;False;1;0;0;True;;Toggle;2;Key0;Key1;Reference;57;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;123;6281.453,1414.289;Inherit;False;fullFade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;259;2999.016,-2403.146;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;176;2993.115,-2238.526;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;177;3015.115,-2149.526;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;258;2972.016,-2323.146;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;129;3305.944,-1988.403;Inherit;False;123;fullFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;484;3382.41,-2373.518;Inherit;False;_UberTransformUV;443;;11722;894b1de51a5f4c74cbe7828262f1344b;0;5;25;FLOAT;0;False;26;FLOAT2;0,0;False;1;FLOAT2;0,0;False;18;SAMPLER2D;0;False;3;SAMPLER2D;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;131;3266.866,-2134.612;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;130;3651.881,-2106.533;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;145;3940.446,-2146.193;Inherit;False;Property;_UberFading;Uber Fading;26;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;4;NONE;Key1;Key2;Key3;Reference;139;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleRemainderNode;486;4222.693,-2046.305;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;487;4449.801,-1972.639;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleRemainderNode;488;4664.06,-2056.074;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;381;5159.958,-1852.951;Inherit;False;377;spriteRectMax;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;380;5166.152,-1941.533;Inherit;False;376;spriteRectMin;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;485;4859.071,-2156.215;Inherit;False;Property;_TilingFix;Tiling Fix;17;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;378;5475.994,-2043.135;Inherit;False;Remap2D;-1;;11734;f79f855c0a5c94649b58f3d8127375ae;0;5;13;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;1,1;False;5;FLOAT2;0,0;False;6;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;382;5739.72,-1830.532;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMinOpNode;383;5870.72,-1934.532;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;371;6107.095,-2156.984;Inherit;False;Property;_SpriteSheetFix1;Sprite Sheet Fix;15;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;366;False;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;463;6361.842,-2067.504;Inherit;False;460;originalUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;452;6307.805,-1976.09;Inherit;False;450;uvAfterPixelArt;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;442;6299.205,-1884.911;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;470;6618.802,-2035.91;Inherit;False;_PixelArtUV_2;-1;;11735;4b65626ba2313ca40a96813b19044794;0;4;21;FLOAT2;0,0;False;17;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;SAMPLER2D;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;427;7006.263,-2158.381;Inherit;False;Property;_PixelPerfectUV;Pixel Perfect UV;14;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;146;7280.32,-2157.36;Inherit;False;finalUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;147;-2226.428,-390.3136;Inherit;False;146;finalUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;189;-2234.721,-201.4758;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;471;-1990.589,-315.832;Inherit;False;_UberSample;542;;11736;1028d755b36e2b04da25c3b882a2e2ec;0;2;1;FLOAT2;0,0;False;2;SAMPLER2D;;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;358;-1921.027,-115.7738;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;356;-1686.027,-106.7738;Inherit;False;ColorMultiply;-1;;11755;1f51da7edd80c06488c56d28bc096dec;0;2;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;354;-1350.718,-125.6577;Inherit;False;Property;_Keyword0;Keyword 0;12;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;342;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;191;-1018.041,-121.917;Inherit;False;originalColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;179;-1036.103,27.02582;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;149;-977.8615,164.14;Inherit;False;146;finalUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;343;-737.796,35.8288;Inherit;False;_UberCustomAlpha;553;;11756;d68af6e3188f53845b23cf6e39df15fe;0;3;1;COLOR;0,0,0,0;False;6;SAMPLER2D;0;False;7;FLOAT2;0,0;False;2;FLOAT;12;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;148;-614.0953,-401.0159;Inherit;False;146;finalUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;240;-683.451,-246.0232;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;180;-687.1025,-126.9742;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;181;-423.344,-436.9742;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;239;-404.7228,-125.1053;Inherit;False;_UberGenerated;527;;11761;52defa3f7cca25740a6a77f065edb382;0;4;10;FLOAT;0;False;8;SAMPLER2D;0;False;7;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;178;-455.0759,-519.7366;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;242;-432.6598,-617.0601;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;183;-37.83691,-91.99512;Inherit;False;182;hologramFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;361;-136.8644,-329.1863;Inherit;False;_UberColor;72;;11765;db48f560e502b78409f7fbe481a93597;0;6;39;FLOAT;0;False;40;FLOAT2;0,0;False;1;FLOAT2;0,0;False;24;SAMPLER2D;0;False;3;COLOR;0,0,0,0;False;5;SAMPLER2D;0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;51;183.7499,-168.0946;Inherit;False;_Hologram;207;;11801;76082a965d84d0e4da33b2cff51b3691;0;3;42;FLOAT;0;False;40;FLOAT;0;False;1;COLOR;1,1,1,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;161;687.0067,-317.0453;Inherit;False;154;glitchPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;56;438.7324,-454.4984;Inherit;False;Property;_EnableHologram;Enable Hologram;205;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;163;668.7452,-235.3598;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;162;704.0067,-153.0455;Inherit;False;152;glitchFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;104;973.7388,-316.5438;Inherit;False;_Glitch;227;;11804;97a01281f94bcc04fbb9a7c1cd328f08;0;5;34;FLOAT;0;False;31;FLOAT2;0,0;False;33;SAMPLER2D;;False;29;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;164;250.219,-2894.672;Inherit;False;fullDistortionAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;247;1456.499,-578.4069;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;246;1400.383,-493.8317;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;57;1278.486,-397.6114;Inherit;False;Property;_EnableGlitch;Enable Glitch;220;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;171;1364.183,-273.4383;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;473;1656.25,-345.9998;Inherit;False;_UberEffect;238;;11808;93c7a07f758a0814998210619e8ad1cb;0;4;40;FLOAT;0;False;41;FLOAT2;0,0;False;3;COLOR;0,0,0,0;False;37;SAMPLER2D;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;166;1853.428,-195.4143;Inherit;False;164;fullDistortionAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;78;2104.106,-267.8359;Inherit;False;AlphaMultiply;-1;;11848;d24974f7959982d48aab81e9e7692f35;0;2;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;167;1037.158,-2834.03;Inherit;False;directionalDistortionAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;168;2616.17,-223.2014;Inherit;False;167;directionalDistortionAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;77;2492.977,-348.4961;Inherit;False;Property;_EnableFullDistortion;Enable Full Distortion;437;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;76;3014.405,-219.2272;Inherit;False;AlphaMultiply;-1;;11849;d24974f7959982d48aab81e9e7692f35;0;2;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;75;3434.708,-336.5002;Inherit;False;Property;_EnableDirectionalDistortion;Enable Directional Distortion;425;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;249;3627.646,-39.2937;Inherit;False;235;shaderPosition;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;169;3611.012,-138.045;Inherit;False;159;uberNoiseTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;480;3955.587,-349.0083;Inherit;False;146;finalUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;479;3931.587,-267.0083;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;193;-400.3044,37.52343;Inherit;False;customVertexAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;248;3912.165,-179.9706;Inherit;False;_UberFading;366;;11850;f8f5d1f402d6b694f9c47ef65b4ae91d;0;3;18;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;17;SAMPLER2D;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;478;4200.417,-206.2577;Inherit;False;_UberColorFinal;45;;11865;6ac57aba23ea6404ba71b6806ea93971;0;3;14;FLOAT2;0,0;False;15;SAMPLER2D;;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;196;4178.044,-19.14569;Inherit;False;193;customVertexAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;348;4610.214,-299.2399;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;344;4548.086,-90.46628;Inherit;False;AlphaMultiply;-1;;11876;d24974f7959982d48aab81e9e7692f35;0;2;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;200;3977.696,393.365;Inherit;False;Constant;_ZeroVector;Zero Vector;67;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;353;4889.3,-222.093;Inherit;False;ColorMultiply;-1;;11877;1f51da7edd80c06488c56d28bc096dec;0;2;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;194;4622.439,201.8054;Inherit;False;191;originalColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;476;3958.333,566.3407;Inherit;False;475;fixedUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;199;4171.966,521.7499;Inherit;False;_Squish;522;;11879;6d6a73cc3433bad4186f7028cad3d98c;0;2;82;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;195;4877.096,136.7432;Inherit;False;TintVertex;-1;;11878;b0b94dd27c0f3da49a89feecae766dcc;0;1;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;342;5134.784,-128.8904;Inherit;False;Property;_VertexTintFirst;Vertex Tint First;12;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;All;9;1;FLOAT4;0,0,0,0;False;0;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;5;FLOAT4;0,0,0,0;False;6;FLOAT4;0,0,0,0;False;7;FLOAT4;0,0,0,0;False;8;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;124;4962.062,258.7927;Inherit;False;123;fullFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;125;5403.405,74.92608;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;198;4453.426,400.9801;Inherit;False;Property;_EnableSquish;Enable Squish;521;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;251;4509.569,562.3449;Inherit;False;237;shaderTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;143;5663.995,-69.69315;Inherit;False;Property;_UberFading;Uber Fading;26;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;4;NONE;Key1;Key2;Key3;Reference;139;True;True;All;9;1;FLOAT4;0,0,0,0;False;0;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;5;FLOAT4;0,0,0,0;False;6;FLOAT4;0,0,0,0;False;7;FLOAT4;0,0,0,0;False;8;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;141;4826.724,532.0556;Inherit;False;123;fullFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;424;4759.72,401.6135;Inherit;False;_UberTransformOffset;488;;11880;ee5e9e731457b2342bdb306bdb8d2401;0;2;8;FLOAT;0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;340;6010.595,-19.64009;Inherit;False;BakingHandler;18;;11884;f63dfe0dc7c747c43b593d357b168fa0;0;1;7;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;121;5091.355,506.223;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;503;6414.627,233.3515;Inherit;False;Property;_AlphaClip;Alpha Clip;1;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;497;6281.262,104.0129;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;500;5991.064,398.0179;Inherit;False;146;finalUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;501;6204.6,355.6115;Inherit;False;_3DLitStuff;2;;11885;a67b30a19d14d0c498b219c7041d411f;0;1;11;FLOAT2;0,0;False;4;FLOAT3;21;FLOAT;9;FLOAT;10;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;142;5362.269,308.0164;Inherit;False;Property;_UberFading;Uber Fading;26;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;4;NONE;Key1;Key2;Key3;Reference;139;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;187;489.4036,-2756.916;Inherit;False;157;spriteTexture;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;507;6765.377,59.35749;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;Deferred;0;3;Deferred;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Deferred;True;2;True;17;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;xboxseries;ps4;playstation;psp2;n3ds;wiiu;switch;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;509;6765.377,59.35749;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ShadowCaster;0;5;ShadowCaster;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;504;6765.377,59.35749;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ExtraPrePass;0;0;ExtraPrePass;6;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=ForwardBase;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;505;6765.377,59.35749;Float;False;True;-1;2;SpriteShadersUltimate.SSUShaderGUI;0;14;Sprite Shaders Ultimate/3D Lit Cutout BuiltIn SSU;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ForwardBase;0;1;ForwardBase;18;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;False;0;;0;0;Standard;40;Workflow,InvertActionOnDeselection;1;0;Surface;0;0;  Blend;0;0;  Refraction Model;0;0;  Dither Shadows;1;0;Two Sided;0;638021455051577892;Deferred Pass;1;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;  Use Shadow Threshold;0;0;Receive Shadows;1;0;GPU Instancing;1;0;LOD CrossFade;0;638021455199064546;Built-in Fog;1;0;Ambient Light;1;0;Meta Pass;1;0;Add Pass;1;0;Override Baked GI;0;0;Extra Pre Pass;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Fwd Specular Highlights Toggle;0;0;Fwd Reflections Toggle;0;0;Disable Batching;0;0;Vertex Position,InvertActionOnDeselection;1;0;0;6;False;True;True;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;506;6765.377,59.35749;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ForwardAdd;0;2;ForwardAdd;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;True;4;1;False;;1;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;True;1;LightMode=ForwardAdd;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;508;6765.377,59.35749;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;Meta;0;4;Meta;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;;0;0;Standard;0;False;0
WireConnection;105;0;502;0
WireConnection;157;0;105;0
WireConnection;483;1;363;0
WireConnection;483;12;411;0
WireConnection;482;1;483;0
WireConnection;482;12;411;0
WireConnection;438;0;157;0
WireConnection;460;0;482;0
WireConnection;432;0;438;0
WireConnection;469;1;460;0
WireConnection;469;2;411;0
WireConnection;449;1;460;0
WireConnection;449;0;469;0
WireConnection;374;0;373;1
WireConnection;374;1;373;2
WireConnection;375;0;373;3
WireConnection;375;1;373;4
WireConnection;434;0;435;0
WireConnection;434;1;432;0
WireConnection;450;0;449;0
WireConnection;376;0;374;0
WireConnection;377;0;375;0
WireConnection;433;0;434;0
WireConnection;436;0;433;0
WireConnection;436;1;432;0
WireConnection;370;13;450;0
WireConnection;370;5;376;0
WireConnection;370;6;377;0
WireConnection;437;1;435;0
WireConnection;437;0;436;0
WireConnection;366;1;450;0
WireConnection;366;0;370;0
WireConnection;237;0;341;0
WireConnection;439;61;437;0
WireConnection;439;3;441;0
WireConnection;475;0;366;0
WireConnection;99;0;101;0
WireConnection;235;0;439;0
WireConnection;159;0;99;0
WireConnection;365;9;477;0
WireConnection;365;3;475;0
WireConnection;84;0;365;0
WireConnection;79;195;253;0
WireConnection;79;160;84;0
WireConnection;79;194;184;0
WireConnection;83;1;84;0
WireConnection;83;0;79;174
WireConnection;81;182;254;0
WireConnection;81;160;83;0
WireConnection;81;181;188;0
WireConnection;82;1;83;0
WireConnection;82;0;81;174
WireConnection;182;0;53;0
WireConnection;38;0;82;0
WireConnection;102;19;244;0
WireConnection;102;18;243;0
WireConnection;102;16;160;0
WireConnection;152;0;102;15
WireConnection;154;0;102;0
WireConnection;52;77;255;0
WireConnection;52;55;182;0
WireConnection;52;76;173;0
WireConnection;52;37;38;0
WireConnection;52;39;174;0
WireConnection;231;27;261;0
WireConnection;231;22;122;0
WireConnection;231;18;228;0
WireConnection;231;25;208;0
WireConnection;231;23;229;0
WireConnection;231;21;210;0
WireConnection;231;26;230;0
WireConnection;223;21;261;0
WireConnection;223;1;122;0
WireConnection;223;16;219;0
WireConnection;223;18;208;0
WireConnection;223;20;210;0
WireConnection;59;1;38;0
WireConnection;59;0;52;0
WireConnection;292;1;122;0
WireConnection;292;2;204;0
WireConnection;103;23;256;0
WireConnection;103;13;155;0
WireConnection;103;22;175;0
WireConnection;103;3;153;0
WireConnection;103;1;59;0
WireConnection;139;1;122;0
WireConnection;139;0;122;0
WireConnection;139;2;292;0
WireConnection;139;3;223;0
WireConnection;139;4;231;0
WireConnection;62;1;59;0
WireConnection;62;0;103;0
WireConnection;123;0;139;0
WireConnection;484;25;259;0
WireConnection;484;26;258;0
WireConnection;484;1;62;0
WireConnection;484;18;176;0
WireConnection;484;3;177;0
WireConnection;130;0;131;0
WireConnection;130;1;484;0
WireConnection;130;2;129;0
WireConnection;145;1;484;0
WireConnection;145;0;130;0
WireConnection;145;2;130;0
WireConnection;145;3;130;0
WireConnection;145;4;130;0
WireConnection;486;0;145;0
WireConnection;487;0;486;0
WireConnection;488;0;487;0
WireConnection;485;1;145;0
WireConnection;485;0;488;0
WireConnection;378;13;485;0
WireConnection;378;2;380;0
WireConnection;378;3;381;0
WireConnection;382;0;378;0
WireConnection;382;1;380;0
WireConnection;383;0;382;0
WireConnection;383;1;381;0
WireConnection;371;1;485;0
WireConnection;371;0;383;0
WireConnection;470;21;463;0
WireConnection;470;17;452;0
WireConnection;470;1;371;0
WireConnection;470;2;442;0
WireConnection;427;1;371;0
WireConnection;427;0;470;0
WireConnection;146;0;427;0
WireConnection;471;1;147;0
WireConnection;471;2;189;0
WireConnection;356;1;471;0
WireConnection;356;2;358;0
WireConnection;354;1;471;0
WireConnection;354;0;356;0
WireConnection;191;0;354;0
WireConnection;343;1;191;0
WireConnection;343;6;179;0
WireConnection;343;7;149;0
WireConnection;239;10;240;0
WireConnection;239;8;180;0
WireConnection;239;7;148;0
WireConnection;239;1;343;0
WireConnection;361;39;240;0
WireConnection;361;40;242;0
WireConnection;361;1;148;0
WireConnection;361;24;178;0
WireConnection;361;3;239;0
WireConnection;361;5;181;0
WireConnection;51;42;240;0
WireConnection;51;40;183;0
WireConnection;51;1;361;0
WireConnection;56;1;361;0
WireConnection;56;0;51;0
WireConnection;104;34;244;0
WireConnection;104;31;161;0
WireConnection;104;33;163;0
WireConnection;104;29;162;0
WireConnection;104;1;56;0
WireConnection;164;0;79;0
WireConnection;57;1;56;0
WireConnection;57;0;104;0
WireConnection;473;40;247;0
WireConnection;473;41;246;0
WireConnection;473;3;57;0
WireConnection;473;37;171;0
WireConnection;78;1;473;0
WireConnection;78;2;166;0
WireConnection;167;0;81;0
WireConnection;77;1;473;0
WireConnection;77;0;78;0
WireConnection;76;1;77;0
WireConnection;76;2;168;0
WireConnection;75;1;77;0
WireConnection;75;0;76;0
WireConnection;193;0;343;12
WireConnection;248;18;249;0
WireConnection;248;1;75;0
WireConnection;248;17;169;0
WireConnection;478;14;480;0
WireConnection;478;15;479;0
WireConnection;478;3;248;0
WireConnection;344;1;478;0
WireConnection;344;2;196;0
WireConnection;353;1;344;0
WireConnection;353;2;348;0
WireConnection;199;82;200;0
WireConnection;199;1;476;0
WireConnection;195;1;194;0
WireConnection;342;1;353;0
WireConnection;342;0;344;0
WireConnection;125;0;195;0
WireConnection;125;1;342;0
WireConnection;125;2;124;0
WireConnection;198;1;200;0
WireConnection;198;0;199;0
WireConnection;143;1;342;0
WireConnection;143;0;125;0
WireConnection;143;2;125;0
WireConnection;143;3;125;0
WireConnection;143;4;125;0
WireConnection;424;8;251;0
WireConnection;424;2;198;0
WireConnection;340;7;143;0
WireConnection;121;1;424;0
WireConnection;121;2;141;0
WireConnection;497;0;340;0
WireConnection;501;11;500;0
WireConnection;142;1;424;0
WireConnection;142;0;121;0
WireConnection;142;2;121;0
WireConnection;142;3;121;0
WireConnection;142;4;121;0
WireConnection;505;0;340;0
WireConnection;505;1;501;0
WireConnection;505;2;501;21
WireConnection;505;4;501;10
WireConnection;505;5;501;9
WireConnection;505;7;497;3
WireConnection;505;8;503;0
WireConnection;505;15;142;0
ASEEND*/
//CHKSM=54C1AA81AE0BAD4057CB634D343BD5BE4607BE23
