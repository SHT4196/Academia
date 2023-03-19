using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GoogleSignInClass : MonoBehaviour
{
    public Text infoText;
    public string webClientId = "310848617796-nf5a9ds97humnn8nqv5gihmnnqg6j3g4.apps.googleusercontent.com";

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    [SerializeField] private bool IsEditor = false;
    [FormerlySerializedAs("Canvas")] [SerializeField] private MainMenu Canvas; //  정확한 기능이 무엇인가요? ->
                                                                               // 로그인 후 MainCanvas의 GoInGame()을 통해 게임 내로 진입할 수 있도록 합니다!
                                                                               // FormerlySerializedAs는 단순히 변수명 바꾸기 위해서 사용했습니다.
                                                                               // (FormerlySerializedAs로 변수명을 바꾸면 Unity Editor의 inspector창에서 다시 지정하지 않아도 됩니다)

    void Start()
    {
        Invoke("SignInWithGoogle",0.5f);
    }

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        CheckFirebaseDependencies();
        if (Application.isEditor)
            IsEditor = true;
    }

    Firebase.Auth.FirebaseUser user;

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                    auth = FirebaseAuth.DefaultInstance;
                else
                    AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
            else
            {
                AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        if (!IsEditor)
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            AddToInformation("Calling SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }
        else
        {
            Canvas.GoMainMenu();
        }
    }

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            // AddToInformation("Google ID Token = " + task.Result.IdToken); 왜 주석처리 했는지 ->
            // 이건 별 의미 없고 AddToInformation 함수가 로그인 성공 확인여부로 해당 계정의 각종 정보들을 화면에 써주는 역할을 하는데
            // 다른 건 괜찮아도 IdToken이 너무 길어서 그 뒤에 제가 확인하고자 하는 정보가 안보여서 주석처리해두었습니다.
            AddToInformation("Email = " + task.Result.Email);
            SignInWithGoogleOnFirebase(task.Result.IdToken, task.Result.UserId);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken, string UID) // uid ? ->
                                                                        // 해당 계정의 task.Result.UserId를 받아와서 Player에 저장해줍니다
                                                                        // (Player.instance.setGoogleUID(UID))
                                                                        // Player에서 Firebase database에 deviceID 대신 UID로 저장합니다. 
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null) // if문의 기능이 어떻게 되나요 ->
                            // 구글 로그인 시 exception이 존재할 경우
                            // (error 발생 시 해당 error code와 error message 출력)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0)) 
                    AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                AddToInformation("Sign In Successful., UID: " + UID);
                Canvas.GoMainMenu();
                Player.instance.SetIsSignIn(true);
                Player.instance.setGoogleUID(UID);
                AddToInformation("Success2");
                //StartCoroutine(signInSetPlayer());
            }
        });
    }

    //IEnumerator signInSetPlayer() // 해당 코루틴 기능이 어떻게 되나요 ->
                                  // SignInWithGoogleOnFirebase() 함수에서 로그인에 성공했을 때 player가 이름을 적을 수 있게 namePanel을 활성화시키는 역할을 합니다.
                                  // 원인은 정확하게 파악하지 못했지만 SignInWithGoogleOnFirebase함수의 Async 람다 함수 안에서 
                                  // mainCanvas.namePanel.gameObject.SetActive(true); 을 통해
                                  // 게임 오브젝트 활성화함수를 호출하게 되면 잘 작동하지 않아서 코루틴을 동작시킬 수 있게끔 IEnumerator 함수로 새로 만들었습니다. 
    //{
       // yield return new WaitForEndOfFrame();
        //mainCanvas.namePanel.gameObject.SetActive(true);
        //StopCoroutine(signInSetPlayer());
   // }
    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void AddToInformation(string str) { infoText.text += "\n" + str; }

    
  


}