using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Little namespace for common things like enums that will be used in multiple scripts
namespace MankindGames {   
    public enum CharacterType {        
        Player,
        Enemy1
    }

    public enum Orientation
    {
        Up, 
        UpRight,
        DownRight,
        Down,
        DownLeft,
        UpLeft,
    }

    public enum Colors {
        Brown,
        Black,
        Blue,
        Green,
        Orange,
        Red,
        Pink,
        None
    }

    public class Static{
        public static Dictionary<Colors,Color> worldColors;
        public static Dictionary<Colors,Color> characterColors;
        public static Dictionary<Colors,Color> bulletColors;
        public static int colorDmgMultiplier = 2;
        public static bool GamePaused{get; private set;}

        public static void Pause(bool val){
            GamePaused = val;
            Time.timeScale = val? 0:1;
        }
    }

    public class Log{
        public static void log(string str){
            if(LogUI.Instance != null){
                LogUI.Instance.Log(str);
            }else{
                Debug.Log(str);
            }
        }
    }
}