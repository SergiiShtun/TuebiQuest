using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIElement : MonoBehaviour {

    public float MaxOffset;
    public float MaxRotationOffset;
    public float RotationSpeed;
    public float MoveSpeed;
    public float OffsetChangeTime;

    private Vector3 initPosition;
    private Quaternion initRotation;
    private Vector3 offset;
    private Vector3 offsetAcceleration;
    private Vector3 offsetAccelerationWorker;
    private Vector3 rotOffset;
    private Vector3 rotOffsetAcceleration;
    private Vector3 rotOffsetAccelerationWorker;
    private float offsetTimer;



	// Use this for initialization
	void Start () {
        initPosition = transform.position;
        initRotation = transform.rotation;
        offsetTimer = OffsetChangeTime;
	}
	
	// Update is called once per frame
	void Update () {
        offsetTimer += Time.deltaTime;

        offset += offsetAcceleration * Time.deltaTime;
        offsetAcceleration = Vector3.Lerp(offsetAcceleration, offsetAccelerationWorker, 0.01f);
        if (Mathf.Abs(offset.x) > MaxOffset &&
            (Mathf.Sign(offset.x) == Mathf.Sign(offsetAccelerationWorker.x)))
            offsetAccelerationWorker.x = 0f;
        if (Mathf.Abs(offset.y) > MaxOffset &&
            (Mathf.Sign(offset.y) == Mathf.Sign(offsetAccelerationWorker.y)))
            offsetAccelerationWorker.y = 0f;
        if (Mathf.Abs(offset.z) > MaxOffset &&
            (Mathf.Sign(offset.z) == Mathf.Sign(offsetAccelerationWorker.z)))
            offsetAccelerationWorker.z = 0f;

        rotOffset += rotOffsetAcceleration * Time.deltaTime;
        rotOffsetAcceleration = Vector3.Lerp(rotOffsetAcceleration, rotOffsetAccelerationWorker, 0.01f);
        if (Mathf.Abs(rotOffset.x) > MaxRotationOffset && 
            (Mathf.Sign(rotOffset.x) == Mathf.Sign(rotOffsetAccelerationWorker.x)))
            rotOffsetAccelerationWorker.x = 0f;
        if (Mathf.Abs(rotOffset.y) > MaxRotationOffset &&
            (Mathf.Sign(rotOffset.y) == Mathf.Sign(rotOffsetAccelerationWorker.y)))
            rotOffsetAccelerationWorker.y = 0f;
        if (Mathf.Abs(rotOffset.z) > MaxRotationOffset &&
            (Mathf.Sign(rotOffset.z) == Mathf.Sign(rotOffsetAccelerationWorker.z)))
            rotOffsetAccelerationWorker.z = 0f;

        if (offsetTimer > OffsetChangeTime)
        {
            offsetTimer = 0;
            offsetAccelerationWorker = new Vector3(
                Random.Range(-MoveSpeed, MoveSpeed),
                Random.Range(-MoveSpeed, MoveSpeed),
                Random.Range(-MoveSpeed, MoveSpeed)
                );
            rotOffsetAccelerationWorker = new Vector3(
                Random.Range(-RotationSpeed, RotationSpeed),
                Random.Range(-RotationSpeed, RotationSpeed),
                Random.Range(-RotationSpeed, RotationSpeed)
                );
        }

        transform.position = initPosition + offset;
        transform.rotation = Quaternion.Euler(initRotation.eulerAngles + rotOffset);

    }
}
