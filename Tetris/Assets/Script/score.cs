using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    // Start is called before the first frame update
    public  int totalScore=0;
    public SpriteRenderer _spriteRenderer;
    Text text;
    
    void Start()
    {
        text = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = totalScore.ToString();
        if(_spriteRenderer.color.a<1)
        {
            _spriteRenderer.color= new Color(255,255,255, 0.1f * (totalScore / 100));
        }
        

    }
}
