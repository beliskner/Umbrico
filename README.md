#Umbraco Microservices Example
Currently WIP, if you're running into trouble or have questions feel free to contact Wilmar

##To run the containers make sure to

1. [Download Docker Desktop](https://www.docker.com/products/docker-desktop/ "Docker desktop download link")
2. [Ensure WSL 2 is installed and enabled](https://docs.microsoft.com/en-us/windows/wsl/install-manual#step-1---enable-the-windows-subsystem-for-linux "WSL 2 install documentation")
3. Clone this repo
4. Get into the project's root folder with terminal
5. Build the correct images by running the ```docker compose build``` command
6. Once the images are built, these images can now be employed to create and run containers from them using ```docker compose up -d```
7. In the docker desktop app you should now be able to see a container group called ```umbricoservices``` that have the containers inside
8. To stop these containers from running run the command ```docker compose down```


* To access the database that is running inside of its own container you can use SQL authentication with:
    * Server name: ```localhost```
    * Username: ```SA```
    * Password ```SQL_password123```

* To access the Umbraco Backoffice head to http://localhost:5080/umbraco and login with the following credentials:
    * Username: ```w.dehoogd@gmail.com```
    * Password: ```1234567890```

The frontend of the website is reachable through ```http://localhost:5080/```

##Current state of the project
The database is running correctly and is reachable, the unattended umbraco install seems to be going wrong somewhere causing the umbraco container to constantly restart