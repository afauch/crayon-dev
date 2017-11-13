using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon
{
	
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

		// Fade-Ins (New)

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

		// Set Color

		// No parameters - use defaults
		public static void SetColor(this GameObject gameObject, Color color) {
			TweenColor (gameObject, color, Defaults._duration, Defaults._easing);
		}

		// Add duration
		public static void SetColor(this GameObject gameObject, Color color, float duration) {
			TweenColor (gameObject, color, duration, Defaults._easing);
		}

		// Add duration
		public static void SetColor(this GameObject gameObject, Color color, float duration, Easing easing) {
			TweenColor (gameObject, color, duration, easing);
		}

		// No parameters - use defaults
		public static void SetColor(this GameObject gameObject, string hexColor) {
			Color color = Color.white;
			ColorUtility.TryParseHtmlString (hexColor, out color);
			TweenColor (gameObject, color, Defaults._duration, Defaults._easing);
		}

		// ---
		// Generic methods
		// ---

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

		private static void TweenColor(GameObject gameObject, Color targetColor, float duration, Easing easing) {
			// Create instance of material
			Material targetMaterial = Object.Instantiate(gameObject.GetComponent<Renderer> ().material);
			targetMaterial.SetColor("_Color", targetColor);
			// Call Coroutine
			CrayonRunner.Instance.Run (TweenColorCoroutine (gameObject, null, targetMaterial, duration, easing));
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

		private static IEnumerator TweenColorCoroutine(GameObject gameObject, Material startMaterial, Material endMaterial, float duration, Easing easing) {

			// Debug.Log ("Fade Called on GameObject " + gameObject.name);
			Debug.Log("original color for " + gameObject.name + " in TweenColorCoroutine: " + gameObject.GetComponent<Renderer>().material.color);
			// elapsedTime
			float elapsedTime = 0;

			// get the starting material and color
			Renderer r = gameObject.GetComponent<Renderer>();

			// what material are we using?
			Material m = Utils.GetUsableMaterial(r);

			// If start material is null, assume we're tweening FROM the current material
			if (startMaterial == null) {
				Debug.Log ("StartMaterial is null");
				startMaterial = Utils.GetUsableMaterial (r);
			}

			// If end material is null, assume we're tweening TO the current material
			if (endMaterial == null) {
				Debug.Log ("EndMaterial is null");				
				endMaterial = Utils.GetUsableMaterial (r);
			}

			Debug.Log ("Start Material Color: " + startMaterial.color);
			Debug.Log ("End Material Color: " + endMaterial.color);

			while (elapsedTime < duration) {

				// this interpolates color
				float t = elapsedTime / duration;
				// shift 't' based on the easing function
				t = Utils.GetT (t, easing);
				elapsedTime += Time.deltaTime;

				Debug.Log ("Material lerping " + gameObject.name + "t" + t);

				// Try this
				m.Lerp(startMaterial, endMaterial, t);

				yield return null;
			}

		}


	}		

}