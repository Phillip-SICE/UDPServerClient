using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagOperation
{
    public static class TagOperation
    {
        public static string AddTag(string tag, string content)
        {
            string tagStart = "<" + tag + ">";
            string tagEnd = "<\\" + tag + ">";
            return tagStart + content + tagEnd;
        }
    }
}
