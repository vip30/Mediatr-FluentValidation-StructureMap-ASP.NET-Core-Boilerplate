using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using CQRSBoilerPlate.Entities.DBModels;
using CQRSBoilerPlate.Entities.Models;

namespace YourNamespace
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<Plan, PlanModel>();
            CreateMap<PlanFeature, PlanFeatureModel>();
            CreateMap<PlanFeature, PlanFeatureModel>()
                .ForMember<string>(c => c.FeatureName, d => d.MapFrom(e => e.Feature.FeatureName));
            CreateMap<Plan, PlanModel>()
                .ForMember<List<PlanFeatureModel>>(c => c.Features, b => b.MapFrom(d => d.PlanFeatures));
        }
    }
}