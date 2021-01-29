using dnsimple;
using dnsimple.Services;
using JAD.Heroku.SDK.Models.Entities;
using JAD.Heroku.SDK.Models.EntityCreateOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Test.Controllers
{
    [ApiController]
    [Route("controller")]
    public class HerokuServiceController : ControllerBase
    {
        private readonly HerokuService client;

        public HerokuServiceController(HerokuService client)
        {
            this.client = client;
        }

        [HttpPost("apps/{appId}/addOns")]
        public async Task<List<AddOnConfigVars>> CreateAddon(Guid appId, [FromBody] AddOnCreateOptions createOptions)
        {
            var addOn = await client.CreateAddOnAsync(appId, createOptions);

            var configVars = await client.GetAddOnConfigurationVariablesAsync(addOn.Id);

            return configVars;
        }

        [HttpGet("apps/{appId}/addOns")]
        public async Task<List<AddOnConfigVars>> GetAddons(Guid appId)
        {
            var addOn = await client.GetAddOnsByAppIdAsync(appId);

            var configVars = await client.GetAddOnConfigurationVariablesAsync(addOn[0].Id);

            return configVars;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            // Create Postgres DB addon
            var addOntest = await client.CreateAddOnAsync(new Guid("appId"), new AddOnCreateOptions
            {
                plan = "planId",
            });

            var attachment = await client.CreateAddOnAttachmentAsync(new AddOnAttachmentCreateOptions() 
            { 
                Addon = addOntest.Id.ToString(),
                App = "appId"
            });

            // GET Heroku Account Info
            var account = await client.GetMyAccountAsync();

            // Initialize Github Client
            var githubClient = new GitHubClient(new ProductHeaderValue("the-serious-test-try"));

            // SET Github Credentials
            githubClient.Credentials = new Credentials("<GithubToken>");

            // GET DNSimple Credentials
            var dnsimpleClient = new dnsimple.Client();
            var credentials = new OAuth2Credentials("<DnsimpleToken>");
            dnsimpleClient.AddCredentials(credentials);

            var dnsimpleAccounts = dnsimpleClient.Accounts.List();
            var dnsimpleAccount = dnsimpleAccounts.Data[0];
            var dnsimpleZones = dnsimpleClient.Zones.ListZones(dnsimpleAccount.Id);
            var dnsimpleZone = dnsimpleZones.Data[0];

            // SET Tenant names
            var tenantNames = new List<string>() { "loreal" , "volkswagen" };


            foreach (var tenantName in tenantNames)
            {
                #region Tenant 

                // GET Latest Tarball from Repo
                var apiArchive = await githubClient.Repository.Content.GetArchive("JA-AmbosImmo", "Hypecast.API", ArchiveFormat.Tarball);
                var apiReleases = await githubClient.Repository.Release.GetAll("JA-AmbosImmo", "Hypecast.API");
                var apiRelease = apiReleases.First();

                // GET Latest Tarball from Repo
                var webAppArchive = await githubClient.Repository.Content.GetArchive("JA-AmbosImmo", "Hypecast.WebApp", ArchiveFormat.Tarball);
                var webAppReleases = await githubClient.Repository.Release.GetAll("JA-AmbosImmo", "Hypecast.WebApp");
                var webAppRelease = webAppReleases.First();

                // GET Latest Tarball from Repo
                var cmsArchive = await githubClient.Repository.Content.GetArchive("JA-AmbosImmo", "Hypecast.Admin.WebApp", ArchiveFormat.Tarball);
                var cmsReleases = await githubClient.Repository.Release.GetAll("JA-AmbosImmo", "Hypecast.Admin.WebApp");
                var cmsRelease = cmsReleases.First();

                // CREATE Pipeline
                var pipeline = await client.CreatePipelineAsync(new PipelineCreateOptions
                {
                    Name = $"{tenantName}-hype-pipeline",
                    Owner = new Owner
                    {
                        Id = account.Id,
                        Type = OwnerType.User
                    }
                });

                #region API

                // GET Heroku UploadLinks
                var apiSource = await client.GetSourceAsync();

                // UPLOAD tarball to urlLink
                await client.UploadSourceAsync(apiSource.SourceBlob.PutUrl, apiArchive);

                // CREATE API
                var api = await client.CreateAppAsync(new AppCreateOptions
                {
                    Name = $"api-{tenantName}",
                    Region = RegionFormat.Europe,
                    Stack = StackFormat.Heroku20,
                });

                // Create Postgres DB addon
                var addOn = await client.CreateAddOnAsync(api.Id, new AddOnCreateOptions
                {
                    plan = "062a1cc7-f79f-404c-9f91-135f70175577",
                    Attachment = new AddOnCreateAttachment
                    {
                        Name = "DATABASE_URL"
                    }
                });

                // CREATE Pipeline Coupling
                var apiPipelineCoupling = await client.CreatePipelineCouplingAsync(new PipelineCouplingCreateOptions
                {
                    AppId = api.Id,
                    PipelineId = pipeline.Id,
                    Stage = "production"
                });

                // CREATE Dynos
                var apiDyno = await client.CreateDynoAsync(api.Id, new DynoCreateOptions
                {
                    Command = "cd $HOME/heroku_output",
                    Attach = false,
                    Environment = new Dictionary<string, string>() { { "COLUMNS", "80" }, { "LINES", "24" } },
                    Size = "Free",
                    TimeToLive = 1800,
                    Type = "web"
                });

                // CREATE BUild and Deploy in Production
                var apiBuild = await client.CreateBuildAsync(api.Id, new BuildCreateOptions
                {
                    SourceBlob = new SourceBlob
                    {
                        Url = apiSource.SourceBlob.GetUrl,
                        Version = apiRelease.TagName
                    },
                    BuildPacks = new List<BuildPack>() {
                        new BuildPack {
                            Url = "https://github.com/jincod/dotnetcore-buildpack.git"
                        }
                    }
                });

                // CREATE Subdomain in Heroku
                var apiDomain = await client.CreateDomainAsync(api.Id, new DomainCreateOptions
                {
                    Hostname = $"api.{tenantName}.{dnsimpleZone.Name}"
                });

                // CREATE Subdomain
                var apiDnsimpleDomain = dnsimpleClient.Zones.CreateZoneRecord(dnsimpleAccount.Id, dnsimpleZone.Name, new ZoneRecord
                {
                    Name = $"api.{tenantName}",
                    Type = ZoneRecordType.ALIAS,
                    Content = apiDomain.Cname,
                    Ttl = 3600
                });

                #endregion

                #region WebApp

                // GET Heroku UploadLinks
                var webAppSource = await client.GetSourceAsync();

                // UPLOAD tarball to urlLink
                await client.UploadSourceAsync(webAppSource.SourceBlob.PutUrl, webAppArchive);

                // CREATE API
                var webApp = await client.CreateAppAsync(new AppCreateOptions
                {
                    Name = $"webapp-{tenantName}",
                    Region = RegionFormat.Europe,
                    Stack = StackFormat.Heroku20
                });

                // CREATE Pipeline Coupling
                var pipelineCouplingWebapp = await client.CreatePipelineCouplingAsync(new PipelineCouplingCreateOptions
                {
                    AppId = webApp.Id,
                    PipelineId = pipeline.Id,
                    Stage = "production"
                });

                // CREATE Dynos
                var webappDyno = await client.CreateDynoAsync(webApp.Id, new DynoCreateOptions
                {
                    Command = "cd $HOME/heroku_output",
                    Attach = false,
                    Environment = new Dictionary<string, string>() { { "COLUMNS", "80" }, { "LINES", "24" } },
                    Size = "Free",
                    TimeToLive = 1800,
                    Type = "web"
                });

                // CREATE BUild and Deploy in Production
                var webAppBuild = await client.CreateBuildAsync(webApp.Id, new BuildCreateOptions
                {
                    SourceBlob = new SourceBlob
                    {
                        Url = webAppSource.SourceBlob.GetUrl,
                        Version = webAppRelease.TagName
                    },
                    BuildPacks = new List<BuildPack>() {
                        new BuildPack {
                            Url = "https://github.com/heroku/heroku-buildpack-nodejs"
                        }
                    }
                });

                // CREATE Subdomain in Heroku
                var webAppDomain = await client.CreateDomainAsync(webApp.Id, new DomainCreateOptions 
                {
                    Hostname = $"{tenantName}.{dnsimpleZone.Name}"
                });

                // CREATE Subdomain
                var webAppDnsimpleDomain = dnsimpleClient.Zones.CreateZoneRecord(dnsimpleAccount.Id, dnsimpleZone.Name, new ZoneRecord
                {
                    Name = $"{tenantName}",
                    Type = ZoneRecordType.ALIAS,
                    Content = webAppDomain.Cname,
                    Ttl = 3600
                });

                #endregion

                #region CMS

                // GET Heroku UploadLinks
                var cmsSource = await client.GetSourceAsync();

                // UPLOAD tarball to urlLink
                await client.UploadSourceAsync(cmsSource.SourceBlob.PutUrl, cmsArchive);

                // CREATE API
                var cms = await client.CreateAppAsync(new AppCreateOptions
                {
                    Name = $"cms-{tenantName}",
                    Region = RegionFormat.Europe,
                    Stack = StackFormat.Heroku20
                });

                // CREATE Pipeline Coupling
                var cmspipelineCoupling = await client.CreatePipelineCouplingAsync(new PipelineCouplingCreateOptions
                {
                    AppId = cms.Id,
                    PipelineId = pipeline.Id,
                    Stage = "production"
                });

                // CREATE Dynos
                var cmsDyno = await client.CreateDynoAsync(cms.Id, new DynoCreateOptions
                {
                    Command = "cd $HOME/heroku_output",
                    Attach = false,
                    Environment = new Dictionary<string, string>() { { "COLUMNS", "80" }, { "LINES", "24" } },
                    Size = "Free",
                    TimeToLive = 1800,
                    Type = "web"
                });

                // CREATE BUild and Deploy in Production
                var cmsBuild = await client.CreateBuildAsync(cms.Id, new BuildCreateOptions
                {
                    SourceBlob = new SourceBlob
                    {
                        Url = cmsSource.SourceBlob.GetUrl,
                        Version = cmsRelease.TagName
                    },
                    BuildPacks = new List<BuildPack>() {
                        new BuildPack {
                            Url = "https://github.com/heroku/heroku-buildpack-nodejs"
                        }
                    }
                });

                // CREATE Subdomain in Heroku
                var cmsDomain = await client.CreateDomainAsync(cms.Id, new DomainCreateOptions
                {
                    Hostname = $"admin.{tenantName}.{dnsimpleZone.Name}"
                });

                // CREATE Subdomain
                var cmsDnsimpleDomain = dnsimpleClient.Zones.CreateZoneRecord(dnsimpleAccount.Id, dnsimpleZone.Name, new ZoneRecord
                {
                    Name = $"admin.{tenantName}",
                    Type = ZoneRecordType.ALIAS,
                    Content = cmsDomain.Cname,
                    Ttl = 3600
                });
                #endregion

                #endregion
            }


            return "YEAH!!!!!!";
        }
    }
}
