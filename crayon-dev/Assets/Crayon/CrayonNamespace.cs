using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon
{

	// Easing
	public enum Easing {
		Back,
		Bounce,
		Circular,
		Cubic,
		Elastic,
		Exponential,
		Linear,
		Quadratic,
		Quartic,
		Quintic
	}

	// FadeDirection
	public enum FadeDirection {
		In,
		Out
	}

	// Stores instances of Defaults
	public static class Defaults {

		public const float _duration = 0.8f;
		public const Easing _easing = Easing.Linear;

	}

	// Extension Methods
	public static class ExtensionMethods {

		// ---
		// GameObject Extensions
		// ---

		// Specify no duration - use a default
		public static void FadeIn(this GameObject gameObject) {

			Fade (gameObject, FadeDirection.In, Defaults._duration, Defaults._easing);

		}

		// Specify duration only
		public static void FadeIn(this GameObject gameObject, float duration) {

			Fade (gameObject, FadeDirection.In, duration, Defaults._easing);

		}

		// Specify duration and easing
		public static void FadeIn(this GameObject gameObject, float duration, Easing easing) {

			Fade (gameObject, FadeDirection.In, duration, easing);

		}

		// Specify duration and easing as string 
		public static void FadeIn(this GameObject gameObject, float duration, string easing) {

			Fade (gameObject, FadeDirection.In, duration, Utils.GetEasing(easing));

		}


		// Generic method to handle all fades
		private static void Fade(GameObject gameObject, FadeDirection fadeDirection, float duration, Easing easing) {

			// TODO: Run on each child, which may be different types
			CrayonRunner.Instance.Run (Fade (gameObject, duration, easing));
			foreach (Transform t in gameObject.transform) {
				CrayonRunner.Instance.Run (Fade (t.gameObject, duration, easing));
			}

		}

		// ---
		// Renderer Extensions
		// ---
	
		// Given a renderer, return a material that can be used for fading color/opacity
		public static Material GetUsableMaterial(this Renderer renderer) {

			Material material = renderer.material;
			string shaderName = material.shader.name;

			switch (shaderName) {
			case "Standard":
				Debug.Log ("Standard shader used");
				// TODO: Add conditionals to make this more efficient
				// Turn on FadeMode
				StandardShaderUtils.ChangeRenderMode(material,StandardShaderUtils.BlendMode.Fade);
				break;
			default:
				Debug.Log ("Default called");
				break;
			}

			// TODO: Is this efficient?
			material.enableInstancing = true;

			return material;
				
		}
			
		// ---
		// Coroutines
		// ---

		// TODO: This will need to become more generic to handle fade ins, outs, instantiates, destroys, etc.
		// Actually fade the material
		private static IEnumerator Fade(GameObject g, float duration, Easing easing) {

			Debug.Log ("Fade Called");

			// elapsedTime
			float elapsedTime = 0;

			// get the starting material and color
			Renderer r = g.GetComponent<Renderer>();

			// What material are we using

			Material m = r.GetUsableMaterial();

			Color endColor = m.GetColor ("_Color");
			Color startColor = new Color(endColor.r, endColor.g, endColor.b, 0.0f);

			while (elapsedTime < duration) {
				// this lerps color
				float t = elapsedTime / duration;
				t = Utils.GetT (t, easing);

				elapsedTime += Time.deltaTime;
				Color currentColor = Color.Lerp (startColor, endColor, t);
				m.SetColor ("_Color", currentColor);
				yield return null;
			}

		}

	}
			

	// Methods for doing things like 
	public static class Utils {

		// Returns an Easing enum from a string input
		public static Easing GetEasing(string easing) {
			easing = easing.ToLower ();
			switch (easing)
			{
				case "back": return Easing.Back;
				case "bounce": return Easing.Bounce;
				case "bircular": return Easing.Circular;
				case "cubic": return Easing.Cubic;
				case "elastic": return Easing.Elastic;
				case "exponential": return Easing.Exponential;
				case "linear": return Easing.Linear;
				case "quadratic": return Easing.Quadratic;
				case "quartic": return Easing.Quartic;
				case "quintic": return Easing.Quintic;
				default: return Easing.Linear;
			}
		}

		// Returns t based on easing
		public static float GetT(float t, Easing easing) {
			switch (easing) {
				case Easing.Back: return EasingTypes.Back.InOut (t);
				case Easing.Bounce: return EasingTypes.Bounce.InOut (t);
				case Easing.Circular: return EasingTypes.Circular.InOut (t);
				case Easing.Cubic: return EasingTypes.Cubic.InOut (t);
				case Easing.Elastic: return EasingTypes.Elastic.InOut (t);
				case Easing.Exponential: return EasingTypes.Exponential.InOut (t);
				case Easing.Linear: return EasingTypes.Linear (t);
				case Easing.Quadratic: return EasingTypes.Quadratic.InOut (t);
				case Easing.Quartic: return EasingTypes.Quartic.InOut (t);
				case Easing.Quintic: return EasingTypes.Quintic.InOut (t);
				default: return t;
			}
		}

	}

}