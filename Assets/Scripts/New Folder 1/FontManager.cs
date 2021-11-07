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
    // ��Ʈ ũ��
    public void bigFontSize() 
    {
        Sample_Text.fontSize = 100;
        
    }

    // ��Ʈ �߰�
    public void middleFontSize()
    {
        Sample_Text.fontSize = 80;
        
    }

    // ��Ʈ �۰�
    public void smallFontSize()
    {
        Sample_Text.fontSize = 60;
        
    }

    // �ٰ��� �а�
    public void wideLineSpace()
    {
        Sample_Text.lineSpacing = 1.2f;
       
    }

    // �ٰ��� ����
    public void middleLineSpace()
    {
        Sample_Text.lineSpacing = 1;
        
    }

    // �ٰ��� ����
    public void narrowLineSpace()
    {
        Sample_Text.lineSpacing = 0.8f;
        
    }


}
