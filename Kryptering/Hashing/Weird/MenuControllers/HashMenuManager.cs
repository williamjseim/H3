class HashMenuManager : MenuManager
{
    public override void Setup()
    {
        this.MenuItems = this.GetMenuItems(typeof(HashMenuManager));
    }
}