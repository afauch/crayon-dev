using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {

	// This is the high-level class that will store presets
	// and the list of custom states used across the application

	public class CrayonStateGlobals : MonoBehaviour {

		public CrayonStateGlobals Instance;

		public Dictionary<string, CrayonStateManager> _presets;			// these are presets of CrayonStateManagers (think of them like CSS classes or Sketch style presets)
		public List<string> _customStates;

		void Awake() {
			Instance = this;
		}


	}

}
