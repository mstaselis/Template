using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model.Util
{
    public class SystemLanguages
    {
        public ICollection<Language> Languages { get; set; }

        public SystemLanguages()
        {
            this.Languages = new List<Language>();

            //fill in default languages for this template
            this.Languages.Add(new Language
                {
                    Culture = "lt-LT",
                    IsDefault = false,
                    Title = "Lietuvių",
                    Code = "lt"
                });

            this.Languages.Add(new Language
            {
                Culture = "en-US",
                IsDefault = true,
                Title = "English",
                Code = "en"
            });
        }
    }
}
