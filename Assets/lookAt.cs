// This complete script can be attached to a camera to make it
// continuously point at another object.

// The target variable shows up as a property in the inspector.
// Drag another object onto it to make the camera look at it.
using UnityEngine;
using System.Collections;

public class lookAt : MonoBehaviour
{
    public Transform target;
    public string which = "right";
        void LateUpdate()
    {
    	if (which == "right"){
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward,

            target.transform.rotation * Vector3.right);
    }
        	if (which == "forward"){
        transform.LookAt(transform.position + target.transform.rotation * Vector3.up,

            target.transform.rotation * Vector3.forward);
    }
            	if (which == "up"){
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward,

            target.transform.rotation * Vector3.up);
    }
    }
}