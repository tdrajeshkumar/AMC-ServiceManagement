# Workspace Setup Options

Date: 2026-05-27 (UTC)

## Scope

This document evaluates **workspace setup mechanisms other than `apt-get`** and whether they can provision **.NET SDK 8** in the current environment.

## Supported Setup Mechanisms

### 1) Setup script support
- **Status:** Supported.
- **Evidence:** Standard shell runtimes and fetch tools are available (`bash`, `sh`, `curl`, `wget`).
- **Implication:** Script-based installers (for example, Microsoft `dotnet-install.sh`) are a viable mechanism in principle.

### 2) Devcontainer support
- **Status:** Not currently configured.
- **Evidence:** No `.devcontainer/` folder or `devcontainer.json` exists in this repository; `devcontainer` CLI is not present.
- **Implication:** Devcontainer-based provisioning is not immediately available without adding new configuration.

### 3) Dockerfile support
- **Status:** Not currently configured.
- **Evidence:** No `Dockerfile` exists in this repository; container runtime CLIs (`docker`, `podman`) are not present.
- **Implication:** Docker-based provisioning is not immediately available in this workspace.

### 4) Language/runtime configuration support
- **Status:** Partially supported.
- **Evidence:** `mise` is installed (`2026.4.28`), but no repo-level runtime config (`mise.toml`, `.tool-versions`, `global.json`) is present.
- **Implication:** Runtime pinning/provisioning can be introduced via `mise`, but is not yet configured in the repository.

## Can .NET SDK 8 Be Provisioned?

- **Yes, potentially**, using setup-script or `mise`-based provisioning.
- **Current state:** `.NET` is not installed (`dotnet --info` previously failed with `command not found`).
- **Constraint:** Provisioning success is dependent on external network/package retrieval and repository policy for adding setup files.

## Recommended Approach

1. **Primary recommendation:** Add a repository-managed runtime configuration path using `mise` (e.g., `mise.toml`) to pin `.NET 8` for repeatable setup.
2. **Fallback recommendation:** Use scripted bootstrap (`dotnet-install.sh`) when `mise` is not preferred.
3. **Validation step after provisioning:** Run:
   - `dotnet --info`
   - `dotnet restore`
   - `dotnet build`

## Limitations

- No existing devcontainer or Dockerfile scaffolding in the repository.
- No local container runtime (`docker`/`podman`) detected.
- No existing runtime config file (`mise.toml`, `.tool-versions`, or `global.json`) detected.
- `mise` update check showed transient HTTP failures during validation, indicating possible network variability.
- .NET SDK 8 is not preinstalled in the current environment.
