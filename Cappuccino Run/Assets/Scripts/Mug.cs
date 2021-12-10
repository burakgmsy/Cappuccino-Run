using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mug : MonoBehaviour
{
    public GameObject mLiquid;
    public GameObject mLiquidMesh;
    public GameObject fakeMug;
    public GameObject milkObj;
    public float rotateAngle;

    public float rateOffRise;
    public float limitY;

    private int mSloshSpeed = 60;
    private int mRotateSpeed = 15;

    private int difference = 25;

    public float xSpeed;


    Rigidbody _rigidbody;

    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {

        SloshControl(rotateAngle);
        mLiquidMesh.transform.Rotate(Vector3.up * mRotateSpeed * Time.deltaTime, Space.World);

    }
    private void SloshControl(float rotateAngle)
    {
        xSpeed = ((Mathf.Abs(transform.InverseTransformVector(_rigidbody.velocity).x)));
        // objenin hareket hızı
        // yavaş ise 
        // mouse kaydırma hıızı ?

        if (InputHandler.Instance.isSwerving)
        {

            if (InputHandler.Instance.mouseOffset.x > 0)
            {

                fakeMug.transform.DOLocalRotate(new Vector3(0f, 0f, rotateAngle), 0.1f);
            }
            if (InputHandler.Instance.mouseOffset.x < 0)
            {

                fakeMug.transform.DOLocalRotate(new Vector3(0f, 0f, -rotateAngle), 0.1f);
            }
        }
        else
        {
            fakeMug.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 1f);
        }
        Slosh();
    }
    private void Slosh()
    {
        //ınverse cup rotation
        Quaternion inverseRotation = Quaternion.Inverse(fakeMug.transform.localRotation);

        //rotate to
        Vector3 finalRotation = Quaternion.RotateTowards(mLiquid.transform.localRotation, inverseRotation, mSloshSpeed * Time.deltaTime).eulerAngles;

        //clamp
        finalRotation.x = ClampRotationValue(finalRotation.x, difference);
        finalRotation.z = ClampRotationValue(finalRotation.z, difference);

        //set
        mLiquid.transform.localEulerAngles = finalRotation;
    }

    private float ClampRotationValue(float value, int difference)
    {
        float returnvalue = 0.0f;

        if (value > 180)
        {
            //clamp
            returnvalue = Mathf.Clamp(value, 360 - difference, 360);
        }
        else
        {
            //clamp
            returnvalue = Mathf.Clamp(value, 0, difference);
        }
        return returnvalue;
    }

    private void FillLiquid(float value, float valueY)
    {
        //Debug.Log(mLiquid.transform.position.y);
        //sıvının şuanki yüksekliği ve sınırı 
        if (mLiquid.transform.position.y < valueY)
        {
            mLiquid.transform.DOMoveY(mLiquid.transform.position.y + value, 1f);
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Coffee"))
        {
            FillLiquid(rateOffRise, limitY);
        }
        if (other.gameObject.CompareTag("Milk"))
        {
            FillLiquid(rateOffRise, limitY);
            milkObj.transform.DOScale(new Vector3(5.5f, milkObj.transform.localScale.y, 5.5f), 1f);
        }
    }


}
