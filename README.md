# jQuery DataTable Server-Side with .NET

A simple Visual Studio solution using jQuery DataTable with Server-Side processing using .NET

## Table of Contents

* [Getting Started](#getting-started)
* [Releases](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases)
* [Branches](#branches)
* [Dependencies](#dependencies)
* [External libraries](#external-libraries)
* [Testing](#testing)
* [Pull requests](#pull-requests)
* [Author](#author)

## Getting Started

* Download the latest release [here](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/latest), or download a previous release [here](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases)
* Uncompress the ZIP and open the solution with Visual Studio
* Run or Debug the solution and you'll have 1000 rows being server-side processed with jQuery DataTable plugin
* Export the filtered registers in several formats: Excel, CSV and HTML

## Branches

| Branches | Releases | .NET Version | Description |
|-|-|-|-|
| [master](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/master) |  | 6.0 (SDK: 6.0.302) | Development branch |
| [net-60](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/net-60) | [3.3.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/3.3.0)<br>[3.2.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/3.2.0)<br>[3.1.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/3.1.0)<br>[3.0.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/3.0.0) | 6.0 (SDK: 6.0.302) | Latest version with .NET 6.0 |
| [net-50](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/net-50) | [2.0.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/2.0.0) | 5.0 (SDK: 5.0.402) | Latest version with .NET 5.0 |
| [net-50-preview](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/net-50-preview) | [1.4.0-preview](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/1.4.0-preview) | 5.0 (SDK: 5.0.100-preview.7.20366.6) | Latest version with .NET 5.0 ([info](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/master/docs/net-50)) 🧪 |
| [net-core-31](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/net-core-31) | [1.3.1](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/1.3.1)<br>[1.3.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/1.3.0) | 3.1 (SDK: 3.1.301) | Latest version with .NET Core 3.1 |
| [net-core-30](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/net-core-30) | [1.2.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/1.2.0) | 3.0 (SDK: 3.0.103) | Latest version with .NET Core 3.0 |
| [net-core-22](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/net-core-22) | [1.1.1](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/1.1.1)<br>[1.1.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/1.1.0) | 2.2 (SDK: 2.2.202) | Latest version with .NET Core 2.2 |
| [net-core-22-old](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/tree/net-core-22-old) | [1.0.0](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/releases/tag/1.0.0) | 2.2 (SDK: 2.2.202) | Old .NET Core 2.2 version (discontinued) |

## Dependencies

* [EPPlus (v6.2.3)](https://www.nuget.org/packages/EPPlus/6.2.3)
* [Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore (v6.0.7)](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore/6.0.7)
* [Microsoft.AspNetCore.Identity.EntityFrameworkCore (v6.0.7)](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/6.0.7)
* [Microsoft.AspNetCore.Identity.UI (v6.0.7)](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.UI/6.0.7)
* [Microsoft.AspNetCore.Mvc.NewtonsoftJson (v6.0.7)](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson/6.0.7)
* [Microsoft.EntityFrameworkCore.SqlServer (v6.0.7)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/6.0.7)
* [Microsoft.EntityFrameworkCore.Tools (v6.0.7)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/6.0.7)
* [RandomGen (v1.1.6)](https://www.nuget.org/packages/RandomGen/1.1.6)
* [Swashbuckle.AspNetCore (v6.5.0)](https://www.nuget.org/packages/Swashbuckle.AspNetCore/6.5.0)
* [Swashbuckle.AspNetCore.Swagger (v6.5.0)](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/6.5.0)
* [YamlDotNet (v13.1.0)](https://www.nuget.org/packages/YamlDotNet/13.1.0)

## External libraries

* [Bootstrap (v5.2.3)](https://getbootstrap.com/)
* [jQuery (v3.6.4)](https://jquery.com/)
* [jQuery-datatable (v1.13.4)](https://datatables.net/)
* [datetime-moment (v1.10.21)](https://datatables.net/plug-ins/sorting/datetime-moment)
* [momentjs (v2.29.4)](https://momentjs.com/)

## Testing

This project was developed and tested using:

* Microsoft Visual Studio Enterpise 2022 (version 17.5.5) + GhostDoc Community VS Extension (v2021.2.21290) for generating XML Documentation
* Windows 11 Pro (version 21H2) (OS build 22000.1936)

## Issues

Feel free to report any kind of issue [here](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/issues/new)

## Pull requests

You can add new features, improve an existing item or fix bugs by doing a pull request [here](https://github.com/DavidSuescunPelegay/jQuery-datatable-server-side-net-core/pulls)

## Author

* **David Suescun Pelegay** - [LinkedIn](https://www.linkedin.com/in/DavidSuescunPelegay) - [PayPal](https://www.paypal.me/DavidSuescunPelegay)
