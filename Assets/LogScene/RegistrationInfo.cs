using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class RegistrationInfo
{
    public string UserName;
    public string Name;
    public string Email;
    public int Steps;

    public RegistrationInfo()
    {
        UserName = RegistrationHandler.UserName;
        Name = RegistrationHandler.Name;
        Email = RegistrationHandler.EmailID;
        Steps = RegistrationHandler.Steps;
        
    }
}
