using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CQRSBoilerPlate.Entities.DBModels
{
    public class Plan
    {
        [Key]
        public int PlanID { get; set; }
        public decimal Price { get; set; }
        public byte PlanType { get; set; }
        public string PlanName { get; set; }
        public List<PlanFeature> PlanFeatures { get; set; }
    }
}