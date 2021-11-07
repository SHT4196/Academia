using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontManager : MonoBehaviour
{
    public Text Sample_Text;
  
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // 폰트 크게
    public void bigFontSize() 
    {
        Sample_Text.fontSize = 100;
        
    }

    // 폰트 중간
    public void middleFontSize()
    {
        Sample_Text.fontSize = 80;
        
    }

    // 폰트 작게
    public void smallFontSize()
    {
        Sample_Text.fontSize = 60;
        
    }

    // 줄간격 넓게
    public void wideLineSpace()
    {
        Sample_Text.lineSpacing = 1.2f;
       
    }

    // 줄간격 보통
    public void middleLineSpace()
    {
        Sample_Text.lineSpacing = 1;
        
    }

    // 줄간격 좁게
    public void narrowLineSpace()
    {
        Sample_Text.lineSpacing = 0.8f;
        
    }


}
