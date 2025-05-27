#!/bin/bash

# Удаляем записи старше 6 месяцев из таблицы posts
PGPASSWORD=password psql -h db -U aragami -d voluntarydb -f /sql/cleanup_Requests.sql
