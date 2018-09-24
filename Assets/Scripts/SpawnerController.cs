using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    private EnemySpawner[] spawnersScript;
    private float timer = 0;
    private float spawnTimer = 5;
    private float[] timeModifier;
    private int spawnIndex;
    private int modifierIndex = 0;

    // Use this for initialization
    void Awake()
    {
        print("entrou");
		timeModifier = new float[] { 6, 4, 4, 3, 3, 2, 1, -2, -1, 0, -2, 0, 4, 3, 4, -4, -4, 0 };
        int i;
		spawnersScript = new EnemySpawner[transform.childCount];
        for (i = 0; i < transform.childCount; i++)
        {
            spawnersScript[i] = transform.GetChild(i).GetComponentInChildren<EnemySpawner>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTimer + timeModifier[modifierIndex])
        {   
            spawnIndex = Mathf.FloorToInt(Random.Range(0, spawnersScript.Length));
            if (spawnersScript[spawnIndex].spawn() == true)
            {
                timer = 0;
                if (modifierIndex < timeModifier.Length - 1)
                    modifierIndex++;
            }
        }
    }
}
