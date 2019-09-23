using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour {

    public GameObject[] Characters;

    public GameObject GetCharacter(string name)
    {
        switch(name)
        {
            case "Eberhard_Guide":
                return Characters[0];
            case "Hölderlin":
                return Characters[1];
            case "Auktorialer_Guide":
                return Characters[2];
            case "Nauclerus":
                return Characters[3];
            case "Spielbeschreibung":
                return Characters[4];
            case "Eberhard_Mini":
                return Characters[5];
            case "Astronom":
                return Characters[6];
            case "AugustusKopf":
                return Characters[7];
            default:
                break;
        }

        return Characters[0];
    }
}
