using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask groundLayer;
    void FixedUpdate()
    {
        Move();
    }

    public override void OnInit()
    {
        base.OnInit();
        level_in_game = 0;
    }
    public void Move()
    {
        // di chuyen ban phim
        #region
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 nextPoint = moveDirection + tf.position;

        if (moveDirection != Vector3.zero && CheckGround(nextPoint))
        {
            tf.position = Vector3.MoveTowards(tf.position, nextPoint, moveSpeed * Time.fixedDeltaTime);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            tf.rotation = Quaternion.RotateTowards(tf.rotation, toRotation, rotationSpeed);
        }
        #endregion

    }

    public bool CheckGround(Vector3 pos)
    {
        return Physics.Raycast(pos, Vector3.down, 5f,groundLayer);
    }
}
