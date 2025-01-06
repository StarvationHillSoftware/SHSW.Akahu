# SHSW.Akahu

SHSW.Akahu is a .NET 8 library for accessing the Akahu Open Finance APIs.
This SDK is written in C# 12.

## Installation

If via your IDE, look for SHSW.Akahu in the NuGet tool

If via .NET command line interface, run dotnet add package SHSW.Akahu --version

If via .csproj file, add \<PackageReference Include="SHSW.Akahu" Version="<LATEST_VERSION>"\/>

## Usage

in program.cs

```c#
using SHSW.Akahu.Service;

// add a singleton...
builder.Services.AddSingleton<SHSW.Akahu.Service.AkahuConnection>();
.
.
.
var app = builder.Build();
.
.
// Initialise your connection with your API keys (for security these should probably be stored in appsettings.json file)
app.Services.GetRequiredService<AkahuConnection>().
    InitialiseConnection("user_token_abc123xyz098", "app_token_abc123xyz098");
```
using in a page
```c#
@inject AkahuConnection akahu
.
.
@code
{
private List<SHSW.Akahu.Model.Account> accounts { get; set; }
private List<SHSW.Akahu.Model.Transaction> transactions { get; set; }
private string AccountId = "";

  protected override async Task OnInitializedAsync()
  {
      akahu.OpenConnection();
      accounts = await akahu.GetAccountsAsync();
// AccountId can be set to select transactions from a selected account only.
      transactions = await akahu.GetTransactionsASync(AccountId) 
  }
}
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
