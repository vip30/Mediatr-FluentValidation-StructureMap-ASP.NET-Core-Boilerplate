using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSBoilerPlate.Entities.DBModels
{
    public class PlanFeature
    {
       
        public int PlanID { get; set; }
        [ForeignKey("PlanID")]
        public Plan Plan { get; set; }

        public int FeatureID { get; set; }
        [ForeignKey("FeatureID")]
        public Feature Feature { get; set; }

        public string Value { get; set; }
        public string Type { get; set; }
    }
}
