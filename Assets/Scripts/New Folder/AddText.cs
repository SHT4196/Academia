using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentID = "M0_0";
        textBox = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        textBox.transform.SetParent(gameObject.transform, true);
        textBox.transform.position += new Vector3(0, -40, 0);
        AddScript();
    }

    public void AddScript()
    {
        script = MakeDialog.Instance.FindScript(NextContainer.Instance.nextText);
        if (currentID[0] == script.id[0] && currentID[1] == script.id[1]) // 같은 종류 스토리
        {
            textBox.transform.position += new Vector3(0, 1400, 0);
            textBox.text += "\n\n\n\n\n\n\n\n\n\n\n\n\n";
            textBox.text += script.text;
            num++;
        }
        else //새로운 스토리
        {
            textBox.transform.position += new Vector3(0, -1 * num * 1400, 0);
            textBox.text = script.text;
            num = 0;
        }
        NextContainer.Instance.nextChoice = script.next;
        currentID = script.id;
    }
    
}
