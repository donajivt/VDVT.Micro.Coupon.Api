using AutoMapper;
using VDVT.Micro.Coupon.Api.Models.Dto;
using VDVT.Micro.Coupon.Api.Models;

namespace VDVT.Micro.Coupon.Api
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Models.Coupon>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
