using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

namespace Crayon {

	public class CrayonStateManager : MonoBehaviour {

		public Dictionary<CrayonStateType, CrayonState> _allStates;

		public void ChangeState(CrayonStateType stateType, string customState = "") {

			// What's our start state (does it matter)?
			// Keep in mind - you might want a transition to start halfway through an interpolation
			// So you should pass interpolated values

			// What's our end state

			// Get the parameters of the things we're tweening

			// Actually do the tween

			// 


		}

	}

}
