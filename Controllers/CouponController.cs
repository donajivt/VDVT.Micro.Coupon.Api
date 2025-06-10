using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDVT.Micro.Coupon.Api.Data;
using VDVT.Micro.Coupon.Api.Models.Dto;

namespace VDVT.Micro.Coupon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Models.Coupon> objList = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Models.Coupon obj = _dbContext.Coupons.First(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                Models.Coupon coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponCode.ToLower() == code.ToLower());
                if (coupon is null)
                {
                    _response.IsSuccess = false;
                }
                //establecemos el mapeo de la entidad a retornar como resultado de la petición
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR, VENTAS")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Models.Coupon obj = _mapper.Map<Models.Coupon>(couponDto);
                _dbContext.Coupons.Add(obj);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Models.Coupon obj = _dbContext.Coupons.First(u => u.CouponId == id);
                _dbContext.Coupons.Remove(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Models.Coupon obj = _mapper.Map<Models.Coupon>(couponDto);
                _dbContext.Coupons.Update(obj);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
