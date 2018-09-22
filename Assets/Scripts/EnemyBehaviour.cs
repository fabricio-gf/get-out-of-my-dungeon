using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	[SerializeField] private float speed = 1.1f;
	[SerializeField] private float hp = 100f;
	[SerializeField] private float dano = 20f;
	private Vector3 movement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		movement = transform.position;
		movement.x-=speed*Time.deltaTime;
		transform.position=movement;	

		if(hp <= 0){
			Destroy(gameObject);
		}

	}

	void OnCollisionEnter2D(Collision2D collision){
		Debug.Log("hit");
		if(collision.gameObject.name.StartsWith("SkeletonAttack")){
			hp-=dano;
			Debug.Log("dano");
		}
		// else if(collision.gameObject.tag == "minion" ){

		// }
	}

}
