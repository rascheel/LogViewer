using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    static class Utils
    {
        public static string PublishVersion
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                else
                    return "Not Published";
            }
        }

        public static string PreviousPath
        {
            get
            {
                return Properties.Settings.Default.PreviousPath;
            }

            set
            {
                Properties.Settings.Default.PreviousPath = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
