using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

namespace Crayon {

	[System.Serializable]
	public class CrayonPreset {

		public string _id;
		public List<CrayonStateData> _crayonStatesData;


		public CrayonPreset(string id) {
			_id = id;
		}

		public void AddStateData (CrayonStateData stateData) {

			if(_crayonStatesData == null) {

				_crayonStatesData = new List<CrayonStateData>();

			}

			_crayonStatesData.Add(stateData);

		}

	}

}
