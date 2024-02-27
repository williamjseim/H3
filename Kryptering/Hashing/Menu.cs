using Hashing;

public class Menu{

    [MenuItem("WriteText")]
    public void Input(){
        Program.Write("Write Plain text", ConsoleColor.Red);
        SingleTon.Instance.InputText = Console.ReadLine()!;
    }
}