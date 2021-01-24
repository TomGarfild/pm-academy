CREATE FUNCTION insert_user (first_name varchar, last_name varchar)
RETURNS void AS $$
    INSERT INTO users(first_name, last_name)
    VALUES (first_name, last_name);
$$ LANGUAGE SQL;