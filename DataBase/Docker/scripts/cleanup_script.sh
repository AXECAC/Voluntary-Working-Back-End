#!/bin/bash

BACKUP_DIR="/backups"
# Удаляем записи старше 6 месяцев из таблицы posts
PGPASSWORD=password psql -h db -U aragami -d voluntarydb -c "DELETE FROM posts WHERE created_at < NOW() - INTERVAL '6 months';"
# Удаляем бэкапы старше 2 месяцев
find "$BACKUP_DIR" -name "*.dump" -type f -mtime +60 -delete -printf "Deleted old backup: %f\n" >> /var/log/cron.log
