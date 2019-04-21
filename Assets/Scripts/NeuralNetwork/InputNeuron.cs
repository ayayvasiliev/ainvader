using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNeuron : NeuralCell {
	public float Set {
		set { result = value; }
	}
}
