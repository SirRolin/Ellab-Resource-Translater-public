using Ellab_Resource_Translater.Util;

namespace Ellab_Resource_Translater.Translators
{
    internal class EMSuite(TranslationService? translationService, ConnectionProvider? connProv, CancellationTokenSource source) : DBProcessorBase(translationService, connProv, source, langToLocal: getLocaleVariant(), 1, Config.Get().threadsToUse)
    {
        private static Func<string, string> getLocaleVariant() => lang => "." + lang.ToLower();

        internal void Run(string path, ListView view, Label progresText)
        {
            Run(path, view, progresText, new(@".*\\Resources\\.*(?<!\.[\w-]*)\.resx"));
        }
    }
}
