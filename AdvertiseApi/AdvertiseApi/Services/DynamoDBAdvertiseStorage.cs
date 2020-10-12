using System;
using System.Threading.Tasks;
using AdvertiseApi.Models;
using AutoMapper;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace AdvertiseApi.Services
{
    public class DynamoDBAdvertiseStorage : IadvertiseStorageService
    {
         private readonly IMapper _mapper;

    public DynamoDBAdvertiseStorage(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<string> Add(AdvertiseModel model)
    {
        var dbModel = _mapper.Map<AdvertiseDBModel>(model);

        dbModel.Id = new Guid().ToString();
        dbModel.CreationDate = DateTime.UtcNow;
        dbModel.Status = AdvertiseStatus.Pending;

        using (var client = new AmazonDynamoDBClient())
        {
            using (var context = new DynamoDBContext(client))
            {
                await context.SaveAsync(dbModel);
            }
        }
        return dbModel.Id;
    }

    public async Task Confirm(ConfirmAdvertiseModel model)
    {
        using (var client = new AmazonDynamoDBClient())
        {
            using (var context = new DynamoDBContext(client))
            {
                var record = await context.LoadAsync<AdvertiseDBModel>(model.Id);
                if (record == null)
                {
                    throw new KeyNotFoundException($"A record with Id={model.Id} is not found");
                }
                if (model.Status == AdvertiseStatus.Active)
                {
                    record.Status = AdvertiseStatus.Active;
                    await context.SaveAsync(record);
                }
                else
                    await context.DeleteAsync(record);

            }
        }
    }
    public async Task<bool> CheckHealthAsync()
    {
        Console.WriteLine("Health checking...");
        using (var client = new AmazonDynamoDBClient())
        {
            var tableData = await client.DescribeTableAsync("Advertisements");
            return string.Compare(tableData.Table.TableStatus, "active", true) == 0;
        }
    }
    public async Task<List<AdvertiseModel>> GetAllAsync()
    {
        using (var client = new AmazonDynamoDBClient())
        {
            using (var context = new DynamoDBContext(client))
            {
                var scanResult =
                    await context.ScanAsync<AdvertiseDBModel>(new List<ScanCondition>()).GetNextSetAsync();
                return scanResult.Select(item => _mapper.Map<AdvertiseModel>(item)).ToList();
            }
        }
    }

    public async Task<AdvertiseModel> GetByIdAsync(string id)
    {
        using (var client = new AmazonDynamoDBClient())
        {
            using (var context = new DynamoDBContext(client))
            {
                var dbModel = await context.LoadAsync<AdvertiseDBModel>(id);
                if (dbModel != null) return _mapper.Map<AdvertiseModel>(dbModel);
            }
        }

        throw new KeyNotFoundException();
    }
}
}
