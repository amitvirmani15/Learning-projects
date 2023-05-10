liquibase --url="jdbc:h2:tcp://localhost:9090/mem:%ENV%" --username=%DBUSERNAME% --password=%DBPASSWORD% tag "%BUILD%"
liquibase --url="jdbc:h2:tcp://localhost:9090/mem:%ENV%" --username=%DBUSERNAME% --password=%DBPASSWORD% --changeLogfile=mydbchangelog.xml status
liquibase --url="jdbc:h2:tcp://localhost:9090/mem:%ENV%" --username=%DBUSERNAME% --password=%DBPASSWORD% --changeLogFile=mydbchangelog.xml updateSql
liquibase --url="jdbc:h2:tcp://localhost:9090/mem:%ENV%" --username=%DBUSERNAME% --password=%DBPASSWORD% --changeLogFile=mydbchangelog.xml update