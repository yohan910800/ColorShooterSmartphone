using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MankindGames;

public class TestInteractionManager : MonoBehaviour {

    public string jsonFileDialogName;
    public FloatingDialogManager dialog;
    public DialogManager dialog2;

    Interactable interaction;
    
    DialogSet set;

    void Start()
    {
            interaction = GetComponent<Interactable>();
            interaction.OnTrigger += OnTrigger;
    }

    void Update() {
        
    }
    
    void OnTrigger(){
        if (dialog != null)
        {
            dialog.NewDialog(jsonFileDialogName);
            dialog.OnOptionSelected += OnOptionSelect;
            dialog.OnDialogEnded += OnDialogEnd;
        }
        else if(dialog2!=null)
        {
            dialog2.NewDialog(jsonFileDialogName);
            dialog2.OnOptionSelected += OnOptionSelect;
            dialog2.OnDialogEnded += OnDialogEnd;
        }
    }

    void OnOptionSelect(string id, bool val){
        string opt = val? "YES" : "NO";
        Log.log(opt + " Option selected for line id " + id);
        if(val){
            // Executed if the player clicks yes
        }else{
            // Executed if the player clicks no
        }
    }

    void OnDialogEnd(){
        if (dialog != null)
        {
            dialog.OnOptionSelected -= OnOptionSelect;
            dialog.OnDialogEnded -= OnDialogEnd;
        }
        else if (dialog2 != null)
        {
            dialog2.OnOptionSelected -= OnOptionSelect;
            dialog2.OnDialogEnded -= OnDialogEnd;
        }
    }

    void OnDestroy(){
        
            interaction.OnTrigger -= OnTrigger;
        

        if (dialog != null)
        {
            
            dialog.OnOptionSelected -= OnOptionSelect;
            dialog.OnDialogEnded -= OnDialogEnd;
        }
        else if (dialog2 != null)
        {
            dialog2.OnOptionSelected -= OnOptionSelect;
            dialog2.OnDialogEnded -= OnDialogEnd;
        }
    }
}
