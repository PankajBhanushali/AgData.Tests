namespace AgData.Tests.UiTests.Pages
{
    public abstract class PageBase
    {
        public readonly NavigationBar HeaderNav;

        public PageBase()
        {
            HeaderNav = new NavigationBar();
        }
    }
}
