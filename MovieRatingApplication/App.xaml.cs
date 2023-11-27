namespace MovieRatingApplication
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjkwODMxMkAzMjMzMmUzMDJlMzBQcndIM1lHamlDRWJBSzlia2xTa1lNRGVOS3VIQlFlOGQwNTNzQ0xNeDM4PQ==");
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}