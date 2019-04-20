using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 1f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private List<GameObject> enemies;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawn", spawnTime, spawnTime);
	}
	

	private void spawn() {

		if (enemies.Length != 0)
			return;
        enemies.Add((GameObject)Instantiate(enemy, this.gameObject.transform.position, this.gameObject.transform.rotation));
	}
}
