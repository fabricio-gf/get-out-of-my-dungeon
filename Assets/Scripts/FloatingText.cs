using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;
    private Text damageText;
    private int direction;
    private int i = 0;

    void Start()
    {
        direction = Random.Range(-1, 1);
        if (direction == 0) direction = 1;
        Debug.Log(direction);
    }

    void Update()
    {
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, transform.position + Quaternion.Euler(0, 0, 1f * i * direction) * (transform.up * 3), speed);
            i += 2;
        }
    }
}