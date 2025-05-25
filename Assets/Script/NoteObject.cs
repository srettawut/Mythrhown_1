using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject goodEffect, perfectEffect, missEffect, catchEffect;

    public GameObject middleBottomPosition;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                GameManager.instance.tapSound.Play();
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if (transform.position.y < -2.6 && transform.position.y > -3.2)
                {
                    Debug.Log("Perfect Hit");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect,transform.position,perfectEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Good Hit");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);

            GameManager.instance.CatchHit();
            GameManager.instance.playHitSound();
            Instantiate(catchEffect, middleBottomPosition.transform.position, catchEffect.transform.rotation);
        }
        if (other.tag == "MissArea")
        {
            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            //GameManager.instance.NoteMissed();
        }
    }
}