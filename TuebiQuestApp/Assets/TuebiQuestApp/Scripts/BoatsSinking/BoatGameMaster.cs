using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatGameMaster : MonoBehaviour {

    public static BoatGameMaster Instance;

    public GameObject Boat;
    public Transform BoatSpawn1;
    public Transform BoatSpawn2;
    public float BoatUpOffset;
    public float RightOffset;
    public Text PointsText; 
    public Text TimerText;

    private Vector3 BoatSpawnDirection;
    private float BoatSpawnLength;
    private float timer;
    private float Points;

    private float GameTimer;

    private bool GameOver;

    public SpriteRenderer FadeScreen;
    public GameObject EndScreen;

    private bool cameraAvailable;
    private WebCamTexture backCamera;
    public RawImage background;
    public AspectRatioFitter fitter;

    void Start ()
    {
        Instance = this;
        Screen.orientation = ScreenOrientation.Portrait;
        WebCamDevice[] webCamDevices = WebCamTexture.devices;

        if (webCamDevices.Length == 0)
        {
            cameraAvailable = false;
            Debug.Log("No camera supported on this device");
            return;
        }

        backCamera = new WebCamTexture(webCamDevices[0].name, Screen.width, Screen.height);
        
        backCamera.Play();
        background.texture = backCamera;

        cameraAvailable = true;

        BoatSpawnDirection = (BoatSpawn2.position - BoatSpawn1.position).normalized;
        BoatSpawnDirection.y = 0;
	}
	

	void Update ()
    {
        if (!cameraAvailable)
        {
            Debug.Log("No camera available");
            return;
        }

        float ratio = (float)backCamera.width / (float)backCamera.height;
        fitter.aspectRatio = ratio;

        float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orientation = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);



        if (!GameOver && timer < 0)
        {
            BoatSpawnLength = UnityEngine.Random.Range(10f, 30f);
            Instantiate(Boat, transform.position + Vector3.right * RightOffset + BoatSpawnDirection * BoatSpawnLength + Vector3.up * BoatUpOffset, Quaternion.identity);
            timer = UnityEngine.Random.Range(2f, 4f);

        }
        timer -= Time.deltaTime;
        GameTimer += Time.deltaTime;
        if (!GameOver)
        {
            if (GameTimer == 0.0f)
                TimerText.text = "0";
            TimerText.text = (90 - GameTimer).ToString().Split('.')[0];
        }

            

        if(!GameOver && (GameTimer > 90 || Points >= 300))
        {
            GameOver = true;
            Array.ForEach(FindObjectsOfType<Rock>(), r => Destroy(r.gameObject));
            FindObjectOfType<Schleuder>().enabled = false;
            StartCoroutine(EndGame());
        }

	}


    public void BoatHit(int points)
    {
        Points += points;
        if (Points < 0)
            Points = 0;
        if (Points > 300)
            Points = 300;
        PointsText.text = Points + "/300";
    }

    private IEnumerator EndGame()
    {
        for (int i = 0; i < 100; i++)
        {
            FadeScreen.color = new Color(0, 0, 0, FadeScreen.color.a + 1 / 150f);
            yield return null;
        }
        var text = Instantiate(EndScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>();
        text.text = "Hurra du hast gewonnen! \n" + PointsText.text;
    }


}
