using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MugsController : MonoBehaviour
{
    public Transform cameraPoint;
    public GameObject mug, saucer;
    public GameObject mugBroken, saucerBroken;

    public Transform dokulme;


    public List<GameObject> MugsList;
    public List<Transform> finishTransfomList;
    private MugCopy mugCopyScript;
    private Mug mugScript;

    private MugMoveController moveController;
    Rigidbody _rigidbody, rbMugBroken, rbSaucerBroken;




    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        rbMugBroken = mugBroken.GetComponent<Rigidbody>();
        rbSaucerBroken = saucerBroken.GetComponent<Rigidbody>();
        moveController = GetComponent<MugMoveController>();
        mugScript = GetComponent<Mug>();
    }

    private void Update()
    {
        int count = MugsList.Count + 1;
        //listCounter.text = "Mug count: " + count.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("addMug"))
        {
            // other obje kaybolacak;
            // effect çıkabilir 
            other.gameObject.SetActive(false);
            // havuzdan çekilen objenin mugcopy scirptine ulaşılacak
            var obj = ObjectPool.Instance.GetPooledObject(0, this.gameObject);
            mugCopyScript = obj.GetComponent<MugCopy>();

            if (MugsList.Count < 1)//liste boş ise 
            {
                // bu obje hedef olarak verilecek
                // spawn konumu bu objenin z ekseninde 2 eksiğinde olacak 
                mugCopyScript.target = this.gameObject.transform;
                obj.transform.position = gameObject.transform.position + new Vector3(0, 0, -2f);
                MugsList.Add(obj);
            }
            else
            {
                //en sondaki objenin konumu alınacak z eksenin de 1 eksiğinde spawn olacak
                //en son objeyi target olarak belirleyecek
                int count = MugsList.Count;
                var lastobj = MugsList[count - 1];
                mugCopyScript.target = lastobj.transform;
                obj.transform.position = lastobj.transform.position + new Vector3(0, 0, -1f);
                MugsList.Add(obj);
            }
        }
        if (other.gameObject.CompareTag("obstacle"))
        {
            //objelisteden çıkartılır
            //setactive false olur
            // pool'dan kırık obje çekilir.
            // küçük addforce uygulanır.
            //Dökülme efekti eklenebilir
            if (MugsList.Count < 1)// Büyük kupa
            {
                //gameover
                //Debug.Log("GAMEOVER");
                //GameManager.Instance.LoseGame();
                if (GameManager.Instance.speed > 0)
                {
                    Camera.main.DOShakeRotation(3f, .5f).OnComplete(() => moveController._camera.transform.DOLocalRotate(new Vector3(15f, 0, 0), 0.1f));
                }
                //mug.transform.DOLocalMove(new Vector3(0, 0, -1f), 0.5f);
                //mug.transform.DOLocalRotate(new Vector3(-90, 0, 0), 0.5f).OnComplete(() => GameManager.Instance.LoseGame());
                GameManager.Instance.speed = 0;
                mug.SetActive(false);
                saucer.SetActive(false);
                mugBroken.SetActive(true);
                saucerBroken.SetActive(true);
                rbMugBroken.AddForce(Vector3.right * 20f);
                rbSaucerBroken.AddForce(Vector3.right * 50f);
                dokulme.transform.DOScale(new Vector3(80, 10, 80), 1.5f).OnComplete(() => GameManager.Instance.LoseGame());




            }
            else// Küçük kupalar
            {
                // çıkarılan obje setactive alınır.
                // listenin sonundaki obje çıkar.
                Camera.main.DOShakeRotation(.5f, .5f).OnComplete(() => moveController._camera.transform.DOLocalRotate(new Vector3(15f, 0, 0), 0.1f));
                var lastobj = MugsList[MugsList.Count - 1];
                var mugScript = lastobj.GetComponent<MugCopy>();
                mugScript.BrokeObject();
                mugScript.enabled = false;
                MugsList.RemoveAt(MugsList.Count - 1);

            }
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            //speed 0 olacak
            //mugslistin en sondaki elemanın transformu =finish trasforum ilk elemanına DOMoving 
            //moveController.speed = 0;
            moveController.isFinal = true;
            moveController._camera.transform.DOMove(cameraPoint.position, 1.5f);
            moveController._camera.transform.DORotateQuaternion(cameraPoint.rotation, 1.5f);
            int count = MugsList.Count;
            for (int i = 0; i < count; i++)
            {
                var pointPos = finishTransfomList[i];
                var obj = MugsList[(MugsList.Count - 1) - i];
                mugCopyScript = obj.GetComponent<MugCopy>();
                mugCopyScript.enabled = false;
                obj.transform.DOMove(pointPos.position, 1.5f);
            }
            transform.DOMove(finishTransfomList[MugsList.Count].position, 2.5f).OnComplete(() => GameManager.Instance.WinGame());
            //Kamera konumu değişecek
            // kamera takibi 
        }
    }
}
