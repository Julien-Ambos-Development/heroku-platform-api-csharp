using JAD.Heroku.SDK.Extensions;
using JAD.Heroku.SDK.Models;
using JAD.Heroku.SDK.Models.Entities;
using JAD.Heroku.SDK.Models.EntityCreateOptions;
using JAD.Heroku.SDK.Models.EntityUpdateOptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK
{
    public class HerokuService : IHerokuService
    {
        private readonly HttpClient client;
        private readonly ILogger<HerokuService> logger;

        public HerokuService(HttpClient client, ILogger<HerokuService> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        #region Account
        public async Task<Account> GetMyAccountAsync()
        {
            logger.LogInformation("--- Fetching heroku account ---");

            var response = await client.GetFromJsonAsync<Account>(EntityNames.Account);

            logger.LogInformation($"--- Returning heroku account of: {response.Name} ---");

            return response;
        }
        #endregion

        #region AddOn
        public async Task<List<AddOn>> GetAddOnsAsync()
        {
            logger.LogInformation("--- Fetching all heroku add-ons ---");

            var response = await client.GetFromJsonAsync<List<AddOn>>(EntityNames.AddOn);

            logger.LogInformation($"--- Returning {response.Count} heroku add-ons ---");

            return response;
        }

        public async Task<AddOn> GetAddOnByIdAsync(Guid id)
        {
            logger.LogInformation($"--- Fetching heroku add-ons by id: {id} ---");

            var response = await client.GetFromJsonAsync<AddOn>($"{EntityNames.AddOn}/{id}");

            logger.LogInformation($"--- Returning heroku add-on with the name: {response.Name} ---");

            return response;
        }

        public async Task<List<AddOn>> GetAddOnsByAppIdAsync(Guid appId)
        {
            logger.LogInformation($"--- Fetching all heroku add-ons by appId: {appId} ---");

            var response = await client.GetFromJsonAsync<List<AddOn>>($"{EntityNames.App}/{appId}/{EntityNames.AddOn}");

            logger.LogInformation($"--- Returning {response.Count} heroku add-ons ---");

            return response;
        }

        public async Task<AddOn> GetAddOnbyAppIdAsync(Guid id, Guid appId)
        {
            logger.LogInformation($"--- Fetching heroku add-on with id: {id} for appId: {appId} ---");

            var response = await client.GetFromJsonAsync<AddOn>($"{EntityNames.App}/{appId}/{EntityNames.AddOn}/{id}");

            logger.LogInformation($"--- Returning heroku add-on with the name: {response.Name} ---");

            return response;
        }

        public async Task<AddOn> CreateAddOnAsync(Guid appId, AddOnCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku add-on for appId: {appId} ---");

            var serializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"{EntityNames.App}/{appId}/{EntityNames.AddOn}",
                options,
                serializerOptions
            );

            var model = await response.Content.ReadFromJsonAsync<AddOn>(); 

            logger.LogInformation($"--- Successfully added heroku add-on with id: {model.Id} for appId: {appId} ---");

            return model;
        }

        public async Task<AddOn> UpdateAddOnAsync(Guid appId, AddOnUpdateOptions options)
        {
            logger.LogInformation($"--- Creating heroku add-on for appId: {appId} ---");

            HttpResponseMessage response = await client.PatchAsync(
                $"{EntityNames.App}/{appId}/{EntityNames.AddOn}",
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<AddOn>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully updated heroku add-on with id: {model.Id} for appId: {appId}---");

            return model;
        }
        #endregion

        #region AddOn ConfigVars
        public async Task<List<AddOnConfigVars>> GetAddOnConfigurationVariablesAsync(Guid addOnId)
        {
            logger.LogInformation($"--- Fetching heroku add-on config vars by addOnId: {addOnId} ---");

            var response = await client.GetFromJsonAsync<List<AddOnConfigVars>>($"{EntityNames.AddOn}/{addOnId}/config");

            logger.LogInformation($"--- Returning heroku add-on config vars ---");

            return response;
        }

        #endregion

        #region AddOn Attachment

        public async Task<List<AddOnAttachment>> GetAddOnAttachmentsAsync()
        {
            logger.LogInformation($"--- Fetching heroku add-on attachments ---");

            var response = await client.GetFromJsonAsync<List<AddOnAttachment>>($"{EntityNames.Attachment}");

            logger.LogInformation($"--- Returning {response.Count} heroku add-on attachments ---");

            return response;
        }

        public async Task<List<AddOnAttachment>> GetAddOnAttachmentsByAppAsync(Guid appId)
        {
            logger.LogInformation($"--- Fetching heroku add-on attachments by appId: {appId} ---");

            var response = await client.GetFromJsonAsync<List<AddOnAttachment>>($"{EntityNames.App}/{appId}/{EntityNames.Attachment}");

            logger.LogInformation($"--- Returning {response.Count} heroku add-on attachments ---");

            return response;
        }        
        
        public async Task<List<AddOnAttachment>> GetAddOnAttachmentsByAddOnAsync(Guid addOnId)
        {
            logger.LogInformation($"--- Fetching heroku add-on attachments by addOnId: {addOnId} ---");

            var response = await client.GetFromJsonAsync<List<AddOnAttachment>>($"{EntityNames.AddOn}/{addOnId}/{EntityNames.Attachment}");

            logger.LogInformation($"--- Returning {response.Count} heroku add-on attachments ---");

            return response;
        }

        public async Task<AddOnAttachment> GetAddOnAttachmentByIdAsync(Guid attachmentId)
        {
            logger.LogInformation($"--- Fetching heroku add-on attachment by attachmentId: {attachmentId} ---");

            var response = await client.GetFromJsonAsync<AddOnAttachment>($"{EntityNames.Attachment}/{attachmentId}");

            logger.LogInformation($"--- Returning heroku add-on attachment with the id: {response.Id} ---");

            return response;
        }

        public async Task<AddOnAttachment> GetAddOnAttachmentByIdAsync(Guid addOnId, Guid attachmentId)
        {
            logger.LogInformation($"--- Fetching heroku add-on attachment by attachmentId: {attachmentId} for addOnId: {addOnId} ---");

            var response = await client.GetFromJsonAsync<AddOnAttachment>($"{EntityNames.AddOn}/{addOnId}/{EntityNames.Attachment}/{attachmentId}");

            logger.LogInformation($"--- Returning heroku add-on attachment with the id: {response.Id} ---");

            return response;
        }

        public async Task<AddOnAttachment> CreateAddOnAttachmentAsync(AddOnAttachmentCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku add on attachment ---");

            var response = await client.PostAsync(
                EntityNames.Attachment,
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<AddOnAttachment>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully created heroku add on attachment with the id: {model.Id} ---");

            return model;
        }

        public async Task DeleteAddOnAttachmentAsync(Guid attachmentId)
        {
            logger.LogInformation($"--- Deleting heroku add-on attachment by attachmentId: {attachmentId} ---");

            await client.DeleteAsync($"{EntityNames.Attachment}/{attachmentId}");

            logger.LogInformation($"--- Successfully deleted heroku add-on attachment ---");
        }

        #endregion

        #region Apps
        public async Task<List<App>> GetAppsAsync()
        {
            logger.LogInformation("--- Fetching all heroku apps ---");

            var response = await client.GetFromJsonAsync<List<App>>(EntityNames.App);

            logger.LogInformation($"--- Returning {response.Count} heroku apps ---");

            return response;
        }

        public async Task<App> GetAppByIdAsync(Guid id)
        {
            logger.LogInformation($"--- Fetching heroku app by id: {id} ---");

            var response = await client.GetFromJsonAsync<App>($"{EntityNames.App}/{id}");

            logger.LogInformation($"--- Returning heroku app with the name: {response.Name} ---");

            return response;
        }

        public async Task<App> CreateAppAsync(AppCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku app ---");

            var response = await client.PostAsync(
                EntityNames.App,
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<App>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully created heroku app with the id: {model.Id} ---");

            return model;
        }
        #endregion

        #region Config Vars
        public async Task<Dictionary<string, string>> GetConfigVarsAsync(Guid appId)
        {
            logger.LogInformation($"--- Fetching all config variables for the heroku app by id: {appId} ---");

            var response = await client.GetFromJsonAsync<Dictionary<string, string>>($"{EntityNames.App}/{appId}/{EntityNames.ConfigVars}");

            logger.LogInformation($"--- Returning {response.Count} configuration variables ---");

            return response;
        }

        public async Task<Dictionary<string, string>> UpdateConfigVarsAsync(Guid appId, Dictionary<string, string> options)
        {
            logger.LogInformation($"--- Updating config variables for appId: {appId} ---");

            HttpResponseMessage response = await client.PatchAsync(
                $"{EntityNames.App}/{appId}/{EntityNames.ConfigVars}",
                HerokuExtensions.CreateStringContent(options)
            );

            logger.LogInformation($"--- Successfully updated config variables ---");

            return JsonSerializer.Deserialize<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
        }
        #endregion

        #region Pipeline
        public async Task<List<Pipeline>> GetPipelinesAsync()
        {
            logger.LogInformation("--- Fetching all heroku pipelines ---");

            var response = await client.GetFromJsonAsync<List<Pipeline>>(EntityNames.Pipeline);

            logger.LogInformation($"--- Returning {response.Count} heroku pipelines ---");

            return response;
        }

        public async Task<Pipeline> GetPipelineByIdAsync(Guid id)
        {
            logger.LogInformation($"--- Fetching heroku pipeline by id: {id} ---");

            var response = await client.GetFromJsonAsync<Pipeline>($"{EntityNames.Pipeline}/{id}");

            logger.LogInformation($"--- Returning heroku pipeline with the name: {response.Name} ---");

            return response;
        }

        public async Task<Pipeline> CreatePipelineAsync(PipelineCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku pipeline ---");

            HttpResponseMessage response = await client.PostAsync(
                EntityNames.Pipeline,
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<Pipeline>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully created heroku pipeline with the id: {model.Id} ---");

            return model;
        }
        #endregion

        #region Pipeline Coupling
        public async Task<List<PipelineCoupling>> GetPipelineCouplingsAsync()
        {
            logger.LogInformation($"--- Fetching heroku pipeline coupling ---");

            var response = await client.GetFromJsonAsync<List<PipelineCoupling>>(EntityNames.PipelineCoupling);

            logger.LogInformation($"--- Returning {response.Count} heroku pipeline couplings ---");

            return response;
        }

        public async Task<PipelineCoupling> GetPipelineCouplingsByPipelineIdAsync(Guid pipelineId)
        {
            logger.LogInformation($"--- Fetching heroku pipeline coupling by pipelineId: {pipelineId} ---");

            var response = await client.GetFromJsonAsync<PipelineCoupling>($"{EntityNames.Pipeline}/{pipelineId}/{EntityNames.PipelineCoupling}");

            logger.LogInformation($"--- Returning heroku pipeline coupling with id: {response.Id} ---");

            return response;
        }

        public async Task<PipelineCoupling> CreatePipelineCouplingAsync(PipelineCouplingCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku pipeline coupling ---");

            HttpResponseMessage response = await client.PostAsync(
                EntityNames.PipelineCoupling,
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<PipelineCoupling>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully created heroku pipeline coupling with the id: {model.Id} ---");

            return model;
        }
        #endregion

        #region Source
        public async Task<Source> GetSourceAsync()
        {
            logger.LogInformation($"--- Fetching heroku source links ---");

            HttpResponseMessage response = await client.PostAsync(
                EntityNames.Source,
                null
            );

            var model = JsonSerializer.Deserialize<Source>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Returning heroku source links ---");

            return model;
        }

        public async Task UploadSourceAsync(string uploadUrl, byte[] binaryData)
        {
            logger.LogInformation($"--- Uploading binary-data to uploadlink ---");

            var uploadClient = new HttpClient();
            var uri = new Uri(uploadUrl);

            var byteArrayContent = new ByteArrayContent(binaryData);

            var result = await uploadClient.PutAsync(
                uri,
                byteArrayContent
            );

            logger.LogInformation($"--- Successfully uploaded binary-data ---");
        }
        #endregion

        #region Build
        public async Task<Build> GetBuildById(Guid appId, Guid buildId)
        {
            logger.LogInformation($"--- Fetching heroku build by id: {buildId} ---");

            var response = await client.GetFromJsonAsync<Build>($"{EntityNames.App}/{appId}/{EntityNames.Build}/{buildId}");

            logger.LogInformation($"--- Returning heroku build with id: {response.Id}");

            return response;
        }

        public async Task<List<Build>> GetBuilds(Guid appId)
        {
            logger.LogInformation($"--- Fetching heroku builds ---");

            var response = await client.GetFromJsonAsync<List<Build>>($"{EntityNames.App}/{appId}/{EntityNames.Build}");

            logger.LogInformation($"--- Returning {response.Count} heroku builds ---");

            return response;
        }

        public async Task<Build> CreateBuildAsync(Guid appId, BuildCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku build for appId: {appId} ---");

            HttpResponseMessage response = await client.PostAsync(
                $"{EntityNames.App}/{appId}/{EntityNames.Build}",
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<Build>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully deployed heroku build ---");

            return model;
        }
        #endregion

        #region Dyno
        public async Task<Dyno> GetDynoById(Guid appId, Guid dynoId)
        {
            logger.LogInformation($"--- Fetching heroku dyno by id: {dynoId} ---");

            var response = await client.GetFromJsonAsync<Dyno>($"{EntityNames.App}/{appId}/{EntityNames.Dyno}/{dynoId}");

            logger.LogInformation($"--- Returning heroku dyno with id: {response.Id} ---");

            return response;
        }

        public async Task<List<Dyno>> GetDynos(Guid appId)
        {
            logger.LogInformation($"--- Fetching heroku dynos ---");

            var response = await client.GetFromJsonAsync<List<Dyno>>($"{EntityNames.App}/{appId}/{EntityNames.Dyno}");

            logger.LogInformation($"--- Returning {response.Count} heroku dynos ---");

            return response;
        }

        public async Task<Dyno> CreateDynoAsync(Guid appId, DynoCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku dyno for appId: {appId} ---");

            HttpResponseMessage response = await client.PostAsync(
                $"{EntityNames.App}/{appId}/{EntityNames.Dyno}",
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<Dyno>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully created heroku dyno with id {model.Id} ---");

            return model;
        }

        #endregion

        #region Domain
        public async Task<Domain> GetDomainByIdAsync(Guid appId, Guid domainId)
        {
            logger.LogInformation($"--- Fetching heroku domain by id: {domainId} ---");

            var response = await client.GetFromJsonAsync<Domain>($"{EntityNames.App}/{appId}/{EntityNames.Domain}/{domainId}");

            logger.LogInformation($"--- Returning heroku domain with id {response.Id} ---");

            return response;
        }

        public async Task<List<Domain>> GetDomainsAsync(Guid appId, Guid domainId)
        {
            logger.LogInformation($"--- Fetching heroku domains for appId: {appId} ---");

            var response = await client.GetFromJsonAsync<List<Domain>>($"{EntityNames.App}/{appId}/{EntityNames.Domain}");

            logger.LogInformation($"--- Returning {response.Count} heroku domains ---");

            return response;
        }

        public async Task<Domain> CreateDomainAsync(Guid appId, DomainCreateOptions options)
        {
            logger.LogInformation($"--- Creating heroku domain for appId: {appId} ---");

            HttpResponseMessage response = await client.PostAsync(
                $"{EntityNames.App}/{appId}/{EntityNames.Domain}",
                HerokuExtensions.CreateCamelCaseStringContent(options)
            );

            var model = JsonSerializer.Deserialize<Domain>(await response.Content.ReadAsStringAsync());

            logger.LogInformation($"--- Successfully added heroku domain with id: {model.Id} for appId: {appId}---");

            return model;
        }

        #endregion
    }
}
