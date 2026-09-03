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

## 4. Make Code Changes

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

> Note: `restart` does not rebuild the image. Use `docker compose up --build` after changing application code that is baked into the image.

## 5. Database / Infrastructure Changes

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

## 6. Troubleshooting

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

For normal development, this is usually enough:

```bash
# Start / rebuild
docker compose up --build

# In another terminal, monitor logs
docker compose logs -f finervo.api
```

When finished:

```bash
docker compose down
```

For background development:

```bash
docker compose up --build -d
docker compose logs -f finervo.api
```

---

# Quick Reference

| Task | Command |
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
| Swagger | `http://localhost:8080/docs/swagger` |
