using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySpawner : MonoBehaviour
{
    [SerializeField] private GameObject MinionPrefab;
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private float snapValue = 1;
    private GameObject Minion;
    private GameObject[] Arrows = new GameObject[4];
    private Vector3 mousePos;
    private Minion MinionScript;
    private float snapInverse;
    private int instanceState = 0;

    public int InstanceState
    {
        get { return instanceState; }
        set
        {
            if (value >= 0 && value <= 3)
                instanceState = value;
            else
                throw new System.ArgumentOutOfRangeException();
        }
    }

    void Start()
    {
        snapInverse = 1 / snapValue;
    }

    void Update()
    {
        if (instanceState == 1)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Minion.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            if (MinionScript.IsClicked == true)
            {
                MinionScript.InGame = true;
                SnapOnGrid(Minion);
                if (MinionScript is Skeleton)
                {
                    instanceState = 2;
                    for (int i = 0; i < 4; i++)
                    {
                        Arrows[i] = Instantiate(ArrowPrefab, Minion.transform.position + Quaternion.Euler(0, 0, 90 * i) * Minion.transform.right, Quaternion.Euler(0, 0, 90 * (i + 1)));
                        ArrowBehaviour ArrowScript = Arrows[i].GetComponent<ArrowBehaviour>();
                        ArrowScript.Spawner = this;
                        ArrowScript.Direction = i;
                        ArrowScript.Minion = MinionScript as Skeleton;
                    }
                }
                else
                {
                    instanceState = 0;
                }
            }
        }
        else if (instanceState == 3)
        {
            foreach (GameObject Arrow in Arrows)
            {
                Destroy(Arrow);
            }
            instanceState = 0;
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