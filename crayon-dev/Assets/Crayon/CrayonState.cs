using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {

	public enum CrayonStateType {
		def,
		hover,
		selected,
		visited,
		custom
	}

	public class CrayonState {

		public CrayonStateType _crayonStateType;
		public Material _material;
		public Color _color;
		public Easing _easing;
		public float _duration;
		public Vector3 _relativePosition;
		public Vector3 _relativeRotation;
		public Vector3 _relativeScale;

	}

}
