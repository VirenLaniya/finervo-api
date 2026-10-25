# Finervo - Docker Development Guide

## Docker Compose Commands

### Start / Stop

```bash
# Start containers
docker compose up

# Start and rebuild images
docker compose up --build

# Start in background
docker compose up -d

# Start in background and rebuild
docker compose up --build -d

# Stop and remove containers
docker compose down

# Stop and remove containers and volumes
docker compose down -v
```

### Logs

```bash
# View all logs
docker compose logs

# Follow all logs
docker compose logs -f

# Follow API logs
docker compose logs -f finervo.api
```

### Status

```bash
# Show container status
docker compose ps

# Show Docker images
docker images

# Show Docker volumes
docker volume ls
```

### Build

```bash
# Rebuild API image
docker compose build finervo.api

# Rebuild without cache
docker compose build --no-cache
```

### Container

```bash
# Open a shell inside the API container
docker compose exec finervo.api sh

# Restart API container
docker compose restart finervo.api
```

### Cleanup

```bash
# Remove stopped containers
docker compose rm

# Remove unused Docker resources
docker system prune
```

---

# Development Flow

## 1. Start Development Environment

After cloning the project or pulling the latest changes:

```bash
docker compose up --build
```

This builds the latest API image and starts the configured services.

For background execution:

```bash
docker compose up --build -d
```

## 2. Verify Containers

Check that the containers are running:

```bash
docker compose ps
```

The API should be available at:
```text
http://localhost:8080
```

Swagger:
```text
http://localhost:8080/docs/swagger
```

## 3. Check Logs

If you want to monitor the API:

```bash
docker compose logs -f finervo.api
```

Use this especially when troubleshooting startup, database connection, migrations, or application errors.

## 4. Debugging with Visual Studio 2026

Since the Docker Compose orchestration project (`.dcproj`) is excluded from the solution explorer, you can debug your containerized code by attaching the Visual Studio debugger manually to the running container runtime.

### Step 1: Start the container in Debug Mode
Ensure your `docker-compose.override.yml` is passing the `BUILD_CONFIGURATION=Debug` argument, then spin up the environment in the background:
```bash
docker compose up --build -d
```

### Step 2: Attach the Visual Studio Debugger
1. Open your `Finervo.slnx` solution in **Visual Studio 2026**.
2. Set your breakpoints in the source code.
3. Open the menu: **Debug** > **Attach to Process...** (or press `Ctrl + Alt + P`).
4. Change the **Connection type** drop-down to **Docker (Linux Container)**.
5. Click the **Browse...** button next to *Connection target*.
6. From the list, select your running API container (e.g., `finervo-api`).
7. In the process grid below, look for the **`dotnet`** process (the row where the command line matches `Finervo.API.dll`).
8. Ensure **Attach to:** at the bottom says **Managed (.NET Core for Unix)** or automatic.
9. Click **Attach**. 

> 💡 **Tip:** If the debugger warns about lack of symbols, verify that your Dockerfile isn't hardcoded to a `Release` configuration and that the `appsettings.Development.json` is correctly mounted via your compose overrides.

## 5. Make Code Changes

During development:

1. Stop/rebuild the container when required.
2. Make your code changes.
3. Rebuild the API image.
4. Start the containers again.

```bash
docker compose up --build
```

If the containers are already running and you only need to restart the API:

```bash
docker compose restart finervo.api
```

> Note: `restart` does not rebuild the image. Use `docker compose up --build` after changing application code that is baked into the image. After rebuilding and restarting, repeat the **Attach to Process** steps to re-engage your debugger session.

## 6. Database / Infrastructure Changes

When changing PostgreSQL or other infrastructure configuration, rebuild/start normally:

```bash
docker compose up --build
```

If you need to completely recreate the environment, including Docker volumes:

```bash
docker compose down -v
docker compose up --build
```

> `down -v` deletes Docker volumes. For PostgreSQL, this means the database data stored in that volume will be deleted.

## 7. Troubleshooting

### API is not starting

Check the API logs:

```bash
docker compose logs -f finervo.api
```

### Container status

```bash
docker compose ps
```

### Need a clean rebuild

```bash
docker compose down
docker compose build --no-cache
docker compose up
```

### Need to inspect the container

```bash
docker compose exec finervo.api sh
```

### Configuration changes are not taking effect

Recreate the containers:

```bash
docker compose down
docker compose up --build
```

If the change involves database/environment volumes and you intentionally want a fresh database:

```bash
docker compose down -v
docker compose up --build
```

---

# Common Daily Workflow

For normal development with debugging active, this workflow is ideal:

```bash
# 1. Start up your stack cleanly in the background
docker compose up --build -d

# 2. Open Visual Studio 2026 -> Ctrl + Alt + P -> Select container -> Attach to 'dotnet'

# 3. In a side terminal window, actively track standard output text logs
docker compose logs -f finervo.api
```

When finished:

```bash
docker compose down
```

---

# Quick Reference

| Task | Command / Action |
|---|---|
| Start | `docker compose up` |
| Start + rebuild | `docker compose up --build` |
| Start in background | `docker compose up -d` |
| Start background + rebuild | `docker compose up --build -d` |
| Stop | `docker compose down` |
| Stop + delete volumes | `docker compose down -v` |
| Check status | `docker compose ps` |
| Follow API logs | `docker compose logs -f finervo.api` |
| Rebuild API | `docker compose build finervo.api` |
| Clean rebuild | `docker compose build --no-cache` |
| Restart API | `docker compose restart finervo.api` |
| API shell | `docker compose exec finervo.api sh` |
| **Attach Debugger** | **VS 2026 -> `Ctrl + Alt + P` -> Linux Container -> target `dotnet`** |
| Swagger | `http://localhost:8080/docs/swagger` |
