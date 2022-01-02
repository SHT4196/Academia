using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MakeDialog
{
    private static MakeDialog instance;
    public static MakeDialog Instance
    {
        get
        {
            if (instance == null) 
            { 
                instance = new MakeDialog();
            }
            return instance;
        }
    }
    public MakeDialog()
    {
        List<Dictionary<string, object>> ME_Dialog = CSVReader.Read("ME");
        List<Dictionary<string, object>> BE_Dialog = CSVReader.Read("BE");
        List<Dictionary<string, object>> SE_Dialog = CSVReader.Read("SE");
        List<Dictionary<string, object>> SE_Dialog_choice = CSVReader.Read("SE_choice");
        List<Dictionary<string, object>> ME_Dialog_choice = CSVReader.Read("ME_choice");
        List<Dictionary<string, object>> BE_Dialog_choice = CSVReader.Read("BE_choice");

        object storyID = null;
        object storyText = null;
        object storyNext = null;
        object choiceID = null;
        object choiceText = null;
        object choiceNext = null;
        object storyResult = null;
        object spritename = null;
        object storyInterval = null;


        string story_start_str = "_1";
        for (int i = 0; i < ME_Dialog.Count; i++)
        {

            if (ME_Dialog[i].TryGetValue("StoryID", out storyID) && ME_Dialog[i].TryGetValue("StoryText", out storyText) && ME_Dialog[i].TryGetValue("StoryNext", out storyNext) && ME_Dialog[i].TryGetValue("StoryResult", out storyResult) && ME_Dialog[i].TryGetValue("Sprite", out spritename) && ME_Dialog[i].TryGetValue("StoryInterval", out storyInterval))
            {
                Script_Dialog.Add(new Script(storyID.ToString(), storyText.ToString().Replace('n', '\n'), storyNext.ToString().Split('&').ToList(), storyResult.ToString(), spritename.ToString(), Int32.Parse(storyInterval.ToString())));

            }
           
        }

        for (int i = 0; i < ME_Dialog_choice.Count; i++)
        {
            if (ME_Dialog_choice[i].TryGetValue("ChoiceID", out choiceID) && ME_Dialog_choice[i].TryGetValue("ChoiceText", out choiceText) && ME_Dialog_choice[i].TryGetValue("ChoiceNext", out choiceNext))
                Choice_Dialog.Add(new Choice(choiceID.ToString(), choiceText.ToString(), choiceNext.ToString()));
        }

        for (int i = 0; i < BE_Dialog.Count; i++)
        {

            if (BE_Dialog[i].TryGetValue("StoryID", out storyID) && BE_Dialog[i].TryGetValue("StoryText", out storyText) && BE_Dialog[i].TryGetValue("StoryNext", out storyNext) && BE_Dialog[i].TryGetValue("StoryResult", out storyResult) && BE_Dialog[i].TryGetValue("Sprite", out spritename))
            {
                Script scr_temp = new Script(storyID.ToString(), storyText.ToString().Replace('n', '\n'), storyNext.ToString().Split('&').ToList(), storyResult.ToString(), spritename.ToString(),0);

                Script_Dialog.Add(scr_temp);
                if(scr_temp.id.Contains(story_start_str))
                    RandomPool.Instance.RandomPool_List.Add(scr_temp); // RandomPool Add
            }
           
        }

        for (int i = 0; i < BE_Dialog_choice.Count; i++)
        {
            if (BE_Dialog_choice[i].TryGetValue("ChoiceID", out choiceID) && BE_Dialog_choice[i].TryGetValue("ChoiceText", out choiceText) && BE_Dialog_choice[i].TryGetValue("ChoiceNext", out choiceNext))
                Choice_Dialog.Add(new Choice(choiceID.ToString(), choiceText.ToString(), choiceNext.ToString()));
        }

        for (int i = 0; i < SE_Dialog.Count; i++)
        {

            if (SE_Dialog[i].TryGetValue("StoryID", out storyID) && SE_Dialog[i].TryGetValue("StoryText", out storyText) && SE_Dialog[i].TryGetValue("StoryNext", out storyNext) && SE_Dialog[i].TryGetValue("StoryResult", out storyResult) && SE_Dialog[i].TryGetValue("Sprite", out spritename))
            {
                Script scr_temp = new Script(storyID.ToString(), storyText.ToString().Replace('n', '\n'), storyNext.ToString().Split('&').ToList(), storyResult.ToString(), spritename.ToString(),0);

                Script_Dialog.Add(scr_temp);
                if (scr_temp.id.Contains(story_start_str))
                    RandomPool.Instance.RandomPool_List.Add(scr_temp); // RandomPool Add
            }
        }
        for(int i = 0; i < SE_Dialog_choice.Count; i++)
        {
            if (SE_Dialog_choice[i].TryGetValue("ChoiceID", out choiceID) && SE_Dialog_choice[i].TryGetValue("ChoiceText", out choiceText) && SE_Dialog_choice[i].TryGetValue("ChoiceNext", out choiceNext))
                Choice_Dialog.Add(new Choice(choiceID.ToString(), choiceText.ToString(), choiceNext.ToString()));
        }
    }

    private List<Script> Script_Dialog = new List<Script>();
    private List<Choice> Choice_Dialog = new List<Choice>();

    public Script FindScript(string _storyID)
    {
        if (_storyID[0].Equals('B'))
        {
            RandomPool.Instance.DeleteFromRandomPool(_storyID);
        }
        foreach (Script s in Script_Dialog)
        {
            if (s.id == _storyID) return s;
        }
        return null;
    }

    public Choice FindChoice(string _choiceID)
    {
        foreach (Choice c in Choice_Dialog)
        {
            if (c.id == _choiceID) return c;
        }
        return null;
    }
}