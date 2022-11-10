using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform tf;
    private Vector3 offset;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        offset =  tf.position - target.position ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tf.position = Vector3.Lerp(tf.position, target.position + offset, speed * Time.deltaTime);
    }
}
