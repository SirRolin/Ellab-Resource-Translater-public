using Ellab_Resource_Translater.Objects;
using Ellab_Resource_Translater.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellab_Resource_Translater.Translators
{
    internal class ValSuite(TranslationService? translationService, ConnectionProvider? DBCon, CancellationTokenSource source) : DBProcessorBase(translationService, DBCon, source, getLocaleVarient(), 0, Config.Get().threadsToUse)
    {
        internal string[] folders = ["dottxt20", "Popup20", "ReportTxtStr"];

        private static Func<string, string> getLocaleVarient() => lang => "." + lang.ToLower() + "-" + uppercaseShort(lang);

        internal void Run(string path, ListView view, Label progresText)
        {
            string folderStr = String.Join('|', folders);

            Run(path,
                view,
                progresText,
                // \\(dottxt20|popup20|ReportTxtstr)\\ means that it has to be in a folder that's either dottxt20, popup20 or ReportTxtstr.
                new($@".*\\({folderStr})\\.*(?<!\.[\w-]*)\.resx"));
        }

        private static string uppercaseShort(string shortLang)
        {
            return shortLang.ToLower() switch
            {
                "zh" => "CH",
                "sv" => "SE",
                "ja" => "JP",
                "ar" => "SA",
                _ => shortLang.ToUpper()
            };
        }
    }
}
