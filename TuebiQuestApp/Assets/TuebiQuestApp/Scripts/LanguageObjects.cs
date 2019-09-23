using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageObjects : MonoBehaviour {

    public string DeText;
    public string EnText;

    private Text text;
    private GameMaster gm;

    private void Start()
    {
        gm = GameMaster.Instance;
        text = GetComponent<Text>();
        UpdateText();
    }

    void OnEnable ()
    {
        if(gm != null && text != null)
            UpdateText();
	}

    public void UpdateText()
    {
        if (gm.SelectedLanguage == Language.en)
            text.text = EnText;
        else
            text.text = DeText;
    }
	
}
