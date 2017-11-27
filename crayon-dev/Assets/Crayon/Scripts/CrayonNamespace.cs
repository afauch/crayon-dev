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
		public static void FadeIn(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier) {
			Fade (gameObject, FadeDirection.In, 0.0f, duration, easing, false, cubicBezier);
		}


		// Fade-Ins (New)

		// Normal parameters
		public static void FadeInNew(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier) {
			gameObject = GameObject.Instantiate (gameObject, Vector3.zero, Quaternion.identity);
			Fade (gameObject, FadeDirection.In, 0.0f, duration, easing, false, cubicBezier);
		}
		// Specify target transform as well
		public static void FadeInNew(this GameObject gameobject, Transform transform, float duration, Easing easing, string cubicBezier = Defaults._cubicBezier) {
			gameobject = GameObject.Instantiate (gameobject, transform.position, transform.rotation);
			Fade (gameobject, FadeDirection.In, 0.0f, duration, easing, false, cubicBezier);
		}

		// Fade - Outs

		// Normal parameters
		public static void FadeOut(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier) {
			Fade (gameObject, FadeDirection.Out, 0.0f, duration, easing, false, cubicBezier);
		}
		// Specify easing as string 
		public static void FadeOut(this GameObject gameObject, float duration, string easing, string cubicBezier = Defaults._cubicBezier) {
			Fade (gameObject, FadeDirection.Out, 0.0f, duration, Utils.GetEasing(easing), false, cubicBezier);
		}


		// Fade - Out and Destroy
	
		// Specify no duration - use a default
		public static void FadeOutAndDestroy(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier) {
			Fade (gameObject, FadeDirection.Out, 0.0f, duration, easing, true, cubicBezier);
		}
		// Specify duration and easing as string 
		public static void FadeOutAndDestroy(this GameObject gameObject, float duration, string easing, string cubicBezier = Defaults._cubicBezier) {
			Fade (gameObject, FadeDirection.Out, 0.0f, duration, Utils.GetEasing(easing), true, cubicBezier);
		}

		// Set Opacity
		// Normal parameters
		public static void SetOpacity(this GameObject gameObject, float opacity, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier) {
			Fade (gameObject, FadeDirection.Out, opacity, duration, easing, false, cubicBezier);
		}

		// Set Color

		// Normal parameters
		public static void SetColor(this GameObject gameObject, Color color, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier) {
			TweenColor (gameObject, color, duration, easing, cubicBezier);
		}

		// Use hex color
		public static void SetColor(this GameObject gameObject, string hexColor) {
			// TODO: Add error handling here
			Color color;
			ColorUtility.TryParseHtmlString (hexColor, out color);
			TweenColor (gameObject, color, Defaults._duration, Defaults._easing, Defaults._cubicBezier);
		}

		public static void SetColor(this GameObject gameObject, string hexColor, float opacity) {
			// TODO: Add error handling here
			// Color color = Color.white;
			Color color;
			ColorUtility.TryParseHtmlString (hexColor, out color);
			color.a = opacity;
			TweenColor (gameObject, color, Defaults._duration, Defaults._easing, Defaults._cubicBezier);
		}

		// Set Texture
		public static void SetTexture(this GameObject gameObject, Texture texture, float duration = Defaults._duration, Easing easing = Defaults._easing) {
			// TODO: Texture Logic goes here
		}

		// Set Material
		// TODO: Create a method for tweening to a specific material which might be supplied via the inspector

		// Set Position
		public static void SetPosition(this GameObject gameObject, Vector3 targetPosition, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			TweenPosition (gameObject, targetPosition, duration, easing, false, false, cubicBezier);
		}

		// TODO: Rename this and add optional parameters to fold into the SetPosition method
//		public static void SetPositionCB(this GameObject gameObject, Vector3 targetPosition, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = ""){
//			TweenPosition (gameObject, targetPosition, duration, easing, false, false, cubicBezier);
//		}
			
		// Set Relative Position
		public static void SetRelativePosition(this GameObject gameObject, Vector3 targetPosition, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			TweenPosition (gameObject, targetPosition, duration, easing, false, true, cubicBezier);
		}
//		public static void SetRelativePositionCB(this GameObject gameObject, Vector3 targetPosition, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = ""){
//			TweenPosition (gameObject, targetPosition, duration, easing, false, true, cubicBezier);
//		}


		// Set Rotation
		public static void SetRotation(this GameObject gameObject, Vector3 targetRotation, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			Quaternion targetQuaternionRotation = Quaternion.Euler (targetRotation);
			TweenRotation (gameObject, targetQuaternionRotation, duration, easing, false, false, cubicBezier);
		}

		// Set Relative Rotation
		public static void SetRelativeRotation(this GameObject gameObject, Vector3 targetRotation, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			Quaternion targetQuaternionRotation = Quaternion.Euler (targetRotation);
			TweenRotation (gameObject, targetQuaternionRotation, duration, easing, false, true, cubicBezier);
		}

		// Set Scale
		public static void SetScale(this GameObject gameObject, Vector3 targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			TweenScale (gameObject, targetScale, duration, easing, false, false, cubicBezier);
		}
		public static void SetScale(this GameObject gameObject, float targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			Vector3 targetVector3Scale = new Vector3 (targetScale, targetScale, targetScale);
			TweenScale (gameObject, targetVector3Scale, duration, easing, false, false, cubicBezier);
		}

		// Set Relative Scale
		public static void SetRelativeScale(this GameObject gameObject, Vector3 targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			TweenScale (gameObject, targetScale, duration, easing, false, true, cubicBezier);
		}
		public static void SetRelativeScale(this GameObject gameObject, float targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier){
			Vector3 targetVector3Scale = new Vector3 (targetScale, targetScale, targetScale);
			TweenScale (gameObject, targetVector3Scale, duration, easing, false, true, cubicBezier);
		}

		// ---
		// State methods
		// ---

		public static void SetState(this GameObject gameObject, CrayonStateType stateType, string customState = "") {
			gameObject.GetComponent<CrayonStateManager> ().ChangeState (stateType, customState);
		}

		// ---
		// Generic methods
		// ---

		// Generic method to handle all fades
		private static void Fade(GameObject gameObject, FadeDirection fadeDirection, float opacity, float duration, Easing easing, bool destroy, string cubicBezier) {

			Transform[] objectAndChildren = gameObject.GetComponentsInChildren<Transform> ();
			// If there are child objects
			for(int i = 0; i < objectAndChildren.Length; i++) {
				// TODO: Provide a check to see whether the child component already has this code.
				// I was hitting issues before where child objects were not fading in because they had their own
				// components calling the same coroutine
				CrayonRunner.Instance.Run (FadeCoroutine (objectAndChildren[i].gameObject, fadeDirection, opacity, duration, easing, destroy, cubicBezier));
			}
		}

		private static void TweenColor(GameObject gameObject, Color targetColor, float duration, Easing easing, string cubicBezier) {
			// Create instance of material
			Renderer r = gameObject.GetComponent<Renderer>();
			if (r == null)
				return;
			Material targetMaterial = Object.Instantiate(r.material);
			targetMaterial.SetColor("_Color", targetColor);
			// Call Coroutine
			CrayonRunner.Instance.Run (TweenColorCoroutine (gameObject, null, targetMaterial, duration, easing, cubicBezier));
		}

		private static void TweenPosition(GameObject gameObject, Vector3 targetPosition, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier) {
			CrayonRunner.Instance.Run (TweenPositionCoroutine(gameObject, targetPosition, duration, easing, destroy, isRelative, cubicBezier));
		}

		private static void TweenRotation(GameObject gameObject, Quaternion targetRotation, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier) {
			CrayonRunner.Instance.Run (TweenRotationCoroutine(gameObject, targetRotation, duration, easing, destroy, isRelative, cubicBezier));
		}

		private static void TweenScale(GameObject gameObject, Vector3 targetScale, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier) {
			CrayonRunner.Instance.Run (TweenScaleCoroutine(gameObject, targetScale, duration, easing, destroy, isRelative, cubicBezier));
		}

		// ---
		// Coroutines
		// ---

		// TODO: This will need to become more generic to handle fade ins, outs, instantiates, destroys, etc.
		// Actually fade the material
		private static IEnumerator FadeCoroutine(GameObject gameObject, FadeDirection fadeDirection, float opacity, float duration, Easing easing, bool destroy, string cubicBezier) {
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
				startColor = new Color (endColor.r, endColor.g, endColor.b, opacity);
			} else {
				// reverse if it's fading out
				startColor = Utils.GetUsableColor (r.material);
				endColor = new Color (startColor.r, startColor.g, startColor.b, opacity);
			}

			if (duration < 0.0001f) {
				m.SetColor ("_Color", endColor);
			} else {

				while (elapsedTime < duration) {
					// this interpolates color
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					Color currentColor = Color.Lerp (startColor, endColor, t);
					m.SetColor ("_Color", currentColor);
					yield return null;
				}
				m.SetColor ("_Color", endColor);
			}
			if (destroy)
				GameObject.Destroy (gameObject);
		}


		private static IEnumerator TweenColorCoroutine(GameObject gameObject, Material startMaterial, Material endMaterial, float duration, Easing easing, string cubicBezier) {


			// Debug.Log ("Fade Called on GameObject " + gameObject.name);
			// Debug.Log("original color for " + gameObject.name + " in TweenColorCoroutine: " + gameObject.GetComponent<Renderer>().material.color);
			// elapsedTime
			float elapsedTime = 0;

			// get the starting material and color
			Renderer r = gameObject.GetComponent<Renderer>();
			// this is the material we'll tween
			Material m = Utils.GetUsableMaterial(r);

			// TODO: Condense the following code into Lambda functions

			// If start material is null, assume we're tweening FROM the current material
			if (startMaterial == null) {
				// Debug.Log ("StartMaterial is null");
				startMaterial = Utils.GetUsableMaterial (r);
			}

			// If end material is null, assume we're tweening TO the current material
			if (endMaterial == null) {
				// Debug.Log ("EndMaterial is null");				
				endMaterial = Utils.GetUsableMaterial (r);
			}

			// Shortcut: Don't Lerp if not necessary
			if (duration < 0.0001f) {
				m.CopyPropertiesFromMaterial(endMaterial);
			} else {

				// TODO: Optimize this for performance. No need to switch to 'Fade'
				// mode if you're tweening between two Opaque Materials
				startMaterial = Utils.GetUsableMaterial (startMaterial);
				endMaterial = Utils.GetUsableMaterial (endMaterial);

				while (elapsedTime < duration) {

					// this interpolates color
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					m.Lerp (startMaterial, endMaterial, t);
					yield return null;
				}
				m.CopyPropertiesFromMaterial(endMaterial);
			}

		}

		private static IEnumerator TweenPositionCoroutine(GameObject gameObject, Vector3 targetPosition, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier) {

			// Debug.Log ("TweenPosition Called on GameObject " + gameObject.name + " and isRelative is " + isRelative);
			// elapsedTime
			float elapsedTime = 0;
			// are we moving in absolute terms
			// or relative to current position
			Vector3 startPosition = gameObject.transform.localPosition;
			Vector3 endPosition;
			if (isRelative) {
				// the supplied Vector3 is relative to the original position
				endPosition = startPosition + targetPosition;
			} else {
				endPosition = targetPosition;
			}

			if (duration < 0.0001f) {
				gameObject.transform.localPosition = endPosition;
			} else {

				while (elapsedTime < duration) {
					// this interpolates position
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					// set the position
					Vector3 interpolatedPosition = Vector3.Lerp (startPosition, endPosition, t);
					gameObject.transform.localPosition = interpolatedPosition;
					yield return null;
				}
				gameObject.transform.localPosition = endPosition;
			}
			if (destroy)
				GameObject.Destroy (gameObject);

		}

		private static IEnumerator TweenRotationCoroutine(GameObject gameObject, Quaternion targetRotation, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier) {

			// Debug.Log ("TweenRotation Called on GameObject " + gameObject.name + " and isRelative is " + isRelative);
			// elapsedTime
			float elapsedTime = 0;
			// are we moving in absolute terms
			// or relative to current position
			Quaternion startRotation = gameObject.transform.localRotation;
			Quaternion endRotation;
			if (isRelative) {
				// the supplied Quaternion is relative to the original position
				endRotation = startRotation * targetRotation;
			} else {
				endRotation = targetRotation;
			}

			if (duration < 0.0001f) {
				gameObject.transform.localRotation = endRotation;
			} else {
				while (elapsedTime < duration) {
					// this interpolates position
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					// set the position
					Quaternion interpolatedRotation = Quaternion.Slerp (startRotation, endRotation, t);
					gameObject.transform.localRotation = interpolatedRotation;
					yield return null;
				}
				gameObject.transform.localRotation = endRotation;
			}
			if (destroy)
				GameObject.Destroy (gameObject);

		}

		private static IEnumerator TweenScaleCoroutine(GameObject gameObject, Vector3 targetScale, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier) {

			// Debug.Log ("TweenScale Called on GameObject " + gameObject.name + " and isRelative is " + isRelative);
			// elapsedTime
			float elapsedTime = 0;
			// are we moving in absolute terms
			// or relative to current position
			Vector3 startScale = gameObject.transform.localScale;
			Vector3 endScale;
			if (isRelative) {
				// the supplied Vector3 is relative to the original scale
				endScale = new Vector3(startScale.x * targetScale.x, startScale.y * targetScale.y, startScale.z * targetScale.z);
			} else {
				endScale = targetScale;
			}

			if (duration < 0.0001f) {
				gameObject.transform.localScale = endScale;
			} else {

				while (elapsedTime < duration) {
					// this interpolates position
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					// set the position
					Vector3 interpolatedScale = Vector3.Lerp (startScale, endScale, t);
					gameObject.transform.localScale = interpolatedScale;
					yield return null;
				}
				gameObject.transform.localScale = endScale;
			}
			if (destroy)
				GameObject.Destroy (gameObject);

		}

	}		

}