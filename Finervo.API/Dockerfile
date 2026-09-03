# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# ─── Stage 1: Restore ─────────────────────────────────────────-
# Separate restore stage for better layer caching
# Restore only runs again if .csproj files change
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /src

# Copy solution and project files
COPY Finervo.slnx .
COPY Finervo.API/Finervo.API.csproj                         Finervo.API/
COPY Finervo.Application/Finervo.Application.csproj         Finervo.Application/
COPY Finervo.Core/Finervo.Core.csproj                       Finervo.Core/
COPY Finervo.Infrastructure/Finervo.Infrastructure.csproj   Finervo.Infrastructure/
COPY Finervo.Contracts/Finervo.Contracts.csproj             Finervo.Contracts/
COPY Finervo.Shared/Finervo.Shared.csproj                   Finervo.Shared/

# Restore against solution — restores all projects
RUN dotnet restore Finervo.slnx

# ─── Stage 2: Build ───────────────────────────────────────────
FROM restore AS build
WORKDIR /src

# Copy everything else — source code
# .dockerignore excludes: bin/, obj/, appsettings.*.Development.json etc
COPY . .

# Build
RUN dotnet build Finervo.API/Finervo.API.csproj \
    --configuration Release \
    --no-restore

# ─── Stage 3: Publish ─────────────────────────────────────────
FROM build AS publish
WORKDIR /src

RUN dotnet publish Finervo.API/Finervo.API.csproj \
    --configuration Release \
    --no-restore \
    --no-build \
    --output /app/publish \
    /p:UseAppHost=false    # Reduces image size, not needed inside Linux container

# ─── Stage 4: Runtime ─────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Environment variables — best practices for .NET in containers
ENV DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_HTTP_PORTS=8080

# Non-root user — security best practice - a non-root user named app is already provided by image
#RUN addgroup --system finervo && \
    #adduser --system --ingroup finervo finervo

# Create logs directory with correct ownership
RUN mkdir -p /app/logs && \
    chown -R app:app /app

# Copy published output from publish stage
COPY --from=publish --chown=app:app /app/publish .

# Aspnet image does not contain curl by default - install explicitly as root
RUN apt-get update && \
    apt-get install -y --no-install-recommends curl && \
    rm -rf /var/lib/apt/lists/*

# Switch to non-root user
USER app

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s \
            --timeout=10s \
            --start-period=30s \
            --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "Finervo.API.dll"]

# ─── Stage 5: Final (VS debugging support) ────────────────────
# When running normally this stage is skipped
# VS uses this as the target when debugging in container
FROM runtime AS final
WORKDIR /app
COPY --from=publish --chown=app:app /app/publish .