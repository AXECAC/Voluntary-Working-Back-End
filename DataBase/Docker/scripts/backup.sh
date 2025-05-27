#!/bin/bash

current_date=$(date +\%Y-\%m-\%d_\%H-\%M-\%S)

PGPASSWORD=password pg_dump -h db -U aragami -d voluntarydb -Fc > /sql/backups/voluntarydb_${current_date}.dump 2>> /var/log/cron.log
