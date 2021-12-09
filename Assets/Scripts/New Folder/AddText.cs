using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AddText : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI textPrefab;
    private static List<TextMeshProUGUI> textBox = new List<TextMeshProUGUI>();
    private static string currentID;
    private Script script;
    private List<Choice> choice;

    public Image imgprefab;
    private static List<Image> imgarr = new List<Image>();
    

    // Start is called before the first frame update
    void Start()
    {
        currentID = "M0_0";
        AddScript();
    }

    public void AddScript()
    {
        script = MakeDialog.Instance.FindScript(NextContainer.Instance.nextText);
        if (currentID[0] == script.id[0] && currentID[1] == script.id[1]) // ���� ���� ���丮
        {
            textBox.Add(Instantiate(Resources.Load<TextMeshProUGUI>("Prefab/TextPrefab")));
            textBox[textBox.Count-1].transform.SetParent(GameObject.Find("Content").transform , true);
            textBox[textBox.Count-1].text = script.text;
            GameObject.Find("Content").GetComponent<Scroll>().pos += 600;
            GameObject.Find("Content").GetComponent<Scroll>().IsScroll = true;
        }
        else //���ο� ���丮
        {
            DestroyScript();
            DestroyPicture();
            textBox.Add(Instantiate(Resources.Load<TextMeshProUGUI>("Prefab/TextPrefab")));
            textBox[0].transform.SetParent(GameObject.Find("Content").transform , true);
            textBox[0].text = script.text;
            GameObject.Find("Content").GetComponent<Scroll>().pos =0;
            GameObject.Find("Content").GetComponent<Scroll>().IsScroll = true;
        }
        NextContainer.Instance.nextChoice = script.next;
        currentID = script.id;
    }

    public void DestroyScript(){
        for(int i = 0; i < textBox.Count(); i++){
            Destroy(textBox[i].gameObject);
        }
        textBox.Clear();
    }
    public void AddPicture(Sprite picture, int n)          //n is changed by sprite size
    {
        Image img = Instantiate(imgprefab);
        imgarr.Add(img);
        img.sprite = picture;
        img.transform.SetParent(textBox[textBox.Count-1].transform, true);
        img.transform.position = textBox[textBox.Count-1].transform.position + new Vector3(0, n, 0);
    }

    public void DestroyPicture(){
        for(int i = 0; i < imgarr.Count();i++){
            Destroy(imgarr[i]);
        }
        imgarr.Clear();
    }

}
