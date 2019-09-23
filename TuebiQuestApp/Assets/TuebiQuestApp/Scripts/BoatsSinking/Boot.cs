using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boot : MonoBehaviour {


    public float MoveSpeed;
    [HideInInspector]
    private bool CanBeHit;

    public GameObject BootPoints;

    public Sprite Good1;
    public Sprite Good2;
    public Sprite Bad1;
    public Sprite Bad2;
    public float BadPercentage;

    private SpriteRenderer sr;

    void Start () {
        Destroy(gameObject, 30);
        sr = GetComponent<SpriteRenderer>();
        if(Random.Range(0.0f, 100.0f) > BadPercentage)
        {
            CanBeHit = false;
            if (Random.Range(0.0f, 100.0f) > 50.0f)
            {
                sr.sprite = Bad1;
            }
            else
                sr.sprite = Bad2;
        }
        else
        {
            CanBeHit = true;
            if (Random.Range(0.0f, 100.0f) > 50.0f)
            {
                sr.sprite = Good1;
            }
            else
                sr.sprite = Good2;
        }
	}
	
	void Update () {
        transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            int zPos = (int)transform.position.z;
            int points = CanBeHit ? 10 + (zPos < 17 ? 0 : (zPos < 22 ? 10 : 40)) : -20;
            GameObject btP = Instantiate(BootPoints, transform.position + Vector3.up * 2, Quaternion.identity);
            btP.GetComponentInChildren<Text>().text = points.ToString();
            Destroy(btP, 2);
            Destroy(gameObject);
            Destroy(other.gameObject);
            BoatGameMaster.Instance.BoatHit(points);
            //9.23676
            //27.71028
        }
    }
}
