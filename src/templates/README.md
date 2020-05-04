# How to install template

*TODO: install template from nuget*

 1. Clone repository to your local (any temp folder will do)
   ``` 
      git clone   git@github.com:markglibres/dotnetcore-service-template.git
   ```
 2. Change directory
 ```
 cd dotnetcore-service-template
 ``` 
 4. Install using dotnet new
 ```
  dotnet new --install .\src\templates\
 ```

# How to use template
1. Create your working directory
```
mkdir MyProject
cd MyProject
```
2. Use the project template
```
dotnet new bizzpomicroservice -n <name-of-your-microservice>
```
