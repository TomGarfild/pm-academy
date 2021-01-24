CREATE FUNCTION update_note_mark_deleted(note_id uuid)
RETURNS void AS
    'UPDATE notes n
    SET is_deleted = true,
        modified_at = current_timestamp
    WHERE n.id = note_id'
LANGUAGE SQL;