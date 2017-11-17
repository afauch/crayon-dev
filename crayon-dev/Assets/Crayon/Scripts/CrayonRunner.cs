using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {
	
	public class CrayonRunner : MonoBehaviour {

		// TODO: Roll this singleton correctly
		public static CrayonRunner Instance;

		void Awake() {
			Instance = this;
		}

		// Uses the MonoBehaviour instance to run coroutines from other static classes
		public void Run(IEnumerator coroutine) {

			// Run the Coroutine
			StartCoroutine (coroutine);

			return;

		}

	}

}