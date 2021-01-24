CREATE FUNCTION select_note(note_id uuid)
RETURNS TABLE
    (
        id          uuid,
        header      varchar,
        body        varchar,
        is_deleted  boolean,
        modified_at timestamp with time zone,
        user_id     int,
        first_name  varchar,
        last_name   varchar
    ) AS
    'SELECT n.id, header, body, is_deleted, modified_at,
        user_id, first_name, last_name
    FROM notes n
    JOIN users u on u.id = n.user_id
    WHERE n.id = note_id'
LANGUAGE SQL;