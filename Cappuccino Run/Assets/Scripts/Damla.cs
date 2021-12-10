using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damla : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        speed = Random.Range(3, 6);
    }
    void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * speed));
    }
}
