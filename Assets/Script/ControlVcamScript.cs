using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVcamScript : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject startCamera;
    public GameObject emote1Camera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            playerCamera.SetActive(false);
            startCamera.SetActive(false);
            emote1Camera.SetActive(true);
            StartCoroutine(returnplayerCamera());
        }
    }

    IEnumerator returnplayerCamera()
    {
        yield return new WaitForSeconds(2f);
        playerCamera.SetActive(true);
        startCamera.SetActive(false);
        emote1Camera.SetActive(false);
    }
}
