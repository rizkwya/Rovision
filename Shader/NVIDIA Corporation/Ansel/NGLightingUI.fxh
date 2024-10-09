//Stochastic Screen Space Ray Tracing
//Written by MJ_Ehsan for Reshade
//Version 0.9.3 - UI

//license
//CC0 ^_^

#if !NGL_HYBRID_MODE
uniform int GI <
	ui_type = "combo";
	ui_label = "Mode";
	ui_items = "Reflection\0GI\0";
> = 1;
#endif

uniform bool UseCatrom <
	ui_label = "Use Catrom resampling";
	ui_tooltip = "Uses Catrom resampling for Upscaling and  Reprojection. Slower but sharper.";
> = 0; // false

uniform bool SharpenGI <
	ui_label = "Sharpen the GI";
	ui_tooltip = "(No performance impact) Further improves the edge clarity. Try Catrom resampling first tho.";
> = 1; // true

uniform float fov <
	ui_label = "Field of View";
	ui_type = "slider";
	ui_category = "Ray Tracing";
	ui_tooltip = "Set it according to the game's field of view.";
	ui_min = 18;
	ui_max = 120;
> = 50;

uniform float BUMP <
	ui_label = "Bump mapping";
	ui_type = "slider";
	ui_category = "Ray Tracing";
	ui_tooltip = "Adds tiny details to the lighting.";
	ui_min = 0.0;
	ui_max = 1;
> = 0.5;

uniform float roughness <
	ui_label = "Roughness";
	ui_type = "slider";
	ui_category = "Ray Tracing";
	ui_tooltip = "Blurriness of the reflections.";
	ui_min = 0.0;
	ui_max = 0.999;
> = 0.4;

uniform bool TemporalRefine <
	ui_label = "Temporal Refining (EXPERIMENTAL)";
	ui_category = "Ray Tracing (Advanced)";
	ui_tooltip = "EXPERIMENTAL! Expect issues\n"
				 "Reduce (Surface depth) and increase (Step Length Jitter)\n"
				 "Then enable this option to have more accurate Reflection/GI.";
	ui_category_closed = true;
> = 0;

uniform float RAYINC <
	ui_label = "Ray Increment";
	ui_type = "slider";
	ui_category = "Ray Tracing (Advanced)";
	ui_tooltip = "Increases ray length at the cost of accuracy.";
	ui_category_closed = true;
	ui_min = 1;
	ui_max = 2;
> = 2;

uniform uint UI_RAYSTEPS <
	ui_label = "Max Steps"; 
	ui_type = "slider";
	ui_category = "Ray Tracing (Advanced)";
	ui_tooltip = "Increases ray length at the cost of performance.";
	ui_category_closed = true;
	ui_min = 1;
	ui_max = 32;
> = 32;

uniform float RAYDEPTH <
	ui_label = "Surface depth";
	ui_type = "slider";
	ui_category = "Ray Tracing (Advanced)";
	ui_tooltip = "More coherency at the cost of accuracy.";
	ui_category_closed = true;
	ui_min = 0.05;
	ui_max = 10;
> = 5;

uniform float MVErrorTolerance <
	ui_label = "Motion Vector\nError Tolerance";
	ui_type = "slider";
	ui_category = "Denoiser (Advanced)";
	ui_tooltip = "Lower values are  more sensitive to\n"
				 "Motion Estimation errors. Thus relying\n"
				 "more on spatial filtering rather than\n"
				 "temporal accumulation";
	ui_category_closed = true;
	ui_step = 0.01;
> = 0.96;

uniform int MAX_Frames <
	ui_label = "History Length";
	ui_type = "slider";
	ui_category = "Denoiser (Advanced)";
	ui_tooltip = "Higher values increase smoothness\n"
				 "while preserves more details. But\n"
				 "introduces more temporal lag.";
	ui_category_closed = true;
	ui_min = 8;
	ui_max = 8;
> = 8;

uniform float Sthreshold <
	ui_label = "Spatial Denoiser\nThreshold";
	ui_type = "slider";
	ui_category = "Denoiser (Advanced)";
	ui_tooltip = "Reduces noise at the cost of detail.";
	ui_category_closed = true;
> = 0.003;

static const bool HLFix = 1;

uniform float EXP <
	ui_label = "Reflection rim fade";
	ui_type = "slider";
	ui_category = "Blending Options";
	ui_tooltip = "Blending intensity for shiny materials.";
	ui_min = 1;
	ui_max = 10;
> = 4;

uniform float AO_Radius_Background <
	ui_label = "Image AO";
	ui_type = "slider";
	ui_category = "Blending Options";
	ui_tooltip = "Radius of AO for the image.";
> = 1;

uniform float AO_Radius_Reflection <
	ui_label = "GI AO";
	ui_type = "slider";
	ui_category = "Blending Options";
	ui_tooltip = "Radius of AO for the GI.";
> = 1;

uniform float AO_Intensity <
	ui_label = "AO Power";
	ui_type = "slider";
	ui_category = "Blending Options";
	ui_tooltip = "AO falloff curve";
> = 1;

uniform float depthfade <
	ui_label = "Depth Fade";
	ui_type = "slider";
	ui_category = "Blending Options";
	ui_tooltip = "Higher values decrease the intesity on distant objects.\n"
				 "Reduces blending issues within in-game fogs.";
	ui_min = 0;
	ui_max = 1;
> = 1;

uniform bool LinearConvert <
	ui_type = "radio";
	ui_label = "sRGB to Linear";
	ui_category = "Color Management";
	ui_tooltip = "Disable if the game is HDR";
	ui_category_closed = true;
> = 1;

uniform float2 SatExp <
	ui_type = "slider";
	ui_label = "Saturation\n& Exposure";
	ui_category = "Color Management";
	ui_tooltip = "Left slider is Saturation. Right one is Exposure.";
	ui_category_closed = true;
	ui_min = 0;
	ui_max = 4;
> = float2(4,4);

uniform uint debug <
	ui_type = "combo";
	ui_items = "None\0Lighting\0Depth\0Normal\0Accumulation\0Roughness Map\0";
	ui_category = "Extra";
	ui_category_closed = true;
	ui_min = 0;
	ui_max = 5;
> = 0;

uniform float SkyDepth <
	ui_type = "slider";
	ui_label = "Sky Masking Depth";
	ui_tooltip = "Minimum depth to consider sky and exclude from the calculation.";
	ui_category = "Extra";
	ui_category_closed = true;
> = 0.99;
