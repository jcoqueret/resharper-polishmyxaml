using System;
using System.Xml.Serialization;
using JetBrains.UI.Updates;
using JetBrains.VSIntegration.Updates;

namespace Julco.ReSharper.PolishMyXaml
{
    [XmlType]
    [XmlRoot("PluginLocalInfo")]
    [Serializable]
    public class PluginLocalEnvironmentInfo
    {
        [XmlElement]
        [UpdatesLocalInfoManager.QueryStringContainer]
        public UpdateLocalEnvironmentInfoVs LocalEnvironment = new UpdateLocalEnvironmentInfoVs();

        [XmlElement]
        [UpdatesLocalInfoManager.QueryStringContainer]
        public UpdateLocalEnvironmentInfo.VersionSubInfo PluginVersion = new UpdateLocalEnvironmentInfo.VersionSubInfo();
    }
}