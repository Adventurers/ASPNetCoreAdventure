using System.Collections.Generic;

namespace ASPNetCore.Localization.Models
{
    public class Culture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Resource> Resources { get; set; }
    }
}
