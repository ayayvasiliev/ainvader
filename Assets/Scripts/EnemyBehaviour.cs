using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	private float[] inputs;
	private float[] weights;
	private float[] outputs;

	private bool inited = false;

	public bool Init {
		set { inited = true; }
	}

	public float this[int i] {
		set { inputs[i % inputs.Length] = value; }
		get { return outputs[i % outputs.Length]; }
	}


	// Use this for initialization
	void Start () {
		inputs = new float[5];
		outputs = new float[3];
		weights = new float[inputs.Length * outputs.Length];
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void SetWeight(int inputIndex, int outputIndex, float value) {
		weights[inputIndex % inputs.Length + (outputIndex % outputs.Length) * inputs.Length] = value;
	}
	float GetWeight(int inputIndex, int outputIndex) {
		return weights[inputIndex % inputs.Length + (outputIndex % outputs.Length) * inputs.Length];
	}

	private static float Sigmoid(float value) {
		float k = System.Convert.ToSingle(System.Math.Exp(value));
		return k / (1.0f + k);
	}

	private void Calc() {
		for (int i = 0; i < outputs.Length; i++) {
			float value = 0.0f;
			for (int j = 0; j < inputs.Length; j++)
				value = this[j] * GetWeight(j, i);
			this[i] = Sigmoid(value);
		}
	}
}
