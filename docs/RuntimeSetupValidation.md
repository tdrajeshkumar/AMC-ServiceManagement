# Runtime Setup Validation

Date: 2026-05-27 (UTC)

## Commands Executed

```bash
mise trust
mise install
dotnet --info
dotnet restore
dotnet build
```

## SDK Version

- 8.0.421

## Runtime Version

- Host runtime: 8.0.27
- Microsoft.AspNetCore.App: 8.0.27
- Microsoft.NETCore.App: 8.0.27

## Restore Result

- Failed.
- Error: `MSBUILD : error MSB1003: Specify a project or solution file. The current working directory does not contain a project or solution file.`

## Errors

- `dotnet restore`: `MSBUILD : error MSB1003: Specify a project or solution file. The current working directory does not contain a project or solution file.`
- `dotnet build`: `MSBUILD : error MSB1003: Specify a project or solution file. The current working directory does not contain a project or solution file.`

## Warnings

- `mise install` completed successfully after transient HTTP retry warnings while fetching tool metadata.
