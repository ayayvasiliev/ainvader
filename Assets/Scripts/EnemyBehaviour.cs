using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	private float[] inputs = new float[EnemyPerceptronSize.InputLayerSize];
	private float[] weights = new float[EnemyPerceptronSize.InputLayerSize * EnemyPerceptronSize.OutputLayerSize];
	private float[] outputs = new float[EnemyPerceptronSize.OutputLayerSize];

	private bool inited = false;

	private float rot = 0.0f;
	private float x = 0.0f, y = 0.0f;


	public void Init() {
		inited = true;
	}


	// Use this for initialization
	void Start () {		
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
		rot = 0.0f;
	}
	// Update is called once per frame
	void Update () {
		if (inited)
		{
			// Update inputs
			inputs[0] = 5 - y;
			inputs[1] = 5 + x;
			inputs[2] = 5 + y;
			inputs[3] = 5 - x;
			inputs[4] = rot;

			// Update outputs
			Calc();

			Debug.Log("Perceptron " + outputs[0].ToString() + " " + outputs[1].ToString());

			// Accept outputs
			float dt = Time.deltaTime;
			rot += outputs[0] * dt;
			x -= outputs[1] * (float)System.Math.Sin(rot) * dt;
			y += outputs[1] * (float)System.Math.Cos(rot) * dt;
			if (x > 5)
				x = 5;
			if (x < -5)
				x = -5;
			if (y > 5)
				y = 5;
			if (y < -5)
				y = -5;
			
			Debug.Log(dt.ToString() + "    " + x.ToString() + " " + y.ToString());

			// Affect to GameObject
			this.gameObject.transform.position = new Vector3(x, y, 0);
			this.gameObject.transform.rotation = new Quaternion(0, 0, 180.0f * rot / (float)System.Math.PI, 0);
			//this.gameObject.transform.rotation = Quaternion(0, 0, 180.0f * rot / (float)System.Math.PI, 0);
		}
	}
	
	
	public void SetWeight(int inputIndex, int outputIndex, float value) {
		weights[inputIndex % inputs.Length + (outputIndex % outputs.Length) * inputs.Length] = value;
	}
	public float GetWeight(int inputIndex, int outputIndex) {
		return weights[inputIndex % inputs.Length + (outputIndex % outputs.Length) * inputs.Length];
	}

	private static float Sigmoid(float value) {
		float k = System.Convert.ToSingle(System.Math.Exp(value));
		return k / (1.0f + k);
	}

	private void Calc() {
		for (int i = 0; i < outputs.Length; i++) {
			float value = 0.0f;
			for (int j = 0; j < inputs.Length; j++) {
				value += inputs[j] * GetWeight(j, i);
			}
			outputs[i] = Sigmoid(value);
		}
	}
}
