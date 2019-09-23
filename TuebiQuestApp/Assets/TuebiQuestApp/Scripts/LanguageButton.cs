using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    en,
    de
}

public class LanguageButton : MonoBehaviour {


    private GameMaster gm;
    private Transform Highlight;

	// Use this for initialization
	void Start () {
        gm = GameMaster.Instance;
        Highlight = transform.GetChild(0);

        if (gm.SelectedLanguage == Language.en)
            Highlight.localScale = new Vector3(1, 1, 1);
        else
            Highlight.localScale = new Vector3(-1, -1, 1);
    }


    public void ChangeLanguage()
    {
        gm.ChangeLanguage();
        if (gm.SelectedLanguage == Language.en)
            Highlight.localScale = new Vector3(1, 1, 1);
        else
            Highlight.localScale = new Vector3(-1, -1, 1);
    }

}
