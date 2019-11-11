using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Row[] rows;

    private void OnMouseDown()
    {
        if (!rows[0].rowStopped)
            rows[0].rowStopped = true;
        else if (!rows[1].rowStopped)
            rows[1].rowStopped = true;
        else if (!rows[2].rowStopped)
            rows[2].rowStopped = true;
    }
}
