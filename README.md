# heroku-platform-api-csharp

[![NuGet](https://img.shields.io/nuget/v/JAD.Heroku.SDK.svg)](https://www.nuget.org/packages/JAD.Heroku.SDK/)

The Unofficial [Heroku Platform API][herokuAPI] .NET Standard 5.0+ &amp; .NET Core 5.0+ async-based Library. 


## Installation

Using the [NuGet Command Line Interface (CLI)][nuget-cli]:

```sh
nuget install JAD.Heroku.SDK
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package JAD.Heroku.SDK
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on *Manage NuGet Packages...*
4. Click on the *Browse* tab and search for "JAD.Heroku.SDK".
5. Click on the JAD.Heroku.SDK package, select the appropriate version in the
   right-tab and click *Install*.

## Usage
First it is required to get a Heroku API token via the Heroku CLI. Please head over to the provided documentation to learn more about the setup and usage of the [Heroku CLI][heroku-cli].

For more information about the REST API implementation please find Herokus [Getting Started][getting-started] documentation.

To generate a token you need to excecute following command:

```
heroku authorizations:create
```
You will receive the following response (sample):

```
ID:          a6e98151-f242-4592-b107-25fbac5ab410
Description: Long-lived user authorization
Scope:       global
Token:       cf0e05d9-4eca-4948-a012-b91fe9704bab <------- <HerokuToken>
Updated at:  Fri Jan 01 2021 13:26:56 GMT-0700 (PDT) (less than a minute ago)
```

You will provide this token to the `HerokuService`, which you can register as an injectable service in the Startup.cs of your project. Use your token in the code below, marked with `<HerokuToken>` :

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddHerokuService("<HerokuToken>");
}
```

You can now inject the `HerokuService`, e.g. in your controller:

```c#
private readonly HerokuService client;

public HerokuServiceController(HerokuService client)
{
    this.client = client;
}
```
## Examples

An entire automatic provisioning process for two tenants in a multi tenancy approach (via multiple GitHub repos) can be found in the `JAD.Heroku.SDK.Test` project of this repo.

For each tenant the following services / apps / addons will be provisioned:
1. Asp.NET Core Web Api (app)
2. Angular Webapp (app)
3. Postgres Database (addOn)
4. Subdomain Routing (with [dnsimple][dnsimple])

### Get your account
The account is required to specify the owner e.g. for pipelines or apps.
```c#
var account = await client.GetMyAccountAsync();
```

### Create a pipeline:
A pipeline allows grouping of apps into different stages. 

Before you can create a pipeline you have attach an owner. To get your account id, you first have to call `await client.GetMyAccountAsync();`
```c#
var pipeline = await client.CreatePipelineAsync(new PipelineCreateOptions
{
    Name = $"example-pipeline",
    Owner = new Owner
    {
        Id = account.Id, // The id of the previously fetched account 
        Type = OwnerType.User
    }
});
```

### Create an app:
An app represents the program that you would like to deploy and run on Heroku.
```c#
var app = await client.CreateAppAsync(new AppCreateOptions
{
    Name = "your-app-name", // Please note: There are specific naming requirements
    Region = RegionFormat.Europe,
    Stack = StackFormat.Heroku20,
});
```

### Create a pipeline coupling:
By creating a pipeline coupling you can couple an app with a pipeline.
```c#
var pipelineCoupling = await client.CreatePipelineCouplingAsync(new PipelineCouplingCreateOptions
{
    AppId = app.Id, // The id of the previously created app
    PipelineId = pipeline.Id, // The id of the previously created pipeline
    Stage = PipelineStage.Test
});
```

### Create an add-on:
Add-ons represent add-ons that have been provisioned and attached to one or more apps. PostgresDbs are an add-on and each have their one Id for each price tier. `PlanIds` can be found only in context of an add-on.
```c#
var addOn = await client.CreateAddOnAsync(
    app.Id, // The id of the previously created app
    new AddOnCreateOptions
    {
        // The input of a plan can be either a planId or its name,
        // e.g. heroku-postgresql:standard-0
        Plan = "planId-or-name", 
        // Setting the Attachment here will automatically generate an
        // environment variable in the app with the connectionstring
        Attachment = new AddOnCreateAttachment
        {
            Name = "DATABASE_URL" // These names are Heroku-specific
        }
    }
);
```

### Create a dyno:
Dynos encapsulate running processes of an app on Heroku. Detailed information about dyno sizes can be found at: [DynoTypes][dyno-types].
```c#
var dyno = await client.CreateDynoAsync(
    app.Id, // The id of the previously created app
    new DynoCreateOptions
    {
        // This command will be overriden if a Procfile is present
        Command = "cd $HOME/heroku_output",
        Attach = false,
        Environment = new Dictionary<string, string>() { 
            { "COLUMNS", "80" }, 
            { "LINES", "24" } 
        },
        // This is the name of dyno at Heroku
        Size = "Free",
        TimeToLive = 1800,
        Type = "web"
    }
);
```

### Create a build:
A build represents the process of transforming a code tarball into a slug which then is deploying your code into the app.

 For this build it is required to upload a `tarball` file, which e.g. has been downloaded from a release of a GitHub repository. You need to upload this tarball file as a byte array to the `PutUrl` of the source.

```c#
var apiSource = await client.GetSourceAsync();
var tarballByteArray = new byte[] { };

// Uploading the byte array to the PutUrl of Heroku
await client.UploadSourceAsync(apiSource.SourceBlob.PutUrl, tarballByteArray);
```
Then in the build you can reference the `GetUrl` of the source to which you just uploaded our code and deploy your code in the app. You also need to reference the correct `buildpack`, either the name of an official Heroku buildpack or a buildpack from GitHub.

```c#
var build = await client.CreateBuildAsync(
    app.Id, // The id of the previously created app
    new BuildCreateOptions
    {
        SourceBlob = new SourceBlob
        {
            Url = apiSource.SourceBlob.GetUrl, 
            Version = "v1.0.0" // you can also use a GitHub release tag
        },
        BuildPacks = new List<BuildPack>() {
            new BuildPack {
                Url = "https://github.com/jincod/dotnetcore-buildpack.git"
            }
        }
    }
);
```

## Support

For any inquiries, bugs or comments, please open an issue in this repository.

[heroku]: https://dashboard.heroku.com/
[herokuAPI]: https://devcenter.heroku.com/articles/platform-api-reference
[package-manager-console]: https://docs.microsoft.com/en-us/nuget/tools/package-manager-console
[nuget-cli]: https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference
[heroku-cli]: https://devcenter.heroku.com/articles/heroku-cli
[nuget-website]: https://www.nuget.org/packages/JAD.Heroku.SDK/
[getting-started]: https://devcenter.heroku.com/articles/platform-api-quickstart
[dyno-types]: https://devcenter.heroku.com/articles/dyno-types