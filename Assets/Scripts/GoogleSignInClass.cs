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
    //public Text infoText;
    public string webClientId = "310848617796-nf5a9ds97humnn8nqv5gihmnnqg6j3g4.apps.googleusercontent.com";

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    [SerializeField] private bool IsEditor = false;
    [FormerlySerializedAs("Canvas")] [SerializeField] private GameStart Canvas; //  ��Ȯ�� ����� �����ΰ���? ->
                                                                                       // �α��� �� MainCanvas�� GoInGame()�� ���� ���� ���� ������ �� �ֵ��� �մϴ�!
                                                                                       // FormerlySerializedAs�� �ܼ��� ������ �ٲٱ� ���ؼ� ����߽��ϴ�.
                                                                                       // (FormerlySerializedAs�� �������� �ٲٸ� Unity Editor�� inspectorâ���� �ٽ� �������� �ʾƵ� �˴ϴ�)
    private void Awake()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        CheckFirebaseDependencies();
        if (Application.isEditor)
            IsEditor = true;
    }

    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                    auth = FirebaseAuth.DefaultInstance;
                //else
                    //AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
            //else
            //{
                //AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            //}
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
            //AddToInformation("Calling SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }
        else
        {
            Canvas.GoMainMenu();
        }
    }

    private void OnSignOut()
    {
        //AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        //AddToInformation("Calling Disconnect");
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
                    //AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    //AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            //AddToInformation("Canceled");
        }
        else
        {
            //AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            //AddToInformation("Email = " + task.Result.Email);
            // AddToInformation("Google ID Token = " + task.Result.IdToken); �� �ּ�ó�� �ߴ��� ->
            // �̰� �� �ǹ� ���� AddToInformation �Լ��� �α��� ���� Ȯ�ο��η� �ش� ������ ���� �������� ȭ�鿡 ���ִ� ������ �ϴµ�
            // �ٸ� �� �����Ƶ� IdToken�� �ʹ� �� �� �ڿ� ���� Ȯ���ϰ��� �ϴ� ������ �Ⱥ����� �ּ�ó���صξ����ϴ�.
            //AddToInformation("Email = " + task.Result.Email);
            SignInWithGoogleOnFirebase(task.Result.IdToken, task.Result.UserId);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken, string UID) // uid ? ->
                                                                        // �ش� ������ task.Result.UserId�� �޾ƿͼ� Player�� �������ݴϴ�
                                                                        // (Player.instance.setGoogleUID(UID))
                                                                        // Player���� Firebase database�� deviceID ��� UID�� �����մϴ�. 
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null) // if���� ����� ��� �ǳ��� ->
                            // ���� �α��� �� exception�� ������ ���
                            // (error �߻� �� �ش� error code�� error message ���)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0)) ;
                    //AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                //AddToInformation("Sign In Successful., UID: " + UID);
                // mainCanvas.GoInGame();
                Player.instance.SetIsSignIn(true);
                Player.instance.setGoogleUID(UID);
                //AddToInformation("Success2");
                //StartCoroutine(signInSetPlayer());
            }
        });
    }

    //IEnumerator signInSetPlayer() // �ش� �ڷ�ƾ ����� ��� �ǳ��� ->
                                  // SignInWithGoogleOnFirebase() �Լ����� �α��ο� �������� �� player�� �̸��� ���� �� �ְ� namePanel�� Ȱ��ȭ��Ű�� ������ �մϴ�.
                                  // ������ ��Ȯ�ϰ� �ľ����� �������� SignInWithGoogleOnFirebase�Լ��� Async ���� �Լ� �ȿ��� 
                                  // mainCanvas.namePanel.gameObject.SetActive(true); �� ����
                                  // ���� ������Ʈ Ȱ��ȭ�Լ��� ȣ���ϰ� �Ǹ� �� �۵����� �ʾƼ� �ڷ�ƾ�� ���۽�ų �� �ְԲ� IEnumerator �Լ��� ���� ��������ϴ�. 
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
        //AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        //AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    //private void AddToInformation(string str) { infoText.text += "\n" + str; }
}