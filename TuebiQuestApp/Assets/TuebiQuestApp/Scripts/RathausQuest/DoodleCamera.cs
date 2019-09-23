using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleCamera : MonoBehaviour {



    public Transform Target;
    public GameObject Plattform;
    public float MinSpawnDistance;
    public float MaxSpawnDistance;

    private float camYPosition;
    private float lastYSpawn;
    private float nextSpawnDistance;

    private void Start()
    {
        nextSpawnDistance = Random.Range(MinSpawnDistance, MaxSpawnDistance);
    }

    void Update () {
		if(Target.position.y > camYPosition)
        {
            camYPosition = Target.position.y;
        }

        if(camYPosition - lastYSpawn > nextSpawnDistance)
        {
            nextSpawnDistance = Random.Range(MinSpawnDistance, MaxSpawnDistance);
            lastYSpawn = camYPosition;
            Instantiate(Plattform, transform.position + Vector3.forward * 10 + Vector3.up * 7 + Vector3.right * Random.Range(-3.2f, 3.2f), Quaternion.identity);
        }

        transform.position = new Vector3(transform.position.x, camYPosition, transform.position.z);
        transform.LookAt(Target);
	}
}
