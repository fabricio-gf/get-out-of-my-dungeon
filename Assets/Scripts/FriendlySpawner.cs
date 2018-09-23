using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FriendlySpawner : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject MinionPrefab;
    [SerializeField] private GameObject MinionImage;
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private float snapValue = 1;
    [SerializeField] private float cost;
    private GameObject Canvas;
    private Text MoneyText;
    private GameObject Minion;
    private GameObject[] Arrows = new GameObject[4];
    private Vector3 mousePos;
    private Minion MinionScript;
    private float snapInverse;
    private int instanceState = 0;
    public Tilemap tilemap;
    private TileBase tilecell;

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
        Canvas = GameObject.Find("/Canvas");
        MoneyText = GameObject.Find("/Canvas/SideBar/Money/Text").GetComponent<Text>();
    }

    void Update()
    {
        if (instanceState == 3)
        {
            foreach (GameObject Arrow in Arrows)
            {
                Destroy(Arrow);
            }
            instanceState = 0;
        }
    }

    void SnapOnGrid(GameObject minion)
    {
        Vector3Int v1 = Vector3Int.CeilToInt(Minion.transform.position);
        Vector3Int v2 = Vector3Int.FloorToInt(Minion.transform.position);
        minion.transform.position = new Vector3((v1.x + v2.x) / 2f, (v1.y + v2.y) / 2f, (v1.z + v2.z) / 2f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(instanceState);
        if (instanceState == 0 && int.Parse(MoneyText.text) >= cost)
        {
            MoneyText.text = (int.Parse(MoneyText.text) - cost).ToString();
            Minion = Instantiate(MinionImage);
            Minion.transform.SetParent(Canvas.transform, false);
            MinionScript = Minion.GetComponent<Minion>();
            instanceState = 1;
        }
        else if (instanceState == 1)
        {
            mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Minion.transform.position = mousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (instanceState == 1)
        {
            GameObject Minion2 = Minion;
            Minion = Instantiate(MinionPrefab, Camera.main.ScreenToWorldPoint(Minion.transform.position), Minion.transform.rotation);
            Destroy(Minion2);
            MinionScript = Minion.GetComponent<Minion>();
            MinionScript.IsClicked = true;
            MinionScript.InGame = true;
            SnapOnGrid(Minion);
            if (MinionScript is Skeleton)
            {
                instanceState = 2;
                for (int i = 0; i < 4; i++)
                {
                    Arrows[i] = Instantiate(ArrowPrefab, new Vector3(Minion.transform.position.x, Minion.transform.position.y + Minion.GetComponent<SpriteRenderer>().bounds.size.y / 2, Minion.transform.position.z) + Quaternion.Euler(0, 0, 90 * i) * Minion.transform.right, Quaternion.Euler(0, 0, 90 * (i + 1)));
                    ArrowBehaviour ArrowScript = Arrows[i].GetComponent<ArrowBehaviour>();
                    ArrowScript.Spawner = this;
                    ArrowScript.Direction = i;
                    ArrowScript.Minion = MinionScript as Skeleton;
                }
            }
            else if (MinionScript is Miner)
            {
                Vector3Int v = Vector3Int.FloorToInt(Minion.transform.position);
                v.z = 0;
                tilecell = tilemap.GetTile(v);
                Debug.Log(tilecell);
                if (tilecell && tilecell.name == "Floor(1)_1")
                {
                    (MinionScript as Miner).Mine = true;
                }
                instanceState = 0;
            }
            else
            {
                instanceState = 0;
            }
        }
    }
}