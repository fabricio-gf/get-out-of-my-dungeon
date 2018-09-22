using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miner : Minion
{
    [SerializeField] private GameObject PopupTextPrefab;
    private GameObject Canvas;

    public void CreateFloatingText(string text, Transform location)
    {
        GameObject instance = Instantiate(PopupTextPrefab);
        Text PopupTextText = instance.GetComponent<Text>();
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));

        instance.transform.SetParent(Canvas.transform, false);
        instance.transform.position = screenPosition;
        PopupTextText.text = text;
    }

    public override void Action()
    {
        CreateFloatingText("+20", transform);
    }

    void Start()
    {
        Canvas = GameObject.Find("/Canvas");
    }
}
