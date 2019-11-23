# dring-spot 


## Technology overview

1. Backend:
    - .Net (.net core 3.0)
    - Tests:
        - test framework: xUnit
        - mocking library: NSubstitute
2. Frontend:
    - react-native
3. Database:
    - postresql (for now its sql server, needs to be changed to cloud storage)
## Project management
  [HeySpace](https://app.hey.space/projects/4790edab-f577-4052-a1dc-aa8e9a16054f) - task management and chat tool for the project
  
  
  Development requirements:
  -  Visual Studio Code
  -  Node.JS ver. 10+
  -  .NET Core SDK 3.0 (probably we will have to lower this to core 2.2 because AWS doesn't support core 3.0 right now)
  -  PostgreSQL
  -  ExpoClient App for Android or iOS
  
 [Doc](https://docs.google.com/document/d/1FcQTzrqknLfCmG1Na9QcXNGaWxyvVXQ0WuGzo3cTTK4/edit#heading=h.n63kq6ti6ndx) - project documentation
 [Resources](https://drive.google.com/drive/folders/1IKBgiNeSPRKz5NNEoPB7NbW4dAHJfwZV?usp=sharing) - google drive folder containing all work not related to coding.
 
 ## How to run project.
 
 ### Backend:
 For now what you need:
 visual studio code or visual studio 2019
 For visual studio code:
    

        cd [project_localization]/backend/src/DringSpot.WebApi
        code .
     
 
 Then in terminal just type

        dotnet run
 
 ### Frontend: (this is about test angular web api)
 Only visual studio code is needed:
 
        cd [project_localization]/frontend/dring-spot-test/dring-spot-test
        code .
 
 Then in terminal just type
 
        npm install
        ng serve
 
 App starts on port localhost:4200 by default.
