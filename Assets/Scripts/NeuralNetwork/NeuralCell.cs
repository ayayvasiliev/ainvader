using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralCell {
    
    public struct Synapse {
        public NeuralCell cell;
        public float weight;
    };

	protected float result = 0.0f;
    public float Result {
        get { return result; }
    }
    virtual public void Calc() {

    }
}
