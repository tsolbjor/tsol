# tsol - String Extensions Library

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

tsol is a simple .NET library providing string extension methods. It contains a single utility class `StringExtensions` with the `IsNullOrEmptyOr` method that checks if a string is null, empty, or equals a specific value.

## Working Effectively

### Prerequisites and Setup
- .NET 9.0 SDK is required and should already be installed in the environment
- Verify installation: `dotnet --version` (should show 9.x.x)
- No additional dependencies or external tools are required

### Build Process
The project now targets .NET 9.0 and uses the modern .slnx solution format for improved tooling integration.

#### Current Reality - What Works:
- `cd src/tsol`
- `dotnet restore` - completes in ~1 second, usually reports "Nothing to do"
- `dotnet build --no-restore` - succeeds with .NET 9.0 targeting
- `dotnet test --no-build --verbosity normal` - succeeds but finds no tests (no actual test projects exist)
- From repository root: `dotnet build tsol.slnx` - uses the XML solution format

#### Legacy Support:
- Traditional .sln format still available at `src/tsol/tsol.sln` for compatibility
- GitHub Actions workflow uses .NET 9.0 and .slnx format

### Timing Expectations
- `dotnet restore`: ~1 second
- `dotnet build` (when working): ~2 seconds  
- `dotnet test`: ~1 second (no actual tests to run)
- **NEVER CANCEL**: All commands complete within seconds for this simple project

### Working Solution Approach
To actually build and test this library:

1. **Create a modern project file** for building/testing:
   ```xml
   <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <TargetFramework>net9.0</TargetFramework>
       <RootNamespace>tsol</RootNamespace>
       <AssemblyName>tsol</AssemblyName>
       <ImplicitUsings>disable</ImplicitUsings>
       <Nullable>disable</Nullable>
       <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
     </PropertyGroup>
   </Project>
   ```

2. **Build with modern project file**:
   - The main project already uses this format as `tsol.csproj`
   - `dotnet build tsol.slnx` - succeeds in ~2 seconds with .NET 9

3. **Test the library functionality**:
   - Create a simple console app to exercise the StringExtensions
   - Use the modern project format targeting net9.0
   - Run: `dotnet run --project [test-project.csproj]`

## Validation Scenarios

**CRITICAL**: Always manually validate the StringExtensions functionality after making changes:

### Essential Test Scenario
Create and run this validation test:
```csharp
using System;
using tsol;

class Program
{
    static void Main()
    {
        string testNull = null;
        string testEmpty = "";
        string testValue = "hello";

        Console.WriteLine($"null.IsNullOrEmptyOr('test'): {testNull.IsNullOrEmptyOr("test")}"); // Should be True
        Console.WriteLine($"''.IsNullOrEmptyOr('test'): {testEmpty.IsNullOrEmptyOr("test")}"); // Should be True  
        Console.WriteLine($"'hello'.IsNullOrEmptyOr('hello'): {testValue.IsNullOrEmptyOr("hello")}"); // Should be True
        Console.WriteLine($"'hello'.IsNullOrEmptyOr('world'): {testValue.IsNullOrEmptyOr("world")}"); // Should be False
        
        Console.WriteLine("StringExtensions functionality test completed successfully!");
    }
}
```

Expected output:
```
null.IsNullOrEmptyOr('test'): True
''.IsNullOrEmptyOr('test'): True
'hello'.IsNullOrEmptyOr('hello'): True
'hello'.IsNullOrEmptyOr('world'): False
StringExtensions functionality test completed successfully!
```

### Key Validation Rules
- **Always test null string input** - the method must handle null without throwing
- **Always test empty string input** - should return true when checking any value
- **Always test exact match scenario** - should return true when string equals the comparison value
- **Always test non-match scenario** - should return false when string doesn't equal comparison value

## Repository Structure and Navigation

### Root Directory Contents
```
.
├── .git/
├── .github/
│   └── workflows/
│       └── dotnet.yml          # GitHub Actions workflow (.NET 9 + .slnx)
├── .gitignore                  # Git ignore rules
├── README.md                   # Project documentation
├── tsol.slnx                   # XML solution format (primary)
└── src/
    └── tsol/
        ├── Properties/
        │   └── AssemblyInfo.cs # Assembly metadata
        ├── StringExtensions.cs  # Main library code - THE CORE FILE
        ├── tsol.csproj         # Modern SDK-style project (.NET 9)
        └── tsol.sln            # Traditional solution (legacy compatibility)
```

### Important Files
- **StringExtensions.cs**: The only functional code file - contains the `IsNullOrEmptyOr` method
- **tsol.csproj**: Modern SDK-style project file targeting .NET 9.0
- **tsol.slnx**: XML solution format for .NET 9+ (current primary format)
- **src/tsol/tsol.sln**: Traditional solution format (legacy compatibility)
- **.github/workflows/dotnet.yml**: CI configuration using .NET 9 and .slnx format

### Frequently Modified Files
- When changing library functionality: `src/tsol/StringExtensions.cs`
- When updating build configuration: `tsol.slnx` (primary) or `src/tsol/tsol.sln` (legacy)
- When adding tests: create new test project (none currently exist)

## Common Development Tasks

### Adding New String Extension Methods
1. Open `src/tsol/StringExtensions.cs`
2. Add new static methods to the `StringExtensions` class
3. Follow the existing pattern: `public static [ReturnType] MethodName(this string s, [parameters])`
4. **Always validate with test scenarios** as described above

### Testing Changes
1. Create a console application project using modern .NET format
2. Reference the StringExtensions.cs file or build as library
3. Write test scenarios exercising all code paths
4. Run and verify output matches expectations

### Fixing Build Issues
- The project now uses modern SDK-style format targeting .NET 9.0
- **Primary build command**: `dotnet build tsol.slnx` from repository root  
- **Legacy compatibility**: `dotnet build src/tsol/tsol.sln` for traditional format
- **Target net9.0** for current development

## CI/Build System Notes

### GitHub Actions Status
- **Current workflow updated** for .NET 9.0 and .slnx format
- Workflow uses .NET 9.0.x SDK with `dotnet build tsol.slnx`
- **CI should pass** with the modernized project configuration

### Required CI Fixes (if implementing)
- Project already updated to use SDK-style format targeting .NET 9.0
- Workflow updated to use .slnx format and .NET 9.0
- Legacy .sln format maintained for backward compatibility

### No Linting/Formatting Tools
- No .editorconfig, linting rules, or formatting tools are configured
- Standard C# formatting conventions should be followed
- No automated formatting validation exists

## Development Environment Notes

### What's Available
- .NET 9.0 SDK (for CI environment)
- Standard dotnet CLI tools
- MSBuild via dotnet

### What's NOT Available
- Older .NET Framework targeting packs (no longer needed)
- Mono/xbuild (not needed with modern .NET)
- Package managers beyond dotnet CLI
- Linting tools

### Limitations
- Local development may require .NET 9 SDK for full compatibility
- Legacy .sln format available for older .NET versions
- CI workflow uses .NET 9 and .slnx format
- Library functionality is extremely simple (single method)

## Quick Reference Commands

All commands assume you're in the repository root `/home/runner/work/tsol/tsol`:

```bash
# Navigate to source
cd src/tsol

# Check .NET installation
dotnet --version

# Build using modern .slnx format (primary)
dotnet build ../../tsol.slnx  # ~2 seconds

# Build using legacy .sln format (compatibility)
dotnet build tsol.sln

# Test command (succeeds but no tests)
dotnet test --no-build --verbosity normal

# From repository root - recommended approach:
dotnet build tsol.slnx  # ~2 seconds
```

Remember: **Always create test scenarios to validate library functionality** rather than relying only on successful compilation.