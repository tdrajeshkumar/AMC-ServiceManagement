# Build and Runtime Setup

This repository uses a repository-managed .NET runtime configuration via `mise.toml` and `global.json`.

## Prerequisites

- `mise` installed on your machine.

## Environment Setup

Run the following commands from the repository root:

```bash
mise install
```

```bash
dotnet --info
```

```bash
dotnet restore
```

```bash
dotnet build
```

```bash
dotnet run
```

## Notes

- `mise install` provisions the .NET SDK defined in `mise.toml`.
- `global.json` pins the SDK feature band for consistent build behavior.
