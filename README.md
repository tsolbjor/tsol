# tsol - String Extensions Library

A simple .NET library providing string extension methods.

## Building

### Current (.NET 9)
```bash
cd src/tsol
dotnet restore
dotnet build --no-restore
```

Or from repository root:
```bash
dotnet build tsol.slnx
```

### Legacy (.NET 8 and earlier)
For older .NET versions, you can use the traditional solution format:
```bash
dotnet build src/tsol/tsol.sln
```

## Solution File Formats

- `tsol.slnx` - XML solution format (current, requires .NET 9+)
- `src/tsol/tsol.sln` - Traditional MSBuild solution format (legacy compatibility)

The repository uses the .slnx format with .NET 9 for improved tooling and development experience.