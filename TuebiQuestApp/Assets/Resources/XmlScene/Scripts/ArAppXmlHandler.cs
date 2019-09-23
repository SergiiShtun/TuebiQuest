using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;


enum EventType
{

}

public class ArAppXmlHandler : MonoBehaviour {

    private string path;

	void Start ()
    {
        path = Application.dataPath + "/XmlFile/TestXml.xml";
        




    }
	
	
	void Update ()
    {
		
	}



    void LoadXML(int chapter)
    {
        XmlReader xmlReader = null;// XmlReader.Create();

        while(xmlReader.Read())
        {

        }


    }
}
