--  *********************************************************************
--  SQL to roll back currently unexecuted changes
--  *********************************************************************
--  Change Log: ChangeLog-UpdateTestingRollback.sql
--  Ran at: 29/12/22, 2:17 pm
--  Against: amit@localhost@jdbc:mysql://localhost:3306/liquibasetest1
--  Liquibase version: 4.17.0
--  *********************************************************************

--  Lock Database
UPDATE liquibasetest1.DATABASECHANGELOGLOCK SET `LOCKED` = 1, LOCKEDBY = 'VIRMANIA-W01 (192.168.1.39)', LOCKGRANTED = NOW() WHERE ID = 1 AND `LOCKED` = 0;

--  Release Database Lock
UPDATE liquibasetest1.DATABASECHANGELOGLOCK SET `LOCKED` = 0, LOCKEDBY = NULL, LOCKGRANTED = NULL WHERE ID = 1;

