using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTest : MonoBehaviour
{
    public Sprite testimg;
    public Sprite test2;
    public void addpic()
    {
        GameObject.Find("Content").GetComponent<AddText>().AddPicture(testimg, -1000);
        GameObject.Find("Content").GetComponent<AddText>().AddPicture(test2, -1500);
    }

}
