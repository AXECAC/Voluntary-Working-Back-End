#!/bin/bash

BACKUP_DIR="/backups"

# Удаляем бэкапы старше 2 месяцев
find "$BACKUP_DIR" -name "*.dump" -type f -mtime +60 -delete -printf "Deleted old backup: %f\n" >> /var/log/cron.log

