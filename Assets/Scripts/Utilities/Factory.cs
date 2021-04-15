
public class Factory {

    public static IInputModule NewInput(){
        #if UNITY_EDITOR
            return new EditorInput()/*TouchScreenInput()*/;
#else

            //return new TouchScreenInput();
            return new EditorInput();
#endif
    }

}