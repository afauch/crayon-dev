// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using UnityEngine;

namespace Crayon.Core
{

	/// <summary>
	/// This class contains the coroutines that actually execute the tweens asynchronously.
	/// </summary>
	public class CrayonTweenCoroutines {

		/// <summary>
		/// Coroutine for fading opacity.
		/// </summary>
		public static IEnumerator FadeCoroutine(GameObject gameObject, FadeDirection fadeDirection, float opacity, float duration, Easing easing, bool destroy, string cubicBezier)
		{
			float elapsedTime = 0;
			// get the starting material and color
			Renderer r = gameObject.GetComponent<Renderer>();
			Material m = Utils.GetUsableMaterial (r);
			Color endColor;
			Color startColor;

			// Is the object fading in or out?
			if (fadeDirection == FadeDirection.In)
			{
				// get the color instance with the right alpha value
				endColor = Utils.GetUsableColor (r.material);
				startColor = new Color (endColor.r, endColor.g, endColor.b, opacity);
			}
			else
			{
				// Reverse if it's fading out
				startColor = Utils.GetUsableColor (r.material);
				endColor = new Color (startColor.r, startColor.g, startColor.b, opacity);
			}

			// Skip ahead if this is meant to be an immediate transition.
			if (duration < 0.0001f)
			{
				m.SetColor ("_Color", endColor);
			}
			else
			{
				while (elapsedTime < duration)
				{
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

			// Destroy if specified
			if (destroy)
			{
				GameObject.Destroy (gameObject);
			}

		}

		/// <summary>
		/// Coroutine for tweening material or color-only properties.
		/// </summary>
		public static IEnumerator TweenMaterialCoroutine(GameObject gameObject, Material startMaterial, Material endMaterial, float duration, Easing easing, string cubicBezier) {
			float elapsedTime = 0;

			// get the starting material and color
			Renderer r = gameObject.GetComponent<Renderer>();
			// this is the material we'll tween
			Material m = Utils.GetUsableMaterial(r);

			// If start material is null, assume we're tweening FROM the current material
			if (startMaterial == null)
			{
				startMaterial = Utils.GetUsableMaterial (r);
			}

			// If end material is null, assume we're tweening TO the current material
			if (endMaterial == null)
			{
				// Debug.Log ("EndMaterial is null");				
				endMaterial = Utils.GetUsableMaterial (r);
			}

			// Skip ahead if this is meant to be an immediate transition.
			if (duration < 0.0001f)
			{
				m.CopyPropertiesFromMaterial(endMaterial);
			}
			else
			{
				startMaterial = Utils.GetUsableMaterial (startMaterial);
				endMaterial = Utils.GetUsableMaterial (endMaterial);
				while (elapsedTime < duration)
				{
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

		/// <summary>
		/// Coroutine for tweening material or color-only properties.
		/// </summary>
		public static IEnumerator TweenSpriteCoroutine(GameObject gameObject, Color startColor, Color endColor, float duration, Easing easing, string cubicBezier) {
			float elapsedTime = 0;

			// get the starting material and color
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

			// If start material is null, assume we're tweening FROM the current material
			if (startColor == null)
			{
				startColor = sr.color;
			}

			// If end material is null, assume we're tweening TO the current material
			if (endColor == null)
			{
				endColor = sr.color;
			}

			// Skip ahead if this is meant to be an immediate transition.
			if (duration < 0.0001f)
			{
				sr.color = endColor;
			}
			else
			{
				while (elapsedTime < duration)
				{
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					Color currentColor = Color.Lerp (startColor, endColor, t);
					sr.color = currentColor;
					yield return null;
				}
				sr.color = endColor;
			}

		}

		/// <summary>
		/// Coroutine for tweening position.
		/// </summary>
		public static IEnumerator TweenPositionCoroutine(GameObject gameObject, Vector3 targetPosition, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier)
		{
			float elapsedTime = 0;
			// are we moving in absolute terms
			// or relative to current position
			Vector3 startPosition = gameObject.transform.localPosition;
			Vector3 endPosition;

			if (isRelative)
			{
				// the supplied Vector3 is relative to the original position
				endPosition = startPosition + targetPosition;
			}
			else
			{
				endPosition = targetPosition;
			}

			if (duration < 0.0001f)
			{
				gameObject.transform.localPosition = endPosition;
			}
			else
			{
				while (elapsedTime < duration)
				{
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

			// Destroy if specified
			if (destroy)
			{
				GameObject.Destroy (gameObject);
			}
		}

		/// <summary>
		/// Coroutine for tweening rotation.
		/// </summary>
		public static IEnumerator TweenRotationCoroutine(GameObject gameObject, Quaternion targetRotation, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier)
		{
			float elapsedTime = 0;
			// are we moving in absolute terms
			// or relative to current position
			Quaternion startRotation = gameObject.transform.localRotation;
			Quaternion endRotation;
			if (isRelative)
			{
				// the supplied Quaternion is relative to the original position
				endRotation = startRotation * targetRotation;
			}
			else
			{
				endRotation = targetRotation;
			}

			if (duration < 0.0001f)
			{
				gameObject.transform.localRotation = endRotation;
			}
			else
			{
				while (elapsedTime < duration)
				{
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					// set the rotation
					Quaternion interpolatedRotation = Quaternion.Slerp (startRotation, endRotation, t);
					gameObject.transform.localRotation = interpolatedRotation;
					yield return null;
				}
				gameObject.transform.localRotation = endRotation;
			}

			// Destroy if specified
			if (destroy)
			{
				GameObject.Destroy (gameObject);
			}
		}

		/// <summary>
		/// Coroutine for tweening scale.
		/// </summary>
		public static IEnumerator TweenScaleCoroutine(GameObject gameObject, Vector3 targetScale, float duration, Easing easing, bool destroy, bool isRelative, string cubicBezier)
		{
			float elapsedTime = 0;
			Vector3 startScale = gameObject.transform.localScale;
			Vector3 endScale;

			if (isRelative)
			{
				// the supplied Vector3 is relative to the original scale
				endScale = new Vector3(startScale.x * targetScale.x, startScale.y * targetScale.y, startScale.z * targetScale.z);
			}
			else
			{
				endScale = targetScale;
			}

			if (duration < 0.0001f)
			{
				gameObject.transform.localScale = endScale;
			}
			else
			{
				while (elapsedTime < duration)
				{
					float t = elapsedTime / duration;
					// shift 't' based on the easing function
					t = Utils.GetT (t, easing, cubicBezier);
					elapsedTime += Time.deltaTime;
					// set the scale
					Vector3 interpolatedScale = Vector3.Lerp (startScale, endScale, t);
					gameObject.transform.localScale = interpolatedScale;
					yield return null;
				}
				gameObject.transform.localScale = endScale;
			}

			// Destroy if specified.
			if (destroy)
			{
				GameObject.Destroy (gameObject);
			}
		}
	}
}
