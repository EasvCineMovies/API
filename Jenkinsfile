pipeline {
  agent any
  triggers {
    pollSCM("@daily")
  }
  stages
  {
    stage("BUILD")
    {
      steps
      {
        sh "dotnet restore"
        sh "dotnet build CineMoviesAPI/CineMoviesAPI.csproj"
        echo "BUILD STAGE HAS BEEN COMPLETED"
      }
    }
    stage("TEST")
    {
      steps
      {
        sh "dotnet restore"
        sh "dotnet test Tests/Tests.csproj"
        echo "TEST STAGE HAS BEEN COMPLETED"
      }
    }
    stage("DEPLOY")
    {
      steps
      {
        echo "Deployment started."
      }
    }
  }
}
