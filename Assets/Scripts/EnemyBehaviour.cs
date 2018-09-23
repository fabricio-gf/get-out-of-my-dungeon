using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float speed = 1.1f;
    [SerializeField] private float hp = 100f;
    [SerializeField] private float dano = 20f;
    [SerializeField] private int score = 10;
    [SerializeField] private GameObject PopupTextPrefab;
    private Tilemap tilemap;
    private GameObject TextObject;
    private int dir=0;
    private GameObject Canvas;
    private Text TextScript;
    private Text ScoreText;
    private Vector3 movement;
    private bool col = false;
    private float stopTime = 1, stopTimer;
    public float damagetimer = 0, immuneDamageTime = 1.6f;
    private TileBase tilecell;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject.name.StartsWith("Angel") ){   
            Vector3Int v = Vector3Int.FloorToInt(gameObject.transform.position);
            v.x-=1;
            v.z = 0;
            tilecell = tilemap.GetTile(v);
            if(tilecell && tilecell.name.StartsWith("Floor(1)_0")){
                //gameover
            }
            if(tilecell && tilecell.name.StartsWith("Wall")){
                if(dir==0){
                    v = Vector3Int.RoundToInt(gameObject.transform.position);
                    v.z=0;
                    int x;
                    if( (x=Mathf.RoundToInt( Random.Range(0,1) )) == 0)
                        v.y-=1;
                    else
                        v.y+=1;
                    tilecell = tilemap.GetTile(v);
                    if(tilecell && tilecell.name.StartsWith("Wall")){
                        if(x==0)
                            dir = 2; // desce
                        else
                            dir=1;
                    }
                    else{
                        if(x==0)
                            dir = 1; // desce
                        else
                            dir=2;
                    }
                }
            }
            else{
                if(dir!=0)
                    move();
                    move();
                dir = 0; // em frente
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

    public void move(){

        if (!col && stopTimer <= 0)
        {
            movement = transform.position;
            
            if(dir==0)
                movement.x -= speed * Time.deltaTime;
            else if(dir==1)
                movement.y -= speed * Time.deltaTime;
            else if(dir==2)
                movement.y += speed * Time.deltaTime;
            
            transform.position = movement;
        }
        else if (col)
        {
            movement = transform.position;
            if(dir==0)
                movement.x += 6 * speed * Time.deltaTime;
            else if(dir==1)
                movement.x += 6 * speed * Time.deltaTime;
            else if(dir==2)
                movement.x += 6 * speed * Time.deltaTime;
            transform.position = movement;
            stopTimer = stopTime;
            col = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("oi");
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
