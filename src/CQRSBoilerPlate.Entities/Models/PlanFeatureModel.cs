using System.Collections.Generic;

namespace CQRSBoilerPlate.Entities.Models
{
    public class PlanFeatureModel
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}