using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float speed = 1.1f;
    [SerializeField] private float hp = 100f;
    [SerializeField] private float dano = 20f;
    [SerializeField] private GameObject PopupTextPrefab;
    private GameObject TextObject;
    private GameObject Canvas;
    private Text TextScript;
    private Vector3 movement;
    private bool col = false;
    private float stopTime = 1, stopTimer;
    public float damagetimer = 0, immuneDamageTime = 1.6f;

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
        Canvas = GameObject.Find("/Canvas");
        TextObject = GameObject.Find("/Canvas/SideBar/Money/Text");
        TextScript = TextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!col && stopTimer <= 0)
        {
            movement = transform.position;
            movement.x -= speed * Time.deltaTime;
            transform.position = movement;
        }
        else if (col)
        {
            movement = transform.position;
            movement.x += 6 * speed * Time.deltaTime;
            transform.position = movement;
            stopTimer = stopTime;
            col = false;
        }

        damagetimer -= Time.deltaTime;
        stopTimer -= Time.deltaTime;

        if (hp <= 0)
        {
            Destroy(gameObject);

            CreateFloatingText("+5", transform);
            TextScript.text = (int.Parse(TextScript.text) + 5).ToString();
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
