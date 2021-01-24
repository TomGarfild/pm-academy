CREATE TABLE notes
(
    id          uuid PRIMARY KEY,
    header      varchar(128)             NOT NULL,
    body        varchar(1024)            NOT NULL,
    is_deleted  boolean                  NOT NULL DEFAULT FALSE,
    user_id     int                      NOT NULL,
    modified_at timestamp with time zone NOT NULL DEFAULT current_timestamp,
    FOREIGN KEY (user_id) REFERENCES users (id)
);
CREATE INDEX ON notes(modified_at);