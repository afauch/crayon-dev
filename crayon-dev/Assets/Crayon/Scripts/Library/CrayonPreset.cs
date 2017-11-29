// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using Crayon;

namespace Crayon
{

	/// <summary>
	/// A Preset is a collection of Crayon States and their associated transitions and properties, that can
	/// be added quickly to new GameObjects via CrayonStateManager. Think of them as styles.
	/// CrayonPreset is a serialized class used to hold data about Crayon States when saving presets.
	/// </summary>
	[System.Serializable]
	public class CrayonPreset {

		public string _id; // unique ID of the preset
		public List<CrayonStateData> _crayonStatesData; // Stores, as a list, data on each of the Crayon States for this preset

		/// <summary>
		/// Constructor function.
		/// </summary>
		public CrayonPreset(string id)
		{
			_id = id;
		}

		/// <summary>
		/// Called to add state data to a preset.
		/// </summary>
		/// <param name="stateData">State data.</param>
		public void AddStateData (CrayonStateData stateData)
		{
			if(_crayonStatesData == null)
			{
				_crayonStatesData = new List<CrayonStateData>();
			}
			_crayonStatesData.Add(stateData);
		}

	}

}
