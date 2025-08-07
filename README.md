# tsol - String Extensions Library

A simple .NET library providing string extension methods.

## Building

### Current (.NET 8)
```bash
cd src/tsol
dotnet restore
dotnet build --no-restore
```

Or from repository root:
```bash
dotnet build src/tsol/tsol.sln
```

### Future (.NET 9+)
When upgrading to .NET 9 or later, you can use the XML solution format:
```bash
dotnet build tsol.slnx
```

## Solution File Formats

- `src/tsol/tsol.sln` - Traditional MSBuild solution format (current)
- `tsol.slnx` - XML solution format (for .NET 9+)

The repository has been prepared for the transition to .slnx format when upgrading to .NET 9+.