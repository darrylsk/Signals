using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Signals.ApplicationLayer.Abstract;
using Signals.ApplicationLayer.Services.Base;
using Signals.CoreLayer.Abstract;
using Signals.CoreLayer.Entities;

namespace Signals.ApplicationLayer.Services;

public class IndexItemService :BusinessService<IndexItem> , IIndexItemService
{
    public IIndexItemRepository Repository { get; }

    public IndexItemService(IIndexItemRepository repository) : base(repository)
    {
        Repository = repository;
    }
    // public async Task<IEnumerable<IndexItem>> GetAll()
    // {
    //     throw new NotImplementedException();
    // }

    // public async Task<IndexItem> GetById(Guid id)
    // {
    //     throw new NotImplementedException();
    // }

    // public async Task<int> Update(IndexItem model)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<IndexItem?> GetBySymbol(string symbol)
    {
        var indexItem = (await Repository.GetAsync(x => x.Symbol == symbol)).FirstOrDefault();
        return indexItem;
    }
}