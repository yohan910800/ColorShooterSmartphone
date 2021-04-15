using UnityEngine;
using MankindGames;

[System.Serializable]
public class DialogSet {

    public bool isInteractive;
    public DialogBranch[] branches;
    
    int lineIdx;
    int branchIdx;
    DialogLine currentLine;
    DialogBranch currentBranch;
    
    public static DialogSet CreateFromJSON(string jsonString){
        DialogSet set = JsonUtility.FromJson<DialogSet>(jsonString);
        set.currentBranch = set.branches[0];
        return set;
    }

    public DialogLine NextLine(){
        if(lineIdx == currentBranch.lines.Length){
            return null;
        }
        if(branchIdx >= branches.Length){
            Log.log("The dialog JSON is missing a branch.");
            return null;
        }
        currentLine = branches[branchIdx].lines[lineIdx];
        lineIdx++;
        return currentLine;
    }

    public void NextBranch(bool option){
        branchIdx = option? branchIdx+1 : branchIdx+2;
        currentBranch = branches[branchIdx];
        lineIdx = 0;
    }
}

[System.Serializable]
public class DialogBranch{
    public DialogLine[] lines;
}

[System.Serializable]
public class DialogLine {
    public string id;
    public string spriteName;
    public string name;
    public string line;
    public bool hasOptions;
}
