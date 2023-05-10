--  *********************************************************************
--  SQL to roll back currently unexecuted changes
--  *********************************************************************
--  Change Log: dbchangeloga7.xml
--  Ran at: 29/12/22, 2:39 pm
--  Against: amit@localhost@jdbc:mysql://localhost:3306/liquibasetest1
--  Liquibase version: 4.17.0
--  *********************************************************************

--  Lock Database
UPDATE liquibasetest1.DATABASECHANGELOGLOCK SET `LOCKED` = 1, LOCKEDBY = 'VIRMANIA-W01 (192.168.1.39)', LOCKGRANTED = NOW() WHERE ID = 1 AND `LOCKED` = 0;

--  Rolling Back ChangeSet: dbchangeloga7.xml::a7.3::activity.7
ALTER TABLE liquibasetest1.person_a7 DROP COLUMN country;

DELETE FROM liquibasetest1.DATABASECHANGELOG WHERE ID = 'a7.3' AND AUTHOR = 'activity.7' AND FILENAME = 'dbchangeloga7.xml';

--  Rolling Back ChangeSet: dbchangeloga7.xml::a7.2::activity.7
DROP TABLE liquibasetest1.company_a7;

DELETE FROM liquibasetest1.DATABASECHANGELOG WHERE ID = 'a7.2' AND AUTHOR = 'activity.7' AND FILENAME = 'dbchangeloga7.xml';

--  Rolling Back ChangeSet: dbchangeloga7.xml::a7.1::activity.7
DROP TABLE liquibasetest1.person_a7;

DELETE FROM liquibasetest1.DATABASECHANGELOG WHERE ID = 'a7.1' AND AUTHOR = 'activity.7' AND FILENAME = 'dbchangeloga7.xml';

--  Release Database Lock
UPDATE liquibasetest1.DATABASECHANGELOGLOCK SET `LOCKED` = 0, LOCKEDBY = NULL, LOCKGRANTED = NULL WHERE ID = 1;

