// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using UnityEngine;

namespace Crayon
{
	/// <summary>
	/// We need a MonoBehaviour to call coroutines. This is the class that does that for us.
	/// </summary>
	public class CrayonRunner : MonoBehaviour
	{

		public static CrayonRunner Instance;

		void Awake() {
			Instance = this;
		}

		/// <summary>
		/// Uses the MonoBehaviour instance to run coroutines from other static classes
		/// </summary>
		public void Run(IEnumerator coroutine)
		{
			// Run the Coroutine
			StartCoroutine (coroutine);
			return;
		}

	}

}