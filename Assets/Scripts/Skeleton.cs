using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Minion
{
    [SerializeField] private GameObject attack;
    private int direction = -1;

    public int Direction
    {
        get { return direction; }
        set
        {
            if (value >= 0 && value <= 3)
                direction = value;
            else
                throw new System.ArgumentOutOfRangeException();
        }
    }

    public override void Action()
    {
        if (direction != -1)
        {
            Instantiate(attack, transform.position, Quaternion.Euler(0, 0, 90 * direction));
        }
    }
}