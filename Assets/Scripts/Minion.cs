using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour
{
    [SerializeField] private float reload;
    [SerializeField] private float hp;
    [SerializeField] private float damage = 15;
    private bool isClicked = false;
    private bool inGame = false;
    private float timer = 0f;

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

    public float Damage
    {
        get { return damage; }
        private set { damage = value; }
    }

    public void OnMouseDown()
    {
        isClicked = true;
    }

    void Update()
    {
        if (inGame)
        {
            if (hp <= 0)
            {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
        if (collision.gameObject.tag == "enemy" && inGame && enemy.damagetimer <= 0)
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            hp = hp - enemy.Dano;
            enemy.damagetimer = enemy.immuneDamageTime;
        }
    }

}
