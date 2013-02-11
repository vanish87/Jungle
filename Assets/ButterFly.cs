using UnityEngine;
using System.Collections;

public class ButterFly : MonoBehaviour {
	
	Vector3 pos_;
	const int range_ = 4;
	Vector3 max_range_, min_range_;
	bool catched = false;
	GameObject catcher_;
	// Use this for initialization
	void Start () {
		//Random.seed((int)Time.deltaTime);
		max_range_ = new Vector3(5, 5, 1);
		min_range_ = new Vector3(-5, -5, 1);
	}
	
	// Update is called once per frame
	void Update () {
		float pos = Random.value * range_;
		Vector3 new_pos = new Vector3(pos, pos, 3);
		if(catched)
		{
			transform.localPosition = catcher_.transform.localPosition;			
			//Debug.Log(transform.position.x + " "+ transform.position.y + " " + transform.position.z );
			//Debug.Log("cp:" + catcher_.transform.position.x + " "+ catcher_.transform.position.y + " " + catcher_.transform.position.z );
		}
		else
		{
			
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
