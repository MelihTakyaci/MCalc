#!/bin/bash
# Build for Linux (adjust for other OS if needed)
dotnet publish -r linux-x64 -c Release --self-contained true -p:PublishSingleFile=true

# Check if publish was successful
if [ $? -eq 0 ]; then
  echo "Build successful. The executable is located at:"
  echo "bin/Release/net8.0/linux-x64/publish/MCalc"
else
  echo "Build failed."
fi
