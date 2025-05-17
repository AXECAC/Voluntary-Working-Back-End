DELETE FROM Requests WHERE created_at < NOW() - INTERVAL '6 months';
