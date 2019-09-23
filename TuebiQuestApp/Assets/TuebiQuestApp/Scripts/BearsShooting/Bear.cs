using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {

    BearGameMaster bgm;
    public float MoveSpeed;

    public int currentLane;
    public Transform goal;
    public Sprite[] BearSprites;

	void Start () {
        bgm = BearGameMaster.Instance;
        Destroy(gameObject, 30);
	}
	
	void Update () {
        transform.position += (goal.position - transform.position).normalized * Time.deltaTime * MoveSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meat"))
        {
            bgm.CollectPoints(10 + currentLane * 10);
            GetComponent<Collider>().enabled = false;
            MoveSpeed = 0.3f;
            Destroy(gameObject, 1);
            GetComponent<SpriteRenderer>().sprite = BearSprites[0];
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Honey") && MoveSpeed > 0)
        {
            bgm.CollectPoints(30 + currentLane * 10);
            bgm.CollectHoney(currentLane);
            GetComponent<SpriteRenderer>().sprite = BearSprites[1];
            Destroy(other.gameObject);
        }
    }
}
