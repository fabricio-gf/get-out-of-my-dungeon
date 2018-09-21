using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject MinionPrefab;
    [SerializeField] private float snapValue = 1;
    private GameObject Minion;
    private Vector3 mousePos;
    private Minion MinionScript;
    private float snapInverse;
    private int instanceState = 0;

    // Use this for initialization
    void Start()
    {
        snapInverse = 1 / snapValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (instanceState == 1)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Minion.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            if (MinionScript.IsClicked == true)
            {
                instanceState = 0;
                MinionScript.InGame = true;
                SnapOnGrid(Minion);
            }
        }
    }

    void OnMouseDown()
    {
        if (instanceState == 0)
        {
            Minion = Instantiate(MinionPrefab);
            MinionScript = Minion.GetComponent<Minion>();
            instanceState = 1;
        }
    }

    void SnapOnGrid(GameObject minion)
    {
        float x, y, z;
        x = Mathf.Round(minion.transform.position.x * snapInverse) / snapInverse;
        y = Mathf.Round(minion.transform.position.y * snapInverse) / snapInverse;
        z = 10f;
        minion.transform.position = new Vector3(x, y, z);
    }
}