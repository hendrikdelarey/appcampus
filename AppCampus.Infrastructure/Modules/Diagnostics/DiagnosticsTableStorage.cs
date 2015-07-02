using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCampus.Infrastructure.Modules.Diagnostics
{
    public class DiagnosticsTableStorage
    {
        private CloudTable table;

        public DiagnosticsTableStorage(string connectionString, string tableName)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();
            tableClient.DefaultRequestOptions.PayloadFormat = TablePayloadFormat.JsonNoMetadata;
            tableClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry();

            table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
        }

        public void Write(DiagnosticsTableEntity tableEntity)
        {
            TableOperation insertOperation = TableOperation.InsertOrReplace(tableEntity);

            table.Execute(insertOperation);
        }

        public DiagnosticsTableEntity ReadLatest(Guid signboardId)
        {
            return table
                .CreateQuery<DiagnosticsTableEntity>()
                .Where(x => x.PartitionKey == signboardId.ToString())
                .FirstOrDefault();
        }

        public IList<DiagnosticsTableEntity> ReadFrom(Guid signboardId, DateTime date, int take)
        {
            var rowKey = String.Format("{0:D19}", (DateTime.MaxValue - date).Ticks);

            return table
                .CreateQuery<DiagnosticsTableEntity>()
                .Where(x => x.PartitionKey == signboardId.ToString() && x.RowKey.CompareTo(rowKey) >= 0)
                .Take(take)
                .ToList();
        }
    }
}