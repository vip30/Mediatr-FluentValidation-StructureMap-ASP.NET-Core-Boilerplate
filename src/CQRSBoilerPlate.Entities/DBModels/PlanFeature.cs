using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSBoilerPlate.Entities.DBModels
{
    public class PlanFeature
    {
        [ForeignKey("Plan")]
        public int PlanID { get; set; }
        public Plan Plan { get; set; }

        [ForeignKey("Feature")]
        public int FeatureID { get; set; }
        public Feature Feature { get; set; }

        public string Value { get; set; }
        public string Type { get; set; }
    }
}