using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Proyecto26;
using UnityEngine.SceneManagement;
public class AuthController : MonoBehaviour
{
    
    public static bool LoggedIn = false;
    public InputField emailInput, passwordInput;
    public GameObject authPanel, registrationPanel;
    AndroidJavaObject currentActivity;
    
    void Start()
    {
        //currentActivity androidjavaobject must be assigned for toasts to access currentactivity;
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }
    public void Login()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
            passwordInput.text).ContinueWith((task =>
            {
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (!task.IsCompleted) return;
                LoggedIn = true;
                print(LoggedIn);
                
            }));
    }

    void Update()
    {
        if (LoggedIn)
        {
            SceneManager.LoadScene("FeedScene");
        }
    }
   
    
    
    public void Logout()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
            print("User is Logged Out");
        }
    }

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();
        SendToast(msg);
    }

    
    public void Register()
    {
        authPanel.SetActive(false);
        registrationPanel.SetActive(true);
        
    }

    public void SendToast(string message)
    {
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }
}
