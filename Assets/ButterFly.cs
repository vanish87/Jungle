using UnityEngine;
using System.Collections;

public class ButterFly : MonoBehaviour {
	
	Vector3 pos_;
	const int range_ = 4;
	Vector3 max_range_, min_range_;
	const int bound = 12;
	float speed = 0.15f;
	float speed_range = 0.2f;
	float speed_range_y = 0.4f;
	bool catched = false;
	float damping =20;
	GameObject catcher_;
	// Use this for initialization
	void Start () {
		//Random.seed((int)Time.deltaTime);
		max_range_ = new Vector3(bound, bound - 5, 1);
		min_range_ = new Vector3(-bound, -2, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
		//Vector3 new_pos = new Vector3(pos, pos, 3);
		
		Vector3 butterfly_pos = transform.localPosition;
		//if(Time.frameCount % 3 ==0 )
		{
			if(butterfly_pos.x > max_range_.x || butterfly_pos.x < min_range_.x || Random.value < 0.003f)
			{
				speed_range = -speed_range;
			}			
			speed = Random.value * speed_range;
			butterfly_pos.x += speed;
			
			
			if(butterfly_pos.y > max_range_.y || butterfly_pos.y< min_range_.y|| Random.value < 0.005f)
			{
				speed_range_y = -speed_range_y;
				butterfly_pos.y += speed_range_y;
				//Debug.Log ("here");
			}
			speed = Random.value * speed_range_y;
			if(Random.value < 0.01f)
				speed = Random.value * speed_range_y * range_;
			butterfly_pos.y += speed;
		}
		if(catched)
		{
			transform.localPosition = catcher_.transform.localPosition;			
			//Debug.Log(transform.position.x + " "+ transform.position.y + " " + transform.position.z );
			//Debug.Log("cp:" + catcher_.transform.position.x + " "+ catcher_.transform.position.y + " " + catcher_.transform.position.z );
		}
		else
		{
			transform.localPosition = Vector3.Lerp(this.transform.localPosition,  butterfly_pos, damping * Time.deltaTime);;
		}
		//Debug.Log(transform.localPosition.x + " "+ transform.localPosition.y + " " + transform.localPosition.z );
		//transform.localPosition = new_pos;
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.collider.name == "catcher_")
		{
			Debug.Log("catch");
			catcher_ = GameObject.Find("HandRiseDetector");
			catched = true;
		}
	}
}
