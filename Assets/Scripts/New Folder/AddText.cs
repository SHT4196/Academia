using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AddText : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI textPrefab;
    private static TextMeshProUGUI textBox;
    private static string currentID;
    private Script script;
    private List<Choice> choice;
    private int num = 0;

    public Image imgprefab;
    private static List<Image> imgarr = new List<Image>();

    
    
    // Start is called before the first frame update
    void Start()
    {
        currentID = "M0_0";
        textBox = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        textBox.transform.SetParent(gameObject.transform, true);
        textBox.transform.position = new Vector3(0, 0, 0);
        AddScript();
    }

    public void AddScript()
    {
        script = MakeDialog.Instance.FindScript(NextContainer.Instance.nextText);
        if (currentID[0] == script.id[0] && currentID[1] == script.id[1]) // ���� ���� ���丮
        {
            textBox.transform.position += new Vector3(0, 1400, 0);
            textBox.text += "\n\n\n\n\n\n\n\n\n\n\n\n\n";
            textBox.text += script.text;
            num++;
        }
        else //���ο� ���丮
        {
            textBox.transform.position += new Vector3(0, -1 * num * 1400, 0);
            textBox.text = script.text;
            num = 0;
            DestroyPicture();
        }
        NextContainer.Instance.nextChoice = script.next;
        currentID = script.id;
    }

    public void AddPicture(Sprite picture, int n)          //n is changed by sprite size
    {
        Image img = Instantiate(imgprefab);
        imgarr.Add(img);
        img.sprite = picture;
        img.transform.SetParent(textBox.transform, true);
        img.transform.position = textBox.transform.position + new Vector3(0, n, 0);
    }

    public void DestroyPicture(){
        for(int i = 0; i < imgarr.Count();i++){
            Destroy(imgarr[i]);
        }
    }
    public void Stat()
    {
        string string_val = "";
        if (script.result != "-")
        {
            if (script.result[0] == '지')
            {
                if (script.result[1] == '+')
                {
                    for (int j = 2; j < script.result.Length; j++)
                    {
                        string_val += script.result[j];
                    }
                    Player.Instance.Changeability(player_ability.intellect, Int32.Parse(string_val));
                }
                else
                {
                    for (int j = 2; j < script.result.Length; j++)
                    {
                        string_val += script.result[j];
                    }
                    Player.Instance.Changeability(player_ability.intellect, Int32.Parse(string_val) * (-1));
                }
            }
            if (script.result[0] == '무')
            {
                if (script.result[1] == '+')
                {
                    for (int j = 2; j < script.result.Length; j++)
                    {
                        string_val += script.result[j];
                    }
                    Player.Instance.Changeability(player_ability.force, Int32.Parse(string_val));
                }
                else
                {
                    for (int j = 2; j < script.result.Length; j++)
                    {
                        string_val += script.result[j];
                    }
                    Player.Instance.Changeability(player_ability.force, Int32.Parse(string_val) * (-1));
                }
            }
            if (script.result[0] == '정')
            {
                if (script.result[1] == '+')
                {
                    for (int j = 2; j < script.result.Length; j++)
                    {
                        string_val += script.result[j];
                    }
                    Player.Instance.Changeability(player_ability.political_power, Int32.Parse(string_val));
                }
                else
                {
                    for (int j = 2; j < script.result.Length; j++)
                    {
                        string_val += script.result[j];
                    }
                    Player.Instance.Changeability(player_ability.political_power, Int32.Parse(string_val) * (-1));
                }
            }
        }
    }

}
