using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager instance = null;

    public static DatabaseManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    
    public DatabaseReference Reference;

    public FirebaseAuth auth;
    // Start is called before the first frame update
    void Start()
    { 
        Reference = FirebaseDatabase.DefaultInstance.GetReference("users");
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SetPlayerName_DB(string playerName, string uniqueID)
    {
        Reference.Child(uniqueID).Child("playerName").SetValueAsync(playerName);
    }

    public void SaveAchievementInfo(string uniqueID, int achievementID, int nowNum, AchieveState achieveState, string timeStamp = "")
    {
        Reference.Child(uniqueID).Child("Achievement").Child(achievementID.ToString()).Child("nowNum").SetValueAsync(nowNum);
        Reference.Child(uniqueID).Child("Achievement").Child(achievementID.ToString()).Child("AchieveState").SetValueAsync(achieveState.ToString());
        Reference.Child(uniqueID).Child("Achievement").Child(achievementID.ToString()).Child("timeStamp").SetValueAsync(timeStamp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
