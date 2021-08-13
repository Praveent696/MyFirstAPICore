using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/policies")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private IPoliciesServices _policiesServices;

        public PoliciesController(IPoliciesServices policiesServices)
        {
            _policiesServices = policiesServices;
        }

        // GET: api/policies/get-all
        [HttpGet("get-all")]
        public async Task<ActionResult<HttpResponseModel>> GetInsurancePolicies()
        {
            HttpResponseModel model = new HttpResponseModel();
            model.Data = await _policiesServices.GetAllPolicies();
            model.Count = ((List<InsurancePolicy>)model.Data).Count;
            model.Success = true;
            return model;
        }

        // GET: api/policies/get/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<HttpResponseModel>> GetInsurancePolicy(int id)
        {
            HttpResponseModel model = new HttpResponseModel();
            var insurancePolicy = await _policiesServices.GetPolicy(id);
            if (insurancePolicy == null)
            {
                model.Success = false;
                model.Count = 0;
                model.Message = "Not Found";
                return NotFound(model);
            }
            model.Data = insurancePolicy;
            model.Count = 1;
            model.Success = true;
            return model;
        }

        // PUT: api/policy/update/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<ActionResult<HttpResponseModel>> PutInsurancePolicy(int id, InsurancePolicyVM insurancePolicy)
        {
            HttpResponseModel model = new HttpResponseModel();
            model = await _policiesServices.Update(id, insurancePolicy);
            if (!model.Success)
            {
                return BadRequest(model);
            }
            return Ok(model);
        }

        // POST: api/InsurancePolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add-new")]
        public async Task<ActionResult<HttpResponseModel>> PostInsurancePolicy(InsurancePolicyVM insurancePolicy)
        {
            HttpResponseModel model = new HttpResponseModel();
            model = await _policiesServices.Create(insurancePolicy);
            return Created("get-all", model);
        }

        // DELETE: api/InsurancePolicies/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<HttpResponseModel>> DeleteInsurancePolicy(int id)
        {
            HttpResponseModel model = new HttpResponseModel();
            bool status = await _policiesServices.Delete(id);
            if (!status)
            {
                model.Message = "No data found with given details.";
                model.Success = false;
                return NotFound(model);
            }

            model.Message = "Data deleted successfully.";
            model.Success = true;

            return Ok(model);
        }
    }
}
