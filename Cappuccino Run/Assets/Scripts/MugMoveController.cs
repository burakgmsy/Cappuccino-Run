using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugMoveController : MonoBehaviour
{
    public Transform _camera;
    private Vector3 offset;
    //public float speed;
    public float xLimitL, xLimitR;
    public GameObject finalObj;
    public bool isFinal;
    //-1.4f, 1.6f
    private void Start()
    {
        offset = _camera.position - transform.position;
    }
    private void FixedUpdate()
    {
        if (!isFinal)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * GameManager.Instance.speed);
            _camera.transform.position = new Vector3(0, offset.y + transform.position.y, offset.z + transform.position.z);
            LimitXAxis(xLimitL, xLimitR);
        }
        if (transform.position.z > 60)
        {
            finalObj.SetActive(true);
        }

    }
    private void LimitXAxis(float xBoundL, float xBoundR)
    {
        if (transform.position.x < xBoundL)
        {
            transform.position = new Vector3(xBoundL, transform.position.y, transform.position.z);

        }
        if (transform.position.x > xBoundR)
        {
            transform.position = new Vector3(xBoundR, transform.position.y, transform.position.z);

        }
    }

}
