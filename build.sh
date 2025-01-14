#!/bin/bash

export NUGET_PUBLISH_PATH="/home/justin/Repos/packages"

echo Build PipelineWeaver.Ado project
dotnet build ./PipelineWeaver.Ado/PipelineWeaver.Ado.csproj
if [ $? -ne 0 ]; then
    echo "Build failed for PipelineWeaver.Ado"
    exit 1
fi

printf "\n\n"
echo Publish PipelineWeaver.Ado nuget
dotnet nuget push ./PipelineWeaver.Ado/bin/Debug/PipelineWeaver.Ado.1.0.0.nupkg --source $NUGET_PUBLISH_PATH
if [ $? -ne 0 ]; then
    echo "Nuget publish failed for PipelineWeaver.Ado"
    exit 1
fi

printf "\n\n"
echo Build PipelineWeaver.Core project
dotnet build ./PipelineWeaver.Core/PipelineWeaver.Core.csproj
if [ $? -ne 0 ]; then
    echo "Build failed for PipelineWeaver.Core"
    exit 1
fi

printf "\n\n"
echo Publish PipelineWeaver.Core nuget
dotnet nuget push ./PipelineWeaver.Core/bin/Debug/PipelineWeaver.Core.1.0.0.nupkg --source $NUGET_PUBLISH_PATH
if [ $? -ne 0 ]; then
    echo "Nuget publish failed for PipelineWeaver.Core"
    exit 1
fi

printf "\n\n"
echo Build PipelineWeaver.AdoTranspiler project
dotnet build ./PipelineWeaver.AdoTranspiler/PipelineWeaver.AdoTranspiler.csproj
if [ $? -ne 0 ]; then
    echo "Build failed for PipelineWeaver.AdoTranspiler"
    exit 1
fi

printf "\n\n"
echo Publish PipelineWeaver.AdoTranspiler nuget
dotnet nuget push ./PipelineWeaver.AdoTranspiler/bin/Debug/PipelineWeaver.AdoTranspiler.1.0.0.nupkg --source $NUGET_PUBLISH_PATH
if [ $? -ne 0 ]; then
    echo "Nuget publish failed for PipelineWeaver.AdoTranspilerClient"
    exit 1
fi

printf "\n\n"
echo Change directory to PipelineWeaver.Template
cd PipelineWeaver.Template || exit

printf "\n\n"
echo Check if the template is installed before attempting to uninstall
if dotnet new list | grep -q "PipelineWeaver"; then
    dotnet new uninstall ./
    echo "Template uninstalled."
else
    echo "Template is not installed; skipping uninstallation."
fi

printf "\n\n"
echo Install the template
dotnet new install ./
if [ $? -ne 0 ]; then
    echo "Install failed for PipelineWeaver.Template"
    exit 1
fi

printf "\n\n"
echo Go back to the root directory
cd ..

if [ -d "PipelineWeaver.Playground" ]; then
    printf "\n\n"
    echo Step 8: Delete PipelineWaver nugets from global nuget cache
    rm -rf ~/.nuget/packages/pipelineweaver.*
    echo "Deleted existing PipelineWeaver nugets from global nuget cache"
    
    printf "\n\n"
    echo Step 9: Build PipelineWeaver.Playground project
    dotnet build ./PipelineWeaver.Playground/PipelineWeaver.Playground.csproj
    if [ $? -ne 0 ]; then
        echo "Build failed for PipelineWeaver.Core"
        exit 1
    fi
fi

echo Success!