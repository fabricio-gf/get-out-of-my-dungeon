using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

	public GameObject[] spawners;
	private EnemySpawner[] spawnersScript;
	private float timer = 0;
	private float spawnTimer = 5;
	private float[] timeModifier = new float[] {4,3,2,2,2,0,0,-2,-1,0,-2,0,4,3,4,-4,-4,0};
	private int spawnIndex;
	private int modifierIndex=0;

	// Use this for initialization
	void Awake () {
		spawnersScript = new EnemySpawner[spawners.Length];
		int i=0;
		foreach(var spawner in spawners){
			spawnersScript[i] = spawner.GetComponent<EnemySpawner>();
			i++;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		if(timer+timeModifier[modifierIndex]  >= spawnTimer){
			spawnIndex = Mathf.FloorToInt( Random.Range(0,spawnersScript.Length));
			if( spawnersScript[spawnIndex].spawn() == true ){
				timer=0;
				if(modifierIndex < timeModifier.Length)
					modifierIndex++;
			}
		}	
	}
}
