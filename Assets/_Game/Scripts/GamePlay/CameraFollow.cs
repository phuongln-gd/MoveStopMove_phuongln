using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform tf;
    public Vector3 offset;
    [SerializeField] private float speed;

    void LateUpdate()
    {
        tf.position = Vector3.Lerp(tf.position, target.position + offset, speed * Time.deltaTime);
    }
}
