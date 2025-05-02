# JwtAuthentication 

## Packages Needed
- Here, we installed the below packages
    - Microsoft.AspNetCore.Authentication.JwtBearer (8.0.14)
    - System.IdentityModel.Tokens.Jwt (8.9.0)

## Registering Packages in Program.cs
- We registered the JWT authentication service (dependency) in the `Program.cs` file as shown below:
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


- We included the below code in the `Program.cs` file to enable us use the Authentication service just above the `UseAuthorization()` method:
```csharp
app.UseAuthentication();
```