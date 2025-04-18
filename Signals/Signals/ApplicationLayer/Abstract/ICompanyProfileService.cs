﻿using System.Threading.Tasks;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Abstract;

public interface ICompanyProfileService : IBusinessService<CompanyProfile>
{
    Task<CompanyProfile?> GetBySymbol(string symbol);
}