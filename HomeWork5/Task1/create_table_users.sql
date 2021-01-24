CREATE TABLE users
(
    id         serial PRIMARY KEY,
    first_name varchar(128) NOT NULL,
    last_name  varchar(128) NOT NULL
);