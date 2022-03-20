# RD.NET

Premiumize .NET wrapper library written in C#

Supports all API calls, but only ApiKey authentication.

## Usage

Create an instance of `PremiumizeNETClient` for each user you want to authenticate. If you need to support multiple users you will need to create a new instance every time you switch users.

```csharp
var client = new PremiumizeNETClient("api key");
```

Pass in the Api Key for the user.

Use intellisense on the client to get the API calls, mimics the format of the API here: https://app.swaggerhub.com/apis-docs/premiumize.me/api/1.7.1#/folder/folderPaste

For example `/account/info` can be accessed through:

```csharp
var client = new PremiumizeNETClient("api key");
var accountInfo = client.Account.InfoAsync();
```

## Authentication

Each user has its own API key, which can be found here: <https://www.premiumize.me/account>.

## Unit tests

The unit tests are not designed to be ran all at once, they are used to act as a test client.

Create a file `setup.txt` and put your API token in there.

Some functions will need replacement ID's to work properly.