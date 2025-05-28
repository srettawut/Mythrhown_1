using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe_menu : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;

    private int currentFocusedIndex = -1; // ติดตาม index ปัจจุบันที่ focus อยู่

    void Update()
    {
        int childCount = transform.childCount;
        pos = new float[childCount];
        float distance = 1f / (childCount - 1f);

        for (int i = 0; i < childCount; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < childCount; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(
                        scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                // Focused item
                child.localScale = Vector2.Lerp(child.localScale, new Vector2(1f, 1f), 0.1f);

                // ถ้าเป็นครั้งแรกที่ focus item นี้
                if (currentFocusedIndex != i)
                {
                    currentFocusedIndex = i;
                    PlayMusicForItem(child.gameObject);
                }

                // Shrink others
                for (int a = 0; a < childCount; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(
                            transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }

    void PlayMusicForItem(GameObject item)
    {
        // หยุดเพลงทั้งหมดก่อน
        for (int i = 0; i < transform.childCount; i++)
        {
            AudioSource otherAudio = transform.GetChild(i).GetComponent<AudioSource>();
            if (otherAudio != null && otherAudio.isPlaying)
            {
                otherAudio.Stop();
            }
        }

        // เล่นเพลงของไอเทมที่เลือก
        AudioSource audio = item.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
    }
}
