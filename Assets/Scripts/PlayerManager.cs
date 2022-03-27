using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Image> health;
    [SerializeField] private List<Image> mental;
    private GameObject Force;
    private GameObject Intellect;
    private GameObject Mana;

    [SerializeField] private Text forceAmount;
    [SerializeField] private Text manaAmount;
    [SerializeField] private Text intellectAmount;
    [SerializeField] private Image diepanel;

    [SerializeField] private Image[] stat_fill = new Image[3];    //force, intel, poli ������ ���
    
    // Start is called before the first frame update
    void Start()
    {
        //imgSet();
    }
    private void Awake()
    {
        health = new List<Image>();
        mental = new List<Image>();
        GameObject HealthGO = GameObject.FindWithTag("Health");
        GameObject MentalGO = GameObject.FindWithTag("Mental");
        for(int i = 0; i < 5; i++)
        {
            health.Add(HealthGO.transform.GetChild(i).GetComponent<Image>());
        }
        for (int i = 0; i < 5; i++)
        {
            mental.Add(MentalGO.transform.GetChild(i).GetComponent<Image>());
        }
        Force = GameObject.Find("Force");
        Intellect = GameObject.Find("Intellect");
        Mana = GameObject.Find("Mana");
        forceAmount = Force.transform.GetChild(2).GetComponent<Text>();
        manaAmount = Mana.transform.GetChild(2).GetComponent<Text>();
        intellectAmount = Intellect.transform.GetChild(2).GetComponent<Text>();
        stat_fill[0] = Force.transform.GetChild(1).GetComponent<Image>();
        stat_fill[1] = Intellect.transform.GetChild(1).GetComponent<Image>();
        stat_fill[2] = Mana.transform.GetChild(1).GetComponent<Image>();
        diepanel = GameObject.Find("Canvas").transform.Find("DiePanel").GetComponent<Image>();

        if (Player.instance.IsPlayerReset)
            Player.instance.ResetPlayer();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void imgChange(int state, int value) // Change Images by state & value
    {
        //state 0: health, 1: mental
        if (state == 0) {
            for (int i = 0; i < value; i++)
                health[i].gameObject.SetActive(true);
            for (int i = 4; i >= value; i--)
                health[i].gameObject.SetActive(false);
        }
        else if(state == 1)
        {
            for (int i = 0; i < value; i++)
                mental[i].gameObject.SetActive(true);
            for (int i = 4; i >= value; i--)
                mental[i].gameObject.SetActive(false);
        }
    }
    public void imgSet(int health, int mental)
    {
        imgChange(0, health);
        imgChange(1, mental);
    }
    public void changeability_amount(PlayerAbility ability, int value)
    {
        if (ability == PlayerAbility.Force)
        {
            forceAmount.text = value.ToString();
            stat_fill[0].fillAmount = value / 20.0f;
        }
        else if (ability == PlayerAbility.Intellect)
        {
            intellectAmount.text = value.ToString();
            stat_fill[1].fillAmount = value / 20.0f;
        }
        else if (ability == PlayerAbility.Mana)
        {
            manaAmount.text = value.ToString();
            stat_fill[2].fillAmount = value / 20.0f;
        }
    }
    public void DieAndSceneChange()
    {
        //Application.Quit();
        SceneManager.LoadScene(1);
        //EditorApplication.Exit(0);
        Achivement.Acv.change_scene();
    }
    public void Die()
    {
       
        Player.instance.Die();
        
    }
    public void DiepanelActive()
    {
        diepanel.gameObject.SetActive(true);
        Player.instance.IsPlayerReset = true;
        GameObject.Find("Content").GetComponent<AddText>().DestroyPicture();
    }
}

