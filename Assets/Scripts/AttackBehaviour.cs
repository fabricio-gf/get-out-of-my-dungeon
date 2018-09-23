using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private Vector3 targetPosition;
    private float damage;
    private float speed = 10;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (
            transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x ||
            transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x ||
            transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y ||
            transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y
        )
        {
            Destroy(this.gameObject);
        }
    }
}
