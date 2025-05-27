DELETE FROM Requests WHERE (IsCompleted = TRUE OR IsFailed = TRUE) AND created_at < NOW() - INTERVAL '6 months';
