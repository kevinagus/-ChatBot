using System;
using System.Collections.Generic;

namespace ChatBot.DTOs
{
    public class OpeningHoursDto
    {
        public IList<Tuple<string, string>> RegularOpening { get; set; }

        public IList<Tuple<string, string>> SpecialOpenings { get; set; }
    }
}
