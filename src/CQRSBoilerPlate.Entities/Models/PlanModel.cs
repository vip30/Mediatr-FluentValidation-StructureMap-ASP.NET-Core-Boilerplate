using System.Collections.Generic;

namespace CQRSBoilerPlate.Entities.Models
{
    public class PlanModel
    {
        public int PlanID { get; set; }
        public decimal Price { get; set; }
        public byte PlanType { get; set; }
        public string PlanName { get; set; }
        public List<PlanFeatureModel> Features { get; set; }
    }
}