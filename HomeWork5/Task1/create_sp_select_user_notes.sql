CREATE FUNCTION select_user_notes(id_user int)
RETURNS TABLE
        (
            id          uuid,
            header      varchar,
            body        varchar,
            is_deleted  boolean,
            user_id     int,
            modified_at timestamp with time zone
        ) AS
    'SELECT * FROM notes n
    WHERE n.user_id = id_user AND n.is_deleted = false'
LANGUAGE SQL;