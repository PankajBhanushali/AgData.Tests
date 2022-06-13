using System;

namespace AgData.Tests.UiTests.Pages
{
    public class AllPages
    {
        [ThreadStatic]
        public static CareersPage CareersPage;
        public static CareerDetailsPage CareerDetailsPage;

        public static void Init()
        {
            CareersPage = new CareersPage();
            CareerDetailsPage = new CareerDetailsPage();
        }
    }
}
