using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
