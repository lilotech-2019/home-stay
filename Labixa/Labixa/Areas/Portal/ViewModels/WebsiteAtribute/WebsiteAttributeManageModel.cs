using System.Collections.Generic;

namespace Labixa.Areas.Portal.ViewModels.WebsiteAtribute
{
    public class WebsiteAttributeManageModel
    {

        public string Type { get; set; }
        public List<Outsourcing.Data.Models.WebsiteAtribute> WebsiteAttributes { get; set; }
    }
}