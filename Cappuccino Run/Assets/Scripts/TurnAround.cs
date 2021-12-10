using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);

    }
}
