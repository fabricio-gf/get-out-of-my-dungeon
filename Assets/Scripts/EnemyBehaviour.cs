using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float speed = 1.1f;
    [SerializeField] private float hp = 100f;
    [SerializeField] private float dano = 20f;
    [SerializeField] private int score = 10;
    [SerializeField] private GameObject PopupTextPrefab;
    private Vector3 Target;
    private Tilemap tilemap;
    private GameObject TextObject;
    private int dir = 0;
    private GameObject Canvas;
    private Text TextScript;
    private Text ScoreText;
    private Vector3 movement;
    private bool col = false;
    private float stopTime = 1, stopTimer;
    public float damagetimer = 0, immuneDamageTime = 1.6f;
    private TileBase tilecell;
    private float _timeStartedLerping;
    private Vector3 _startPosition;
    private float _timeCollided;
    public void CreateFloatingText(string text, Transform location)
    {
        GameObject instance = Instantiate(PopupTextPrefab);
        Text PopupTextText = instance.GetComponent<Text>();
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));

        instance.transform.SetParent(Canvas.transform, false);
        instance.transform.position = screenPosition;
        PopupTextText.text = text;
    }

    // Use this for initialization
    void Start()
    {
        tilemap = GameObject.Find("/Grid/Tilemap").GetComponent<Tilemap>();
        Canvas = GameObject.Find("/Canvas");
        TextObject = GameObject.Find("/Canvas/SideBar/Money/Text");
        TextScript = TextObject.GetComponent<Text>();
        ScoreText = GameObject.Find("/Canvas/SideBar/Score/Text").GetComponent<Text>();
        Target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.name.StartsWith("Angel"))
        {
            if (gameObject.transform.position == Target)
            {
                Vector3Int v = Vector3Int.FloorToInt(gameObject.transform.position);
                Vector3Int v2 = Vector3Int.CeilToInt(gameObject.transform.position);
                Vector3 check = new Vector3(v.x - 1, (v.y + v2.y) / 2f, 0);
                tilecell = tilemap.GetTile(Vector3Int.FloorToInt(check));
                if (tilecell && tilecell.name.StartsWith("Floor(1)_0"))
                {
                    SceneManager.LoadScene("GameOver");
                }
                else if (tilecell && tilecell.name.StartsWith("Wall"))
                {
                    if (dir == 0)
                    {
                        v = Vector3Int.FloorToInt(gameObject.transform.position);
                        v2 = Vector3Int.CeilToInt(gameObject.transform.position);
                        if (Mathf.RoundToInt(Random.Range(0, 2)) == 0)
                        {
                            check = new Vector3(v.x, (v.y + v2.y) / 2f + 1, 0);
                            tilecell = tilemap.GetTile(Vector3Int.FloorToInt(check));
                            if (tilecell && tilecell.name.StartsWith("Wall"))
                            {
                                Target = new Vector3(check.x, check.y - 2, check.z);
                                dir = 1;
                            }
                            else
                            {
                                Target = check;
                                dir = -1;
                            }
                        }
                        else
                        {
                            check = new Vector3(v.x, (v.y + v2.y) / 2f - 1, 0);
                            tilecell = tilemap.GetTile(Vector3Int.FloorToInt(check));
                            if (tilecell && tilecell.name.StartsWith("Wall"))
                            {
                                Target = new Vector3(check.x, check.y + 2, check.z);
                                dir = -1;
                            }
                            else
                            {
                                Target = check;
                                dir = 1;
                            }
                        }
                    }
                    else
                    {
                        v = Vector3Int.FloorToInt(gameObject.transform.position);
                        v2 = Vector3Int.CeilToInt(gameObject.transform.position);
                        if (dir == 1)
                        {
                            check = new Vector3(v.x, (v.y + v2.y) / 2f - 1, 0);
                            tilecell = tilemap.GetTile(Vector3Int.FloorToInt(check));
                            Target = check;
                        }
                        else if (dir == -1)
                        {
                            check = new Vector3(v.x, (v.y + v2.y) / 2f + 1, 0);
                            tilecell = tilemap.GetTile(Vector3Int.FloorToInt(check));
                            Target = check;
                        }
                    }
                }
                else
                {
                    Target = check;
                    dir = 0;
                }
                _timeStartedLerping = Time.time;
                _startPosition = transform.position;
            }
        }
        else
        {
            if (gameObject.transform.position == Target)
            {
                Vector3Int v = Vector3Int.FloorToInt(gameObject.transform.position);
                Vector3Int v2 = Vector3Int.CeilToInt(gameObject.transform.position);
                Target = new Vector3(v.x - 1, (v.y + v2.y) / 2f, 0);
                _timeStartedLerping = Time.time;
                _startPosition = transform.position;
            }
        }
        move();


        damagetimer -= Time.deltaTime;
        stopTimer -= Time.deltaTime;

        if (hp <= 0)
        {
            Destroy(gameObject);

            CreateFloatingText("+5", transform);
            ScoreText.text = (int.Parse(ScoreText.text) + score).ToString();
            TextScript.text = (int.Parse(TextScript.text) + 5).ToString();
        }
    }

    public static float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }

    public void move()
    {
        if (!col && stopTimer <= 0)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / speed;

            transform.position = Vector3.Lerp(_startPosition, Target, percentageComplete);
        }
        else if (col)
        {
            transform.position = _startPosition;
            _timeStartedLerping = Time.time;
            col = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("SkeletonAttack"))
        {
            hp -= dano;
            //Debug.Log("dano");
        }
        else if (collision.gameObject.tag == "enemy")
        {
            // nothing
        }
        // else if(){

        // }
        else
        {
            Minion m = collision.gameObject.GetComponent<Minion>();
            if (m && m.InGame)
                col = true;
        }
        // else if(collision.gameObject.tag == "minion" ){

        // }
    }

}
