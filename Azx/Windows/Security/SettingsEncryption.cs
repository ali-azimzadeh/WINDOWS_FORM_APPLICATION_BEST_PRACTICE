using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azx.Windows.Security
{
    public class SettingsEncryption : object
    {
        public SettingsEncryption() : base()
        {
            // Define the Dpapi provider name.
            Provider = "RSAProtectedConfigurationProvider";
            ///"DataProtectionConfigurationProvider";
            // string strProvider = "RSAProtectedConfigurationProvider";
        }

        protected string Provider { get; }
        public void Encription(string app_ConfigPathName, SettingsSection settingsSectionType, string assemblyName)
        {
            AppSettingsProtection(pathName: app_ConfigPathName, IsProtect: true,
                settingsSectionType: settingsSectionType, assemblyName: assemblyName);
        }

        public void Decription(string app_ConfigPathName, SettingsSection settingsSectionType, string assemblyName)
        {
            AppSettingsProtection(pathName: app_ConfigPathName, IsProtect: true,
                settingsSectionType: settingsSectionType, assemblyName: assemblyName);
        }

        private void AppSettingsProtection(string pathName, bool IsProtect, SettingsSection settingsSectionType, string assemblyName)
        {

            System.Configuration.Configuration oConfiguration = null;

            System.Configuration.AppSettingsSection appSettingsSection = null;

            System.Configuration.ConnectionStringsSection connectionStringsSection = null;

            ConfigurationSection oSection = null;

            try
            {
                // Open the configuration file and retrieve the connectionStrings section.

                // For Web!
                // oConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

                // For Windows!
                // Takes the executable file name without the config extension.
                oConfiguration =
                    System.Configuration.ConfigurationManager.OpenExeConfiguration(pathName);

                //دستور زیر کار نمی کند 
                //oConfiguration = ConfigurationManager.OpenExeConfiguration(
                //                               ConfigurationUserLevel.PerUserRoamingAndLocal);

                if (oConfiguration != null)
                {
                    bool blnChanged = false;

                    switch (settingsSectionType)
                    {
                        case SettingsSection.AppSetting:
                            {
                                appSettingsSection =
                                    oConfiguration.GetSection("appSettings") as System.Configuration.AppSettingsSection;

                                oSection = appSettingsSection;

                                break;
                            }
                        case SettingsSection.ConnectionStringSetting:
                            {
                                connectionStringsSection =
                                    oConfiguration.GetSection("connectionStrings") as System.Configuration.ConnectionStringsSection;

                                oSection = connectionStringsSection;

                                break;
                            }
                        case SettingsSection.UserSetting:
                            {

                                string sectionName =
                                    $"userSettings/{assemblyName}.Properties.Settings";

                                oSection =
                                   oConfiguration.GetSection(sectionName: sectionName);

                                break;
                            }
                    }

                    if (oSection != null)
                    {
                        if ((!(oSection.ElementInformation.IsLocked)) && (!(oSection.SectionInformation.IsLocked)))
                        {
                            if (IsProtect)
                            {
                                if (!(oSection.SectionInformation.IsProtected))
                                {
                                    blnChanged = true;

                                    // Encrypt the section.
                                    oSection.SectionInformation.ProtectSection(Provider);
                                }
                            }
                            else
                            {
                                if (oSection.SectionInformation.IsProtected)
                                {
                                    blnChanged = true;

                                    // Remove encryption.
                                    oSection.SectionInformation.UnprotectSection();
                                }
                            }
                        }

                        if (blnChanged)
                        {
                            // Indicates whether the associated configuration section will be saved even if it has not been modified.
                            oSection.SectionInformation.ForceSave = true;

                            // Save the current configuration.
                            oConfiguration.Save(ConfigurationSaveMode.Full);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }
    }
}
