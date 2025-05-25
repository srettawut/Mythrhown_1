using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public TMP_Text GemCountText;

    private int GemCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTenGems()
    {
        GemCount += 10;
        GemCountText.text = GemCount.ToString();
    }

}
