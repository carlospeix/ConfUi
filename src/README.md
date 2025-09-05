# ConfUI - Dynamic Entity Management Framework

ConfUI is a .NET Framework 4.8 solution for dynamically generating entity management interfaces in ASP.NET MVC applications. It provides a powerful framework for building CRUD operations with minimal code.

## Project Structure

The solution consists of the following projects:

- **Model**: Contains domain entities and repository interfaces.
- **Infrastructure**: Provides NHibernate-based implementation of repositories and data access.
- **WebGenerics**: MVC web application demonstrating dynamic entity management.
- **MetadataBuilder**: Core library for metadata-driven UI generation.
- **MetadataBuilder.Tests**: Unit tests for the MetadataBuilder library.

## Features

- **Dynamic Entity Management**: Generate UI components based on entity metadata.
- **Fluent API**: Configure entity metadata using a fluent, type-safe API.
- **Custom Validation**: Apply validation rules at the metadata level.
- **Reference Handling**: Automatically handle entity references with dropdown lists.
- **Flexible Customization**: Override default behavior with custom templates and configurations.
- **NHibernate Integration**: Seamless data access using NHibernate ORM.

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.8
- SQL Server (MS SQL 2008 or later)

### Installation

1. Clone this repository
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Update the connection string in the Web.config file
5. Build the solution

### Configuration

The application uses a fluent API for configuring entity metadata:

```csharp
// Example metadata configuration
var reg = ConfigurationHolder.GetRootRegistrar().WireToRuntime();

reg.ForType<Customer>()
   .ForProperty(m => m.FirstName, md => {
       md.DisplayName("First Name");
       md.Required();
       md.StringLength(50, 1);
   })
   .ForProperty(m => m.LastName, md => {
       md.DisplayName("Last Name");
       md.Required();
       md.StringLength(50, 1);
   });
```

## Sample Applications

The WebGenerics project includes sample implementations for:

- Jurisdictions
- Institutions
- Educators
- Centers
- Organizations
- Workshops

## Development

### Adding a New Entity

1. Create a new entity class in the Model project
2. Configure metadata in `Web\Config\MetadataBuilderConfig.cs`
3. Add a repository interface and implementation
4. Register in the IoC container

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Uses NHibernate for ORM functionality
- Uses ConfOrm for mapping configuration
- Built on ASP.NET MVC framework