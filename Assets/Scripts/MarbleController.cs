using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
	[Header ("Marble Specific")]
	public GameObject marble;

	public float moveForce = 100.0f;
	public float maxFlickLength = 10.0f;

	[Header("Flick Display Specific")]
	public GameObject flickDisplayObject;
	public Color flickDisplayColorMin;
	public Color flickDisplayColorMax;
	public float minFlickDisplayLength = 0.5f;
	public float maxFlickDisplayLength = 5.0f;

	private bool _flicking = false;
	private Vector3 _startFlickPosition;
	private Rigidbody _rigidbody;
	
	void Start ()
	{
		_rigidbody = marble.GetComponent<Rigidbody>();

		flickDisplayObject.transform.localPosition = Vector3.zero;
		flickDisplayObject.SetActive(false);
	}
	
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Time.timeScale = 0.0f;

			_rigidbody.velocity = Vector3.zero;

			flickDisplayObject.SetActive(true);

			_startFlickPosition = Input.mousePosition;
			_flicking = true;
		}

		if (_flicking)
		{
			Vector3 currentFlickPosition = Input.mousePosition;

			Vector3 flickOffset = currentFlickPosition - _startFlickPosition;
			flickOffset = new Vector3(flickOffset.x, 0, flickOffset.y);

			float flickLength = Mathf.Clamp(flickOffset.magnitude, 0.0f, maxFlickLength) / maxFlickLength;
			Vector3 flickDirection = flickOffset.normalized;

			ChangeFlickDisplay(flickDirection, flickLength);
			
			if (Input.GetMouseButtonUp(0))
			{
				_rigidbody.AddForce(-flickDirection * moveForce * flickLength, ForceMode.Impulse);

				Time.timeScale = 1.0f;

				flickDisplayObject.SetActive(false);

				_flicking = false;
			}
		}
	}

	void ChangeFlickDisplay(Vector3 flickDirection, float flickLength)
	{
		Vector3 lookAtPosition = marble.transform.position - flickDirection;

		flickDisplayObject.transform.position = marble.transform.position;
		flickDisplayObject.transform.LookAt(lookAtPosition, Vector3.up);

		flickDisplayObject.transform.position += flickDisplayObject.transform.forward * 1.0f;

		Vector3 flickScale = flickDisplayObject.transform.localScale;
		flickScale.z = minFlickDisplayLength + (maxFlickDisplayLength - minFlickDisplayLength) * flickLength;
		flickDisplayObject.transform.localScale = flickScale;

		flickDisplayObject.GetComponent<Renderer>().material.color = Color.Lerp(flickDisplayColorMin, flickDisplayColorMax, flickLength);
	}
		
}
