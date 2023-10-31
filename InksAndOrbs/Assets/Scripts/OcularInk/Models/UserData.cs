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
            case 10:
                return Score.Level11;
            case 11:
                return Score.Level12;
            case 12:
                return Score.Level13;
            case 13:
                return Score.Level14;
            case 14:
                return Score.Level15;
            case 15:
                return Score.Level16;
            case 16:
                return Score.Level17;
            case 17:
                return Score.Level18;
            case 18:
                return Score.Level19;
            case 19:
                return Score.Level20;
            case 20:
                return Score.Level21;
            case 21:
                return Score.Level22;
            case 22:
                return Score.Level23;
            case 23:
                return Score.Level24;
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
    [FirestoreProperty] public int Level11 { get; set; }
    [FirestoreProperty] public int Level12 { get; set; }
    [FirestoreProperty] public int Level13 { get; set; }
    [FirestoreProperty] public int Level14 { get; set; }
    [FirestoreProperty] public int Level15 { get; set; }
    [FirestoreProperty] public int Level16 { get; set; }
    [FirestoreProperty] public int Level17 { get; set; }
    [FirestoreProperty] public int Level18 { get; set; }
    [FirestoreProperty] public int Level19 { get; set; }
    [FirestoreProperty] public int Level20 { get; set; }
    [FirestoreProperty] public int Level21 { get; set; }
    [FirestoreProperty] public int Level22 { get; set; }
    [FirestoreProperty] public int Level23 { get; set; }
    [FirestoreProperty] public int Level24 { get; set; }
}