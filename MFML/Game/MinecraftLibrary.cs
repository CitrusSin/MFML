using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Game
{
    public class MinecraftLibrary
    {
        public string name;
        public LibraryDownloads downloads;
        public Dictionary<string, string> natives;
        public Dictionary<string, List<string>> extract;
        public List<Rule> rules;
    }
}
