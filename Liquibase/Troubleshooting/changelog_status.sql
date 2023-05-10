-- liquibase formatted sql

-- changeset liquibase:18
create table country1 (
    id int primary key,
    name varchar(50) not null,
    city1 varchar(50),
    city2 varchar(50),
    address varchar(30)
)


-- changeset liquibase:19
create table country2 (
    id int primary key,
    name varchar(50) not null,
    city3 varchar(50),
    city4 varchar(50),
    address2 varchar(30)
)
-- rollback drop table country2