using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectTowards : MonoBehaviour {
	public float speed = 5f;
	public Transform target;
	// Use this for initialization
	public void Update()
	{
		transform.LookAt(target);	
		/*Vector2 direction = target.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);*/
	}
}
