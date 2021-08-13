using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;

namespace WebAPI.Data.Services.Interfaces
{
    public interface IPoliciesServices
    {
        public Task<List<InsurancePolicy>> GetAllPolicies();
        public Task<InsurancePolicy> GetPolicy(int id);
        public Task<HttpResponseModel> Create(InsurancePolicyVM insurancePolicy);
        public Task<HttpResponseModel> Update(int id, InsurancePolicyVM insurancePolicy);
        public Task<bool> Delete(int id);
    }
}
