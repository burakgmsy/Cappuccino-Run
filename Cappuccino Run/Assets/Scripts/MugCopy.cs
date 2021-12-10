using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MugCopy : MonoBehaviour
{
    public Transform target;
    public float intervalRate;
    public GameObject mug, saucer, mugBroken, saucerBroken, Liquid;

    private float speed;

    private Vector3 direction;
    private const float Epsilon = 0.1f;

    private Rigidbody rbMug, rbSaucer;
    private void Start()
    {
        rbMug = mugBroken.GetComponent<Rigidbody>();
        rbSaucer = saucerBroken.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        direction = (target.position - transform.position).normalized;
        if ((transform.position - target.position).magnitude > Epsilon)
        {
            float x = target.position.z - transform.position.z;
            speed = x * intervalRate;
            //Debug.Log(speed);
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }
    public void BrokeObject()
    {
        Liquid.SetActive(false);
        mug.SetActive(false);
        saucer.SetActive(false);
        mugBroken.SetActive(true);
        saucerBroken.SetActive(true);
        rbMug.AddForce(Vector3.back * 10f);
        rbSaucer.AddForce(Vector3.back * 10f);
    }
}
