using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteriodSpawner : MonoBehaviour
{
    public asterioid asteriodperfab;
    public float tracVariance=15f;
    public float speedRate=2f;
    public int SpawnAmount = 1;
    public float spawndistance = 15f;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), speedRate, speedRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < SpawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawndistance;
            Vector3 spawnpoint=transform.position+spawnDirection;

            float variance = Random.Range(-tracVariance,tracVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            asterioid asterioid = Instantiate(asteriodperfab,spawnpoint,rotation);
            asterioid.size = Random.Range(asterioid.minSize, asterioid.maxSize);
            asterioid.settrajctory(rotation * -spawnDirection);
        }
    }
}
