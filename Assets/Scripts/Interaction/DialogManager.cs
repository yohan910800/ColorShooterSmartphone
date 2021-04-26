using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;
using System;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public GameObject dialogPanel;
    public TextMeshProUGUI nameTmp;
    public TextMeshProUGUI lineTmp;
    public Button nextBtn;
    public GameObject optionsPanel;
    public event Action<string,bool> OnOptionSelected;
    public event Action OnDialogEnded;

    DialogSet set;
    bool isActive;
    DialogLine currentLine;

    public DialogSet NewDialog(string fileName){
        if(isActive){
            Log.log("An other dialog is active.");
            return null;
        }
        set = LoadDialogSet(fileName);
        if(set==null) return null;
        isActive = true;
        ToNextLine();
        Static.Pause(true);
        dialogPanel.SetActive(true);
        return set;
    }

    void ToNextLine(){
        currentLine = set.NextLine();
        if(currentLine == null){
            EndDialog();
            return;
        }
        if(set.isInteractive && !currentLine.hasOptions){
            nextBtn.enabled = true;
            optionsPanel.SetActive(false);
        }
        
        nameTmp.SetText(currentLine.name);
        lineTmp.SetText(currentLine.line);
    }

    public void NextBtnPress(){
        ToNextLine();
    }
    public void OptionSelect(bool val){
        if(OnOptionSelected!=null){
            OnOptionSelected(currentLine.id, val);
        }
        set.NextBranch(val);
        ToNextLine();
    }
    
    void EndDialog(){
        set = null;
        currentLine = null;
        isActive = false;
        nameTmp.SetText("");
        lineTmp.SetText("");
        dialogPanel.SetActive(false);
        Static.Pause(false);
        if(OnDialogEnded!=null) OnDialogEnded();
    }

    DialogSet LoadDialogSet(string fileName){
        TextAsset file = Resources.Load<TextAsset>("Dialogs/"+fileName);
        if(file!=null){
            try{
                DialogSet dialog = DialogSet.CreateFromJSON(file.text);
                return dialog;
            }
            catch (Exception e){
                Log.log("Error Loading JSON");
                Log.log(e.Message);
            }
        }else{
            Log.log("Dialog JSON file does not exist.");
        }
        return null;
    }
}
