using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOptimizer : MonoBehaviour
{

	MeshRenderer mRender;

	void Awake ()
	{
		mRender = GetComponent<MeshRenderer> ();
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (this.tag == "Environment" && other.tag == "SphereCheck") {
			mRender.enabled = true;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (this.tag == "Environment" && other.tag == "SphereCheck") {
			mRender.enabled = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (this.tag == "Environment" && other.tag == "SphereCheck") {
			mRender.enabled = false;
		}
	}
}
