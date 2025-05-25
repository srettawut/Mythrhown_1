using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNote : MonoBehaviour
{
    public float fallSpeed = 5f;
    private float screenHeight;

    AudioSource audioSource;
    public AudioClip hitsound;

    SpriteRenderer spriteRenderer;
    Collider2D noteCollider;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        noteCollider = GetComponent<Collider2D>();

        screenHeight = Camera.main.orthographicSize * 2;

        transform.position = new Vector3(Random.Range(-5f, 5f), screenHeight / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(hitsound);
            spriteRenderer.enabled = false;
            noteCollider.enabled = false;

            //StartCoroutine(destroyNote());
        }
    }

    IEnumerator destroyNote()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    
}
