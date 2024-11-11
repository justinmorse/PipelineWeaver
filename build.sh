#!/bin/bash


echo Build PipelineWeaver.Ado project
dotnet build ./PipelineWeaver.Ado/PipelineWeaver.Ado.csproj
if [ $? -ne 0 ]; then
    echo "Build failed for PipelineWeaver.Ado"
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
echo Build PipelineWeaver.AdoTranspilerClient project
dotnet build ./PipelineWeaver.AdoTranspilerClient/PipelineWeaver.AdoTranspilerClient.csproj
if [ $? -ne 0 ]; then
    echo "Release build failed for PipelineWeaver.AdoTranspilerClient"
    exit 1
fi

printf "\n\n"
echo Copy Transpiler artifacts to template
cp -rf ./PipelineWeaver.AdoTranspilerClient/bin/Debug/net8.0/PipelineWeaver.AdoTranspilerClient.dll ./PipelineWeaver.Template/lib
cp -rf ./PipelineWeaver.AdoTranspilerClient/bin/Debug/net8.0/PipelineWeaver.Ado.dll ./PipelineWeaver.Template/lib

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
    echo Step 7.5: Delete PipelineWeaver.Playground lib directory if it exists
    
        rm -rf PipelineWeaver.Playground/lib
        echo "Deleted existing PipelineWeaver.Playground lib directory."
    
    
    printf "\n\n"
    echo Step 8: Copy Template lib to Playground
    cp -rf ./PipelineWeaver.Template/lib ./PipelineWeaver.Playground/lib
    
    printf "\n\n"
    echo Step 9: Build PipelineWeaver.Playground project
    dotnet build ./PipelineWeaver.Playground/PipelineWeaver.Playground.csproj
    if [ $? -ne 0 ]; then
        echo "Release build failed for PipelineWeaver.Core"
        exit 1
    fi
fi

echo Success!