# tsol - String Extensions Library

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

tsol is a simple .NET library providing string extension methods. It contains a single utility class `StringExtensions` with the `IsNullOrEmptyOr` method that checks if a string is null, empty, or equals a specific value.

## Working Effectively

### Prerequisites and Setup
- .NET 8.0 SDK is required and should already be installed in the environment
- Verify installation: `dotnet --version` (should show 8.x.x)
- No additional dependencies or external tools are required

### Build Process
**CRITICAL ISSUE**: The original project file targets .NET Framework 4.0 which does not work on Linux environments. The GitHub Actions workflow references .NET 5.0.x but the project file has not been modernized.

#### Current Reality - What Works:
- `cd src/tsol`
- `dotnet restore` - completes in ~1 second, usually reports "Nothing to do"
- `dotnet build --no-restore` - **FAILS** with .NET Framework 4.0 targeting issue
- `dotnet test --no-build --verbosity normal` - succeeds but finds no tests (no actual test projects exist)

#### What Does NOT Work:
- Building the original project file (tsol.csproj) fails due to missing .NET Framework 4.0 targeting pack
- The GitHub Actions workflow will fail in its current state
- No package manager installations are needed or possible

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
       <TargetFramework>net8.0</TargetFramework>
       <RootNamespace>tsol</RootNamespace>
       <AssemblyName>tsol</AssemblyName>
       <ImplicitUsings>disable</ImplicitUsings>
       <Nullable>disable</Nullable>
       <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
     </PropertyGroup>
   </Project>
   ```

2. **Build with modern project file**:
   - Save above content as `tsol-modern.csproj` in `src/tsol/`
   - `dotnet build tsol-modern.csproj` - succeeds in ~2 seconds

3. **Test the library functionality**:
   - Create a simple console app to exercise the StringExtensions
   - Use the modern project format targeting net8.0
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
│       └── dotnet.yml          # GitHub Actions workflow (currently broken)
├── README                      # Simple greeting file, not documentation
└── src/
    └── tsol/
        ├── Properties/
        │   └── AssemblyInfo.cs # Assembly metadata
        ├── StringExtensions.cs  # Main library code - THE CORE FILE
        ├── tsol.csproj         # Original project file (broken on Linux)
        └── tsol.sln            # Solution file
```

### Important Files
- **StringExtensions.cs**: The only functional code file - contains the `IsNullOrEmptyOr` method
- **tsol.csproj**: Original project file targeting .NET Framework 4.0 (incompatible with Linux/CI)
- **.github/workflows/dotnet.yml**: CI configuration that needs the project modernized to work

### Frequently Modified Files
- When changing library functionality: `src/tsol/StringExtensions.cs`
- When fixing build issues: create modern project file as shown above
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
- **Do not attempt to fix the original tsol.csproj** for .NET Framework 4.0 on Linux
- **Create a modern project file** using the template provided above
- **Target net8.0** or compatible .NET version
- **Disable GenerateAssemblyInfo** to avoid conflicts with existing AssemblyInfo.cs

## CI/Build System Notes

### GitHub Actions Status
- **Current workflow is broken** due to .NET Framework 4.0 targeting
- Workflow attempts to use .NET 5.0.x but project file is incompatible
- **Do not run the existing CI workflow expecting it to pass**

### Required CI Fixes (if implementing)
- Update tsol.csproj to use SDK-style format targeting .NET 5.0+ 
- Ensure GenerateAssemblyInfo is disabled
- Update workflow to use .NET 8.0 for consistency with development environment

### No Linting/Formatting Tools
- No .editorconfig, linting rules, or formatting tools are configured
- Standard C# formatting conventions should be followed
- No automated formatting validation exists

## Development Environment Notes

### What's Available
- .NET 8.0 SDK (confirmed working)
- Standard dotnet CLI tools
- MSBuild via dotnet

### What's NOT Available
- .NET Framework targeting packs
- Mono/xbuild
- Package managers beyond dotnet CLI
- Linting tools

### Limitations
- Cannot build original project file on Linux
- No existing test framework or test projects
- CI workflow will fail without project modernization
- Library functionality is extremely simple (single method)

## Quick Reference Commands

All commands assume you're in the repository root `/home/runner/work/tsol/tsol`:

```bash
# Navigate to source
cd src/tsol

# Check .NET installation
dotnet --version

# Try original build (will fail)
dotnet restore
dotnet build --no-restore  # Fails with .NET Framework 4.0 error

# Test command (succeeds but no tests)
dotnet test --no-build --verbosity normal

# Working approach - create modern project file first, then:
dotnet build tsol-modern.csproj  # ~2 seconds
```

Remember: **Always create test scenarios to validate library functionality** rather than relying only on successful compilation.