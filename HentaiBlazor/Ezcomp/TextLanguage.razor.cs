using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public partial class TextLanguage
    {

        [Parameter]
        public string Language { get; set; }

        private static Dictionary<string, string> _language;

        static TextLanguage() 
        {
            _language = new Dictionary<string, string>();
            _language.Add("ja", "日本語");
            _language.Add("zh", "中文");
            _language.Add("en", "English");
        }

        private string _text()
        {
            return _language.GetValueOrDefault(Language, "");
        }

    }
}
