# Run docker + postgres

```bash
sudo docker-compose up --no-start

sudo docker start postgresVoluntaryWorking pgadmin4VoluntaryWorking redisVoluntaryWorking
```

# Stop docker + postgres

```bash
sudo docker stop postgresVoluntaryWorking pgadmin4VoluntaryWorking redisVoluntaryWorking
```

# Backup db

```bash
sudo docker exec postgresVoluntaryWorking pg_dump -U aragami voluntarydb > backup.sql
```

# Restore db

```bash
sudo docker exec -i postgresVoluntaryWorking psql -U aragami -d voluntarydb < backup.sql
```
