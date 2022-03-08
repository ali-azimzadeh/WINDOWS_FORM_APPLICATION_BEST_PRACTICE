using System;
using System.Collections.Generic;
using System.Text;

namespace Azx.Windows.Security
{
    public class AppSettingsEncription : object
    {
        public AppSettingsEncription() : base()
        {
        }

        public void Encription(string app_ConfigPathName)
        {
            AppSettingsProtection(app_ConfigPathName, true);
        }

        public void Decription(string app_ConfigPathName)
        {
            AppSettingsProtection(app_ConfigPathName, false);
        }

        private void AppSettingsProtection(string pathName, bool protect)
        {
            // Define the Dpapi provider name.
            string strProvider = "DataProtectionConfigurationProvider";
            // string strProvider = "RSAProtectedConfigurationProvider";

            System.Configuration.Configuration oConfiguration = null;
            System.Configuration.AppSettingsSection oSection = null;

            try
            {
                // Open the configuration file and retrieve the connectionStrings section.

                // For Web!
                // oConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

                // For Windows!
                // Takes the executable file name without the config extension.
                oConfiguration = System.Configuration.ConfigurationManager.OpenExeConfiguration(pathName);

                if (oConfiguration != null)
                {
                    bool blnChanged = false;
                    oSection = oConfiguration.GetSection("appSettings") as System.Configuration.AppSettingsSection;

                    if (oSection != null)
                    {
                        if ((!(oSection.ElementInformation.IsLocked)) && (!(oSection.SectionInformation.IsLocked)))
                        {
                            if (protect)
                            {
                                if (!(oSection.SectionInformation.IsProtected))
                                {
                                    blnChanged = true;

                                    // Encrypt the section.
                                    oSection.SectionInformation.ProtectSection(strProvider);
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
                            oConfiguration.Save();
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
