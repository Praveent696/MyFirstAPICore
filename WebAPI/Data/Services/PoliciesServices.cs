using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Models.ViewModels;
using WebAPI.Data.Services.Interfaces;

namespace WebAPI.Data.Services
{
    public class PoliciesServices : IPoliciesServices
    {
        private AppDbContext _context;
        public PoliciesServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<HttpResponseModel> Create(InsurancePolicyVM insurancePolicy)
        {
            HttpResponseModel model = new HttpResponseModel();

            InsurancePolicy insurancePolicym = new InsurancePolicy()
            {
                Id = insurancePolicy.Id,
                PolicyNumber = insurancePolicy.PolicyNumber,
                SumInsured = insurancePolicy.SumInsured,
                GrossPremium = insurancePolicy.GrossPremium,
                NetPremium = insurancePolicy.NetPremium,
                ODPremium = insurancePolicy.ODPremium,
                BusinessType = insurancePolicy.BusinessType,
                VehicleNumber = insurancePolicy.VehicleNumber,
                Make = insurancePolicy.Make,
                Model = insurancePolicy.Model,
                InsuranceCompanyId = insurancePolicy.InsuranceCompanyId,
                VehicleTypeId = insurancePolicy.VehicleTypeId,
                InsuranceTypeId = insurancePolicy.InsuranceTypeId,
                StaffUserId = insurancePolicy.StaffUserId,
                DealerUserId = insurancePolicy.DealerUserId
            };

            _context.InsurancePolicies.Add(insurancePolicym);
            await _context.SaveChangesAsync();
            model.Message = "Created Successfully";
            model.Data = await GetPolicy(insurancePolicym.Id);

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            bool status = false;
            var insurancePolicy = await _context.InsurancePolicies.FindAsync(id);
            if (insurancePolicy == null)
            {
                return status;
            }
            _context.InsurancePolicies.Remove(insurancePolicy);
            await _context.SaveChangesAsync();
            status = true;
            return status;
        }

        public async Task<List<InsurancePolicy>> GetAllPolicies()
        {
            return await _context.InsurancePolicies.Include(x=>x.StaffUser).Include(x=>x.DealerUser).Include(x=>x.InsuranceCompany).Include(x=>x.InsuranceType).Include(x=>x.VehicleType).Include(x=>x.VehicleType.Children).Include(x=>x.VehicleType.Parent).Include(x=>x.Payment).Include(x=>x.InsuranceType).ToListAsync();
        }

        public async Task<InsurancePolicy> GetPolicy(int id)
        {
            var policies = await _context.InsurancePolicies.Include(x => x.StaffUser).Include(x => x.DealerUser).Include(x => x.InsuranceCompany).Include(x => x.InsuranceType).Include(x => x.VehicleType).Include(x => x.VehicleType.Children).Include(x => x.VehicleType.Parent).Include(x => x.Payment).Include(x => x.InsuranceType).ToListAsync();
            return policies.FirstOrDefault();
        }

        public async Task<HttpResponseModel> Update(int id,InsurancePolicyVM insurancePolicy)
        {
            HttpResponseModel model = new HttpResponseModel();
            if (id != insurancePolicy.Id)
            {
                model.Success = false;
                model.Message = "Bad Request";
                return model;
            }

            InsurancePolicy insurancePolicym = await GetPolicy(id);
            insurancePolicym.Id = insurancePolicy.Id;
            insurancePolicym.PolicyNumber = insurancePolicy.PolicyNumber;
            insurancePolicym.SumInsured = insurancePolicy.SumInsured;
            insurancePolicym.GrossPremium = insurancePolicy.GrossPremium;
            insurancePolicym.NetPremium = insurancePolicy.NetPremium;
            insurancePolicym.ODPremium = insurancePolicy.ODPremium;
            insurancePolicym.BusinessType = insurancePolicy.BusinessType;
            insurancePolicym.VehicleNumber = insurancePolicy.VehicleNumber;
            insurancePolicym.Make = insurancePolicy.Make;
            insurancePolicym.Model = insurancePolicy.Model;
            insurancePolicym.InsuranceCompanyId = insurancePolicy.InsuranceCompanyId;
            insurancePolicym.VehicleTypeId = insurancePolicy.VehicleTypeId;
            insurancePolicym.InsuranceTypeId = insurancePolicy.InsuranceTypeId;
            insurancePolicym.StaffUserId = insurancePolicy.StaffUserId;
            insurancePolicym.DealerUserId = insurancePolicy.DealerUserId;

            _context.Entry(insurancePolicym).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await GetPolicy(id))!=null)
                {
                    model.Success = false;
                    model.Message = "Not Found";
                    return model;
                }
                else
                {
                    throw;
                }
            }

            model.Success = true;
            model.Message = "Sucessfully Updated";
            return model;
        }
    }
}
