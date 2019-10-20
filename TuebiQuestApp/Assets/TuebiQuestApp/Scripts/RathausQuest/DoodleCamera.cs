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

    public float minSpawn = -3.2f;
    public float maxSpawn = 3.2f;
    public float stepSize = 0.5f;
    float adjustedValue;
    float prevValue = 1f;

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
            Plattform.transform.Rotate(new Vector3(0, -175f, 0));

            float randomHealth = Random.Range(minSpawn, maxSpawn);
            float numSteps = Mathf.Floor(randomHealth / stepSize);
            adjustedValue = numSteps * stepSize;
            if (prevValue < 0 && adjustedValue < 0)
                adjustedValue *= -1;
            else if (prevValue > 0 && adjustedValue > 0)
                adjustedValue *= -1;
            prevValue = adjustedValue;

            print("av" + adjustedValue);
            var a = transform.position + Vector3.forward * 10 + Vector3.up * 7 + Vector3.right * adjustedValue;
            print("a " + a);
            Instantiate(Plattform, transform.position + Vector3.forward * 10 + Vector3.up * 7 + Vector3.right * adjustedValue, Quaternion.identity);

        }

        transform.position = new Vector3(transform.position.x, camYPosition, transform.position.z);
        transform.LookAt(Target);
	}
}
