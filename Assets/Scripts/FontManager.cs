using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontManager : MonoBehaviour
{
    public TextMeshProUGUI Sample_Text;
    public TextMeshProUGUI storyText;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ��Ʈ ũ��
    /// </summary>
    public void bigFontSize() 
    {
        Sample_Text.fontSize = 16;
        storyText.fontSize = 16;
    }

    /// <summary>
    ///   ��Ʈ �߰�
    /// </summary>
    public void middleFontSize()
    {
        Sample_Text.fontSize = 14;
        storyText.fontSize = 14;

    }

    /// <summary>
    /// ��Ʈ �۰�
    /// </summary>    

    public void smallFontSize()
    {
        Sample_Text.fontSize = 12;
        storyText.fontSize = 12;

    }

    /// <summary>
    /// �ٰ��� �а�
    /// </summary>

    public void wideLineSpace()
    {
        Sample_Text.lineSpacing = 20f;
        storyText.lineSpacing  = 20f;


    }

    /// <summary>
    /// �ٰ��� ����
    /// </summary>
    public void middleLineSpace()
    {
        Sample_Text.lineSpacing = 0f;
        storyText.lineSpacing = 0f;


    }

    /// <summary>
    /// �ٰ��� ����
    /// </summary>
    public void narrowLineSpace()
    {
        Sample_Text.lineSpacing = -20f;
        storyText.lineSpacing = -20f;


    }


}
