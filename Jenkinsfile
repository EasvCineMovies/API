pipeline {
  agent any
  triggers {
    pollSCM("@daily")
  }
  stages
  {
    stage("STARTUP")
    {
      steps
      {
        dir("Tests")
        sh "rm -rf TestResults"
      }
    }
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
        dir("Tests")
        { 
          sh "dotnet add package coverlet.collector"
          sh "dotnet test --collect:'XPlat Code Coverage'"
          sh "dotnet restore"
          sh "dotnet test Tests.csproj"
          echo "TEST STAGE HAS BEEN COMPLETED"
        }
      }
      post
      {
        success
        {
          archiveArtifacts "Tests/TestResults/*/coverage.cobertura.xml"
          publishCoverage adapters: [istanbulCoberturaAdapter(path: 'Tests/TestResults/*/coverage.cobertura.xml', thresholds:
          [[failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]])], checksName: '',
          sourceFileResolver: sourceFiles('NEVER STORE')
        }
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
