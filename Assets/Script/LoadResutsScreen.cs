using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResutsScreen : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            spriteRenderer.enabled = false;
            StartCoroutine(goToResultsScreen());   
        }
        else if(other.tag == "MissArea")
        {
            spriteRenderer.enabled = false;
            StartCoroutine(goToResultsScreen());
        }
    }

    private IEnumerator goToResultsScreen()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.showResultScreen();
    }
}
