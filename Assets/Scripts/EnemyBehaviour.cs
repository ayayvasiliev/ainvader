using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	private float[] inputs;
	private float[] weights;
	private float[] outputs;


	public float this[int i] {
		set { inputs[i % inputs.Length] = value; }
		get { return outputs[i % outputs.Length]; }
	}


	// Use this for initialization
	void Start () {
		inputs = new float[5];
		outputs = new float[3];
		weights = new float[inputs.Lenght * outputs.Lenght];
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void SetWeight(int inputIndex, int outputIndex, float value) {
		weights[inputIndex % inputs.Lenght + (outputIndex % outputs.Lenght) * inputs.Lenght] = value;
	}
	float GetWeight(int inputIndex, int outputIndex) {
		return weights[inputIndex % inputs.Lenght + (outputIndex % outputs.Lenght) * inputs.Lenght];
	}

	private static float Sigmoid(double value) {
		float k = Math.Exp(value);
		return k / (1.0f + k);
	}

	private void Calc() {
		for (int i = 0; i < outputs.Lenght; i++)
		{
			double value = 0.0;
			for (int j = 0; j < inputs.Lenght; j++)
				value = this[j] * GetWeight(j, i);
			this[i] = Sigmoid(value);
		}
	}
}
