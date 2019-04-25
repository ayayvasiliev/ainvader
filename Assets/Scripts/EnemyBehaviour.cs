using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 100.0f;
	public bool dead = false;

	private NeuralCell[] perceptron;
	private InputNeuron[] inputLayer;
	private Neuron[] hiddenLayer;
	private Neuron[] outputLayer;

	private bool inited = false;

	private float rotation = 0.0f;
	private float direction = 0.0f;
	private float x = 0.0f, y = 0.0f;

    
	static private float width = 2.0f, height = 2.0f;

    // NEW
    public Animator animator;
    private Vector3 movement;


    public void Init() {
		inited = true;
	}


	public EnemyBehaviour() : base() {
		perceptron = new NeuralCell[EnemyPerceptronTopology.InputLayerSize + EnemyPerceptronTopology.HiddenLayerSize + EnemyPerceptronTopology.OutputLayerSize];
		inputLayer = new InputNeuron[EnemyPerceptronTopology.InputLayerSize];
		for (int i = 0; i < EnemyPerceptronTopology.InputLayerSize; i++)
			perceptron[i] = inputLayer[i] = new InputNeuron();
		hiddenLayer = new Neuron[EnemyPerceptronTopology.HiddenLayerSize];
		NeuralCell.Synapse[] syns;
		for (int i = 0; i < EnemyPerceptronTopology.HiddenLayerSize; i++) {
			syns = new NeuralCell.Synapse[EnemyPerceptronTopology.HiddenLayer[i].Size];
			for (int j = 0; j < EnemyPerceptronTopology.HiddenLayer[i].Size; j++)
				syns[j] = new NeuralCell.Synapse { cell = perceptron[EnemyPerceptronTopology.HiddenLayer[i][j]], weight = 0.0f };
			perceptron[EnemyPerceptronTopology.InputLayerSize + i] = hiddenLayer[i] = new Neuron(syns);
		}
		outputLayer = new Neuron[EnemyPerceptronTopology.OutputLayerSize];
		for (int i = 0; i < EnemyPerceptronTopology.OutputLayerSize; i++) {
			syns = new NeuralCell.Synapse[EnemyPerceptronTopology.OutputLayer[i].Size];
			for (int j = 0; j < EnemyPerceptronTopology.OutputLayer[i].Size; j++)
				syns[j] = new NeuralCell.Synapse { cell = perceptron[EnemyPerceptronTopology.OutputLayer[i][j]], weight = 0.0f };
			perceptron[EnemyPerceptronTopology.InputLayerSize + EnemyPerceptronTopology.HiddenLayerSize + i] = outputLayer[i] = new Neuron(syns);
		}
	}


	// Use this for initialization
	void Start () {		
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
		rotation = 0.0f;
		direction = 0.0f;
	}
	// Update is called once per frame
	void Update () {
		if (health < 0)
			dead = true;
		if (inited && !dead)
		{
			// Update inputs
			inputLayer[0].Set = (2 - y) / 4.0f;
			inputLayer[1].Set = (2 + x) / 4.0f;
			inputLayer[2].Set = (2 + y) / 4.0f;
			inputLayer[3].Set = (2 - x) / 4.0f;
			inputLayer[4].Set = direction;
			inputLayer[5].Set = rotation;

			Vector3 playerRelative = GameObject.Find("Player").transform.position - this.gameObject.transform.position;
			playerRelative.z = 0.0f;
			inputLayer[6].Set = System.Convert.ToSingle(System.Math.Atan2(playerRelative.x, playerRelative.y) - rotation + System.Math.PI / 2);
			inputLayer[7].Set = playerRelative.magnitude;


			inputLayer[8].Set = 0.6f; // Magic synapse

			// Update outputs
			Calc(); 

			// Accept outputs
			float dt = Time.deltaTime;
			direction += outputLayer[0].Result * dt;
			x -= outputLayer[1].Result * (float)System.Math.Sin(direction) * dt;
			y += outputLayer[1].Result * (float)System.Math.Cos(direction) * dt;
			rotation += outputLayer[2].Result * dt;
			
			while (rotation < -System.Math.PI)
				rotation += (float)System.Math.PI * 2;
			while (rotation > System.Math.PI)
				rotation -= (float)System.Math.PI * 2;
			while (direction < -System.Math.PI)
				direction += (float)System.Math.PI * 2;
			while (direction > System.Math.PI)
				direction -= (float)System.Math.PI * 2;
			if (x > 2)
				x = 2;
			if (x < -2)
				x = -2;
			if (y > 2)
				y = 2;
			if (y < -2)
				y = -2;

			// Affect to GameObject
			this.gameObject.transform.position = new Vector3(x, y, 0);
			//this.gameObject.transform.eulerAngles = new Vector3(0, 0, 180.0f * rotation / (float)System.Math.PI);

            // NEW
            
            animator.SetFloat("moveX", -(float)System.Math.Sin(direction));
            animator.SetFloat("moveY", (float)System.Math.Cos(direction));
            Debug.Log("туц");
        }
    }
	
    void OnMouseDown()
    {
		CauseDamage(150.0f);
    }

	void CauseDamage(float dmg) {
		health -= dmg;
	}
	
	
	public void SetWeight(int synapseIndex, int outputIndex, float value) {
		if ((outputIndex >= EnemyPerceptronTopology.InputLayerSize) && (outputIndex < perceptron.Length))
			((Neuron)perceptron[outputIndex])[synapseIndex] = value;
	}
	public float GetWeight(int synapseIndex, int outputIndex) {
		if ((outputIndex >= EnemyPerceptronTopology.InputLayerSize) && (outputIndex < perceptron.Length))
			return ((Neuron)perceptron[outputIndex])[synapseIndex];
		return 0.0f;
	}

	private void Calc() {
		foreach (NeuralCell nc in perceptron)
			nc.Calc();
	}
}
