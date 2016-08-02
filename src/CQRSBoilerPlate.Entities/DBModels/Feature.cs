using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CQRSBoilerPlate.Entities.DBModels
{
    public class Feature
    {
        [Key]
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }

        public List<PlanFeature> PlanFeatures { get; set; }
    }
}