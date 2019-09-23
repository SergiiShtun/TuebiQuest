using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class points : MonoBehaviour
{
    public static int counter;
    public Text text;

    private void Start()
    {
        counter = 0;
    }

    private void Update()
    {
        if (counter != 1)
        {
            text.text = counter + " points";
        }
        else
        {
            text.text = "1 point";
        }
    }

}