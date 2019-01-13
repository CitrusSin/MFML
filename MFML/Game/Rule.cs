using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Game
{
    public class Rule
    {
        public class OS
        {
            public string name;
            public string arch;
            public string version;
            public bool ConditionRight
            {
                get
                {
                    if (this.name == "windows")
                    {
                        if (this.arch != null && this.arch != ((Environment.Is64BitProcess) ? "x64" : "x86"))
                        {
                            return false;
                        }
                        if (this.version != null &&
                            this.version.Substring(1, version.IndexOf('.')) != Environment.OSVersion.Version.Major.ToString())
                        {
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
            }
        }

        public string action;
        public OS os;
        public Dictionary<string, bool> features;

        public bool Allowed
        {
            get
            {
                if (os != null && !os.ConditionRight)
                {
                    return !AllowIfConditionRight;
                }
                if (features != null)
                {
                    return !AllowIfConditionRight;
                }
                return AllowIfConditionRight;
            }
        }

        public bool AllowIfConditionRight
        {
            get { return action == "allow"; }
        }
    }
}
