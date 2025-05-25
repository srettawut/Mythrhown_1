using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class MainCameraControlScript : MonoBehaviour
{
    public GameObject mainCamera, mainCharacter;
    public GameObject playerCamera, cafeCamera;
    public TMP_Text welcomeText;
    CinemachineBrain cinemachineBrain;
    bool isGameStarted;
    // Start is called before the first frame update
    void Start()
    {
        cinemachineBrain=mainCamera.GetComponent<CinemachineBrain>();
        mainCharacter.GetComponent<PlayerController>().enabled = false;
        playerCamera.SetActive(false);
        cafeCamera.SetActive(true);

        StartCoroutine(MoveCameraToPlayer());
        StartCoroutine(IntroText("Hey you!", 4.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!cinemachineBrain.IsBlending)
        {
            ICinemachineCamera cafeCam = cafeCamera.GetComponent<ICinemachineCamera>();
            bool isCafeCamLive = CinemachineCore.Instance.IsLive(cafeCam);
            if (!isCafeCamLive)
            {
                mainCharacter.GetComponent<PlayerController>().enabled = true;             
            }
        }
    }

    IEnumerator MoveCameraToPlayer()
    {
        yield return new WaitForSeconds(2f);
        cafeCamera.SetActive(false);
        playerCamera.SetActive(true);
    }

    IEnumerator IntroText(string text, float delay)
    {     
        yield return new WaitForSeconds(delay);
        welcomeText.SetText(text);
        yield return new WaitForSeconds(1.5f);

        welcomeText.SetText("Yes you!");
        yield return new WaitForSeconds(2f);

        welcomeText.SetText("Go to the cafe..");
        yield return new WaitForSeconds(2f);
        welcomeText.gameObject.SetActive(false);
    }    
}
