<h3>Install Package</h3>
Nuget Package Manager
  Install-Package Microsoft.EntityFrameworkCore -Version 6.0.3

<h3>Configuration</h3>
  Add ConnectionString path in the root of your appsettings.json
  <pre>
  "ConnectionStrings": 
  {
    "DefaultConnection": "Server=<b>server_path</b>; Database=<b>database_name<b>; Trusted_Connection=true;"
  }
  </pre>
  
  Add Google Api key in the root of your appsettings.json
   <pre>
  "GoogeGeoCodingApi": 
  {
    "ApiKey": "<b>Enter Google Api Key</b>"
  }
  </pre>
  
  That's all!
