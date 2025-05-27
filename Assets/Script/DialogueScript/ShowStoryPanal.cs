using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStoryPanal : MonoBehaviour
{
    [SerializeField] private GameObject storyPanal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            storyPanal.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            storyPanal.SetActive(false);
        }
    }
}
