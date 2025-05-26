#!/bin/bash

# Удаляем записи старше 7 дней из таблицы posts
PGPASSWORD=postgres psql -h db -U aragami -d voluntarydb -c "DELETE FROM posts WHERE created_at < NOW() - INTERVAL '6 months';"
