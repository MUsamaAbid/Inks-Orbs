using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class NameModal : BasicModal
{
    [SerializeField] private TMP_InputField nameInput;

    public async void SaveName()
    {
        var username = nameInput.text;

        if (username.Trim().Length <= 3)
        {
            AlertManager.Instance.ShowSingleAlert("ERROR", "Name must be longer than 3 letters");
            return;
        }
        
        if (username.Contains(" "))
        {
            AlertManager.Instance.ShowSingleAlert("ERROR", "Name cannot contain spaces");
            return;
        }
        
        await UserManager.Instance.SetName(username);
        AlertManager.Instance.ShowSingleAlert("SUCCESS", "Changed name successfully!");
        Hide();
    }
}
