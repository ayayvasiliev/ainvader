using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 1f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private List<GameObject> enemies = new List<GameObject>();
	private float[][] weights;


	// Use this for initialization
	void Start () {
		/*weights = new float[2, EnemyPerceptronTopology.InputLayerSize, EnemyPerceptronTopology.HiddenLayerSize];
		for (int l = 0; l < 2; l++)
			for (int i = 0; i < EnemyPerceptronTopology.InputLayerSize; i++)
				for (int j = 0; j < EnemyPerceptronTopology.HiddenLayerSize; j++)
					weights[l, i, j] = Random.Range(-3.0f, 3.0f);*/

		weights = new float[EnemyPerceptronTopology.HiddenLayerSize + EnemyPerceptronTopology.OutputLayerSize][];
		for (int i = 0; i < EnemyPerceptronTopology.HiddenLayerSize; i++) {
			weights[i] = new float[EnemyPerceptronTopology.HiddenLayer[i].Size];
			for (int j = 0; j < EnemyPerceptronTopology.HiddenLayer[i].Size; j++)
				weights[i][j] = Random.Range(-1.0f, 1.0f);
		}
		for (int i = 0; i < EnemyPerceptronTopology.OutputLayerSize; i++) {
			weights[EnemyPerceptronTopology.HiddenLayerSize + i] = new float[EnemyPerceptronTopology.OutputLayer[i].Size];
			for (int j = 0; j < EnemyPerceptronTopology.OutputLayer[i].Size; j++)
				weights[EnemyPerceptronTopology.HiddenLayerSize + i][j] = Random.Range(-1.0f, 1.0f);
		}
		
		InvokeRepeating("Spawn", spawnTime, spawnTime);
		Spawn();
	}
	

	private void Spawn() {
		if (enemies.Count == 0)
			for (int _ = 0; _ < 10; _++) {
				GameObject gObject = (GameObject)Instantiate(enemy, this.gameObject.transform.position, this.gameObject.transform.rotation);
				EnemyBehaviour script = gObject.GetComponent<EnemyBehaviour>();
				for (int i = 0; i < EnemyPerceptronTopology.HiddenLayerSize; i++) {
					for (int j = 0; j < EnemyPerceptronTopology.HiddenLayer[i].Size; j++)
						script.SetWeight(j, EnemyPerceptronTopology.InputLayerSize + i, weights[i][j] + Random.Range(-1.0f, 1.0f));
				}
				for (int i = 0; i < EnemyPerceptronTopology.OutputLayerSize; i++) {
					for (int j = 0; j < EnemyPerceptronTopology.OutputLayer[i].Size; j++)
						script.SetWeight(j, EnemyPerceptronTopology.InputLayerSize + EnemyPerceptronTopology.HiddenLayerSize + i, weights[EnemyPerceptronTopology.HiddenLayerSize + i][j] + Random.Range(-1.0f, 1.0f));
				}
				script.Init();
				enemies.Add(gObject);
			}
		else {
			bool alive = false;
			foreach (GameObject enemy in enemies) {
				EnemyBehaviour script = enemy.GetComponent<EnemyBehaviour>();
				if (!script.dead) {
					alive = true;
					return;
				}
			}
			if (alive == false) {
				GameObject e = enemies[Random.Range(0, enemies.Count)];
				EnemyBehaviour script = e.GetComponent<EnemyBehaviour>();
				weights = new float[EnemyPerceptronTopology.HiddenLayerSize + EnemyPerceptronTopology.OutputLayerSize][];
				for (int i = 0; i < EnemyPerceptronTopology.HiddenLayerSize; i++) {
					weights[i] = new float[EnemyPerceptronTopology.HiddenLayer[i].Size];
					for (int j = 0; j < EnemyPerceptronTopology.HiddenLayer[i].Size; j++)
						weights[i][j] = script.GetWeight(j, EnemyPerceptronTopology.InputLayerSize + i);
				}
				for (int i = 0; i < EnemyPerceptronTopology.OutputLayerSize; i++) {
					weights[EnemyPerceptronTopology.HiddenLayerSize + i] = new float[EnemyPerceptronTopology.OutputLayer[i].Size];
					for (int j = 0; j < EnemyPerceptronTopology.OutputLayer[i].Size; j++)
						weights[i][j] = script.GetWeight(j, EnemyPerceptronTopology.InputLayerSize + EnemyPerceptronTopology.HiddenLayerSize + i);
				}
				foreach (GameObject e1 in enemies)
					Destroy(e1, 0.0f);
				enemies.Clear();
			}
		}
	}
}
