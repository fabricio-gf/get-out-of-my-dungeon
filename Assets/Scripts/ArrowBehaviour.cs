using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private FriendlySpawner spawner;
    private Skeleton minion;
    private int direction;

    public int Direction
    {
        private get { return direction; }
        set { direction = value; }
    }

    public Skeleton Minion
    {
        private get { return minion; }
        set { minion = value; }
    }

    public FriendlySpawner Spawner
    {
        private get { return spawner; }
        set { spawner = value; }
    }

    void OnMouseDown()
    {
        minion.Direction = direction;
        spawner.InstanceState = 3;
    }
}
