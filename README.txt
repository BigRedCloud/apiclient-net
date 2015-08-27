************************ Big Red Cloud API .NET Client ************************

The Big Red Cloud API .NET Client library is open source library to interact 
with Big Red Cloud API from .NET applications.

-------------------------------- Preconditions --------------------------------

To work with this library the following conditions must be satisfied:
1. .NET Framework 4.5 or greater must be installed.
2. Active account in Big Red Cloud Application with active API Key must exist.

-------------------------------- Configuration --------------------------------

Configuration settings for this library must be specified in app.config or 
web.config file of your application:

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="bigRedCloudApiSection" 
	  type="BigRedCloud.Api.Configuration.BigRedCloudApiSection, 
	        BigRedCloud.Api" />
  </configSections>
  
  <bigRedCloudApiSection apiServerUrl="https://app.bigredcloud.com/api/v1/">
    <apiKeys>
      <apiKey name="key1_name" value="key1_value" isDefault="true" />
      <apiKey name="key2_name" value="key2_value" />
    </apiKeys>
  </bigRedCloudApiSection>
</configuration>

In section <apiKeys> you must specify list of Api Keys which will be used to 
interact with Big Red Cloud API. In property "name" you must specify a 
friendly name of API Key. In property "value" you must specify the actual 
value of API Key. You can obtain this value in Big Red Cloud Application in 
the following way: login in application as Tenant Administrator, choose 
"Administration" tab at the top, choose "Users / API" tab below, choose "API 
Keys" tab at the left - here you can manage API Keys for your companies. Also 
you can choose one default key by setting property "isDefault" to "true".

------------------------------- Library building ------------------------------

To build this library you need a Visual Studio 2012 and .NET Framework 4.5 or 
greater.

-------------------------------- Library using --------------------------------

This library depends on the next third party components:
System.Net.Http.Formatting.dll version="5.2.3.0"
Newtonsoft.Json.dll version="6.0.0.0"

All third party components are placed in folder "Libs" in root of the project. 
When you add reference to Big Red Cloud API .NET Client library in your 
project, you must also add references to third party libraries described above.

Big Red Cloud API .NET Client is thread-safe. You can use it in multi-threading 
scenarious.

To make a call to API under the default API Key use the next pattern:
ApiClientProvider.Default.< Entities >.< Operation >()

For example, to receive a Sales Invoice with Id = 1 under the default API Key 
use:
ApiClientProvider.Default.SalesInvoices.Get(1)

To make a call to API under a specific API Key use the next pattern:
ApiClientProvider.For(< API Key friendly name >).< Entities >.< Operation >()

For example, to receive a Sales Invoice with Id = 1 under API Key with name 
"key1_name" use:
ApiClientProvider.For("key1_name").SalesInvoices.Get(1)
