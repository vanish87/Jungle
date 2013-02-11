using UnityEngine;
using System.Collections;
using System;

public class HandRise : MonoBehaviour {
	bool track_hand_;
	Vector3 hand_pos_ ;
	
	public Vector3 Scale = new Vector3(0.02f, 0.02f, -0.02f);
	public Vector3 bias;
	public float damping = 5;
    public Vector3 bounds ;//= new Vector3(12, 12, 2);
	
	Vector3 focusPoint;
	public Vector3 desiredPos;
	
	GameObject net_, catcher_;
	// Use this for initialization
	void Start () {
		track_hand_ = false;
		desiredPos = transform.localPosition;
		focusPoint =new Vector3(1.5f ,0.0f, 2.0f);
		bounds = new Vector3(18, 15, 2);
		
		net_ = GameObject.Find("net_");
		net_.renderer.enabled = false;
		catcher_ = GameObject.Find("catcher_");
		Renderer[] allChildren = catcher_.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in allChildren) 
		{
   				 child.enabled = false;	
		}	

		//bias = new Vector3(0.0f ,0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 pos = target.LeftHand.transform.position;
		//string mesg = pos.x + " "+ pos.y +" "+ pos.z;
		//Debug.Log("dwd");

	}
	void Zig_UpdateUser(ZigTrackedUser user)
    {
		ZigInputJoint torso = user.Skeleton[(int)ZigJointId.Torso];
        ZigInputJoint head = user.Skeleton[(int)ZigJointId.Head];
		ZigInputJoint elbow = user.Skeleton[(int)ZigJointId.LeftElbow];
		ZigInputJoint hand = user.Skeleton[(int)ZigJointId.LeftHand];
		
        Vector3 armDirection = (hand.Position - elbow.Position).normalized;
        Vector3 torsoDirection = (head.Position - torso.Position).normalized;
        double angle = Math.Acos(Vector3.Dot(armDirection, torsoDirection)) * 180 / Math.PI;
		hand_pos_ = hand.Position;
        if(angle < 30 && !track_hand_)
		{
			//Debug.Log(angle+"hand raise");
			//focusPoint = hand_pos_;
			Renderer[] allChildren = catcher_.GetComponentsInChildren<Renderer>();
			foreach (Renderer child in allChildren) 
			{
   				child.enabled = true;	
			}
			net_.renderer.enabled = true;
			track_hand_ = true;
		}
		//Debug.Log(bounds.x + " "+ bounds.y + " " + bounds.z );
		Vector3 pos = hand.Position - focusPoint;
        desiredPos = ClampVector(Vector3.Scale(pos, Scale) + bias, -0.5f * bounds, 0.5f * bounds);
		if(track_hand_ )
		{
			//Debug.Log("hand_pos_");
			
			//Vector3 pos = hand_pos_ - focusPoint; ;
        	//desiredPos = ClampVector(Vector3.Scale(pos, Scale) + bias, -0.5f * bounds, 0.5f * bounds);
			//hand_pos_.x /= 200.0f;
			//hand_pos_.y /= 200.0f;
			//hand_pos_.z /= 500.0f;
			//transform.localPosition = hand_pos_;
			Quaternion rot = hand.Rotation;
			//rot.y = -rot.y;
			//rot.z = -rot.z;
			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,  desiredPos, damping * Time.deltaTime);
			
			if(hand.GoodRotation)
				this.transform.localRotation =  rot;;
				//this.transform.Rotate(Vector3.forward);
			//Debug.Log("catcher pos " + transform.position.x + " "+ transform.position.y + " " + transform.position.z );
		}
	}
	
	void UserEngaged(ZigEngageSingleUser user)
    {
        Debug.Log("UserEngaged hand");        
    }
	
	Vector3 ClampVector(Vector3 vec, Vector3 min, Vector3 max) {
        return new Vector3(Mathf.Clamp(vec.x, min.x, max.x),
                           Mathf.Clamp(vec.y, min.y, max.y),
                           Mathf.Clamp(vec.z, min.z, max.z));
    }
}
