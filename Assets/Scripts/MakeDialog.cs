using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class MakeDialog
{
    private static MakeDialog _instance;
    public static MakeDialog instance
    {
        get
        {
            if (_instance == null) 
            { 
                _instance = new MakeDialog();
            }
            return _instance;
        }
    }
    /// <summary>
    /// csv Parsing
    /// </summary>
    public MakeDialog()
    {
        List<Dictionary<string, object>> ME_Dialog = CSVReader.Read("ME");
        List<Dictionary<string, object>> BE_Dialog = CSVReader.Read("BE");
        List<Dictionary<string, object>> SE_Dialog = CSVReader.Read("SE");
        List<Dictionary<string, object>> PME_Dialog = CSVReader.Read("PME");
        List<Dictionary<string, object>> SE_Dialog_choice = CSVReader.Read("SE_choice");
        List<Dictionary<string, object>> ME_Dialog_choice = CSVReader.Read("ME_choice");
        List<Dictionary<string, object>> BE_Dialog_choice = CSVReader.Read("BE_choice");
        List<Dictionary<string, object>> PME_Dialog_choice = CSVReader.Read("PME_choice");

        object storyID = null;
        object storyText = null;
        object storyNext = null;
        object choiceID = null;
        object choiceText = null;
        object choiceNext = null;
        object storyResult = null;
        object spritename = null;
        object storyInterval = null;
        object afterInterval = null;
        object probability = null;
        object acvUpdate = null;



        string story_start_str = "_1";
        for (int i = 0; i < ME_Dialog.Count; i++)
        {

            if (ME_Dialog[i].TryGetValue("StoryID", out storyID) && ME_Dialog[i].TryGetValue("StoryText", out storyText) && ME_Dialog[i].TryGetValue("StoryNext", out storyNext) && ME_Dialog[i].TryGetValue("StoryResult", out storyResult) && ME_Dialog[i].TryGetValue("Sprite", out spritename) && ME_Dialog[i].TryGetValue("StoryInterval", out storyInterval) && ME_Dialog[i].TryGetValue("AfterInterval", out afterInterval) && ME_Dialog[i].TryGetValue("AcvUpdate", out acvUpdate))
            {
                Script_Dialog.Add(new Script(storyID.ToString(), storyText.ToString().Replace('$', '\n'), storyNext.ToString().Split('&').ToList(), storyResult.ToString().Split('&').ToList(), spritename.ToString(), acvUpdate.ToString().Split('#').ToList(), Int32.Parse(storyInterval.ToString()), afterInterval.ToString()));

            }
           
        }

        for (int i = 0; i < ME_Dialog_choice.Count; i++)
        {
            if (ME_Dialog_choice[i].TryGetValue("ChoiceID", out choiceID) && ME_Dialog_choice[i].TryGetValue("ChoiceText", out choiceText) && ME_Dialog_choice[i].TryGetValue("ChoiceNext", out choiceNext))
                Choice_Dialog.Add(new Choice(choiceID.ToString(), choiceText.ToString().Replace('$', '\n'), choiceNext.ToString().Split('&').ToList()));
        }

        for (int i = 0; i < BE_Dialog.Count; i++)
        {

            if (BE_Dialog[i].TryGetValue("StoryID", out storyID) && BE_Dialog[i].TryGetValue("StoryText", out storyText) && BE_Dialog[i].TryGetValue("StoryNext", out storyNext) && BE_Dialog[i].TryGetValue("StoryResult", out storyResult) && BE_Dialog[i].TryGetValue("Sprite", out spritename) && BE_Dialog[i].TryGetValue("AcvUpdate", out acvUpdate))
            {
                Script scr_temp = new Script(storyID.ToString(), storyText.ToString().Replace('$', '\n'), storyNext.ToString().Split('&').ToList(), storyResult.ToString().Split('&').ToList(), spritename.ToString(), acvUpdate.ToString().Split('#').ToList(),0);

                Script_Dialog.Add(scr_temp);
                if(scr_temp.id.Contains(story_start_str))
                    RandomPool.Instance.RandomPoolList.Add(scr_temp); // RandomPool Add
            }
           
        }

        for (int i = 0; i < BE_Dialog_choice.Count; i++)
        {
            if (BE_Dialog_choice[i].TryGetValue("ChoiceID", out choiceID) && BE_Dialog_choice[i].TryGetValue("ChoiceText", out choiceText) && BE_Dialog_choice[i].TryGetValue("ChoiceNext", out choiceNext))
                Choice_Dialog.Add(new Choice(choiceID.ToString(), choiceText.ToString().Replace('$', '\n'), choiceNext.ToString().Split('&').ToList()));
        }

        for (int i = 0; i < SE_Dialog.Count; i++)
        {

            if (SE_Dialog[i].TryGetValue("StoryID", out storyID) && SE_Dialog[i].TryGetValue("StoryText", out storyText) && SE_Dialog[i].TryGetValue("StoryNext", out storyNext) && SE_Dialog[i].TryGetValue("StoryResult", out storyResult) && SE_Dialog[i].TryGetValue("Sprite", out spritename) && SE_Dialog[i].TryGetValue("AcvUpdate", out acvUpdate))
            {
                Script scr_temp = new Script(storyID.ToString(), storyText.ToString().Replace('$', '\n'), storyNext.ToString().Split('&').ToList(), storyResult.ToString().Split('&').ToList(), spritename.ToString(), acvUpdate.ToString().Split('#').ToList(),0);

                Script_Dialog.Add(scr_temp);
                if (scr_temp.id.Contains(story_start_str))
                    RandomPool.Instance.RandomPoolList.Add(scr_temp); // RandomPool Add
            }
        }
        for(int i = 0; i < SE_Dialog_choice.Count; i++)
        {
            if (SE_Dialog_choice[i].TryGetValue("ChoiceID", out choiceID) && SE_Dialog_choice[i].TryGetValue("ChoiceText", out choiceText) && SE_Dialog_choice[i].TryGetValue("ChoiceNext", out choiceNext))
                Choice_Dialog.Add(new Choice(choiceID.ToString(), choiceText.ToString().Replace('$', '\n'), choiceNext.ToString().Split('&').ToList()));
        }
        for (int i = 0; i < PME_Dialog.Count; i++)
        {

            if (PME_Dialog[i].TryGetValue("StoryID", out storyID) && PME_Dialog[i].TryGetValue("StoryText", out storyText) && PME_Dialog[i].TryGetValue("StoryNext", out storyNext) && PME_Dialog[i].TryGetValue("StoryResult", out storyResult) && PME_Dialog[i].TryGetValue("Sprite", out spritename) && PME_Dialog[i].TryGetValue("StoryInterval", out storyInterval) && PME_Dialog[i].TryGetValue("AfterInterval", out afterInterval) && PME_Dialog[i].TryGetValue("AcvUpdate", out acvUpdate) && PME_Dialog[i].TryGetValue("Probability", out probability))
            {
                Script_Dialog.Add(new Script(storyID.ToString(), storyText.ToString().Replace('$', '\n'), storyNext.ToString().Split('&').ToList(), storyResult.ToString().Split('&').ToList(), spritename.ToString(), acvUpdate.ToString().Split('#').ToList(), Int32.Parse(storyInterval.ToString()), afterInterval.ToString(), Int32.Parse(probability.ToString())));
            }
           
        }

        for (int i = 0; i < PME_Dialog_choice.Count; i++)
        {
            if (PME_Dialog_choice[i].TryGetValue("ChoiceID", out choiceID) && PME_Dialog_choice[i].TryGetValue("ChoiceText", out choiceText) && PME_Dialog_choice[i].TryGetValue("ChoiceNext", out choiceNext))
                Choice_Dialog.Add(new Choice(choiceID.ToString(), choiceText.ToString().Replace('$', '\n'), choiceNext.ToString().Split('&').ToList()));
        }
    }

    private List<Script> Script_Dialog = new List<Script>();
    private List<Choice> Choice_Dialog = new List<Choice>();

    /// <summary>
    /// storyID를 통해 Script를 찾을 수 있는 함수
    /// </summary>
    /// <param name="storyID">Script story ID</param>
    /// <returns>해당 story ID를 가진 Script</returns>
    public Script FindScript(string storyID)
    {
        int randomValue = Random.Range(1, 101);
        if (storyID[0].Equals('B'))
        {
            RandomPool.Instance.DeleteFromRandomPool(storyID);
        }

        List<Script> PMEscripts = new List<Script>();
        foreach (Script s in Script_Dialog)
        {
            if (!storyID.Contains("#"))
            {
                if (s.id.Contains("#"))
                {
                    if (s.id.Split('#')[1] == storyID)
                    {
                        PMEscripts.Add(s);
                        // return s;
                    }
                }
                else if (s.id == storyID) return s;
            }
            else if (s.id == storyID) return s;
        }

        if (PMEscripts.Count != 0)
        {
            int tempValue = 0;
            for (int i = 0; i < PMEscripts.Count; i++)
            {
                tempValue += PMEscripts[i].probability;
                if (tempValue >= randomValue)
                {
                    // Debug.Log($"tempvalue: {tempValue}, randomValue: {randomValue}");
                    return PMEscripts[i];
                }
            }
        }
        return null;
    }
    /// <summary>
    /// choiceID를 통해 Choice를 찾을 수 있는 함수
    /// </summary>
    /// <param name="choiceID">Choice choiceID</param>
    /// <returns>해당 choiceID를 가진 Choice</returns>
    public Choice FindChoice(string choiceID)
    {
        foreach (Choice c in Choice_Dialog)
        {
            if (c.id == choiceID) return c;
        }
        return null;
    }
}