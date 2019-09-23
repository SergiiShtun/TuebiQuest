using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_enemy : MonoBehaviour {

    public GameObject enemy; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy.gameObject.SetActive(false);
        points.counter += 10;
    }
}
