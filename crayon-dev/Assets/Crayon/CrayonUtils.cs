using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {

	// Methods for doing things like 
	public static class Utils {

		// Returns an Easing enum from a string input
		public static Easing GetEasing(string easing) {
			easing = easing.ToLower ();
			switch (easing)
			{
			// TODO: define all versions here

			case "back": return Easing.BackInOut;

			case "bounce": return Easing.BounceInOut;

			case "bircular": return Easing.CircularInOut;

			case "cubic": return Easing.CubicInOut;

			case "elastic": return Easing.ElasticInOut;

			case "exponential": return Easing.ExponentialInOut;

			case "linear": return Easing.Linear;

			case "quadratic": return Easing.QuadraticInOut;

			case "quartic": return Easing.QuarticInOut;

			case "quintic": return Easing.QuinticInOut;

			default: return Easing.Linear;
			}
		}

		// Returns t based on easing
		public static float GetT(float t, Easing easing) {
			switch (easing) {

			case Easing.BackIn: return EasingTypes.Back.In (t);
			case Easing.BackOut: return EasingTypes.Back.Out (t);				
			case Easing.BackInOut: return EasingTypes.Back.InOut (t);

			case Easing.BounceIn: return EasingTypes.Bounce.In (t);
			case Easing.BounceOut: return EasingTypes.Bounce.Out (t);				
			case Easing.BounceInOut: return EasingTypes.Bounce.InOut (t);

			case Easing.CircularIn: return EasingTypes.Circular.In (t);
			case Easing.CircularOut: return EasingTypes.Circular.Out (t);
			case Easing.CircularInOut: return EasingTypes.Circular.InOut (t);

			case Easing.CubicIn: return EasingTypes.Cubic.In (t);
			case Easing.CubicOut: return EasingTypes.Cubic.Out (t);				
			case Easing.CubicInOut: return EasingTypes.Cubic.InOut (t);

			case Easing.ElasticIn: return EasingTypes.Elastic.In (t);
			case Easing.ElasticOut: return EasingTypes.Elastic.Out (t);
			case Easing.ElasticInOut: return EasingTypes.Elastic.InOut (t);

			case Easing.ExponentialIn: return EasingTypes.Exponential.In (t);
			case Easing.ExponentialOut: return EasingTypes.Exponential.Out (t);				
			case Easing.ExponentialInOut: return EasingTypes.Exponential.InOut (t);

			case Easing.Linear: return EasingTypes.Linear (t);

			case Easing.QuadraticIn: return EasingTypes.Quadratic.In (t);
			case Easing.QuadraticOut: return EasingTypes.Quadratic.Out (t);				
			case Easing.QuadraticInOut: return EasingTypes.Quadratic.InOut (t);

			case Easing.QuarticIn: return EasingTypes.Quartic.In (t);
			case Easing.QuarticOut: return EasingTypes.Quartic.Out (t);
			case Easing.QuarticInOut: return EasingTypes.Quartic.InOut (t);

			case Easing.QuinticIn: return EasingTypes.Quintic.In (t);
			case Easing.QuinticOut: return EasingTypes.Quintic.Out (t);				
			case Easing.QuinticInOut: return EasingTypes.Quintic.InOut (t);

			default: return t;
			}
		}

		// Given a renderer, return a material that can be used for fading color/opacity
		public static Material GetUsableMaterial(Renderer renderer) {

			// Debug.Log ("GetUsableMaterial called on " + renderer.material.name);

			Material material = renderer.material;
			string shaderName = material.shader.name;

			switch (shaderName) {
			case "Standard":
				// TODO: Add conditionals to make this more efficient
				// Turn on FadeMode
				StandardShaderUtils.ChangeRenderMode(material,StandardShaderUtils.BlendMode.Fade);
				break;
			default:
				// Debug.Log ("Default called for " + renderer.gameObject.name);
				break;
			}

			// TODO: Is this efficient?
			material.enableInstancing = true;

			return material;

		}

		// Given a renderer and material, return an RGBA color that can be used for fading
		public static Color GetUsableColor(Material material) {

			// TODO: This method could be eliminated if color logic is not required
			Color c;
			c = material.GetColor ("_Color");
			c = new Color(c.r, c.g, c.b, c.a);
			return c;

		}

	}

}
