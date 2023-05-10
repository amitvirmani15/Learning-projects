-- liquibase formatted sql

-- changeset activity.7:a7b.1
create table person_a7b (
    id int primary key,
    name varchar(50) not null,
    address1 varchar(50),
    address2 varchar(50),
    city varchar(30)
)
-- rollback drop table person_a7b

-- changeset activity.7:a7b.2
create table company_a7b (
    id int primary key,
    name varchar(50) not null,
    address1 varchar(50),
    address2 varchar(50),
    city varchar(30)
)
-- rollback drop table company_a7b