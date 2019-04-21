using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : NeuralCell {
		
	private NeuralCell.Synapse[] synapses;
	public float this[int index] {
		get { return synapses[index].weight; }
		set { synapses[index].weight = value; }
	}
	public int Size() {
		return synapses.Length;
	}

	public Neuron() {
		synapses = new NeuralCell.Synapse[0];
	}
	public Neuron(NeuralCell.Synapse[] syns) {
		synapses = syns;
	}

	public override void Calc() {
		float value = 0.0f;
		for (int i = 0; i < synapses.Length; i++)
			value += synapses[i].cell.Result * synapses[i].weight;
		result = Sigmoid(value / synapses.Length);
	}

	private static float Sigmoid(float value) {
		float k = System.Convert.ToSingle(System.Math.Exp(value));
		return k / (1.0f + k);
	}
}
