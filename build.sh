#!/bin/bash


echo Step 1: Build PipelineWeaver.Ado project
dotnet build ./PipelineWeaver.Ado/PipelineWeaver.Ado.csproj
if [ $? -ne 0 ]; then
    echo "Build failed for PipelineWeaver.Ado"
    exit 1
fi

printf "\n\n"
echo Step 2: Build PipelineWeaver.Core project
dotnet build ./PipelineWeaver.Core/PipelineWeaver.Core.csproj
if [ $? -ne 0 ]; then
    echo "Build failed for PipelineWeaver.Core"
    exit 1
fi

printf "\n\n"
echo Step 3: Build PipelineWeaver.BuildHook project
dotnet build ./PipelineWeaver.BuildHook/PipelineWeaver.BuildHook.csproj
if [ $? -ne 0 ]; then
    echo "Release build failed for PipelineWeaver.Core"
    exit 1
fi

printf "\n\n"
echo Step 4: Change directory to PipelineWeaver.ProjectTemplate
cd PipelineWeaver.ProjectTemplate || exit

printf "\n\n"
echo Step 5  Check if the template is installed before attempting to uninstall
if dotnet new list | grep -q "PipelineWeaver"; then
    dotnet new uninstall ./
    echo "Template uninstalled."
else
    echo "Template is not installed; skipping uninstallation."
fi

printf "\n\n"
echo Step 6  Install the template
dotnet new install ./
if [ $? -ne 0 ]; then
    echo "Install failed for PipelineWeaver.ProjectTemplate"
    exit 1
fi

printf "\n\n"
echo Step 7: Go back to the previous directory
cd ..

printf "\n\n"
echo Step 7.5: Delete PipelineWeaver.Playground directory if it exists
if [ -d "PipelineWeaver.Playground" ]; then
    rm -rf PipelineWeaver.Playground
    echo "Deleted existing PipelineWeaver.Playground directory."
fi

printf "\n\n"
echo Step 8: Create a new project named PipelineWeaver.Playground using the PipelineWeaver template
mkdir PipelineWeaver.Playground
cd PipelineWeaver.Playground || exit
dotnet new PipelineWeaver -n PipelineWeaver.Playground
if [ $? -ne 0 ]; then
    echo "Create failed for PipelineWeaver.Playground"
    exit 1
fi

printf "\n\n"
echo Step 9: Build PipelineWeaver.Playground project
cd ..
dotnet build ./PipelineWeaver.Playground/PipelineWeaver.Playground.csproj
if [ $? -ne 0 ]; then
    echo "Release build failed for PipelineWeaver.Core"
    exit 1
fi

echo Success!