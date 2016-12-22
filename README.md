# BTnetCorePoc


### Doker

1. Install Docker toolbox
2. Usefull Docker Links: 
  * [docker tutoriallinks: bloggedbychris](http://www.bloggedbychris.com/2016/05/24/setup-visual-studio-net-docker-windows-hour/)
  * http://www.bloggedbychris.com/2016/05/24/setup-visual-studio-net-docker-windows-hour/ 
  * https://www.docker.com/what-docker
  * https://www.katacoda.com/courses/dotnet-in-docker/deploying-aspnet-core-as-docker-container
  * https://stormpath.com/blog/tutorial-deploy-asp-net-core-on-linux-with-docker

2. start Docker quickstart Terminal

3. move to project, with cd command

4. Create Dockerfile (without .txt, just **Dockerfile** as Filename)
```
FROM microsoft/dotnet:1.0.0-preview2-sdk
RUN mkdir /app
WORKDIR /app

COPY project.json /app
RUN ["dotnet", "restore"]

COPY . /app
RUN ["dotnet", "build"]

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
 
ENTRYPOINT ["dotnet", "run"]
```

* Build docker instance

```
docker build -t mydemos:aspnetcorehelloworld
```

* Run Docker

```
docker run -d -p 8080:5000 -t mydemos:aspnetcorehelloworld
```

* Show Running docker instances

```
docker ps
```

* Stop running Docker instance (ref to name on ps call)

```
docker stop app
```

* Uninstall Docker build

```
docker rm app
```

* ready to rebuild new Docker 

