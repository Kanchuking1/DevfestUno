using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Proyecto26;
public class RegistrationHandler : MonoBehaviour
{
    public GameObject registrationCanvas;
    public GameObject loginCanvas;
    
    public InputField RegEmail;
    public InputField password;
    public InputField reEnterPass;
    public InputField userName;
    public InputField nameField;
    
    public static string UserName;
    public static string EmailID;
    public static string Name;
    public static int Steps;
    
    public static bool RegistrationComplete = false;

    AndroidJavaObject currentActivity;
    private void Start()
    {
        //currentActivity androidjavaobject must be assigned for toasts to access currentactivity;
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public void RegisterUser()
    {
        if(RegEmail.text.Equals("") && password.text.Equals(""))
        {
            SendToast("Enter Email/Password");
            return;
        }
        if (password.text != reEnterPass.text)
        {
            
            SendToast("Passwords don't match");
            return;
        }
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(RegEmail.text,
            password.text).ContinueWith((task =>
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
            
            if (task.IsCompleted)
            {
                print("Registration COMPLETE");
                RegistrationComplete = true;
                
                
            }
        }));
        
        UserName = userName.text;
        EmailID = RegEmail.text;
        Name = nameField.text;
        Steps = 0;
        PostToDatabase();
        GoToLoginCanvas();
    }
    
    
    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();
        SendToast(msg);
    }
    
    public void PostToDatabase()
    {
        Debug.Log("yes");
        RegistrationInfo user = new RegistrationInfo();
        RestClient.Put("https://unlock-26c72.firebaseio.com/"+user.UserName+".json",user);
        GoToLoginCanvas();
        
    }

    public void GoToLoginCanvas()
    {
        registrationCanvas.SetActive(false);
        loginCanvas.SetActive(true);
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
