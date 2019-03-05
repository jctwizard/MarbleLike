using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform followTransform;

	private Vector3 _followOffset;
	
	void Start ()
	{
		_followOffset = transform.position - followTransform.position;
	}
	
	void Update ()
	{
		UpdateCameraFollowPosition();
	}

	void UpdateCameraFollowPosition()
	{
		transform.position = followTransform.position + _followOffset;
	}
}
