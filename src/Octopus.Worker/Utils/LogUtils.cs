namespace Octopus.Sync.Utils
{
    public static class LogUtils
    {
        public static string GetBanner()
        {
            var version = GetVersionForBanner();
            var banner = string.Format("\r\n///////////////////////////////////////\r\n///////////////////////////////////////\r\n   ____  " +
                "     _                        \r\n  / __ \\     | |                       \r\n | |  | | ___| |_ ___  _ __  _   _ ___ \r\n | |  " +
                "| |/ __| __/ _ \\| '_ \\| | | / __|\r\n | |__| | (__| || (_) | |_) | |_| \\__ \\\r\n  \\____/ \\___|\\__\\___/| .__/ \\__,_|___/\r\n " +
                "                     | |              \r\n                      |_|  v{0}       \r\n\r\n///////////////////////////////////////\r\n///////////////////////////////////////", version);
            return banner;
        }

        private static string GetVersionForBanner()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion?.Substring(0, fvi.ProductVersion.IndexOf('+')) ?? string.Empty;
            return version;
        }
    }
}
