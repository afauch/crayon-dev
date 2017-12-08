// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace Crayon.Core
{

	/// <summary>
	/// FadeDirection enum used in the CrayonExtension methods.
	/// </summary>
	public enum FadeDirection
	{
		In,
		Out
	}

	/// <summary>
	/// This class handles variations from the publicly-exposed extension methods and reconfigures them for use by
	/// more generic coroutines.
	/// </summary>
	public static class CrayonRouter
	{

		/// <summary>
		/// Generic method to handle all opacity transitions.
		/// </summary>
		public static void Fade(GameObject gameObject, FadeDirection fadeDirection, float opacity, float duration, Easing easing, bool destroy, string cubicBezier)
		{
			Transform[] objectAndChildren = gameObject.GetComponentsInChildren<Transform> ();
			// If there are child objects, loop through them
			for(int i = 0; i < objectAndChildren.Length; i++) {
				// TODO: Provide a check to see whether the child component already has this code.
				// I was hitting issues before where child objects were not fading in because they had their own
				// components calling the same coroutine

				// Check to see if there's a renderer before trying to tween
				if (objectAndChildren [i].gameObject.GetComponent<Renderer> () != null)
				{
					CrayonRunner.Instance.Run (CrayonTweenCoroutines.FadeCoroutine (objectAndChildren [i].gameObject, fadeDirection, opacity, duration, easing, destroy, cubicBezier));
				}
				else
				{
					Debug.LogWarningFormat ("{0} will not tween because it does not contain a Renderer component.", objectAndChildren [i].gameObject.name);
				}

			}
		}

		/// <summary>
		/// Generic method to handle all color-only transitions.
		/// </summary>
		public static void TweenColor(GameObject gameObject, Color targetColor, float duration, Easing easing, string cubicBezier)
		{

			// Is this a sprite renderer?
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
			if (sr != null)
			{
				Debug.LogWarning ("Is a sprite");
				CrayonRunner.Instance.Run (CrayonTweenCoroutines.TweenSpriteCoroutine(gameObject, sr.color, targetColor, duration, easing, cubicBezier));
			}
			else
			{
				// Create instance of material
				Renderer r = gameObject.GetComponent<Renderer> ();
				if (r == null) {
					Debug.LogWarningFormat ("{0} needs a renderer component.", gameObject.name);
					return;
				}
				Material targetMaterial = Object.Instantiate (r.material);
				targetMaterial.SetColor ("_Color", targetColor);
				CrayonRunner.Instance.Run (CrayonTweenCoroutines.TweenMaterialCoroutine (gameObject, null, targetMaterial, duration, easing, cubicBezier));
			}
		}

		/// <summary>
		/// Generic method to handle all material transitions.
		/// </summary>
		public static void TweenMaterial(GameObject gameObject, Material targetMaterial, float duration, Easing easing, string cubicBezier)
		{
			if (targetMaterial != null) {
				Material targetMaterialInstance = Object.Instantiate (targetMaterial);
				CrayonRunner.Instance.Run (CrayonTweenCoroutines.TweenMaterialCoroutine (gameObject, null, targetMaterialInstance, duration, easing, cubicBezier));
			} else {
				Debug.LogWarningFormat ("Material for {0} has not been assigned.", gameObject.name);
			}
		}

		/// <summary>
		/// Generic method to handle all position transitions.
		/// </summary>
		public static void TweenPosition(GameObject gameObject, Vector3 targetPosition, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier)
		{
			CrayonRunner.Instance.Run (CrayonTweenCoroutines.TweenPositionCoroutine(gameObject, targetPosition, duration, easing, destroy, isRelative, cubicBezier));
		}

		/// <summary>
		/// Generic method to handle all rotation transitions.
		/// </summary>
		public static void TweenRotation(GameObject gameObject, Quaternion targetRotation, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier)
		{
			CrayonRunner.Instance.Run (CrayonTweenCoroutines.TweenRotationCoroutine(gameObject, targetRotation, duration, easing, destroy, isRelative, cubicBezier));
		}

		/// <summary>
		/// Generic method to handle all scale transitions.
		/// </summary>
		public static void TweenScale(GameObject gameObject, Vector3 targetScale, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier)
		{
			CrayonRunner.Instance.Run (CrayonTweenCoroutines.TweenScaleCoroutine(gameObject, targetScale, duration, easing, destroy, isRelative, cubicBezier));
		}

	}		

}