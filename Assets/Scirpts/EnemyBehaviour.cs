using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	[SerializeField] private float speed = 1.1f;
	private Vector3 movement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		movement = transform.position;
		movement.x-=speed*Time.deltaTime;
		transform.position=movement;
	}
}
