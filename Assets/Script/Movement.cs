using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotationValue;
    public float translation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            transform.position = transform.position + transform.forward * Time.deltaTime * translation;
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(new Vector3(0, -rotationValue, 0));
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(new Vector3(0, rotationValue, 0));
        }
        
    }
}
