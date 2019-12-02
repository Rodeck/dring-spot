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

To run backend app:
- take database backup (if you haven't done it yet) and restore it using sql serverver management studio (right click on databases and pick restore database, name it DringDB)
- Go to appsettings.developement.json in project root and change Data Source= to you'r sql server name
- Run backend:

        cd [project_localization]/backend/src/DringSpot.WebApi
        dotnet run
 
### Frontend: (this is about test angular web api)
 
- Add environment.ts file (you can find one in [Resources](https://drive.google.com/drive/folders/1IKBgiNeSPRKz5NNEoPB7NbW4dAHJfwZV?usp=sharing)
- Run app:

        > cd [project_localization]/frontend/dring-spot-web/
        > npm install
        > ng serve
 
App starts on port localhost:4200 by default.
