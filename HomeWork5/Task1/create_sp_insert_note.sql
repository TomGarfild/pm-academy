CREATE FUNCTION insert_note(id uuid,
                            header varchar,
                            body varchar,
                            user_id int)
RETURNS void AS $$
    INSERT INTO notes(id, header, body, user_id)
    VALUES (id, header, body, user_id);
$$ LANGUAGE SQL;