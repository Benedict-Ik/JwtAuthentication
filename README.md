# JwtAuthentication 
In this branch, we created a JwtService class that encapsulates the logic for generating JWTs.

## Creating the JwtService
- We first created a `JwtService` embedded within an already created `Services` folder.
- Then created the `GenerateToken` method that generates a JWT token using the `JwtSecurityTokenHandler` class.
- We modified the `appsettings.json` file to include the JWT settings such as `Issuer`, `Audience`, and `Key`.
    - The `issuer` and `audience` properties are set to the same URL which can be gotten from the `launchSettings.json` file found in the `properties` folder.
    - Note that we are concerned with the URL of the `applicationURL` found in the `https` scheme.
```json
{
  "Jwt": {
    "Key": "sUpErsEcrEtkEy321!@#$789",
    "Issuer": "https://localhost:7181",
    "Audience": "https://localhost:7181"
  }
}
```

- We modified the `JwtService` class to call the defined Jwt Settings for the `Issuer`, `Audience`, and `Key` properties from the `appsettings.json` file.

## Modifying Program.cs
- Ensured the `Program.cs` class has the below code:
```csharp
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // The below lines are subjective and are based on the dev's configuration
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
```