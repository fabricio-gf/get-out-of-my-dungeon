using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] private float  SpawnTimer = 5f;
	private float timer = 0;
	public GameObject EnemyPrefab;
	private GameObject Enemy;
	private Vector3 enemyStartingPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
	}

	public bool spawn(){	
		if(timer < SpawnTimer)
			return false;
		timer=0;
		Enemy = Instantiate(EnemyPrefab);
		enemyStartingPos = transform.position;
		enemyStartingPos.z = 10f;
		Enemy.transform.position = enemyStartingPos;
		return true;
	}
}