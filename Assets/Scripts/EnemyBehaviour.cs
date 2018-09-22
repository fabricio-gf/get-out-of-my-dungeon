using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	[SerializeField] private float speed = 1.1f;
	[SerializeField] private float hp = 100f;
	[SerializeField] private float dano = 20f;
	private Vector3 movement;
	private bool col = false;
	private float stopTime=1,stopTimer;
	public float damagetimer = 0, immuneDamageTime = 1.6f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!col && stopTimer<= 0){	
			movement = transform.position;
			movement.x-=speed*Time.deltaTime;
			transform.position=movement;
		}
		else if(col){
			movement = transform.position;
			movement.x+=6*speed*Time.deltaTime;
			transform.position=movement;
			stopTimer = stopTime;
			col = false;
		}

		damagetimer-=Time.deltaTime;
		stopTimer-=Time.deltaTime;

		if(hp <= 0){
			Destroy(gameObject);
		}

	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.name.StartsWith("SkeletonAttack")){
			hp-=dano;
			//Debug.Log("dano");
		}
		else if(collision.gameObject.tag == "enemy"){
			// nothing
		}
		else{
			Minion m = collision.gameObject.GetComponent<Minion>(); 
			if(m && m.InGame)
				col = true;
		}
		// else if(collision.gameObject.tag == "minion" ){

		// }
	}

}
