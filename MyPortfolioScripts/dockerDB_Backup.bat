@echo off
setlocal

:: Imposta variabili
set BACKUP_DIR=%~dp0MyPF_DBBackup
set DATE_TIME=%DATE:~6,4%-%DATE:~3,2%-%DATE:~0,2%_%TIME:~0,2%-%TIME:~3,2%-%TIME:~6,2%
set DATE_TIME=%DATE_TIME: =0%
set FILE_NAME=backup_%DATE_TIME%.sql

:: Crea la cartella di destinazione se non esiste
if not exist "%BACKUP_DIR%" (
    mkdir "%BACKUP_DIR%"
)

:: Esegui il backup nel container
echo Eseguo pg_dump nel container...
docker exec -u postgres myportfolio-mypf_db-1 pg_dump -U franco MyPortfolio_DataDocker -f /tmp/%FILE_NAME%

:: Copia il file dal container al PC
echo Copio il file di backup dal container alla cartella: %BACKUP_DIR%
docker cp myportfolio-mypf_db-1:/tmp/%FILE_NAME% "%BACKUP_DIR%\%FILE_NAME%"

:: (Facoltativo) Elimina il file temporaneo dal container
docker exec myportfolio-mypf_db-1 rm /tmp/%FILE_NAME%

echo Backup completato! File: %BACKUP_DIR%\%FILE_NAME%
pause
