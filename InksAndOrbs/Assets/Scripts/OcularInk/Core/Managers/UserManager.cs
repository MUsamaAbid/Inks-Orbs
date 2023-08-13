using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Firestore;
using OcularInk.Utils;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
    public UserData User { get; private set; }

    public Action<UserData> onSnapshot;

    public async Task CreateUserIfNotExists(string uid)
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        var snapshot = await firestore.Collection("Users").Document(uid).GetSnapshotAsync();

        if (snapshot.Exists)
        {
            return;
        }

        var user = new UserData()
        {
            Name = ""
        };

        await firestore.Collection("Users").Document(uid).SetAsync(user);
    }

    public void ListenToUser(string uid)
    {
        FirebaseFirestore.DefaultInstance.Collection("Users").Document(uid).Listen((doc) =>
        {
            var data = doc.ConvertTo<UserData>();
            data.id = doc.Id;
            User = data;
            onSnapshot.Invoke(User);
        });
    }
    
    public async Task<List<UserData>> GetLeaderboard(int level)
    {
        try
        {
            var snapshot = await FirebaseFirestore.DefaultInstance.Collection("Users").OrderByDescending($"Score.Level{level + 1}").Limit(50).GetSnapshotAsync();

            var users = snapshot.Documents.Select(doc => doc.ConvertTo<UserData>());

            foreach (var userData in users)
            {
                Debug.Log(userData.Name);
            }

            return users.ToList();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    public void SaveScore(int score, int level)
    {
        if (AuthService.Instance.IsGuest || User.id == null)
        {
            return;
        }

        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Collection("Users").Document(User.id).UpdateAsync($"Score.Level{level + 1}", score);
    }

    public Task SetName(string newName)
    {
        return FirebaseFirestore.DefaultInstance.Collection("Users").Document(User.id).UpdateAsync("Name", newName);
    }
}
