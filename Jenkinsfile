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
        sh "dotnet build CineMoviesWeb.csproj"
        echo "BUILD STAGE HAS BEEN COMPLETED"
      }
    }
    stage("TEST")
    {
      steps
      {
        echo "Test started."
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
