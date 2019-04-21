using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 1f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private List<GameObject> enemies = new List<GameObject>();


	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", spawnTime, spawnTime);
		Spawn();
	}
	

	private void Spawn() {
		//for (int _ = 0; _ < 10; _++)
		if (enemies.Count == 0)
		{
			GameObject gObject = (GameObject)Instantiate(enemy, this.gameObject.transform.position, this.gameObject.transform.rotation);
			EnemyBehaviour script = gObject.GetComponent<EnemyBehaviour>();
			for (int i = 0; i < EnemyPerceptronSize.InputLayerSize; i++)
				for (int j = 0; j < EnemyPerceptronSize.OutputLayerSize; j++)
					script.SetWeight(i, j, Random.Range(-3.0f, 3.0f));
			script.Init();
        	enemies.Add(gObject);
		}
	}
}
