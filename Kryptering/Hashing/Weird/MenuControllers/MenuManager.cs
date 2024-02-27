using System.Reflection;

class MenuManager
{
    protected string[] MenuItems;
    public virtual void Setup(){
        this.MenuItems = GetMenuItems(typeof(Menu));
    }

    protected virtual string[] GetMenuItems(Type type){
        var methods = type.GetMethods().ToArray();
        var methodsWithAttribute = methods.Where(i=>Attribute.IsDefined(i, typeof(MenuItem)));
        string[] temp = new string[methodsWithAttribute.Count()];
        for (int i = 0; i < methodsWithAttribute.Count(); i++)
        {
            var attr = methodsWithAttribute.ElementAt(i).GetCustomAttributes<MenuItem>().First();
            this.MenuItems[i] = attr.name;
            Console.WriteLine(temp[i]);
        }
        return temp;
    }

    protected virtual void MenuControl(){

    }

    public static void Write(string text, ConsoleColor consoleColor = ConsoleColor.White){
        Console.ForegroundColor = consoleColor;
        System.Console.WriteLine(text);
        Console.ResetColor();
    }
}