using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon
{

	// Easing
	public enum Easing {
		BackIn,
		BackOut,
		BackInOut,
		BounceIn,
		BounceOut,
		BounceInOut,
		CircularIn,
		CircularOut,
		CircularInOut,
		CubicIn,
		CubicOut,
		CubicInOut,
		ElasticIn,
		ElasticOut,
		ElasticInOut,
		ExponentialIn,
		ExponentialOut,
		ExponentialInOut,
		Linear,
		QuadraticIn,
		QuadraticOut,
		QuadraticInOut,
		QuarticIn,
		QuarticOut,
		QuarticInOut,
		QuinticIn,
		QuinticOut,
		QuinticInOut
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

		// Fade - Ins

		// Specify no duration - use a default
		public static void FadeIn(this GameObject gameObject) {

			Fade (gameObject, FadeDirection.In, Defaults._duration, Defaults._easing, false);

		}

		// Specify duration only
		public static void FadeIn(this GameObject gameObject, float duration) {

			Fade (gameObject, FadeDirection.In, duration, Defaults._easing, false);

		}

		// Specify duration and easing
		public static void FadeIn(this GameObject gameObject, float duration, Easing easing) {

			Fade (gameObject, FadeDirection.In, duration, easing, false);

		}

		// Specify duration and easing as string 
		public static void FadeIn(this GameObject gameObject, float duration, string easing) {

			Fade (gameObject, FadeDirection.In, duration, Utils.GetEasing(easing), false);

		}

		// Fade - Ins

		// Specify no duration - use a default
		public static void FadeInNew(this GameObject gameObject) {

			gameObject = GameObject.Instantiate (gameObject, Vector3.zero, Quaternion.identity);
			Fade (gameObject, FadeDirection.In, Defaults._duration, Defaults._easing, false);

		}

		// Specify duration only
		public static void FadeInNew(this GameObject gameObject, float duration) {

			gameObject = GameObject.Instantiate (gameObject, Vector3.zero, Quaternion.identity);
			Fade (gameObject, FadeDirection.In, duration, Defaults._easing, false);

		}

		// Specify duration and easing
		public static void FadeInNew(this GameObject gameObject, float duration, Easing easing) {

			gameObject = GameObject.Instantiate (gameObject, Vector3.zero, Quaternion.identity);
			Fade (gameObject, FadeDirection.In, duration, easing, false);

		}

		// Specify all parameters
		public static void FadeInNew(this GameObject gameobject, Transform transform, float duration, Easing easing) {

			gameobject = GameObject.Instantiate (gameobject, transform.position, transform.rotation);
			Fade (gameobject, FadeDirection.In, duration, easing, false);

		}

		// Fade - Outs

		// Specify no duration - use a default
		public static void FadeOut(this GameObject gameObject) {

			Fade (gameObject, FadeDirection.Out, Defaults._duration, Defaults._easing, false);

		}

		// Specify duration only
		public static void FadeOut(this GameObject gameObject, float duration) {

			Fade (gameObject, FadeDirection.Out, duration, Defaults._easing, false);

		}

		// Specify duration and easing
		public static void FadeOut(this GameObject gameObject, float duration, Easing easing) {

			Fade (gameObject, FadeDirection.Out, duration, easing, false);

		}
		// Specify duration and easing as string 
		public static void FadeOut(this GameObject gameObject, float duration, string easing) {

			Fade (gameObject, FadeDirection.Out, duration, Utils.GetEasing(easing), false);

		}


		// Fade - Out and Destroy

		// Specify no duration - use a default
		public static void FadeOutAndDestroy(this GameObject gameObject) {

			Fade (gameObject, FadeDirection.Out, Defaults._duration, Defaults._easing, true);

		}

		// Specify duration only
		public static void FadeOutAndDestroy(this GameObject gameObject, float duration) {

			Fade (gameObject, FadeDirection.Out, duration, Defaults._easing, true);

		}

		// Specify duration and easing
		public static void FadeOutAndDestroy(this GameObject gameObject, float duration, Easing easing) {

			Fade (gameObject, FadeDirection.Out, duration, easing, true);

		}
		// Specify duration and easing as string 
		public static void FadeOutAndDestroy(this GameObject gameObject, float duration, string easing) {

			Fade (gameObject, FadeDirection.Out, duration, Utils.GetEasing(easing), true);

		}

		// Generic method to handle all fades
		private static void Fade(GameObject gameObject, FadeDirection fadeDirection, float duration, Easing easing, bool destroy) {

			Transform[] objectAndChildren = gameObject.GetComponentsInChildren<Transform> ();
			// If there are child objects
			for(int i = 0; i < objectAndChildren.Length; i++) {
				// TODO: Provide a check to see whether the child component already has this code.
				// I was hitting issues before where child objects were not fading in because they had their own
				// components calling the same coroutine
				CrayonRunner.Instance.Run (FadeCoroutine (objectAndChildren[i].gameObject, fadeDirection, duration, easing, destroy));
			}

		}


		// ---
		// Coroutines
		// ---

		// TODO: This will need to become more generic to handle fade ins, outs, instantiates, destroys, etc.
		// Actually fade the material
		private static IEnumerator FadeCoroutine(GameObject gameObject, FadeDirection fadeDirection, float duration, Easing easing, bool destroy) {

			// Debug.Log ("Fade Called on GameObject " + gameObject.name);

			// elapsedTime
			float elapsedTime = 0;

			// get the starting material and color
			Renderer r = gameObject.GetComponent<Renderer>();

			// what material are we using?
			Material m = Utils.GetUsableMaterial(r);

			Color endColor;
			Color startColor;
			// Shift depending on whether the object is fading in or out
			if (fadeDirection == FadeDirection.In) {

				// get the color instance with the right alpha value
				endColor = Utils.GetUsableColor (r.material);
				startColor = new Color (endColor.r, endColor.g, endColor.b, 0.0f);

			} else {

				// reverse if it's fading out
				startColor = Utils.GetUsableColor (r.material);
				endColor = new Color (startColor.r, startColor.g, startColor.b, 0.0f);

			}

			while (elapsedTime < duration) {

				// this interpolates color
				float t = elapsedTime / duration;
				// shift 't' based on the easing function
				t = Utils.GetT (t, easing);
				elapsedTime += Time.deltaTime;
			
				Color currentColor = Color.Lerp (startColor, endColor, t);
				m.SetColor ("_Color", currentColor);
				yield return null;
			}

			if (destroy)
				GameObject.Destroy (gameObject);

		}

	}
			

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