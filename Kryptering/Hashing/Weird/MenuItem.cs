public class MenuItem(string name) : Attribute{
    public string name = name;
}

public class NewMenu(string name, Type type) : MenuItem(name){
    public Type MenuType = type;
}