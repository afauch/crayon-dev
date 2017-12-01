// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using Crayon.Core;

namespace Crayon
{

	/// <summary>
	/// These are the GameObject extension methods that can be used to quickly set common parameters used in 3D UI design.
	/// </summary>
	public static class CrayonExtensions
	{

		/// <summary>
		/// Fades in a GameObject (and its children) to full opacity.
		/// </summary>
		/// <param name="gameObject">The GameObject to fade.</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Any easing that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void FadeIn(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.Fade (gameObject, FadeDirection.In, 0.0f, duration, easing, false, cubicBezier);
		}

		/// <summary>
		/// Instantiates a GameObject and fades it to full opacity.
		/// </summary>
		/// <param name="gameObject">The GameObject to instantiate and fade.</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Any easing that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void FadeInNew(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			gameObject = GameObject.Instantiate (gameObject, Vector3.zero, Quaternion.identity);
			CrayonRouter.Fade (gameObject, FadeDirection.In, 0.0f, duration, easing, false, cubicBezier);
		}

		/// <summary>
		/// Instantiates a GameObject and fades it to full opacity.
		/// </summary>
		/// <param name="gameObject">The GameObject to instantiate and fade.</param>
		/// <param name="transform">The transform where the GameObject should be instantiated.</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Any easing that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void FadeInNew(this GameObject gameobject, Transform transform, float duration, Easing easing, string cubicBezier = Defaults._cubicBezier)
		{
			gameobject = GameObject.Instantiate (gameobject, transform.position, transform.rotation);
			CrayonRouter.Fade (gameobject, FadeDirection.In, 0.0f, duration, easing, false, cubicBezier);
		}
			
		/// <summary>
		/// Fades a GameObject (and its children) to zero opacity.
		/// </summary>
		/// <param name="gameObject">The GameObject to instantiate and fade.</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void FadeOut(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.Fade (gameObject, FadeDirection.Out, 0.0f, duration, easing, false, cubicBezier);
		}

		/// <summary>
		/// Fades a GameObject (and its children) to zero opacity.
		/// </summary>
		/// <param name="gameObject">The GameObject to fade out.</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Easing, specified as a string, ex. "cubic."</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param> 
		public static void FadeOut(this GameObject gameObject, float duration, string easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.Fade (gameObject, FadeDirection.Out, 0.0f, duration, Utils.GetEasing(easing), false, cubicBezier);
		}
			
		/// <summary>
		/// Fades a GameObject (and its children) to zero opacity, and destroys the GameObject when complete.
		/// </summary>
		/// <param name="gameObject">The GameObject to fade out.</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void FadeOutAndDestroy(this GameObject gameObject, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.Fade (gameObject, FadeDirection.Out, 0.0f, duration, easing, true, cubicBezier);
		}

		/// <summary>
		/// Fades a GameObject (and its children) to zero opacity, and destroys the GameObject when complete.
		/// </summary>
		/// <param name="gameObject">The GameObject to fade out.</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Easing, specified as a string, ex. "cubic."</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void FadeOutAndDestroy(this GameObject gameObject, float duration, string easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.Fade (gameObject, FadeDirection.Out, 0.0f, duration, Utils.GetEasing(easing), true, cubicBezier);
		}


		/// <summary>
		/// Transitions the opacity to a specified value (0.0f - 1.0f).
		/// </summary>
		/// <param name="gameObject">The GameObject to fade.</param>
		/// <param name="opacity">The target opacity (0.0f - 1.0f).</param>
		/// <param name="duration">The duration of the fade.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetOpacity(this GameObject gameObject, float opacity, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.Fade (gameObject, FadeDirection.Out, opacity, duration, easing, false, cubicBezier);
		}

		/// <summary>
		/// Transitions the material (all values) of the GameObject.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="material">The target material to which the GameObject will transition.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetMaterial(this GameObject gameObject, Material material, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.TweenMaterial (gameObject, material, duration, easing, cubicBezier);
		}
			
		/// <summary>
		/// Transitions the albedo color of the GameObject, maintaining all other Material properties.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="color">The target color to which the GameObject will transition.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetColor(this GameObject gameObject, Color color, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.TweenColor (gameObject, color, duration, easing, cubicBezier);
		}

		/// <summary>
		/// Transitions the albedo color of the GameObject, maintaining all other Material properties.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="hexColor">The target color to which the GameObject will transition, as a hex value ("#FF0000").</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetColor(this GameObject gameObject, string hexColor, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			Color color;
			ColorUtility.TryParseHtmlString (hexColor, out color);
			CrayonRouter.TweenColor (gameObject, color, Defaults._duration, Defaults._easing, Defaults._cubicBezier);
		}

		/// <summary>
		/// Transitions the albedo color of the GameObject, maintaining all other Material properties.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="hexColor">The target color to which the GameObject will transition, as a hex value ("#FF0000").</param>
		/// <param name="opacity">The target opacity of the color at the end of the fade.</param>
		public static void SetColor(this GameObject gameObject, string hexColor, float opacity)
		{
			Color color;
			ColorUtility.TryParseHtmlString (hexColor, out color);
			color.a = opacity;
			CrayonRouter.TweenColor (gameObject, color, Defaults._duration, Defaults._easing, Defaults._cubicBezier);
		}

		/// <summary>
		/// Transitions the GameObject to a specified localPosition.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="targetPosition">The target localPosition.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetPosition(this GameObject gameObject, Vector3 targetPosition, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.TweenPosition (gameObject, targetPosition, duration, easing, false, false, cubicBezier);
		}

		/// <summary>
		/// Transitions the GameObject to a position relative to its current position.
		/// </summary>
		/// <param name="targetPosition">The desired offset from the GameObject's current position.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetRelativePosition(this GameObject gameObject, Vector3 targetPosition, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.TweenPosition (gameObject, targetPosition, duration, easing, false, true, cubicBezier);
		}
			
		/// <summary>
		/// Transitions the GameObject to a specified localRotation.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="targetRotation">The target localRotation.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetRotation(this GameObject gameObject, Vector3 targetRotation, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			Quaternion targetQuaternionRotation = Quaternion.Euler (targetRotation);
			CrayonRouter.TweenRotation (gameObject, targetQuaternionRotation, duration, easing, false, false, cubicBezier);
		}

		/// <summary>
		/// Transitions the GameObject to a rotation relative to its current rotation.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="targetRotation">The desired offset from the GameObject's current rotation, in Euler angles.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetRelativeRotation(this GameObject gameObject, Vector3 targetRotation, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			Quaternion targetQuaternionRotation = Quaternion.Euler (targetRotation);
			CrayonRouter.TweenRotation (gameObject, targetQuaternionRotation, duration, easing, false, true, cubicBezier);
		}

		/// <summary>
		/// Transitions the GameObject to a specified localScale.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="targetScale">The target localScale.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetScale(this GameObject gameObject, Vector3 targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.TweenScale (gameObject, targetScale, duration, easing, false, false, cubicBezier);
		}

		/// <summary>
		/// Transitions the GameObject to a specified localScale.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="targetScale">The target localScale, represented as a single float. ex. 1.1f is equivalent to Vector3(1.1f, 1.f, 1.f).</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetScale(this GameObject gameObject, float targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			Vector3 targetVector3Scale = new Vector3 (targetScale, targetScale, targetScale);
			CrayonRouter.TweenScale (gameObject, targetVector3Scale, duration, easing, false, false, cubicBezier);
		}

		/// <summary>
		/// Transitions the GameObject to a scale relative to its current scale.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="targetScale">The target scale, as a multiple of the object's current scale.</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetRelativeScale(this GameObject gameObject, Vector3 targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			CrayonRouter.TweenScale (gameObject, targetScale, duration, easing, false, true, cubicBezier);
		}

		/// <summary>
		/// Transitions the GameObject to a scale relative to its current scale.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="targetScale">The target scale, as a multiple of the object's current scale; represented as a single float. ex. 1.1f is equivalent to Vector3(1.1f, 1.f, 1.f).</param>
		/// <param name="duration">The duration of the transition.</param>
		/// <param name="easing">Easing function that the transition should follow.</param>
		/// <param name="cubicBezier">If using Easing.Custom, this is the cubic bezier curve that defines the easing function, in the format P0,P1,P2,P3.</param>
		public static void SetRelativeScale(this GameObject gameObject, float targetScale, float duration = Defaults._duration, Easing easing = Defaults._easing, string cubicBezier = Defaults._cubicBezier)
		{
			Vector3 targetVector3Scale = new Vector3 (targetScale, targetScale, targetScale);
			CrayonRouter.TweenScale (gameObject, targetVector3Scale, duration, easing, false, true, cubicBezier);
		}

		/// <summary>
		/// Sets the state of a GameObject using the CrayonState and CrayonStateManager components.
		/// </summary>
		/// <param name="gameObject">The GameObject to modify.</param>
		/// <param name="stateType">The state to transition to.</param>
		/// <param name="customState">If the state is a custom, user-defined state, this is the custom state's ID as a string.</param>
		public static void SetState(this GameObject gameObject, CrayonStateType stateType, string customState = "")
		{
			gameObject.GetComponent<CrayonStateManager> ().ChangeState (stateType, customState);
		}

	}
		
}