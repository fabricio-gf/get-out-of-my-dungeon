using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour
{
    [SerializeField] private float reload;
    private bool isClicked = false;
    private bool inGame = false;
    private float timer = 0f;
    protected float hp;
    private float damage=15;

    public abstract void Action();

    public bool IsClicked
    {
        get { return isClicked; }
        set { isClicked = value; }
    }
    public bool InGame
    {
        get { return inGame; }
        set { inGame = value; }
    }

    void OnMouseDown()
    {
        isClicked = true;
    }

    void Update()
    {
        if (inGame)
        {

            if(hp<=0){
                Destroy(gameObject);
            }

            if (timer >= reload)
            {
                Action();
                timer -= reload;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
        if(collision.gameObject.tag == "enemy" && inGame &&  enemy.damagetimer<=0){
            hp=hp-damage;
            Debug.Log(hp);
            enemy.damagetimer=enemy.immuneDamageTime;
        }
    }

}
