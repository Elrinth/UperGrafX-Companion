using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UperGrafX_Companion
{
    [Serializable]
    public class Settings
    {
        [DefaultValue("")]
        public string LocationOfPCEFiles { get; set; }

        [DefaultValue("_ugx-02.pce")]
        private string mReplacePCEWith = "_ugx-02.pce";
        public string ReplacePCEWith { get { return mReplacePCEWith; } set { mReplacePCEWith = value; } }

        [DefaultValue("")]
        public string LocationOfCCDFiles { get; set; }
        [DefaultValue("")]
        public string LocationOfBinCueFiles { get; set; }
        [DefaultValue("")]
        public string LocationOfCdmFiles { get; set; }

        [DefaultValue("ikaebi.exe")]
        private string mLocationOfIkaebi = "ikaebi.exe";
        public string LocationOfIkaebi { get { return mLocationOfIkaebi; } set { mLocationOfIkaebi = value; } }

        [DefaultValue("C:\\Program Files\\DAEMON Tools Lite\\DTCommandLine.exe")]
        private string mLocationOfDaemonToolsCL = "C:\\Program Files\\DAEMON Tools Lite\\DTCommandLine.exe";
        public string LocationOfDaemonToolsCL { get { return mLocationOfDaemonToolsCL; } set { mLocationOfDaemonToolsCL = value; } }

        [DefaultValue(false)]
        public bool OverwritePCEFiles { get; set; }
        [DefaultValue(false)]
        public bool OverwriteBinCueFiles { get; set; }

        [DefaultValue(true)]
        private bool mConvertToJapaneseRegion = true;
        public bool ConvertToJapaneseRegion { get { return mConvertToJapaneseRegion; } set { mConvertToJapaneseRegion = value; } }

    }
}
