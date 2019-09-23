using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float UpMoveSpeed;
    public float SidewaysRange;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        transform.position += Vector3.up * UpMoveSpeed * Time.deltaTime;
    }
}
