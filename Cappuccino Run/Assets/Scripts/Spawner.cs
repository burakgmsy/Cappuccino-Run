using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] private int poolIndex;

    void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {

            GameObject obj = ObjectPool.Instance.GetPooledObject(poolIndex, this.gameObject);
            obj.transform.position = transform.position;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
