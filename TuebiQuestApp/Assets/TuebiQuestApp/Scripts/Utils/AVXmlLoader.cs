using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class AVXmlLoader : MonoBehaviour
{
    MainGameManager mgm;

    XmlDocument xmlDocument;
    TextAsset xmlAsset;

    string xmlDocumentName;

    void Start()
    {
        mgm = MainGameManager.Instance;
        xmlDocumentName = mgm.CurrentSelectedQuest;

        xmlAsset = (TextAsset)Resources.Load(xmlDocumentName, typeof(TextAsset));
        LoadXML();
        print("LoadedXml");
        print(mgm.ChapterManager.Chapters.Count);
    }

    void LoadXML()
    {
        if (xmlAsset == null)
        {
            print("Path: " + xmlAsset + " does not exist.");
            return;
        }
        xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlAsset.text);
        XmlNodeList childNodes = xmlDocument.ChildNodes[0].ChildNodes;
        foreach (XmlNode node in childNodes)
        {
            if(node.Name == "characters")
                ParseCharacters(node);
            else if(node.Name == "Animations")
                ParseAnimations(node);
            else if (node.Name == "randomfacts")
                ParseRandomFacts(node);
            else if (node.Name == "chapter")
                ParseChapter(node);
        }
    }

    void ParseCharacters(XmlNode charNode)
    {
        //ToDo: CharacterHandler Store Character

        foreach (XmlNode node in charNode)
            mgm.CharacterManager.Characters.Add(
                node.Attributes["tag"].Value, 
                mgm.AssetManager.GetCharacter(node.Attributes["name"].Value));

    }

    void ParseAnimations(XmlNode animNode)
    {
        //ToDo: CharacterHandler Store Animations
    }

    void ParseRandomFacts(XmlNode rndNode)
    {
        //ToDo: RandomFactManager SetRandomFacts
        foreach (XmlNode fact in rndNode)
        {
            XmlNode geoPoint = null;
            XmlNode text = null;
            foreach(XmlNode factChild in fact.ChildNodes)
            {
                if (factChild.Name.Equals("geopunkt"))
                    geoPoint = factChild;
                else if (factChild.Name.Equals("text"))
                    text = factChild;
            }
            try
            {
                XmlNode longitude = null;
                XmlNode latitude = null;
                XmlNode altitude = null;

                foreach (XmlNode geoChild in geoPoint.ChildNodes)
                {
                    if (geoChild.Name.Equals("Latitude"))
                        latitude = geoChild;
                    else if (geoChild.Name.Equals("Longitude"))
                        longitude = geoChild;
                    else if (geoChild.Name.Equals("Altitude"))
                        altitude = geoChild;
                }

                double lat = double.Parse(latitude.InnerText);
                double lon = double.Parse(longitude.InnerText);
                double alt = double.Parse(altitude.InnerText);
                mgm.RandomFactManager.AddLocation(new GeoPoint(lat, lon, alt), text.InnerText);
            }
            catch (Exception e)
            {
                print("Random Fact not loaded.");
                print(e.Message);
            }
        }
    }

    void ParseChapter(XmlNode chptNode)
    {
        //ToDo: ChapterManager set Chapter
        GeoPoint location = new GeoPoint();
        Dictionary<int, PlainText> pTexts = new Dictionary<int, PlainText>();
        Dictionary<int, Event> events = new Dictionary<int, Event>();
        Dictionary<int, MiniGame> mGames = new Dictionary<int, MiniGame>();

        XmlNode dialog = chptNode.ChildNodes[0];

        foreach (XmlNode node in chptNode.ChildNodes)
        {
            if(node.Name == "geopunkt")
            {
                XmlNode longitude = null;
                XmlNode latitude = null;
                XmlNode altitude = null;

                foreach (XmlNode geoChild in node.ChildNodes)
                {
                    if (geoChild.Name.Equals("Latitude"))
                        latitude = geoChild;
                    else if (geoChild.Name.Equals("Longitude"))
                        longitude = geoChild;
                    else if (geoChild.Name.Equals("Altitude"))
                        altitude = geoChild;
                }

                double lat = double.Parse(latitude.InnerText);
                double lon = double.Parse(longitude.InnerText);
                double alt = double.Parse(altitude.InnerText);
                location.Latitude = lat;
                location.Longitude = lon;
                location.Altitude = alt;
            }
            if (node.Name == "dialog")
            {
                dialog = node;
                break;
            }
        }

        foreach (XmlNode dialogNode in dialog.ChildNodes)
        {
            //print(dialogNode.Name);
            if(dialogNode.Name.Equals("plainText"))
            {
                PlainText pTxt = new PlainText();
                pTxt.text = Regex.Replace(dialogNode.InnerText, @"\r\n?|\n|\t|[ ]{2,}", " ").Trim();
                pTxt.charID = dialogNode.Attributes["cTag"].Value;
                pTexts.Add(int.Parse(dialogNode.Attributes["num"].Value), pTxt);
                //print(int.Parse(dialogNode.Attributes["num"].Value));
            }
            else if (dialogNode.Name.Equals("minispiel"))
            {
                MiniGame mGame = new MiniGame();
                mGame.miniSpielID = dialogNode.Attributes["mId"].Value;
                mGames.Add(int.Parse(dialogNode.Attributes["num"].Value), mGame);
                //print(int.Parse(dialogNode.Attributes["num"].Value));
            }
        }
        
        Chapter cpt = new Chapter();
        cpt.pTexts = pTexts;
        cpt.events = events;
        cpt.mGame = mGames;
        cpt.location = location;
        print(location.Latitude);
        print(location.Longitude);
        print(location.Altitude);
        mgm.ChapterManager.AddChapter(cpt);

    }
}
