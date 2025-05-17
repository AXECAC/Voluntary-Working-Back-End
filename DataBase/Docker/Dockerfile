# Dockerfile
FROM postgres:latest

# Установка cron
RUN apt-get update && apt-get install -y cron

# Копирование SQL-скрипта и скрипта для cron
COPY clean_old_posts.sql /docker-entrypoint-initdb.d/
COPY setup-cron.sh /docker-entrypoint-initdb.d/

# Даем права на выполнение
RUN chmod +x /docker-entrypoint-initdb.d/setup-cron.sh
