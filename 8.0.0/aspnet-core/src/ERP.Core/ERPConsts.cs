using ERP.Debugging;

namespace ERP
{
    public class ERPConsts
    {
        public const string LocalizationSourceName = "ERP";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "fd59e7ef34ad41819f5233b2d776093c";
    }
}
