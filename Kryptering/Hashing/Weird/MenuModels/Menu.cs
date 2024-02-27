using Hashing;

public class Menu{

    [MenuItem("New text to hash")]
    public void Input(){
        MenuManager.Write("Write Plain text", ConsoleColor.Red);
        SingleTon.Instance.InputText = Console.ReadLine()!;
    }

    [NewMenu("Choose hash type", typeof(HashMenuManager))]
    public void ChooseHashType(){

    }
}