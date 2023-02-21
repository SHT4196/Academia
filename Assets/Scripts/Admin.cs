using System;
using 
System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Admin : MonoBehaviour
{
    [SerializeField] private TMP_InputField id;

    [SerializeField] private TMP_InputField password;

    [SerializeField] private TMP_Text warningMessage;

    public GameObject idPanel;

    [SerializeField] private GameObject adminSettingPanel;

    [SerializeField] private TMP_InputField mainEventId;

    [SerializeField] private TMP_InputField healthAmount;
    [SerializeField] private TMP_InputField mentalAmount;
    [SerializeField] private TMP_InputField forceAmount;
    [SerializeField] private TMP_InputField intellectAmount;
    [SerializeField] private TMP_InputField manaAmount;
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private TMP_Text adminSetWarning;


    // Start is called before the first frame update
    void Start()
    {
        warningMessage.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Verify()
    {
        if (id.text == "admin")
        {
            if (password.text == "789456123")
            {
                SetAdminVersion();
            }
            else
            {
                warningMessage.text = "Wrong Password";
            }
        }
        else
        {
            warningMessage.text = "Wrong Id";
        }
    }

    public void LoadAdminVersion()
    {
        string mainId = "";
        int health = 0;
        int mental = 0;
        int force = 0;
        int intellect = 0;
        int mana = 0;
        string playername = "";
        
        if (MakeDialog.instance.FindScript(mainEventId.text) != null)
        {
            mainId = mainEventId.text;
            if (Int32.TryParse(healthAmount.text, out health))
            {
                if (health >= 1 && health <= 3)
                {
                    if (Int32.TryParse(mentalAmount.text, out mental))
                    {
                        if (mental >= 1 && mental <= 3)
                        {
                            if (Int32.TryParse(forceAmount.text, out force))
                            {
                                if (Int32.TryParse(intellectAmount.text, out intellect))
                                {
                                    if (Int32.TryParse(manaAmount.text, out mana))
                                    {
                                        playername = playerName.text;
                                        Debug.Log($"ME ID: {mainId}, Health: {health}, Mental: {mental}, force: {force}, intellect: {intellect}, mana: {mana}, playername: {playername}");
                                        Player.instance.isAdmin = true;
                                        NextContainer.instance.EnterAdminMode(mainId, health, mental, force, intellect, mana, playername);
                                        SceneManager.LoadScene(3);
                                    }
                                    else
                                    {
                                        adminSetWarning.text = "ManaAmount must be an int.";
                                    }
                                }
                                else
                                {
                                    adminSetWarning.text = "IntellectAmount must be an int.";
                                }
                            }
                            else
                            {
                                adminSetWarning.text = "ForceAmount must be an int.";
                            }
                        }
                        else
                        {
                            adminSetWarning.text = "Mental must be min(inclusive) 1 ~ max(inclusive) 5.";
                        }
                    }
                    else
                    {
                        adminSetWarning.text = "Mental must be an int.";
                    }
                }
                else
                {
                    adminSetWarning.text = "Health must be min(inclusive) 1 ~ max(inclusive) 5.";
                }
            }
            else
            {
                adminSetWarning.text = "Health must be an int.";
            }
        }
        else
        {
            adminSetWarning.text = "Please Check MainEvent ID.";
        }
    }
    public void Exit()
    {
        this.gameObject.SetActive(false);
        RemoveMessages();
    }

    public void RemoveMessages()
    {
        warningMessage.text = "";
        id.text = "";
        password.text = "";
    }

    public void SetAdminVersion()
    {
        idPanel.SetActive(false);
        adminSettingPanel.SetActive(true);
    }
}
