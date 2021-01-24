CREATE FUNCTION select_users_notes_count()
RETURNS TABLE
        (
            id int,
            first_name varchar,
            last_name varchar,
            count bigint
        ) AS
    'SELECT u.id, first_name, last_name, COUNT(CASE WHEN is_deleted = false THEN 1 END)
    FROM users u
    JOIN notes n on n.user_id = u.id
    GROUP BY u.id
    ORDER BY u.id'
LANGUAGE SQL;