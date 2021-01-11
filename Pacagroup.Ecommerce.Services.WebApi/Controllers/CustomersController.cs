using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers
{
    /// <summary>
    /// CustomersController.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomersApplication _customersApplication;
        #region Ctor
        /// <summary>
        /// Ctor of CustomersController.
        /// </summary>
        /// <param name="customersApplication"></param>
        public CustomersController(ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }
        #endregion
        #region Sync Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody]CustomersDTO customer)
        {
            if (customer == null) return BadRequest();
            var response = _customersApplication.Insert(customer);
            if(response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public IActionResult Update([FromBody] CustomersDTO customer)
        {
            if (customer == null) return BadRequest();
            var response = _customersApplication.Update(customer);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (customerId == "") return BadRequest();
            var response = _customersApplication.Delete(customerId);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("Get/{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (customerId == "") return BadRequest();
            var response = _customersApplication.Get(customerId);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = _customersApplication.GetAll();
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        #endregion
        #region Async Methods
        /// <summary>
        /// InsertAsync.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomersDTO customer)
        {
            if (customer == null) return BadRequest();
            var response = await _customersApplication.InsertAsync(customer);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// UpdateAsync.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomersDTO customer)
        {
            if (customer == null) return BadRequest();
            var response = await _customersApplication.UpdateAsync(customer);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// DeleteAsync.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAsync/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (customerId == "") return BadRequest();
            var response = await _customersApplication.DeleteAsync(customerId);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("GetAsync/{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (customerId == "") return BadRequest();
            var response = await _customersApplication.GetAsync(customerId);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// GetAllAsync.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customersApplication.GetAllAsync();
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response.Message);
        }
        #endregion
    }
}
