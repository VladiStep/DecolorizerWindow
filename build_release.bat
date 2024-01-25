dotnet restore
dotnet build DecolorizerWindow.csproj --no-restore
dotnet publish DecolorizerWindow.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true --output release_x64
pause