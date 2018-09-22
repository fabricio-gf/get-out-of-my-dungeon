using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour
{
    [SerializeField] private float reload;
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

    void OnMouseDown()
    {
        isClicked = true;
    }

    void Update()
    {
        if (inGame)
        {
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
}
