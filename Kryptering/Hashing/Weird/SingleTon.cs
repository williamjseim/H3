class SingleTon
{
    private static SingleTon? _instance;
    public static SingleTon Instance { get{ if(_instance == null) _instance = new(); return _instance; } }

    public string InputText = "";
}