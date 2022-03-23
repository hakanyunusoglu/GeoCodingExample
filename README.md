<h3>Install Package</h3>
  Install-Package Microsoft.EntityFrameworkCore -Version 7.0.0-preview.2.22153.1

<h3>Configuration</h3>
  Add ConnectionString path in the root of your appsettings.json
  <pre>
  "ConnectionStrings": 
  {
    "DefaultConnection": "Server={server_path}; Database={database_name}; Trusted_Connection=true;"
  }
  </pre>
  
  Add Google Api key in the root of your appsettings.json
   <pre>
  "GoogeGeoCodingApi": 
  {
    "ApiKey": "{Enter Google Api Key}"
  }
  </pre>
  
  That's all!
