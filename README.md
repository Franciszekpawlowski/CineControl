# CineControl
System do zarządzania sieciami kin oparty o architekturę mikroserwisu 

# How to run
* Prerequisites
    * Docker desktop or docker version 27.4.0
* configuration
    * For app to work set these values in system hosts file
    ```
    127.0.0.1 cinecontrol.local
    127.0.0.1 gateway.cinecontrol.local
    127.0.0.1 operatorpanel.cinecontrol.local
    127.0.0.1 adminpanel.cinecontrol.local
    ```
* Run
    ```shell 
    docker compose up
    ```