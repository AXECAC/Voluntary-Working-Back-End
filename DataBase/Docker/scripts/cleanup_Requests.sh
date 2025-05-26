#!/bin/bash

# Удаляем записи старше 6 месяцев из таблицы posts
PGPASSWORD=password psql -h db -U aragami -d voluntarydb -c "DELETE FROM posts WHERE created_at < NOW() - INTERVAL '6 months';"
