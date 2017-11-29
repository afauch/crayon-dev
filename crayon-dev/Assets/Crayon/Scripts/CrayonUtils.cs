using UnityEngine;

namespace Crayon {

	// Methods for doing things like 
	public static class Utils {

		// Returns an Easing enum from a string input
		public static Easing GetEasing(string easing) {
			easing = easing.ToLower ();
			switch (easing)
			{
			// TODO: define all in/out versions here

			case "back": return Easing.BackInOut;
			case "bounce": return Easing.BounceInOut;
			case "circular": return Easing.CircularInOut;
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
		public static float GetT(float t, Easing easing, string cubicBezier = "") {

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

			case Easing.Custom:	return GetCustomT (cubicBezier, t);

			default: return t;
			}
		}

		// Given a renderer, return a material that can be used for fading color/opacity
		public static Material GetUsableMaterial(Renderer renderer) {

			// Debug.Log ("GetUsableMaterial called on " + renderer.material.name);

			if (renderer == null) {

				Debug.LogWarning ("No renderer attached to this GameObject, but you're trying to tween material.");

				return null;

			} else {

				Material material = renderer.material;
				return GetUsableMaterial (material);

			}

		}

		public static Material GetUsableMaterial(Material material) {

			if (material == null) {
				Debug.LogWarning ("No material to access.");
				return null;
			} else {
				// TODO: More case handling
				string shaderName = material.shader.name;
				// Turn on FadeMode
				StandardShaderUtils.ChangeRenderMode(material,StandardShaderUtils.BlendMode.Fade);
				// TODO: Is this efficient?
				material.enableInstancing = true;
				return material;
			}

		}

		// Given a renderer and material, return an RGBA color that can be used for fading
		public static Color GetUsableColor(Material material) {

			// TODO: This method could be eliminated if color logic is not required
			Color c;
			c = material.GetColor ("_Color");
			c = new Color(c.r, c.g, c.b, c.a);
			return c;

		}

		public static float GetCustomT(string cubicBezier, float x) {

			// Debug.Log("GetCustomT called");

			// TODO: Add error handling for bad inputs

			// Parse string
			string[] numArray = cubicBezier.Split (',');

			if (numArray.Length != 4) {
				Debug.LogWarning ("Cubic Bezier input incorrect.");
				return 0.0f;
			}

			// Parse string
			float a = float.Parse(numArray[0]);
			float b = float.Parse(numArray[1]);
			float c = float.Parse(numArray[2]);
			float d = float.Parse(numArray[3]);

			// TODO: Need to revert this to the correct cubic bezier solver function

			return GetTJavascriptRendition (a, b, c, d, x);

		}

		// C# Implementation of cubic bezier approximation,
		// adapted from this answer
		// TODO: Check for attribution / licensing
		// https://stackoverflow.com/questions/11696736/recreating-css3-transitions-cubic-bezier-curve
		// Adapting from this codepen:
		// https://codepen.io/anon/pen/LOzLjY

		public static float GetTJavascriptRendition(float p1x, float p1y, float p2x, float p2y, float t) {

			// STEP 1: Create a new UnitBezier object
			UnitBezier curve = new UnitBezier(p1x, p1y, p2x, p2y);

			// STEP 2: Solve for t.
			return curve.solve (t, curve._epsilon);

		}

	}

	// TODO: Reorganize this somewhere else?
	public class UnitBezier {

		public float _p1x;
		public float _p1y;
		public float _p2x;
		public float _p2y;

		public float _cx;
		public float _bx;
		public float _ax;

		public float _cy;
		public float _by;
		public float _ay;

		public float _epsilon = 0.00001f;

		// constructor function
		public UnitBezier(float p1x, float p1y, float p2x, float p2y) {

			_p1x = p1x;
			_p1y = p1y;
			_p2x = p2x;
			_p2y = p2y;

			Init ();

		}

		// pre-calculate the polynomial coefficients
		// First and last control points are implied to be (0,0) and (1.0, 1.0)
		void Init() {

			_cx = 3.0f * _p1x;
			_bx = 3.0f * (_p2x - _p1x) - _cx;
			_ax = 1.0f - _cx - _bx;

			_cy = 3.0f * _p1y;
			_by = 3.0f * (_p2y - _p1y) - _cy;
			_ay = 1.0f - _cy - _by;

		}

		public float sampleCurveX(float t) {
			return ((_ax * t + _bx) * t + _cx) * t;
		}

		public float sampleCurveY(float t) {
			return ((_ay * t + _by) * t + _cy) * t;
		}

		public float sampleCurveDerivativeX(float t) {
			return (3.0f * _ax * t + 2.0f * _bx) * t + _cx;
		}

		public float solveCurveX(float x, float epsilon) {



			float t0;
			float t1;
			float t2;
			float x2; 
			float d2;
			float i;

			// First try a few iterations of Newton's method -- normally very fast.
			for (t2 = x, i = 0; i < 8; i++) {
				x2 = sampleCurveX(t2) - x;
				if (Mathf.Abs (x2) < epsilon)
					return t2;
				d2 = sampleCurveDerivativeX(t2);
				if (Mathf.Abs(d2) < epsilon)
					break;
				t2 = t2 - x2 / d2;
			}

			// No solution found - use bi-section
			t0 = 0.0f;
			t1 = 1.0f;
			t2 = x;

			if (t2 < t0) return t0;
			if (t2 > t1) return t1;

			while (t0 < t1) {
				x2 = sampleCurveX(t2);
				if (Mathf.Abs(x2 - x) < epsilon)
					return t2;
				if (x > x2) t0 = t2;
				else t1 = t2;

				t2 = (t1 - t0) * 0.5f + t0;
			}

			// Give up
			return t2;

		}

		// Find new T as a function of Y along curve X
		public float solve(float x, float epsilon) {
			return sampleCurveY( solveCurveX (x, epsilon) );
		}

	}
		
}
