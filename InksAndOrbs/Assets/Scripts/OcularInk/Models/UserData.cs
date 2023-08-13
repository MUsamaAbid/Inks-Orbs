using Firebase.Firestore;

[FirestoreData]
public struct UserData
{
    [FirestoreProperty] public string Name { get; set; }

    [FirestoreProperty] public ScoreData Score { get; set; }

    public string id;

    public int GetScore(int level)
    {
        switch (level)
        {
            case 0:
                return Score.Level1;
            case 1:
                return Score.Level2;
            case 2:
                return Score.Level3;
            case 3:
                return Score.Level4;
            case 4:
                return Score.Level5;
            case 5:
                return Score.Level6;
            case 6:
                return Score.Level7;
            case 7:
                return Score.Level8;
            case 8:
                return Score.Level9;
            case 9:
                return Score.Level10;
        }

        return 0;
    }
}

[FirestoreData]
public struct ScoreData
{
    [FirestoreProperty] public int Level1 { get; set; }
    [FirestoreProperty] public int Level2 { get; set; }
    [FirestoreProperty] public int Level3 { get; set; }
    [FirestoreProperty] public int Level4 { get; set; }
    [FirestoreProperty] public int Level5 { get; set; }
    [FirestoreProperty] public int Level6 { get; set; }
    [FirestoreProperty] public int Level7 { get; set; }
    [FirestoreProperty] public int Level8 { get; set; }
    [FirestoreProperty] public int Level9 { get; set; }
    [FirestoreProperty] public int Level10 { get; set; }
}