﻿using System.Threading.Tasks;
using Coderr.Server.Api.Core.ApiKeys.Commands;
using DotNetCqs;
using Coderr.Server.ReportAnalyzer.Abstractions;
using Griffin.Data;

namespace Coderr.Server.PostgreSQL.Core.ApiKeys.Commands
{
    public class EditApiKeyHandler : IMessageHandler<EditApiKey>
    {
        private readonly IAdoNetUnitOfWork _unitOfWork;
        private readonly IMessageBus _messageBus;

        public EditApiKeyHandler(IAdoNetUnitOfWork unitOfWork, IMessageBus messageBus)
        {
            _unitOfWork = unitOfWork;
            _messageBus = messageBus;
        }

        public async Task HandleAsync(IMessageContext context, EditApiKey command)
        {
            using (var cmd = _unitOfWork.CreateDbCommand())
            {
                cmd.CommandText =
                    "UPDATE ApiKeys SET ApplicationName=@appName WHERE Id = @id;";
                cmd.AddParameter("appName", command.ApplicationName);
                cmd.AddParameter("id", command.Id);
                await cmd.ExecuteNonQueryAsync();
            }


            using (var cmd = _unitOfWork.CreateDbCommand())
            {
                cmd.CommandText = "DELETE FROM ApiKeyApplications WHERE ApiKeyId = @id;";
                cmd.AddParameter("id", command.Id);
                await cmd.ExecuteNonQueryAsync();
            }

            foreach (var applicationId in command.ApplicationIds)
            {
                using (var cmd = _unitOfWork.CreateDbCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO ApiKeyApplications (ApiKeyId, ApplicationId) VALUES(@key, @app);";
                    cmd.AddParameter("app", applicationId);
                    cmd.AddParameter("key", command.Id);
                    await cmd.ExecuteNonQueryAsync();
                }
            } 
        }
    }
}