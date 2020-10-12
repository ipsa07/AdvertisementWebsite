using System;
using AdvertiseApi.Models;
using Amazon.DynamoDBv2.DataModel;

namespace AdvertiseApi.Services
{
    [DynamoDBTable("Advertisements")]
    public class AdvertiseDBModel
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public string Description { get; set; }

        [DynamoDBProperty]
        public double Price { get; set; }

        [DynamoDBProperty]
        public DateTime CreationDate { get; set; }

        [DynamoDBProperty]
        public AdvertiseStatus Status { get; set; }

        [DynamoDBProperty] public string FilePath { get; set; }

        [DynamoDBProperty] public string UserName { get; set; }

    }
}
