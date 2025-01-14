# PipelineWeaver

## WARNING: THIS PROJECT IS IN A VERY ROUGH BEGINNING STAGE
## USE AT YOUR OWN RISK

### About
PipelineWeaver is an attempt to create ADO YAML CI/CD pipelines in C# and serialize them to YAML. 

### Getting Started
1. Setting up the bash build script
	1. Open build.sh
	2. Replace NUGET_PUBLISH_PATH with your local path you'd like to use as the nuget repository
	3. Add the nuget repository to your editor. 
	4. Run build.sh
	5. The build script will automatically update a project called PipelineWeaver.Playground using the template created and added with the build script. Create a project as below woth that name to automaticaly update the nugets for testing. 
2. Working with PipelineWeaver
	1. PipelineWeaver works best with a monorepo but can be used with any sln. 
	2. To create a new PipelineWeaver project, run <dotnet new Pipelineweaver -n MyProject>. I recommend PipelineWeaver.Playground for a project name as it will updae itself with the build script. 
	3. The project will automatically have folders for Pipelines, Templates, and Scripts with samples of each type. 
	4. Check out Pipelines/MyPipeline.cs for a very basic sample of a pipelines. 
	5. When the new project is built, a new folder will be created called ADO. Within this folder your YAML pipelines will be serialized in the Pipelines folder (Templates coming later).