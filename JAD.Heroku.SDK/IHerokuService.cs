using JAD.Heroku.SDK.Models.Entities;
using JAD.Heroku.SDK.Models.EntityCreateOptions;
using JAD.Heroku.SDK.Models.EntityUpdateOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK
{
    public interface IHerokuService
    {
        #region Account
        public Task<Account> GetMyAccountAsync();
        #endregion

        #region AddOn
        public Task<List<AddOn>> GetAddOnsAsync();

        public Task<AddOn> GetAddOnByIdAsync(Guid id);

        public Task<List<AddOn>> GetAddOnsByAppIdAsync(Guid appId);

        public Task<AddOn> GetAddOnbyAppIdAsync(Guid id, Guid appId);

        public Task<AddOn> CreateAddOnAsync(Guid appId, AddOnCreateOptions options);

        public Task<AddOn> UpdateAddOnAsync(Guid appId, AddOnUpdateOptions options);

        public Task<List<AddOnConfigVars>> GetAddOnConfigurationVariablesAsync(Guid addOnId);

        #endregion

        #region Apps
        public Task<List<App>> GetAppsAsync();

        public Task<App> GetAppByIdAsync(Guid id);

        public Task<App> CreateAppAsync(AppCreateOptions options);
        #endregion

        #region Config Vars
        public Task<Dictionary<string, string>> GetConfigVarsAsync(Guid appId);

        public Task<Dictionary<string, string>> UpdateConfigVarsAsync(Guid appId, Dictionary<string, string> options);
        #endregion

        #region Attachment
        public Task<AddOnAttachment> GetAddOnAttachmentByIdAsync(Guid attachmentId);

        public Task<AddOnAttachment> GetAddOnAttachmentByIdAsync(Guid appId, Guid attachmentId);

        public Task<List<AddOnAttachment>> GetAddOnAttachmentsAsync();

        public Task<List<AddOnAttachment>> GetAddOnAttachmentsByAddOnAsync(Guid addOnId);

        public Task<List<AddOnAttachment>> GetAddOnAttachmentsByAppAsync(Guid appId);

        public Task<AddOnAttachment> CreateAddOnAttachmentAsync(AddOnAttachmentCreateOptions options);

        public Task DeleteAddOnAttachmentAsync(Guid attachmentId);

        #endregion

        #region Pipeline
        public Task<List<Pipeline>> GetPipelinesAsync();

        public Task<Pipeline> GetPipelineByIdAsync(Guid id);

        public Task<Pipeline> CreatePipelineAsync(PipelineCreateOptions options);

        #endregion

        #region Pipeline Coupling
        public Task<List<PipelineCoupling>> GetPipelineCouplingsAsync();

        public Task<PipelineCoupling> GetPipelineCouplingsByPipelineIdAsync(Guid pipelineId);

        public Task<PipelineCoupling> CreatePipelineCouplingAsync(PipelineCouplingCreateOptions options);

        #endregion

        #region Source
        public Task<Source> GetSourceAsync();

        public Task UploadSourceAsync(string uploadUrl, byte[] binaryData);

        #endregion

        #region Build
        public Task<Build> GetBuildById(Guid appId, Guid buildId);

        public Task<List<Build>> GetBuilds(Guid appId);

        public Task<Build> CreateBuildAsync(Guid appId, BuildCreateOptions options);

        #endregion

        #region Dyno
        public Task<Dyno> GetDynoById(Guid appId, Guid dynoId);

        public Task<List<Dyno>> GetDynos(Guid appId);

        public Task<Dyno> CreateDynoAsync(Guid appId, DynoCreateOptions options);

        #endregion

        #region Domain
        public Task<Domain> GetDomainByIdAsync(Guid appId, Guid domainId);

        public Task<List<Domain>> GetDomainsAsync(Guid appId, Guid domainId);

        public Task<Domain> CreateDomainAsync(Guid appId, DomainCreateOptions options);

        #endregion
    }
}
