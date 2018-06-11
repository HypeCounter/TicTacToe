using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustaCamera : MonoBehaviour {
    private float orthSize = 5;
    [SerializeField] private float aspect = 1.66f;

	// Use this for initialization
	void Start () {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthSize * aspect, orthSize * aspect, -orthSize, orthSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
